using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ECAMSModel;
using ECAMSDataAccess;

namespace ECAMSPresenter
{
    public class ExportTxtEventArgs : EventArgs
    {
        public string FilePath { get; set; }
    }
    public class QueryLogEventArgs : EventArgs
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public EnumLogCategory LogCategory { get; set; }
        public EnumLogType LogType { get; set; }
        public bool IsLikeQuery { get; set; }
        public string LikeQueryStr { get; set; }
    }
    public interface ILogView:IBaseView
    {
        #region 事件
        event EventHandler eventIniLogCategory;
        event EventHandler eventIniLogType;
        event EventHandler<QueryLogEventArgs> eventQueryLog;
        event EventHandler<ExportTxtEventArgs> eventExportTxt;
        #endregion

        #region 方法
        void IniLogCategory(DataTable dt);
        void IniLogType(DataTable dt);
        void ShowLog(DataTable dt);
         
        void ShowMessage(string titleStr, string contentStr);
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
