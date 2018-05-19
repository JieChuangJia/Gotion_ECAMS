using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ECAMSUserControl;
using ECAMSModel;
using ECAMSDataAccess;
using ECAMSPresenter;

namespace ECAMS
{
    public partial class StorageView : BaseView,IStorageView
    {
        #region 全局变量
        private List<StoreHouseProper> houseProList = new List<StoreHouseProper>();
     
        #endregion 

        #region 初始化
        public StorageView()
        {
            InitializeComponent();
        
        }

        private void StorageView_Load(object sender, EventArgs e)
        {
            this.tscb_StoreHouseName.Items.Add(EnumStoreHouse.A1库房.ToString());
            this.tscb_StoreHouseName.Items.Add(EnumStoreHouse.B1库房.ToString());

            OnLoadEvent();
        }

        protected override object CreatePresenter()
        {
            StoragePresenter managerPre = new StoragePresenter(this);
            for (int i = 0; i < allPresenterList.Count; i++)
            {
                if (allPresenterList[i].GetType() == typeof(StoragePresenter))
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
        private void 删除料框ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (this.dgv_GoodsSiteDetail.SelectedRows.Count > 0)
            {
              
                if (this.dgv_GoodsSiteDetail.CurrentRow.Cells["TrayID"].Value == null)
                {
                    ShowMessage("错误","TrayID 为空");
                    return;
                }
                string trayId = this.dgv_GoodsSiteDetail.CurrentRow.Cells["TrayID"].Value.ToString();
                if (this.dgv_GoodsSiteDetail.CurrentRow.Cells["GsName"].Value == null)
                {
                    ShowMessage("错误", "GsName为空");
                    return;
                }
                string gsName = this.dgv_GoodsSiteDetail.CurrentRow.Cells["GsName"].Value.ToString();
                OnDeleteTray(trayId, gsName);
            }

        }

        private void 添加料框ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            AddTrayDetailView atdView = new AddTrayDetailView();
            atdView.ShowDialog();
            if (atdView.isSet == true)
            {
                OnAddTray(atdView.trayCode);
            }

        }

        private void cb_goodsSiteStatus_SelectedIndexChanged(object sender, EventArgs e)
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

        private void bt_GoodsSiteExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 仓位点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void storageControl1_PositionsClick(object sender, ECAMSUserControl.StorageControl.ClickPositionsEventArgs e)
        {
            this.storageControl1.selectPositions = e.Positions;
            if (this.storageControl1.selectPositions != null && this.storageControl1.selectPositions.Visible == true)
            {
                OnQueryGoodsSite(e.Positions.GoodsSiteID);
            }
        }

        private void tsb_UnuseGs_Click(object sender, EventArgs e)
        {
            if (this.storageControl1.selectPositions != null && this.storageControl1.selectPositions.Visible == true)
            {
                OnUnuseGs(this.storageControl1.selectPositions.GoodsSiteID);
                QueryStorage();//状态修改后刷新界面
            }
            else
            {
                MessageBox.Show("请选中要禁止的货位", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void tsb_UsrGs_Click(object sender, EventArgs e)
        {
            if (this.storageControl1.selectPositions != null && this.storageControl1.selectPositions.Visible == true)
            {
                OnUseGs(this.storageControl1.selectPositions.GoodsSiteID);
                QueryStorage();//状态修改后刷新界面
            }
            else
            {
                MessageBox.Show("请选中要启用的货位", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 作者:np
        /// 时间:2014年4月17日
        /// 内容:出库点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemClickEventHandler(object sender, EventArgs e)
        {
            OnOutHouseByHand(sender.ToString());
        }

        private void cb_StoreHouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.tscb_Rowth.Items.Clear();
            if (this.tscb_StoreHouseName.Text == EnumStoreHouse.A1库房.ToString())
            {
                this.tscb_GoodsSiteStatus.Items.Clear();
                this.tscb_GoodsSiteStatus.Items.Add(EnumGSStoreStatus.空货位.ToString());
                this.tscb_GoodsSiteStatus.Items.Add(EnumGSStoreStatus.有货.ToString());
                #region 初始化右键菜单
                this.cms_Storage.Items.Clear();
                ToolStripMenuItem tsmi_FRCK = new ToolStripMenuItem();
                tsmi_FRCK.Text = EnumTaskName.分容出库_A1.ToString();

                ToolStripMenuItem tsmi_DXCK = new ToolStripMenuItem();
                tsmi_DXCK.Text = EnumTaskName.电芯出库_A1.ToString();

                tsmi_FRCK.Click += MenuItemClickEventHandler;
                tsmi_DXCK.Click += MenuItemClickEventHandler;

                this.cms_Storage.Items.Add(tsmi_FRCK);
                this.cms_Storage.Items.Add(tsmi_DXCK);
                #endregion
            }
            else
            {
                this.cms_Storage.Items.Clear();
                ToolStripMenuItem tsmi_KLKCK = new ToolStripMenuItem();
                tsmi_KLKCK.Text = EnumTaskName.空料框出库.ToString();
                ToolStripMenuItem tsmi_JCCK = new ToolStripMenuItem();
                tsmi_JCCK.Text = EnumTaskName.电芯出库_B1.ToString();

                tsmi_KLKCK.Click += MenuItemClickEventHandler;
                tsmi_JCCK.Click += MenuItemClickEventHandler;

                this.cms_Storage.Items.Add(tsmi_KLKCK);
                this.cms_Storage.Items.Add(tsmi_JCCK);

                this.tscb_GoodsSiteStatus.Items.Clear();
                this.tscb_GoodsSiteStatus.Items.Add(EnumGSStoreStatus.空货位.ToString());
                this.tscb_GoodsSiteStatus.Items.Add(EnumGSStoreStatus.空料框.ToString());
                this.tscb_GoodsSiteStatus.Items.Add(EnumGSStoreStatus.有货.ToString());
            }
            StoreHouseProper housePro = GetHousePro((EnumStoreHouse)Enum.Parse(typeof(EnumStoreHouse), this.tscb_StoreHouseName.Text));
            for (int i = 0; i < housePro.Rows; i++)
            {
                this.tscb_Rowth.Items.Add(i + 1);
            }
            if (housePro.Rows > 0)
            {
                this.tscb_Rowth.SelectedIndex = 0;
            }

            this.tscb_Columnth.Text = "1";
            this.tscb_Columnth.Items.Clear();
            for (int i = 0; i < housePro.Columns; i++)
            {
                this.tscb_Columnth.Items.Add(i + 1);
            }
            this.tscb_Layerth.Text = "1";
            this.tscb_Layerth.Items.Clear();
            for (int i = 0; i < housePro.Layers; i++)
            {
                this.tscb_Layerth.Items.Add(i + 1);
            }
        }

        private void cb_Rowth_SelectedIndexChanged(object sender, EventArgs e)
        {
            QueryStorage();
        }

        private void bt_refreshStatus_Click(object sender, EventArgs e)
        {
            QueryStorage();
        }

        private void bt_SetGsStatusByHand_Click(object sender, EventArgs e)
        {
            OnSetGsStatus();
            QueryStorage();//状态修改后刷新界面
        }


        private void 刷新ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QueryStorage();
        }
      
        private void tscb_Columnth_TextChanged(object sender, EventArgs e)
        {
            if (IsIntNum(this.tscb_Columnth.Text) == false)
            {
                this.tscb_Columnth.Text = "1";
                MessageBox.Show("您输入列数不合法！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            { 
               StoreHouseProper housePro =  GetHousePro((EnumStoreHouse)Enum.Parse(typeof(EnumStoreHouse),this.tscb_StoreHouseName.Text));
               if (int.Parse(this.tscb_Columnth.Text) <= 0 || int.Parse(this.tscb_Columnth.Text) > housePro.Columns)
               {
                   this.tscb_Columnth.Text = "1";
                   MessageBox.Show("您输入列数超出范围！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
               }
            }
        }

        private void tscb_Layerth_TextChanged(object sender, EventArgs e)
        {
            if (IsIntNum(this.tscb_Layerth.Text) == false)
            {
                this.tscb_Layerth.Text = "1";
                MessageBox.Show("您输入层数不合法！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
            else
            {
                StoreHouseProper housePro = GetHousePro((EnumStoreHouse)Enum.Parse(typeof(EnumStoreHouse), this.tscb_StoreHouseName.Text));
                if (int.Parse(this.tscb_Layerth.Text) <= 0 || int.Parse(this.tscb_Layerth.Text) > housePro.Layers)
                {
                    this.tscb_Layerth.Text = "1";
                    MessageBox.Show("您输入层数超出范围！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void tsb_Query_Click(object sender, EventArgs e)
        {
            bool isSendSuccess = OnQueryGsByRcl();

            if (isSendSuccess == true)
            {
                TrayLocate();
            }
        }

        private void tsb_TrayCodeSearch_Click(object sender, EventArgs e)
        {
            OnSearchTray();
        }
        #endregion

        #region 实现IStorageView事件
        public event EventHandler<GsStatusEventArgs> eventSetGsStatus;
        public event EventHandler<StorageEventArgs> eventLoadStorageData;
        public event EventHandler<QueryGoodsSiteEventArgs> eventQueryGoodsSite;
        public event EventHandler<OutHouseByHandEventArgs> eventOutHouseByHand;
        public event EventHandler eventLoad;
        public event EventHandler<QueryGsByRCLEventArgs> eventQueryGsByRcl;
        /// <summary>
        /// 作者:np
        /// 时间:2014年5月31日
        /// 内容:禁用货位
        /// </summary>
        public event EventHandler<QueryGoodsSiteEventArgs> eventUnuseGs;
        /// <summary>
        /// 作者:np
        /// 时间:2014年5月31日
        /// 内容:启用货位
        /// </summary>
        public event EventHandler<QueryGoodsSiteEventArgs> eventUseGs;

        public event EventHandler<TrayEventArgs> eventDeleteTray;
        public event EventHandler<TrayEventArgs> eventAddTray;
        /// <summary>
        /// 作者:np
        /// 时间:2014年8月20日
        /// 内容:通过托盘号查找托盘位置
        /// </summary>
        public event EventHandler<SearchTrayEventArgs> eventSearchTray;
        #endregion

        #region 实现IStorageView方法
        /// <summary>
        /// 作者:np
        /// 时间:2014年8月20日
        /// 内容:通过托盘ID定位托盘
        /// </summary>
        /// <param name="storeHouseName"></param>
        /// <param name="rowth"></param>
        /// <param name="columnth"></param>
        /// <param name="layerth"></param>
        public void SearchTray(string storeHouseName, string rowth, string columnth, string layerth)
        {
            this.tscb_StoreHouseName.Text = storeHouseName;
            this.tscb_Rowth.Text = rowth;
            this.tscb_Columnth.Text = columnth;
            this.tscb_Layerth.Text = layerth;
            bool isSendSuccess = OnQueryGsByRcl();

            if (isSendSuccess == true)
            {
                TrayLocate();
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
            if (visible)
            {
                this.tlp_GoodsSite.RowStyles[0].Height = 50;
            }
            else
            {
                this.tlp_GoodsSite.RowStyles[0].Height = 0;
            }
        }
        /// <summary>
        /// 作者:np
        /// 时间:2014年6月13日
        /// 内容:设置货位启用禁用功能显隐
        /// </summary>
        /// <param name="visible"></param>
        public void SetGsUseOrUnuseFunc(bool visible)
        {
            this.tsb_UnuseGs.Visible = visible;
            this.tsb_UsrGs.Visible = visible;
        }
        /// <summary>
        /// 查询排列层数量
        /// </summary>
        /// <param name="rowth"></param>
        /// <param name="columnth"></param>
        /// <param name="layers"></param>
        public  void QueryRCLData(List<StoreHouseProper> HouseProList)
        {
            houseProList = HouseProList;
        }
        public void RefreshStorage(DataTable dt)
        {
            this.storageControl1.selectPositions = null;
            this.storageControl1.DataSource = dt;
        }

        public void RefreshGSDetail(string[] valueArr)
        {
            this.dgv_GoodsSiteDetail.Rows.Clear();
            this.dgv_GoodsSiteDetail.Rows.Add();
            if (valueArr.Count() == 9)
            {
                this.dgv_GoodsSiteDetail.Rows[0].Cells["goodsSiteID"].Value = valueArr[0];
                this.dgv_GoodsSiteDetail.Rows[0].Cells["gsStoreStatus"].Value = valueArr[1];
                this.dgv_GoodsSiteDetail.Rows[0].Cells["gsRunStatus"].Value = valueArr[2];
                this.dgv_GoodsSiteDetail.Rows[0].Cells["productName"].Value = valueArr[3];
                this.dgv_GoodsSiteDetail.Rows[0].Cells["productStatus"].Value = valueArr[4];
                this.dgv_GoodsSiteDetail.Rows[0].Cells["inHouseTime"].Value = valueArr[5];
                this.dgv_GoodsSiteDetail.Rows[0].Cells["stockID"].Value = valueArr[6];
                this.dgv_GoodsSiteDetail.Rows[0].Cells["GsName"].Value = valueArr[7];
                this.dgv_GoodsSiteDetail.Rows[0].Cells["GsTaskStatus"].Value = valueArr[8];
            }
        }

        /// <summary>
        /// 更新货位有货、空料框、待用数量
        /// </summary>
        /// <param name="nullFrameNum">空料框数量</param>
        /// <param name="productNum">有货数量</param>
        /// <param name="nullNum">空货位数量</param>
        public void RefreshGSStatsuNum(int nullFrameNum, int productNum, int nullNum,int taskLockNum,int forbitNum)
        {
            this.lb_NullFrameNum.Text = nullFrameNum.ToString();
            this.lb_PrductNum.Text = productNum.ToString();
            this.lb_NullGsNum.Text = nullNum.ToString();
            this.lb_TaskLock.Text = taskLockNum.ToString();
            this.lb_ForbitNum.Text = forbitNum.ToString();
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
        /// 时间:2014年6月1日
        /// 内容:显示货位详细
        /// </summary>
        /// <param name="stockDetailModelList"></param>
        /// <param name="trayDetailModelList"></param>
        public void ShowStockDetail(List<View_StockListDetailModel> stockDetailModelList, List<TB_Tray_indexModel> trayDetailModelList)
        {
            this.dgv_GoodsSiteDetail.Rows.Clear();
           
            if (stockDetailModelList != null)
            {
                for (int i = 0; i < stockDetailModelList.Count; i++)
                {
                    this.dgv_GoodsSiteDetail.Rows.Add();
                    this.dgv_GoodsSiteDetail.Rows[i].Cells["goodsSiteID"].Value = stockDetailModelList[i].GoodsSiteID;
                    this.dgv_GoodsSiteDetail.Rows[i].Cells["gsStoreStatus"].Value = stockDetailModelList[i].GoodsSiteStoreStatus;
                    this.dgv_GoodsSiteDetail.Rows[i].Cells["gsRunStatus"].Value = stockDetailModelList[i].GoodsSiteRunStatus;
                    this.dgv_GoodsSiteDetail.Rows[i].Cells["productName"].Value = stockDetailModelList[i].ProductName;
                    this.dgv_GoodsSiteDetail.Rows[i].Cells["productStatus"].Value = stockDetailModelList[i].ProductStatus;
                    this.dgv_GoodsSiteDetail.Rows[i].Cells["inHouseTime"].Value = stockDetailModelList[i].InHouseTime;
                    this.dgv_GoodsSiteDetail.Rows[i].Cells["stockID"].Value = stockDetailModelList[i].StockID;
                    this.dgv_GoodsSiteDetail.Rows[i].Cells["GsName"].Value = stockDetailModelList[i].GoodsSiteName;
                    this.dgv_GoodsSiteDetail.Rows[i].Cells["GsTaskStatus"].Value = stockDetailModelList[i].GoodsSiteInOutType;

                    if (trayDetailModelList[i] != null)
                    {
                        this.dgv_GoodsSiteDetail.Rows[i].Cells["batch"].Value = trayDetailModelList[i].Tf_BatchID;
                        this.dgv_GoodsSiteDetail.Rows[i].Cells["batchType"].Value = trayDetailModelList[i].Tf_Batchtype;
                        this.dgv_GoodsSiteDetail.Rows[i].Cells["cellCount"].Value = trayDetailModelList[i].Tf_CellCount;
                        this.dgv_GoodsSiteDetail.Rows[i].Cells["TrayID"].Value = trayDetailModelList[i].Tf_TrayId;
                    }
                }
            }
        }

        /// <summary>
        /// 刷新界面
        /// </summary>
       public void RefreshGoodsSite()
       {
           QueryStorage();
       }
        #endregion

        #region 触发IStorageView事件
        private void OnSearchTray()
        {
            if (this.eventSearchTray != null)
            {
                if (this.tb_trayCode.Text == "")
                {
                    MessageBox.Show("请输入托盘号码！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                SearchTrayEventArgs steArgs = new SearchTrayEventArgs();
                steArgs.TrayCode = this.tb_trayCode.Text.Trim();
                this.eventSearchTray.Invoke(this, steArgs);
            }
        }
        private void OnDeleteTray(string trayId,string gsName)
        {
            if (this.eventDeleteTray != null)
            {
                if (this.storageControl1.selectPositions == null)
                {
                    ShowMessage("信息提示","请选中要删除的货位！");
                    return;
                }
                TrayEventArgs trayArgs = new TrayEventArgs();
                trayArgs.TrayID = trayId;
                trayArgs.GoodsSiteName = gsName;
                trayArgs.GsRunStatus = this.storageControl1.selectPositions.RunStatus;
                this.eventDeleteTray.Invoke(this, trayArgs);
            }
        }

        private void OnAddTray(string trayId)
        {
            if (this.eventDeleteTray != null)
            {
                if (this.storageControl1.selectPositions == null)
                {
                    ShowMessage("信息提示", "请选中要添加的货位！");
                    return;
                }
                TrayEventArgs trayArgs = new TrayEventArgs();
                trayArgs.TrayID = trayId;
                trayArgs.GsRunStatus = this.storageControl1.selectPositions.RunStatus;
                this.eventAddTray.Invoke(this, trayArgs);
            }
        }

        private void OnUnuseGs(int gsID)
        {
            if (this.eventUnuseGs != null)
            {
                QueryGoodsSiteEventArgs qgsArgs = new QueryGoodsSiteEventArgs();
                qgsArgs.GoodsSiteID = gsID;
                this.eventUnuseGs.Invoke(this, qgsArgs);
            }
        }
        private void OnUseGs(int gsID)
        {
            if (this.eventUnuseGs != null)
            {
                QueryGoodsSiteEventArgs qgsArgs = new QueryGoodsSiteEventArgs();
                qgsArgs.GoodsSiteID = gsID;
                this.eventUseGs.Invoke(this, qgsArgs);
            }
        }
        private bool OnQueryGsByRcl()
        {
            bool sendSuccess = false;
            if (this.eventQueryGsByRcl != null)
            {
                if (this.tscb_Rowth.Text == "")
                {
                    MessageBox.Show("库存排不能为空！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                if (this.tscb_Columnth.Text == "")
                {
                    MessageBox.Show("库存列不能为空！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                if (this.tscb_Layerth.Text == "")
                {
                    MessageBox.Show("库存层不能为空！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }

                QueryGsByRCLEventArgs queryByRclArgs = new QueryGsByRCLEventArgs();
                queryByRclArgs.storeHouse = (EnumStoreHouse)Enum.Parse(typeof(EnumStoreHouse), this.tscb_StoreHouseName.Text.Trim());
                queryByRclArgs.Rowth = int.Parse(this.tscb_Rowth.Text);
                queryByRclArgs.Columnth = int.Parse(this.tscb_Columnth.Text);
                queryByRclArgs.Layerth = int.Parse(this.tscb_Layerth.Text);
                this.eventQueryGsByRcl.Invoke(this, queryByRclArgs);
                sendSuccess = true;
                return sendSuccess;
            }
            else
            {
                return false;
            }
        }

        private void OnLoadEvent()
        {
            if (this.eventLoad != null)
            {
                this.eventLoad.Invoke(this, null);
            }
        }
        

        private void OnOutHouseByHand(string outStorageType)
        {
            if (this.eventOutHouseByHand != null)
            {
                OutHouseByHandEventArgs gsArgs = new OutHouseByHandEventArgs();
                if (this.storageControl1.selectPositions == null || this.storageControl1.selectPositions.Visible == false)
                {
                    MessageBox.Show("新选择要出库的货位！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                gsArgs.GoodsSiteID = this.storageControl1.selectPositions.GoodsSiteID;
                gsArgs.GsStoreStatus = (EnumGSStoreStatus)Enum.Parse(typeof(EnumGSStoreStatus), this.storageControl1.selectPositions.StoreStatus);
                gsArgs.GsRunStatus = (EnumGSRunStatus)Enum.Parse(typeof(EnumGSRunStatus), this.storageControl1.selectPositions.RunStatus);
                gsArgs.OutHouseType = outStorageType;
              
                this.eventOutHouseByHand.Invoke(this, gsArgs);
            }
            QueryStorage();//出库后刷新状态
        }

        private void OnQueryGoodsSite(int goodsSiteID)
        {
            if (this.eventQueryGoodsSite != null)
            {
                QueryGoodsSiteEventArgs gsArgs = new QueryGoodsSiteEventArgs();
                gsArgs.GoodsSiteID = goodsSiteID;
                this.eventQueryGoodsSite.Invoke(this, gsArgs);
            }
        }

        private void OnSetGsStatus()
        {
            if (this.eventSetGsStatus != null)
            {
                GsStatusEventArgs gsStatusArgs = new GsStatusEventArgs();
                if (this.tscb_TaskRunStatus.Text == "" || this.tscb_GoodsSiteStatus.Text == "")
                {
                    MessageBox.Show("请选择货位状态及任务完成状态！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (this.dgv_GoodsSiteDetail.SelectedRows.Count > 0)
                {
                    gsStatusArgs.GsRunStatus = (EnumGSRunStatus)Enum.Parse(typeof(EnumGSRunStatus), this.tscb_TaskRunStatus.Text.Trim());
                    gsStatusArgs.GsStoreStatus = (EnumGSStoreStatus)Enum.Parse(typeof(EnumGSStoreStatus), this.tscb_GoodsSiteStatus.Text.Trim());
                    gsStatusArgs.GsTaskType = (EnumTaskCategory)Enum.Parse(typeof(EnumTaskCategory), this.dgv_GoodsSiteDetail.SelectedRows[0].Cells["GsTaskStatus"].Value.ToString());
                    if (this.storageControl1.selectPositions != null)
                    {
                        gsStatusArgs.GoodsSiteID = this.storageControl1.selectPositions.GoodsSiteID;
                        this.eventSetGsStatus.Invoke(this, gsStatusArgs);
                    }
                    else
                    {
                        MessageBox.Show("请任务锁定一个货位！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("您当前选择的货位没有料框信息！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void OnLoadStorageData(EnumStoreHouse enumStoreHouse,int rowth)
        {
            if (this.eventLoadStorageData != null)
            {
                StorageEventArgs storeArgs = new StorageEventArgs();
                storeArgs.StoreHouse = enumStoreHouse;
                storeArgs.Rowth = rowth;
                this.eventLoadStorageData.Invoke(this, storeArgs);
            }
        }
        #endregion

        #region 界面私有方法
        private void TrayLocate()
        {
            int columnth = int.Parse(tscb_Columnth.Text.Trim()) - 1;
            int layerth = int.Parse(this.tscb_Layerth.Text.Trim()) - 1;
            Positions pos = this.storageControl1.GetPositionsByRcl(columnth, layerth);
            if (pos != null)
            {
                this.storageControl1.selectPositions = pos;

                if (pos.Location.X + pos.Width > this.storageControl1.Width - 10)//垂直滚动条宽度10
                {
                    int hScrollValue = pos.Location.X + pos.Width - this.storageControl1.Width + 50;
                    if (hScrollValue > this.storageControl1.HorizontalScroll.Maximum)
                    {
                        hScrollValue = this.storageControl1.HorizontalScroll.Maximum;
                    }
                    this.storageControl1.HorizontalScroll.Value = hScrollValue;
                    this.storageControl1.HorizontalScroll.Value = hScrollValue;
                }
                else
                {
                    this.storageControl1.HorizontalScroll.Value = 0;
                    this.storageControl1.HorizontalScroll.Value = 0;
                }
                if (pos.Location.Y + pos.Height > this.storageControl1.Height - 10)//横向滚动条宽度10
                {
                    int vScrollValue = pos.Location.Y - this.storageControl1.Height + 50;
                    if (vScrollValue > this.storageControl1.VerticalScroll.Maximum)
                    {
                        vScrollValue = this.storageControl1.VerticalScroll.Maximum;
                    }
                    this.storageControl1.VerticalScroll.Value = vScrollValue;//设置两次才好使
                    this.storageControl1.VerticalScroll.Value = vScrollValue;
                }
                else
                {
                    this.storageControl1.VerticalScroll.Value = 0;
                    this.storageControl1.VerticalScroll.Value = 0;
                }
                this.storageControl1.Invalidate();
                this.storageControl1.Invalidate();
            }
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

        private void QueryStorage()
        {
          
            if (this.tscb_StoreHouseName.SelectedItem != null
                && this.tscb_StoreHouseName.SelectedItem.ToString() == EnumStoreHouse.A1库房.ToString())
            {
                StoreHouseProper housePro = GetHousePro(EnumStoreHouse.A1库房);
                if (housePro == null)
                {
                    return;
                }
             
                if (this.tscb_Rowth.SelectedItem != null)
                {
                    this.storageControl1.Columns = housePro.Columns;
                    this.storageControl1.Layers = housePro.Layers;
                    this.storageControl1.QueryRowth = int.Parse(this.tscb_Rowth.Text);
                    OnLoadStorageData(EnumStoreHouse.A1库房, int.Parse(this.tscb_Rowth.Text.Trim()));
                }
            }
            else if (this.tscb_StoreHouseName.SelectedItem != null
                && this.tscb_StoreHouseName.SelectedItem.ToString() == EnumStoreHouse.B1库房.ToString())
            {
                StoreHouseProper housePro = GetHousePro(EnumStoreHouse.B1库房);
             
                if (housePro == null)
                {
                    return;
                }

                if (this.tscb_Rowth.SelectedItem != null)
                {
                    this.storageControl1.Columns = housePro.Columns;
                    this.storageControl1.Layers = housePro.Layers;
                    this.storageControl1.QueryRowth = int.Parse(this.tscb_Rowth.Text);

                    this.storageControl1.Columns = housePro.Columns;
                    this.storageControl1.Layers = housePro.Layers;
                    this.storageControl1.QueryRowth = int.Parse(this.tscb_Rowth.Text);
                    OnLoadStorageData(EnumStoreHouse.B1库房, int.Parse(this.tscb_Rowth.Text.Trim()));
                }
            }
           
        }

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

        #endregion

     
    }
}
