using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ACTETHERLib;
using ECAMSModel;
namespace PLCControl
{
    /// <summary>
    /// PLC读写功能类
    /// </summary>
    public class PLCRW:IPlcRW
    {
        #region 数据区
        
        private ActQNUDECPUTCP _actObj;
      
        private bool isConnected = false;

       
        #endregion 

        /// <summary>
        /// 构造函数
        /// </summary>
        public PLCRW()
        {
            _actObj = new ActQNUDECPUTCP();
            _actObj.ActCpuType = 0x90; //Q03UDECPU
            _actObj.ActHostAddress = "192.168.1.13";
           
            
            //_actObj.ActNetworkNumber = 0;
           // _actObj.ActStationNumber = 0;
           
          
           
        }
        #region  接口实现
        public int PlcID { get; set; }
        /// <summary>
        /// 连接断开事件
        /// </summary>
        public event EventHandler<PlcReLinkArgs> eventLinkLost;
        public event EventHandler<LogEventArgs> eventLogDisp;
        public int StationNumber { get; set; }
        public bool IsConnect 
        {
            get
            {
                return isConnected;
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
           
            string[] splitStr = new string[] { ",", ";", ":", "-", "|" };
            string[] strArray = plcAddr.Split(splitStr, StringSplitOptions.RemoveEmptyEntries);
            if (strArray.Count() < 2)
            {
                return false;
            }
            _actObj.ActHostAddress = strArray[0];
           // _actObj.ActDestinationPortNumber = int.Parse(strArray[1]);
            int re = _actObj.Open();
            if (re == 0)
            {
                isConnected = true;
                reStr = "连接PLC成功！";

                return true;
            }
            else
            {
                reStr = "连接PLC失败!";
                isConnected = false;
                return false;
            }
            
        }
        //关闭连接
        public bool CloseConnect()
        {
            if (_actObj.Close() == 0)
            {
                isConnected = false;
                return true;
            }
            else
            {
                
                return false;
            }
        }
        public bool ReadDB(string addr, ref int val)
        {
            //object io;
            //short[] reVals = new short[15]; ;
            //_actObj.ReadDeviceBlock2(addr, 15, out reVals[0]);//批量读取
           
            short int16Val = 0;
            int re = _actObj.GetDevice2(addr, out int16Val);
         
            if (re == 0x01010020)
            {
                //Link error，Link communications could not be made.
                isConnected = false;
                if (eventLinkLost != null)
                {
                    eventLinkLost.Invoke(this, null);
                }
            }
            if (re != 0)
                return false; //读取失败
            
            //val = Convert.ToInt32(io);
            val = int16Val;

            return true;
        }
        public bool WriteDB(string addr, int val)
        {
            int re = _actObj.SetDevice(addr, val);
            if (re == 0x01010020)
            {
                isConnected = false;
                if (eventLinkLost != null)
                {
                    eventLinkLost.Invoke(this, null);
                }
            }
            if (re != 0) 
                return false; //写入失败
            return true;
        }
        public bool ReadMultiDB(string addr, int blockNum, ref short[] vals)
        {
            if (blockNum <= 0)
            {
                return false;
            }
            vals = new short[blockNum];
 
            int re =_actObj.ReadDeviceBlock2(addr, blockNum, out vals[0]);//批量读取
            if (re != 0)
                return false; //读取失败
            return true;
        }
        public bool WriteMultiDB(string addr, int blockNum, short[] vals)
        {
            if (blockNum <= 0)
            {
                return false;
            }
            if (vals == null || vals.Count() < blockNum)
            {
                return false;
            }
            int re = _actObj.WriteDeviceBlock2(addr, blockNum, ref vals[0]);
            if (re != 0)
                return false; //读取失败
            return true;
        }
        #endregion
    }
}
