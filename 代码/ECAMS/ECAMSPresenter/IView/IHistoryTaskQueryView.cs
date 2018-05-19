using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using ECAMSDataAccess;

namespace ECAMSPresenter
{
    public class QueryHisTaskEventArgs:EventArgs 
    {
        public DateTime StartTime{get;set;}
        public DateTime EndTime{get;set;}
        public string TasktypeName{get;set;}
        public bool TasktypeNameChecked { get; set; }

        public string StartPosition { get; set; }
        public bool StartPositionChecked { get; set; }

        public string EndPostion { get; set; }
        public bool EndPostionChecked { get; set; }
    }

    public interface IHistoryTaskQueryView:IBaseView
    {
        #region 事件
        /// <summary>
        /// 初始化任务类型名称combox
        /// </summary>
        event EventHandler eventIniTasktypeCom;
        event EventHandler<QueryHisTaskEventArgs> eventQueryTask;
        #endregion

        #region 方法
        void ShowHisTaskData(List<HistoryManageTaskModel> hisTaskList);
        void IniTaskTypeNameCom(DataSet ds);
        /// <summary>
        /// 作者:np
        /// 时间:2014年8月25日
        /// 内容:设置退出按钮可用状态
        /// </summary>
        /// <param name="enabled"></param>
        void SetExitButtonEnabled(bool enabled);
        #endregion

    }
}
