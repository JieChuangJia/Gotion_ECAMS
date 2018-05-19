using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Data;
using ECAMSDataAccess;
using ECAMSModel;
namespace WCSTest
{
    public class TestBusinessManager
    {
        #region 私有数据
        private TestWarehouseInOutBusiness warehouseInOutTest = null;
        private TestBusinessFillPallet fillPalletTest = null;
        private TestBusinessGrisp grispTest1 = null;
        private TestBusinessGrisp grispTest2 = null;
       // private TestWarehouseOutBusiness warehouseOutTest = null;
        private Thread sysWorkingThread = null;
        private int scanInterval = 20;
        private bool exitRunning = false;
        /// <summary>
        /// 设备列表
        /// </summary>
        private IDictionary<string, DevBase> devDicList = null;

        private IPlcRW plcRW = null;
        private DeviceBll devBll = null;
        private EventHandler<LogEventArgs> eventLogDisp = null;
        public IDBMonitorView DbMonitorView { get; set; }
        
        #endregion
        #region 公有
        public TestWarehouseInOutBusiness WarehouseInOutTest
        {
            get
            {
                return warehouseInOutTest;
            }
            private set { }
        }
        public TestBusinessFillPallet FillPalletTest
        {
            get
            {
                return fillPalletTest;
            }
            private set { }
        }
        public TestBusinessGrisp GrispTest1
        {
            get
            {
                return grispTest1;
            }
            set
            {
                grispTest1 = value;
            }
        }
        public TestBusinessGrisp GrispTest2
        {
            get
            {
                return grispTest2;
            }
            set
            {
                grispTest2 = value;
            }
        }
        //public TestWarehouseOutBusiness WarehouseOutTest
        //{
        //    get
        //    {
        //        return warehouseOutTest;
        //    }
        //    private set { }
        //}
        public bool InitTestManager(ref string resultStr)
        {
            devDicList = new Dictionary<string, DevBase>();
            devBll = new DeviceBll();
            plcRW = new PlcRWSim();
            //1 创建设备对象
            IList<DeviceModel> devList = devBll.GetModelList(" ");
            foreach (DeviceModel devME in devList)
            {
                if (devME == null)
                {
                    continue;
                }
                DevBase ecamsDev = null;
                if (devME.DeviceType == EnumDevType.堆垛机.ToString())
                {
                    ecamsDev = new Stacker(devME, plcRW, devBll);

                }
                else if (devME.DeviceType == EnumDevType.站台.ToString())
                {

                    if (devME.BytesLenDB1 <= 0 || devME.BytesLenDB2 <= 0)
                    {
                        continue;
                    }
                    ecamsDev = new TransPort(devME, plcRW, devBll);
                }
                else if (devME.DeviceType == EnumDevType.机械手.ToString())
                {
                    ecamsDev = new XYZGriper(devME, plcRW, devBll);
                }
                else
                {
                    ecamsDev = null;
                    continue;
                }
                if (!ecamsDev.Init())
                {
                    resultStr = ecamsDev.DevModel.DeviceType + " " + ecamsDev.DevModel.DeviceID + " " + "初始化失败";
                    return false;
                   
                }
                devDicList[devME.DeviceID] = ecamsDev;
            }

            //2 创建业务模拟对象
            warehouseInOutTest = new TestWarehouseInOutBusiness();
           // warehouseOutTest = new TestWarehouseOutBusiness();
            warehouseInOutTest.Stacker1001 = devDicList["1001"] as Stacker;
            warehouseInOutTest.Stacker1002 = devDicList["1002"] as Stacker;
            warehouseInOutTest.TransPort2002 = devDicList["2002"] as TransPort;
            warehouseInOutTest.TransPort2004 = devDicList["2004"] as TransPort;
            warehouseInOutTest.TransPort2006 = devDicList["2006"] as TransPort;
            warehouseInOutTest.TransPort2008 = devDicList["2008"] as TransPort;
            warehouseInOutTest.TransPort2009 = devDicList["2009"] as TransPort;
            //warehouseInOutTest.TransPort2003 = devDicList["2003"] as TransPort;
            //warehouseInOutTest.TransPort2005 = devDicList["2005"] as TransPort;
            //warehouseInOutTest.TransPort2007 = devDicList["2007"] as TransPort;
            //warehouseInOutTest.TransPort2009 = devDicList["2009"] as TransPort;

            fillPalletTest = new TestBusinessFillPallet();
            fillPalletTest.Grisper5001 = devDicList["5001"] as XYZGriper;

            grispTest1 = new TestBusinessGrisp();
            grispTest1.Grisper = devDicList["5002"] as XYZGriper;
            grispTest2 = new TestBusinessGrisp();
            grispTest2.Grisper = devDicList["5003"] as XYZGriper;

            //3
            sysWorkingThread = new Thread(new ThreadStart(SysWorkingProc));
            sysWorkingThread.IsBackground = true;
            sysWorkingThread.Name = "业务测试主线程";

            return true;
        }
        public bool ExitTestManager()
        {
            return true;
        }
        public void AttachLogHandler(EventHandler<LogEventArgs> handler)
        {
            for (int i = 0; i < devDicList.Count(); i++)
            {
                devDicList.ElementAt(i).Value.eventLogDisp += handler;
            }
            warehouseInOutTest.eventLogDisp += handler;
            fillPalletTest.eventLogDisp += handler;
            grispTest1.eventLogDisp += handler;
            grispTest2.eventLogDisp += handler;
            //warehouseOutTest.eventLogDisp += handler;
            eventLogDisp += handler;
        }
        public bool StartTest(ref string resultStr)
        {
            try
            {
                if (sysWorkingThread.ThreadState == (ThreadState.Unstarted | ThreadState.Background))
                {
                    sysWorkingThread.Start();
                }
                else
                {
                    // Monitor.Exit(sysWorkingThreadLock);
                    sysWorkingThread.Resume();
                }
                return true;
            }
            catch (System.Exception ex)
            {
                resultStr = "启动工作线程失败，错误信息：" + ex.Message;
                return false;
            }
        }
        public void StopTest(ref string resultStr)
        {
            try
            {
                if ((sysWorkingThread.ThreadState & (ThreadState.Stopped | ThreadState.Unstarted)) == 0)
                {
                    sysWorkingThread.Suspend();
                }
                
            }
            catch (System.Exception ex)
            {
                resultStr = "停止工作线程失败,返回错误信息:" + ex.Message;
               
            }
        }
        public void ExitTest(ref string resultStr)
        {
            StopTest(ref resultStr);
        }
        public void DevcomClear(string devID)
        {
            if (devDicList.Keys.Contains(devID))
            {
                DevBase dev = devDicList[devID];
                Array.Clear(dev.DB1, 0, dev.DB1.Count());
                Array.Clear(dev.DB2, 0, dev.DB2.Count());
                dev.DevStatusCommit();
                dev.DevDB1Commit();
                
            }

            //switch (devID)
            //{
            //    case "1001":
            //        {
            //            break;
            //        }
            //    case "1002":
            //        {
            //            break;
            //        }
            //    case "5001":
            //        {
            //            break;
            //        }
            //    case "5002":
            //        {
            //            break;
            //        }
            //    case "5003":
            //        {
            //            break;
            //        }
            //}
        }
       #endregion
        #region 私有功能
        private void SysWorkingProc()
        {
           
            while (!exitRunning)
            {
                Thread.Sleep(scanInterval);
               
                try
                {
                    //1 遍历设备的接口
                   
                    foreach (KeyValuePair<string, DevBase> keyVal in devDicList)
                    {
                        DataTable db1Dt = new DataTable();
                        db1Dt.Columns.Add("字节号");
                        db1Dt.Columns.Add("数值");
                        DataTable db2Dt = new DataTable();
                        db2Dt.Columns.Add("字节号");
                        db2Dt.Columns.Add("数值");
                        if (keyVal.Value == null)
                        {
                            continue;
                        }
                        DevBase dev = keyVal.Value;
                        if (!dev.ReadDB1())
                        {
                            //读设备DB2状态出现错误，在日志显示
                        }
                        for (int i = 0; i < dev.DB1.Count(); i++)
                        {
                            db1Dt.Rows.Add(new object[] { i, dev.DB1[i] });
                        }
                        for (int i = 0; i < dev.DB2.Count(); i++)
                        {
                            db2Dt.Rows.Add(new object[] { i, dev.DB2[i] });
                        }
                        if (DbMonitorView != null)
                        {
                            
                            DbMonitorView.RefreshPlcData(int.Parse(dev.DevModel.DeviceID), db1Dt, db2Dt);

                        }
                    }

                    //2 业务逻辑模拟（执行层）
                    if (warehouseInOutTest != null)
                    {
                        warehouseInOutTest.ExeBusiness();
                    }
                    if (fillPalletTest != null)
                    {
                        fillPalletTest.ExeBusiness();
                    }
                    if (grispTest1 != null)
                    {
                        grispTest1.ExeBusiness();
                    }
                    if (grispTest2 != null)
                    {
                        grispTest2.ExeBusiness();
                    }
                    //3 写DB2
                    foreach (KeyValuePair<string, DevBase> keyVal in devDicList)
                    {
                        if (keyVal.Value == null)
                        {
                            continue;
                        }
                        DevBase dev = keyVal.Value;
                        if (!dev.DevStatusCommit())
                        {
                            //设备指令发送，如出现错误，在日志显示
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    LogModel log = new LogModel();
                    log.logCategory = EnumLogCategory.控制层日志.ToString();
                    log.logContent = "业务模拟系统异常，信息：" + ex.Message;
                    log.logTime = System.DateTime.Now;

                    log.logType = EnumLogType.错误.ToString();
                    AddLog(log);
                }

            }

        }
        /// <summary>
        /// 增加一条日志记录
        /// </summary>
        protected void AddLog(LogModel log)
        {
            if (eventLogDisp != null)
            {
                LogEventArgs arg = new LogEventArgs();
                // arg.happenTime = System.DateTime.Now;
                // arg.logMes = log.logContent;
                arg.LogTime = System.DateTime.Now;
                arg.LogCate = EnumLogCategory.控制层日志;
                arg.LogContent = log.logContent;
                arg.LogType = EnumLogType.调试信息;
                eventLogDisp.Invoke(this, arg);
            }

        }
       
        #endregion
    }
}
