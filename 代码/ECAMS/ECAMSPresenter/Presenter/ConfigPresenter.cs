using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECAMSDataAccess;
using ECAMSModel;

namespace ECAMSPresenter
{
    public class ConfigPresenter :BasePresenter<IConfigView>
    {
        #region 全局变量
        private readonly TaskTypeBll bllTaskType = new TaskTypeBll();   //业务流程逻辑
        private MainPresenter mainPre = null;                          //获取主控逻辑
        #endregion

        #region 初始化
        public ConfigPresenter(IConfigView view): base(view)
        {
            mainPre = (MainPresenter)this.View.GetPresenter(typeof(MainPresenter));
        }

        protected override void OnViewSet()
        {
            this.View.eventLoadData += LoadDataEventHandler;
            this.View.eventSetTaskType += SetTaskTypeEventHandler;

        }
        #endregion

        #region 实现函数
        private void LoadDataEventHandler(object sender, EventArgs e)
        {
            List<TaskTypeModel> taskTypeList = bllTaskType.GetAutoTaskList(EnumTaskName.电芯出库_A1.ToString(), EnumTaskName.电芯出库_B1.ToString(), EnumTaskName.分容出库_A1.ToString());
            this.View.ShowData(taskTypeList);
        }

        private void SetTaskTypeEventHandler(object sender, SetTaskTypeEventArgs e)
        {
            if (e.TaskTypeData != null)
            {
                bool isSetStatus = true;
                for (int i = 0; i < e.TaskTypeData.Rows.Count; i++)
                {
                    int taskTypeCode = int.Parse(e.TaskTypeData.Rows[i]["taskTypeID"].ToString());
                    string taskMode = e.TaskTypeData.Rows[i]["taskMode"].ToString();
                    int needTime = int.Parse(e.TaskTypeData.Rows[i]["needTime"].ToString());
                    bool updateStatus = bllTaskType.SetTaskType(taskTypeCode, taskMode, needTime);
                    if (!updateStatus)
                    {
                        isSetStatus = false;
                        break;
                    }
                }
                if (isSetStatus)
                {
                    this.View.ShowMessage("信息提示", "出库设置成功！");
                    mainPre.View.SetProcessTaskMode();
                }
                else
                {
                    this.View.ShowMessage("信息提示", "出库设置失败！");
                }
            }
        }
        #endregion
    }
}
