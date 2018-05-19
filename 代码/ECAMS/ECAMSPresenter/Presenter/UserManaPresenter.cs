using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECAMSDataAccess;
using System.Data;

namespace ECAMSPresenter
{
    public class UserManaPresenter:BasePresenter<IUserManageView>
    {    
        #region 全局变量
        private MainPresenter mainPre = null;                          //获取主控逻辑
        private readonly User_ListBll bllUser = new User_ListBll();
        private readonly User_RoleBll bllRole = new User_RoleBll();
        private readonly User_LimitBll bllUserLimit = new User_LimitBll();
        private readonly User_FunctionListBll bllFunctionList = new User_FunctionListBll();
        #endregion

        #region 初始化
        public UserManaPresenter(IUserManageView view)
            : base(view)
        {
            mainPre = (MainPresenter)this.View.GetPresenter(typeof(MainPresenter));
        }

        protected override void OnViewSet()
        {
            this.View.eventLoadAllFunction += LoadAllFunctionEventHandler;
            this.View.eventNewRole += NewRoleEventHandler;
            this.View.eventSaveRole += SaveRoleEventHandler;
            this.View.eventLoadAllRole += LoadAllRoleEventHandler;
            this.View.eventEidtRole += EidtRoleEventHandler;
            this.View.eventDeleteRole += DeleteRoleEventHandler;
            this.View.eventLoadCbRole += LoadCbRoleEventHandler;
            this.View.eventLoadAllUsers += LoadAllUsersEventHandler;
            this.View.eventNewUser += NewUserEventHandler;
            this.View.eventDeleteUser += DeleteUserEventHandler;
            this.View.eventSaveUser += SaveUserEventHandler;
        }
        #endregion

        #region 实现IUserManaView事件函数
        private void SaveUserEventHandler(object sender, UserSaveEventArgs e)
        {
            if (bllUser.IsExistUser(e.RoleId, e.UserName) == false)
            {
                User_ListModel userModel = new User_ListModel();
                userModel.RoleID = e.RoleId;
                userModel.RoleName = e.RoleName;
                userModel.UserName = e.UserName;
                userModel.UserID = e.UserID;
                userModel.UserPassWord = e.Password;
                bool saveStatus = bllUser.Update(userModel);
                if (saveStatus == true)
                {
                    List<User_ListModel> userModelList = bllUser.GetModelList("");
                    this.View.RefreshAllUserData(userModelList);
                    if (mainPre != null)
                    {
                        mainPre.ShowLog(ECAMSModel.EnumLogCategory.管理层日志, ECAMSModel.EnumLogType.提示, "用户修改成功！！");
                    }
                }
                else
                {
                    if (mainPre != null)
                    {
                        mainPre.ShowLog(ECAMSModel.EnumLogCategory.管理层日志, ECAMSModel.EnumLogType.提示, "用户修改失败！！");
                    }
                }
            }
            else
            {
                if (mainPre != null)
                {
                    mainPre.View.AddLog(ECAMSModel.EnumLogCategory.管理层日志, ECAMSModel.EnumLogType.提示, "已存在同角色同名称用户!!");
                }
            }
        }
        private void DeleteUserEventHandler(object sender, UserDeleteEventArgs e)
        {
           bool deleteStatus =  bllUser.Delete(e.UserID);
           if (deleteStatus == true)
           {
               List<User_ListModel> userModelList = bllUser.GetModelList("");
               this.View.RefreshAllUserData(userModelList);
               if (mainPre != null)
               {
                   mainPre.View.AddLog(ECAMSModel.EnumLogCategory.管理层日志, ECAMSModel.EnumLogType.提示, "删除用户成功！！");
               }
           }
           else
           {
               if (mainPre != null)
               {
                   mainPre.View.AddLog(ECAMSModel.EnumLogCategory.管理层日志, ECAMSModel.EnumLogType.提示, "删除用户失败！！");
               }
           }
        }

        private void NewUserEventHandler(object sender, UserEventArgs e)
        {
            User_RoleModel roleModel = bllRole.GetModel(e.RoleId);
            if (roleModel != null)
            {
                if (bllUser.IsExistUser(e.RoleId, e.UserName) == false)
                {
                    User_ListModel userModel = new User_ListModel();
                    userModel.RoleID = e.RoleId;
                    userModel.UserName = e.UserName;
                    userModel.UserPassWord = e.Password;
                    userModel.RoleName = roleModel.RoleName;
                    int addStatus = bllUser.Add(userModel);
                    if (addStatus != 1)
                    {
                        List<User_ListModel> userModelList = bllUser.GetModelList("");
                        this.View.RefreshAllUserData(userModelList);
                        if (mainPre != null)
                        {
                            mainPre.View.AddLog(ECAMSModel.EnumLogCategory.管理层日志, ECAMSModel.EnumLogType.提示, "新建用户成功！！");
                        }
                    }
                    else
                    {
                        if (mainPre != null)
                        {
                            mainPre.View.AddLog(ECAMSModel.EnumLogCategory.管理层日志, ECAMSModel.EnumLogType.提示, "新建用户失败！！");
                        }
                    }
                }
                else
                {
                    if (mainPre != null)
                    {
                        mainPre.View.AddLog(ECAMSModel.EnumLogCategory.管理层日志, ECAMSModel.EnumLogType.提示, "已存在同角色同名称用户!!");
                    }
                }
            }
        }

        private void LoadAllUsersEventHandler(object sender, EventArgs e)
        {
            List<User_ListModel> userModelList = bllUser.GetModelList("");
            if (userModelList != null)
            {
                this.View.RefreshAllUserData(userModelList);
            }
        }

        private void LoadCbRoleEventHandler(object sender, EventArgs e)
        {
            DataSet ds = bllRole.GetList("");
            this.View.BindCbRoleList(ds);
        }

        private void DeleteRoleEventHandler(object sender, EditRoleEventArgs e)
        {
            bool deleteStatus = bllRole.Delete(e.RoleID);
            if (deleteStatus == true)
            {
                List<User_RoleModel> roleList = bllRole.GetModelList("");
                List<User_FunctionListModel> functionListModel = bllFunctionList.GetModelList("");
                this.View.RefreshRoleListData(roleList);
                this.View.RefreshSetFuncData(functionListModel);
                if (mainPre != null)
                {
                    mainPre.View.AddLog(ECAMSModel.EnumLogCategory.管理层日志, ECAMSModel.EnumLogType.提示, "删除角色成功！！");
                }
            }
            else
            {
                if (mainPre != null)
                {
                    mainPre.View.AddLog(ECAMSModel.EnumLogCategory.管理层日志, ECAMSModel.EnumLogType.提示, "删除角色失败！！");
                }
            }
        }

        private void EidtRoleEventHandler(object sender, EditRoleEventArgs e)
        {
            List<int> functionList = bllUserLimit.GetFunctionIDList(e.RoleID);
            List<User_FunctionListModel> funcModelList = new List<User_FunctionListModel>();

            for (int i = 0; i < functionList.Count; i++)
            {
                funcModelList.Add(bllFunctionList.GetModel(functionList[i]));
            }
            this.View.RefreshSetFuncData(funcModelList);

        }

        private void LoadAllRoleEventHandler(object sender, EventArgs e)
        {
            List<User_RoleModel> roleList = bllRole.GetModelList("");
            this.View.RefreshRoleListData(roleList);
        }


        private void SaveRoleEventHandler(object sender, SaveRoleEventArgs e)
        {
            bool saveStatus = true;
            bool deleteRoleStatus = bllUserLimit.DeleteByRoleID(e.RoleID);
            for (int i = 0; i < e.FuncIDList.Count; i++)
            {
                User_LimitModel limitModel = new User_LimitModel();
                limitModel.FunctionID = e.FuncIDList[i];
                limitModel.RoleID = e.RoleID;
                int addStaus = bllUserLimit.Add(limitModel);
                if (addStaus == 1)
                {
                    saveStatus = false;
                    break;
                }

            }
            if (saveStatus == true)
            {
                if (mainPre != null)
                {
                    mainPre.View.AddLog(ECAMSModel.EnumLogCategory.管理层日志, ECAMSModel.EnumLogType.提示, "保存用户角色成功！！");
                }
            }
            else
            {
                if (mainPre != null)
                {
                    mainPre.View.AddLog(ECAMSModel.EnumLogCategory.管理层日志, ECAMSModel.EnumLogType.提示, "保存用户角色失败！！");
                }
            }
        }

        private void NewRoleEventHandler(object sender, NewRoleEventArgs e)
        { 
            User_RoleModel roleModel = new User_RoleModel();
            roleModel.RoleName = e.RoleName;
            roleModel.Remarks = e.Remark;
            int roleID =  bllRole.Add(roleModel);
            if (roleID != 1)
            {
                for (int i = 0; i < e.FuncIDList.Count; i++)
                {
                    User_LimitModel limitModel = new User_LimitModel();
                    limitModel.FunctionID = e.FuncIDList[i];
                    limitModel.RoleID = roleID;
                    bllUserLimit.Add(limitModel);
                }

                List<User_RoleModel> roleList = bllRole.GetModelList("");
                this.View.RefreshRoleListData(roleList);
                if (mainPre != null)
                {
                    mainPre.View.AddLog(ECAMSModel.EnumLogCategory.管理层日志, ECAMSModel.EnumLogType.提示, "新建用户角色成功！");
                }
            }
            else
            {
                if (mainPre != null)
                {
                    mainPre.View.AddLog(ECAMSModel.EnumLogCategory.管理层日志, ECAMSModel.EnumLogType.提示, "新建用户角色成功！");
                }
            }
         
        }

        private void LoadAllFunctionEventHandler(object sender, EventArgs e)
        {
            List<User_FunctionListModel> functionList = bllFunctionList.GetModelList("");
            this.View.RefreshAllFunctionData(functionList);
        }
        #endregion
    }
}
