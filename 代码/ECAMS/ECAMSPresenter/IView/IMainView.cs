using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECAMSModel;
using ECAMSDataAccess;
using System.Data;
namespace ECAMSPresenter
{
    public class SetLimitEventArgs:EventArgs 
    {
        public int RoleID{get;set;}
    }

    public class PlcCommOpEventArgs : EventArgs
    {
        public EnumDevPLC PlcID;
    }
    public interface IMainView:IBaseView
    {

        #region 事件
        /// <summary>
        /// 启动系统
        /// </summary>
        event EventHandler eventStartSystem;
        /// <summary>
        /// 停止系统
        /// </summary>
        event EventHandler eventStopSystem;
        event EventHandler<SetLimitEventArgs> eventSetLimit;
        event EventHandler eventChangeUser;
        event EventHandler<ECAMSErrorEventArgs> eventSaveErrorLog;
        event EventHandler<LogEventArgs> eventSaveLog;
        event EventHandler eventExitSys;
        event EventHandler eventFormLoad;
        event EventHandler eventRefreshBatch;
        event EventHandler<PlcCommOpEventArgs> eventClosePLCComm; //关闭PLC通信
        event EventHandler<PlcCommOpEventArgs> eventReOpenPLCComm; //重新打开PLC通信
        event EventHandler eventSetProcessTaskMode;
        #endregion

        #region 方法
        void AddLog(EnumLogCategory logcate, EnumLogType EnumLogType,string logContent);
        void AddLogErrorCode(EnumLogCategory logcate, EnumLogType EnumLogType, string logContent,int errorCode);
        void SetLimit(string [] funclist);
        void SetViewHide();
        void ShowView();
        void OnSetLimit(int roleID);
        void OnStop();
        void ExitSystem();
        /// <summary>
        /// 弹出是否请求对话框
        /// </summary>
        /// <param name="content"></param>
        /// <returns>返回：0是，1：否</returns>
        int AskMessBox(string content);
        void RefreshDeviceStatus(DataTable dt);
        /// <summary>
        /// 初始化dgv列表
        /// </summary>
        /// <param name="count"></param>
        //void IniDGVDevice(List<DeviceModel> deviceModelList);
        /// <summary>
        /// 刷新通信设备状态
        /// </summary>
        /// <param name="dtComm"></param>
        void RefreshCommDeviceGridView(DataTable dtComm);
        /// <summary>
        /// 作者:np
        /// 时间:2014年5月19日
        /// 内容:刷新产品批次下拉列表
        /// </summary>
        void RefreshBatchList(DataTable dt);
        /// <summary>
        /// 作者:np
        /// 时间:2014年5月20日
        /// 内容:存储产品出库批次
        /// </summary>
        /// <returns></returns>
        bool SaveProductBatch();
        /// <summary>
        /// 作者:np
        /// 时间:2014年5月20日
        /// 内容:加载出库批次
        /// </summary>
        /// <returns></returns>
        Dictionary<string, List<string>> LoadOutStorageBatchNum();

        /// <summary>
        /// 作者:np
        /// 时间:2014年5月27日
        /// 内容:设置开始、停止按钮可用状态
        /// </summary>
        /// <param name="enabled"></param>
        void SetStartEnabled(bool enabled);
        void SetStopEnabled(bool enabled);
        /// <summary>
        /// 作者:np
        /// 时间:2014年6月13日
        /// 内容:获取登录角色名称
        /// </summary>
        /// <returns></returns>
        string GetCurrentRoleName();
        /// <summary>
        /// 作者:np
        /// 时间:2014年6月13日
        /// 内容:设置登录角色名称
        /// </summary>
        /// <returns></returns>
        void SetCurrentRoleName(string roleName);

        /// <summary>
        /// 作者:np
        /// 时间:2014年6月16日
        /// 内容:关闭所有子窗体
        /// </summary>
        void CloseAllChildForm();
        void SetProcessTaskMode();
        /// <summary>
        /// 获取授权时间
        /// </summary>
        /// <returns></returns>
        DateTime GetLicenceDate();
        #endregion
    }
}
