using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ECAMSDataAccess;
using ECAMSPresenter;

namespace ECAMS
{
    public partial class UserManageView : BaseView,IUserManageView
    {
        #region 初始化
        public UserManageView()
        {
            InitializeComponent();
        }

        private void UserManageView_Load(object sender, EventArgs e)
       {
           OnLoadAllFunction();
           OnLoadAllRole();
           OnLoadCbRole();
           OnLoadAllUsers();
       }
        protected override object CreatePresenter()
        {
            UserManaPresenter managerPre = new UserManaPresenter(this);
            for (int i = 0; i < allPresenterList.Count; i++)
            {
                if (allPresenterList[i].GetType() == typeof(UserManaPresenter))
                {
                    allPresenterList.RemoveAt(i);
                }
            }
            allPresenterList.Add(managerPre);
            return managerPre;
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
        private void bt_HandTaskExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bt_userManageExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bt_RoleManageExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bt_roleAddFunc_Click(object sender, EventArgs e)
        {
            if (this.dgv_FunctionList.SelectedRows.Count > 0)
            {
                int rowIndex = this.dgv_FunctionList.CurrentRow.Index;
                string functionID = this.dgv_FunctionList.Rows[rowIndex].Cells["functionID"].Value.ToString();
                string functionName = this.dgv_FunctionList.Rows[rowIndex].Cells["functionName"].Value.ToString();
                string remark =  this.dgv_FunctionList.Rows[rowIndex].Cells["remark"].Value.ToString();
            
                for (int i = 0; i < this.dgv_SetFunction.Rows.Count; i++)
                {
                    if (this.dgv_SetFunction.Rows[i].Cells["functionIDTemp"].Value.ToString() == functionID)
                    {
                        return;
                    }
                }
                this.dgv_SetFunction.Rows.Add();
                int lastIndex = this.dgv_SetFunction.Rows.Count - 1;
                this.dgv_SetFunction.Rows[lastIndex].Cells["functionIDTemp"].Value = functionID;
                this.dgv_SetFunction.Rows[lastIndex].Cells["functionNameTemp"].Value = functionName;
                this.dgv_SetFunction.Rows[lastIndex].Cells["remarkTemp"].Value = remark;

            }
        }

        private void bt_DeleteFunc_Click(object sender, EventArgs e)
        {
            int rowIndex = this.dgv_SetFunction.CurrentRow.Index;
            this.dgv_SetFunction.Rows.RemoveAt(rowIndex);
        }

        private void bt_NewRole_Click(object sender, EventArgs e)
        {
            OnNewRole();
        }

        private void bt_RoleSave_Click(object sender, EventArgs e)
        {
            OnSaveRole();
        }
 
        private void dgv_roleList_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            OnEditRole();
        }


        private void bt_deleteRole_Click(object sender, EventArgs e)
        {
            OnDeleteRole();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnLoadCbRole();
        }

        private void bt_NewUser_Click(object sender, EventArgs e)
        {
            OnNewUser();
        }

        private void bt_deleteUser_Click(object sender, EventArgs e)
        {
            OnDeleteUser();
        }


        private void bt_saveUser_Click(object sender, EventArgs e)
        {
            OnSaveUser();
        }

        private void dgv_usersList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dgv_usersList.SelectedRows.Count > 0)
            { 
                int currentIndex = this.dgv_usersList.CurrentRow.Index;
                string userName = this.dgv_usersList.Rows[currentIndex].Cells["UserName"].Value.ToString();
                string password = this.dgv_usersList.Rows[currentIndex].Cells["userPassword"].Value.ToString();
                string userRoleName = this.dgv_usersList.Rows[currentIndex].Cells["userRoleName"].Value.ToString();
                this.tb_userName.Text = userName;
                this.tb_Password.Text = password;
                for (int i = 0; i < this.cb_AllRoleList.Items.Count; i++)
                {
                    DataRowView drv = (DataRowView)this.cb_AllRoleList.Items[i];
                    string roleNameTemp = drv.Row[1].ToString();
                   
                    if (userRoleName == roleNameTemp)
                    {
                        this.cb_AllRoleList.SelectedItem = this.cb_AllRoleList.Items[i];
                        break;
                    }
                }
            }
        }

        #endregion

        #region 实现IUserManageView事件
        public event EventHandler eventLoadAllFunction;
        public event EventHandler<NewRoleEventArgs> eventNewRole;
        public event EventHandler<SaveRoleEventArgs> eventSaveRole;
        public event EventHandler eventLoadAllRole;
        public event EventHandler<EditRoleEventArgs> eventEidtRole;
        public event EventHandler<EditRoleEventArgs> eventDeleteRole;
        public event EventHandler eventLoadCbRole;

        public event EventHandler eventLoadAllUsers;
        public event EventHandler<UserEventArgs> eventNewUser;
        public event EventHandler<UserDeleteEventArgs> eventDeleteUser;
        public event EventHandler<UserSaveEventArgs> eventSaveUser;
        #endregion

        #region 实现IUserManageView方法
        public void RefreshAllUserData(List<User_ListModel> userModelList)
        {
            this.dgv_usersList.Rows.Clear();

            for (int i = 0; i < userModelList.Count; i++)
            {
                this.dgv_usersList.Rows.Add();
                this.dgv_usersList.Rows[i].Cells["userInforID"].Value = userModelList[i].UserID;
                this.dgv_usersList.Rows[i].Cells["userName"].Value = userModelList[i].UserName;
                this.dgv_usersList.Rows[i].Cells["userPassword"].Value = userModelList[i].UserPassWord;
                this.dgv_usersList.Rows[i].Cells["userRoleName"].Value = userModelList[i].RoleName;
            }
        }

        public void RefreshSetFuncData(List<User_FunctionListModel> functionList)
        {
            this.dgv_SetFunction.Rows.Clear();

            for (int i = 0; i < functionList.Count; i++)
            {
                this.dgv_SetFunction.Rows.Add();
                this.dgv_SetFunction.Rows[i].Cells["functionIDTemp"].Value = functionList[i].FunctionID;
                this.dgv_SetFunction.Rows[i].Cells["functionNameTemp"].Value = functionList[i].FunctionName;
                this.dgv_SetFunction.Rows[i].Cells["remarkTemp"].Value = functionList[i].Remarks;
            }
        }

       public void RefreshAllFunctionData(List<User_FunctionListModel> functionList)
       {
           this.dgv_FunctionList.Rows.Clear();
       
           for (int i = 0; i < functionList.Count; i++)
           {
               this.dgv_FunctionList.Rows.Add();
               this.dgv_FunctionList.Rows[i].Cells["functionID"].Value =  functionList[i].FunctionID;
               this.dgv_FunctionList.Rows[i].Cells["functionName"].Value =   functionList[i].FunctionName;
               this.dgv_FunctionList.Rows[i].Cells["remark"].Value = functionList[i].Remarks;
           }
       }

       public void RefreshRoleListData(List<User_RoleModel> roleList)
       {
           this.dgv_roleList.Rows.Clear();

           for (int i = 0; i < roleList.Count; i++)
           {
               this.dgv_roleList.Rows.Add();
               this.dgv_roleList.Rows[i].Cells["roleID"].Value = roleList[i].RoleID;
               this.dgv_roleList.Rows[i].Cells["roleName"].Value = roleList[i].RoleName;
               this.dgv_roleList.Rows[i].Cells["roleRemark"].Value = roleList[i].Remarks;
           }
       }

       public void BindCbRoleList(DataSet ds)
       {
           if(ds!= null&& ds.Tables.Count>0&& ds.Tables[0].Rows.Count>0)
           {
                this.cb_AllRoleList.DataSource = ds.Tables[0];
                this.cb_AllRoleList.DisplayMember = "RoleName";
                this.cb_AllRoleList.ValueMember = "RoleID";
           }
        
       }
        #endregion

        #region 触发IUserManageView事件
       private void OnSaveUser()
       {
           if (this.eventSaveUser != null)
           {
               if (this.dgv_usersList.SelectedRows.Count > 0 && this.cb_AllRoleList.SelectedItem!= null
                   && this.tb_userName.Text!= "" && this.tb_Password.Text != "")
               {
                   int currentIndex = this.dgv_usersList.CurrentRow.Index;

                   UserSaveEventArgs saveArgs = new UserSaveEventArgs();
                   saveArgs.UserID = int.Parse(this.dgv_usersList.Rows[currentIndex].Cells["userInforID"].Value.ToString());
                   saveArgs.RoleId = int.Parse( this.cb_AllRoleList.SelectedValue.ToString());
                   saveArgs.RoleName = this.cb_AllRoleList.Text;
                   saveArgs.UserName = this.tb_userName.Text.Trim();
                   saveArgs.Password = this.tb_Password.Text.Trim();
                   this.eventSaveUser.Invoke(this, saveArgs);

               }
           }
       }
       private void OnDeleteUser()
       {
           if (this.eventDeleteUser != null)
           {
               if (this.dgv_usersList.SelectedRows.Count > 0)
               {
                   int currentIndex = this.dgv_usersList.CurrentRow.Index;
                   int userID = int.Parse(this.dgv_usersList.Rows[currentIndex].Cells["userInforID"].Value.ToString());
                   UserDeleteEventArgs deleteUserArgs = new UserDeleteEventArgs();
                   deleteUserArgs.UserID = userID;
                   this.eventDeleteUser.Invoke(this, deleteUserArgs);
               }
           }
       }
       private void OnNewUser()
       {
           if (this.eventNewUser != null)
           {
               if (this.cb_AllRoleList.SelectedItem == null)
               {
                   MessageBox.Show("请选择用户角色！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   return;
               }
               if (this.tb_userName.Text == "")
               {
                   MessageBox.Show("请输入用户名称！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   return;
               }
               if (this.tb_Password.Text == "")
               {
                   MessageBox.Show("密码不允许为空！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   return;
               }

               UserEventArgs userArgs = new UserEventArgs();
               userArgs.UserName = this.tb_userName.Text.Trim();
               userArgs.RoleId = int.Parse(this.cb_AllRoleList.SelectedValue.ToString());
               userArgs.Password = this.tb_Password.Text.Trim();
               this.eventNewUser.Invoke(this, userArgs);

           }
       }

       private void OnLoadAllUsers()
       {
           if (this.eventLoadAllUsers != null)
           {
               this.eventLoadAllUsers.Invoke(this, null);
           }
       }
        private void OnLoadCbRole()
        {
            if (this.eventLoadCbRole != null)
            {
                this.eventLoadCbRole.Invoke(this, null);
            }
        }

       private void OnDeleteRole()
       {
           if (this.eventDeleteRole != null)
           {
               if (this.dgv_roleList.CurrentRow == null)
               {
                   return;
               }
               int roleIndex = this.dgv_roleList.CurrentRow.Index;
               int roleID = int.Parse(this.dgv_roleList.Rows[roleIndex].Cells["roleID"].Value.ToString());
               EditRoleEventArgs deleteRoleArgs = new EditRoleEventArgs();
               deleteRoleArgs.RoleID = roleID;
               this.eventDeleteRole.Invoke(this, deleteRoleArgs);
           }
       }
       private void OnEditRole()
       {
           if (this.eventEidtRole != null)
           {
               if (this.dgv_roleList.CurrentRow == null)
               {
                   return;
               }
               int roleIndex = this.dgv_roleList.CurrentRow.Index;
               int roleID= int.Parse(this.dgv_roleList.Rows[roleIndex].Cells["roleID"].Value.ToString());
               this.tb_RoleName.Text = this.dgv_roleList.Rows[roleIndex].Cells["roleName"].Value.ToString();
               this.tb_remark.Text = this.dgv_roleList.Rows[roleIndex].Cells["roleRemark"].Value.ToString();
               EditRoleEventArgs eidtRoleArgs = new EditRoleEventArgs();
               eidtRoleArgs.RoleID = roleID;
               this.eventEidtRole.Invoke(this, eidtRoleArgs);
           }
       }

       private void OnNewRole()
       {
           if (this.eventNewRole != null)
           {
               if(this.tb_RoleName.Text.Trim() == "")
               {
                   MessageBox.Show("角色名称不能为空！","信息提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                   return;
               }
               List<int> functionIDList = new List<int>();
               for (int i = 0; i < this.dgv_SetFunction.Rows.Count; i++)
               {
                   functionIDList.Add(int.Parse(this.dgv_SetFunction.Rows[i].Cells["functionIDTemp"].Value.ToString()));
               }
               NewRoleEventArgs roleArgs = new NewRoleEventArgs();
               roleArgs.FuncIDList = functionIDList;
               roleArgs.RoleName = this.tb_RoleName.Text.Trim();
               roleArgs.Remark = this.tb_remark.Text;
               this.eventNewRole.Invoke(this, roleArgs);

           }
       }

       private void OnLoadAllFunction()
       {
           if (this.eventLoadAllFunction != null)
           {
               this.eventLoadAllFunction.Invoke(this, null);
           }
       }

       private void OnSaveRole()
       {
           if (this.eventSaveRole != null)
           {
               SaveRoleEventArgs saveArgs = new SaveRoleEventArgs();
               if (this.dgv_roleList.CurrentRow == null)
               {
                   return;

               }
               if (this.dgv_SetFunction.Rows.Count == 0)
               {
                   DialogResult dialog = MessageBox.Show("您确定当前任务锁定角色权限为空？", "信息提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                   if (dialog == System.Windows.Forms.DialogResult.No)
                   {
                       return;
                   }
               }
               List<int> functionIDList = new List<int>();
               for (int i = 0; i < this.dgv_SetFunction.Rows.Count; i++)
               {
                   functionIDList.Add(int.Parse(this.dgv_SetFunction.Rows[i].Cells["functionIDTemp"].Value.ToString()));
               }
               int roleIndex = this.dgv_roleList.CurrentRow.Index;
               int roleID = int.Parse(this.dgv_roleList.Rows[roleIndex].Cells["roleID"].Value.ToString());
               saveArgs.RoleID = roleID;
               saveArgs.FuncIDList = functionIDList;
               this.eventSaveRole.Invoke(this, saveArgs);
           }


       }

       private void OnLoadAllRole()
       {
           if (this.eventLoadAllRole != null)
           {
               this.eventLoadAllRole.Invoke(this, null);
           }
        }
        #endregion

        #region 界面私有方法
       public DataTable GetDgvToTable(DataGridView dgv)
       {
           DataTable dt = new DataTable();
           for (int count = 0; count < dgv.Columns.Count; count++)
           {
               DataColumn dc = new DataColumn(dgv.Columns[count].Name.ToString(), typeof(string));
               dt.Columns.Add(dc);
           }
           for (int count = 0; count < dgv.Rows.Count; count++)
           {
               DataRow dr = dt.NewRow(); for (int countsub = 0; countsub < dgv.Columns.Count; countsub++)
               {
                   dr[countsub] = Convert.ToString(dgv.Rows[count].Cells[countsub].Value);
               }
               dt.Rows.Add(dr);
           } return dt;
       }
        #endregion

      

     

    }
}
