using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCSTest
{
    //public class DevDBDataArgs : EventArgs
    //{
    //    public string strDevData{ get; set; }
    //}
    /// <summary>
    /// 入库业务模拟模块相关的view接口
    /// </summary>
    public interface ITestHouseInView
    {
        #region 方法
        /// <summary>
        /// 刷新A1入库相关设备的DB1
        /// </summary>
        /// <param name="strData"></param>
        void UpdateA1DB1(string strData);
        void UpdateA1DB2(string strData);

        /// <summary>
        /// 刷新B1入库相关设备的DB1
        /// </summary>
        /// <param name="strData"></param>
        void UpdateB1DB1(string strData);
        void UpdateB1DB2(string strData);
        #endregion
        #region 事件
        //event EventHandler<DevDBDataArgs> eventUpdateA1HouseInDB1;
        //event EventHandler<DevDBDataArgs> eventUpdateB1HouseInDB1;
        #endregion
    }
}
