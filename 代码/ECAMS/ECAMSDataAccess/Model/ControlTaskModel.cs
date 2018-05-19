using System;
namespace ECAMSDataAccess
{
    /// <summary>
    /// 控制接口
    /// </summary>
    [Serializable]
    public partial class ControlTaskModel
    {
        public ControlTaskModel()
        { }
        #region Model
        private long _controltaskid;
        private long _taskid;
        private string _tasktypename;
        private int _tasktypecode;
        private string _tasktype;
        private string _controlcode;
        private string _startarea;
        private string _startdevice;
        private string _targetarea;
        private string _targetdevice;
        private string _taskparameter;
        private string _taskstatus;
        private string _createmode;
        private string _taskphase;
        private DateTime _createtime;
        /// <summary>
        /// 
        /// </summary>
        public long ControlTaskID
        {
            set { _controltaskid = value; }
            get { return _controltaskid; }
        }
        /// <summary>
        /// 任务ID
        /// </summary>
        public long TaskID
        {
            set { _taskid = value; }
            get { return _taskid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TaskTypeName
        {
            set { _tasktypename = value; }
            get { return _tasktypename; }
        }
        /// <summary>
        /// 任务类型编码
        /// </summary>
        public int TaskTypeCode
        {
            set { _tasktypecode = value; }
            get { return _tasktypecode; }
        }
        /// <summary>
        /// 任务类型（出入库）
        /// </summary>
        public string TaskType
        {
            set { _tasktype = value; }
            get { return _tasktype; }
        }
        /// <summary>
        /// 控制条码
        /// </summary>
        public string ControlCode
        {
            set { _controlcode = value; }
            get { return _controlcode; }
        }
        /// <summary>
        /// 开始区域
        /// </summary>
        public string StartArea
        {
            set { _startarea = value; }
            get { return _startarea; }
        }
        /// <summary>
        /// 开始设备
        /// </summary>
        public string StartDevice
        {
            set { _startdevice = value; }
            get { return _startdevice; }
        }
        /// <summary>
        /// 目标区域
        /// </summary>
        public string TargetArea
        {
            set { _targetarea = value; }
            get { return _targetarea; }
        }
        /// <summary>
        /// 目标设备
        /// </summary>
        public string TargetDevice
        {
            set { _targetdevice = value; }
            get { return _targetdevice; }
        }
        /// <summary>
        /// 任务参数
        /// </summary>
        public string TaskParameter
        {
            set { _taskparameter = value; }
            get { return _taskparameter; }
        }
        /// <summary>
        /// 任务状态
        /// </summary>
        public string TaskStatus
        {
            set { _taskstatus = value; }
            get { return _taskstatus; }
        }
        /// <summary>
        /// 任务创建模式（人工创建还是系统创建主要用在手动出库）
        /// </summary>
        public string CreateMode
        {
            set { _createmode = value; }
            get { return _createmode; }
        }
        /// <summary>
        /// 当前任务执行到的阶段
        /// </summary>
        public string TaskPhase
        {
            set { _taskphase = value; }
            get { return _taskphase; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        #endregion Model


    }
}

