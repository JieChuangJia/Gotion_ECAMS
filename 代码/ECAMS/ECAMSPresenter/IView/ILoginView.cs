using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ECAMSPresenter
{
    public class LoginEventArgs : EventArgs
    {
        public int RoleID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsChangeUser { get; set; }
    }
    public interface ILoginView:IBaseView
    {
        #region 事件
        event EventHandler eventBindRoleData;
        event EventHandler<LoginEventArgs> eventLogin;
        #endregion

        #region 方法
        void RefreshCbRoleList(DataTable dt);
        void ShowMainForm(int roleID);

        void ShowLoginForm();
        void HideLoginForm();
      
        void ShowDialog(string content);
        #endregion
        #region 属性
        string UserName { get; set; }
        #endregion
    }
}
