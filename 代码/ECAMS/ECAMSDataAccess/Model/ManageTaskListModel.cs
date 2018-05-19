/**  版本信息模板在安装目录下，可自行修改。
* ManageTaskList.cs
*
* 功 能： N/A
* 类 名： ManageTaskList
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013-12-1 12:50:02   N/A    初版
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
	/// 逻辑区域
	/// </summary>
	[Serializable]
	public partial class ManageTaskListModel
	{
		public ManageTaskListModel()
		{}
        #region Model
        private long _tasklistid;
        private long _taskid;
        private string _productcode;
        private long _stocklistid;
        private string _taskstartposition;
        private string _taskendposition;
        private string _taskcreateperson;
        private string _productbatch;
        private DateTime _taskcreatetime;
        private DateTime? _taskcompletetime;
        private string _taskparameter;
        private string _taskremark;
        /// <summary>
        /// 任务列表ID
        /// </summary>
        public long TaskListID
        {
            set { _tasklistid = value; }
            get { return _tasklistid; }
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
        /// 产品码
        /// </summary>
        public string ProductCode
        {
            set { _productcode = value; }
            get { return _productcode; }
        }
        /// <summary>
        /// 库存ID
        /// </summary>
        public long StockListID
        {
            set { _stocklistid = value; }
            get { return _stocklistid; }
        }
        /// <summary>
        /// 任务开始位置
        /// </summary>
        public string TaskStartPosition
        {
            set { _taskstartposition = value; }
            get { return _taskstartposition; }
        }
        /// <summary>
        /// 任务目标位置
        /// </summary>
        public string TaskEndPosition
        {
            set { _taskendposition = value; }
            get { return _taskendposition; }
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
        /// 物料批次
        /// </summary>
        public string ProductBatch
        {
            set { _productbatch = value; }
            get { return _productbatch; }
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
        /// <summary>
        /// 任务备注
        /// </summary>
        public string TaskRemark
        {
            set { _taskremark = value; }
            get { return _taskremark; }
        }
        #endregion Model

	}
}

