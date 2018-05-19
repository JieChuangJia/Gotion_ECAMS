using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECAMSPresenter
{
    public class BasePresenter<IView>
    {
        public IView View { get; set; }
        public BasePresenter(IView view)
        {

            this.View = view;
            this.OnViewSet();
        }

        /// <summary>
        /// 子类实现，父类构造函数内调用，在此函数内注册view的事件响应函数
        /// </summary>
        protected virtual void OnViewSet()
        { }

    }
}
