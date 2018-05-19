using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ECAMSModel; 
namespace ECAMSPresenter
{
    public class DataEventArgs : EventArgs
    {
        public EnumDataList EnumDataList { get; set; }
        public DataRow DataRow { get; set; }
        public long ListID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }

    public class SaveEventArgs : EventArgs
    {
        public EnumDataList EnumDataList { get; set; }
        public DataRow DataRow { get; set; }
        //public long ListID { get; set; }
        public object EditModel { get; set; }
    }

    public class DeleteEventArgs : EventArgs
    {
        public EnumDataList EnumDataList { get; set; }
        public long[] ListID { get; set; }
    }

    public class DatabaseEventArgs : EventArgs
    {
        public bool IsBackUp { get; set; }
        public string Path { get; set; }
    }
    public interface IDataManaView:IBaseView
    {
        #region 事件
        event EventHandler<DeleteEventArgs> eventDelete;
        event EventHandler<DataEventArgs> eventQuery;
        event EventHandler<SaveEventArgs> eventSave;
        event EventHandler eventFormatSystem;
        event EventHandler<DatabaseEventArgs> eventDatabaseBakRecover;
        #endregion

        #region 方法
        void RefreshDataList(DataTable dt,EnumDataList enumDataList);

        /// <summary>
        /// 弹出是否请求对话框
        /// </summary>
        /// <param name="content"></param>
        /// <returns>返回：0是，1：否</returns>
        int AskMessBox(string content);
        #endregion
    }
}
