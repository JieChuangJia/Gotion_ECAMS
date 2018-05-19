/**  版本信息模板在安装目录下，可自行修改。
* StockDetail.cs
*
* 功 能： N/A
* 类 名： StockDetail
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013-12-1 12:50:04   N/A    初版
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
	public partial class StockDetailModel
	{
		public StockDetailModel()
		{}
        #region Model
        private long _stockdetailid;
        private long _stocklistid;
        private string _trayid;
        private string _corecode;
        private int? _corepositionid;
        private int? _corequalitysign;
        private string _remarks;
        /// <summary>
        /// 库存详细ID
        /// </summary>
        public long StockDetailID
        {
            set { _stockdetailid = value; }
            get { return _stockdetailid; }
        }
        /// <summary>
        /// 库存列表ID
        /// </summary>
        public long StockListID
        {
            set { _stocklistid = value; }
            get { return _stocklistid; }
        }
        /// <summary>
        /// 料框条码
        /// </summary>
        public string TrayID
        {
            set { _trayid = value; }
            get { return _trayid; }
        }
        /// <summary>
        /// 电芯条码
        /// </summary>
        public string CoreCode
        {
            set { _corecode = value; }
            get { return _corecode; }
        }
        /// <summary>
        /// 电芯位置ID
        /// </summary>
        public int? CorePositionID
        {
            set { _corepositionid = value; }
            get { return _corepositionid; }
        }
        /// <summary>
        /// 电芯质量标记
        /// </summary>
        public int? CoreQualitySign
        {
            set { _corequalitysign = value; }
            get { return _corequalitysign; }
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

