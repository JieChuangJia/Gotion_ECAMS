using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECAMSModel;
using ECAMSDataAccess;
namespace WCSTest
{
    public abstract class TestBusinessBase
    {
        public event EventHandler<LogEventArgs> eventLogDisp;

        public TestBusinessBase()
        {

        }
        protected void AddLog(string content)
        {
            LogModel log = new LogModel();
            log.logCategory = EnumLogCategory.控制层日志.ToString();
            log.logContent = content;
            log.logTime = System.DateTime.Now;
            AddLog(log);
        }
        /// <summary>
        /// 增加一条日志记录
        /// </summary>
        protected void AddLog(LogModel log)
        {
            if (eventLogDisp != null)
            {
                LogEventArgs arg = new LogEventArgs();
                // arg.happenTime = System.DateTime.Now;
                // arg.logMes = log.logContent;
                arg.LogTime = System.DateTime.Now;
                arg.LogCate = EnumLogCategory.控制层日志;
                arg.LogContent = log.logContent;
                arg.LogType = EnumLogType.调试信息;
                eventLogDisp.Invoke(this, arg);
            }

        }
        /// <summary>
        /// 处理业务
        /// </summary>
        /// <returns></returns>
        public virtual bool ExeBusiness()
        {

            return true;
        }
    }
}
