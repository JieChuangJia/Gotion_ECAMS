using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECAMSDataAccess;
using ECAMSModel;
namespace WCSTest
{
    public class AssistTool
    {
        #region 
        DeviceBll devBll = new DeviceBll();
        #endregion
        public void FillDB1(string devID, string db1Addr)
        {
            switch (devID)
            {
                case "5001":
                    {
                        break;
                    }
                case "5002":
                    {
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
        //public void FillDB2(string devID)
        //{
        //    switch (devID)
        //    {
        //        case "5001":
        //            {
        //                string addrStr = "DB0340";
        //                for (int i = 1; i < 60; i++)
        //                {
        //                    string addr = ",DB" + (340 + i).ToString().PadLeft(4, '0');
        //                    addrStr += addr;
        //                }
        //                DeviceModel dev = devBll.GetModel(devID);
        //                if (dev != null)
        //                {
        //                    dev.PLCDB2 = addrStr;
        //                    dev.BytesLenDB2 = 120;
        //                    devBll.Update(dev);
        //                }
        //                break;
        //            }
        //        case "5002":
        //            {
                       
        //                break;
        //            }
        //        default:
        //            {
        //                break;
        //            }
        //    }
        //}
    }
}
