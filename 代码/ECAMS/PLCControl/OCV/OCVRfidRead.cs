using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECAMSModel;
using ECAMSDataAccess;
namespace PLCControl
{
    public class OCVRfidRead
    {
        #region 私有数据
        public string ocvRfidName = "";
        private bool rfidRequireLast = false;
        private IrfidRW rfidReader = null;
        public IrfidRW RfidReader
        {
            get
            {
                return rfidReader;
            }
            set
            {
                rfidReader = value;
            }
        }
        OCVRfidReadingBll ocvRfidBll = new OCVRfidReadingBll();
        RfidRdRecordBll rfidRecordBll = new RfidRdRecordBll();
        private OCVPalletBll ocvPalletBll = new OCVPalletBll();
        public ECAMSTransPort PortDev { get; set; }
        #endregion
        public OCVRfidRead(IrfidRW reader)
        {
            this.rfidReader = reader;
        }
        #region 公共接口
        public bool OCVRfidInit()
        {
            if (rfidReader == null)
            {
                return false;
            }
            //读数据库
            OCVRfidReadingModel model = ocvRfidBll.GetModel(rfidReader.ReaderID);
            if (model == null)
            {
                return false;
            }
            this.rfidRequireLast = model.readRequire;
           
            return true;
        }
        public bool ExeOCVRfidBusiness(ref string reStr)
        {
            reStr = "";
            if (rfidReader == null)
            {
                return false;
            }
            
            OCVRfidReadingModel model = ocvRfidBll.GetModel(rfidReader.ReaderID);
            if(model == null )
            {
                return false;
            }
            if (model.readComplete)
            {
                PortDev.DicCommuDataDB1[1].Val = 2;
                if (!PortDev.DevCmdCommit())
                {
                    PortDev.AddLog(PortDev.devName + ",发送命令数据失败", EnumLogType.错误);
                }
                if (!model.readRequire)
                {
                    //如果读卡请求复位，则读卡完成信号复位
                    model.readComplete = false;
                    if (!ocvRfidBll.Update(model))
                    {
                        reStr = "OCV读卡对象不存在，数据库未配置！";
                        return false;
                    }

                }
            }
            if (PortDev.DicCommuDataDB2[2].Val.ToString() == "2")
            {
                PortDev.DicCommuDataDB1[1].Val = 1;
                if (!PortDev.DevCmdCommit())
                {
                    PortDev.AddLog(PortDev.devName + ",发送命令数据失败", EnumLogType.错误);
                }
            }
            if(model.readRequire && (!model.readComplete))
            {
                //读卡
                //byte[] bytesID = rfidReader.ReadSBlock(0);
                //if (bytesID == null || bytesID.Count() < 4)
                //{
                //    return true;
                //}
                //int id = bytesID[0] +(bytesID[1] << 8) + (bytesID[2] << 16) + (bytesID[3] << 24);
                byte[] recvByteArray = null;
                string palletID = rfidReader.ReadPalletID(ref recvByteArray);
                if (string.IsNullOrWhiteSpace(palletID))
                {
                    reStr = "读卡错误，托盘号为空";
                    return false;
                }
                if (!ocvPalletBll.Exists(palletID))
                {
                    reStr = "托盘号不存在：" + palletID+ " ";
                    return false;
                }
                RfidRdRecordModel rfidRecord = new RfidRdRecordModel();
                rfidRecord.rfidReaderID = rfidReader.ReaderID;
                //if(rfidReader.ReaderID == 4)
                //{
                //    rfidRecord.readerName = "一次检测区";
                //}
                //else if (rfidReader.ReaderID == 7)
                //{
                //    rfidRecord.readerName = "二次检测区";
                //}
                rfidRecord.readerName = ocvRfidName;
                rfidRecord.readingContent = palletID;
                rfidRecord.readingTime = System.DateTime.Now;
                rfidRecordBll.Add(rfidRecord);

                model.rfidValue = palletID;
                model.readComplete = true;
                if(!ocvRfidBll.Update(model))
                {
                    reStr = "OCV读卡接口数据库更新失败！";
                }
            }
            return true;
        }
        #endregion
    }
}
