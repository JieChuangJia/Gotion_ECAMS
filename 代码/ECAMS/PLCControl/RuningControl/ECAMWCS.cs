using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using SygoleHFReaderIF;
using ECAMSModel;
using ECAMSDataAccess;
using System.Configuration;

namespace PLCControl
{
   
    /// <summary>
    /// WCS控制类，作为底层设备的控制代理，静态类
    /// </summary>
    public  class ECAMWCS
    {
        public ECAMWCS()
        {
            DevDicList = new Dictionary<string, ECAMSDevBase>();
            devBll = new DeviceBll();
            ctlInterfaceBll = new ControlInterfaceBll();
            ctlTaskBll = new ControlTaskBll();
            logBll = new LogBll();
        }
     
        #region 数据区
        private Thread sysWorkingThread = null;
        private Thread devMonitorThread = null;
        private Thread ocvRfidIOThread = null; //ocv工位读卡服务线程
        private Thread XYZGriperThread5001 = null;
        private Thread XYZGriperThread5002 = null;
        private Thread XYZGriperThread5003 = null;
        private int devMonitorInverval = 500;

        private int scanInterval = 300;
        private int fxPlcScanInterval = 500;
        private int ocvRfidInterval = 300;
        private bool exitRunning = false;
        private bool exitDevMonitor = false;
        private bool exitOCVRfid = false;
        private bool pauseFlag = false;

        private IDictionary<int, IPlcRW> plcDic = null;
        public IDictionary<int, IPlcRW> PlcDic
        {
            get
            {
                return plcDic;
            }
        }
        private IPlcRW plcRW1 = null;
        public IPlcRW PlcRW1
        {
            get
            {
                return plcRW1;
            }
        }
        private IPlcRW plcRW2 = null;
        public IPlcRW PlcRW2
        {
            get
            {
                return plcRW2;
            }
        }
        private IPlcRW plcRW3 = null;
        public IPlcRW PlcRW3
        {
            get
            {
                return plcRW3;
            }
        }
        private IPlcRW stackerPlcRW = null; //A1库小车PLC接口
        private IPlcRW stackerPlcRW2 = null; //B1库小车PLC接口
        public IPlcRW StackerPlcRW
        {
            get
            {
                return stackerPlcRW;
            }
            set
            {
                stackerPlcRW = value;
            }
        }
        /// <summary>
        /// FX PLC通信断开时的时间
        /// </summary>
        private System.DateTime fxCommDiscntTime;
        /// <summary>
        /// FX PLC通信恢复的时间间隔
        /// </summary>
        public float FxCommResumeInterval = 3.0f;

        /// <summary>
        /// 设备列表
        /// </summary>
        private  IDictionary<string,ECAMSDevBase> DevDicList = null;

        /// <summary>
        /// ocv 检测机 rfid读写器列表
        /// </summary>
        private IList<OCVRfidRead> devOCVRfidList = null;
        

        private DeviceBll devBll = null;
        private ControlInterfaceBll ctlInterfaceBll = null;
        private ControlTaskBll ctlTaskBll = null;
        private LogBll logBll = null;
      //  private EventHandler<DebugLogEventArgs> eventLogDisp = null;

        private EventHandler<LogEventArgs> eventLogDisp = null;
        private EventHandler<LogEventArgs> logHandler = null;
        private EventHandler<ECAMSErrorEventArgs> eventError = null;
        private EventHandler<ECAMSErrorEventArgs> errorHandler = null;

        /// <summary>
        /// 通信设备状态刷新事件
        /// </summary>
        private EventHandler<CommDeviceEventArgs> eventCommDevStatus = null;

        /// <summary>
        /// 设备状态刷新事件
        /// </summary>
        private EventHandler<DeviceStatusEventArgs> eventDevStatus = null;
        /// <summary>
        /// 读写接口列表，一个接口对应一个读写器
        /// </summary>
        public static IDictionary<int,IrfidRW> rfidRWDic = null;
        /// <summary>
        /// 读写器端口列表，一个端口可以带多个读写设备
        /// </summary>
        public static HFReaderIF[] RfidReaderPorts = null;
        /// <summary>
        /// 三个入口处电芯料框（非空）ID缓存的队列
        /// </summary>
        public static Dictionary<string, Queue<string>> PalletInputDeque = null;
        private object portinDequeLock = new object();
        //需要动态配置的数据
        
        public static bool DebugMode = true;
        public static bool SimMode = true;
        public static string PlcIP = "192.168.1.1";
      
        public static int PlcPort = 6000;
        /// <summary>
        /// 报警信息配置
        /// </summary>
        public static IDictionary<int, Man_WarnnigConfigModel> DicWarnCfg = null;

        public static string userName = "";
        private System.Windows.Forms.Timer timerSys = null;
        private object plcAutoReadLock = new object();
        #endregion
      

        /// <summary>
        /// 订阅控制层日志事件
        /// </summary>
        /// <param name="handler"></param>
        public void AttachLogHandler(EventHandler<LogEventArgs> handler)
        {
            //for(int i=0;i<DevDicList.Count();i++)
            //{
            //    DevDicList.ElementAt(i).Value.eventLogDisp += handler;
            //}
            eventLogDisp += handler;
            this.logHandler = handler;
        }

        /// <summary>
        /// 订阅控制层错误事件
        /// </summary>
        /// <param name="handler"></param>
        public void AttachErrorHandler(EventHandler<ECAMSErrorEventArgs> handler)
        {
            //for (int i = 0; i < DevDicList.Count(); i++)
            //{
            //    DevDicList.ElementAt(i).Value.eventError += handler;
            //}
            eventError += handler;
            this.errorHandler = handler;
        }

        public void AttachCommDevStatusHandler(EventHandler<CommDeviceEventArgs> handler)
        {
            eventCommDevStatus += handler;
        }
        public void AttachDevStatusHandler(EventHandler<DeviceStatusEventArgs> handler)
        {
            eventDevStatus += handler;
        }
        #region 公共接口

        /// <summary>
        /// WCS初始化
        /// </summary>
        /// <param name="resultStr">返回信息</param>
        /// <returns>是否成功</returns>
        public bool WCSInit(ref string reStr)
        {
            
            try
            {
                AddLog("控制层开始初始化...",EnumLogType.提示);
                float taskTimeOutMins = float.Parse(ConfigurationManager.AppSettings["taskTimeOut"]);
                int taskTimeOutCounts = (int)(taskTimeOutMins * 60.0f * 1000.0f / scanInterval);
                this.FxCommResumeInterval = float.Parse(ConfigurationManager.AppSettings["FxCommResumeInterval"]);
                if (this.FxCommResumeInterval < 0.1f)
                {
                    this.FxCommResumeInterval = 0.1f;
                }
                if (this.FxCommResumeInterval > 10.0f)
                {
                    this.FxCommResumeInterval = 10.0f;
                }
                string debugModestr = ConfigurationManager.AppSettings["CtlDebugMode"];
                string simModestr = ConfigurationManager.AppSettings["CtlSimMode"];
                if (debugModestr.ToUpper() == "TRUE")
                {
                    ECAMWCS.DebugMode = true;
                }
                else
                {
                    ECAMWCS.DebugMode = false;
                }
                if (simModestr.ToUpper() == "TRUE")
                {
                    ECAMWCS.SimMode = true;
                }
                else
                {
                    ECAMWCS.SimMode = false;
                }

                //1 变量初始化
                IPlcRW plcRW = null;
                rfidRWDic = new Dictionary<int, IrfidRW>();
                plcDic = new Dictionary<int, IPlcRW>();

                if (ECAMWCS.SimMode)
                {
                    
                    stackerPlcRW = new PlcRWSim();
                    stackerPlcRW2 = new PlcRWSim();
                    plcRW1 = new PlcRWSim();
                    for (int i = 0; i < 8; i++)
                    {
                        int rfidReaderID = i + 1;
                        rfidRWSim rfidSimObj = new rfidRWSim();
                        rfidSimObj.ReaderID = (byte)rfidReaderID;
                        rfidRWDic[rfidReaderID] = rfidSimObj;
                    }
                    for (int i = 1; i < 6; i++)
                    {
                        plcRW = new PlcRWSim();
                        plcRW.PlcID = i;
                        plcDic[plcRW.PlcID] = plcRW;
                    }
                }
                else
                {
                    
                    for (int i = 1; i < 4; i++)
                    {
                        plcRW = new PLCRWNet();
                       // plcRW = new PLCRW();
                        plcRW.PlcID = i;
                        plcDic[plcRW.PlcID] = plcRW;
                        plcRW.Init();
                    }
                    plcRW1 = plcDic[1];
                    stackerPlcRW = new PlcRW485BD();
                    stackerPlcRW2 = new PlcRW485BD();
                    stackerPlcRW.PlcID = 4;
                    stackerPlcRW2.PlcID = 5;
                    plcDic[stackerPlcRW.PlcID] = stackerPlcRW;
                    for (int i = 0; i < 8; i++)
                    {
                        int rfidReaderID = i + 1;
                        rfidRWDic[rfidReaderID] = new SgrfidRW((byte)rfidReaderID);
                    }

                }
                PalletInputDeque = new Dictionary<string, Queue<string>>();
                PalletInputDeque["2002"] = new Queue<string>();
                PalletInputDeque["2004"] = new Queue<string>();
                PalletInputDeque["2006"] = new Queue<string>();

                if (!RestorePalletInputQueue())
                {
                    AddLog("恢复立库入口处缓存料框信息失败", EnumLogType.错误);
                    return false;
                }
               
                //2 查询数据库，建立报警码配置字典
                if (DicWarnCfg == null)
                {
                    DicWarnCfg = new Dictionary<int, Man_WarnnigConfigModel>();
                    Man_WarnnigConfigBll warnCfgBll = new Man_WarnnigConfigBll();
                    string strWhere = "WarningLayer= '" + EnumWarnLayer.控制层.ToString() + "' ";
                    IList<Man_WarnnigConfigModel> warnCfgList = warnCfgBll.GetModelList(strWhere);
                    foreach (Man_WarnnigConfigModel warnCfgModel in warnCfgList)
                    {
                        if (warnCfgModel != null)
                        {
                            DicWarnCfg[warnCfgModel.WarningCode] = warnCfgModel;
                        }
                    }
                }

                //3 读配置文件，配置设备连接参数
                if (!ECAMWCS.SimMode)
                {
                    ECAMWCS.PlcIP = ConfigurationManager.AppSettings["plcIP"];
                    ECAMWCS.PlcPort = int.Parse(ConfigurationManager.AppSettings["plcPort"]);
                    int rfidPortNum = int.Parse(ConfigurationManager.AppSettings["RFIDComPortNum"]);
                    ECAMWCS.RfidReaderPorts = new HFReaderIF[rfidPortNum];
                    string rfidPortAlloc = ConfigurationManager.AppSettings["RfidComAlloc"];//所有的COM口
                    string[] rfidComPorts = ECAMWCS.SplitStrings(rfidPortAlloc);
                    if (rfidComPorts != null)
                    {
                        for (int i = 0; i < Math.Min(rfidComPorts.Count(), rfidPortNum); i++)
                        {
                            HFReaderIF readerIF = new HFReaderIF();
                            readerIF.ComPort = rfidComPorts[i];
                            if (!readerIF.OpenComport(ref reStr))
                            {
                                AddLog("读卡器打开端口失败," + reStr, EnumLogType.错误);
                               // return false;
                            }
                            ECAMWCS.RfidReaderPorts[i] = readerIF;
                            string keyStr = "RfidCom" + (i + 1).ToString() + "LoadDevs";
                            string portLoadDevs = ConfigurationManager.AppSettings[keyStr];
                            string[] devIDs = ECAMWCS.SplitStrings(portLoadDevs);
                            if (devIDs == null || devIDs.Count() < 1)
                            {
                                continue;
                            }
                            for (int idIndex = 0; idIndex < devIDs.Count(); idIndex++)
                            {
                                int readerID = int.Parse(devIDs[idIndex]);
                                SgrfidRW sgRfid = rfidRWDic[readerID] as SgrfidRW;
                                sgRfid.ReaderIF = readerIF;
                                sgRfid.ReaderID = (byte)readerID;
                                if (!sgRfid.Connect())
                                {
                                    AddLog(readerID.ToString() + " 号读卡器连接失败", EnumLogType.错误);
                                    //return false;
                                }
                            }
                        }
                    }
                }
               
                //4 连接PLC
                 
                for (int i = 1; i < 4; i++)
                {
                    string plcAddrKey = "plcIP" + i.ToString();
                    string ip = ConfigurationManager.AppSettings[plcAddrKey];
                    string plcAddr = ip + ":" + ECAMWCS.PlcPort;
                    plcRW = plcDic[i];
                    if (!plcRW.ConnectPLC(plcAddr, ref reStr))
                    {
                        //错误,2102
                        // int errorCode = 2102;
                        string strLog = string.Format("{0} 号机械手PLC连接失败", i);
                        AddLog(strLog, EnumLogType.错误);
                        //if (!GetErrorContent(errorCode, ref reStr))
                        //{
                        //    AddLog("PLC 通信连接失败",EnumLogType.错误);
                        //}
                        //else
                        //{
                        //    reStr = "和总控PLC通信连接失败";
                        //    OnErrorHappen(errorCode, reStr, true);
                        //}
                        //return false;//test
                    }
                    plcRW.eventLinkLost += this.PlcLostConnectHandler;

                }
               
                //plcRW1.eventLinkLost += this.PlcLostConnectHandler;

                //堆垛机PLC连接
                string stackerPlcCom = ConfigurationManager.AppSettings["stackerPlcCom"];
                string stackerPlcCom2 = ConfigurationManager.AppSettings["stackerPlcCom2"];

                string DatagrmWaiteTime = ConfigurationManager.AppSettings["FXDatagrmWaitTime"];
                string stackerPlcCommInterval = ConfigurationManager.AppSettings["FXPlcScanInterval"];
                string stackerPlcRecvInterval = ConfigurationManager.AppSettings["FXRecvInterval"];

                if(!ECAMWCS.SimMode)
                {
                    byte dgrmWait = byte.Parse(DatagrmWaiteTime);
                   
                    
                    if (dgrmWait < 0x30)
                    {
                        dgrmWait = 0x30;
                    }
                    if (dgrmWait > 0x39)
                    {
                        dgrmWait = 0x39;
                    }
                    int fxRecvInterval = int.Parse(stackerPlcRecvInterval);
                    if (fxRecvInterval < 0)
                    {
                        fxRecvInterval = 0;
                    }
                    if (fxRecvInterval > 10)
                    {
                        fxRecvInterval = 10;
                    }
                    (stackerPlcRW as PlcRW485BD).datagrmWaitTime =dgrmWait;
                    (stackerPlcRW as PlcRW485BD).recvInterval = fxRecvInterval;
                }
                fxPlcScanInterval = int.Parse(stackerPlcCommInterval);
                if (fxPlcScanInterval < 300)
                {
                    fxPlcScanInterval = 300;
                }
                if (fxPlcScanInterval > 2000)
                {
                    fxPlcScanInterval = 2000;
                }
                if (!stackerPlcRW.ConnectPLC(stackerPlcCom, ref reStr))
                {
                    stackerPlcRW.CloseConnect();
                    this.fxCommDiscntTime = System.DateTime.Now;
                    AddLog(reStr, EnumLogType.错误);
                    AddLog("A1库堆垛机plc 通信端口关闭", EnumLogType.错误);
                }
                else
                {
                    AddLog(reStr, EnumLogType.提示);
                }
                if (!stackerPlcRW2.ConnectPLC(stackerPlcCom2, ref reStr))
                {
                    stackerPlcRW2.CloseConnect();
                    this.fxCommDiscntTime = System.DateTime.Now;
                    AddLog(reStr, EnumLogType.错误);
                    AddLog("B1库堆垛机plc 通信端口关闭", EnumLogType.错误);
                }
                stackerPlcRW.eventLinkLost += StackerPlcLostConnectHandler;
                stackerPlcRW2.eventLinkLost += StackerPlcLostConnectHandler;
                //5 设备列表
                IList<DeviceModel> devList = devBll.GetModelList(" ");
                foreach (DeviceModel devME in devList)
                {
                    if (devME == null)
                    {
                        continue;
                    }
                    ECAMSDevBase ecamsDev = null;
                    if (devME.DeviceType == EnumDevType.堆垛机.ToString())
                    {
                        if (devME.DeviceID == "1001")
                        {
                            ecamsDev = new ECAMSStacker(devME, stackerPlcRW, devBll, ctlInterfaceBll, ctlTaskBll, logBll);
                        }
                        else if (devME.DeviceID == "1002")
                        {
                            ecamsDev = new ECAMSStacker(devME, stackerPlcRW2, devBll, ctlInterfaceBll, ctlTaskBll, logBll);
                        }
                    }
                    else if (devME.DeviceType == EnumDevType.机械手.ToString())
                    {
                       
                        switch (devME.DeviceID)
                        {
                            case "5001":
                                {
                                    plcRW = plcDic[1];
                                    ecamsDev = new ECAMSXYZGriper(devME, plcRW, devBll, ctlInterfaceBll, ctlTaskBll, logBll);
                                    break;
                                }

                            case "5002":
                                {
                                    plcRW = plcDic[2];
                                    ecamsDev = new ECAMSXYZGriper(devME, plcRW, devBll, ctlInterfaceBll, ctlTaskBll, logBll);
                                    break;
                                }
                            case "5003":
                                {
                                    plcRW = plcDic[3];
                                    ecamsDev = new ECAMSXYZGriper(devME, plcRW, devBll, ctlInterfaceBll, ctlTaskBll, logBll);
                                    break;
                                }
                            default:
                                break;
                        }
                    }
                    else if (devME.DeviceType == EnumDevType.站台.ToString() && devME.BytesLenDB1 > 0 && devME.BytesLenDB2 > 0)
                    {
                        plcRW = null;
                        switch (devME.DeviceID)
                        {
                            case "2002":
                            case "2003":
                            case "2004":
                                {
                                    plcRW = plcDic[1]; 
                                    ecamsDev = new ECAMSTransPort(devME, plcRW, devBll, ctlInterfaceBll, ctlTaskBll, logBll);
                                    break;
                                }
                            case "2005":
                            case "2006":
                            case "2009":
                                {
                                    plcRW = plcDic[2];
                                    ecamsDev = new ECAMSTransPort(devME, plcRW, devBll, ctlInterfaceBll, ctlTaskBll, logBll);
                                    break;
                                }
                            case "2007":
                            case "2008":
                                {
                                    plcRW = plcDic[3];
                                    ecamsDev = new ECAMSTransPort(devME, plcRW, devBll, ctlInterfaceBll, ctlTaskBll, logBll);
                                    break;
                                }
                            default:
                                break;
                        }

                    }
                    else
                    {
                        ecamsDev = null;
                        continue;
                    }
                    //ecamsDev.DebugMode = ECAMWCS.DebugMode;
                    ecamsDev.WcsManager = this;
                    ecamsDev.TaskTimeOutCounts = taskTimeOutCounts;
                    ecamsDev.TaskTimeOutMinutes = taskTimeOutMins;
                    ecamsDev.eventLogDisp += this.logHandler;
                    ecamsDev.eventError += this.errorHandler;
                    plcRW1.eventLogDisp += this.logHandler;
                    stackerPlcRW.eventLogDisp += this.logHandler;
                    if (!ecamsDev.Init())
                    {
                        AddLog(ecamsDev.devName + " 初始化失败", EnumLogType.错误);
                        ////错误，2103
                        //int errorCode = 2103;
                        //if (!GetErrorContent(errorCode, ref reStr))
                        //{
                        //    AddLog(ecamsDev.devName+" 初始化失败",EnumLogType.错误);
                        //}
                        //else
                        //{
                        //    reStr = ecamsDev.devName +"初始化失败";
                        //    OnErrorHappen(errorCode, reStr, true);
                        //}
                       // return false; //test
                    }
                    DevDicList[devME.DeviceID] = ecamsDev;
                    string checkStr = string.Empty;
                    if (!ecamsDev.DevStatusRestore(ref checkStr))
                    {
                        AddLog(checkStr,EnumLogType.错误);
                    }
                }

                devOCVRfidList = new List<OCVRfidRead>();
                OCVRfidRead ocvReader = new OCVRfidRead(ECAMWCS.rfidRWDic[4]);
                ocvReader.ocvRfidName = "OCV3 读卡器";
                ocvReader.PortDev = DevDicList["2005"] as ECAMSTransPort;
                devOCVRfidList.Add(ocvReader);
                ocvReader = new OCVRfidRead(ECAMWCS.rfidRWDic[7]);
                ocvReader.ocvRfidName = "OCV4 读卡器";
                ocvReader.PortDev = DevDicList["2007"] as ECAMSTransPort;
                devOCVRfidList.Add(ocvReader);

                //2 
                sysWorkingThread = new Thread(new ThreadStart(SysWorkingProc));
                sysWorkingThread.IsBackground = true;
                sysWorkingThread.Name = "WCS主线程";

                XYZGriperThread5001 = new Thread(new ThreadStart(XYZ5001Proc));
                XYZGriperThread5001.IsBackground = true;
                XYZGriperThread5001.Name = "机械手1的任务线程";

                XYZGriperThread5002 = new Thread(new ThreadStart(XYZ5002Proc));
                XYZGriperThread5002.IsBackground = true;
                XYZGriperThread5002.Name = "机械手2的任务线程";

                XYZGriperThread5003 = new Thread(new ThreadStart(XYZ5003Proc));
                XYZGriperThread5003.IsBackground = true;
                XYZGriperThread5003.Name = "机械手1的任务线程";

                //timerSys = new System.Windows.Forms.Timer();
                //timerSys.Interval = scanInterval;
                //timerSys.Tick += TimerSysWorkingProc;
              

                devMonitorThread = new Thread(new ThreadStart(DevMonitorProc));
                devMonitorThread.IsBackground = true;
                devMonitorThread.Name = "通信设备状态监控线程";
                //devMonitorThread.Start();

                ocvRfidIOThread = new Thread(new ThreadStart(OCVRfidProc));
                ocvRfidIOThread.IsBackground = true;
                ocvRfidIOThread.Name = "OCV 检测机读卡服务线程";
                ocvRfidIOThread.Start();
                return true;
            }
            catch (System.Exception ex)
            {
                //异常，加错误日志,2001
                string errorContent = "";
                int errorCode = 2001;
                if (!GetErrorContent(errorCode, ref errorContent))
                {
                    AddLog("控制层初始化出现异常," +ex.ToString(),EnumLogType.错误);
                }
                else
                {
                    errorContent += (ex.Message + "," + ex.ToString());
                    OnErrorHappen(errorCode, errorContent, true);
                }
                return false;
            }
            finally
            {
                AddLog("控制层初始化完毕！",EnumLogType.提示);
            }
            
        }

        /// <summary>
        /// WCS层启动
        /// </summary>
        /// <param name="resultStr">返回信息</param>
        /// <returns>是否成功</returns>
        public bool WCSStart(ref string reStr)
        {
            try
            {
                if (devMonitorThread.ThreadState == (ThreadState.Unstarted | ThreadState.Background))
                {
                    devMonitorThread.Start();
                }
                if (sysWorkingThread.ThreadState == (ThreadState.Unstarted | ThreadState.Background))
                {
                    sysWorkingThread.Start();
                }
                if (XYZGriperThread5001.ThreadState == (ThreadState.Unstarted | ThreadState.Background))
                {
                    XYZGriperThread5001.Start();
                }
                if (XYZGriperThread5002.ThreadState == (ThreadState.Unstarted | ThreadState.Background))
                {
                    XYZGriperThread5002.Start();
                }
                if (XYZGriperThread5003.ThreadState == (ThreadState.Unstarted | ThreadState.Background))
                {
                    XYZGriperThread5003.Start();
                }
                pauseFlag = false;
                
                //timerSys.Start();
                reStr = "控制层启动成功";
                return true;
            }
            catch (System.Exception ex)
            {
                //启动异常，2002

                int errorCode = 2002;

                if (!GetErrorContent(errorCode, ref reStr))
                {
                    AddLog("控制层启动出现异常," + ex.Message + "," + ex.StackTrace,EnumLogType.错误);
                }
                else
                {

                    reStr += (ex.Message + "," + ex.StackTrace);
                    OnErrorHappen(errorCode, reStr, true);
                }
                return false;
            }

            
        }

        /// <summary>
        /// WCS停止
        /// </summary>
        /// <param name="resultStr"></param>
        /// <returns></returns>
        public bool WCSStop(ref string resultStr)
        {
            
            pauseFlag = true;
           // timerSys.Stop();
            resultStr = "控制层停止";
            return true;
          
        }
        public bool WCSExit(ref string resultStr)
        {
            this.exitRunning = true;
            this.exitDevMonitor = true;
            this.exitOCVRfid = true;
            if (this.sysWorkingThread != null && (this.sysWorkingThread.ThreadState == (ThreadState.Running | ThreadState.Background)))
            {
                if (!this.sysWorkingThread.Join(2000))
                {
                    this.sysWorkingThread.Abort();
                }
            }

            if (this.XYZGriperThread5001 != null && (this.XYZGriperThread5001.ThreadState == (ThreadState.Running | ThreadState.Background)))
            {
                if (!this.XYZGriperThread5001.Join(2000))
                {
                    this.XYZGriperThread5001.Abort();
                }
            }
            if (this.XYZGriperThread5002 != null && (this.XYZGriperThread5002.ThreadState == (ThreadState.Running | ThreadState.Background)))
            {
                if (!this.XYZGriperThread5002.Join(2000))
                {
                    this.XYZGriperThread5002.Abort();
                }
            }
            if (this.XYZGriperThread5003 != null && (this.XYZGriperThread5003.ThreadState == (ThreadState.Running | ThreadState.Background)))
            {
                if (!this.XYZGriperThread5003.Join(2000))
                {
                    this.XYZGriperThread5003.Abort();
                }
            }
           // devMonitorThread.Join();
            if (this.devMonitorThread.ThreadState == (ThreadState.Running | ThreadState.Background))
            {
                if (!devMonitorThread.Join(2000))
                {
                    devMonitorThread.Abort();
                }
            }
            if (this.ocvRfidIOThread.ThreadState == (ThreadState.Running | ThreadState.Background))
            {
                if (!this.ocvRfidIOThread.Join(2000))
                {
                    this.ocvRfidIOThread.Abort();
                }
            }
            for (int i = 1; i < 4; i++)
            {
                plcDic[i].Exit();
            }
            resultStr = "控制层退出";
            return true;
        }
        public bool WCSClosePLCComm(EnumDevPLC plcID,ref string reStr)
        {
            try
            {
                switch (plcID)
                {
                    case EnumDevPLC.PLC_STACKER_FX:
                        {
                            if (!stackerPlcRW.CloseConnect())
                            {
                                reStr = "A1库堆垛机PLC关闭失败";
                                return false;
                            }
                            if (!stackerPlcRW2.CloseConnect())
                            {
                                reStr = "B1库堆垛机PLC关闭失败";
                                return false;
                            }
                            reStr = "PLC通信关闭";
                            break;
                        }
                    case EnumDevPLC.PLC_MASTER_Q:
                        {
                            if(!plcRW1.CloseConnect())
                            {
                                reStr = "机械手1 PLC关闭失败";
                                return false;
                            }
                            if(!plcRW2.CloseConnect())
                            {
                                 reStr = "机械手2 PLC关闭失败";
                                 return false;
                            }
                            if (!plcRW3.CloseConnect())
                            {
                                reStr = "机械手3 PLC关闭失败";
                                return false;
                            }
                            reStr = "PLC通信关闭";
                            break;
                        }
                    default:
                        break;
                }
            }
            catch (System.Exception ex)
            {
                reStr += "关闭PLC通信遇到异常：" + ex.ToString();
                return false;
            }
            return true;
        }
        public bool WCSReOpenPLCComm(EnumDevPLC plcID, ref string reStr)
        {
            try
            {
                string stackerPlcCom = ConfigurationManager.AppSettings["stackerPlcCom"];
                string plcAddr = ECAMWCS.PlcIP + ":" + ECAMWCS.PlcPort;
                switch (plcID)
                {
                    case EnumDevPLC.PLC_STACKER_FX:
                        {
                            if (!stackerPlcRW.CloseConnect())
                            {
                                reStr = "A1库堆垛机PLC关闭失败";
                                return false;
                            }
                            if (!stackerPlcRW2.CloseConnect())
                            {
                                reStr = "B1库堆垛机PLC关闭失败";
                                return false;
                            }

                            if (!stackerPlcRW.ConnectPLC(stackerPlcCom,ref reStr))
                            {
                                AddLog(reStr, EnumLogType.错误);
                                return false;
                            }
                            if (!stackerPlcRW2.ConnectPLC(stackerPlcCom, ref reStr))
                            {
                                AddLog(reStr, EnumLogType.错误);
                                return false;
                            }
                           
                            break;
                        }
                    case EnumDevPLC.PLC_MASTER_Q:
                        {
                            if (plcRW1.CloseConnect())
                            {
                                reStr = "关闭失败";
                                return false;
                            }
                            if (!plcRW1.ConnectPLC(plcAddr, ref reStr))
                            {
                                AddLog(reStr, EnumLogType.错误);
                                return false;
                            }
                            
                            break;
                        }
                    default:
                        break;
                }
            }
            catch (System.Exception ex)
            {
                reStr += "重新打开PLC通信遇到异常：" + ex.ToString();
                return false;
            }
            return true;
        }
        /// <summary>
        /// 根据设备号获取设备对象
        /// </summary>
        /// <param name="devID"></param>
        /// <returns></returns>
        public ECAMSDevBase GetDev(string devID)
        {
            if (DevDicList != null)
            {
                if (DevDicList.Keys.Contains(devID))
                {
                    return DevDicList[devID];
                }

            }
            return null;
        }

        /// <summary>
        /// 清理设备任务
        /// </summary>
        /// <param name="devID"></param>
        /// <returns></returns>
        public bool ClearDevTask(string devID)
        {
            ECAMSDevBase dev = GetDev(devID);
            if (dev == null)
            {
                return false;
            }
            bool re = dev.ClearRunningTask();
            if (!re)
            {
                return re;
            }
            re = dev.ClearDevCmd();
            return re;
        }

        /// <summary>
        /// 清除任务
        /// </summary>
        /// <param name="taskToDel"></param>
        /// <returns></returns>
        public bool ClearTask(ControlTaskModel taskToDel)
        {
            foreach (KeyValuePair<string, ECAMSDevBase> keyVal in DevDicList)
            {
                if (keyVal.Value == null)
                {
                    continue;
                }

                ECAMSDevBase dev = keyVal.Value;
                if (dev.CurrentTask != null && dev.CurrentTask.ControlTaskID == taskToDel.ControlTaskID)
                {
                    if (ClearDevTask(dev.DevModel.DeviceID))
                    {
                        AddLog("设备：" + dev.DevModel.DeviceID + "清理控制任务ID：" + taskToDel.ControlTaskID + " 成功!",EnumLogType.错误);
                        return true;
                    }
                   
                }
            }
           
            AddLog("清理控制任务ID：" + taskToDel.ControlTaskID + " 失败,未找到执行该任务的设备，可能数据错误！",EnumLogType.错误);
             
            return true;
        }
        /// <summary>
        /// 获取设备当前运行信息
        /// </summary>
        /// <param name="devID">设备ID</param>
        /// <param name="taskinfoDt">设备当前任务信息</param>
        /// <param name="db1Dt">设备通信DB1数据</param>
        /// <param name="db2Dt">设备通信DB2数据</param>
        /// <returns></returns>
        public bool GetDevRunningInfo(string devID,ref DataTable taskinfoDt,ref DataTable db1Dt,ref DataTable db2Dt)
        {
            if (!DevDicList.Keys.Contains(devID))
            {
                return false;
            }
            ECAMSDevBase dev = DevDicList[devID];
            //任务
            taskinfoDt = new DataTable();
            taskinfoDt.Columns.Add("索引");
            taskinfoDt.Columns.Add("名称");
            taskinfoDt.Columns.Add("内容");
          
            int rowIndex = 1;
            DataRow dr = taskinfoDt.NewRow();
            dr[0] = (rowIndex++).ToString();
            dr[1] = "设备描述";
            dr[2] = dev.DevModel.DeviceDescribe;
            taskinfoDt.Rows.Add(dr);

            dr = taskinfoDt.NewRow();
            dr[0] = (rowIndex++).ToString();
            dr[1] = "设备状态";
            dr[2] = dev.DevModel.DeviceStatus;
            taskinfoDt.Rows.Add(dr);

            dr = taskinfoDt.NewRow();
            dr[0] = (rowIndex++).ToString();
            dr[1] = "当前任务";
            if (dev.CurrentTask == null)
            {
                dr[2] = "无";
                taskinfoDt.Rows.Add(dr);
            }
            else
            {
                dr[2] = dev.CurrentTask.TaskTypeName;
                taskinfoDt.Rows.Add(dr);
                dr = taskinfoDt.NewRow();
                dr[0] = (rowIndex++).ToString();
                dr[1] = "任务状态";
                dr[2] = dev.CurrentTask.TaskStatus;
                taskinfoDt.Rows.Add(dr);

                dr = taskinfoDt.NewRow();
                dr[0] = (rowIndex++).ToString();
                dr[1] = "任务详细信息";
                dr[2] = dev.GetRunningTaskDetail();
                taskinfoDt.Rows.Add(dr);
            }

            db1Dt = dev.GetDB1DataDetail();
            db2Dt = dev.GetDB2DataDetail();
            //db1
            //IList<string> db1ValList = dev.GetDB1Data();
            
            //db1Dt = new DataTable();
            //db1Dt.Columns.Add("索引");
            //db1Dt.Columns.Add("内容");
            //db1Dt.Columns.Add("描述");
            //int index = 1;
            //foreach (string valStr in db1ValList)
            //{
            //    dr = db1Dt.NewRow();
            //    dr[0] = index;
            //    dr[1] = valStr;
            //    db1Dt.Rows.Add(dr);
            //    index++;

            //}
            ////db2
            //index = 1;
            //IList<string> db2ValList = dev.GetDB2Data();
            //db2Dt = new DataTable();
            //db2Dt.Columns.Add("索引");
            //db2Dt.Columns.Add("内容");
            //db2Dt.Columns.Add("描述");
            //foreach (string valStr in db2ValList)
            //{
            //    dr = db2Dt.NewRow();
            //    dr[0] = index;
            //    dr[1] = valStr;
            //    db2Dt.Rows.Add(dr);
            //    index++;
            //}
            return true;
        }
        /// 取得当前源码的哪一行
        /// </summary>
        /// <returns></returns>
        public static int GetLineNum()
        {
            System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace(1, true);
            return st.GetFrame(0).GetFileLineNumber();
        }

        /// <summary>
        /// 取当前源码的源文件名
        /// </summary>
        /// <returns></returns>
        public static string GetCurSourceFileName()
        {
            System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace(1, true);

            return st.GetFrame(0).GetFileName();

        }
        /// <summary>
        /// 根据报警号获得内容
        /// </summary>
        /// <param name="warningCode"></param>
        /// <param name="errContent"></param>
        /// <returns></returns>
        public static bool GetErrorContent(int warningCode, ref string errContent)
        {
            if (DicWarnCfg == null || (!DicWarnCfg.Keys.Contains(warningCode)))
            {
                return false;
            }
            Man_WarnnigConfigModel warnCfg = DicWarnCfg[warningCode];
            errContent = warnCfg.WarningLayer + "," + warnCfg.WarningCata + "," + warnCfg.WarningName + "," + warnCfg.WarningExplain;
            return true;
        }
        //public static IrfidRW GetrfidRW(int rfidIndex)
        //{
        //    if (rfidRWDic.Count() < 1 || rfidIndex < 0 || rfidIndex >= rfidRWDic.Count())
        //    {
        //        return null;
        //    }
        //    return rfidRWDic[rfidIndex];
        //}
        public static string[] SplitStrings(string strSource)
        {
            string[] splitStr = new string[] { ",", ";", ":", "-", "|" };
            return strSource.Split(splitStr, StringSplitOptions.RemoveEmptyEntries);
        }
        #endregion
        #region 私有功能
        private void OCVRfidProc()
        {
            string reStr = "";
            while (!exitOCVRfid)
            {
                Thread.Sleep(ocvRfidInterval);
                try
                {
                    foreach (OCVRfidRead ocvRfid in devOCVRfidList)
                    {
                        if (ocvRfid != null)
                        {
                            if (!ocvRfid.ExeOCVRfidBusiness(ref reStr))
                            {
                                AddLog(ocvRfid.ocvRfidName + ",读RFID卡失败:" + reStr, EnumLogType.错误);
                            }
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    AddLog("OCV读卡出现异常:" + ex.ToString(), EnumLogType.错误);
                }
               
            }
        }
        private void DevMonitorProc()
        {
            while (!exitDevMonitor)
            {
                Thread.Sleep(devMonitorInverval);
                if (pauseFlag)
                {
                    continue;
                }
                try
                {
                    //foreach (KeyValuePair<string, ECAMSDevBase> keyVal in DevDicList)
                    //{
                    //    if (keyVal.Value == null)
                    //    {
                    //        continue;
                    //    }

                    //    ECAMSDevBase dev = keyVal.Value;
                    //    if (dev.DevModel.DeviceType != EnumDevType.站台.ToString())
                    //    {
                    //        continue;
                    //    }
                    //    if (!dev.ReadDB2())
                    //    {
                    //        //读设备DB2状态出现错误，在日志显示
                    //        AddLog(dev.devName + " 读取状态数据失败", EnumLogType.错误);
                    //        continue;

                    //    }

                    //    dev.DevRefreshStatus();
                    //    if (!dev.DevCmdCommit())
                    //    {

                    //        AddLog(dev.devName + " 发送设备指令出现错误", EnumLogType.错误);
                    //        continue;
                    //    }
                    //}
                    foreach (KeyValuePair<string, ECAMSDevBase> keyVal in DevDicList)
                    {
                        if (keyVal.Value == null)
                        {
                            continue;
                        }

                        ECAMSDevBase dev = keyVal.Value;
                        if (dev.DevModel.DeviceType == EnumDevType.堆垛机.ToString())
                        {
                            ECAMSStacker stacker = dev as ECAMSStacker;
                            stacker.ExeBusinesAtInPut();
                        }
                    }

                  
                    //执行设备类状态
                    DataTable devDt = new DataTable();
                    devDt.Columns.Add("设备号");
                    devDt.Columns.Add("设备名称");
                    devDt.Columns.Add("当前任务信息");
                    devDt.Columns.Add("设备故障码");
                   // index = 1;
                    foreach (KeyValuePair<string, ECAMSDevBase> keyVal in DevDicList)
                    {
                        if (keyVal.Value == null)
                        {
                            continue;
                        }

                        ECAMSDevBase dev = keyVal.Value;
                        //if (dev.DevModel.DeviceType == EnumDevType.堆垛机.ToString())
                        //{
                        //    ECAMSStacker stacker = dev as ECAMSStacker;
                        //    stacker.ExeBusinesAtInPut();
                        //}
                        if (dev.DevModel.DeviceType == EnumDevType.机械手.ToString() || dev.DevModel.DeviceType == EnumDevType.堆垛机.ToString())
                        {
                            devDt.Rows.Add(dev.DevModel.DeviceID, dev.DevModel.DeviceDescribe, dev.CurrentTaskDescribe, dev.DicCommuDataDB2[1].Val.ToString());

                        }
                    }
                    if (this.eventDevStatus != null)
                    {
                        DeviceStatusEventArgs args = new DeviceStatusEventArgs();
                        args.DtStatus = devDt;
                        eventDevStatus.Invoke(this, args);
                    }
                }
                catch (System.Exception ex)
                {
                    //自动运行异常，3003
                    int errorCode = 2003;
                    string reStr = "";
                    if (!GetErrorContent(errorCode, ref reStr))
                    {
                        AddLog("控制层自动运行出现异常," + ex.Message + "," + ex.StackTrace, EnumLogType.错误);
                    }
                    else
                    {

                        reStr += (ex.Message + "," + ex.StackTrace);
                        OnErrorHappen(errorCode, reStr, true);
                    }
                }
                
            }
        }

        private void SysWorkingProc()
        {
            
            while (!exitRunning)
            {
                Thread.Sleep(fxPlcScanInterval);
               
                if (pauseFlag)
                {
                    continue;
                }
                
                try
                {
                   // AddLog("扫描周期开始：", EnumLogType.提示);
                    //遍历设备的接口
                    foreach (KeyValuePair<string, ECAMSDevBase> keyVal in DevDicList)
                    {
                        if (keyVal.Value == null)
                        {
                            continue;
                        }
                        
                        ECAMSDevBase dev = keyVal.Value;
                        if (dev.DevModel.DeviceType != EnumDevType.堆垛机.ToString())
                        {
                            continue;
                        }
                        
                        int scanRepeats = 1;

                        //若设备任务已启动，则连续扫描PLC状态，尽快的把命令参数发送到PLC
                        if (dev.DevModel.DeviceStatus == EnumDevStatus.工作中.ToString())
                        {
                            scanRepeats = 4;
                        }
                        else
                        {
                            scanRepeats = 1;
                        }
                        for (int i = 0; i < scanRepeats; i++)
                        {
                            if (!dev.ReadDB2())
                            {
                                //读设备DB2状态出现错误，在日志显示
                                AddLog(dev.devName + " 读取状态数据失败", EnumLogType.错误);
                                
                                continue;
                                
                            }
                            //Thread.Sleep(100);
                            dev.DevRefreshStatus();
                            if (!dev.ExeBusiness())
                            {

                                AddLog(dev.devName + " 执行控制任务出现错误", EnumLogType.错误);
                                continue;
                            }
                            if (!dev.DevCmdCommit())
                            {

                                AddLog(dev.devName + " 发送设备指令出现错误", EnumLogType.错误);
                                continue;
                            }

                        }
                        if (!SavePalletInputQueue())
                        {
                            OnErrorHappen(2105, "保存立库入口处料框缓存信息失败", true );
                        }
                        dev.SaveLastLoopData();
                    }
                  //  AddLog("扫描周期结束：", EnumLogType.提示);
                }
                catch (System.Exception ex)
                {
                    //自动运行异常，3003

                    int errorCode = 2003;
                    string reStr = "";
                    if (!GetErrorContent(errorCode, ref reStr))
                    {
                        AddLog("控制层自动运行出现异常," + ex.Message + "," + ex.StackTrace, EnumLogType.错误);
                    }
                    else
                    {

                        reStr += (ex.Message + "," + ex.StackTrace);
                        OnErrorHappen(errorCode, reStr, true);
                    }
                }
                
            }

        }
        private void XYZ5001Proc()
        {
            string[] portList = new string[]{"2002","2003","2004"};
            while (!exitRunning)
            {
                Thread.Sleep(scanInterval);

                if (pauseFlag)
                {
                    continue;
                }
                try
                {
                    for (int i = 0; i < portList.Count(); i++)
                    {
                        string portID = portList[i];
                        ECAMSDevBase portDev = DevDicList[portID];
                        if (!portDev.ReadDB2())
                        {
                            //读设备DB2状态出现错误，在日志显示
                            AddLog(portDev.devName + " 读取状态数据失败", EnumLogType.错误);
                            continue;

                        }

                        portDev.DevRefreshStatus();
                        if (!portDev.DevCmdCommit())
                        {

                            AddLog(portDev.devName + " 发送设备指令出现错误", EnumLogType.错误);
                            continue;
                        }
                    }
                    ECAMSXYZGriper grisper = DevDicList["5001"] as ECAMSXYZGriper;
                    if (!grisper.PlcRW.IsConnect)
                    {
                        AddLog(grisper.devName + " 通信未连接", EnumLogType.错误);
                        continue;
                    }
                   
                    if (!grisper.ReadDB2())
                    {
                        //读设备DB2状态出现错误，在日志显示
                        AddLog(grisper.devName + " 读取状态数据失败", EnumLogType.错误);
                        continue;        
                    }
                   
                    grisper.DevRefreshStatus();
                    if (!grisper.ExeBusiness())
                    {

                        AddLog(grisper.devName + " 执行控制任务出现错误", EnumLogType.错误);
                        continue;
                    }
                    if (!grisper.DevCmdCommit())
                    {

                        AddLog(grisper.devName +",机械手1发送设备指令出现错误", EnumLogType.错误);
                        
                    }
                    grisper.SaveLastLoopData();
                }
                catch (System.Exception ex)
                {
                    //自动运行异常，3003

                    int errorCode = 2003;
                    string reStr = "";
                    if (!GetErrorContent(errorCode, ref reStr))
                    {
                        AddLog("机械手1自动运行出现异常," + ex.Message + "," + ex.StackTrace, EnumLogType.错误);
                    }
                    else
                    {

                        reStr += (ex.Message + "," + ex.StackTrace);
                        reStr += "机械手1任务执行异常," + reStr;
                        OnErrorHappen(errorCode, reStr, true);
                    }
                }
            }
        }
        private void XYZ5002Proc()
        {
            string[] portList = new string[] { "2005", "2006", "2009" };
            while (!exitRunning)
            {

                Thread.Sleep(scanInterval);

                if (pauseFlag)
                {
                    continue;
                }
                try
                {
                    for (int i = 0; i < portList.Count(); i++)
                    {
                        string portID = portList[i];
                        ECAMSDevBase portDev = DevDicList[portID];
                        if (!portDev.ReadDB2())
                        {
                            //读设备DB2状态出现错误，在日志显示
                            AddLog(portDev.devName + " 读取状态数据失败", EnumLogType.错误);
                            continue;

                        }

                        portDev.DevRefreshStatus();
                        if (!portDev.DevCmdCommit())
                        {

                            AddLog(portDev.devName + " 发送设备指令出现错误", EnumLogType.错误);
                            continue;
                        }
                    }

                    ECAMSXYZGriper grisper = DevDicList["5002"] as ECAMSXYZGriper;
                    
                    if (!grisper.ReadDB2())
                    {
                        //读设备DB2状态出现错误，在日志显示
                        AddLog(grisper.devName +" 读取状态数据失败", EnumLogType.错误);
                        continue;
                    }

                    grisper.DevRefreshStatus();
                    if (!grisper.ExeBusiness())
                    {

                        AddLog(grisper.devName +" 执行控制任务出现错误", EnumLogType.错误);
                    }
                    if (!grisper.DevCmdCommit())
                    {

                        AddLog(grisper.devName +",机械手2发送设备指令出现错误", EnumLogType.错误);
                    }
                    grisper.SaveLastLoopData();
                }
                catch (System.Exception ex)
                {
                    //自动运行异常，3003

                    int errorCode = 2003;
                    string reStr = "";
                    if (!GetErrorContent(errorCode, ref reStr))
                    {
                        AddLog("机械手2自动运行出现异常," + ex.Message + "," + ex.StackTrace, EnumLogType.错误);
                    }
                    else
                    {

                        reStr += (ex.Message + "," + ex.StackTrace);
                        reStr += "机械手2任务执行异常," + reStr;
                        OnErrorHappen(errorCode, reStr, true);
                    }
                }
            }
        }
        private void XYZ5003Proc()
        {
            string[] portList = new string[] { "2007", "2008"};
            while (!exitRunning)
            {
                Thread.Sleep(scanInterval);

                if (pauseFlag)
                {
                    continue;
                }
                try
                {
                    for (int i = 0; i < portList.Count(); i++)
                    {
                        string portID = portList[i];
                        ECAMSDevBase portDev = DevDicList[portID];
                        if (!portDev.ReadDB2())
                        {
                            //读设备DB2状态出现错误，在日志显示
                            AddLog(portDev.devName + " 读取状态数据失败", EnumLogType.错误);
                            continue;

                        }

                        portDev.DevRefreshStatus();
                        if (!portDev.DevCmdCommit())
                        {

                            AddLog(portDev.devName + " 发送设备指令出现错误", EnumLogType.错误);
                            continue;
                        }
                    }

                    ECAMSXYZGriper grisper = DevDicList["5003"] as ECAMSXYZGriper;
                    //if (!grisper.ReadDB1())
                    //{
                    //    AddLog(grisper.devName + " 读取命令数据失败", EnumLogType.错误);
                    //    continue;
                    //}
                    if (!grisper.ReadDB2())
                    {
                        //读设备DB2状态出现错误，在日志显示
                        AddLog(grisper.devName + " 读取状态数据失败", EnumLogType.错误);
                        continue;
                    }

                    grisper.DevRefreshStatus();
                    if (!grisper.ExeBusiness())
                    {

                        AddLog(grisper.devName + " 执行控制任务出现错误", EnumLogType.错误);
                    }
                    if (!grisper.DevCmdCommit())
                    {

                        AddLog(grisper.devName + ",机械手3发送设备指令出现错误", EnumLogType.错误);
                    }
                    grisper.SaveLastLoopData();
                }
                catch (System.Exception ex)
                {
                    //自动运行异常，3003

                    int errorCode = 2003;
                    string reStr = "";
                    if (!GetErrorContent(errorCode, ref reStr))
                    {
                        AddLog("机械手3自动运行出现异常," + ex.Message + "," + ex.StackTrace, EnumLogType.错误);
                    }
                    else
                    {

                        reStr += (ex.Message + "," + ex.StackTrace);
                        reStr+= "机械手3任务执行异常,"+reStr;
                        OnErrorHappen(errorCode, reStr, true);
                    }
                }
            }
        }
      
        /// <summary>
        /// 增加一条日志记录
        /// </summary>
        public void AddLog(LogModel log,EnumLogType logType)
        {
            if (eventLogDisp != null)
            {
                LogEventArgs arg = new LogEventArgs();
                // arg.happenTime = System.DateTime.Now;
                // arg.logMes = log.logContent;
                arg.LogTime = System.DateTime.Now;
                arg.LogCate = EnumLogCategory.控制层日志;
                arg.LogContent = log.logContent;
                arg.LogType =  logType;
                eventLogDisp.Invoke(this, arg);
            }

        }
        public void AddLog(string content,EnumLogType logType)
        {
            LogModel log = new LogModel();
            log.logCategory = EnumLogCategory.控制层日志.ToString();
            log.logContent = content;
            log.logType = logType.ToString();
            log.logTime = System.DateTime.Now;
            AddLog(log,logType);
        }
        private void OnErrorHappen(int errorCode,string errorDescribe,bool stopRunning)
        {
            if (eventError != null)
            {
                ECAMSErrorEventArgs args = new ECAMSErrorEventArgs();
                args.LogCate = EnumLogCategory.控制层日志;
                args.ErrorCode = errorCode;
                args.LogContent = errorDescribe;
                args.RequireRunningStop = stopRunning;
                args.LogTime = System.DateTime.Now;
                eventError.Invoke(this, args);
            }
        }

        /// <summary>
        /// PLC 断开连接的事件响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlcLostConnectHandler(object sender, PlcReLinkArgs e)
        {
            IPlcRW plcRW = plcDic[e.PlcID];
            if (plcRW == null)
            {
                return;
            }
            string connInfo = string.Format("{0} 号机械手PLC通信断开，正在重连...", e.PlcID);
            AddLog(connInfo,EnumLogType.提示);

            string connStr = e.StrConn;// PlcIP + ":" + PlcPort.ToString();
            string reStr = "";
            
            if (plcRW.ConnectPLC(connStr, ref reStr))
            {
                connInfo = string.Format("{0} 号机械手PLC重连成功", e.PlcID);
                AddLog(connInfo,EnumLogType.提示);
            }
            else
            {
                connInfo = string.Format("{0} 号机械手PLC重连失败", e.PlcID);
                AddLog(connInfo, EnumLogType.错误);
            }

        }

        /// <summary>
        /// 堆垛机PLC通信断开触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StackerPlcLostConnectHandler(object sender, EventArgs e)
        {
            if (stackerPlcRW == null)
            {
                return;
            }
            AddLog("堆垛机PLC 通信连接断开，正在重连...", EnumLogType.提示);
           
            string reStr = "";
            //堆垛机PLC连接
            string stackerPlcCom = ConfigurationManager.AppSettings["stackerPlcCom"];
            
            if (!stackerPlcRW.ConnectPLC(stackerPlcCom, ref reStr))
            {
                int errorCode = 2102;
                if (!GetErrorContent(errorCode, ref reStr))
                {
                    AddLog("堆垛机PLC 通信连接失败", EnumLogType.错误);
                }
                else
                {
                    reStr = "堆垛机PLC 通信连接失败";
                    OnErrorHappen(errorCode, reStr, false);
                }
            }
            else
            {
                AddLog("堆垛机PLC重连成功!", EnumLogType.提示);
            }
        }
        /// <summary>
        /// 系统启动后，从文件恢复入库口料框队列
        /// </summary>
        /// <returns></returns>
        private bool RestorePalletInputQueue()
        {
            try
            {
                string exePath = AppDomain.CurrentDomain.BaseDirectory;
               // string palletQueueFile = exePath + @"Data\PortInQueue.pltbf";
                string palletQueueFile = exePath + @"PortInQueue.pltbf";
                if (!System.IO.File.Exists(palletQueueFile))
                {
                    FileStream stream = new FileStream(palletQueueFile, FileMode.Create);
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(stream, PalletInputDeque);
                    stream.Close();
                }
                else
                {
                    FileStream stream = new FileStream(palletQueueFile, FileMode.Open);
                    BinaryFormatter formatter = new BinaryFormatter();
                    PalletInputDeque = formatter.Deserialize(stream) as Dictionary<string, Queue<string>>;
                }
                return true;
            }
            catch (System.Exception ex)
            {
                AddLog(ex.ToString(), EnumLogType.错误);
                return false;
            }
           
           
            
        }

        /// <summary>
        /// 保存入库口料框队列到文件
        /// </summary>
        /// <returns></returns>
        public bool SavePalletInputQueue()
        {
            try
            {
                lock (portinDequeLock)
                {
                    string exePath = AppDomain.CurrentDomain.BaseDirectory;
                    string palletQueueFile = exePath + @"PortInQueue.pltbf";

                    FileStream stream = new FileStream(palletQueueFile, FileMode.OpenOrCreate);
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(stream, PalletInputDeque);
                    stream.Close();
                    return true;
                }
             
            }
            catch (System.Exception ex)
            {

                AddLog("保存入口处料框信息到文件失败," + ex.ToString(), EnumLogType.错误);
                return false;
            }
           
            
           
        }
        #endregion
    }
}
