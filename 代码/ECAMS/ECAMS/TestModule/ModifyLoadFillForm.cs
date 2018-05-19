using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ECAMSDataAccess;
namespace ECAMS
{
    public partial class ModifyLoadFillForm : Form
    {
        private TB_Tray_indexBll gxTrayBll = new TB_Tray_indexBll();
        private TB_Batch_IndexBll gxBatchBll = new TB_Batch_IndexBll();
        private TB_After_GradeDataBll gxBatteryBll = new TB_After_GradeDataBll();
        private OCVBatteryBll ocvBatteryBll = new OCVBatteryBll();
        public string PalletID { get; set; }
        public string CurrentBatchID { get; set; }
        public string CurrentBatteryID { get; set; }
        public int batteryPos { get; set; }
        public ModifyLoadFillForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void buttonQueryBattery_XG_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ModifyLoadFillForm_Load(object sender, EventArgs e)
        {
            List<TB_Batch_IndexModel> batchList = gxBatchBll.GetModelList(" ");
            if (batchList == null || batchList.Count < 1)
            {
                return;
            }
            foreach (TB_Batch_IndexModel batch in batchList)
            {
                if (batch != null)
                {
                    this.comboBoxNewBatch_XG.Items.Add(batch.Tf_BatchID);
                }

            }
            this.comboBoxNewBatch_XG.Text = this.CurrentBatchID;
            this.textBoxPalletID_XG.Text = this.PalletID;
            this.textBoxBatteryPos_XG.Text = this.batteryPos.ToString();
            if (!string.IsNullOrWhiteSpace(this.CurrentBatchID))
            {
                this.textboxCurrentBatch_XG.Text = this.CurrentBatchID;
            }
            if (!string.IsNullOrWhiteSpace(this.CurrentBatteryID))
            {
                this.textBoxCurrentBatteryID_XG.Text = this.CurrentBatteryID;
            }
        }
        private void Modify_BatteryInfo()
        {
            string batchID = this.comboBoxNewBatch_XG.Text;
            string batteyID = this.textBoxNewBatteryID_XG.Text;
            if (string.IsNullOrWhiteSpace(batchID))
            {
                MessageBox.Show("未设定批次");
                return;
            }
            if (string.IsNullOrWhiteSpace(batteyID))
            {
                MessageBox.Show("电芯条码为空");
                return;
            }
            TB_Batch_IndexModel batchModel = gxBatchBll.GetModel(batchID);
            if (batchModel == null)
            {
                MessageBox.Show("批次不存在");
                return;
            }
            if (string.IsNullOrWhiteSpace(this.CurrentBatteryID))
            {
                //新插入

                TB_After_GradeDataModel batteryModel = new TB_After_GradeDataModel();
                batteryModel.Tf_BatchID = batchID;
                batteryModel.Tf_Batchtype = batchModel.Tf_Batchtype;
                batteryModel.Tf_TrayId = this.PalletID;
                batteryModel.Tf_ChannelNo = this.batteryPos;
                batteryModel.Tf_CellSn = batteyID;
                if (!gxBatteryBll.Add(batteryModel))
                {
                    MessageBox.Show("提交失败!");
                    return;
                }
                OCVBatteryModel battery = new OCVBatteryModel();
                battery.batteryID = batteyID;
                battery.checkResult = "良品"; //初始值假设为合格品
                battery.rowIndex = this.batteryPos / 12 + 1;
                battery.columnIndex = this.batteryPos - (battery.rowIndex - 1) * 12 + 1;
                battery.hasBattery = true;
                battery.palletID = this.PalletID;
                battery.positionCode = this.batteryPos;
                if (ocvBatteryBll.Add(battery))
                {
                    MessageBox.Show("提交成功!");
                }
            }
            else
            {
                //更新
                TB_After_GradeDataModel batteryModel = gxBatteryBll.GetModel(this.CurrentBatchID, PalletID, this.CurrentBatteryID);
                if (batteryModel == null)
                {
                    MessageBox.Show("不存在的电芯");
                    return;
                }
                batteryModel.Tf_BatchID = batchID;
                batteryModel.Tf_CellSn = batteyID;
                if (gxBatteryBll.Update(batteryModel,PalletID))
                {
                    MessageBox.Show("提交成功!");

                }
                else
                {
                    MessageBox.Show("提交失败!");
                }
            }
        }
        private void buttonModify_XG_Click(object sender, EventArgs e)
        {
            Modify_BatteryInfo();
        }
    }
}
