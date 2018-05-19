using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ECAMSModel;
using ECAMSPresenter;
using ECAMSDataAccess;

namespace ECAMS
{
    public partial class StockManaView :BaseView,IStockManaView
    {
        #region 全局变量
        private List<StoreHouseProper> houseProList = new List<StoreHouseProper>();
        private delegate void RefreshDGVDataInvoke(List<View_QueryStockListModel> modelList);
        private delegate void ShowTrayCoreNumInvoke(string coreNum);
        private delegate void ShowStockDetailInvoke(List<View_StockListDetailModel> stockDetailModelList, List<TB_Tray_indexModel> trayDetailModelList);
        #endregion

        #region 初始化
        public StockManaView()
        {
            InitializeComponent();
        }

        private void StockManaView_Load(object sender, EventArgs e)
        {
            IniControlValue();
            OnLoadEvent();
        }
       
        protected override object CreatePresenter()
        {
            StockManaPresenter managerPre = new StockManaPresenter(this);
            for (int i = 0; i < allPresenterList.Count; i++)
            {
                if (allPresenterList[i].GetType() == typeof(StockManaPresenter))
                {
                    allPresenterList.RemoveAt(i);
                }
            }
            allPresenterList.Add(managerPre);
            return managerPre;
        }

        /// <summary>
        /// 获取指定逻辑
        /// </summary>
        /// <param name="presenterType"></param>
        /// <returns></returns>
        public object GetPresenter(Type presenterType)
        {
            object presenter = null;
            for (int i = 0; i < allPresenterList.Count; i++)
            {
                if (allPresenterList[i].GetType() == presenterType) 
                {
                    presenter = allPresenterList[i];
                    break;
                }
            }
            return presenter;
        }

        #endregion

        #region UI事件
        private void 料框详细ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.dgv_GSDetail.SelectedRows.Count > 0)
            {
                string trayId = this.dgv_GSDetail.CurrentRow.Cells["trayID"].Value.ToString();
                OnTrayDetail(trayId);
            }
        }
        private void dgv_GSDetail_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (this.dgv_GSDetail.SelectedRows.Count > 0)
            {
                string trayId = this.dgv_GSDetail.CurrentRow.Cells["trayID"].Value.ToString();
                OnTrayDetail(trayId);
            }
        }

    
        private void tscb_StoreHouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.tscb_ProductStatus.Items.Clear();
            if (tscb_StoreHouse.SelectedItem.ToString() == EnumStoreHouse.A1库房.ToString())//不同库房流程不同
            {
                this.tscb_ProductStatus.Items.Add(EnumProductStatus.A1库静置10小时.ToString());
                this.tscb_ProductStatus.Items.Add(EnumProductStatus.A1库老化3天.ToString());
                this.tscb_ProductStatus.Items.Add("所有");
                this.tscb_ProductStatus.SelectedIndex = 2;

                this.tscb_GoodsSiteStatus.Items.Clear();
                this.tscb_GoodsSiteStatus.Items.Add(EnumGSStoreStatus.空货位.ToString());
                this.tscb_GoodsSiteStatus.Items.Add(EnumGSStoreStatus.有货.ToString());
            }
            else if (tscb_StoreHouse.SelectedItem.ToString() == EnumStoreHouse.B1库房.ToString())
            {
                this.tscb_ProductStatus.Items.Add(EnumProductStatus.B1库静置10天.ToString());
                this.tscb_ProductStatus.Items.Add(EnumProductStatus.空料框.ToString());
                this.tscb_ProductStatus.Items.Add("所有");
                this.tscb_ProductStatus.SelectedIndex = 2;

                this.tscb_GoodsSiteStatus.Items.Clear();
                this.tscb_GoodsSiteStatus.Items.Add(EnumGSStoreStatus.空货位.ToString());
                this.tscb_GoodsSiteStatus.Items.Add(EnumGSStoreStatus.空料框.ToString());
                this.tscb_GoodsSiteStatus.Items.Add(EnumGSStoreStatus.有货.ToString());
            }
            else
            {
                this.tscb_ProductStatus.Items.Add(EnumProductStatus.A1库静置10小时.ToString());
                this.tscb_ProductStatus.Items.Add(EnumProductStatus.B1库静置10天.ToString());
                this.tscb_ProductStatus.Items.Add(EnumProductStatus.A1库老化3天.ToString());
                this.tscb_ProductStatus.Items.Add(EnumProductStatus.空料框.ToString());
                this.tscb_ProductStatus.Items.Add("所有");
                this.tscb_ProductStatus.SelectedIndex = 4;
            }


            this.tscb_StockRow.Items.Clear();
            StoreHouseProper housePro = GetHousePro((EnumStoreHouse)Enum.Parse(typeof(EnumStoreHouse), this.tscb_StoreHouse.Text));
            for (int i = 0; i < housePro.Rows; i++)
            {
                if (i == 0)
                {
                    this.tscb_StockRow.Items.Add("所有");
                }
                this.tscb_StockRow.Items.Add(i + 1);
            }
            this.tscb_StockRow.Text = "所有";
            this.tscb_StockColumn.Items.Clear();
            for (int i = 0; i < housePro.Columns; i++)
            {
                if (i == 0)
                {
                    this.tscb_StockColumn.Items.Add("所有");
                }
                this.tscb_StockColumn.Items.Add(i + 1);
            }
            this.tscb_StockColumn.Text = "所有";
            this.tscb_StockLayer.Items.Clear();
            for (int i = 0; i < housePro.Layers; i++)
            {
                if (i == 0)
                {
                    this.tscb_StockLayer.Items.Add("所有");
                }
                this.tscb_StockLayer.Items.Add(i + 1);
            }
            this.tscb_StockLayer.Text = "所有";

            this.cb_QueryGSTaskStatus.SelectedIndex = 0;
            this.cb_QueryGSStoreType.SelectedIndex = 0;
            this.cb_QueryGSTaskType.SelectedIndex = 0;

            this.cb_OutStorageBatchNum.Text = "所有";

           
        }

        private void tscb_ProductStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.tscb_workFlowTime.Items.Clear();
            if (this.tscb_ProductStatus.SelectedItem != null && this.tscb_ProductStatus.SelectedItem.ToString() == EnumProductStatus.空料框.ToString())
            {
                this.tscb_workFlowTime.Items.Add(EnumWorkFolwStatus.所有.ToString());
            }
            else
            {
                this.tscb_workFlowTime.Items.Add(EnumWorkFolwStatus.已达到.ToString());
                this.tscb_workFlowTime.Items.Add(EnumWorkFolwStatus.未到达.ToString());
                this.tscb_workFlowTime.Items.Add(EnumWorkFolwStatus.所有.ToString());
            }
        }

        private void tsbt_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void StockManaView_FormClosing(object sender, FormClosingEventArgs e)
        {
        
        }

        private void tscb_workFlowTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            //OnQueryStock();
        }

        private void tsbt_OutByHand_Click(object sender, EventArgs e)
        {
            OnOutHouseByHand();
        }

        private void tsb_deleteStockList_Click(object sender, EventArgs e)
        {
            OnDeleteStock();
        }

        private void 手动出库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnOutHouseByHand();
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnDeleteStock();
        }

        private void tsb_QueryStockList_Click(object sender, EventArgs e)
        {
            this.lb_ProductPatch.Text = this.cb_OutStorageBatchNum.Text;
            this.lb_HouseName.Text = this.tscb_StoreHouse.Text;
            OnQueryStock();
        }

      
        private void tscb_GoodsSiteStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.tscb_TaskRunStatus.Items.Clear();
            if (this.tscb_GoodsSiteStatus.SelectedItem != null && tscb_GoodsSiteStatus.SelectedItem.ToString() == EnumGSStoreStatus.空货位.ToString())
            {
                this.tscb_TaskRunStatus.Items.Add(EnumGSRunStatus.待用.ToString());
            }
            else
            {
                this.tscb_TaskRunStatus.Items.Add(EnumGSRunStatus.任务锁定.ToString());
                this.tscb_TaskRunStatus.Items.Add(EnumGSRunStatus.任务完成.ToString());
            }
        }

        private void tsb_SetByHand_Click(object sender, EventArgs e)
        {
            if (this.dgv_StockQuery.SelectedRows.Count == 0)
            {
                MessageBox.Show("请选择要设置的货位！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            OnSetGsStatus();
        }
        private void 手动设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnSetGsStatus();
        }

        private void 查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnQueryStock();
        }

        private void dgv_StockQuery_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dgv_StockQuery.SelectedRows.Count > 0)
            {
                int goodsiteID = int.Parse(this.dgv_StockQuery.CurrentRow.Cells["goodsSiteID"].Value.ToString());
                OnStockDetail(goodsiteID);
            }
        }

        #endregion

    
        #region 实现IStockManaView事件
        /// <summary>
        /// 查询库存事件
        /// </summary>
        public event EventHandler<QueryStockEventArgs> eventQueryStock;

        public event EventHandler<StockEventArgs> eventOutHouseByHand;

        public event EventHandler<DeleteStockEventArgs> eventDeleteStock;

        public event EventHandler eventLoad;

        public event EventHandler<StockStatusEventArgs> eventSetGsStatus;
        public event EventHandler<QueryGoodsSiteEventArgs> eventStockDetail;
        public event EventHandler<TrayEventArgs> eventTrayDetail;
        #endregion

        #region 实现IStockManaView方法
        public void ShowTrayCoreNum(string coreNum)
        {
            if (this.lb_CoreNum.InvokeRequired)
            {
                ShowTrayCoreNumInvoke stcnInvoke = new ShowTrayCoreNumInvoke(ShowTrayCoreNum);
                this.lb_CoreNum.Invoke(stcnInvoke, new object[1] { coreNum });
            }
            else
            {
                this.lb_CoreNum.Text = coreNum;
            }
        }

        public void ShowTrayDetail(List<TB_After_GradeDataModel> coreModelList)
        {
            TrayDetailView tdView = new TrayDetailView(coreModelList);
            tdView.ShowDialog();
        }

        /// <summary>
        /// 作者:np
        /// 时间:2014年6月1日
        /// 内容:显示货位详细
        /// </summary>
        /// <param name="stockDetailModelList"></param>
        /// <param name="trayDetailModelList"></param>
        public void ShowStockDetail(List<View_StockListDetailModel> stockDetailModelList, List<TB_Tray_indexModel> trayDetailModelList)
        {
            if (this.dgv_GSDetail.InvokeRequired)
            {
                ShowStockDetailInvoke ssdInvoke = new ShowStockDetailInvoke(ShowStockDetail);
                this.dgv_GSDetail.Invoke(ssdInvoke,new object[2]{stockDetailModelList,trayDetailModelList});
            }
            else
            {
                this.dgv_GSDetail.Rows.Clear();
                if (stockDetailModelList != null && stockDetailModelList.Count>0)
                {
                    for (int i = 0; i < stockDetailModelList.Count; i++)
                    {
                        this.dgv_GSDetail.Rows.Add();
                        this.dgv_GSDetail.Rows[i].Cells["gsID"].Value = stockDetailModelList[i].GoodsSiteID;
                        this.dgv_GSDetail.Rows[i].Cells["gsStoreStatus"].Value = stockDetailModelList[i].GoodsSiteStoreStatus;
                        this.dgv_GSDetail.Rows[i].Cells["gsRunStatus"].Value = stockDetailModelList[i].GoodsSiteRunStatus;
                        this.dgv_GSDetail.Rows[i].Cells["productNameStr"].Value = stockDetailModelList[i].ProductName;
                        this.dgv_GSDetail.Rows[i].Cells["productStatusStr"].Value = stockDetailModelList[i].ProductStatus;
                        this.dgv_GSDetail.Rows[i].Cells["IntoHouseTime"].Value = stockDetailModelList[i].InHouseTime;
                        this.dgv_GSDetail.Rows[i].Cells["StockIDStr"].Value = stockDetailModelList[i].StockID;
                        this.dgv_GSDetail.Rows[i].Cells["GsName"].Value = stockDetailModelList[i].GoodsSiteName;
                        this.dgv_GSDetail.Rows[i].Cells["GsTaskStatus"].Value = stockDetailModelList[i].GoodsSiteInOutType;

                        if (trayDetailModelList[i] != null)
                        {
                            this.dgv_GSDetail.Rows[i].Cells["batch"].Value = trayDetailModelList[i].Tf_BatchID;
                            this.dgv_GSDetail.Rows[i].Cells["batchType"].Value = trayDetailModelList[i].Tf_Batchtype;
                            this.dgv_GSDetail.Rows[i].Cells["cellCount"].Value = trayDetailModelList[i].Tf_CellCount;
                            this.dgv_GSDetail.Rows[i].Cells["TrayID"].Value = trayDetailModelList[i].Tf_TrayId;
                        }
                    }
                }
                else
                {
                    this.dgv_GSDetail.Rows.Clear();
                }
            }
        }

        /// <summary>
        /// 作者:np
        /// 时间:2014年6月15日
        /// 内容:设置货位状态显示功能
        /// </summary>
        /// <param name="visible"></param>
        public void SetGsStatusFunc(bool visible)
        {
            this.gb_GsStatusModify.Visible = visible;
        }

        /// <summary>
        /// 作者:np
        /// 时间:2014年6月13日
        /// 内容:设置删除功能的显隐
        /// </summary>
        /// <param name="visible"></param>
        public void SetDeleteFunc(bool visible)
        {
            this.删除ToolStripMenuItem.Visible = visible;
        }
        /// <summary>
        /// 显示信息
        /// </summary>
        /// <param name="titleStr"></param>
        /// <param name="contentStr"></param>
        public void ShowMessage(string titleStr, string contentStr)
        {
            MessageBox.Show(contentStr, titleStr, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /// <summary>
        /// 作者:np
        /// 时间:2014年5月11日
        /// 内容:初始化产品批次号下拉列表
        /// </summary>
        /// <returns></returns>
        public void SetProductBatchNumList(DataTable dt)
        {
            this.cb_OutStorageBatchNum.Items.Clear();
            if (dt == null)
            {
                this.cb_OutStorageBatchNum.Items.Add("所有");
            }
            else
            {
                dt.Rows.Add();
                dt.Rows[dt.Rows.Count - 1][0] = "所有";
                this.cb_OutStorageBatchNum.DataSource = dt;
                this.cb_OutStorageBatchNum.DisplayMember = "ProductBatchNum";
                this.cb_OutStorageBatchNum.SelectedItem = null;
            }

        }
      
        /// <summary>
        /// 刷新数据
        /// </summary>
        /// <param name="model"></param>
        public void RefreshDGVData(List<View_QueryStockListModel> modelList)
        {
            if (this.dgv_StockQuery.InvokeRequired)
            {
                RefreshDGVDataInvoke rdfvdInvoke = new RefreshDGVDataInvoke(RefreshDGVData);
                this.dgv_StockQuery.Invoke(rdfvdInvoke, new object[1] { modelList });
            }
            else
            {
                this.dgv_StockQuery.Rows.Clear();
                //this.SetProgressBarMaxValue(modelList.Count);
                for (int i = 0; i < modelList.Count; i++)
                {
                   
                    //this.SetProgressBarValue(i);
                    this.dgv_StockQuery.Rows.Add();
                    this.dgv_StockQuery.Rows[i].Cells["StockID"].Value = modelList[i].StockID;

                    this.dgv_StockQuery.Rows[i].Cells["ProductPatch"].Value = modelList[i].ProductBatchNum;
                    this.dgv_StockQuery.Rows[i].Cells["ProductCode"].Value = modelList[i].ProductCode;
                    this.dgv_StockQuery.Rows[i].Cells["ProductStatus"].Value = modelList[i].ProductStatus;
                    this.dgv_StockQuery.Rows[i].Cells["ProductNum"].Value = modelList[i].ProductNum;
                    this.dgv_StockQuery.Rows[i].Cells["ProductName"].Value = modelList[i].ProductName;
                    this.dgv_StockQuery.Rows[i].Cells["GoodsSiteName"].Value = modelList[i].GoodsSiteName;
                    this.dgv_StockQuery.Rows[i].Cells["gs_StoreStatus"].Value = modelList[i].GoodsSiteStoreStatus;
                    this.dgv_StockQuery.Rows[i].Cells["gs_RunStatus"].Value = modelList[i].GoodsSiteRunStatus;
                    this.dgv_StockQuery.Rows[i].Cells["InHouseTime"].Value = modelList[i].InHouseTime;
                    this.dgv_StockQuery.Rows[i].Cells["updateTime"].Value = modelList[i].UpdateTime;
                    this.dgv_StockQuery.Rows[i].Cells["goodsSiteID"].Value = modelList[i].GoodsSiteID;
                    this.dgv_StockQuery.Rows[i].Cells["gsTaskType"].Value = modelList[i].GoodsSiteInOutType;
                    
                    Application.DoEvents();
                    System.Threading.Thread.Sleep(0);
                }
            }
        }

        /// <summary>
        /// 弹出是否请求对话框
        /// </summary>
        /// <param name="content"></param>
        /// <returns>返回：0是，1：否</returns>
        public int AskMessBox(string content)
        {
            DialogResult result = MessageBox.Show(content, "信息提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                return 0;
            }
            else
            {
                return 1;
            }

        }

        /// <summary>
        /// 查询排列层数量
        /// </summary>
        /// <param name="rowth"></param>
        /// <param name="columnth"></param>
        /// <param name="layers"></param>
        public void QueryRCLData(List<StoreHouseProper> HouseProList)
        {
            houseProList = HouseProList;
        }

        /// <summary>
        /// 作者:np
        /// 时间:2014年8月25日
        /// 内容:设置退出按钮可用状态
        /// </summary>
        /// <param name="enabled"></param>
        public void SetExitButtonEnabled(bool enabled)
        {
            this.bt_Exit.Enabled = enabled;
        }

        #endregion

        #region 触发IStockManaView事件
        private void OnTrayDetail(string trayID)
        {
            if (this.eventTrayDetail != null)
            {
                TrayEventArgs trayArgs = new TrayEventArgs();
                trayArgs.TrayID = trayID;
                this.eventTrayDetail.Invoke(this, trayArgs);
            }
        }

        private void OnStockDetail(int goodsSiteID)
        {
            if (this.eventStockDetail != null)
            {
                QueryGoodsSiteEventArgs gsArgs = new QueryGoodsSiteEventArgs();
                gsArgs.GoodsSiteID = goodsSiteID;
                this.eventStockDetail.Invoke(this, gsArgs);
            }
        }

        private void OnDeleteStock()
        {
            if (this.eventDeleteStock != null)
            {
                if (this.dgv_StockQuery.SelectedRows.Count > 0)
                {
                    int selectRows = this.dgv_StockQuery.SelectedRows.Count;
                    long[] stockIDArr = new long[selectRows];
                    //int currentIndex = this.dgv_StockQuery.CurrentRow.Index;
                    for (int i = 0; i < selectRows; i++)
                    {
                        stockIDArr[i] = long.Parse(this.dgv_StockQuery.Rows[this.dgv_StockQuery.SelectedRows[i].Index].Cells["StockID"].Value.ToString());
                    }

                    //long stockId = long.Parse(this.dgv_StockQuery.Rows[currentIndex].Cells["StockID"].Value.ToString());
                    DeleteStockEventArgs deleteArgs = new DeleteStockEventArgs();
                    deleteArgs.StockID = stockIDArr;
                    this.eventDeleteStock.Invoke(this, deleteArgs);
                }
                else
                {
                    MessageBox.Show("请选择要删除的货位信息!", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void OnQueryStock()
        {
            bool isRight = true;
            IsControlValueRight(ref isRight);
            if (this.eventQueryStock != null && isRight == true)
            {
                QueryStockEventArgs stockArgs = new QueryStockEventArgs();
                EnumWorkFolwStatus workFlowStatus = new EnumWorkFolwStatus();
                if (this.tscb_workFlowTime.SelectedItem.ToString() == EnumWorkFolwStatus.已达到.ToString())
                {
                    workFlowStatus = EnumWorkFolwStatus.已达到;
                }
                else if (this.tscb_workFlowTime.SelectedItem.ToString() == EnumWorkFolwStatus.未到达.ToString())
                {
                    workFlowStatus = EnumWorkFolwStatus.未到达;
                }
                else if (this.tscb_workFlowTime.SelectedItem.ToString() == EnumWorkFolwStatus.所有.ToString())
                {
                    workFlowStatus = EnumWorkFolwStatus.所有;
                }
                stockArgs.Rowth = this.tscb_StockRow.Text;
                stockArgs.Columnth = this.tscb_StockColumn.Text;
                stockArgs.Layerth = this.tscb_StockLayer.Text;
                stockArgs.WorkFolwStatus = workFlowStatus;
                stockArgs.StoreHouse = this.tscb_StoreHouse.SelectedItem.ToString();
                stockArgs.ProductStatus = this.tscb_ProductStatus.SelectedItem.ToString();
                stockArgs.GsRunStatus = this.cb_QueryGSTaskStatus.SelectedItem.ToString().Trim();
                stockArgs.GsStoreStatus = this.cb_QueryGSStoreType.SelectedItem.ToString().Trim();
                stockArgs.GsTaskType = this.cb_QueryGSTaskType.SelectedItem.ToString().Trim();
                stockArgs.ProductBatchNum = this.cb_OutStorageBatchNum.Text;
                this.eventQueryStock.Invoke(this, stockArgs);
            }
        }

        private void OnOutHouseByHand()
        {

            if (this.eventOutHouseByHand != null && this.dgv_StockQuery.CurrentRow != null)
            {
                long[] stockIDArr = new long[this.dgv_StockQuery.SelectedRows.Count];
                EnumProductStatus[] enumProductStatusArr = new EnumProductStatus[stockIDArr.Length];
                EnumGSRunStatus[] gsRunStatusArr = new EnumGSRunStatus[stockIDArr.Length];
                EnumGSStoreStatus[] gsStoreStatusArr = new EnumGSStoreStatus[stockIDArr.Length];
                EnumTaskCategory[] GSTaskTypeArr = new EnumTaskCategory[stockIDArr.Length];

                for (int i = 0; i < this.dgv_StockQuery.SelectedRows.Count; i++)
                {
                    DataGridViewRow rowSelect = this.dgv_StockQuery.SelectedRows[i];// 从大到小
                    if (rowSelect != null)
                    {
                        long stockID = long.Parse(rowSelect.Cells["StockID"].Value.ToString());
                        string productStatus = rowSelect.Cells["ProductStatus"].Value.ToString();
                        string gsRunStatus = rowSelect.Cells["gs_RunStatus"].Value.ToString();
                        string gsStoreStatus = rowSelect.Cells["gs_StoreStatus"].Value.ToString();
                        string gsTaskType = rowSelect.Cells["gsTaskType"].Value.ToString();
                        if (stockID != 0 && productStatus != "")
                        {
                            stockIDArr[i] = stockID;
                            enumProductStatusArr[i] = (EnumProductStatus)Enum.Parse(typeof(EnumProductStatus), productStatus);
                            gsRunStatusArr[i] = (EnumGSRunStatus)Enum.Parse(typeof(EnumGSRunStatus), gsRunStatus);
                            gsStoreStatusArr[i] = (EnumGSStoreStatus)Enum.Parse(typeof(EnumGSStoreStatus), gsStoreStatus);
                            GSTaskTypeArr[i] = (EnumTaskCategory)Enum.Parse(typeof(EnumTaskCategory), gsTaskType);
                        }

                    }
                }

                StockEventArgs stockArgs = new StockEventArgs();
                stockArgs.ProductStatusArr = enumProductStatusArr;
                stockArgs.StockIDArr = stockIDArr;
                stockArgs.GsRunStatusArr = gsRunStatusArr;
                stockArgs.GsStoreStatusArr = gsStoreStatusArr;
                stockArgs.GSTaskTypeArr = GSTaskTypeArr;

                this.eventOutHouseByHand.Invoke(this, stockArgs);
            }
            else
            {
                MessageBox.Show("请选择要出库的货位!", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void OnLoadEvent()
        {
            if (this.eventLoad != null)
            {
                this.eventLoad.Invoke(this, null);
            }
        }

        private void OnSetGsStatus()
        {
            if (this.eventSetGsStatus != null)
            {
                StockStatusEventArgs gsArgs = new StockStatusEventArgs();
                if (this.tscb_GoodsSiteStatus.Text == "")
                {
                    MessageBox.Show("请选择货位存储状态！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else if (this.tscb_TaskRunStatus.Text == "")
                {
                    MessageBox.Show("请选择货位任务状态！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (this.dgv_StockQuery.SelectedRows.Count > 0)
                {
                    int selectCount = this.dgv_StockQuery.SelectedRows.Count;
                    //int[] goodsSiteIDArr = new int[selectCount];
                    //EnumTaskCategory[] gsTaskTypeArr = new EnumTaskCategory[selectCount];
                    //for (int i = 0; i < selectCount; i++)
                    //{
                    //    int goodsSiteID = int.Parse(this.dgv_StockQuery.SelectedRows[i].Cells["goodsSiteID"].Value.ToString());
                    //    goodsSiteIDArr[i] = goodsSiteID;
                    //    gsTaskTypeArr[i] = (EnumTaskCategory)Enum.Parse(typeof(EnumTaskCategory), this.dgv_StockQuery.SelectedRows[i].Cells["gsTaskType"].Value.ToString());
                    //}
                    int goodsSiteID = int.Parse(this.dgv_StockQuery.CurrentRow.Cells["goodsSiteID"].Value.ToString());
                    gsArgs.GsStoreStatus = (EnumGSStoreStatus)Enum.Parse(typeof(EnumGSStoreStatus), this.tscb_GoodsSiteStatus.Text.Trim());
                    gsArgs.GsRunStatus = (EnumGSRunStatus)Enum.Parse(typeof(EnumGSRunStatus), this.tscb_TaskRunStatus.Text.Trim());
                    gsArgs.GoodsSiteID = goodsSiteID;
                    gsArgs.GsTaskType = (EnumTaskCategory)Enum.Parse(typeof(EnumTaskCategory), this.dgv_StockQuery.CurrentRow.Cells["gsTaskType"].Value.ToString()); ;
                    this.eventSetGsStatus.Invoke(this, gsArgs);

                }
                else
                {
                    MessageBox.Show("请选择要设置的货位!", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
     
        #endregion

        #region 业务私有方法
        private StoreHouseProper GetHousePro(EnumStoreHouse enumHouseName)
        {
            StoreHouseProper housePro = null;
            for (int i = 0; i < houseProList.Count(); i++)
            {
                if (houseProList[i].HouseName == enumHouseName)
                {
                    housePro = houseProList[i];
                }
            }
            return housePro;
        }
        private bool IsIntNum(string numText)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex("^[0-9]+$");              //判断是不是数据，要不是就表示没有选择，则从隐藏域里读出来      
            System.Text.RegularExpressions.Match ma = reg.Match(numText);
            if (ma.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void IsControlValueRight(ref bool isRight)
        {
          
            if (this.tscb_StoreHouse.Text == "")
            {
                MessageBox.Show("请选择库房！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                isRight = false;
                return ;
            }
            if (this.tscb_StockRow.Text == "")
            {
                MessageBox.Show("请选择排数！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                isRight = false;
                return ;
            }
            if (this.tscb_StockColumn.Text == "")
            {
                MessageBox.Show("请选择列数！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                isRight = false;
                return ;
            }
            if (this.tscb_StockLayer.Text == "")
            {
                MessageBox.Show("请选择层数！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                isRight = false;
                return ;
            }
            if (this.tscb_ProductStatus.Text == "")
            {
                MessageBox.Show("请选择物料状态！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                isRight = false;
                return ;
            }
            if (this.tscb_workFlowTime.Text == "")
            {
                MessageBox.Show("请选择状态时间！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                isRight = false;
                return ;
            }
            if (this.cb_QueryGSStoreType.Text == "")
            {
                MessageBox.Show("请选择货位存储类型！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                isRight = false;
                return;
            }

            if (this.cb_QueryGSTaskStatus.Text == "")
            {
                MessageBox.Show("请选择货位任务状态！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                isRight = false;
                return;
            }

            if (this.cb_QueryGSTaskType.Text == "")
            {
                MessageBox.Show("请选择货位任务类型！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                isRight = false;
                return;
            }
            

            
        }

        private void IniControlValue()
        {
            this.tscb_StoreHouse.Items.Clear();
            this.tscb_StoreHouse.Items.Add(EnumStoreHouse.A1库房.ToString());
            this.tscb_StoreHouse.Items.Add(EnumStoreHouse.B1库房.ToString());

            this.tscb_workFlowTime.Items.Add(EnumWorkFolwStatus.未到达.ToString());
            this.tscb_workFlowTime.Items.Add(EnumWorkFolwStatus.已达到.ToString());
            this.tscb_workFlowTime.Items.Add(EnumWorkFolwStatus.所有.ToString());
            this.tscb_workFlowTime.SelectedIndex =2;

            this.cb_QueryGSStoreType.Items.Clear();
            this.cb_QueryGSStoreType.Items.Add("所有");
            this.cb_QueryGSStoreType.Items.Add(EnumGSStoreStatus.空货位.ToString());
            this.cb_QueryGSStoreType.Items.Add(EnumGSStoreStatus.空料框.ToString());
            this.cb_QueryGSStoreType.Items.Add(EnumGSStoreStatus.有货.ToString());
       
            this.cb_QueryGSTaskStatus.Items.Clear();
            this.cb_QueryGSTaskStatus.Items.Add("所有");
            this.cb_QueryGSTaskStatus.Items.Add(EnumGSRunStatus.待用.ToString());
            this.cb_QueryGSTaskStatus.Items.Add(EnumGSRunStatus.任务锁定.ToString());
            this.cb_QueryGSTaskStatus.Items.Add(EnumGSRunStatus.任务完成.ToString());
       
            this.cb_QueryGSTaskType.Items.Clear();
            this.cb_QueryGSTaskType.Items.Add("所有");
            this.cb_QueryGSTaskType.Items.Add(EnumTaskCategory.入库.ToString());
            this.cb_QueryGSTaskType.Items.Add(EnumTaskCategory.出库.ToString());

        }
        #endregion

    

    }
}
