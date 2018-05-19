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
    public partial class DataManageView : BaseView,IDataManaView
    {
        #region  全局变量
        private int editRowIndex = 0;
        #endregion

        #region 初始化
        public DataManageView()
        {
            InitializeComponent();
        }

        private void DataManageView_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < Enum.GetNames(typeof(EnumDataList)).Length; i++)
            {
                this.tscb_dataList.Items.Add(Enum.GetNames(typeof(EnumDataList))[i]);
            }
        }

        protected override object CreatePresenter()
        {
            DataManaPresenter logPres = new DataManaPresenter(this);
            for (int i = 0; i < allPresenterList.Count; i++)
            {
                if (allPresenterList[i].GetType() == typeof(DataManaPresenter))
                {
                    allPresenterList.RemoveAt(i);
                }
            }
            allPresenterList.Add(logPres);
            return logPres;
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
        private void tscb_dataList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (this.tscb_dataList.SelectedItem != null && this.tscb_dataList.SelectedItem.ToString() == "控制任务表")
            //{
            //    this.dtp_endTime.Enabled = false;
            //    this.dtp_startTime.Enabled = false;
            //}
            //else
            //{
            //    this.dtp_endTime.Enabled = true;
            //    this.dtp_startTime.Enabled = true;
            //}
        }

        private void tsb_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsb_Query_Click(object sender, EventArgs e)
        {
            OnQuery();
        }

        private void tsb_Save_Click(object sender, EventArgs e)
        {
            OnSave();
        }

        private void tsb_Delete_Click(object sender, EventArgs e)
        {
            OnDelete();
        }

        private void dgv_DataDetail_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            editRowIndex = this.dgv_DataDetail.CurrentRow.Index;
        }

        private void tsb_FormatSystem_Click(object sender, EventArgs e)
        {
            OnFormatSystem();
        }

        private void tsb_BackUp_Click(object sender, EventArgs e)
        {
            OnDatabaseBakRecover(true);
        }

        private void tsb_Recover_Click(object sender, EventArgs e)
        {
            OnDatabaseBakRecover(false);
        }

        #endregion

        #region 实现IDataManaView 事件
        public event EventHandler<DeleteEventArgs> eventDelete;
  
        public event EventHandler<DataEventArgs> eventQuery;
        public event EventHandler< SaveEventArgs> eventSave;
        /// <summary>
        /// 格式化系统将清空管理任务表、控制任务表、库存表、初始化货位表
        /// 恢复到出厂设置
        /// </summary>
        public event EventHandler eventFormatSystem;
        public  event EventHandler<DatabaseEventArgs> eventDatabaseBakRecover;
        #endregion

        #region 触发IDataManaView 事件
        private void OnDatabaseBakRecover(bool isbak)
        {
            if (this.eventDatabaseBakRecover != null)
            {
                if (isbak == true)
                {
                    SaveFileDialog saveDia = new SaveFileDialog();
                    saveDia.Filter = ".bak|*.bak";
                    if (saveDia.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        DatabaseEventArgs databaseArgs = new DatabaseEventArgs();
                        databaseArgs.Path = saveDia.FileName;
                        databaseArgs.IsBackUp = true;
                        this.eventDatabaseBakRecover.Invoke(this, databaseArgs);
                    }
                }
                else
                {
                    OpenFileDialog openDia = new OpenFileDialog();
                    openDia.Filter = ".bak|*.bak";
                    if (openDia.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        DatabaseEventArgs databaseArgs = new DatabaseEventArgs();
                        databaseArgs.Path = openDia.FileName;
                        databaseArgs.IsBackUp = false;
                        this.eventDatabaseBakRecover.Invoke(this, databaseArgs);
                    }
                }
               

            }
        }

        private void OnFormatSystem()
        {
            if (this.eventFormatSystem != null)
            {
                this.eventFormatSystem.Invoke(this, null);
            }
        }

        private void OnDelete()
        {
            if (this.eventDelete != null)
            {
                DeleteEventArgs deleteArgs = new DeleteEventArgs();
                if (this.dgv_DataDetail.SelectedRows.Count > 0)
                {
                    EnumDataList enumDataList = (EnumDataList)Enum.Parse(typeof(EnumDataList), this.tscb_dataList.SelectedItem.ToString());
                    deleteArgs.EnumDataList = enumDataList;
                    switch (enumDataList)
                    {
                        case EnumDataList.管理任务表:
                            int selectManaCount = this.dgv_DataDetail.SelectedRows.Count;
                            long[] ManaListIDArr = new long[selectManaCount];
                            for (int i = 0; i < selectManaCount; i++)
                            {
                                ManaListIDArr[i] = long.Parse(this.dgv_DataDetail.SelectedRows[i].Cells["TaskID"].Value.ToString());
                            }
                            deleteArgs.ListID = ManaListIDArr;
                            break;
                        case EnumDataList.控制接口表:
                            int selectCount = this.dgv_DataDetail.SelectedRows.Count;
                            long[] ListIDArr = new long[selectCount];
                            for (int i = 0; i < selectCount; i++)
                            {
                                ListIDArr[i] = long.Parse(this.dgv_DataDetail.SelectedRows[i].Cells["ControlInterfaceID"].Value.ToString());
                            }
                            deleteArgs.ListID = ListIDArr;
                            break;
                        case EnumDataList.控制任务表:
                            int selectControlCount = this.dgv_DataDetail.SelectedRows.Count;
                            long[] controlListIDArr = new long[selectControlCount];
                            for (int i = 0; i < selectControlCount; i++)
                            {
                                controlListIDArr[i] = long.Parse(this.dgv_DataDetail.SelectedRows[i].Cells["ControlTaskID"].Value.ToString());
                            }
                            deleteArgs.ListID = controlListIDArr;
                            break;
                        case EnumDataList.库存列表:
                            int selectStockListCount = this.dgv_DataDetail.SelectedRows.Count;
                            long[] stockListIDArr = new long[selectStockListCount];
                            for (int i = 0; i < selectStockListCount; i++)
                            {
                                stockListIDArr[i] = long.Parse(this.dgv_DataDetail.SelectedRows[i].Cells["StockListID"].Value.ToString());
                            }
                            deleteArgs.ListID = stockListIDArr;
                            break;
                        default:
                            break;
                    }
                    this.eventDelete.Invoke(this, deleteArgs);

                }
                else
                {
                    MessageBox.Show("请选择要删除的数据!", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void OnQuery()
        {
            if (this.tscb_dataList.SelectedItem != null)
            { 
                DataEventArgs dataArgs = new DataEventArgs();
                EnumDataList enumDataList = (EnumDataList)Enum.Parse(typeof(EnumDataList), this.tscb_dataList.SelectedItem.ToString());
                dataArgs.EnumDataList = enumDataList;
                switch (enumDataList)
                {
                    case EnumDataList.管理任务表:
                        dataArgs.StartTime = DateTime.Parse(this.dtp_startTime.Value.ToString("yyyy-MM-dd 0:00:00"));
                        dataArgs.EndTime =  DateTime.Parse(this.dtp_endTime.Value.ToString("yyyy-MM-dd 23:59:59"));
                        break;
                    case EnumDataList.控制接口表:
                        dataArgs.StartTime = DateTime.Parse(this.dtp_startTime.Value.ToString("yyyy-MM-dd 0:00:00"));
                        dataArgs.EndTime =  DateTime.Parse(this.dtp_endTime.Value.ToString("yyyy-MM-dd 23:59:59"));
                        break;
                    case EnumDataList.控制任务表:
                         dataArgs.StartTime = DateTime.Parse(this.dtp_startTime.Value.ToString("yyyy-MM-dd 0:00:00"));
                        dataArgs.EndTime =  DateTime.Parse(this.dtp_endTime.Value.ToString("yyyy-MM-dd 23:59:59"));
                        break;
                    case EnumDataList.库存列表:
                         dataArgs.StartTime = DateTime.Parse(this.dtp_startTime.Value.ToString("yyyy-MM-dd 0:00:00"));
                        dataArgs.EndTime =  DateTime.Parse(this.dtp_endTime.Value.ToString("yyyy-MM-dd 23:59:59"));
                        break;
                    default:
                        break;
                }
                this.eventQuery.Invoke(this, dataArgs);

            }
            else
            {
                MessageBox.Show("请选择数据表", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void OnSave()
        {
            if (this.eventSave != null)
            {
                SaveEventArgs dataArgs = new SaveEventArgs();
                if (this.tscb_dataList.SelectedItem != null)
                {
                    EnumDataList enumDataList = (EnumDataList)Enum.Parse(typeof(EnumDataList), this.tscb_dataList.SelectedItem.ToString());
                    dataArgs.EnumDataList = enumDataList;
                    switch (enumDataList)
                    {
                        case EnumDataList.管理任务表:
                            if (this.dgv_DataDetail.SelectedRows.Count > 0)
                            {
                                long listID = long.Parse(this.dgv_DataDetail.Rows[editRowIndex].Cells["TaskID"].Value.ToString());
                                ManageTaskModel manaTaskModel = new ManageTaskModel();
                                manaTaskModel.TaskID = listID;
                                manaTaskModel.TaskCreateTime = DateTime.Parse(this.dgv_DataDetail.Rows[editRowIndex].Cells["TaskCreateTime"].Value.ToString());
                                manaTaskModel.TaskCode =this.dgv_DataDetail.Rows[editRowIndex].Cells["TaskCode"].Value.ToString();
                                manaTaskModel.TaskID = long.Parse(this.dgv_DataDetail.Rows[editRowIndex].Cells["TaskID"].Value.ToString());
                                manaTaskModel.TaskCreatePerson = this.dgv_DataDetail.Rows[editRowIndex].Cells["TaskCreatePerson"].Value.ToString();
                                manaTaskModel.TaskType = this.dgv_DataDetail.Rows[editRowIndex].Cells["TaskType"].Value.ToString();
                                manaTaskModel.TaskStartArea = this.dgv_DataDetail.Rows[editRowIndex].Cells["TaskStartArea"].Value.ToString();
                                manaTaskModel.TaskStartPostion = this.dgv_DataDetail.Rows[editRowIndex].Cells["TaskStartPostion"].Value.ToString();
                                manaTaskModel.TaskEndArea = this.dgv_DataDetail.Rows[editRowIndex].Cells["TaskEndArea"].Value.ToString();
                                manaTaskModel.TaskEndPostion = this.dgv_DataDetail.Rows[editRowIndex].Cells["TaskEndPostion"].Value.ToString();
                               
                                manaTaskModel.TaskStatus = this.dgv_DataDetail.Rows[editRowIndex].Cells["TaskStatus"].Value.ToString();
                                manaTaskModel.TaskTypeName = this.dgv_DataDetail.Rows[editRowIndex].Cells["TaskTypeName"].Value.ToString();
                                dataArgs.EditModel = manaTaskModel;
                                this.eventSave.Invoke(this, dataArgs);
                            }


                            break;
                        case EnumDataList.控制接口表:
                            if (this.dgv_DataDetail.SelectedRows.Count > 0)
                            {
                                long listID = long.Parse(this.dgv_DataDetail.Rows[editRowIndex].Cells["ControlInterfaceID"].Value.ToString());
                              
                                ControlInterfaceModel ctrlInterModel = new ControlInterfaceModel();
                                ctrlInterModel.ControlInterfaceID = listID;
                                ctrlInterModel.CreateTime = DateTime.Parse(this.dgv_DataDetail.Rows[editRowIndex].Cells["CreateTime"].Value.ToString());
                                ctrlInterModel.DeviceCode = this.dgv_DataDetail.Rows[editRowIndex].Cells["DeviceCode"].Value.ToString();
                                ctrlInterModel.InterfaceParameter = this.dgv_DataDetail.Rows[editRowIndex].Cells["InterfaceParameter"].Value.ToString();
                                ctrlInterModel.InterfaceStatus = this.dgv_DataDetail.Rows[editRowIndex].Cells["InterfaceStatus"].Value.ToString();
                                ctrlInterModel.InterfaceType = this.dgv_DataDetail.Rows[editRowIndex].Cells["InterfaceType"].Value.ToString();
                                ctrlInterModel.TaskCode = this.dgv_DataDetail.Rows[editRowIndex].Cells["TaskCode"].Value.ToString();
                                dataArgs.EditModel = ctrlInterModel;
                                this.eventSave.Invoke(this, dataArgs);
                            }

                            break;
                        case EnumDataList.控制任务表:
                            if (this.dgv_DataDetail.SelectedRows.Count > 0)
                            {
                                long listID = long.Parse(this.dgv_DataDetail.Rows[editRowIndex].Cells["ControlTaskID"].Value.ToString());
                                ControlTaskModel controlTask = new ControlTaskModel();
                                controlTask.ControlTaskID = listID;
                                controlTask.ControlCode = this.dgv_DataDetail.Rows[editRowIndex].Cells["ControlCode"].Value.ToString();
                                controlTask.TaskTypeCode = int.Parse(this.dgv_DataDetail.Rows[editRowIndex].Cells["TaskTypeCode"].Value.ToString());
                                controlTask.TaskID = long.Parse(this.dgv_DataDetail.Rows[editRowIndex].Cells["TaskID"].Value.ToString());
                                controlTask.CreateMode = this.dgv_DataDetail.Rows[editRowIndex].Cells["CreateMode"].Value.ToString();
                                controlTask.TaskType = this.dgv_DataDetail.Rows[editRowIndex].Cells["TaskType"].Value.ToString();
                                controlTask.StartArea = this.dgv_DataDetail.Rows[editRowIndex].Cells["StartArea"].Value.ToString();
                                controlTask.StartDevice = this.dgv_DataDetail.Rows[editRowIndex].Cells["StartDevice"].Value.ToString();
                                controlTask.TargetArea = this.dgv_DataDetail.Rows[editRowIndex].Cells["TargetArea"].Value.ToString();
                                controlTask.TargetDevice = this.dgv_DataDetail.Rows[editRowIndex].Cells["TargetDevice"].Value.ToString();
                                controlTask.TaskID = long.Parse(this.dgv_DataDetail.Rows[editRowIndex].Cells["TaskID"].Value.ToString());
                                controlTask.TaskStatus = this.dgv_DataDetail.Rows[editRowIndex].Cells["TaskStatus"].Value.ToString();
                                controlTask.TaskTypeName = this.dgv_DataDetail.Rows[editRowIndex].Cells["TaskTypeName"].Value.ToString();
                                controlTask.CreateTime = DateTime.Parse(this.dgv_DataDetail.Rows[editRowIndex].Cells["CreateTime"].Value.ToString());
                                dataArgs.EditModel = controlTask;
                                this.eventSave.Invoke(this, dataArgs);
                            }
                            break;
                        case EnumDataList.库存列表:
                            if (this.dgv_DataDetail.SelectedRows.Count > 0)
                            {
                                long listID = long.Parse(this.dgv_DataDetail.Rows[editRowIndex].Cells["StockListID"].Value.ToString());
                                StockListModel stockListModel = new StockListModel();
                                stockListModel.StockListID = listID;
                                stockListModel.GoodsSiteName = this.dgv_DataDetail.Rows[editRowIndex].Cells["GoodsSiteName"].Value.ToString();
                                stockListModel.InHouseTime =DateTime.Parse( this.dgv_DataDetail.Rows[editRowIndex].Cells["InHouseTime"].Value.ToString());
                                stockListModel.ManaTaskID = long.Parse(this.dgv_DataDetail.Rows[editRowIndex].Cells["ManaTaskID"].Value.ToString());
                                stockListModel.ProductCode = this.dgv_DataDetail.Rows[editRowIndex].Cells["ProductCode"].Value.ToString();
                                stockListModel.ProductName = this.dgv_DataDetail.Rows[editRowIndex].Cells["ProductName"].Value.ToString();
                                stockListModel.ProductNum = int.Parse(this.dgv_DataDetail.Rows[editRowIndex].Cells["ProductNum"].Value.ToString());
                                stockListModel.ProductStatus = this.dgv_DataDetail.Rows[editRowIndex].Cells["ProductStatus"].Value.ToString();
                                stockListModel.StockID = long.Parse(this.dgv_DataDetail.Rows[editRowIndex].Cells["StockID"].Value.ToString());
                                stockListModel.StoreHouseName = this.dgv_DataDetail.Rows[editRowIndex].Cells["StoreHouseName"].Value.ToString();
                                stockListModel.UpdateTime = DateTime.Parse( this.dgv_DataDetail.Rows[editRowIndex].Cells["UpdateTime"].Value.ToString());
                                stockListModel.ProductBatchNum = this.dgv_DataDetail.Rows[editRowIndex].Cells["ProductBatchNum"].Value.ToString();
                                stockListModel.ProductFrameCode = this.dgv_DataDetail.Rows[editRowIndex].Cells["ProductFrameCode"].Value.ToString();
                                dataArgs.EditModel = stockListModel;
                                this.eventSave.Invoke(this, dataArgs);
                            }
                            break;
                        default:
                            break;
                    }
                }
             
            }
        }
        #endregion

        #region 实现IDataManaView 方法
        public void RefreshDataList(DataTable dt, EnumDataList enumDataList)
        {
            IniAndValueDataGrid(enumDataList,dt);
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
        #endregion

        #region UI私有方法
        private void IniAndValueDataGrid(EnumDataList enumDataList, DataTable dt)
        {
            this.dgv_DataDetail.Columns.Clear();
            this.dgv_DataDetail.Rows.Clear();
            switch (enumDataList)
            {
                case EnumDataList.管理任务表:
                    #region 管理任务表格初始化
                     DataGridViewTextBoxColumn tb_manaTaskID = new DataGridViewTextBoxColumn();
                     tb_manaTaskID.HeaderText = "管理任务ID";
                     tb_manaTaskID.Name = "TaskID";
                     tb_manaTaskID.ReadOnly = true;
                     this.dgv_DataDetail.Columns.Add(tb_manaTaskID);

                     DataGridViewTextBoxColumn tb_TaskCreatePerson = new DataGridViewTextBoxColumn();
                     tb_TaskCreatePerson.HeaderText = "创建人";
                     tb_TaskCreatePerson.Name = "TaskCreatePerson";
                     tb_TaskCreatePerson.ReadOnly = true;
                     this.dgv_DataDetail.Columns.Add(tb_TaskCreatePerson);

                     DataGridViewTextBoxColumn dtp_CreateTime  = new DataGridViewTextBoxColumn();
                     dtp_CreateTime.HeaderText = "创建时间";
                     dtp_CreateTime.Name = "TaskCreateTime";
                     this.dgv_DataDetail.Columns.Add(dtp_CreateTime);

                 
                    DataGridViewComboBoxColumn cb_manaTaskTypeName = new DataGridViewComboBoxColumn();
                    cb_manaTaskTypeName.HeaderText = "任务流程名称";
                    cb_manaTaskTypeName.Name = "TaskTypeName";
                    for (int i = 0; i < Enum.GetNames(typeof(EnumTaskName)).Count(); i++)
                    {
                        cb_manaTaskTypeName.Items.Add(Enum.GetNames(typeof(EnumTaskName))[i]);
                    }
                    this.dgv_DataDetail.Columns.Add(cb_manaTaskTypeName);

                    DataGridViewComboBoxColumn cb_manaTaskType = new DataGridViewComboBoxColumn();
                    cb_manaTaskType.HeaderText = "任务类型";
                    cb_manaTaskType.Name = "taskType";
                    cb_manaTaskType.Items.Add(EnumTaskCategory.出库.ToString());
                    cb_manaTaskType.Items.Add(EnumTaskCategory.入库.ToString());
                    this.dgv_DataDetail.Columns.Add(cb_manaTaskType);

                       DataGridViewTextBoxColumn tb_ManaTaskCode= new DataGridViewTextBoxColumn();
                       tb_ManaTaskCode.HeaderText = "任务编码";
                       tb_ManaTaskCode.Name = "TaskCode";
                    tb_ManaTaskCode.ReadOnly = true;
                    this.dgv_DataDetail.Columns.Add(tb_ManaTaskCode);
                    

                    DataGridViewTextBoxColumn tb_ManaStartArea = new DataGridViewTextBoxColumn();
                    tb_ManaStartArea.HeaderText = "开始区域";
                    tb_ManaStartArea.Name = "TaskStartArea";
                    this.dgv_DataDetail.Columns.Add(tb_ManaStartArea);

                    DataGridViewTextBoxColumn tb_manaStartDevice = new DataGridViewTextBoxColumn();
                    tb_manaStartDevice.HeaderText = "开始设备";
                    tb_manaStartDevice.Name = "TaskStartPostion";
                    this.dgv_DataDetail.Columns.Add(tb_manaStartDevice);

                    DataGridViewTextBoxColumn tb_ManaTargetArea = new DataGridViewTextBoxColumn();
                    tb_ManaTargetArea.HeaderText = "目标区域";
                    tb_ManaTargetArea.Name = "TaskEndArea";
                    this.dgv_DataDetail.Columns.Add(tb_ManaTargetArea);

                    DataGridViewTextBoxColumn tb_ManaTargetDevice = new DataGridViewTextBoxColumn();
                    tb_ManaTargetDevice.HeaderText = "目标设备";
                    tb_ManaTargetDevice.Name = "TaskEndPostion";
                    this.dgv_DataDetail.Columns.Add(tb_ManaTargetDevice);

                    DataGridViewComboBoxColumn cb_ManaTaskStatus = new DataGridViewComboBoxColumn();
                    cb_ManaTaskStatus.HeaderText = "任务状态";
                    cb_ManaTaskStatus.Name = "TaskStatus";
                    cb_ManaTaskStatus.Items.Add(EnumTaskStatus.错误.ToString());
                    cb_ManaTaskStatus.Items.Add(EnumTaskStatus.待执行.ToString());
                    cb_ManaTaskStatus.Items.Add(EnumTaskStatus.已完成.ToString());
                    cb_ManaTaskStatus.Items.Add(EnumTaskStatus.执行中.ToString());
                    this.dgv_DataDetail.Columns.Add(cb_ManaTaskStatus);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        this.dgv_DataDetail.Rows.Add();
                        this.dgv_DataDetail.Rows[i].Cells["TaskID"].Value = dt.Rows[i]["TaskID"].ToString();
                        this.dgv_DataDetail.Rows[i].Cells["TaskTypeName"].Value = dt.Rows[i]["TaskTypeName"].ToString();

                        this.dgv_DataDetail.Rows[i].Cells["taskCode"].Value = dt.Rows[i]["taskCode"].ToString();
 
                       
                        this.dgv_DataDetail.Rows[i].Cells["TaskStartArea"].Value = dt.Rows[i]["TaskStartArea"].ToString();
                        this.dgv_DataDetail.Rows[i].Cells["TaskStartPostion"].Value = dt.Rows[i]["TaskStartPostion"].ToString();
                        this.dgv_DataDetail.Rows[i].Cells["TaskEndArea"].Value = dt.Rows[i]["TaskEndArea"].ToString();
                        this.dgv_DataDetail.Rows[i].Cells["TaskEndPostion"].Value = dt.Rows[i]["TaskEndPostion"].ToString();
                        this.dgv_DataDetail.Rows[i].Cells["TaskStatus"].Value = dt.Rows[i]["TaskStatus"].ToString();
                        this.dgv_DataDetail.Rows[i].Cells["taskType"].Value = dt.Rows[i]["taskType"].ToString();
                        this.dgv_DataDetail.Rows[i].Cells["TaskCreatePerson"].Value = dt.Rows[i]["TaskCreatePerson"].ToString();
                        this.dgv_DataDetail.Rows[i].Cells["TaskCreateTime"].Value = dt.Rows[i]["TaskCreateTime"].ToString();
                    }
                    #endregion
                    break;
                case EnumDataList.控制接口表:
                    #region 控制接口
                    DataGridViewTextBoxColumn dgvcCtrlInterID = new DataGridViewTextBoxColumn();
                    dgvcCtrlInterID.HeaderText = "控制接口ID";
                    dgvcCtrlInterID.Name = "ControlInterfaceID";
                    this.dgv_DataDetail.Columns.Add(dgvcCtrlInterID);

                    DataGridViewTextBoxColumn dgvcCtrlInterType = new DataGridViewTextBoxColumn();
                    dgvcCtrlInterType.HeaderText = "接口类型";
                    dgvcCtrlInterType.Name = "InterfaceType";
                    this.dgv_DataDetail.Columns.Add(dgvcCtrlInterType);

                    DataGridViewTextBoxColumn dgvcCtrlInterDeviceID = new DataGridViewTextBoxColumn();
                    dgvcCtrlInterDeviceID.HeaderText = "设备ID";
                    dgvcCtrlInterDeviceID.Name = "DeviceCode";
                    this.dgv_DataDetail.Columns.Add(dgvcCtrlInterDeviceID);

                    DataGridViewTextBoxColumn dgvcCtrlInterTaskCode = new DataGridViewTextBoxColumn();
                    dgvcCtrlInterTaskCode.HeaderText = "控制条码";
                    dgvcCtrlInterTaskCode.Name = "TaskCode";
                    this.dgv_DataDetail.Columns.Add(dgvcCtrlInterTaskCode);

                    DataGridViewComboBoxColumn dgvcCtrlInterStatus = new DataGridViewComboBoxColumn();
                    dgvcCtrlInterStatus.HeaderText = "接口状态";
                    dgvcCtrlInterStatus.Name = "InterfaceStatus";
                    dgvcCtrlInterStatus.Items.Add(EnumCtrlInterStatus.未生成.ToString());
                    dgvcCtrlInterStatus.Items.Add(EnumCtrlInterStatus.已生成.ToString());
                    this.dgv_DataDetail.Columns.Add(dgvcCtrlInterStatus);

                  DataGridViewTextBoxColumn dgvcCtrlInterCreateTime= new DataGridViewTextBoxColumn();
                    dgvcCtrlInterCreateTime.HeaderText = "创建时间";
                    dgvcCtrlInterCreateTime.Name = "CreateTime";
                    dgvcCtrlInterCreateTime.ReadOnly = true;
                    this.dgv_DataDetail.Columns.Add(dgvcCtrlInterCreateTime);

                    DataGridViewTextBoxColumn dgvcCtrlInterPara = new DataGridViewTextBoxColumn();
                    dgvcCtrlInterPara.HeaderText = "接口参数";
                    dgvcCtrlInterPara.Name = "InterfaceParameter";
                    this.dgv_DataDetail.Columns.Add(dgvcCtrlInterPara);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        this.dgv_DataDetail.Rows.Add();
                        this.dgv_DataDetail.Rows[i].Cells["ControlInterfaceID"].Value = dt.Rows[i]["ControlInterfaceID"].ToString();
                        this.dgv_DataDetail.Rows[i].Cells["InterfaceType"].Value = dt.Rows[i]["InterfaceType"].ToString();
                        this.dgv_DataDetail.Rows[i].Cells["DeviceCode"].Value = dt.Rows[i]["DeviceCode"].ToString();
                        this.dgv_DataDetail.Rows[i].Cells["TaskCode"].Value = dt.Rows[i]["TaskCode"].ToString();
                        this.dgv_DataDetail.Rows[i].Cells["InterfaceStatus"].Value = dt.Rows[i]["InterfaceStatus"].ToString();
                        this.dgv_DataDetail.Rows[i].Cells["CreateTime"].Value = dt.Rows[i]["CreateTime"].ToString();
                        this.dgv_DataDetail.Rows[i].Cells["InterfaceParameter"].Value = dt.Rows[i]["InterfaceParameter"].ToString();
                    }
                    #endregion
                    break;
                case EnumDataList.控制任务表:
                    #region 控制任务表格初始化
                    DataGridViewTextBoxColumn tb_controlTaskID = new DataGridViewTextBoxColumn();
                    tb_controlTaskID.HeaderText = "控制任务ID";
                    tb_controlTaskID.Name = "ControlTaskID";
                    tb_controlTaskID.ReadOnly = true;
                    this.dgv_DataDetail.Columns.Add(tb_controlTaskID);

                     DataGridViewTextBoxColumn tb_TaskID = new DataGridViewTextBoxColumn();
                     tb_TaskID.HeaderText = "管理任务ID";
                     tb_TaskID.Name = "TaskID";
                     tb_TaskID.ReadOnly = true;
                     this.dgv_DataDetail.Columns.Add(tb_TaskID);

                     DataGridViewTextBoxColumn tb_TaskTypeCode = new DataGridViewTextBoxColumn();
                     tb_TaskTypeCode.HeaderText = "任务流程ID";
                     tb_TaskTypeCode.Name = "TaskTypeCode";
                     tb_TaskTypeCode.ReadOnly = true;
                     this.dgv_DataDetail.Columns.Add(tb_TaskTypeCode);
                    
                    DataGridViewComboBoxColumn cb_TaskTypeName = new DataGridViewComboBoxColumn();
                    cb_TaskTypeName.HeaderText = "任务流程名称";
                    cb_TaskTypeName.Name = "TaskTypeName";
                    for (int i = 0; i < Enum.GetNames(typeof(EnumTaskName)).Count(); i++)
                    {
                        cb_TaskTypeName.Items.Add(Enum.GetNames(typeof(EnumTaskName))[i]);
                    }
                    this.dgv_DataDetail.Columns.Add(cb_TaskTypeName);

                    DataGridViewComboBoxColumn cb_taskType = new DataGridViewComboBoxColumn();
                    cb_taskType.HeaderText = "任务类型";
                    cb_taskType.Name = "taskType";
                    cb_taskType.Items.Add(EnumTaskCategory.出库.ToString());
                    cb_taskType.Items.Add(EnumTaskCategory.入库.ToString());
                    this.dgv_DataDetail.Columns.Add(cb_taskType);

                    DataGridViewTextBoxColumn tb_ControlCode = new DataGridViewTextBoxColumn();
                    tb_ControlCode.HeaderText = "控制条码";
                    tb_ControlCode.Name = "ControlCode";
                    this.dgv_DataDetail.Columns.Add(tb_ControlCode);

                    DataGridViewTextBoxColumn tb_StartArea = new DataGridViewTextBoxColumn();
                    tb_StartArea.HeaderText = "开始区域";
                    tb_StartArea.Name = "StartArea";
                    this.dgv_DataDetail.Columns.Add(tb_StartArea);

                    DataGridViewTextBoxColumn tb_StartDevice = new DataGridViewTextBoxColumn();
                    tb_StartDevice.HeaderText = "开始设备";
                    tb_StartDevice.Name = "StartDevice";
                    this.dgv_DataDetail.Columns.Add(tb_StartDevice);

                    DataGridViewTextBoxColumn tb_TargetArea = new DataGridViewTextBoxColumn();
                    tb_TargetArea.HeaderText = "目标区域";
                    tb_TargetArea.Name = "TargetArea";
                    this.dgv_DataDetail.Columns.Add(tb_TargetArea);

                    DataGridViewTextBoxColumn tb_TargetDevice = new DataGridViewTextBoxColumn();
                    tb_TargetDevice.HeaderText = "目标设备";
                    tb_TargetDevice.Name = "TargetDevice";
                    this.dgv_DataDetail.Columns.Add(tb_TargetDevice);

                    DataGridViewComboBoxColumn cb_TaskStatus = new DataGridViewComboBoxColumn();
                    cb_TaskStatus.HeaderText = "任务状态";
                    cb_TaskStatus.Name = "TaskStatus";
                    cb_TaskStatus.Items.Add(EnumTaskStatus.错误.ToString());
                    cb_TaskStatus.Items.Add(EnumTaskStatus.待执行.ToString());
                    cb_TaskStatus.Items.Add(EnumTaskStatus.已完成.ToString());
                    cb_TaskStatus.Items.Add(EnumTaskStatus.执行中.ToString());
                    cb_TaskStatus.Items.Add(EnumTaskStatus.超时.ToString());
                    this.dgv_DataDetail.Columns.Add(cb_TaskStatus);

                    DataGridViewComboBoxColumn cb_CreateMode = new DataGridViewComboBoxColumn();
                    cb_CreateMode.HeaderText = "任务创建类型";
                    cb_CreateMode.Name = "CreateMode";
                    cb_CreateMode.Items.Add(EnumCreateMode.手动生成.ToString());
                    cb_CreateMode.Items.Add(EnumCreateMode.系统生成.ToString());
                    cb_CreateMode.Items.Add(EnumCreateMode.手动强制.ToString());
                    this.dgv_DataDetail.Columns.Add(cb_CreateMode);

                    DataGridViewTextBoxColumn cb_CreateTime = new DataGridViewTextBoxColumn();
                   cb_CreateTime.HeaderText = "任务创建时间";
                   cb_CreateTime.Name = "CreateTime";
                   cb_CreateTime.ReadOnly = true;
                   this.dgv_DataDetail.Columns.Add(cb_CreateTime);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        this.dgv_DataDetail.Rows.Add();
                        this.dgv_DataDetail.Rows[i].Cells["ControlTaskID"].Value = dt.Rows[i]["ControlTaskID"].ToString();
                        this.dgv_DataDetail.Rows[i].Cells["TaskTypeName"].Value = dt.Rows[i]["TaskTypeName"].ToString();
                        this.dgv_DataDetail.Rows[i].Cells["TaskTypeCode"].Value = dt.Rows[i]["TaskTypeCode"].ToString();
                        this.dgv_DataDetail.Rows[i].Cells["taskType"].Value = dt.Rows[i]["taskType"].ToString();
                        this.dgv_DataDetail.Rows[i].Cells["taskID"].Value = dt.Rows[i]["taskID"].ToString();
                        this.dgv_DataDetail.Rows[i].Cells["ControlCode"].Value = dt.Rows[i]["ControlCode"].ToString();
                        this.dgv_DataDetail.Rows[i].Cells["StartArea"].Value = dt.Rows[i]["StartArea"].ToString();
                        this.dgv_DataDetail.Rows[i].Cells["StartDevice"].Value = dt.Rows[i]["StartDevice"].ToString();
                        this.dgv_DataDetail.Rows[i].Cells["TargetArea"].Value = dt.Rows[i]["TargetArea"].ToString();
                        this.dgv_DataDetail.Rows[i].Cells["TargetDevice"].Value = dt.Rows[i]["TargetDevice"].ToString();
                        this.dgv_DataDetail.Rows[i].Cells["TaskStatus"].Value = dt.Rows[i]["TaskStatus"].ToString();
                        this.dgv_DataDetail.Rows[i].Cells["taskType"].Value = dt.Rows[i]["taskType"].ToString();
                        this.dgv_DataDetail.Rows[i].Cells["CreateMode"].Value = dt.Rows[i]["CreateMode"].ToString();
                        this.dgv_DataDetail.Rows[i].Cells["CreateTime"].Value = dt.Rows[i]["CreateTime"].ToString();
                    }
                    #endregion
                    break;
                case EnumDataList.库存列表:
                    #region 库存列表表格初始化
                    DataGridViewTextBoxColumn tb_StockListID = new DataGridViewTextBoxColumn();
                    tb_StockListID.HeaderText = "库存列表ID";
                    tb_StockListID.Name = "StockListID";
                    tb_StockListID.ReadOnly = true;
                    this.dgv_DataDetail.Columns.Add(tb_StockListID);

                    DataGridViewTextBoxColumn tb_ManaTaskID = new DataGridViewTextBoxColumn();
                    tb_ManaTaskID.HeaderText = "管理任务ID";
                    tb_ManaTaskID.Name = "ManaTaskID";
                    tb_ManaTaskID.ReadOnly = true;
                    this.dgv_DataDetail.Columns.Add(tb_ManaTaskID);

                    DataGridViewComboBoxColumn cb_StoreHouseName = new DataGridViewComboBoxColumn();
                    cb_StoreHouseName.HeaderText = "库房名称";
                    cb_StoreHouseName.Name = "StoreHouseName";
                    cb_StoreHouseName.Items.Add(EnumStoreHouse.A1库房.ToString());
                    cb_StoreHouseName.Items.Add(EnumStoreHouse.B1库房.ToString());
                    this.dgv_DataDetail.Columns.Add(cb_StoreHouseName);

                    DataGridViewTextBoxColumn tb_StockID = new DataGridViewTextBoxColumn();
                    tb_StockID.HeaderText = "库存ID";
                    tb_StockID.Name = "StockID";
                    tb_StockID.ReadOnly = true;
                    this.dgv_DataDetail.Columns.Add(tb_StockID);

                    DataGridViewTextBoxColumn tb_ProductID = new DataGridViewTextBoxColumn();
                    tb_ProductID.HeaderText = "产品ID";
                    tb_ProductID.Name = "ProductCode";
                    tb_ProductID.ReadOnly = true;
                    this.dgv_DataDetail.Columns.Add(tb_ProductID);

                    DataGridViewTextBoxColumn tb_ProductNum = new DataGridViewTextBoxColumn();
                    tb_ProductNum.HeaderText = "产品数量";
                    tb_ProductNum.Name = "ProductNum";
                    this.dgv_DataDetail.Columns.Add(tb_ProductNum);

                    DataGridViewComboBoxColumn cb_ProductStatus = new DataGridViewComboBoxColumn();
                    cb_ProductStatus.HeaderText = "产品状态";
                    cb_ProductStatus.Name = "ProductStatus";
                    for (int i = 0; i < Enum.GetNames(typeof(EnumProductStatus)).Count(); i++)
                    {
                        cb_ProductStatus.Items.Add(Enum.GetNames(typeof(EnumProductStatus))[i]);

                    }
                    this.dgv_DataDetail.Columns.Add(cb_ProductStatus);

                     DataGridViewTextBoxColumn tb_ProductFrameCode = new DataGridViewTextBoxColumn();
                     tb_ProductFrameCode.HeaderText = "产品跟踪码";
                     tb_ProductFrameCode.Name = "ProductFrameCode";
                     this.dgv_DataDetail.Columns.Add(tb_ProductFrameCode);

                    DataGridViewTextBoxColumn tb_ProductName = new DataGridViewTextBoxColumn();
                    tb_ProductName.HeaderText = "产品名称";
                    tb_ProductName.Name = "ProductName";
                    this.dgv_DataDetail.Columns.Add(tb_ProductName);

                    DataGridViewTextBoxColumn tb_GoodsSiteName = new DataGridViewTextBoxColumn();
                    tb_GoodsSiteName.HeaderText = "货位名称";
                    tb_GoodsSiteName.Name = "GoodsSiteName";
                    this.dgv_DataDetail.Columns.Add(tb_GoodsSiteName);

                    DataGridViewTextBoxColumn tb_ProductBatchNum = new DataGridViewTextBoxColumn();
                    tb_ProductBatchNum.HeaderText = "产品批次号";
                    tb_ProductBatchNum.Name = "ProductBatchNum";
                    this.dgv_DataDetail.Columns.Add(tb_ProductBatchNum);

                    DataGridViewTextBoxColumn tb_InHouseTime = new DataGridViewTextBoxColumn();
                    tb_InHouseTime.HeaderText = "入库时间";
                    tb_InHouseTime.Name = "InHouseTime";
                    this.dgv_DataDetail.Columns.Add(tb_InHouseTime);

                    DataGridViewTextBoxColumn tb_UpdateTime = new DataGridViewTextBoxColumn();
                    tb_UpdateTime.HeaderText = "更新时间";
                    tb_UpdateTime.Name = "UpdateTime";
                    this.dgv_DataDetail.Columns.Add(tb_UpdateTime);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        this.dgv_DataDetail.Rows.Add();
                        this.dgv_DataDetail.Rows[i].Cells["StockListID"].Value = dt.Rows[i]["StockListID"].ToString();
                        this.dgv_DataDetail.Rows[i].Cells["ManaTaskID"].Value = dt.Rows[i]["ManaTaskID"].ToString();
                        this.dgv_DataDetail.Rows[i].Cells["StoreHouseName"].Value = dt.Rows[i]["StoreHouseName"].ToString();
                        this.dgv_DataDetail.Rows[i].Cells["StockID"].Value = dt.Rows[i]["StockID"].ToString();
                        this.dgv_DataDetail.Rows[i].Cells["ProductCode"].Value = dt.Rows[i]["ProductCode"].ToString();
                        this.dgv_DataDetail.Rows[i].Cells["ProductNum"].Value = dt.Rows[i]["ProductNum"].ToString();
                        this.dgv_DataDetail.Rows[i].Cells["GoodsSiteName"].Value = dt.Rows[i]["GoodsSiteName"].ToString();
                        this.dgv_DataDetail.Rows[i].Cells["ProductStatus"].Value = dt.Rows[i]["ProductStatus"].ToString();
                        this.dgv_DataDetail.Rows[i].Cells["ProductName"].Value = dt.Rows[i]["ProductName"].ToString();
                        this.dgv_DataDetail.Rows[i].Cells["InHouseTime"].Value = dt.Rows[i]["InHouseTime"].ToString();
                        this.dgv_DataDetail.Rows[i].Cells["UpdateTime"].Value = dt.Rows[i]["UpdateTime"].ToString();
                        this.dgv_DataDetail.Rows[i].Cells["ProductFrameCode"].Value = dt.Rows[i]["ProductFrameCode"].ToString();
                        this.dgv_DataDetail.Rows[i].Cells["ProductBatchNum"].Value = dt.Rows[i]["ProductBatchNum"].ToString();
                        
                    }
                    #endregion
                    break;
                default:
                    break;
            }
        }
        #endregion
 
    }
}
