using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Data;
using ECAMSModel;
using ECAMSDataAccess;
namespace PLCControl
{
    /// <summary>
    /// 设备的基类
    /// </summary>
    public abstract class ECAMSDevBase
    {
 
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="devModel"></param>
        /// <param name="plcRW"></param>
        /// <param name="ctlInterfaceBll"></param>
        /// <param name="ctlTaskBll"></param>
        public ECAMSDevBase(ECAMSDataAccess.DeviceModel devModel, IPlcRW plcRW, DeviceBll devBll,ControlInterfaceBll ctlInterfaceBll, ControlTaskBll ctlTaskBll,LogBll logBll)
        {
            this.devModel = devModel;
            this.plcRW = plcRW;
            this.devBll = devBll;
            this.ctlInterfaceBll = ctlInterfaceBll;
            this.ctlTaskBll = ctlTaskBll;
            this.logBll = logBll;
            this.rfidRecordBll = new RfidRdRecordBll();
            this.palletTraceBll = new PalletHistoryRecordBll();
            //plcDataDb1 = new byte[devModel.BytesLenDB1];
            //plcDataDb2 = new byte[devModel.BytesLenDB2];
            //plcDataDb1Last = new byte[devModel.BytesLenDB1];
            //plcDataDb2Last = new byte[devModel.BytesLenDB2];
            this.dicCommuDataDB1 = new Dictionary<int, DevCommDatatype>();
            this.dicCommuDataDB2 = new Dictionary<int, DevCommDatatype>();
            dicDataDB1Last = new Dictionary<int, object>();
            dicDataDB2Last = new Dictionary<int, object>();
            dicDB1Read = new Dictionary<int, object>();
            //DebugMode = true; //模拟调试用
        }
      
        /// <summary>
        /// 模拟调试模式
        /// </summary>
       // public bool DebugMode { get; set; }
        public event EventHandler<LogEventArgs> eventLogDisp;

        public event EventHandler<ECAMSErrorEventArgs> eventError;
      
        #region 数据区
        public string devName = "";
        /// <summary>
        /// 运行错误，当处于这种错误状态时，禁止执行任务
        /// </summary>
        protected bool taskRunningFault = false;

        /// <summary>
        ///  任务运行错误状态描述
        /// </summary>
        protected string taskRunningFaultDescribe = "任务运行正常";

        protected ControlTaskModel currentTask = null;
        public ControlTaskModel CurrentTask
        {
            get
            {
                return currentTask;
            }
        }
        /// <summary>
        /// 当前任务执行到的步号,从1开始，初始值0表示未执行
        /// </summary>
        protected int currentTaskPhase = 0;

        /// <summary>
        /// 当前任务描述
        /// </summary>
        protected string currentTaskDescribe = "";
        public string CurrentTaskDescribe
        {
            get { return currentTaskDescribe; }
        }
        /// <summary>
        /// 任务超时判断拍数
        /// </summary>
        protected int taskTimeOutCounts = 0;
        public int TaskTimeOutCounts
        {
            get
            {
                return taskTimeOutCounts;
            }
            set
            {
                taskTimeOutCounts = value;
            }
        }

        /// <summary>
        /// 任务超时分钟数
        /// </summary>
        protected float taskTimeOutMinutes = 0;
        public float TaskTimeOutMinutes
        {
            get
            {
                return taskTimeOutMinutes;
            }
            set
            {
                taskTimeOutMinutes = value;
            }
        }
        /// <summary>
        /// 当前任务消耗的时间计数
        /// </summary>
        protected int taskElapseCounter = 0;

        /// <summary>
        /// 当前任务启动时间
        /// </summary>
        protected System.DateTime currentTaskStartTime;

        /// <summary>
        /// 当前任务已消耗的时间
        /// </summary>
        protected TimeSpan taskElapseTimespan = TimeSpan.Zero;

        /// <summary>
        /// 设备的plc读写接口
        /// </summary>
        protected IPlcRW plcRW = null;
        public IPlcRW PlcRW
        {
            get { return plcRW; }
        }

        /// <summary>
        /// 设备模型，模型数据从数据库中读取
        /// </summary>
        protected ECAMSDataAccess.DeviceModel devModel=null;


        /// <summary>
        /// 通信功能项字典，DB1
        /// </summary>
        protected IDictionary<int, DevCommDatatype> dicCommuDataDB1 = null;
        public IDictionary<int, DevCommDatatype> DicCommuDataDB1
        {
            get { return dicCommuDataDB1; }
        }
        /// <summary>
        /// 上一拍的DB1数据
        /// </summary>
        protected IDictionary<int, object> dicDataDB1Last = null;

        /// <summary>
        /// 查询到的PLC DB1内的实际数据
        /// </summary>
        protected IDictionary<int, object> dicDB1Read = null;
        /// <summary>
        /// 通信功能项字典，DB2
        /// </summary>
        protected IDictionary<int, DevCommDatatype> dicCommuDataDB2 = null;

        public IDictionary<int, DevCommDatatype> DicCommuDataDB2
        {
            get { return dicCommuDataDB2; }
        }

        /// <summary>
        /// 上一拍的DB2数据
        /// </summary>
        protected IDictionary<int, object> dicDataDB2Last = null;
     

        /// <summary>
        /// 设备数据表的业务层接口
        /// </summary>
        protected DeviceBll devBll = null;

        /// <summary>
        /// 控制层任务申请数据表的业务层接口
        /// </summary>
        protected ControlInterfaceBll ctlInterfaceBll = null;

        /// <summary>
        /// 管理层任务下发数据表的业务接口
        /// </summary>
        protected ControlTaskBll ctlTaskBll = null;

        protected RfidRdRecordBll rfidRecordBll = null;
        /// <summary>
        /// 日志数据表接口
        /// </summary>
        protected LogBll logBll = null;

        protected PalletHistoryRecordBll palletTraceBll = null;
        /// <summary>
        /// DB1数据区的锁
        /// </summary>
        private object lockDB1 = new object();

        /// <summary>
        /// DB2数据区的锁
        /// </summary>
        private object lockDB2 = new object();

        //DB1 功能项
        protected bool startWriteParam = false; //开始写入
        protected bool writeCompleted = false; //写入完成
        protected int taskCompletedReq = 1; //任务完成信息接收的应答
        protected int taskTypeSnd = 0; //任务类型号
        protected Int16 taskCodeSnd = 0; //发送的任务控制码

        //DB2 功能项

       // protected byte devReadyStatus = 0; //设备就绪状态
        protected int devErrorCode = 0; //设备当前故障码
        protected short devRunningStatus = 0; //设备工作状态
        protected bool recvTaskEnable = false; //允许接收任务
        protected bool paramRecvOK = false; //任务参数接收完成
        protected bool taskCompleted = false; //任务完成
        //  protected Int16 taskCodeBack = 0; //返回的任务控制码
        protected bool refreshStatusOK = false;
        public bool RefreshStatusOK { 
            get 
            { 
                return refreshStatusOK; 
            } 
        }
        public ECAMWCS WcsManager { get; set; }
        #endregion
        #region 内部功能方法

        /// <summary>
        /// 根据分隔符，解析地址字符串，得到地址列表
        /// </summary>
        /// <param name="addrStr"></param>
        /// <returns></returns>
        protected string[] ParsePlcDBAddr(string addrStr)
        {
            string[] splitStr = new string[]{",",";",":","-","|"};
            return addrStr.Split(splitStr,StringSplitOptions.RemoveEmptyEntries);
        }
        /// <summary>
        /// 分配DB1通信地址
        /// </summary>
        /// <returns></returns>
        protected virtual void AllocDevComAddrsDB1()
        {
            dicCommuDataDB1.Clear();
            
            int db1ID = 1;
            int plcAddrStart = int.Parse(this.devModel.DB1AddrStart.Substring(1));
            string dbName = "D";
            DevCommDatatype commData = null;

            //1.配置“开始写入”功能项
            commData = new DevCommDatatype();
            commData.CommuID = db1ID++;
            commData.CommuMethod = EnumCommMethod.PLC_MIT_COMMU;
            commData.DataByteLen = 2;
            commData.DataDescription = "2：开始写入，1:未开始写入";
            commData.DataTypeDef = EnumCommuDataType.DEVCOM_short;
            commData.Val = 0;
            commData.DataAddr = dbName+plcAddrStart.ToString();//取值为：S7:[Connection_1]DB1,INT，数据类型可变，马天牧
            plcAddrStart ++;
            dicCommuDataDB1[commData.CommuID] = commData;

            //2.配置“写入完成”功能项
            commData = new DevCommDatatype();
            commData.CommuID = db1ID++;
            commData.CommuMethod = EnumCommMethod.PLC_MIT_COMMU;
            commData.DataByteLen = 2;
            commData.DataDescription = "2：参数写入完成，1：未完成";
            commData.DataTypeDef = EnumCommuDataType.DEVCOM_short;
            commData.Val = 0;
            commData.DataAddr = dbName + plcAddrStart.ToString(); 
            plcAddrStart ++;
            dicCommuDataDB1[commData.CommuID] = commData;

            //3.配置"任务完成接收”功能项
            commData = new DevCommDatatype();
            commData.CommuID = db1ID++;
            commData.CommuMethod = EnumCommMethod.PLC_MIT_COMMU;
            commData.DataByteLen = 2;
            commData.DataDescription = "2：任务完成信息成功接收，1：未接收";
            commData.DataTypeDef = EnumCommuDataType.DEVCOM_short;
            commData.Val = 0;
            commData.DataAddr = dbName + plcAddrStart.ToString();
            plcAddrStart ++;
            dicCommuDataDB1[commData.CommuID] = commData;

            //4.配置“任务类型号”功能项
            commData = new DevCommDatatype();
            commData.CommuID = db1ID++;
            commData.CommuMethod = EnumCommMethod.PLC_MIT_COMMU;
            commData.DataByteLen = 2;
            commData.DataDescription = "任务类型号，1：装箱组盘，2：一次分拣，3：二次分拣，5:A1库电芯入库"+
            "6:A1库分容后再入库，7:A1库分容出库，8:A1库出库至一次检测"+
            "9:B1库入库,10:B1库出库至二次检测,11:B1库空料框入库,12:B1库空料框出库";
            commData.DataTypeDef = EnumCommuDataType.DEVCOM_short;
            commData.Val = 0;
            commData.DataAddr = dbName + plcAddrStart.ToString();
            plcAddrStart ++;
            dicCommuDataDB1[commData.CommuID] = commData;

        }

        protected virtual void AllocDevComAddrsDB2()
        {
            dicCommuDataDB2.Clear();
            int db2ID = 1;
            int plcAddrStart = int.Parse(this.devModel.DB2AddrStart.Substring(1));
            string dbName = "D";
            DevCommDatatype commData = null;
            //1 配置故障码
            commData = new DevCommDatatype();
            commData.CommuID = db2ID++;
            commData.CommuMethod = EnumCommMethod.PLC_MIT_COMMU;
            commData.DataByteLen = 2;
            commData.DataDescription = "设备故障码";
            commData.DataTypeDef = EnumCommuDataType.DEVCOM_short;
            commData.Val = 0;
            commData.DataAddr = dbName+plcAddrStart.ToString();
            plcAddrStart ++;
            dicCommuDataDB2[commData.CommuID] = commData;

            //2 配置设备状态
            commData = new DevCommDatatype();
            commData.CommuID = db2ID++;
            commData.CommuMethod = EnumCommMethod.PLC_MIT_COMMU;
            commData.DataByteLen = 2;
            commData.DataDescription = "设备状态，1：空闲，2：工作中，3：故障";
            commData.DataTypeDef = EnumCommuDataType.DEVCOM_short;
            commData.DataAddr = dbName + plcAddrStart.ToString();
            plcAddrStart ++;
            dicCommuDataDB2[commData.CommuID] = commData;
            if (ECAMWCS.SimMode)
            {
                commData.Val = 1;
            }
            else
            {
                commData.Val = 0;
            }

            //3 配置允许接收
            commData = new DevCommDatatype();
            commData.CommuID = db2ID++;
            commData.CommuMethod = EnumCommMethod.PLC_MIT_COMMU;
            commData.DataByteLen = 2;
            commData.DataDescription = "允许接收，1：禁止接收任务，2：允许接收";
            commData.DataTypeDef = EnumCommuDataType.DEVCOM_short;
            commData.DataAddr = dbName + plcAddrStart.ToString();
            plcAddrStart ++;
            dicCommuDataDB2[commData.CommuID] = commData;
            commData.Val = 0;

            //4 配置取数据完成
            commData = new DevCommDatatype();
            commData.CommuID = db2ID++;
            commData.CommuMethod = EnumCommMethod.PLC_MIT_COMMU;
            commData.DataByteLen = 2;
            commData.DataDescription = "2：取数据完成，1：未完成";
            commData.DataTypeDef = EnumCommuDataType.DEVCOM_short;
            commData.DataAddr = dbName + plcAddrStart.ToString();
            plcAddrStart ++;
            dicCommuDataDB2[commData.CommuID] = commData;
            commData.Val = 0;

            //5 配置任务完成
            commData = new DevCommDatatype();
            commData.CommuID = db2ID++;
            commData.CommuMethod = EnumCommMethod.PLC_MIT_COMMU;
            commData.DataByteLen = 2;
            commData.DataDescription = "1：任务未完成，2：任务完成，3：任务撤销";
            commData.DataTypeDef = EnumCommuDataType.DEVCOM_short;
            commData.DataAddr = dbName + plcAddrStart.ToString();
            plcAddrStart ++;
            dicCommuDataDB2[commData.CommuID] = commData;
            commData.Val = 0;
            
        }
        /// <summary>
        /// 分配设备的DB1，DB2地址区
        /// </summary>
        /// <returns></returns>
        protected bool AllocDevDBAddr()
        {
            try
            {
                if (devModel != null)
                {
                    //配置DB1
                    AllocDevComAddrsDB1();

                    //配置DB2
                    AllocDevComAddrsDB2();

                    for (int i = 0; i < dicCommuDataDB1.Count(); i++)
                    {

                        int commuID = i + 1;
                        DevCommDatatype commObj = dicCommuDataDB1[commuID];
                        if (commObj == null)
                        {
                            continue;
                        }
                      
                        dicDataDB1Last[commObj.CommuID] = commObj.Val;
                    }
                    if (dicCommuDataDB2.Count() > 0)
                    {
                        foreach (KeyValuePair<int, DevCommDatatype> keyVal in dicCommuDataDB2)
                        {
                            if (keyVal.Value == null)
                            {
                                continue;
                            }
                            DevCommDatatype commObj = keyVal.Value;
                            dicDataDB2Last[commObj.CommuID] = commObj.Val;
                        }

                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (System.Exception ex)
            {
                //触发错误日志,2004
                int errorCode = 2004;
                string reStr = "";
                if (!ECAMWCS.GetErrorContent(errorCode, ref reStr))
                {
                    AddLog("配置设备" + this.devModel.DeviceID + " 出现异常," + ex.Message + "," + ex.StackTrace,EnumLogType.错误);
                }
                else
                {
                    reStr = "设备：" + this.devModel.DeviceID + reStr;
                    reStr += (ex.Message + "," + ex.StackTrace);
                    OnErrorHappen(errorCode, reStr, true);
                }
                return false;
            }
        }

        /// <summary>
        /// 增加一条日志记录
        /// </summary>
        public void AddLog(LogModel log, EnumLogType logType)
        {
            if (eventLogDisp != null)
            {
                LogEventArgs arg = new LogEventArgs();
                // arg.happenTime = System.DateTime.Now;
                // arg.logMes = log.logContent;
                arg.LogTime = System.DateTime.Now;
                arg.LogCate = EnumLogCategory.控制层日志;
                arg.LogContent = log.logContent;
                arg.LogType = logType;
                eventLogDisp.Invoke(this, arg);
            }

        }
        public void AddLog(string content, EnumLogType logType)
        {
            LogModel log = new LogModel();
            log.logCategory = EnumLogCategory.控制层日志.ToString();
            log.logContent = content;
            log.logType = logType.ToString();
            log.logTime = System.DateTime.Now;
            AddLog(log, logType);
        }
        private void OnErrorHappen(int errorCode,string errorDescribe, bool stopRunning)
        {
            if (eventError != null)
            {
                ECAMSErrorEventArgs args = new ECAMSErrorEventArgs();
                args.ErrorCode = errorCode;
                
                args.LogContent = errorDescribe;
                args.RequireRunningStop = stopRunning;
                args.LogTime = System.DateTime.Now;
                eventError.Invoke(this, args);
            }
        }
        /// <summary>
        /// 通信数据转换成设备状态(DB2)
        /// </summary>
        protected virtual void CommuDataToDevStatusDB2()
        {
            int errCode = int.Parse(this.dicCommuDataDB2[1].Val.ToString());
            if (this.devErrorCode != errCode)
            {
                //报警
                this.devErrorCode = errCode;
            }
            devRunningStatus = short.Parse(this.dicCommuDataDB2[2].Val.ToString());
            byte val = byte.Parse(this.dicCommuDataDB2[3].Val.ToString());
            if (val == 2)
            {
                recvTaskEnable = true;
            }
            else
            {
                recvTaskEnable = false;
            }
            val = byte.Parse(this.dicCommuDataDB2[4].Val.ToString());
            if (val == 2)
            {
                paramRecvOK = true;
            }
            else
            {
                paramRecvOK = false;
            }

            val = byte.Parse(this.dicCommuDataDB2[5].Val.ToString());
            if (val == 2 )
            {
                taskCompleted = true;
            }
            else
            {
                taskCompleted = false;
            }
            
        }

        /// <summary>
        /// 控制命令功能项（DB1）转化成通信数据
        /// </summary>
        protected virtual void CmdToCommuDataDB1()
        {
            if (startWriteParam)
            {
                this.dicCommuDataDB1[1].Val = 2;
            }
            else
            {
                this.dicCommuDataDB1[1].Val = 1;
            }
            if (writeCompleted)
            {
                this.dicCommuDataDB1[2].Val = 2;
            }
            else
            {
                this.dicCommuDataDB1[2].Val = 1;
            }
           
            
            this.dicCommuDataDB1[3].Val = taskCompletedReq;
           
            this.dicCommuDataDB1[4].Val = taskTypeSnd;
          
        }

        protected virtual void DevCmdReset()
        {
            for (int i = 1; i < dicCommuDataDB1.Count() + 1; i++)
            {
                dicCommuDataDB1[i].Val = 0;
            }
            this.startWriteParam = false; 
            this.writeCompleted = false; 
            this.taskCompletedReq = 1;
            this.taskTypeSnd = 0; 
           // this.taskCodeSnd = 0;
            
            DevCmdCommit();
        }
        /// <summary>
        /// 判断系统是否存在未完成的任务类型
        /// </summary>
        /// <param name="taskName"></param>
        /// <param name="taskCat"></param>
        /// <param name="devCode"></param>
        /// <returns></returns>
        protected bool SysExistUnCompletedTask(EnumTaskName taskName, EnumTaskCategory taskCat, string devCode)
        {
            string strWhere = "InterfaceType='" + taskCat.ToString() + "' and DeviceCode='" + devCode + "'and InterfaceStatus='" +
                   EnumCtrlInterStatus.未生成.ToString() + "' ";
            List<ControlInterfaceModel> taskApplyList = ctlInterfaceBll.GetModelList(strWhere);

            strWhere = "TaskTypeName='" + taskName.ToString() + "' and TaskStatus<>'" + EnumTaskStatus.已完成.ToString() + "' and TaskStatus<>'"+EnumTaskStatus.任务撤销.ToString()+"' ";
            List<ControlTaskModel> existTaskList = ctlTaskBll.GetModelList(strWhere);
            if ((taskApplyList == null || taskApplyList.Count() < 1) && (existTaskList == null || existTaskList.Count() < 1))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion
        #region 属性
        public ECAMSDataAccess.DeviceModel DevModel
        {
            get
            {
                return devModel;
            }
            private set { }
        }
        
        //public string[] AddrDB1
        //{
        //    get
        //    {
        //        return plcAddrDb1;
        //    }
        //    set
        //    {
        //        plcAddrDb1 = value;
        //    }
        //}
        //public string[] AddrDB2
        //{
        //    get
        //    {
        //        return plcAddrDb2;
        //    }
        //    set
        //    {
        //        plcAddrDb2 = value;
        //    }
        //}
        //public byte[] DB1
        //{
        //    get
        //    {
        //        return plcDataDb1;
        //    }
        //    private set { }
        //}
        //public byte[] DB2
        //{
        //    get
        //    {
        //        return plcDataDb2;
        //    }
        //    private set { }
        //}
        #endregion
        #region 公共方法

        public bool Init()
        {
            string reStr = "";
          
            //分配地址
            if (!AllocDevDBAddr())
            {
                return false;
            }
            //系统启动后，先把DB1数据读上来
            if (this.devModel.DeviceID == "1001")
            {
                plcRW.StationNumber = 0x01;
            }
            else if (this.devModel.DeviceID == "1002")
            {
                plcRW.StationNumber = 0x02;
            }

            short[] db1Vals = null;
            if (dicCommuDataDB1.Count() > 0)
            {
                if (!plcRW.ReadMultiDB(devModel.DB1AddrStart, dicCommuDataDB1.Count(), ref db1Vals))
                {
                    AddLog("恢复设备命令失败", EnumLogType.错误);
                    return false;
                }
                for (int i = 0; i < dicCommuDataDB1.Count(); i++)
                {

                    int commuID = i + 1;
                    DevCommDatatype commObj = dicCommuDataDB1[commuID];
                    if (commObj == null)
                    {
                        continue;
                    }
                    commObj.Val = db1Vals[i];
                    dicDataDB1Last[commObj.CommuID] = commObj.Val;
                }
            }
            if (this.devModel.DeviceType == EnumDevType.堆垛机.ToString() || this.devModel.DeviceType == EnumDevType.机械手.ToString())
            {
                byte val = byte.Parse(this.dicCommuDataDB1[1].Val.ToString());
                if (val  == 2)
                {
                    startWriteParam = true;
                }
                else
                {
                    startWriteParam = false;
                }
                val = byte.Parse(this.dicCommuDataDB1[2].Val.ToString());
                if (val == 2)
                {
                    writeCompleted = true;
                }
                else
                {
                    writeCompleted = false;
                }
                val = byte.Parse(this.dicCommuDataDB1[3].Val.ToString());

                taskCompletedReq = val;

                taskTypeSnd = int.Parse(this.dicCommuDataDB1[4].Val.ToString());

            }

            if (!ReadDB2())
            {
                AddLog(devName + " 读取设备状态数据失败", EnumLogType.错误);
                return false;
            }
            //上拍数据初始化成和当前值相同
            if (dicCommuDataDB2.Count() > 0)
            {
                foreach (KeyValuePair<int, DevCommDatatype> keyVal in dicCommuDataDB2)
                {
                    if (keyVal.Value == null)
                    {
                        continue;
                    }
                    DevCommDatatype commObj = keyVal.Value;
                    dicDataDB2Last[commObj.CommuID] = commObj.Val;
                }
            
            }
            
            if (!DevStatusRestore(ref reStr))
            {
                string errStr = "";
                int errCode = 2105;
                if (!ECAMWCS.GetErrorContent(errCode, ref errStr))
                {
                    AddLog(devName + " 恢复状态出现错误", EnumLogType.错误);
                }
                else
                {
                    errStr = devName+ reStr;
                    OnErrorHappen(errCode, errStr, false);
                }
                return false;
            }
            return true;
        }
        public bool ReadDB1()
        {
            if (!ECAMWCS.SimMode)
            {
                if (!plcRW.IsConnect)
                {
                    AddLog(devName + " 通信未连接", EnumLogType.错误);
                    return false;
                }
                if (this.devModel.DeviceID == "1001")
                {
                    plcRW.StationNumber = 0x01;
                }
                else if (this.devModel.DeviceID == "1002")
                {
                    plcRW.StationNumber = 0x02;
                }
                int blockNum = this.dicCommuDataDB1.Count();
                if (this.dicCommuDataDB1.Count() > 0)
                {
                    short[] vals = null;
                    if (!plcRW.ReadMultiDB(this.devModel.DB1AddrStart, blockNum, ref vals))
                    {
                        return false;
                    }
                    for (int i = 0; i < blockNum; i++)
                    {
                        int commID = i + 1;
                        this.dicDB1Read[commID] = vals[i];
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// 读DB2数据
        /// </summary>
        /// <returns></returns>
        public bool ReadDB2()
        {
            lock (lockDB2)
            {
                if (!ECAMWCS.SimMode)
                {
                    //if (!plcRW.IsConnect)
                    //{
                    //    //AddLog(devName + " 设备通信未连接", EnumLogType.错误);
                    //    return false;
                    //}
                    if (this.devModel.DeviceID == "1001")
                    {
                        plcRW.StationNumber = 0x01;
                    }
                    else if (this.devModel.DeviceID == "1002")
                    {
                        plcRW.StationNumber = 0x02;
                    }
                    int blockNum = this.dicCommuDataDB2.Count();
                    //if (this.devModel.DeviceType == EnumDevType.堆垛机.ToString())
                    //{
                    //    for (int i = 0; i < blockNum; i++)
                    //    {
                    //        int commID = i + 1;
                    //        int val = 0;
                    //        if (!plcRW.ReadDB(this.dicCommuDataDB2[commID].DataAddr, ref val))
                    //        {
                    //            return false;
                    //        }
                    //        this.dicCommuDataDB2[commID].Val = val;
                    //    }
                    //}
                    //else
                    {
                        if (this.dicCommuDataDB2.Count() > 0)
                        {
                            short[] vals = null;
                            if (!plcRW.ReadMultiDB(this.devModel.DB2AddrStart, blockNum, ref vals))
                            {
                                refreshStatusOK = false;
                                return false;
                            }
                            else
                            {
                                refreshStatusOK = true;
                            }
                            for (int i = 0; i < blockNum; i++)
                            {
                                int commID = i + 1;
                                this.dicCommuDataDB2[commID].Val = vals[i];
                            }
                        }

                    }

                }

                //通信数据转化变量，设备状态
                CommuDataToDevStatusDB2();
            }
            return true;
        }
        /// <summary>
        /// 如有更新，设备指令发送，写入DB1
        /// </summary>
        /// <returns></returns>
        public bool DevCmdCommit()
        {
           
            if (plcRW == null)
            {
                return false;
            }
            //把变量转换成通信数据
            lock (lockDB1)
            {
                //把变量转换成通信数据
                if (this.devModel.DeviceID == "1001")
                {
                    plcRW.StationNumber = 0x01;
                }
                else if (this.devModel.DeviceID == "1002")
                {
                    plcRW.StationNumber = 0x02;
                }
                CmdToCommuDataDB1();
                foreach (KeyValuePair<int, DevCommDatatype> keyVal in dicCommuDataDB1)
                {
                    if (keyVal.Value == null)
                    {
                        continue;
                    }
                    DevCommDatatype commObj = keyVal.Value;
                   // if (commObj.Val.ToString() != dicDB1Read[commObj.CommuID].ToString())
                    if (commObj.Val.ToString() != dicDataDB1Last[commObj.CommuID].ToString())
                    {
                        int val = int.Parse(commObj.Val.ToString());
                        if (!plcRW.WriteDB(commObj.DataAddr,val))
                        {
                            AddLog(devName + "发送命令数据失败,通信可能中断", EnumLogType.错误);
                            return false;
                        }
                        dicDataDB1Last[commObj.CommuID] = commObj.Val;
                    }

                }
            }
            return true;
        }

        /// <summary>
        /// 清理当前正在运行的任务
        /// </summary>
        /// <returns></returns>
        public bool ClearRunningTask()
        {
            //if (devModel.DeviceType == EnumDevType.机械手.ToString())
            //{
            //    if (this.currentTask != null)
            //    {
            //        if (this.ctlTaskBll.Exists(this.currentTask.ControlTaskID))
            //        {
            //            this.currentTask.TaskStatus = EnumTaskStatus.已完成.ToString();
            //            if (!ctlTaskBll.Update(this.currentTask))
            //            {
            //                AddLog(devName + "更新任务状态失败,TaskID:" + this.currentTask.TaskID, EnumLogType.错误);
            //                return false;
            //            }
            //        }
                   
            //    }
            //}

            this.currentTask = null;
            this.currentTaskPhase = 0;
            return true;
        }

        //public IList<string> GetDB1Data()
        //{
        //    IList<string> commDataList = new List<string>();
        //    commDataList.Add(plcDataDb1[0].ToString());
        //    commDataList.Add(startWriteParam.ToString());
        //    commDataList.Add(writeCompleted.ToString());
        //    commDataList.Add(taskCompletedReq.ToString());
        //    commDataList.Add(taskTypeSnd.ToString());
        //    commDataList.Add(taskCodeSnd.ToString());
        //    for (int i = 7; i < devModel.BytesLenDB1; i++)
        //    {
        //        commDataList.Add(plcDataDb1[i].ToString());
        //    }
        //    return commDataList;
        //}
        //public IList<string> GetDB2Data()
        //{
        //    IList<string> commDataList = new List<string>();
        //    commDataList.Add(plcDataDb2[0].ToString());
        //    commDataList.Add(devRunningStatus.ToString());
        //    commDataList.Add(recvTaskEnable.ToString());
        //    commDataList.Add(paramRecvOK.ToString());
        //    commDataList.Add(taskCompleted.ToString());
        //    commDataList.Add(taskCodeBack.ToString());
        //    for (int i = 7; i < plcDataDb2.Count(); i++)
        //    {
        //        commDataList.Add(plcDataDb2[i].ToString());
        //    }
        //    return commDataList;
        //}
        public DataTable GetDB1DataDetail()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("索引");
            dt.Columns.Add("地址");
            dt.Columns.Add("内容");
            dt.Columns.Add("描述");
            int index = 1;
           // lock (lockDB1)
            {
                for (int i = 0; i < dicCommuDataDB1.Count(); i++)
                {
                    DevCommDatatype commObj = dicCommuDataDB1[i + 1];
                    dt.Rows.Add(index++, commObj.DataAddr, commObj.Val, commObj.DataDescription);
                }
            }
      
            return dt;
        }
        public DataTable GetDB2DataDetail()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("索引");
            dt.Columns.Add("地址");
            dt.Columns.Add("内容");
            dt.Columns.Add("描述");
            int index = 1;
          //  lock (lockDB2)
            {
                for (int i = 0; i < dicCommuDataDB2.Count(); i++)
                {
                    DevCommDatatype commObj = dicCommuDataDB2[i + 1];

                    dt.Rows.Add(index++, commObj.DataAddr, commObj.Val, commObj.DataDescription);
                }
            }

            return dt;

        }
        public bool ClearDevCmd()
        {

            DevCmdReset();
            return DevCmdCommit();
        }

        /// <summary>
        /// 获取当前任务的详细信息
        /// </summary>
        /// <returns></returns>
        public string GetRunningTaskDetail()
        {
            if (currentTask == null)
            {
                return "当前任务为空";
            }
            string taskInfo = "当前任务运行到第"+currentTaskPhase.ToString()+" 步;";

            taskInfo += currentTaskDescribe;
            return taskInfo;

        }
        /// <summary>
        /// 处理业务
        /// </summary>
        /// <returns></returns>
        public virtual bool ExeBusiness()
        {
            
            if (currentTask == null || currentTask.TaskStatus == EnumTaskStatus.待执行.ToString())
            {
                return true;
            }
            //判断是否有任务撤销
            if (currentTask != null && currentTaskPhase < 5)
            {
                int taskFinish = int.Parse(dicCommuDataDB2[5].Val.ToString());
                int taskRes = int.Parse(dicCommuDataDB1[3].Val.ToString());
                if (taskFinish == 3 && taskRes != 3)
                {
                    //任务清理
                    ClearDevCmd();

                    currentTask.TaskStatus = EnumTaskStatus.任务撤销.ToString();
                    ctlTaskBll.Update(currentTask);

                    currentTaskDescribe = "任务撤销，等待'撤销信号'复位";
                    taskCompletedReq = 3;
                    if (!DevCmdCommit())
                    {
                        AddLog(devName + "发送命令失败", EnumLogType.错误);
                        return false;
                    }
                   // AddLog(devName + ",Test:任务撤销，发送‘任务完成应答信息:" + dicCommuDataDB1[3].Val.ToString(), EnumLogType.调试信息);
                    return true;
                }
                if (taskFinish == 1 && taskRes == 3)
                {

                    taskCompletedReq = 1;
                    if (!DevCmdCommit())
                    {
                        AddLog(devName + "发送命令失败", EnumLogType.错误);
                        return false;
                    }
                    //AddLog(devName + ",Test:任务撤销应答复位，发送‘任务完成应答信息:" + dicCommuDataDB1[3].Val.ToString(), EnumLogType.调试信息);
                    currentTaskPhase = 0;
                    currentTask = null;
                    currentTaskDescribe = "当前无任务";
                    return true;
                }
            }


            //if (currentTask != null && (taskElapseCounter > taskTimeOutCounts))
            taskElapseTimespan = System.DateTime.Now - currentTaskStartTime;
            if (taskElapseTimespan.TotalMinutes > taskTimeOutMinutes)
            {
                currentTaskStartTime = System.DateTime.Now;
                if (currentTask.TaskStatus == EnumTaskStatus.执行中.ToString())
                {
                    currentTask.TaskStatus = EnumTaskStatus.超时.ToString();
                    ctlTaskBll.Update(currentTask);
                }
                //任务超时，错误码2202
             
                int errCode = 2202;
                string reStr = "";
                if (!ECAMWCS.GetErrorContent(errCode, ref reStr))
                {
                    reStr = devName+ "控制任务ID：" + currentTask.ControlTaskID + "任务超时";
                    AddLog(reStr, EnumLogType.错误);
                }
                else
                {
                    reStr = (devName+ "控制任务ID："+currentTask.ControlTaskID+" 执行任务超时");
                    OnErrorHappen(errCode,reStr,false);
                }
            }
            return true;
        }
         /// <summary>
        /// 刷新设备状态
        /// </summary>
        public virtual void DevRefreshStatus()
        {
            switch (devRunningStatus)
            {
                case 1:
                    {
                        //空闲
                        if (this.devModel.DeviceStatus != EnumDevStatus.空闲.ToString())
                        {
                            this.devModel.DeviceStatus = EnumDevStatus.空闲.ToString();
                            //更新数据库
                            this.devBll.UpdateDevStatus(this.devModel.DeviceID, this.devModel.DeviceStatus);

                        }
                        break;
                    }
                case 2:
                    {
                        //工作中
                        if (this.devModel.DeviceStatus != EnumDevStatus.工作中.ToString())
                        {
                            this.devModel.DeviceStatus = EnumDevStatus.工作中.ToString();
                            //更新数据库
                            this.devBll.UpdateDevStatus(this.devModel.DeviceID, this.devModel.DeviceStatus);
                        }
                        break;
                    }
                case 3:
                    {
                        //故障

                        if (this.devModel.DeviceStatus != EnumDevStatus.故障.ToString())
                        {
                            this.devModel.DeviceStatus = EnumDevStatus.故障.ToString();
                            this.devBll.UpdateDevStatus(this.devModel.DeviceID, this.devModel.DeviceStatus);
                            //错误，2104
                            int errorCode = 2104;
                            string errStr = "";
                            if (!ECAMWCS.GetErrorContent(errorCode, ref errStr))
                            {
                                AddLog(devName+" 发生故障", EnumLogType.错误);
                            }
                            else
                            {
                                errStr = devName + errStr;
                                OnErrorHappen(errorCode, errStr, false);

                            }

                        }
                        break;
                    }
                default:
                    break;
            }
           
        }
        ///// <summary>
        /////一致性检查 
        ///// </summary>
        ///// <param name="errStr"></param>
        ///// <returns></returns>
        //public virtual bool DevStatusRestore(ref string errStr)
        //{
        //    return true;
        //}

        /// <summary>
        /// 恢复系统关闭前的设备状态，包括任务，执行到的阶段
        /// </summary>
        public virtual bool DevStatusRestore(ref string errStr)
        {
            
            if (currentTask != null && currentTask.TaskStatus != EnumTaskStatus.错误.ToString())
            {
                currentTaskStartTime = System.DateTime.Now;
            }
            return true;
        }
        public void SaveLastLoopData()
        {
            //上拍数据初始化成和当前值相同
            foreach (KeyValuePair<int, DevCommDatatype> keyVal in dicCommuDataDB2)
            {
                if (keyVal.Value == null)
                {
                    continue;
                }
                DevCommDatatype commObj = keyVal.Value;
                dicDataDB2Last[commObj.CommuID] = commObj.Val;
            }
        }

        public bool SetDB2ItemValue(int itemID, string valStr)
        {
            if (!dicCommuDataDB2.Keys.Contains(itemID))
            {
                return false;
            }
            DevCommDatatype commObj = dicCommuDataDB2[itemID];
            if (commObj == null)
            {
                return false;
            }
            commObj.Val = valStr;
            return true;
        }
        #endregion
       
    }
}
