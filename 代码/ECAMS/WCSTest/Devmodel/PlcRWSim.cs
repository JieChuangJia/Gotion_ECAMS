using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECAMSDataAccess;
namespace WCSTest
{
    /// <summary>
    /// plc读写模拟类
    /// </summary>
    public class PlcRWSim:IPlcRW
    {
        private PlcDBSimBll dbSimBll = new PlcDBSimBll();
        public bool ReadDB(string addr, ref int val)
        {
            PlcDBSimModel model = dbSimBll.GetModel(addr);
            if (model == null)
            {
                return false;
            }
            val = model.Val;
            return true;
        }
         public bool WriteDB(string addr, int val)
         {
             PlcDBSimModel model = dbSimBll.GetModel(addr);
             if (model == null)
             {
                 return false;
             }
             model.Val = val;
             return dbSimBll.Update(model);
            
         }
    }
}
