using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECAMSModel;
using ECAMSDataAccess;

namespace PLCControl
{
    public enum EnumTransPortStatus
    {
        EMPTY,
        FULL,
        ERROR
    }
    /// <summary>
    /// 站台
    /// </summary>
    public class ECAMSTransPort : ECAMSDevBase
    {
        #region 私有数据
       

   
        #endregion
        public ECAMSTransPort(ECAMSDataAccess.DeviceModel devModel, IPlcRW plcRW, DeviceBll devBll,ControlInterfaceBll ctlInterfaceBll, ControlTaskBll ctlTaskBll,LogBll logBll)
            : base(devModel, plcRW, devBll,ctlInterfaceBll, ctlTaskBll,logBll)
        {
            switch (devModel.DeviceID)
            {
                case "2002":
                    {
                        devName = "A1库入库口";
                        break;
                    }
                case "2003":
                    {
                        devName = "A1库分容出口";
                        break;
                    }
                case "2004":
                    {
                        devName = "A1库分容入口";
                        break;
                    }
                case "2005":
                    {
                        devName = "A1库出口";
                        break;
                    }
                case "2006":
                    {
                        devName = "B1库入口";
                        break;
                    }
                case "2007":
                    {
                        devName = "B1库二次检测出口";
                        break;
                    }
                case "2008":
                    {
                        devName = "B1库的空筐入口";
                        break;
                    }
                case "2009":
                    {
                        devName = "B1库的出口";
                        break;
                    }
                default:
                    break;
            }
        }
        #region 重写虚函数
        protected override void AllocDevComAddrsDB1()
        {
            int db1ID = 1;
            int plcAddrStart = int.Parse(this.devModel.DB1AddrStart.Substring(1));
            string dbName = "D";
            DevCommDatatype commData = null;
            if (devModel.DeviceID == "2002" || devModel.DeviceID == "2004" || devModel.DeviceID == "2006")
            {
             

                //1.配置“开始写入”功能项
                commData = new DevCommDatatype();
                commData.CommuID = db1ID++;
                commData.CommuMethod = EnumCommMethod.PLC_MIT_COMMU;
                commData.DataByteLen = 2;
                commData.DataDescription = "2:前后两筐同一个批次,1:不是同一批次";
                commData.DataTypeDef = EnumCommuDataType.DEVCOM_short;
                commData.Val = 0;
                commData.DataAddr = dbName + plcAddrStart.ToString();//取值为：S7:[Connection_1]DB1,INT，数据类型可变，马天牧
                plcAddrStart++;
                dicCommuDataDB1[commData.CommuID] = commData;

                commData = new DevCommDatatype();
                commData.CommuID = db1ID++;
                commData.CommuMethod = EnumCommMethod.PLC_MIT_COMMU;
                commData.DataByteLen = 2;
                commData.DataDescription = "扫码状态，1：未完成，2：扫码完成，3: 扫码失败，4: 不可识别的托盘,"+
                    "5：批次信息不存在，6: 托盘电芯为空，7: 托盘ID重复读取,入口缓存已存在同样托盘ID，8：国轩数据库已经存在同样的托盘ID";
                commData.DataTypeDef = EnumCommuDataType.DEVCOM_short;
                commData.Val = 0;
                commData.DataAddr = dbName + plcAddrStart.ToString();//取值为：S7:[Connection_1]DB1,INT，数据类型可变，马天牧
                plcAddrStart++;
                dicCommuDataDB1[commData.CommuID] = commData;

                commData = new DevCommDatatype();
                commData.CommuID = db1ID++;
                commData.CommuMethod = EnumCommMethod.PLC_MIT_COMMU;
                commData.DataByteLen = 2;
                commData.DataDescription = "入库请求,1：复位2：入库请求,3：入口处缓存为空，请人工处理";
                commData.DataTypeDef = EnumCommuDataType.DEVCOM_short;
                commData.Val = 0;
                commData.DataAddr = dbName + plcAddrStart.ToString();//取值为：S7:[Connection_1]DB1,INT，数据类型可变，马天牧
                plcAddrStart++;
                dicCommuDataDB1[commData.CommuID] = commData;
            }
            if (devModel.DeviceID == "2005" || devModel.DeviceID == "2007")
            {
                //1.配置“开始写入”功能项
                commData = new DevCommDatatype();
                commData.CommuID = db1ID++;
                commData.CommuMethod = EnumCommMethod.PLC_MIT_COMMU;
                commData.DataByteLen = 2;
                commData.DataDescription = "OCV检测区读卡，1：未完成，2：扫码完成，3: 扫码失败，4: 不可识别的托盘（数据库中不存在）";
                commData.DataTypeDef = EnumCommuDataType.DEVCOM_short;
                commData.Val = 0;
                commData.DataAddr = dbName + plcAddrStart.ToString();//取值为：S7:[Connection_1]DB1,INT，数据类型可变，马天牧
                plcAddrStart++;
                dicCommuDataDB1[commData.CommuID] = commData;
            }
            else if (devModel.DeviceID == "2008")
            {
                commData = new DevCommDatatype();
                commData.CommuID = db1ID++;
                commData.CommuMethod = EnumCommMethod.PLC_MIT_COMMU;
                commData.DataByteLen = 2;
                commData.DataDescription = "入库请求,1：复位2：入库请求";
                commData.DataTypeDef = EnumCommuDataType.DEVCOM_short;
                commData.Val = 0;
                commData.DataAddr = dbName + plcAddrStart.ToString();//取值为：S7:[Connection_1]DB1,INT，数据类型可变，马天牧
                plcAddrStart++;
                dicCommuDataDB1[commData.CommuID] = commData;
            }
            return;
        }
        protected override void AllocDevComAddrsDB2()
        {
            if (devModel.DeviceID == "2003" || devModel.DeviceID == "2005" || devModel.DeviceID == "2007" || devModel.DeviceID == "2009")
            {
                int db2ID = 1;
                int plcAddrStart = int.Parse(this.devModel.DB2AddrStart.Substring(1));
                string dbName = "D";
                DevCommDatatype commData = null;
                //1 配置故障码
                commData = new DevCommDatatype();
                commData.CommuID = db2ID++;
                commData.CommuMethod = EnumCommMethod.PLC_MIT_COMMU;
                commData.DataByteLen = 2;
                commData.DataDescription = "1：禁止出库2：允许出库";
                commData.DataTypeDef = EnumCommuDataType.DEVCOM_short;
                commData.Val = 0;
                commData.DataAddr = dbName + plcAddrStart.ToString();
                plcAddrStart++;
                dicCommuDataDB2[commData.CommuID] = commData;
                if (devModel.DeviceID == "2005" || devModel.DeviceID == "2007")
                {
                    commData = new DevCommDatatype();
                    commData.CommuID = db2ID++;
                    commData.CommuMethod = EnumCommMethod.PLC_MIT_COMMU;
                    commData.DataByteLen = 2;
                    commData.DataDescription = "2：OCV检测读卡完成，1：未完成";
                    commData.DataTypeDef = EnumCommuDataType.DEVCOM_short;
                    commData.Val = 0;
                    commData.DataAddr = dbName + plcAddrStart.ToString();
                    plcAddrStart++;
                    dicCommuDataDB2[commData.CommuID] = commData;
                }
            }
            else
            {
                int db2ID = 1;
                int plcAddrStart = int.Parse(this.devModel.DB2AddrStart.Substring(1));
                string dbName = "D";
                DevCommDatatype commData = null;
                //1 配置故障码
                commData = new DevCommDatatype();
                commData.CommuID = db2ID++;
                commData.CommuMethod = EnumCommMethod.PLC_MIT_COMMU;
                commData.DataByteLen = 2;
                commData.DataDescription = "2：入库请求，1：无入库请求";
                commData.DataTypeDef = EnumCommuDataType.DEVCOM_short;
                commData.Val = 0;
                commData.DataAddr = dbName + plcAddrStart.ToString();
                plcAddrStart++;
                dicCommuDataDB2[commData.CommuID] = commData;

                commData = new DevCommDatatype();
                commData.CommuID = db2ID++;
                commData.CommuMethod = EnumCommMethod.PLC_MIT_COMMU;
                commData.DataByteLen = 2;
                commData.DataDescription = "入口料框数量";
                commData.DataTypeDef = EnumCommuDataType.DEVCOM_short;
                commData.Val = 0;
                commData.DataAddr = dbName + plcAddrStart.ToString();
                plcAddrStart++;
                dicCommuDataDB2[commData.CommuID] = commData;

                commData = new DevCommDatatype();
                commData.CommuID = db2ID++;
                commData.CommuMethod = EnumCommMethod.PLC_MIT_COMMU;
                commData.DataByteLen = 2;
                commData.DataDescription = "2：料框扫码请求，1：无请求";
                commData.DataTypeDef = EnumCommuDataType.DEVCOM_short;
                commData.Val = 0;
                commData.DataAddr = dbName + plcAddrStart.ToString();
                plcAddrStart++;
                dicCommuDataDB2[commData.CommuID] = commData;

                commData = new DevCommDatatype();
                commData.CommuID = db2ID++;
                commData.CommuMethod = EnumCommMethod.PLC_MIT_COMMU;
                commData.DataByteLen = 2;
                commData.DataDescription = "2：收到成功扫码信息，1：没有收到（或已复位）";
                commData.DataTypeDef = EnumCommuDataType.DEVCOM_short;
                commData.Val = 0;
                commData.DataAddr = dbName + plcAddrStart.ToString();
                plcAddrStart++;
                dicCommuDataDB2[commData.CommuID] = commData;
            }
            return;
        }
        public override void DevRefreshStatus()
        {
           
           
        }
        public override bool ExeBusiness()
        {
            //bool re = true;
        
            #region 2 执行业务逻辑，入库
            //if (devModel.DeviceID == "2002")
            //{
            //    re = ExeBusinessA1_2002();
            //}
            //else if (devModel.DeviceID == "2004")
            //{
            //    re = ExeBusinessA1_2004();
            //}
            //else if (devModel.DeviceID == "2006")
            //{
            //    re = ExeBusinessB1_2006();
            //}
            //else if (devModel.DeviceID == "2008")
            //{
            //    re = ExeBusinessB1_2008();
            //}
            //else if (devModel.DeviceID == "2009")
            //{
            //    re = ExeBusinessB1_2009();
            //}
            #endregion
            return DevCmdCommit();
            //处理完之后再把上拍的DB2数据记录下来
           
           
         
        }
        protected override void  CommuDataToDevStatusDB2()
        {
       
        }
        protected override void CmdToCommuDataDB1()
        {

        }
        protected override void DevCmdReset()
        {
            for (int i = 1; i < dicCommuDataDB1.Count() + 1; i++)
            {
                dicCommuDataDB1[i].Val = 1;
            }
            DevCmdCommit();
            //if (this.devModel.DeviceID == "2002" || this.devModel.DeviceID == "2004" || this.devModel.DeviceID == "2006")
            //{
            //    dicCommuDataDB1[1].Val = 0;
            //    dicCommuDataDB1[2].Val = 1;
            //}
        }
        /// 站台2002处入库业务逻辑
        /// </summary>
        /// <returns></returns>
        private bool ExeBusinessA1_2002()
        {
           
            return true;
        }

        /// <summary>
        /// 站台2004处分容入库业务逻辑
        /// </summary>
        /// <returns></returns>
        private bool ExeBusinessA1_2004()
        {
           
            return true;
        }

        /// <summary>
        /// 站台2006处，B1库入库业务逻辑
        /// </summary>
        /// <returns></returns>
        private bool ExeBusinessB1_2006()
        {
           
            return true;
        }

        /// <summary>
        /// 申请空料框入库
        /// </summary>
        /// <returns></returns>
        private bool ExeBusinessB1_2008()
        {
            
            return true;
        }

        /// <summary>
        /// 空料框出库申请
        /// </summary>
        /// <returns></returns>
        private bool ExeBusinessB1_2009()
        {
            
            return true;
        }
      
        #endregion
    }
}
