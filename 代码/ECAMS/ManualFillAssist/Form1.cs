using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using ECAMSDataAccess;
namespace ManualFillAssist
{
    public partial class Form1 : Form
    {
        #region 数据
        private string version = "版本：v1.1.0 2018-08-08";
        private readonly TB_Batch_IndexBll bllBatchIndex = new TB_Batch_IndexBll();
        private readonly TB_Tray_indexBll bllTrayIndex = new TB_Tray_indexBll();
        private readonly LogBll bllLog = new LogBll();
        private PalletHistoryRecordBll palletTraceBll = new PalletHistoryRecordBll();
        private OCVPalletBll palletBll = new OCVPalletBll();
        private TB_Tray_indexBll gxTrayBll = new TB_Tray_indexBll();
        private TB_Batch_IndexBll gxBatchBll = new TB_Batch_IndexBll();
        private OCVPalletBll ocvPalletBll = new OCVPalletBll();
        private OCVBatteryBll ocvBatteryBll = new OCVBatteryBll();
        private TB_After_GradeDataBll gxBatteryBll = new TB_After_GradeDataBll();
        #endregion
       
        public Form1()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.label4.Text = version;
            this.comboBoxDBset_BL.Items.AddRange(new string[]{"初入库","二次分容入库"});
            this.comboBoxDBset_BL.SelectedIndex = 0;

            string jxDB = ConfigurationManager.AppSettings["JXDataBase"];
            PubConstant.ConnectionString = jxDB;
            PubConstant.ConnectionString2 = ConfigurationManager.AppSettings["GXDataBase"];

            ToolTip ttpSettings = new ToolTip();
            ttpSettings.InitialDelay = 200;
            ttpSettings.AutoPopDelay = 10 * 1000;
            ttpSettings.ReshowDelay = 200;
            ttpSettings.ShowAlways = true;
            ttpSettings.IsBalloon = true;
            ttpSettings.SetToolTip(this.textBoxTrayID_BL, "请输入完整的托盘条码TP+7位数字");
            Console.SetOut(new TextBoxWriter(this.richTextBox1));
            
        }

        private void buttonExistCheck_Click(object sender, EventArgs e)
        {
            try
            {
                string dbSet = this.comboBoxDBset_BL.Text;
                if (string.IsNullOrWhiteSpace(dbSet))
                {
                    MessageBox.Show("未设定入库类型");
                    return;
                }
                if (string.IsNullOrWhiteSpace(this.textBoxTrayID_BL.Text))
                {
                    MessageBox.Show("托盘id为空,请重新输入");
                    return;
                }
                string palletID = this.textBoxTrayID_BL.Text.ToUpper().Trim();
                if (dbSet == "初入库")
                {
                    if (palletBll.Exists(palletID))
                    {
                        this.labelWarn.Text = "该托盘装载信息已经存在";
                    }
                    else
                    {
                        this.labelWarn.Text = "该托盘装载信息不存在，可以补录";
                    }
                }
                else if (dbSet == "二次分容入库")
                {
                    if (gxTrayBll.Exists(palletID))
                    {
                        this.labelWarn.Text = "该托盘装载信息已经存在";
                    }
                    else
                    {
                        this.labelWarn.Text = "该托盘装载信息不存在，可以补录";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
           
        }

        private void buttonGetFillInfo_BL_Click(object sender, EventArgs e)
        {
            try
            {
                GetFillInfo(this.textBoxTrayID_BL.Text);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("异常：" + ex.Message);
            }
        }
        private void GetFillInfo(string palletID)
        {
            string dbSet = this.comboBoxDBset_BL.Text;
            if (string.IsNullOrWhiteSpace(dbSet))
            {
                MessageBox.Show("未设定入库类型");
                return;
            }
            if (dbSet == "初入库")
            {
                OCVPalletModel palletModel = ocvPalletBll.GetModel(palletID);
                if (palletModel == null)
                {

                    MessageBox.Show("提示：本地信息库里未找到该托盘号信息");
                    return;
                }
                this.labelWarn.Text = "";
                this.comboBoxBatch_BL.Text = palletModel.batchID;
                this.textBoxCurrentBatch_BL.Text = palletModel.batchID;
                this.dataGridViewBatterys_BL.Rows.Clear();
                for (int i = 0; i < 4; i++)
                {
                    //if (i <3)
                    {
                        this.dataGridViewBatterys_BL.Rows.Add();
                    }
                    this.dataGridViewBatterys_BL.RowCount = this.dataGridViewBatterys_BL.Rows.Count;
                    DataGridViewRow dr = this.dataGridViewBatterys_BL.Rows[i];

                    //DataRow dr = dt.NewRow();
                    for (int j = 0; j < 12; j++)
                    {
                        dr.Cells[j].Value = "";

                    }
                }
                string strWhere = "palletID = '" + palletID + "'";
                // List<TB_After_GradeDataModel> batteryList = gxBatteryBll.GetModelList(strWhere);
                List<OCVBatteryModel> batteryList = ocvBatteryBll.GetModelList(strWhere);

                for (int i = 0; i < batteryList.Count(); i++)
                {
                    //TB_After_GradeDataModel battery = batteryList[i];
                    OCVBatteryModel battery = batteryList[i];
                    if (battery == null)
                    {
                        continue;
                    }
                    int rowIndex = (int)battery.rowIndex - 1;// (int)(battery.Tf_ChannelNo - 1) / 12;
                    int cowIndex = (int)battery.columnIndex - 1;// (int)battery.Tf_ChannelNo - rowIndex * 12 - 1;
                    DataGridViewRow dr = this.dataGridViewBatterys_BL.Rows[rowIndex];
                    dr.Cells[cowIndex].Value = battery.batteryID;// battery.Tf_CellSn;

                }
            }
            else if (dbSet == "二次分容入库")
            {
                TB_Tray_indexModel trayModel = gxTrayBll.GetModel(palletID);
                if (trayModel == null)
                {
                    MessageBox.Show("提示：国轩托盘信息库里未找到该托盘号信息");
                    return;
                }
                this.labelWarn.Text = "";
                this.comboBoxBatch_BL.Text = trayModel.Tf_BatchID;
                this.textBoxCurrentBatch_BL.Text = trayModel.Tf_BatchID;
                if (this.dataGridViewBatterys_BL.Rows.Count > 0)
                {
                    this.dataGridViewBatterys_BL.Rows.Clear();
                }

                for (int i = 0; i < 4; i++)
                {
                    if (i < 3)
                    {
                        this.dataGridViewBatterys_BL.Rows.Add();
                    }
                    this.dataGridViewBatterys_BL.RowCount = this.dataGridViewBatterys_BL.Rows.Count;
                    DataGridViewRow dr = this.dataGridViewBatterys_BL.Rows[i];

                    //DataRow dr = dt.NewRow();
                    for (int j = 0; j < 12; j++)
                    {
                        dr.Cells[j].Value = "";

                    }
                }

                string strWhere = " Tf_TrayId = '" + palletID + "' and Tf_BatchID='" + trayModel.Tf_BatchID + "' ";
                List<TB_After_GradeDataModel> batteryList = gxBatteryBll.GetModelList(strWhere);
                for (int i = 0; i < batteryList.Count(); i++)
                {
                    TB_After_GradeDataModel battery = batteryList[i];
                    if (battery == null)
                    {
                        continue;
                    }
                    int rowIndex = (int)(battery.Tf_ChannelNo - 1) / 12;
                    int cowIndex = (int)battery.Tf_ChannelNo - rowIndex * 12 - 1;
                    DataGridViewRow dr = this.dataGridViewBatterys_BL.Rows[rowIndex];
                    dr.Cells[cowIndex].Value = battery.Tf_CellSn;

                }
            }
            else
            {
                MessageBox.Show("入库类型设置错误");
                return;
            }


            this.dataGridViewBatterys_BL.BeginEdit(false);
            this.dataGridViewBatterys_BL.CurrentCell = null;

        }
        private void buttonTrayUninstall_Click(object sender, EventArgs e)
        {
            try
            {
                string palletID = this.textBoxTrayID_BL.Text.ToUpper().Trim();
                if (string.IsNullOrWhiteSpace(palletID))
                {
                    Console.WriteLine("托盘号为空!");
                    return;
                }
                if (DialogResult.No == PopAskBox("提示", "确实要解绑该托盘：" + palletID + "吗？"))
                {
                    return;
                }
                string reStr = "";
                TrayUninstall(palletID, ref reStr);

                //MessageBox.Show(reStr);
                Console.WriteLine(reStr);
                this.dataGridViewBatterys_BL.Rows.Clear();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
           
        }
        /// <summary>
        /// 托盘解绑
        /// </summary>
        /// <param name="trayID"></param>
        private bool TrayUninstall(string trayID, ref string reStr)
        {
            reStr = "解绑托盘" + trayID + "成功";
            //服务器注销托盘
            string strWhere = string.Format("Tf_TrayId='{0}' and tf_traystat=1", trayID);
            List<TB_Tray_indexModel> trayList = gxTrayBll.GetModelList(strWhere);
            if (trayList != null && trayList.Count > 0)
            {
                TB_Tray_indexModel tray = trayList[0];
                if (tray != null)
                {
                    tray.tf_traystat = 0;
                    if (!gxTrayBll.Update(tray))
                    {
                        reStr = "解绑" + trayID + "失败，更新杭可托盘信息表失败";
                        return false;
                    }
                }
            }
            //删除本地
            ocvPalletBll.Delete(trayID);
            return true;
        }
        private void buttonClear_BL_Click(object sender, EventArgs e)
        {

            this.dataGridViewBatterys_BL.AllowUserToAddRows = true;
            this.dataGridViewBatterys_BL.Rows.Clear();
            this.labelWarn.Text = "提示：";
        }

        private void buttonAddFillinfo_BL_Click(object sender, EventArgs e)
        {
            try
            {
                ExeAddFillInfo();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
           
        }
        private void ExeAddFillInfo()
        {
            if (string.IsNullOrWhiteSpace(this.textBoxTrayID_BL.Text))
            {
                MessageBox.Show("托盘id为空,请重新输入");
                return;
            }
            if (this.textBoxTrayID_BL.Text.Length != 9 || this.textBoxTrayID_BL.Text.Substring(0, 2) != "TP")
            {
                MessageBox.Show("托盘条码输入错误，检查是否以TP开始，后面7位数字");
                return;
            }
            int a = 0;
            if (!int.TryParse(this.textBoxTrayID_BL.Text.Substring(2, 7), out a))
            {
                MessageBox.Show("托盘条码输入错误，检查是否以TP开始，后面7位数字");
                return;
            }
            string palletID = this.textBoxTrayID_BL.Text.ToUpper().Trim();

            //string batchID = this.comboBoxBatch_BL.Text;
            //if (string.IsNullOrWhiteSpace(batchID))
            //{
            //    MessageBox.Show("请选择批次信息");
            //    return;
            //}
            string dbSet = this.comboBoxDBset_BL.Text;
            if (string.IsNullOrWhiteSpace(dbSet))
            {
                MessageBox.Show("未设定入库类型");
                return;
            }
            try
            {
                List<string> batterList = new List<string>();
                string[] batteryIDS = new string[48];
                int index = 0;
                for (int i = 0; i < Math.Min(4, this.dataGridViewBatterys_BL.RowCount); i++)
                {
                    DataGridViewRow rw = this.dataGridViewBatterys_BL.Rows[i];
                    if (rw == null)
                    {
                        index += 12;
                        continue;
                    }
                    for (int j = 0; j < 12; j++)
                    {
                        if (rw.Cells[j].Value == null)
                        {
                            batteryIDS[index] = string.Empty;
                        }
                        else
                        {
                            batteryIDS[index] = rw.Cells[j].Value.ToString().Trim();
                            //由12位条码改为13位，modify by zwx,2015-07-22
                            if (batteryIDS[index].Length > 24)
                            {
                                batteryIDS[index] = batteryIDS[index].Substring(0, 24);
                            }
                        }
                        index++;
                    }
                }
                string batchID = "";

                IList<string> errCells = null;
                //1 重码检查
                string reStr="";
                if(BarcodeRepetition(batteryIDS,ref errCells,ref reStr))
                {
                    labelWarn.Text = reStr;
                    Console.WriteLine(reStr);
                   
                    foreach(string strCell in errCells)
                    {
                        string[] strArray = strCell.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                        if(strArray.Count()<2)
                        {
                            continue;
                        }
                        int row = int.Parse(strArray[0])-1;
                        int col = int.Parse(strArray[1])-1;
                        this.dataGridViewBatterys_BL.Rows[row].Cells[col].Style.BackColor = Color.Red;
                    }
                    return;
                }
                //2 混批检查
                if(0 !=BatchParse(batteryIDS,ref errCells,ref batchID,ref reStr))
                {
                    labelWarn.Text = reStr;
                    Console.WriteLine("混批检查失败,{0}", reStr);
                    foreach (string strCell in errCells)
                    {
                        string[] strArray = strCell.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                        if (strArray.Count() < 2)
                        {
                            continue;
                        }
                        int row = int.Parse(strArray[0])-1;
                        int col = int.Parse(strArray[1])-1;
                        this.dataGridViewBatterys_BL.Rows[row].Cells[col].Style.BackColor = Color.Red;
                    }

                    return;
                }
                //3 托盘解绑
                if(!TrayUninstall(palletID, ref reStr))
                {
                    Console.WriteLine("解绑托盘{0}失败,{1}", palletID, reStr);
                    return;
                }
                //4 装载

                if (dbSet == "初入库")
                {
                    //if (gxTrayBll.Exists(palletID))
                    //{
                    //    MessageBox.Show("该托盘装载信息已经存在");
                    //    return;
                    //}
                    if (RecordFillInfoToLocal(palletID, batchID, batteryIDS, ref reStr))
                    {
                        this.textBoxCurrentBatch_BL.Text = batchID;
                        MessageBox.Show("装载信息成功补录!");
                        this.dataGridViewBatterys_BL.Rows.Clear();
                        this.comboBoxDBset_BL.Text = "";
                       // palletTraceBll.AddHistoryEvent(palletID, EnumOCVProcessStatus.A1库老化.ToString(), "手动装载，等待首次入A1库", MainPresenter.userNameStr);
                    }
                    else
                    {
                        MessageBox.Show("装载信息成功失败," + reStr, "录入错误");
                    }
                }
                else if (dbSet == "二次分容入库")
                {
                    //先解绑
                  //  
                    RecordFillInfoToLocal(palletID, batchID, batteryIDS, ref reStr);
                    bool batchNumEnable = false;
                    if (this.checkBoxBatchNumEnable_BL.Checked)
                    {
                        batchNumEnable = true;
                    }
                    else
                    {
                        batchNumEnable = false;
                    }
                    if (UploadFillInfoToGX(palletID, batchID, batteryIDS, batchNumEnable, ref reStr))
                    {
                        this.textBoxCurrentBatch_BL.Text = batchID;
                        MessageBox.Show("装载信息成功补录!");
                        this.dataGridViewBatterys_BL.Rows.Clear();
                        this.comboBoxDBset_BL.Text = "";
                       // palletTraceBll.AddHistoryEvent(palletID, EnumOCVProcessStatus.A1库老化.ToString(), "手动装载，等待二次分容入A1库", MainPresenter.userNameStr);
                    }
                    else
                    {
                        MessageBox.Show("装载信息成功失败," + reStr, "录入错误");
                    }
                }
                else
                {
                    MessageBox.Show("入库类型设置错误");
                    return;
                }

            }
            catch (System.Exception ex)
            {
                MessageBox.Show("补录装载信息出现异常：" + ex.Message + "," + ex.StackTrace);
            }

        }
        /// <summary>
        /// 记录到本地
        /// </summary>
        /// <returns></returns>
        private bool RecordFillInfoToLocal(string palletID, string batchID, string[] batteryIDs, ref string reStr)
        {
            //  1、	查询批次信息表(TB_Batch_Index)得到批次编号和批次类型
            // 2、	插入托盘信息到托盘信息表(TB_Tray_index)中
            // 3、	更新批次信息表(TB_Batch_Index)中托盘数量和电池总数量
            // 4、	插入电池信息到电池信息表(TB_After_GradeData)
            reStr = "";

            try
            {
                TB_Batch_IndexModel batchModel = gxBatchBll.GetModel(batchID);
                if (batchModel == null)
                {
                    reStr = "批次：" + batchID + "不存在";
                    return false;
                }


                //1 检查托盘信息
                if (ocvPalletBll.Exists(palletID))
                {
                    reStr = "托盘：" + palletID + "已经存在";
                    return false;
                }

                //2 坚持查电芯数据是否合格
                for (int i = 0; i < batteryIDs.Count() - 1; i++)
                {
                    int row1 = i / 12 + 1;
                    int col1 = i - (row1 - 1) * 12 + 1;
                    if (string.IsNullOrWhiteSpace(batteryIDs[i]))
                    {
                        continue;
                    }
                    if (batteryIDs[i].Length < 12) //电芯条码升级为13位，,2015-07-23
                    {
                        reStr = "输入电芯条码错误：第" + (i + 1).ToString() + "个电芯条码不足12位";
                        return false;
                    }
                    for (int j = i + 1; j < batteryIDs.Count(); j++)
                    {
                        int row2 = j / 12 + 1;
                        int col2 = j - (row2 - 1) * 12 + 1;
                        if (batteryIDs[i] == batteryIDs[j])
                        {

                            reStr = "输入电芯条码有重复：第[" + row1.ToString() + "行," + col1.ToString() + "列]和[" + row2.ToString() + "行," + col2.ToString() + "列]电芯条码有重复";
                            return false;
                        }
                    }
                }
             
                OCVPalletModel palletModel = new OCVPalletModel();
                palletModel.palletID = palletID;
                palletModel.batchID = batchID;
                palletModel.loadInTime = System.DateTime.Now;
                palletModel.processStatus = "A1库老化";
                ocvPalletBll.Add(palletModel);

                for (int i = 0; i < batteryIDs.Count(); i++)
                {
                    if (string.IsNullOrWhiteSpace(batteryIDs[i]) || batteryIDs[i].Length < 12) //电芯条码升级为13位，modify by zwx,2015-07-23
                    {
                        continue;
                    }
                   

                    OCVBatteryModel battery = new OCVBatteryModel();
                    battery.batteryID = batteryIDs[i];
                    battery.checkResult = "良品"; //初始值假设为合格品
                    battery.rowIndex = i / 12 + 1;
                    battery.columnIndex = i - (battery.rowIndex - 1) * 12 + 1;
                    battery.hasBattery = true;
                    battery.palletID = palletID;
                    battery.positionCode = (i + 1);
                    ocvBatteryBll.Add(battery);

                }
                
                return true;
            }
            catch (System.Exception ex)
            {
                ocvPalletBll.Delete(palletID);
                
                MessageBox.Show("录入数据库出现异常，可能有重复的电芯条码，请检查," + ex.Message);
                return false;
            }


        }
        /// <summary>
        /// 上传电芯数据到客户的数据库系统
        /// </summary>
        /// <param name="palletID"></param>
        /// <param name="batchID"></param>
        /// <param name="batteryIDs"></param>
        /// <param name="reStr"></param>
        /// <returns></returns>
        private bool UploadFillInfoToGX(string palletID, string batchID, string[] batteryIDs, bool batchNumEnable, ref string reStr)
        {
            //  1、	查询批次信息表(TB_Batch_Index)得到批次编号和批次类型
            // 2、	插入托盘信息到托盘信息表(TB_Tray_index)中
            // 3、	更新批次信息表(TB_Batch_Index)中托盘数量和电池总数量
            // 4、	插入电池信息到电池信息表(TB_After_GradeData)
            reStr = "";

            try
            {
                TB_Batch_IndexModel batchModel = gxBatchBll.GetModel(batchID);
                if (batchModel == null)
                {
                    reStr = "批次：" + batchID + "不存在";
                    return false;
                }
                //1 检查托盘信息
                if (gxTrayBll.Exists(palletID))
                {
                    reStr = "托盘：" + palletID + "已经存在";
                    return false;
                }

                //2 坚持查电芯数据是否合格
                for (int i = 0; i < batteryIDs.Count() - 1; i++)
                {
                    int row1 = i / 12 + 1;
                    int col1 = i - (row1 - 1) * 12 + 1;
                    if (string.IsNullOrWhiteSpace(batteryIDs[i]))
                    {
                        continue;
                    }
                    if (batteryIDs[i].Length < 12) //modify by zwx,2015-07-23
                    {
                        reStr = "输入电芯条码错误：第" + (i + 1).ToString() + "个电芯条码不足12位";
                        return false;
                    }
                    for (int j = i + 1; j < batteryIDs.Count(); j++)
                    {
                        int row2 = j / 12 + 1;
                        int col2 = j - (row2 - 1) * 12 + 1;
                        if (batteryIDs[i] == batteryIDs[j])
                        {

                            reStr = "输入电芯条码有重复：第[" + row1.ToString() + "行," + col1.ToString() + "列]和[" + row2.ToString() + "行," + col2.ToString() + "列]电芯条码有重复";
                            return false;
                        }
                    }
                }

                TB_Tray_indexModel trayModel = new TB_Tray_indexModel();
                trayModel.Tf_BatchID = batchID;
                trayModel.Tf_Batchtype = batchModel.Tf_Batchtype;
                trayModel.Tf_CellCount = 0;
                trayModel.Tf_TrayId = palletID;
                trayModel.tf_CheckInTime = System.DateTime.Now;
                gxTrayBll.Add(trayModel);
                //3插入电池信息

                int batteryCount = 0;
                for (int i = 0; i < batteryIDs.Count(); i++)
                {
                    if (string.IsNullOrWhiteSpace(batteryIDs[i]) || batteryIDs[i].Length < 12) //modify by zwx,2015-07-23
                    {
                        continue;
                    }
                    //若已存在电芯，更新其绑定的托盘号
                    List<TB_After_GradeDataModel> batteryList = gxBatteryBll.GetModelList("Tf_CellSn = '" + batteryIDs[i] + "' and Tf_BatchID='" + batchID + "' ");// modify by zwx,2015-12-13
                    if (batteryList != null && batteryList.Count > 0)
                    {
                        TB_After_GradeDataModel battery = batteryList[0];
                        if (battery != null)
                        {
                            string oldTrayID = battery.Tf_TrayId;
                            battery.Tf_TrayId = palletID;
                            battery.Tf_BatchID = batchID;
                            battery.Tf_ChannelNo = (i + 1);
                            battery.Tf_Pick = null;
                            battery.Tf_Tag = 0;
                            gxBatteryBll.Update(battery, oldTrayID);
                            batteryCount++;
                        }
                    }
                    else
                    {
                        TB_After_GradeDataModel batteryModel = new TB_After_GradeDataModel();
                        batteryModel.Tf_BatchID = batchID;
                        batteryModel.Tf_Batchtype = batchModel.Tf_Batchtype;
                        batteryModel.Tf_TrayId = palletID;
                        batteryModel.Tf_ChannelNo = (i + 1);
                        batteryModel.Tf_CellSn = batteryIDs[i];
                        if (gxBatteryBll.Add(batteryModel))
                        {
                            batteryCount++;
                        }
                    }

                }
                trayModel.Tf_CellCount = batteryCount;
                if (!gxTrayBll.Update(trayModel))
                {
                    reStr = "更新托盘数据表出现错误";
                    ocvPalletBll.Delete(palletID);
                    string strWhere = "Tf_TrayId='" + palletID + "' and Tf_BatchID='" + batchID + "' ";
                    List<TB_After_GradeDataModel> batteryList = gxBatteryBll.GetModelList(strWhere);
                    foreach (TB_After_GradeDataModel battery in batteryList)
                    {
                        if (battery == null)
                        {
                            continue;
                        }
                        gxBatteryBll.Delete(battery.Tf_BatchID, battery.Tf_TrayId, battery.Tf_CellSn);
                    }
                    gxTrayBll.Delete(palletID);
                    return false;
                }

                //2 更新批次信息
                batchModel.Tf_TrayCount++;
                if (batchNumEnable)
                {
                    batchModel.Tf_CellCount += trayModel.Tf_CellCount;
                }

                gxBatchBll.Update(batchModel);
                return true;
            }
            catch (System.Exception ex)
            {

                string strWhere = "Tf_TrayId='" + palletID + "' and Tf_BatchID='" + batchID + "' ";
                List<TB_After_GradeDataModel> batteryList = gxBatteryBll.GetModelList(strWhere);
                foreach (TB_After_GradeDataModel battery in batteryList)
                {
                    if (battery == null)
                    {
                        continue;
                    }
                    gxBatteryBll.Delete(battery.Tf_BatchID, battery.Tf_TrayId, battery.Tf_CellSn);
                }
                gxTrayBll.Delete(palletID);
                MessageBox.Show("录入数据库出现异常，可能有重复的电芯条码，请检查," + ex.Message);
                return false;
            }


        }
        private void dataGridViewBatterys_BL_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            this.dataGridViewBatterys_BL.BeginEdit(true);
            this.dataGridViewBatterys_BL.CurrentCell = this.dataGridViewBatterys_BL.Rows[e.RowIndex].Cells[e.ColumnIndex];
            //this.dataGridViewBatterys_BL.EditMode = DataGridViewEditMode.EditProgrammatically;//.EditOnKeystroke;//.EditOnEnter;

            //this.dataGridViewBatterys_BL.CurrentCell



        }
        private void dataGridViewBatterys_BL_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{Tab}");
                this.dataGridViewBatterys_BL.BeginEdit(true);
                e.Handled = true;
            }
        }

        private void checkBoxBatchNumEnable_BL_CheckedChanged(object sender, EventArgs e)
        {

        }
        private DialogResult PopAskBox(string title, string content)
        {
            DialogResult re = MessageBox.Show(content, title, MessageBoxButtons.YesNoCancel);
            return re;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult.Yes != PopAskBox("提示", "确定要退出？"))
            {
                e.Cancel = true;
               
            }
        }
        private bool BarcodeRepetition(string[] batteryIDS, ref IList<string> errCelss,ref string reStr)
        {
            errCelss = new List<string>();
            reStr = "";
            int repeatCounter = 0;
            for (int i = 0; i < batteryIDS.Count()-1; i++)
            {
                if(string.IsNullOrWhiteSpace(batteryIDS[i]))
                {
                    continue;
                }
                int row1 = (int)(i / 12)+1;
                int col1 = (int)(i % 12)+1;
                string batteryID = batteryIDS[i];
                for (int j = i + 1; j < batteryIDS.Count() - 1; j++)
                {
                    string targetBatteryID = batteryIDS[j];
                    if(string.IsNullOrWhiteSpace(targetBatteryID))
                    {
                        continue;
                    }
                    if (batteryID.ToUpper() == targetBatteryID.ToUpper())
                    {
                        //reStr = string.Format("第{0}个电芯跟第{1}个电芯重码，{2}", i + 1, j + 1, batteryID);
                        //return true;
                       
                        int row2 = (int)(j / 12)+1;
                        int col2 = (int)(j % 12)+1;
                        reStr = reStr + string.Format("[{0},{1}]:[{2},{3}],",row1,col1,row2,col2);
                        errCelss.Add(string.Format("{0},{1}", row2, col2));
                        repeatCounter++;
                    }
                }
            }
            if(repeatCounter>0)
            {
                reStr = "电芯码重复，位置标号：" + reStr;
                return true;
            }
            return false;
        }
        /// <summary>
        /// 批次检测,如果有不同批，不存在的批次，均返回错误代码
        /// </summary>
        /// <param name="batteryIDS"></param>
        /// <param name="bat"></param>
        /// <returns>0：正常，1：批次不存在，2：存在不同批,3:批次为空,4:其它错误</returns>
        private int BatchParse(string[] batteryIDS,  ref IList<string> errCelss,ref string batchID, ref string reStr)
        {
            errCelss = new List<string>();
            int re = 0;
            try
            {
                batchID = "";
               // string lastBatchID = "";

                for (int i = 0; i < batteryIDS.Count(); i++)
                {
                    //if (!System.Text.RegularExpressions.Regex.IsMatch(batteryIDS[i], @"^[a-zA-Z0-9-]{13,13}$"))
                    //{
                    //    continue;
                    //}
                    // batchID = batteryIDS[i].Substring(2, 5);
                    int row1 = (int)(i / 12)+1;
                    int col1 = (int)(i % 12)+1;
                    if (string.IsNullOrEmpty(batteryIDS[i]) || batteryIDS[i].Length < 13)
                    {
                        continue;
                    }
                    if(string.IsNullOrWhiteSpace(batchID))
                    {
                        if (batteryIDS[i].Length == 13)
                        {
                            batchID = batteryIDS[i].Substring(0, 7);
                        }
                        else
                        {
                            batchID = batteryIDS[i].Substring(5, 3) + batteryIDS[i].Substring(14, 4);
                        }
                        continue;
                    }
                    string cmpBatch = "";
                    if (batteryIDS[i].Length==13)
                    {
                        cmpBatch = batteryIDS[i].Substring(0, 7);
                    }
                    else
                    {
                        cmpBatch = batteryIDS[i].Substring(5, 3) + batteryIDS[i].Substring(14, 4);
                    }
                    if (!string.IsNullOrWhiteSpace(batchID))
                    {
                        if (!string.IsNullOrWhiteSpace(cmpBatch))
                        {
                            if (batchID.ToUpper() != cmpBatch.ToUpper())
                            {
                                reStr = reStr+string.Format("批次：{0},电芯位置[{1},{2}];", cmpBatch,row1,col1);
                                errCelss.Add(string.Format("{0},{1}", row1, col1));
                                re = 2;
                               
                            }
                        }
                        
                    }
                    //if (gxBatchBll.Exists(batchID))
                    //{
                    //    break;
                    //}
                }
                if(re == 2)
                {
                    reStr = "存在不同批，" + reStr;
                    return re;
                }
                if (string.IsNullOrWhiteSpace(batchID))
                {
                    re = 3;
                    return re;
                }
                if (!gxBatchBll.Exists(batchID))
                {
                    reStr = string.Format("批次:{0}不存在", batchID);
                    re = 1;
                    return re;
                }

                re = 0;
                return re;
            }
            catch (Exception ex)
            {
                reStr = ex.ToString();
                re = 4;
                return re;
            }
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData) //激活回车键
        {
            if (this.dataGridViewBatterys_BL.Rows.Count > 4)
            {
                this.dataGridViewBatterys_BL.AllowUserToAddRows = false;

            }
            if (keyData == Keys.Enter)    //监听回车事件   
            {

                if (this.dataGridViewBatterys_BL.IsCurrentCellInEditMode)
                {
                    if (this.dataGridViewBatterys_BL.CurrentRow.Index < 3 && this.dataGridViewBatterys_BL.SelectedCells[0].ColumnIndex == 11)
                    {
                        int rowIndex = this.dataGridViewBatterys_BL.CurrentRow.Index + 1;
                        if (rowIndex >= this.dataGridViewBatterys_BL.RowCount)
                        {
                            this.dataGridViewBatterys_BL.Rows.Add();

                        }
                        if (rowIndex >= this.dataGridViewBatterys_BL.RowCount)
                        {
                            return false;
                        }
                        this.dataGridViewBatterys_BL.CurrentCell = this.dataGridViewBatterys_BL[0, rowIndex];
                        this.dataGridViewBatterys_BL.CurrentCell.Selected = true;
                    }
                    else
                    {
                        // SendKeys.Send("{Up}");
                        SendKeys.Send("{Tab}");
                        this.dataGridViewBatterys_BL.BeginEdit(true);
                        return true;
                    }

                }

            }

            return base.ProcessCmdKey(ref msg, keyData);

        }
    }
}
