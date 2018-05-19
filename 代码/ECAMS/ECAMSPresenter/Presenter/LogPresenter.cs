using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECAMSDataAccess;
using System.Data;
using System.IO;
using System.ComponentModel;
using System.Threading;
namespace ECAMSPresenter
{
    public class LogPresenter:BasePresenter<ILogView>
    { 
        #region 全局变量
        private readonly LogBll bllLog = new LogBll();
        private List<LogModel> currentQueryLog = new List<LogModel>();    
        #endregion

        #region 初始化
        public LogPresenter(ILogView view)
            : base(view)
        {
          
        }

        protected override void OnViewSet()
        {
            this.View.eventIniLogCategory += IniLogCategoryEventHandler;
            this.View.eventIniLogType += IniLogTypeEventHandler;
            this.View.eventQueryLog += QueryLogEventHandler;
            this.View.eventExportTxt += ExportTxtEventHandler;

        }
        #endregion

        #region 实现函数
        private void ExportTxtEventHandler(object sender, ExportTxtEventArgs e)
        {
            if (this.currentQueryLog.Count == 0)
            {
                this.View.ShowMessage("信息提示", "数据为空，没有数据导出！请查询日志数据！");
                return;
            }
            StringBuilder logsb = new StringBuilder();
            for (int i = 0; i < currentQueryLog.Count; i++)
            {
                logsb.Append("时间："+currentQueryLog[i].logTime.ToString()+"  "+currentQueryLog[i].logCategory + "  " + currentQueryLog[i].logType + 
                    "  " + currentQueryLog[i].logContent+"  "+currentQueryLog[i].warningCode  +"\r\n");
            }
            if (ExportTxt(e.FilePath, logsb.ToString()))
            {
                this.View.ShowMessage("信息提示", "日志导出成功！");
            }
            else
            {
                this.View.ShowMessage("信息提示", "日志导出失败！");
            }

        }
        private void QueryLogEventHandler(object sender, QueryLogEventArgs e)
        {        
            bool openProgress = this.View.OpenProgressBar();
            if (openProgress)
            {
                this.View.SetExitButtonEnabled(false);
            
                DataTable dt = bllLog.GetLogModelList(e.StartTime, e.EndTime, e.LogCategory.ToString(), e.LogType.ToString(), e.IsLikeQuery, e.LikeQueryStr);
                this.View.ShowLog(dt);
              
                //List<LogModel> logList = bllLog.GetModelList(e.StartTime, e.EndTime, e.LogCategory.ToString(), e.LogType.ToString(), e.IsLikeQuery, e.LikeQueryStr);
                //currentQueryLog = logList;
                //this.View.ShowLog(logList);
                this.View.CloseProgressBar();
                this.View.SetExitButtonEnabled(true);
            }
        }
        private void IniLogCategoryEventHandler(object sender, EventArgs e)
        {
            DataTable dt = bllLog.DistinctLogCategory();
            this.View.IniLogCategory(dt);
        }

        private void IniLogTypeEventHandler(object sender, EventArgs e)
        {
            DataTable dt = bllLog.DistinctLogType();
            this.View.IniLogType(dt);
        }

        #endregion

        #region 导出日志写入TXT文件
     
        private bool ExportTxt(string filePath,string mes)
        {
            StreamWriter writeStream = new StreamWriter(filePath, true, System.Text.Encoding.Default);
            try
            {
                writeStream.Write( mes);
                writeStream.Close();
                return true;
            }
            catch (System.Exception ex)
            {
                this.View.ShowMessage("系统错误", ex.StackTrace);
                return false;
            }
            finally
            {
                writeStream.Close();

            }
        }
        #endregion

        #region 私有函数
       
        #endregion
    }
}
