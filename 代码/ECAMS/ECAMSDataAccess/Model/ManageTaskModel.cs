/**  版本信息模板在安装目录下，可自行修改。
* ManageTask.cs
*
* 功 能： N/A
* 类 名： ManageTask
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013-12-1 12:50:01   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：np　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace ECAMSDataAccess
{
    /// <summary>
    /// ManageTask:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class ManageTaskModel
    {
        public ManageTaskModel()
        { }
        #region Model
        private long _taskid;
        private string _taskstatus;
        private string _tasktypename;
        private string _tasktype;
        private string _taskcode;
        private string _taskstartarea;
        private string _taskstartpostion;
        private string _taskendarea;
        private string _taskendpostion;
        private string _taskcreateperson;
        private DateTime _taskcreatetime;
        private DateTime? _taskcompletetime;
        private string _taskparameter;
        /// <summary>
        /// 任务ID
        /// </summary>
        public long TaskID
        {
            set { _taskid = value; }
            get { return _taskid; }
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
        /// 业务流程名称
        /// </summary>
        public string TaskTypeName
        {
            set { _tasktypename = value; }
            get { return _tasktypename; }
        }
        /// <summary>
        /// 任务类型
        /// </summary>
        public string TaskType
        {
            set { _tasktype = value; }
            get { return _tasktype; }
        }
        /// <summary>
        /// 任务编码
        /// </summary>
        public string TaskCode
        {
            set { _taskcode = value; }
            get { return _taskcode; }
        }
        /// <summary>
        /// 任务开始库区
        /// </summary>
        public string TaskStartArea
        {
            set { _taskstartarea = value; }
            get { return _taskstartarea; }
        }
        /// <summary>
        /// 任务开始位置
        /// </summary>
        public string TaskStartPostion
        {
            set { _taskstartpostion = value; }
            get { return _taskstartpostion; }
        }
        /// <summary>
        /// 任务结束库区
        /// </summary>
        public string TaskEndArea
        {
            set { _taskendarea = value; }
            get { return _taskendarea; }
        }
        /// <summary>
        /// 任务目标位置
        /// </summary>
        public string TaskEndPostion
        {
            set { _taskendpostion = value; }
            get { return _taskendpostion; }
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
        public DateTime? TaskCompleteTime
        {
            set { _taskcompletetime = value; }
            get { return _taskcompletetime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TaskParameter
        {
            set { _taskparameter = value; }
            get { return _taskparameter; }
        }
        #endregion Model

    }
}



