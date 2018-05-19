/**  版本信息模板在安装目录下，可自行修改。
* ManageTaskDetail.cs
*
* 功 能： N/A
* 类 名： ManageTaskDetail
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
	/// 逻辑区域
	/// </summary>
	[Serializable]
	public partial class ManageTaskDetailModel
	{
		public ManageTaskDetailModel()
		{}
		#region Model
		private long _taskdetailid;
		private long _taskdetaillistid;
		private string _corecode;
		private byte[] _remark;
		/// <summary>
		/// 任务详细 ID
		/// </summary>
		public long TaskDetailID
		{
			set{ _taskdetailid=value;}
			get{return _taskdetailid;}
		}
		/// <summary>
		/// 任务详细列表ID
		/// </summary>
		public long TaskDetailListID
		{
			set{ _taskdetaillistid=value;}
			get{return _taskdetaillistid;}
		}
		/// <summary>
		/// 电芯条码
		/// </summary>
		public string CoreCode
		{
			set{ _corecode=value;}
			get{return _corecode;}
		}
		/// <summary>
		/// 备注
		/// </summary>
		public byte[] Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		#endregion Model

	}
}

