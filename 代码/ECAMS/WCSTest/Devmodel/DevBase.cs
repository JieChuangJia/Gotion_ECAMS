using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECAMSModel;
using ECAMSDataAccess;
namespace WCSTest
{
    /// <summary>
    /// 设备的基类
    /// </summary>
    public abstract class DevBase
    {
  

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="devModel"></param>
        /// <param name="plcRW"></param>
        /// <param name="ctlInterfaceBll"></param>
        /// <param name="ctlTaskBll"></param>
        public DevBase(ECAMSDataAccess.DeviceModel devModel, IPlcRW plcRW, DeviceBll devBll)
        {
            this.devModel = devModel;
            this.plcRW = plcRW;
            this.devBll = devBll;
           
           
            plcDataDb1 = new byte[devModel.BytesLenDB1];
            plcDataDb2 = new byte[devModel.BytesLenDB2];
            plcDataDb1Last = new byte[devModel.BytesLenDB1];
            plcDataDb2Last = new byte[devModel.BytesLenDB2];
            DebugSimModel = true; //模拟调试用
        }
        #region 调试用
        /// <summary>
        /// 模拟调试模式
        /// </summary>
        public bool DebugSimModel { get; set; }
        public event EventHandler<LogEventArgs> eventLogDisp;
        
        #endregion
        #region 数据区
        
        /// <summary>
        /// 设备的plc读写接口
        /// </summary>
        protected IPlcRW plcRW = null;

        /// <summary>
        /// 设备模型，模型数据从数据库中读取
        /// </summary>
        protected ECAMSDataAccess.DeviceModel devModel=null;

        /// <summary>
        /// 设备的plc DB1数据区地址
        /// </summary>
        protected string[] plcAddrDb1=null;

        /// <summary>
        /// 设备的plc DB2数据区地址
        /// </summary>
        protected string[] plcAddrDb2=null;

        /// <summary>
        /// plc数据区DB1字节数组
        /// </summary>
        protected byte[] plcDataDb1 = null;

        /// <summary>
        /// 上一拍DB1的数据
        /// </summary>
        protected byte[] plcDataDb1Last = null;

        /// <summary>
        /// plc数据区DB2字节数组
        /// </summary>
        protected byte[] plcDataDb2 = null;

        /// <summary>
        /// 上一拍的DB2的数据
        /// </summary>
        protected byte[] plcDataDb2Last = null;
        /// <summary>
        /// 设备数据表的业务层接口
        /// </summary>
        protected DeviceBll devBll = null;

      
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
        /// 分配设备的DB1，DB2地址区
        /// </summary>
        /// <returns></returns>
        protected bool AllocDevDBAddr()
        {
            try
            {
                if (devModel != null)
                {
                    int DB1BlockNum = devModel.BytesLenDB1 / 2;
                    int DB2BlockNum = devModel.BytesLenDB2 / 2;
                    if (DB1BlockNum > 0)
                    {
                        plcAddrDb1 = new string[DB1BlockNum];
                        for (int i = 0; i < DB1BlockNum; i++)
                        {
                            plcAddrDb1[i] = "DB" + (int.Parse(devModel.DB1AddrStart.Substring
                                (2, 4)) + i).ToString().PadLeft(4, '0');
                        }
                    }
                    if (DB2BlockNum > 0)
                    {
                        plcAddrDb2 = new string[DB2BlockNum];
                        for (int i = 0; i < DB2BlockNum; i++)
                        {
                            plcAddrDb2[i] = "DB" + (int.Parse(devModel.DB2AddrStart.Substring
                              (2, 4)) + i).ToString().PadLeft(4, '0');
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
                return false;
            }
        }

        /// <summary>
        /// 增加一条日志记录
        /// </summary>
        protected void AddLog(LogModel log)
        {
            if (eventLogDisp != null)
            {
                LogEventArgs arg = new LogEventArgs();
                // arg.happenTime = System.DateTime.Now;
                // arg.logMes = log.logContent;
                arg.LogTime = System.DateTime.Now;
                arg.LogCate = EnumLogCategory.控制层日志;
                arg.LogContent = log.logContent;
                arg.LogType = EnumLogType.调试信息;
                eventLogDisp.Invoke(this, arg);
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
        
        public string[] AddrDB1
        {
            get
            {
                return plcAddrDb1;
            }
            set
            {
                plcAddrDb1 = value;
            }
        }
        public string[] AddrDB2
        {
            get
            {
                return plcAddrDb2;
            }
            set
            {
                plcAddrDb2 = value;
            }
        }
        public byte[] DB1
        {
            get
            {
                return plcDataDb1;
            }
            private set { }
        }
        public byte[] DB1Last
        {
            get
            {
                return plcDataDb1Last;
            }
            private set { }
        }
        public byte[] DB2
        {
            get
            {
                return plcDataDb2;
            }
            private set { }
        }
        public byte[] DB2Last
        {
            get 
            {
                return plcDataDb2Last;
            }
            private set { }
        }
        #endregion
        #region 公共方法

        public bool Init()
        {
            //分配地址
            if (!AllocDevDBAddr())
            {
                return false;
            }
            //系统启动后，先把DB1,DB2数据读上来
            for (int i = 0; i < plcAddrDb1.Count(); i++)
            {
                string dbAddr = plcAddrDb1[i];
                int val = 0;
                if (!plcRW.ReadDB(dbAddr, ref val))
                {
                    return false;
                }
                plcDataDb1[2 * i] = (byte)(val & 0xff);
                plcDataDb1[2 * i + 1] = (byte)(val >> 8);
            }
            Array.Copy(plcDataDb1, plcDataDb1Last, plcDataDb1Last.Count());

            for (int i = 0; i < plcAddrDb2.Count(); i++)
            {
                string dbAddr = plcAddrDb2[i];
                int val = 0;
                if (!plcRW.ReadDB(dbAddr, ref val))
                {
                    return false;
                }
                plcDataDb2[2 * i] = (byte)(val & 0xff);
                plcDataDb2[2 * i + 1] = (byte)(val >> 8);
            }
            Array.Copy(plcDataDb2, plcDataDb2Last, plcDataDb2Last.Count());
            return true;
        }
        /// <summary>
        /// 读DB2数据
        /// </summary>
        /// <returns></returns>
        public bool ReadDB1()
        {
            for (int i = 0; i < plcAddrDb1.Count(); i++)
            {
                string dbAddr = plcAddrDb1[i];
                int val = 0;
                if (!plcRW.ReadDB(dbAddr, ref val))
                {
                    return false;
                }
                plcDataDb1[2 * i] = (byte)(val & 0xff);
                plcDataDb1[2 * i + 1] = (byte)(val >> 8);
            }
            return true;
        }

       

        /// <summary>
        /// 如有更新，设备指令发送，写入DB1
        /// </summary>
        /// <returns></returns>
        public bool DevStatusCommit()
        {
            if(plcRW == null)
            {
                return false;
            }
            for (int i = 0; i < plcAddrDb2.Count(); i++)
            {
                int byteIndex = 2 * i;
                if ((plcDataDb2[byteIndex] != plcDataDb2Last[byteIndex]) || (plcDataDb2[byteIndex+1]!= plcDataDb2Last[byteIndex+1]))
                {
                    int val = plcDataDb2[byteIndex]+(plcDataDb2[byteIndex+1]<<8);
                    if (plcRW.WriteDB(plcAddrDb2[i], val))
                    {
                        plcDataDb2Last[byteIndex] = plcDataDb2[byteIndex];
                        plcDataDb2Last[byteIndex+1] = plcDataDb2[byteIndex+1];
                    }
                }
            }
            return true;
        }

        public bool DevDB1Commit()
        {
            if (plcRW == null)
            {
                return false;
            }
            for (int i = 0; i < plcAddrDb1.Count(); i++)
            {
                int byteIndex = 2 * i;
                if ((plcDataDb1[byteIndex] != plcDataDb1Last[byteIndex]) || (plcDataDb1[byteIndex + 1] != plcDataDb1Last[byteIndex + 1]))
                {
                    int val = plcDataDb1[byteIndex] + (plcDataDb1[byteIndex + 1] << 8);
                    if (plcRW.WriteDB(plcAddrDb1[i], val))
                    {
                        plcDataDb1Last[byteIndex] = plcDataDb1[byteIndex];
                        plcDataDb1Last[byteIndex + 1] = plcDataDb1[byteIndex + 1];
                    }
                }
            }
            return true;
        }
        #endregion
       
    }
}
