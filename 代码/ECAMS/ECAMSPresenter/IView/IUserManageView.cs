using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ECAMSDataAccess;

namespace ECAMSPresenter
{
    public class NewRoleEventArgs:EventArgs 
    {
        public string RoleName { get; set; }
        public string Remark { get; set; }
        public List<int> FuncIDList { get; set; } 
        
    }

    public class SaveRoleEventArgs : EventArgs
    {
        public int RoleID { get; set; }
        public List<int> FuncIDList { get; set; } 
    }

    public class EditRoleEventArgs:EventArgs 
    {
        public int RoleID {get;set;}
    }

    public class UserEventArgs : EventArgs
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
    }
    public class UserDeleteEventArgs : EventArgs
    {
        public int UserID { get; set; }
    }
     
    public class UserSaveEventArgs :EventArgs
    {
        public int UserID{get;set;}
        public int RoleId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string RoleName{get;set;}
    }
    public interface IUserManageView:IBaseView
    {
        #region 事件
        event EventHandler eventLoadAllFunction;
        event EventHandler eventLoadAllRole;
        event EventHandler<NewRoleEventArgs> eventNewRole;
        event EventHandler<SaveRoleEventArgs> eventSaveRole;
        event EventHandler<EditRoleEventArgs> eventEidtRole;
        event EventHandler<EditRoleEventArgs> eventDeleteRole;

        event EventHandler eventLoadCbRole;
        event EventHandler eventLoadAllUsers;
        event EventHandler<UserEventArgs> eventNewUser;
        event EventHandler<UserDeleteEventArgs> eventDeleteUser;
        event EventHandler<UserSaveEventArgs> eventSaveUser;
        #endregion

        #region 方法
        void RefreshAllFunctionData(List<User_FunctionListModel> functionList);
        void RefreshRoleListData(List<User_RoleModel> roleList);
        void RefreshSetFuncData(List<User_FunctionListModel> functionList);
        void BindCbRoleList(DataSet ds);
        void RefreshAllUserData(List<User_ListModel> userModelList);
        #endregion
    }
}
