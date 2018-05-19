using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECAMSModel;
using ECAMSDataAccess;
namespace PLCControl
{
    /// <summary>
    /// plc读写模拟类
    /// </summary>
    public class PlcRWSim:IPlcRW
    {
        private PlcDBSimBll dbSimBll = new PlcDBSimBll();
        /// <summary>
        /// 连接断开事件
        /// </summary>
        public event EventHandler<PlcReLinkArgs> eventLinkLost;
        public event EventHandler<LogEventArgs> eventLogDisp;
        public int PlcID { get; set; }
        public int StationNumber { get; set; }
        public bool IsConnect
        {
            get
            {
                return true;
            }

        }
        public void Init()
        {
            
        }
        public void Exit()
        {
           
        }
        public bool ConnectPLC(string plcAddr, ref string reStr)
        {
            reStr = "连接成功！";
            return true;
        }
        public bool CloseConnect()
        {
            return true;
        }
        public bool ReadDB(string addr, ref int val)
        {
            //PlcDBSimModel model = dbSimBll.GetModel(addr);
            //if (model == null)
            //{
            //    return false;
            //}
            //val = model.Val;
            val = 1;
            return true;
        }
        public bool ReadMultiDB(string addr, int blockNum, ref short[] vals)
        {
        
            vals = new short[blockNum];
            return true;
        }
        public bool WriteDB(string addr, int val)
        {
            //PlcDBSimModel model = dbSimBll.GetModel(addr);
            //if (model == null)
            //{
            //    return false;
            //}
            //model.Val = val;
            //return dbSimBll.Update(model);
            
            return true;

        }
        public bool WriteMultiDB(string addr, int blockNum, short[] vals)
        {
            
            return true;
        }
    }
}
