using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ECAMSPresenter
{
    public class DeviceEventArgs:EventArgs
    {
        public string DeviceID{get;set;}
    }
    public interface IDeviceMonitorView:IBaseView
    {
        #region 事件
        event EventHandler eventFormLoad;
        event EventHandler<DeviceEventArgs> eventDeviceChanged;
        
        #endregion

        #region 方法
        /// <summary>
        /// 初始化设备下拉列表
        /// </summary>
        /// <param name="dt"></param>
        void IniDeviceList(List<string> deviceList);
        /// <summary>
        /// 刷新任务、db1、db2数据
        /// </summary>
        /// <param name="dtTaskStatus"></param>
        /// <param name="dtDB1"></param>
        /// <param name="dtDB2"></param>
        void RefreshGridView(DataTable dtTaskStatus,DataTable dtDB1,DataTable dtDB2);


        #endregion
    }
}
