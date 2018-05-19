using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECAMSDataAccess;

namespace ECAMSPresenter
{
    public class ProductManagePresenter:BasePresenter<IProductManageView>
    {     
        #region 全局变量
        private readonly ProductBll bllProduct = new ProductBll();
        private MainPresenter mainPre = null;
        #endregion

        #region 初始化
        public ProductManagePresenter(IProductManageView view)
            : base(view)
        {
            mainPre = (MainPresenter)this.View.GetPresenter(typeof(MainPresenter));
        }

        protected override void OnViewSet()
        {
            this.View.eventLoadProduct += LoadProductEventHandler;
            this.View.eventAddProduct += AddProductEventHandler;
            this.View.eventDeleteProduct += deleteEventHandler;
            this.View.eventSaveProduct += SaveProductEventHandler;
        }
        #endregion

        #region 实现函数
        private void SaveProductEventHandler(object sender, ProductEventArgs e)
        {
            //ProductModel productModel = bllProduct.GetModelByCode(e.ProductCode);
            //if (productModel != null)
            //{
            //    this.View.ShowMessageDialog("系统已存在产品编码为" + e.ProductCode + "的产品！");
            //    return;
            //}
            ProductModel updateModel = new ProductModel();
            updateModel.ProductCode = e.ProductCode;
            updateModel.ProductName = e.ProductName;
            updateModel.ProductUnit = e.ProductUnit;
            updateModel.FullTrayNum = e.FullNum;
            bool status = bllProduct.UpdateByCode(updateModel);
            if (status == true)
            {
                List<ProductModel> allProduct = bllProduct.GetModelList("");
                this.View.ShowProductData(allProduct);
                this.View.ShowMessageDialog("更新产品成功！");
            }
            else
            {
                this.View.ShowMessageDialog("更新产品失败！");
            }
        }

        private void deleteEventHandler(object sender, ProductEidtEventArgs e)
        {
            int deleteSure = this.View.AskMessBox("您确定要删除选中产品么？");
            if (deleteSure == 0)
            {
                bool status = bllProduct.Delete(e.ProductID);
                if (status == true)
                {
                    List<ProductModel> allProduct = bllProduct.GetModelList("");
                    this.View.ShowProductData(allProduct);
                    this.View.ShowMessageDialog("删除产品成功！");
                }
                else
                {
                    this.View.ShowMessageDialog("删除产品失败！");
                }
            }
        }
        private void LoadProductEventHandler(object sender, EventArgs e)
        {
            List<ProductModel> allProduct = bllProduct.GetModelList("");
            this.View.ShowProductData(allProduct);
        }
        private void AddProductEventHandler(object sender, ProductEventArgs e)
        {
            ProductModel productModel = bllProduct.GetModelByCode(e.ProductCode);
            if (productModel != null)
            {
                this.View.ShowMessageDialog("系统已存在产品编码为" + e.ProductCode + "的产品！");
                return;
            }
            ProductModel product = new ProductModel();
            product.FullTrayNum = e.FullNum;
            product.ProductCode = e.ProductCode;
            product.ProductName = e.ProductName;
            product.ProductUnit = e.ProductUnit;
            int addStatus = bllProduct.Add(product);
            if (addStatus != 1)
            {
                List<ProductModel> allProduct = bllProduct.GetModelList("");
                this.View.ShowProductData(allProduct);
                if (mainPre != null)
                {
                    mainPre.ShowLog(ECAMSModel.EnumLogCategory.管理层日志, ECAMSModel.EnumLogType.提示, "产品添加成功！");
                }
                else
                {
                    mainPre.ShowLog(ECAMSModel.EnumLogCategory.管理层日志, ECAMSModel.EnumLogType.提示, "产品添加失败！");
                }
            }
        }

        #endregion
    }
}
