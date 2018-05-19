using System;
namespace ECAMSDataAccess
{
    /// <summary>
    /// TaskTypeModel:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class TaskTypeModel
    {
        public TaskTypeModel()
        { }
        #region Model
        private int _tasktypecode;
        private string _tasktypename;
        private string _tasktypevalue;
        private int _startlogicareaid;
        private string _startdevice;
        private int _endlogicareaid;
        private string _enddevice;
        private string _productstartstatus;
        private string _productendstatus;
        private int? _needtime;
        private string _tasktypemode;
        private string _tasktypedescribe;
        /// <summary>
        /// 任务类型编码
        /// </summary>
        public int TaskTypeCode
        {
            set { _tasktypecode = value; }
            get { return _tasktypecode; }
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
        /// 
        /// </summary>
        public string TaskTypeValue
        {
            set { _tasktypevalue = value; }
            get { return _tasktypevalue; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int StartLogicAreaID
        {
            set { _startlogicareaid = value; }
            get { return _startlogicareaid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string StartDevice
        {
            set { _startdevice = value; }
            get { return _startdevice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int EndLogicAreaID
        {
            set { _endlogicareaid = value; }
            get { return _endlogicareaid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string EndDevice
        {
            set { _enddevice = value; }
            get { return _enddevice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ProductStartStatus
        {
            set { _productstartstatus = value; }
            get { return _productstartstatus; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ProductEndStatus
        {
            set { _productendstatus = value; }
            get { return _productendstatus; }
        }
        /// <summary>
        /// 任务流程所需时间 如老化3天 单位小时
        /// </summary>
        public int? NeedTime
        {
            set { _needtime = value; }
            get { return _needtime; }
        }
        /// <summary>
        /// 任务执行模式，分为手动和自动
        /// </summary>
        public string TaskTypeMode
        {
            set { _tasktypemode = value; }
            get { return _tasktypemode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TaskTypeDescribe
        {
            set { _tasktypedescribe = value; }
            get { return _tasktypedescribe; }
        }
        #endregion Model


    }
}

