using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECAMSDataAccess;
using System.Data;
using ECAMSModel;
using System.Configuration;
namespace ECAMSPresenter
{
    public class LoginPresenter : BasePresenter<ILoginView>
    {
        #region 全局变量
        private readonly User_ListBll bllUser = new User_ListBll();
        private readonly User_RoleBll bllRole = new User_RoleBll();
        private LogBll logBll = new LogBll();
        #endregion

        #region 初始化
        public LoginPresenter(ILoginView view)
            : base(view)
        {
            
        }

        protected override void OnViewSet()
        {
            string jxDB = ConfigurationManager.AppSettings["JXDataBase"];
            string jxDBUserPwd = ConfigurationManager.AppSettings["JXDataBaseUserPwd"];
            jxDBUserPwd = EncAndDec.Decode(jxDBUserPwd, "zwx", "xwz");
            if (string.IsNullOrEmpty(jxDBUserPwd))
            {
                
                return;
            }
            PubConstant.ConnectionString = jxDB + jxDBUserPwd;
            //PubConstant.ConnectionString = jxDB + "Persist Security info = True;Initial Catalog=ECAMSDatabase;User ID=sa;Password=123456;";
            this.View.eventBindRoleData += BindRoleDataEventHandler;
            this.View.eventLogin += LoginEventHandler;

        }
        #endregion

        #region 实现ILoginView事件方法
        private void LoginEventHandler(object sender, LoginEventArgs e)
        {
            bool isRegister = bllUser.IsUserRegister(e.RoleID, e.UserName, e.Password);
            string roleName = "一般用户";
            if (e.RoleID == 5)
            {
                roleName = "管理员";
            }
            else if (e.RoleID == 6)
            {
                roleName = "系统管理员";
            }
            string loginInfo = null;
            if (isRegister)
            {
                loginInfo = string.Format("用户：{0},角色:{1},登录系统，结果：成功！", e.UserName, roleName);
            }
            else
            {
                loginInfo = string.Format("用户：{0},角色:{1},登录系统，结果：失败！", e.UserName, e.RoleID);
            }
            LogModel logModel = new LogModel();
            logModel.logCategory = EnumLogCategory.管理层日志.ToString();
            logModel.logType = EnumLogType.提示.ToString();
            logModel.logContent = loginInfo;
            logModel.logTime = System.DateTime.Now;
            logBll.Add(logModel);
            if (e.IsChangeUser == false)
            {
                if (isRegister == true)
                {
                    this.View.HideLoginForm();
                    this.View.ShowMainForm(e.RoleID);
                }
                else
                {
                    this.View.ShowDialog("登录失败！用户权限、用户名或密码错误！");
                }
            }
            else
            {
                if (isRegister == true)
                {
                    MainPresenter mainPre = (MainPresenter)this.View.GetPresenter(typeof(MainPresenter));
                    if (mainPre != null)
                    {
                        mainPre.View.ShowView();
                        mainPre.View.OnSetLimit(e.RoleID);
                        this.View.HideLoginForm();
                    }
                }
                else
                {
                    this.View.ShowDialog("登录失败！用户权限、用户名或密码错误！");
                }
             
            }
        }

        private void BindRoleDataEventHandler(object sender, EventArgs e)
        {
            DataSet ds = bllRole.GetList("");
            if (ds != null && ds.Tables.Count>0)
            {
                this.View.RefreshCbRoleList(ds.Tables[0]);
            }

        }
        #endregion
    }
}
