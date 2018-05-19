using System;
namespace ECAMSDataAccess
{
    /// <summary>
    /// 管理任务记录，方便日后查询
    ///
    /// </summary>
    [Serializable]
    public partial class HistoryManageTaskModel
    {
        public HistoryManageTaskModel()
        { }
        #region Model
        private long _historytaskid;
        private string _tasktypename;
        private string _tasktype;
        private int _productid;
        private string _productname;
        private string _taskstartpsotion;
        private string _taskstartarea;
        private string _taskendposition;
        private string _taskendaera;
        private string _taskcreateperson;
        private DateTime _taskcreatetime;
        private DateTime _taskcompletetime;
        private string _taskparameter;
        /// <summary>
        /// 历史任务表，已经完成的管理任务记录
        /// </summary>
        public long HistoryTaskID
        {
            set { _historytaskid = value; }
            get { return _historytaskid; }
        }
        /// <summary>
        /// 任务类型名称
        /// </summary>
        public string TaskTypeName
        {
            set { _tasktypename = value; }
            get { return _tasktypename; }
        }
        /// <summary>
        /// 任务类型（出库、入库）
        /// </summary>
        public string TaskType
        {
            set { _tasktype = value; }
            get { return _tasktype; }
        }
        /// <summary>
        /// 产品ID号
        /// </summary>
        public int ProductID
        {
            set { _productid = value; }
            get { return _productid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ProductName
        {
            set { _productname = value; }
            get { return _productname; }
        }
        /// <summary>
        /// 任务开始位置
        /// </summary>
        public string TaskStartPsotion
        {
            set { _taskstartpsotion = value; }
            get { return _taskstartpsotion; }
        }
        /// <summary>
        /// 任务开始区域
        /// </summary>
        public string TaskStartArea
        {
            set { _taskstartarea = value; }
            get { return _taskstartarea; }
        }
        /// <summary>
        /// 任务结束位置
        /// </summary>
        public string TaskEndPosition
        {
            set { _taskendposition = value; }
            get { return _taskendposition; }
        }
        /// <summary>
        /// 任务技术区域
        /// </summary>
        public string TaskEndAera
        {
            set { _taskendaera = value; }
            get { return _taskendaera; }
        }
        /// <summary>
        /// 任务创建人
        /// </summary>
        public string TaskCreatePerson
        {
            set { _taskcreateperson = value; }
            get { return _taskcreateperson; }
        }
        /// <summary>
        /// 任务创建时间
        /// </summary>
        public DateTime TaskCreateTime
        {
            set { _taskcreatetime = value; }
            get { return _taskcreatetime; }
        }
        /// <summary>
        /// 任务完成时间
        /// </summary>
        public DateTime TaskCompleteTime
        {
            set { _taskcompletetime = value; }
            get { return _taskcompletetime; }
        }
        /// <summary>
        /// 任务参数
        /// </summary>
        public string TaskParameter
        {
            set { _taskparameter = value; }
            get { return _taskparameter; }
        }
        #endregion Model

    }
}

