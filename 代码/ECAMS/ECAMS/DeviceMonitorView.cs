using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ECAMSPresenter;

namespace ECAMS
{
    public partial class DeviceMonitorView : BaseView,IDeviceMonitorView
    {
        #region 全局变量
        private delegate  void  DelegateRefreshGridView(DataTable dtTaskStatus, DataTable dtDB1, DataTable dtDB2);
        #endregion
        #region 初始化
        public DeviceMonitorView()
        {
            InitializeComponent();
        }
        private void DeviceMonitorView_Load(object sender, EventArgs e)
        {
            OnIniDeviceList();
        }
        protected override object CreatePresenter()
        {
            DeviceMonitorPresenter devicePre = new DeviceMonitorPresenter(this);
            for (int i = 0; i < allPresenterList.Count; i++)
            {
                if (allPresenterList[i].GetType() == typeof(DeviceMonitorPresenter))
                {
                    allPresenterList.RemoveAt(i);
                }
            }
            allPresenterList.Add(devicePre);
            return devicePre;
        }

        /// <summary>
        /// 获取指定逻辑
        /// </summary>
        /// <param name="presenterType"></param>
        /// <returns></returns>
        public object GetPresenter(Type presenterType)
        {
            object presenter = null;
            for (int i = 0; i < allPresenterList.Count; i++)
            {
                if (allPresenterList[i].GetType() == presenterType)
                {
                    presenter = allPresenterList[i];
                    break;
                }
            }
            return presenter;
        }

        #endregion

        #region UI事件
        private void tsb_DeviceList_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnDeviceChanged();

        }
        private void tsb_MonitorExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void tsb_Refresh_Click(object sender, EventArgs e)
        {
            OnDeviceChanged();
        }
        #endregion

        #region  实现接口事件
        public event EventHandler eventFormLoad;
        public event EventHandler<DeviceEventArgs> eventDeviceChanged;
      
        #endregion

        #region 实现接口方法
        /// <summary>
        /// 初始化设备下拉列表
        /// </summary>
        /// <param name="dt"></param>
       public void IniDeviceList(List<string> deviceList)
        {
            this.tsb_DeviceList.Items.Clear();
            for (int i = 0; i < deviceList.Count; i++)
            {
                this.tsb_DeviceList.Items.Add(deviceList[i]);
            }
            if (this.tsb_DeviceList.Items.Count > 0)
            {
                this.tsb_DeviceList.SelectedIndex = 0;
            }
       }

       /// <summary>
       /// 刷新任务、db1、db2数据
       /// </summary>
       /// <param name="dtTaskStatus"></param>
       /// <param name="dtDB1"></param>
       /// <param name="dtDB2"></param>
       public void RefreshGridView(DataTable dtTaskStatus, DataTable dtDB1, DataTable dtDB2)
       {
           if (this.dgv_DB1.InvokeRequired || this.dgv_DB2.InvokeRequired || this.dgv_TaskStatus.InvokeRequired)
           {
               DelegateRefreshGridView dtgvDeleate = new DelegateRefreshGridView(RefreshGridView);
               this.Invoke(dtgvDeleate, new object[3] { dtTaskStatus, dtDB1, dtDB2 });
           }
           else
           {
               this.dgv_TaskStatus.DataSource = dtTaskStatus;
               IniDatagridviewStyle(this.dgv_TaskStatus);

               this.dgv_DB1.DataSource = dtDB1;
               IniDatagridviewStyle(this.dgv_DB1);

               this.dgv_DB2.DataSource = dtDB2;
               IniDatagridviewStyle(this.dgv_DB2);
           }
       }
        #endregion

        #region 触发事件函数
   
       private void OnDeviceChanged()
       {
           if (this.eventDeviceChanged != null)
           {
               DeviceEventArgs deviceArgs = new DeviceEventArgs();
               if(this.tsb_DeviceList.SelectedItem!= null)
               {
                   deviceArgs.DeviceID = this.tsb_DeviceList.SelectedItem.ToString();
                   this.eventDeviceChanged.Invoke(this, deviceArgs);
               }
           }
       }
       private void OnIniDeviceList()
       {
           if (this.eventFormLoad != null)
           {
               this.eventFormLoad.Invoke(this, null);

           }
       }
        #endregion

        #region 初始化界面DataGridView显示样式
        /// <summary>
        /// 显示风格
        /// </summary>
        /// <param name="dgv"></param>
       private void IniDatagridviewStyle(DataGridView dgv)
       {
           for (int i = 0; i < dgv.Columns.Count;i++ )
           {
               dgv.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
               dgv.Columns[i].ReadOnly = true;
      
               //dgv.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

           }
       }
        #endregion





    }
}
