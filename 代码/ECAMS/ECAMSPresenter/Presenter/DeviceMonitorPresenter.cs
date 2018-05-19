using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ECAMSDataAccess;
using ECAMSModel;
using PLCControl;
namespace ECAMSPresenter
{
    public class DeviceMonitorPresenter:BasePresenter<IDeviceMonitorView>
    {
        #region 全局变量
        private readonly DeviceBll bllDevice = new DeviceBll();
        #endregion
        #region 初始化
        public DeviceMonitorPresenter(IDeviceMonitorView view) :
            base(view)
        {

        }

        protected override void OnViewSet()
        {
            this.View.eventFormLoad += FormLoadEventHandler;
            this.View.eventDeviceChanged += DeviceChangedEventHandler;
            
        }
        #endregion

        #region 事件实现方法
        private void FormLoadEventHandler(object sender, EventArgs e)
        {
            List<DeviceModel> deviceList = bllDevice.GetModelList("");
            List<string> deviceIDList = new List<string>();
            for (int i = 0; i < deviceList.Count; i++)
            {
                if (deviceList[i].BytesLenDB1 > 0)
                {
                    deviceIDList.Add(deviceList[i].DeviceID);
                }
                
            }
            this.View.IniDeviceList(deviceIDList);
        }

        private void DeviceChangedEventHandler(object sender, DeviceEventArgs e)
        { 
            //通过设备编号调用控制层切换设备的接口
       
            DataTable dt1 = null;
            DataTable dt2 = null;
            DataTable dtTask = null;
            if (!MainPresenter.wcs.GetDevRunningInfo(e.DeviceID, ref dtTask, ref dt1, ref dt2))
            {
                
                return;
            }
            View.RefreshGridView(dtTask, dt1, dt2);
        }

        /// <summary>
        /// 控制层显示设备监控的实现函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void DeviceMonitorEventHandler(object sender, DeviceMonitorEventArgs e)
        //{
        //    this.View.RefreshGridView(e.DtTask, e.DtDB1, e.DtDB2);
        //}
        #endregion
    }
}
