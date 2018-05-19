using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECAMSDataAccess;

namespace ECAMSPresenter
{
    public class ProductEventArgs:EventArgs
    {
       
        public string ProductCode{get;set;}
        public string ProductName{get;set;}
        public string ProductUnit{get;set;}
        public int FullNum{get;set;}
    }

    public class ProductEidtEventArgs:EventArgs 
    {
        public int ProductID{get;set;}
    }
    public interface IProductManageView:IBaseView
    {
        #region 事件
        event EventHandler<ProductEventArgs> eventAddProduct;
        event EventHandler<ProductEventArgs> eventSaveProduct;
        event EventHandler<ProductEidtEventArgs> eventDeleteProduct;
        event EventHandler eventLoadProduct;
        #endregion
        #region 方法
        void ShowProductData(List<ProductModel> productList);
        void ShowMessageDialog(string content);
        /// <summary>
        /// 弹出是否请求对话框
        /// </summary>
        /// <param name="content"></param>
        /// <returns>返回：0是，1：否</returns>
        int AskMessBox(string content);
        #endregion
    }
}
