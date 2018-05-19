using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using ECAMSPresenter;
using ECAMSDataAccess;

namespace ECAMS
{
    public partial class ProductManageView : BaseView,IProductManageView
    {
        #region 全局变量
       
        #endregion


        #region 初始化
        public ProductManageView()
        {
            InitializeComponent();
        }
        protected override object CreatePresenter()
        {
            ProductManagePresenter productPres = new ProductManagePresenter(this);
            for (int i = 0; i < allPresenterList.Count; i++)
            {
                if (allPresenterList[i].GetType() == typeof(ProductManagePresenter))
                {
                    allPresenterList.RemoveAt(i);
                }
            }
            allPresenterList.Add(productPres);
            return productPres;
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
        private void tsb_AddProduct_Click(object sender, EventArgs e)
        {
            OnAddProduct();
        }

        private void tsb_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void ProductManageView_Load(object sender, EventArgs e)
        {
            OnLoadProduct();
        }

        private void tsb_DeleteProduct_Click(object sender, EventArgs e)
        {
            OnDeleteProduct();
        }
        private void tsb_EditProduct_Click(object sender, EventArgs e)
        {
            EditProduct();
        }
        private void dgv_ProductManage_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            EditProduct();
        }


        private void tsb_SaveProduct_Click(object sender, EventArgs e)
        {
            OnSaveProduct();
        }
        #endregion
       
        #region 实现IProductManageView 事件
        public event EventHandler eventLoadProduct;
        public event EventHandler<ProductEventArgs> eventAddProduct;
        public event EventHandler<ProductEventArgs> eventSaveProduct;
        public event EventHandler<ProductEidtEventArgs> eventDeleteProduct;
        #endregion

        #region 触发IProductManageView 事件
        private void OnSaveProduct()
        {
            if (this.eventSaveProduct != null)
            {
                bool isAllRight = true;
                JudgeTextBox(ref isAllRight);
                if (isAllRight == true)
                {
                    ProductEventArgs saveArgs = new ProductEventArgs();
                    saveArgs.FullNum = int.Parse(this.tb_FullNum.Text.Trim());
                    saveArgs.ProductCode = this.tb_ProductCode.Text.Trim();
                    saveArgs.ProductName = this.tb_ProductName.Text.Trim();
                    saveArgs.ProductUnit = this.tb_ProductUnit.Text.Trim();

                    this.eventSaveProduct.Invoke(this, saveArgs);
                }
            }
        }

        private void OnDeleteProduct()
        {
            if (this.eventDeleteProduct != null)
            {
                if (this.dgv_ProductManage.SelectedRows.Count > 0)
                {
                    int rowIndex = this.dgv_ProductManage.CurrentRow.Index;
                    int  productID = int.Parse(this.dgv_ProductManage.Rows[rowIndex].Cells["ProductID"].Value.ToString());
                    ProductEidtEventArgs deleteArgs = new ProductEidtEventArgs();
                    deleteArgs.ProductID = productID;
                    this.eventDeleteProduct.Invoke(this, deleteArgs);
                }
            }
        }

        private void OnLoadProduct()
        {
           
            if (this.eventLoadProduct != null)
            {
                this.eventLoadProduct.Invoke(this, null);
            }
        }
        private void OnAddProduct()
        {
            if (this.eventAddProduct != null)
            {
                bool isAllRight = true;
                JudgeTextBox(ref isAllRight);
                if (isAllRight == true)
                {
                    ProductEventArgs productArgs = new ProductEventArgs();
                    productArgs.ProductCode = this.tb_ProductCode.Text.Trim();
                    productArgs.ProductName = this.tb_ProductName.Text.Trim();
                    productArgs.ProductUnit = this.tb_ProductUnit.Text.Trim();
                    productArgs.FullNum = int.Parse(this.tb_FullNum.Text.Trim());
                    this.eventAddProduct.Invoke(this, productArgs);
                }
            }
        }
        #endregion

        #region 实现IProductManageView 方法
        public void ShowProductData(List<ProductModel> productList)
        {
            this.dgv_ProductManage.Rows.Clear();
            for (int i = 0; i < productList.Count; i++)
            {
                this.dgv_ProductManage.Rows.Add();
                this.dgv_ProductManage.Rows[i].Cells["ProductID"].Value = productList[i].ProductID;
                this.dgv_ProductManage.Rows[i].Cells["ProductCode"].Value = productList[i].ProductCode;
                this.dgv_ProductManage.Rows[i].Cells["ProductName"].Value = productList[i].ProductName;
                this.dgv_ProductManage.Rows[i].Cells["ProductUnit"].Value = productList[i].ProductUnit;
                this.dgv_ProductManage.Rows[i].Cells["FullNum"].Value = productList[i].FullTrayNum;
            }
        }

        public void ShowMessageDialog(string content)
        {
            MessageBox.Show(content, "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        private void EditProduct()
        {
            if (this.dgv_ProductManage.SelectedRows.Count > 0)
            {
               
                int rowIndex = this.dgv_ProductManage.CurrentRow.Index;
                string productID = this.dgv_ProductManage.Rows[rowIndex].Cells["ProductID"].Value.ToString();
                string productCode = this.dgv_ProductManage.Rows[rowIndex].Cells["ProductCode"].Value.ToString();
                string productName = this.dgv_ProductManage.Rows[rowIndex].Cells["ProductName"].Value.ToString();
                string productUnit = this.dgv_ProductManage.Rows[rowIndex].Cells["ProductUnit"].Value.ToString();
                string fullNum = this.dgv_ProductManage.Rows[rowIndex].Cells["FullNum"].Value.ToString();
                this.tb_FullNum.Text = fullNum;
                this.tb_ProductCode.Text = productCode;
                this.tb_ProductName.Text = productName;
                this.tb_ProductUnit.Text = productUnit;
            }
        }

        private void JudgeTextBox(ref bool isAllRight)
        {
            if (this.tb_ProductUnit.Text == "")
            {
                MessageBox.Show("产品单位不能为空！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                isAllRight = false;
                return;
            }
            if (this.tb_ProductName.Text == "")
            {
                MessageBox.Show("产品名称不能为空！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                isAllRight = false;
                return;
            }
            if (this.tb_ProductCode.Text == "")
            {
                MessageBox.Show("产品码不能为空！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                isAllRight = false;
                return;
            }
            Regex reg = new Regex("^[0-9]+$");              //判断是不是数据，要不是就表示没有选择，则从隐藏域里读出来      
            Match ma = reg.Match(this.tb_FullNum.Text.Trim());
            if (!ma.Success)
            {
                MessageBox.Show("满料框数量输入有误！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                isAllRight = false;
                return;
            }
        }
        #endregion

    }
}
