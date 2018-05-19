using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECAMSModel;
using ECAMSDataAccess;

namespace WCSTest
{
    public class XYZGriper : DevBase
    {
        #region 私有数据
        ///// <summary>
        ///// 是否允许申请新的任务
        ///// </summary>
        //private bool enableRequireNewTask = true;
       
        private OCVPalletBll ocvPalletBll = new OCVPalletBll();
        private OCVBatteryBll ocvBatteryBll = new OCVBatteryBll();
     
        #endregion
        public XYZGriper(ECAMSDataAccess.DeviceModel devModel, IPlcRW plcRW,DeviceBll devBll)
            : base(devModel, plcRW, devBll)
        {
            
        }
        #region 重写虚函数

      
        #endregion
        #region 私有功能函数
       
        private string GenerateBoxRfidCode()
        {
            Int64 currentMaxRfidCode = ocvPalletBll.GetMaxPalletID();
            string newRfidCode = (currentMaxRfidCode + 1).ToString().PadLeft(10, '0');
            return newRfidCode;
        }

        /// <summary>
        /// 解析任务参数字符串成字节数组，代表每个电芯是否合格，一个电芯用一位表示，一个字节代表8个电芯
        /// </summary>
        /// <param name="taskParameter"></param>
        /// <returns></returns>
        private bool ParseGrisptaskParameter(string taskParameter,out byte[] batteryStatus, out string boxRfidID)
        {
            batteryStatus = new byte[6];
            boxRfidID = "0";
            string[] splitStr = new string[] { ",", ";", ":", "-", "|" };
            string[] strArray = taskParameter.Split(splitStr, StringSplitOptions.RemoveEmptyEntries);
            if (strArray.Count() < 7)
            {
                return false;
            }
            boxRfidID = strArray[0];
            for (int i = 1; i < 7; i++)
            {
                if (!byte.TryParse(strArray[i], out batteryStatus[i]))
                {
                    return false;
                }
            }
            return true;
        }
        #endregion
    }
}
