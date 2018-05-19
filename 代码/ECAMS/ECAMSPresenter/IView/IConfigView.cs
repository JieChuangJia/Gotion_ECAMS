using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ECAMSDataAccess;

namespace ECAMSPresenter
{

    public class SetTaskTypeEventArgs:EventArgs
    {
        public DataTable TaskTypeData{get;set;}
    }

    public interface IConfigView:IBaseView
    {
        #region 事件
        /// <summary>
        /// 窗体初始化加载事件
        /// </summary>
        event EventHandler eventLoadData;

        /// <summary>
        /// 设置任务流程
        /// </summary>
        event EventHandler<SetTaskTypeEventArgs> eventSetTaskType;
        #endregion
        #region 方法
        /// <summary>
        /// 显示到datagridview
        /// </summary>
        /// <param name="taskTypeList"></param>
        void ShowData(List<TaskTypeModel> taskTypeList);

        void ShowMessage(string titleStr, string contentStr);
        #endregion
    }
}
