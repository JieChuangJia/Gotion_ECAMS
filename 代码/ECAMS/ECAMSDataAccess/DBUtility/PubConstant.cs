using System;
using System.Configuration;

namespace ECAMSDataAccess
{
    
    public class PubConstant
    {        
        
        /// <summary>
        /// 获取连接字符串
        /// </summary>
        public static string ConnectionString
        {
            get;
            set;
            //get 
            //{
            //    string connectStr = "Data Source = .;Initial Catalog=ECAMSDataBase;User ID=sa;Password=123456;";
            //    //string dbFileName = AppDomain.CurrentDomain.BaseDirectory + @"ECAMSDataBase.mdf;";
            //    //string connectStr = @"Data Source =.\SQLEXPRESS;attachDbFileName=" + dbFileName + "Integrated Security=true;User Instance=True";
            //    //string _connectionString = ConfigurationSettings.AppSettings["connectString"];
            //    string _connectionString = connectStr;
            //    return _connectionString; 
            //}
        }
        /// <summary>
        /// 作者:np
        /// 时间:2014年5月15日
        /// 内容:连接客户数据库字符串
        /// </summary>
        public static string ConnectionString2
        {
            get;
            set;
            //get
            //{
            //    string connectStr = "Data Source = .;Initial Catalog=GXDB;User ID=sa;Password=123456;";
            //    return connectStr;
            //}
            //set { }
        }
    }
}
