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
    public partial class TrayDetailView : Form
    {
        public TrayDetailView(List<TB_After_GradeDataModel> coreModelList)
        {
            InitializeComponent();
            IniDataSource(coreModelList);
        }

        private void IniDataSource(List<TB_After_GradeDataModel> coreModelList)
        {
            this.dgv_CoreDetail.Rows.Clear();
            for (int i = 0; i < coreModelList.Count; i++)
            {
                this.dgv_CoreDetail.Rows.Add();
                this.dgv_CoreDetail.Rows[i].Cells["trayCode"].Value = coreModelList[i].Tf_TrayId;
                this.dgv_CoreDetail.Rows[i].Cells["patch"].Value = coreModelList[i].Tf_BatchID;
                this.dgv_CoreDetail.Rows[i].Cells["patchType"].Value = coreModelList[i].Tf_Batchtype;
                this.dgv_CoreDetail.Rows[i].Cells["coreCode"].Value = coreModelList[i].Tf_CellSn;

            }
        }
    }
}
