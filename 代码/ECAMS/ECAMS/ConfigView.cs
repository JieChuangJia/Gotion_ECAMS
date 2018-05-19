using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ECAMSPresenter;
using ECAMSDataAccess;

namespace ECAMS
{
    public partial class ConfigView : BaseView, IConfigView
    {
        #region 全局变量
        #endregion 

        #region 初始化
        public ConfigView()
        {
            InitializeComponent();
        }
        protected override object CreatePresenter()
        {
            ConfigPresenter configPre = new ConfigPresenter(this);
            for (int i = 0; i < allPresenterList.Count; i++)
            {
                if (allPresenterList[i].GetType() == typeof(ConfigPresenter))
                {
                    allPresenterList.RemoveAt(i);
                }
            }
            allPresenterList.Add(configPre);
            return configPre;
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
        private void bt_configExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgv_WorkFlow_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ConfigView_Load(object sender, EventArgs e)
        {
            OnLoadData();
        }

         private void bt_ConfigSet_Click(object sender, EventArgs e)
        {
             OnSetTaskType();
        }
        #endregion

        #region 实现IConfigView事件
        /// <summary>
        /// 窗体初始化加载事件
        /// </summary>
        public event EventHandler eventLoadData;

        /// <summary>
        /// 设置任务流程
        /// </summary>
        public event EventHandler<SetTaskTypeEventArgs> eventSetTaskType;
        #endregion

        #region 实现IConfigView方法
        /// <summary>
        /// 显示信息
        /// </summary>
        /// <param name="titleStr"></param>
        /// <param name="contentStr"></param>
        public void ShowMessage(string titleStr, string contentStr)
        {
            MessageBox.Show(contentStr, titleStr, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 显示到datagridview
        /// </summary>
        /// <param name="taskTypeList"></param>
        public void ShowData(List<TaskTypeModel> taskTypeList)
        {
            this.dgv_WorkFlowSet.Rows.Clear();       
            for (int i = 0; i < taskTypeList.Count; i++)
            {
                this.dgv_WorkFlowSet.Rows.Add();
                this.dgv_WorkFlowSet.Rows[i].Cells["taskTypeID"].Value = taskTypeList[i].TaskTypeCode;
                this.dgv_WorkFlowSet.Rows[i].Cells["taskTypeName"].Value = taskTypeList[i].TaskTypeName;
                this.dgv_WorkFlowSet.Rows[i].Cells["needTime"].Value = taskTypeList[i].NeedTime;
              
                this.dgv_WorkFlowSet.Rows[i].Cells["taskMode"].Value = taskTypeList[i].TaskTypeMode.Trim();
                this.dgv_WorkFlowSet.Rows[i].Cells["taskDescribe"].Value = taskTypeList[i].TaskTypeDescribe;
            }
        }
        #endregion

        #region 触发IConfigView事件
        private void OnLoadData()
        {
            if (this.eventLoadData != null)
            {
                this.eventLoadData.Invoke(this, null);
            }
        }

        private void OnSetTaskType()
        {
            if (this.eventSetTaskType != null)
            {
                SetTaskTypeEventArgs taskArgs = new SetTaskTypeEventArgs();


                DataTable dt = GetDgvToTable(this.dgv_WorkFlowSet);
                taskArgs.TaskTypeData = dt;

                this.eventSetTaskType.Invoke(this, taskArgs);
            }
        }
        #endregion

        #region UI私有方法
        public DataTable GetDgvToTable(DataGridView dgv)
        {
            DataTable dt = new DataTable(); 
            for (int count = 0; count < dgv.Columns.Count; count++)
            {
                DataColumn dc = new DataColumn(dgv.Columns[count].Name.ToString(),typeof(string)); 
                dt.Columns.Add(dc);
            }
            for (int count = 0; count < dgv.Rows.Count; count++)
            {
                DataRow dr = dt.NewRow(); for (int countsub = 0; countsub < dgv.Columns.Count; countsub++)
                {
                    dr[countsub] = Convert.ToString(dgv.Rows[count].Cells[countsub].Value);
                }
                dt.Rows.Add(dr);
            } return dt;
        }
        #endregion


    }
  
}
