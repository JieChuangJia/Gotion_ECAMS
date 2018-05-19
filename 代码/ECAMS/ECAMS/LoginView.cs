using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ECAMSPresenter;

namespace ECAMS
{
    public partial class LoginView : BaseView,ILoginView
    {
        #region 全局变量
        public bool isChangeUser = false;
        #endregion

        #region 初始化
        public LoginView()
        {
            InitializeComponent();
          
        }

        private void LoginView_Load(object sender, EventArgs e)
        {
            OnBindRoleData();
        }

        protected override object CreatePresenter()
        {
            LoginPresenter logPres = new LoginPresenter(this);
            for (int i = 0; i < allPresenterList.Count; i++)
            {
                if (allPresenterList[i].GetType() == typeof(LoginPresenter))
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

        #region 事件
        private void bt_login_Click(object sender, EventArgs e)
        {
            OnLogin();
        }

        private void bt_cancel_Click(object sender, EventArgs e)
        {
            if (this.isChangeUser == false)
            {
                this.Close();
            }
            else
            {
                this.Hide();
            }
        }
        #endregion

        #region 实现ILoginView 事件
        public event EventHandler eventBindRoleData;
        public event EventHandler<LoginEventArgs> eventLogin;
        #endregion

        #region 触发ILoginView 事件
        private void OnLogin()
        {
            if (this.cb_UserRole.SelectedItem == null)
            {
                MessageBox.Show("请选择用户角色！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int roleID = int.Parse(this.cb_UserRole.SelectedValue.ToString());
            string userName = this.tb_userName.Text.Trim();
            string password = this.tb_userPassword.Text.Trim();
            this.UserName = userName;
            LoginEventArgs loginArgs = new LoginEventArgs();
            loginArgs.RoleID = roleID;
            loginArgs.UserName = userName;
            loginArgs.Password = password;
            loginArgs.IsChangeUser = this.isChangeUser;
            this.eventLogin.Invoke(this, loginArgs);
        }
        private void OnBindRoleData()
        {
            if (this.eventBindRoleData != null)
            {
                this.eventBindRoleData.Invoke(this, null);
            }
        }
        #endregion

        #region 实现ILoginView 方法

        public void RefreshCbRoleList(DataTable dt)
        {
            this.cb_UserRole.DataSource = dt;
            this.cb_UserRole.DisplayMember = "RoleName";
            this.cb_UserRole.ValueMember = "RoleID";
        }
        public void ShowMainForm(int roleID)
        {
            MainView main = new MainView(roleID);
            main.ShowDialog();
        }
        public void ShowDialog(string content)
        {
            MessageBox.Show(content, "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public void ShowLoginForm()
        {
            this.isChangeUser = true;
            this.Show();
        }

        public void HideLoginForm()
        {
            this.Hide();
        }
        #endregion

        #region 实现ILoginView属性
        public string UserName { get; set; }
        #endregion

    }
}
