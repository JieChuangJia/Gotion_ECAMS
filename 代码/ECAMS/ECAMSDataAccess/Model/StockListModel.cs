/**  版本信息模板在安装目录下，可自行修改。
* StockList.cs
*
* 功 能： N/A
* 类 名： StockList
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013-12-1 12:50:05   N/A    初版
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
	/// 物料信息表
	/// </summary>
	[Serializable]
	public partial class StockListModel
	{
		public StockListModel()
		{}
        #region Model
        private long _stocklistid;
        private long _manataskid;
        private string _storehousename;
        private long _stockid;
        private string _productcode;
        private int _productnum;
        private string _productstatus;
        private string _productframecode;
        private string _productname;
        private string _goodssitename;
        private string _productbatchnum;
        private DateTime _inhousetime;
        private DateTime? _updatetime;
        private string _remarks;
        /// <summary>
        /// 库存列表ID
        /// </summary>
        public long StockListID
        {
            set { _stocklistid = value; }
            get { return _stocklistid; }
        }
        /// <summary>
        /// 管理任务ID 控制任务完成后 以此为标识
        /// </summary>
        public long ManaTaskID
        {
            set { _manataskid = value; }
            get { return _manataskid; }
        }
        /// <summary>
        /// 库房名称
        /// </summary>
        public string StoreHouseName
        {
            set { _storehousename = value; }
            get { return _storehousename; }
        }
        /// <summary>
        /// 库存ID
        /// </summary>
        public long StockID
        {
            set { _stockid = value; }
            get { return _stockid; }
        }
        /// <summary>
        /// 物料ID
        /// </summary>
        public string ProductCode
        {
            set { _productcode = value; }
            get { return _productcode; }
        }
        /// <summary>
        /// 物料数量
        /// </summary>
        public int ProductNum
        {
            set { _productnum = value; }
            get { return _productnum; }
        }
        /// <summary>
        /// 物料状态
        /// </summary>
        public string ProductStatus
        {
            set { _productstatus = value; }
            get { return _productstatus; }
        }
        /// <summary>
        /// 料框条码
        /// </summary>
        public string ProductFrameCode
        {
            set { _productframecode = value; }
            get { return _productframecode; }
        }
        /// <summary>
        /// 库存产品名称
        /// </summary>
        public string ProductName
        {
            set { _productname = value; }
            get { return _productname; }
        }
        /// <summary>
        /// 库存货位名称
        /// </summary>
        public string GoodsSiteName
        {
            set { _goodssitename = value; }
            get { return _goodssitename; }
        }
        /// <summary>
        /// 物料批次号
        /// </summary>
        public string ProductBatchNum
        {
            set { _productbatchnum = value; }
            get { return _productbatchnum; }
        }
        /// <summary>
        /// 入库时间
        /// </summary>
        public DateTime InHouseTime
        {
            set { _inhousetime = value; }
            get { return _inhousetime; }
        }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateTime
        {
            set { _updatetime = value; }
            get { return _updatetime; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks
        {
            set { _remarks = value; }
            get { return _remarks; }
        }
        #endregion Model
	}
}

