using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Threading;
using System.Timers;
using System.Collections;
using System.IO;
//using Sygole.HFReader;
using ECAMSPresenter;
using SygoleHFReaderIF;
using ECAMSDataAccess;
using ECAMSModel;
using PLCControl;
namespace ECAMS
{
    public partial class TestModuleForm : Form
    {
        public delegate void DelegateAddLog(string log);
        private delegate void DelegateRefeshRfid();
        #region 数据区
        private ECAMWCS ctlManager = null;
        private ControlTaskBll ctlTaskBll = null;
        private DeviceBll devBll = null;
        private ControlInterfaceBll ctlTaskIFBll = null;
        
        private OCVPalletBll palletBll = null;
        private OCVBatteryBll batteryBll = null;
        private ManageTaskBll manTaskBll = null;
        private MakeCardRecordBll makeCardBll = null;
        private GoodsSiteBll bllGoodsSite = new GoodsSiteBll();
        private StockBll bllStock = new StockBll();
        private StockDetailBll bllStockDetail = new StockDetailBll();
        private StockListBll bllStockList = new StockListBll();
        private readonly TB_Batch_IndexBll bllBatchIndex = new TB_Batch_IndexBll();
        private readonly TB_Tray_indexBll bllTrayIndex = new TB_Tray_indexBll();
        private readonly LogBll bllLog = new LogBll();
        private RfidRdRecordBll rfidRecordBll = new RfidRdRecordBll();
        private OCVRfidReadingBll ocvRfidReadBll = new OCVRfidReadingBll();
        private PalletHistoryRecordBll palletTraceBll = new PalletHistoryRecordBll();
        private View_QueryStockListBll bllViewStock = new View_QueryStockListBll();
        PLCRW plcRwObj = null;
        PLCRWNet plcRwObj2 = null;
        IPlcRW plcRwIF = null;
       
        private System.Windows.Forms.Timer timer3 = null;
        private object plcAutoReadLock = new object();
        #endregion
        public TestModuleForm()
        {
            InitializeComponent();
        }
        #region 日志相关
        /// <summary>
        /// 增加一条日志
        /// </summary>
        /// <param name="log"></param>
        private void AddLog(string log)
        {
            if (this.richTextBoxLog.InvokeRequired)
            {
                DelegateAddLog delegateObj = new DelegateAddLog(delegateAddLog);
                this.Invoke(delegateObj, new object[] { log });
            }
            else
            {
                log = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + log;
                this.richTextBoxLog.Text += (log + "\r\n");
            }
        }
        /// <summary>
        /// 作者:np
        /// 时间:2014年8月10日
        /// 内容:添加日志
        /// </summary>
        /// <param name="logStr"></param>
        /// <param name="logType"></param>
        /// <param name="logcate"></param>
        private void AddLog(string logStr,EnumLogType logType,EnumLogCategory logcate)
        {
            LogModel log = new LogModel();
            log.logTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            if (logStr.Length > 1000)//数据库默认1000长度
            {
                log.logContent = logStr.Substring(0, 1000);
            }
            else
            {
                log.logContent = logStr;
            }
            log.logType =logType.ToString();
            log.logCategory = logcate.ToString();

            //bllLog.DeleteHistoryLog(180);//只保留一个月的日志数据
            bllLog.AsyncAddLog(log);
            AddLog(log.logContent);//显示用
        }

        private void delegateAddLog(string log)
        {
            this.richTextBoxLog.Text += (log + "\r\n");
        }
        private void LogEventHandler(object sender, LogEventArgs e)
        {
            string logStr = e.LogTime.ToString()+" " +e.LogContent;
            AddLog(logStr);
        }
        private void ErrorEventHandler(object sender, ECAMSErrorEventArgs e)
        {
            string logStr = e.LogTime.ToString()+"  错误号: "+ e.ErrorCode.ToString() + " " +e.LogContent;
            AddLog(logStr);
        }
        #endregion

        private void Form1_Load(object sender, EventArgs e)
        {
            IniDBProductStatus();


            //ctlManager = new ECAMWCS();
            //ctlManager.AttachErrorHandler(ErrorEventHandler);
            //ctlManager.AttachLogHandler(LogEventHandler);
            this.comboBoxPlcObjList.SelectedIndex = 0;
            ctlManager = MainPresenter.wcs;
            
          //  this.tabPage1.Enabled = false;
            devBll = new DeviceBll();
            ctlTaskBll = new ControlTaskBll();
            ctlTaskIFBll = new ControlInterfaceBll();
            palletBll = new OCVPalletBll();
            batteryBll = new OCVBatteryBll();
            manTaskBll = new ManageTaskBll();
            makeCardBll = new MakeCardRecordBll();
            plcRwObj = new PLCRW();
            plcRwObj.eventLinkLost += PlcLostConnectHandler;
            plcRwObj2 = new PLCRWNet();
            plcRwObj2.eventLinkLost += PlcLostConnectHandler;
            if (this.comboBoxPlcObjList.Text == "PLC控件")
            {
                plcRwIF = plcRwObj;

            }
            else if (this.comboBoxPlcObjList.Text == "重新实现MC协议")
            {
                plcRwIF = plcRwObj2;

            }
           
     
            string[] ports = System.IO.Ports.SerialPort.GetPortNames();
        
            IniTaskTypeList();
            IniMultiTaskList();
            InitFillInfoUI();
            this.tabControl1.TabPages["tabPage4"].Parent = null;

            if (!ECAMWCS.SimMode)
            {
             //   this.buttonClearDevCmd.Visible = false;
                this.groupBoxCtlSim.Visible = false;
            }

          
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            
           
        }
        #region 初始化，启停
        private void buttonInit_Click(object sender, EventArgs e)
        {
            string reStr = "";
            ctlManager.WCSInit(ref reStr);
            AddLog(reStr);
          
            //this.tabControl1.Enabled = true;
            tabPage1.Enabled = true;
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
           
            string reStr = "";
            ctlManager.WCSStart(ref reStr);
            AddLog(reStr);
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            
            string reStr = "";
            ctlManager.WCSStop(ref reStr);
            AddLog(reStr);
        }

        private void buttonClearLog_Click(object sender, EventArgs e)
        {
            this.richTextBoxLog.Clear();
        }
        #endregion
        
        #region 数据表
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshDt(comboBoxDtName.Text);
            //if (comboBoxDtName.Text == "控制任务表")
            //{
            //    this.buttonDtDelete.Visible = true;
            //    this.buttonDtModify.Visible = true;
            //}
            //else
            //{
            //    this.buttonDtDelete.Visible = false;
            //    this.buttonDtModify.Visible = false;
            //}
        }
        private void buttonDtRefresh_Click(object sender, EventArgs e)
        {
            RefreshDt(comboBoxDtName.Text);
        }
        private void RefreshDt(string dtName)
        {
            DataSet ds = null;
            switch (dtName)
            {
                case "设备表":
                    {
                        ds = devBll.GetAllList();
                        
                        break;
                    }
                case "控制接口表":
                    {
                        ds = ctlTaskIFBll.GetAllList();
                        break;
                    }
                case "控制任务表":
                    {
                        ds = ctlTaskBll.GetAllList();
                        
                        break;
                    }
                case "料框数据表":
                    {
                        ds = palletBll.GetAllList();
                        break;
                    }
                case "电芯数据表":
                    {
                        ds = batteryBll.GetAllList();
                        break;
                    }
                case "管理任务表":
                    {
                        ds = manTaskBll.GetAllList();
                        break;
                    }
                case "国轩批次信息表":
                    {
                        ds = gxBatchBll.GetAllList();
                        break;
                    }
                case "国轩托盘信息表":
                    {
                        ds = gxTrayBll.GetAllList();
                        break;
                    }
                case "国轩电芯信息表":
                    {
                        ds = gxBatteryBll.GetAllList();
                        break;
                    }
                case "OCV读卡交互数据表":
                    {
                        ds = ocvRfidReadBll.GetAllList();
                        break;
                    }
                default:
                    break;

            }
            if (ds != null && ds.Tables.Count > 0)
            {
                this.dataGridViewDt.DataSource = ds.Tables[0];
            }
        }
        private void DeleteRow()
        {
            if (comboBoxDtName.Text == "控制任务表")
            {
                DataGridViewSelectedRowCollection rws = this.dataGridViewDt.SelectedRows;
                if (rws != null && rws.Count > 0)
                {

                    foreach (DataGridViewRow rw in rws)
                    {
                        if (rw != null)
                        {
                            int id = int.Parse(rw.Cells["ControlID"].Value.ToString());
                            ctlTaskBll.Delete(id);
                        }
                    }
                }
                RefreshDt("控制任务表");

            }
        }
        private void buttonDtDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DeleteRow();
            }
            catch (System.Exception ex)
            {
                AddLog(ex.Message + ";" + ex.StackTrace);
            }
        }
        private void ModifyRow()
        {
            if (comboBoxDtName.Text == "控制任务表")
            {
                 DataGridViewSelectedRowCollection rws = this.dataGridViewDt.SelectedRows;
                 if (rws != null && rws.Count > 0)
                 {
                     foreach (DataGridViewRow rw in rws)
                     {
                         if (rw != null)
                         {
                             int id = int.Parse(rw.Cells["ControlID"].Value.ToString());
                             ControlTaskModel taskModel = ctlTaskBll.GetModel(id);
                             if (taskModel == null)
                             {
                                 continue;
                             }
                             taskModel.TaskPhase = rw.Cells["TaskPhase"].Value.ToString();
                             taskModel.TaskStatus = rw.Cells["TaskStatus"].Value.ToString();
                         }
                     }
                 }
             }
        }
        private void buttonDtModify_Click(object sender, EventArgs e)
        {
            try
            {
                ModifyRow();
            }
            catch (System.Exception ex)
            {
                AddLog(ex.Message + ";" + ex.StackTrace);
            }
        }
        #endregion
        #region 设备监控
        private void RefreshDev()
        {
            string devID = this.comboBoxDevList.Text;
            ECAMSDevBase dev = ctlManager.GetDev(devID);
            if (dev == null)
            {
                MessageBox.Show("设备号不存在");
                return;
            }
            DataTable dt1 = null;
            DataTable dt2 = null;
            DataTable dtTask = null;
            if (!ctlManager.GetDevRunningInfo(devID, ref dtTask, ref dt1, ref dt2))
            {
                MessageBox.Show("刷新设备信息失败");
                return;
            }
            this.dataGridViewDevDB1.DataSource = dt1;
            for (int i = 0; i < this.dataGridViewDevDB1.Columns.Count; i++)
            {
                this.dataGridViewDevDB1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                this.dataGridViewDevDB1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
            this.dataGridViewDevDB1.Columns[this.dataGridViewDevDB1.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewDevDB2.DataSource = dt2;
            for (int i = 0; i < this.dataGridViewDevDB2.Columns.Count; i++)
            {
                this.dataGridViewDevDB2.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                this.dataGridViewDevDB2.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
            this.dataGridViewDevDB2.Columns[this.dataGridViewDevDB2.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewCurrentTask.DataSource = dtTask;
            for (int i = 0; i < this.dataGridViewCurrentTask.Columns.Count; i++)
            {
                this.dataGridViewCurrentTask.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                this.dataGridViewCurrentTask.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
            this.dataGridViewCurrentTask.Columns[this.dataGridViewCurrentTask.ColumnCount - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void ClearDevCmd()
        {
            string devID = this.comboBoxDevList.Text;
            ECAMSDevBase dev = ctlManager.GetDev(devID);
            if (dev.ClearDevCmd())
            {
                ctlManager.AddLog(dev.devName + " 发送命令已清空",EnumLogType.提示);
                AddLog(dev.devName + " 发送命令已清空");
            }
            else
            {
                ctlManager.AddLog(dev.devName + " 发送命清空失败 ", EnumLogType.提示);
            }
            if (dev.ClearRunningTask())
            {
                ctlManager.AddLog(dev.devName + " 已清空当前任务", EnumLogType.提示);
            }
            else
            {
                ctlManager.AddLog(dev.devName + " 清空当前任务失败",EnumLogType.提示);
            }
        }

        private void buttonRefreshDevStatus_Click(object sender, EventArgs e)
        {
            RefreshDev();  
        }
        private void buttonClearDevCmd_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes== PopAskBox("提示", "确实要复位设备命令数据吗？"))
            {
                ClearDevCmd();
            }
            
        }
        #endregion  
        #region PLC 通信测试
        private void buttonConnectPlc_Click(object sender, EventArgs e)
        {
            string PlcIP = this.textBoxPlcIP.Text;//ConfigurationManager.AppSettings["plcIP"];
            string PlcPort =this.textBoxPlcPort.Text;// ConfigurationManager.AppSettings["plcPort"];
            string plcAddr = PlcIP + ":" + PlcPort;
            string reStr = "";
            
            if (plcRwIF.ConnectPLC(plcAddr, ref reStr))
            {
                this.buttonClosePlc.Enabled = true;
                this.buttonReadPlc.Enabled = true;
                this.buttonWritePlc.Enabled = true;
            }
            AddLog(reStr);
            //this.timer1.Start();
            timer3.Start();
        }
        private void buttonClosePlc_Click(object sender, EventArgs e)
        {
            
            if (plcRwIF.CloseConnect())
            {
                this.buttonClosePlc.Enabled = false;
                this.buttonReadPlc.Enabled = false;
                this.buttonWritePlc.Enabled = false;
                this.buttonConnectPlc.Enabled = true;
            }
           
           
            AddLog("PLC 连接已关闭!");
        }
        /// <summary>
        /// PLC 断开连接的事件响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlcLostConnectHandler(object sender, EventArgs e)
        {
            AddLog("PLC 通信连接断开，正在重连...");
            string PlcIP = ConfigurationManager.AppSettings["plcIP"];
            string PlcPort = ConfigurationManager.AppSettings["plcPort"];
            string plcAddr = PlcIP + ":" + PlcPort;
            string reStr = "";
            if (plcRwIF.ConnectPLC(plcAddr, ref reStr))
            {
                AddLog("PLC重连成功!");
            }
            else
            {
                AddLog("PLC重连失败!");
            }

        }
        private void buttonReadPlc_Click(object sender, EventArgs e)
        {

            string addr = this.textBoxPlcAddr.Text;
            int val = 0;
            if (!plcRwIF.ReadDB(addr, ref val))
            {
                AddLog("PLC 读取地址：" + addr + "失败");

            }
            else
            {
                this.textBoxPlcVal.Text = val.ToString();
            }

        }

        private void buttonWritePlc_Click(object sender, EventArgs e)
        {
            string addr = this.textBoxPlcAddr.Text;
            int val = int.Parse(this.textBoxPlcVal.Text);
            if (!plcRwIF.WriteDB(addr, val))
            {
                AddLog("PLC 写入地址：" + addr + "失败");
            }
            else
            {
                AddLog("PLC 写入地址：" + addr + "成功");
            }
        }

        private void buttonMultiWritePlc_Click(object sender, EventArgs e)
        {
            string addrStart = this.textBoxPlcAddrStart.Text;
            int blockNum = int.Parse(this.textBoxPlcBlockNum.Text);
            string[] splitStr = new string[] { ",", ":", "-", ";" };
            string strVals = this.richTextBoxMultiDBVal.Text;
            string[] strArray = strVals.Split(splitStr, StringSplitOptions.RemoveEmptyEntries);
            if (strArray == null || strArray.Count()<1)
            {
                MessageBox.Show("输入数据错误");
                return;
            }
            short[] vals = new short[strArray.Count()];
            for (int i = 0; i < vals.Count(); i++)
            {
                vals[i] = short.Parse(strArray[i]);
            }
            if (plcRwIF.WriteMultiDB(addrStart, blockNum, vals))
            {
                AddLog("批量写入成功");
            }
            else
            {
                AddLog("批量写入失败");
            }
        }
        private void buttonPLCDBReset_Click(object sender, EventArgs e)
        {
            try
            {
                string addrStart = this.textBoxPlcAddrStart.Text;
                int blockNum = int.Parse(this.textBoxPlcBlockNum.Text);
                short[] vals = new short[blockNum];
                Array.Clear(vals, 0, vals.Count());
                if (plcRwIF.WriteMultiDB(addrStart, blockNum, vals))
                {
                    AddLog("复位成功");
                }
                else
                {
                    AddLog("复位失败");
                }
            }
            catch (System.Exception ex)
            {
                AddLog("复位异常");
            }
           
        }
        private void buttonMultiReadPlc_Click(object sender, EventArgs e)
        {
            string addrStart = this.textBoxPlcAddrStart.Text;
            int blockNum = int.Parse(this.textBoxPlcBlockNum.Text);
            short[] reVals = null;
            if (plcRwIF.ReadMultiDB(addrStart, blockNum, ref reVals))
            {
                string strVal = "";
                for (int i = 0; i < blockNum; i++)
                {
                    strVal += reVals[i].ToString() + ",";
                }
                this.richTextBoxMultiDBVal.Text = strVal;
            }
            else
            {
                AddLog("批量读取PLC数据失败");
            }

        }
        private void buttonGetCode_Click(object sender, EventArgs e)
        {
            string[] blockStrs = ECAMWCS.SplitStrings(this.textBoxCode.Text);
            if (blockStrs == null || blockStrs.Count() < 7)
            {
                MessageBox.Show("寄存器数据少于7个，请重新输入");
                return;
            }
            byte[] idBytes = new byte[12];
            byte[] byteArray = new byte[14];
            for (int j = 0; j < 7; j++)
            {

                int val = int.Parse(blockStrs[j]);
                byteArray[2 * j] = (byte)(val & 0xff);
                byteArray[2 * j + 1] = (byte)((val >> 8) & 0xff);
               
            }
            Array.Copy(byteArray, 1, idBytes, 0, 12);
            string batteryID = System.Text.Encoding.UTF8.GetString(idBytes);
            this.textBoxCodeVal.Text = batteryID;
            AddLog("二维码转换成功");
        }
      
        private void PlcTimerProc2(object sender, ElapsedEventArgs e)
        {
            lock (plcAutoReadLock)
            {
                int val = 0;
                //plcRwObj.ReadDB("D3000", ref val);
            }
          
        }
        private void comboBoxPlcObjList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBoxPlcObjList.Text == "PLC控件")
            {
               // plcRwIF = plcRwObj;

            }
            else if (this.comboBoxPlcObjList.Text == "重新实现MC协议")
            {
                plcRwIF = plcRwObj2;

            }
        }
        #endregion   
        #region 仿真模拟

        private void buttonDB2Reset_Click(object sender, EventArgs e)
        {
            try
            {
                DB2SimReset();
            }
            catch (System.Exception ex)
            {
                AddLog(ex.Message + "," + ex.StackTrace);
            }

        }

        private void buttonDB2SimSet_Click(object sender, EventArgs e)
        {
            DB2SimSet();
            RefreshDev();
        }
        private void DB2SimSet()
        {

            if(!ECAMWCS.SimMode)
            {
                MessageBox.Show("当前不处于仿真模式");
                return;
            }
            string devID = this.comboBoxDevList.Text;
            ECAMSDevBase dev = ctlManager.GetDev(devID);
            if (dev == null)
            {
                MessageBox.Show("设备号不存在");
                return;
            }
            int itemID = 1;
            switch (comboBoxDB2Items.Text)
            {
                case "故障码":
                    {
                        itemID = 1;
                        dev.SetDB2ItemValue(itemID, this.textBoxDB2ItemVal.Text);
                        break;
                    }
                case "设备状态":
                    {
                        itemID = 2;
                        dev.SetDB2ItemValue(itemID, this.textBoxDB2ItemVal.Text);
                        break;
                    }
                case "允许接收":
                    {
                        itemID = 3;
                        dev.SetDB2ItemValue(itemID, this.textBoxDB2ItemVal.Text);
                        break;
                    }
                case "取数据完成":
                    {
                        itemID = 4;
                        dev.SetDB2ItemValue(itemID, this.textBoxDB2ItemVal.Text);
                        break;
                    }
                case "任务完成":
                    {
                        
                        byte sigVal = byte.Parse(this.textBoxDB2ItemVal.Text);
                        if (sigVal == 2 && dev.CurrentTask != null)
                        {
                            //任务完成，写参数
                            itemID = 7;
                            //返回参数
                            string[] splitStr = new string[] { ",", ";", ":", "-", "|" };
                            string[] backParamStrs = ECAMWCS.SplitStrings(this.textBoxTaskReParam.Text);
                            if (dev.DevModel.DeviceID == "5001")
                            {
                                for (int i = 0; i < 48; i++)
                                {
                                    string batteryID = "EA14E03000" + (i + 1).ToString().PadLeft(2, '0');
                                    byte[] bytesID = System.Text.Encoding.UTF8.GetBytes(batteryID);

                                    byte[] bytesIDAll = new byte[14];
                                    bytesIDAll[0] = 0; //
                                    Array.Copy(bytesID, 0, bytesIDAll, 1, bytesID.Count());
                                    //bytesIDAll[bytesID.Count() + 1] = 0x0d;
                                    //bytesIDAll[bytesID.Count() + 2] = 0x0a;
                                    for (int j = 0; j < 7; j++)
                                    {
                                        int blockVal = bytesIDAll[2 * j] + (bytesIDAll[2 * j + 1] << 8);
                                        dev.SetDB2ItemValue(itemID, blockVal.ToString());
                                        itemID++;
                                    }

                                }
                            }
                            //itemID = dev.BackParamIDStart;
                            //if (backParamStrs != null)
                            //{
                            //    for (int i = 0; i < Math.Min(dev.BackParamNum, backParamStrs.Count()); i++)
                            //    {
                            //        dev.SetDB2ItemValue(itemID, backParamStrs[i]);
                            //        itemID++;
                            //    }
                            //}
                        }

                        itemID = 5;
                        dev.SetDB2ItemValue(itemID, this.textBoxDB2ItemVal.Text);
                        break;
                    }
                case "入口状态":
                    {
                        itemID = 1;
                        dev.SetDB2ItemValue(itemID, this.textBoxDB2ItemVal.Text);
                        break;
                    }
                case "入口料框数量":
                    {
                        itemID = 2;
                        dev.SetDB2ItemValue(itemID, this.textBoxDB2ItemVal.Text);
                        break;
                    }
                case "扫码请求":
                    {
                        itemID = 3;
                        dev.SetDB2ItemValue(itemID, this.textBoxDB2ItemVal.Text);
                        break;
                    }
                case "读卡信息成功接收":
                    {
                        itemID = 4;
                        dev.SetDB2ItemValue(itemID, this.textBoxDB2ItemVal.Text);
                        break;
                    }
                case "出口状态":
                    {
                        itemID = 1;
                        dev.SetDB2ItemValue(itemID, this.textBoxDB2ItemVal.Text);
                        break;
                    }
                  
                case "装箱口状态":
                    {
                        itemID = 6;
                        dev.SetDB2ItemValue(itemID, this.textBoxDB2ItemVal.Text);
                        break;
                    }
                case "分拣口状态":
                    {
                        itemID = 6;
                        dev.SetDB2ItemValue(itemID, this.textBoxDB2ItemVal.Text);
                        break;
                    }
                case "1号读卡结果":
                    {

                        (ECAMWCS.rfidRWDic[1] as rfidRWSim).simReadRfid = "TP"+this.textBoxDB2ItemVal.Text.PadLeft(7, '0');
                        break;
                    }     
                case "2号读卡结果":
                    {
                        (ECAMWCS.rfidRWDic[2] as rfidRWSim).simReadRfid ="TP"+ this.textBoxDB2ItemVal.Text.PadLeft(7, '0');
                        break;
                    }
                case "3号读卡结果":
                    {
                        (ECAMWCS.rfidRWDic[3] as rfidRWSim).simReadRfid = "TP" + this.textBoxDB2ItemVal.Text.PadLeft(7, '0');
                        break;
                    }
                case "4号读卡结果":
                    {
                        (ECAMWCS.rfidRWDic[4] as rfidRWSim).simReadRfid = "TP" + this.textBoxDB2ItemVal.Text.PadLeft(7, '0');
                        break;
                    }
                case "5号读卡结果":
                    {
                        (ECAMWCS.rfidRWDic[5] as rfidRWSim).simReadRfid = "TP" + this.textBoxDB2ItemVal.Text.PadLeft(7, '0');
                        break;
                    }
                case "6号读卡结果":
                    {
                        (ECAMWCS.rfidRWDic[6] as rfidRWSim).simReadRfid = "TP" + this.textBoxDB2ItemVal.Text.PadLeft(7, '0');
                        break;
                    }
                case "7号读卡结果":
                    {
                        (ECAMWCS.rfidRWDic[7] as rfidRWSim).simReadRfid = "TP" + this.textBoxDB2ItemVal.Text.PadLeft(7, '0');
                        break;
                    }
                case "8号读卡结果":
                    {
                        (ECAMWCS.rfidRWDic[8] as rfidRWSim).simReadRfid = "TP" + this.textBoxDB2ItemVal.Text.PadLeft(7, '0');
                        break;
                    }
                
                default:
                    break;
            }

        }
        private void buttonDB2Reset_Click_1(object sender, EventArgs e)
        {
            try
            {
                DB2SimReset();
            }
            catch (System.Exception ex)
            {
                AddLog(ex.Message + "," + ex.StackTrace);
            }
        }
        private void DB2SimReset()
        {
            
            string devID = this.comboBoxDevList.Text;
            ECAMSDevBase dev = ctlManager.GetDev(devID);
            if (dev == null)
            {
                MessageBox.Show("设备号不存在");
                return;
            }
            int itemID = 1;
            for (int i = 0; i < dev.DicCommuDataDB2.Count(); i++)
            {
                dev.SetDB2ItemValue(itemID, "1");
                itemID++;
            }
       
            dev.SetDB2ItemValue(2, "1");
            RefreshDev();

        }

        private void comboBoxDevList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string devID = this.comboBoxDevList.Text;
            ECAMSDevBase dev = ctlManager.GetDev(devID);
            if (dev == null)
            {
                MessageBox.Show("设备号不存在");
                return;
            }
            if (dev.DevModel.DeviceType == EnumDevType.站台.ToString())
            {
                this.comboBoxDB2Items.Items.Clear();
                this.comboBoxDB2Items.Items.Add("入口状态");
                this.comboBoxDB2Items.Items.Add("出口状态");
                this.comboBoxDB2Items.Items.Add("扫码请求");
                this.comboBoxDB2Items.Items.Add("入口料框数量");
                this.comboBoxDB2Items.Items.Add("读卡信息成功接收");
                //if (dev.DevModel.DeviceID == "2002" || dev.DevModel.DeviceID == "2004" || dev.DevModel.DeviceID == "2006")
                //{

                //}
            }
            else
            {
               
                this.comboBoxDB2Items.Items.Clear();
                this.comboBoxDB2Items.Items.Add("就绪状态");
                this.comboBoxDB2Items.Items.Add("工作状态");
                this.comboBoxDB2Items.Items.Add("故障码");
                this.comboBoxDB2Items.Items.Add("允许接收");
                this.comboBoxDB2Items.Items.Add("取数据完成");
                this.comboBoxDB2Items.Items.Add("任务完成");
                this.comboBoxDB2Items.Items.Add("装箱口状态");
                this.comboBoxDB2Items.Items.Add("分拣口状态");
                this.comboBoxDB2Items.Items.Add("1号读卡结果");
                this.comboBoxDB2Items.Items.Add("2号读卡结果");
                this.comboBoxDB2Items.Items.Add("3号读卡结果");
                this.comboBoxDB2Items.Items.Add("4号读卡结果");
                this.comboBoxDB2Items.Items.Add("5号读卡结果");
                this.comboBoxDB2Items.Items.Add("6号读卡结果");
                this.comboBoxDB2Items.Items.Add("7号读卡结果");
                this.comboBoxDB2Items.Items.Add("8号读卡结果");

            }
        }
        #endregion

        #region 强制出库
        private void bt_ForceCreate_Click(object sender, EventArgs e)
        {
            if(this.cb_TaskType.Text == "")
            {
               MessageBox.Show("请选择任务类型", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (this.cb_TaskType.Text == EnumTaskName.电芯出库_A1.ToString() || this.cb_TaskType.Text == EnumTaskName.分容出库_A1.ToString()
                || this.cb_TaskType.Text == EnumTaskName.电芯出库_B1.ToString() || this.cb_TaskType.Text == EnumTaskName.空料框出库.ToString())//A库
            {
                if (!CheckGsName(this.tb_OutGoodsSiteName.Text.Trim()))
                {
                    MessageBox.Show("输入的货位位置格式不正确！！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

           
            ForceCreateTask(this.cb_TaskType.Text.Trim());
        }
        private void cb_OutStorageRegular_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cb_OutStorageRegular.Text == "按排出库")
            {
                this.lb_row.Enabled = true;
                this.tb_RowthNum.Enabled = true;
                this.lb_Column.Enabled = false;
                this.tb_ColumnNum.Enabled = false;
                this.lb_layer.Enabled = false;
                this.tb_LayerthNum.Enabled = false;
                this.lb_Attation.Text = "请输入要出库的排数！";
            }
            else if (this.cb_OutStorageRegular.Text == "按列出库")
            {
                this.lb_row.Enabled = true;
                this.tb_RowthNum.Enabled = true;
                this.lb_Column.Enabled = true;
                this.tb_ColumnNum.Enabled = true;
                this.lb_layer.Enabled = false;
                this.tb_LayerthNum.Enabled = false;
                this.lb_Attation.Text = "请输入要出库的第几排、第几列！";
            }
            else if (this.cb_OutStorageRegular.Text == "按层出库")
            {
                this.lb_row.Enabled = true;
                this.tb_RowthNum.Enabled = true;
                this.lb_Column.Enabled = false;
                this.tb_ColumnNum.Enabled = false;
                this.lb_layer.Enabled = true;
                this.tb_LayerthNum.Enabled = true;
                this.lb_Attation.Text = "请输入要出库的第几排、第几层！";
            }
        }
        private void bt_CreateMultiTask_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("您确定要强制生成多货位出库任务吗！", "信息提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
            {
                CreateMultiCtrlTask();
            }
        }
        private Int64 CreateManaTask(string taskName,string goodsSiteName)
        {
            ManageTaskModel manaTaskModel = new ManageTaskModel();
            manaTaskModel.TaskCode = ctlTaskIFBll.GetNewTaskCode();
            manaTaskModel.TaskCreatePerson = "admin";
            manaTaskModel.TaskCreateTime = DateTime.Now;
            if (taskName == EnumTaskName.电芯出库_A1.ToString())
            {
                manaTaskModel.TaskEndArea = "一次检测区";
                manaTaskModel.TaskEndPostion = "2005";
                manaTaskModel.TaskStartArea = "老化区A1";
                manaTaskModel.TaskStartPostion = goodsSiteName;
                manaTaskModel.TaskStatus = EnumTaskStatus.待执行.ToString();
                manaTaskModel.TaskType = EnumTaskCategory.出库.ToString();
                manaTaskModel.TaskTypeName = taskName;
            }
            else if (taskName == EnumTaskName.分容出库_A1.ToString())
            {
                manaTaskModel.TaskEndArea = "A1库分容出口区";
                manaTaskModel.TaskEndPostion = "2003";
                manaTaskModel.TaskStartArea = "老化区A1";
                manaTaskModel.TaskStartPostion = goodsSiteName;
                manaTaskModel.TaskStatus = EnumTaskStatus.待执行.ToString();
                manaTaskModel.TaskType = EnumTaskCategory.出库.ToString();
                manaTaskModel.TaskTypeName = taskName;
            }
            else if (taskName == EnumTaskName.电芯出库_B1.ToString())
            {
                manaTaskModel.TaskEndArea = "二次检测区";
                manaTaskModel.TaskEndPostion = "2007";
                manaTaskModel.TaskStartArea = "B1库静置区";
                manaTaskModel.TaskStartPostion = goodsSiteName;
                manaTaskModel.TaskStatus = EnumTaskStatus.待执行.ToString();
                manaTaskModel.TaskType = EnumTaskCategory.出库.ToString();
                manaTaskModel.TaskTypeName = taskName;
            }
            else if (taskName == EnumTaskName.空料框出库.ToString())
            {
                manaTaskModel.TaskEndArea = "B1库静置区";
                manaTaskModel.TaskEndPostion = "2009";
                manaTaskModel.TaskStartArea = "装箱区";
                manaTaskModel.TaskStartPostion = goodsSiteName;
                manaTaskModel.TaskStatus = EnumTaskStatus.待执行.ToString();
                manaTaskModel.TaskType = EnumTaskCategory.出库.ToString();
                manaTaskModel.TaskTypeName = taskName;

            }
            else if (taskName == EnumTaskName.空料框直接返线.ToString())
            {
                manaTaskModel.TaskEndArea = "装箱区";
                manaTaskModel.TaskEndPostion = "2011";
                manaTaskModel.TaskStartArea = "二次检测区";
                manaTaskModel.TaskStartPostion = "5003";
                manaTaskModel.TaskStatus = EnumTaskStatus.待执行.ToString();
                manaTaskModel.TaskType = EnumTaskCategory.出库.ToString();
                manaTaskModel.TaskTypeName = taskName;
            }
            else if (taskName == EnumTaskName.电芯装箱组盘.ToString())
            {
                manaTaskModel.TaskEndArea = "装箱区";
                manaTaskModel.TaskEndPostion = "2001";
                manaTaskModel.TaskStartArea = "装箱组盘区";
                manaTaskModel.TaskStartPostion = "5001";
                manaTaskModel.TaskStatus = EnumTaskStatus.待执行.ToString();
                manaTaskModel.TaskType = EnumTaskCategory.出库.ToString();
                manaTaskModel.TaskTypeName = taskName;

            }
            else if (taskName == EnumTaskName.电芯一次拣选.ToString())
            {
                manaTaskModel.TaskEndArea = "不良品拣选区";
                manaTaskModel.TaskEndPostion = "5002";
                manaTaskModel.TaskStartArea = "一次检测区";
                manaTaskModel.TaskStartPostion = "4001";
                manaTaskModel.TaskStatus = EnumTaskStatus.待执行.ToString();
                manaTaskModel.TaskType = EnumTaskCategory.出库.ToString();
                manaTaskModel.TaskTypeName = taskName;
            }
            else if (taskName == EnumTaskName.电芯二次拣选.ToString())
            {
                manaTaskModel.TaskEndArea = "拣选区";
                manaTaskModel.TaskEndPostion = "5003";
                manaTaskModel.TaskStartArea = "二次检测区";
                manaTaskModel.TaskStartPostion = "4002";
                manaTaskModel.TaskStatus = EnumTaskStatus.待执行.ToString();
                manaTaskModel.TaskType = EnumTaskCategory.出库.ToString();
                manaTaskModel.TaskTypeName = taskName;
            }
            Int64 manaTaskID = manTaskBll.Add(manaTaskModel);
            return manaTaskID;
        }

        private bool CreateControlTask(Int64 manaTaskID, string taskName,string startDevice)
        {
            ControlTaskModel task = new ControlTaskModel();
            task.TaskID = manaTaskID;
            task.TaskTypeName = taskName;
            task.ControlCode = ctlTaskIFBll.GetNewTaskCode();//.textBoxControlCode.Text;
            task.CreateTime = System.DateTime.Now;
            task.TaskStatus = EnumTaskStatus.待执行.ToString();
            task.CreateMode = EnumCreateMode.手动强制.ToString();
           
            task.TaskPhase = "0";
            if (taskName == EnumTaskName.电芯出库_A1.ToString())
            {
                task.StartArea = "老化区A1";
                task.TargetArea = "一次检测区";
                task.StartDevice = startDevice;
                task.TargetDevice = "2005";
                task.TaskType = "出库";
                task.TaskTypeCode = 6;
            }
            else if (taskName == EnumTaskName.分容出库_A1.ToString())
            {
                task.StartArea = "老化区A1";
                task.TargetArea = "老化区";
                task.StartDevice = startDevice;
                task.TargetDevice = "2003";
                task.TaskType = "出库";
                task.TaskTypeCode = 4;
            }
            else if (taskName == EnumTaskName.电芯出库_B1.ToString())
            {
                task.StartArea = "静置区B1";
                task.TargetArea = "二次检测区";
                task.StartDevice = startDevice;
                task.TargetDevice = "2007";
                task.TaskType = "出库";
                task.TaskTypeCode = 9;
            }
            else if (taskName == EnumTaskName.空料框出库.ToString())
            {
                task.StartArea = "静置区B1";
                task.TargetArea = "B1库出口";
                task.StartDevice = startDevice;
                task.TargetDevice = "2009";
                task.TaskType = "出库";
                task.TaskTypeCode = 2;
            }
            else if (taskName == EnumTaskName.空料框直接返线.ToString())
            {
                task.StartArea = "二次拣选区";
                task.TargetArea = "B1库出口";
                task.StartDevice = "5003";
                task.TargetDevice = "2011";
                task.TaskType = "出库";
                task.TaskTypeCode = 19;
            }
            else if (taskName == EnumTaskName.电芯装箱组盘.ToString())
            {
                task.StartArea = "电芯装箱组盘区";
                task.TargetArea = "老化区";
                task.StartDevice = "5001";
                task.TargetDevice = "2001";
                task.TaskType = "入库";
                task.TaskTypeCode = 11;
            }
            else if (taskName == EnumTaskName.电芯一次拣选.ToString())
            {
                task.StartArea = "老化区A1";
                task.TargetArea = "一次检测区";
                task.StartDevice = "4001";
                task.TargetDevice = "5002";
                task.TaskType = "出库";
                task.TaskTypeCode = 7;
            }
            else if (taskName == EnumTaskName.电芯二次拣选.ToString())
            {
                task.StartArea = "二次检测区";
                task.TargetArea = "拣选区";
                task.StartDevice = "4002";
                task.TargetDevice = "5003";
                task.TaskType = "出库";
                task.TaskTypeCode = 10;
            }
          
            try
            {
                if (ctlTaskBll.Add(task) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (System.Exception ex)
            {
                AddLog("出现异常：" + ex.Message);
                return false;
            }
        }

        private void ForceCreateTask(string taskName)
        {
            string[] gsPosition = this.tb_OutGoodsSiteName.Text.Split('-');

            EnumStoreHouse storeHouseName = EnumStoreHouse.A1库房;
            if (this.cb_TaskType.Text == EnumTaskName.电芯出库_A1.ToString() || this.cb_TaskType.Text == EnumTaskName.分容出库_A1.ToString())//A库
            {
                storeHouseName = EnumStoreHouse.A1库房;
                CreateOutHouseTask(storeHouseName, taskName, int.Parse(gsPosition[0]), int.Parse(gsPosition[1]), int.Parse(gsPosition[2]));
            }

            else if (this.cb_TaskType.Text == EnumTaskName.电芯出库_B1.ToString() || this.cb_TaskType.Text == EnumTaskName.空料框出库.ToString())//B库
            {
                storeHouseName = EnumStoreHouse.B1库房;
                CreateOutHouseTask(storeHouseName, taskName, int.Parse(gsPosition[0]), int.Parse(gsPosition[1]), int.Parse(gsPosition[2]));
            }
            else
            {
                Int64 manaTaskID = CreateManaTask(taskName, this.tb_OutGoodsSiteName.Text.Trim());
                if (CreateControlTask(manaTaskID, taskName, this.tb_OutGoodsSiteName.Text.Trim()))
                {
                    string logStr = "实用工具，强制出库，增加强制出库任务：" + taskName + "," + this.tb_OutGoodsSiteName.Text;
                    AddLog(logStr);
                    AddLog(logStr, EnumLogType.提示, EnumLogCategory.管理层日志);
                }
            }
            
        }

        private void CreateOutHouseTask(EnumStoreHouse storeHouseName, string taskName,int rowth, int columnth, int layerth)
        {
            GoodsSiteModel gsModel = bllGoodsSite.GetGoodsSite(storeHouseName, rowth, columnth, layerth);
            if (gsModel != null)
            {
                if (MessageBox.Show("您确定要强制生成任务吗！", "信息提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
                {
                    Int64 manaTaskID = CreateManaTask(taskName, this.tb_OutGoodsSiteName.Text.Trim());
                    if (CreateControlTask(manaTaskID, taskName, this.tb_OutGoodsSiteName.Text.Trim()))
                    {
                        string trayIDListStr = GetTrayIDListByGSID(gsModel.GoodsSiteID);
                    
                        //AddLog("增加出库任务：" + taskName + "," + this.tb_OutGoodsSiteName.Text);
                        AddLog("实用工具,强制出库，增加出库任务：" + taskName + "," + this.tb_OutGoodsSiteName.Text+"托盘号：" +trayIDListStr, EnumLogType.提示, EnumLogCategory.管理层日志);
                    }
                }

            }
            else
            {
                MessageBox.Show("不存在此货位！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void IniMultiTaskList()
        {
            this.cb_MultiTaskType.Items.Clear();
            this.cb_MultiTaskType.Items.Add(EnumTaskName.电芯出库_A1.ToString());
            this.cb_MultiTaskType.Items.Add(EnumTaskName.电芯出库_B1.ToString());
            this.cb_MultiTaskType.Items.Add(EnumTaskName.分容出库_A1.ToString());
            this.cb_MultiTaskType.Items.Add(EnumTaskName.空料框出库.ToString());
        }

        private void IniTaskTypeList()
        {
            this.cb_TaskType.Items.Clear();
            this.cb_TaskType.Items.Add(EnumTaskName.电芯出库_A1.ToString());
            this.cb_TaskType.Items.Add(EnumTaskName.电芯出库_B1.ToString());
            this.cb_TaskType.Items.Add(EnumTaskName.分容出库_A1.ToString());
            this.cb_TaskType.Items.Add(EnumTaskName.空料框出库.ToString());
            this.cb_TaskType.Items.Add(EnumTaskName.空料框直接返线.ToString());
            this.cb_TaskType.Items.Add(EnumTaskName.电芯二次拣选.ToString());
            this.cb_TaskType.Items.Add(EnumTaskName.电芯一次拣选.ToString());
            this.cb_TaskType.Items.Add(EnumTaskName.电芯装箱组盘.ToString());
        }

        private bool CheckGsName(string txt)
        {
            string gsName =txt;
            string opertions = @"^(\d{1,2}-\d{1,2}-\d{1,2})$";
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(opertions);
            if (!regex.IsMatch(gsName))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private bool CheckIntNum(string intStr)
        {
            string opertions = @"^(\d{1,2})$";
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(opertions);
            if (!regex.IsMatch(intStr))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private string GetTrayIDListByGSID(int goodsSiteID)
        {
            string trayIDListStr = "";
            StockModel stockModel = bllStock.GetModelByGsID(goodsSiteID);
            if (stockModel != null)
            {
                StockListModel stockListModel = bllStockList.GetModelByStockID(stockModel.StockID);
                if (stockListModel != null)
                {
                    trayIDListStr = bllStockDetail.GetTrayIDStrList(stockListModel.StockListID);
                }
            }
            return trayIDListStr; 
        }

        private void CreateMultiCtrlTask()
        {
            if (this.cb_MultiTaskType.Text == "")
            {
                MessageBox.Show("请选择出库任务类型！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int storageAreaID = 2;
            if (this.cb_MultiTaskType.Text == EnumTaskName.电芯出库_A1.ToString())//A库
            {
                storageAreaID = 2;
            }
            else if (this.cb_MultiTaskType.Text == EnumTaskName.分容出库_A1.ToString())//A库
            {
                storageAreaID = 2;
            }
            else if (this.cb_MultiTaskType.Text == EnumTaskName.电芯出库_B1.ToString())//B库
            {
                storageAreaID = 4;
            }
            else if (this.cb_MultiTaskType.Text == EnumTaskName.空料框出库.ToString())//B库
            {
                storageAreaID = 4;
            }
            if (this.cb_OutStorageRegular.Text == "按排出库")
            {
                if (CheckIntNum(this.tb_RowthNum.Text.Trim()))
                {
                    int rowth = int.Parse(this.tb_RowthNum.Text.Trim());
                    List<GoodsSiteModel> gsList = bllGoodsSite.GetRowGs(rowth, storageAreaID);
                    if (gsList != null)
                    {
                        for (int i = 0; i < gsList.Count; i++)
                        {
                            Int64 manaTaskID = CreateManaTask(this.cb_MultiTaskType.Text, gsList[i].DeviceID);
                            if (CreateControlTask(manaTaskID, this.cb_MultiTaskType.Text, gsList[i].DeviceID))
                            {
                                string trayIDListStr = GetTrayIDListByGSID(gsList[i].GoodsSiteID);
                                //AddLog("增加出库任务：" + this.cb_MultiTaskType.Text + "," + gsList[i].DeviceID);
                                AddLog("实用工具，强制出库，增加出库任务：" + this.cb_MultiTaskType.Text + "," + gsList[i].DeviceID + "托盘号：" + trayIDListStr, EnumLogType.提示, EnumLogCategory.管理层日志);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("您输入的排数数据有误,请重新输入！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("您输入的排数数据有误,请重新输入！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (this.cb_OutStorageRegular.Text == "按列出库")
            {
                if (CheckIntNum(this.tb_RowthNum.Text.Trim()) && CheckIntNum(this.tb_ColumnNum.Text.Trim()))
                {
                    int rowth = int.Parse(this.tb_RowthNum.Text.Trim());
                    int columnth = int.Parse(this.tb_ColumnNum.Text.Trim());
                    List<GoodsSiteModel> gsList = bllGoodsSite.GetColumnGs(rowth, columnth, storageAreaID);
                    if (gsList != null)
                    {
                        for (int i = 0; i < gsList.Count; i++)
                        {
                            Int64 manaTaskID = CreateManaTask(this.cb_MultiTaskType.Text, gsList[i].DeviceID);
                            if (CreateControlTask(manaTaskID, this.cb_MultiTaskType.Text, gsList[i].DeviceID))
                            {
                                string trayIDListStr = GetTrayIDListByGSID(gsList[i].GoodsSiteID);
                                //AddLog("增加出库任务：" + this.cb_MultiTaskType.Text + "," + gsList[i].DeviceID);
                                AddLog("实用工具，增加出库任务：" + this.cb_MultiTaskType.Text + "," + gsList[i].DeviceID + "托盘号：" + trayIDListStr, EnumLogType.提示, EnumLogCategory.管理层日志);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("您输入的排数或列数数据有误,请重新输入！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("您输入的排数或列数数据有误,请重新输入！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (this.cb_OutStorageRegular.Text == "按层出库")
            {
                if (CheckIntNum(this.tb_RowthNum.Text.Trim()) && CheckIntNum(this.tb_LayerthNum.Text.Trim()))
                {
                    int rowth = int.Parse(this.tb_RowthNum.Text.Trim());
                    int layerth = int.Parse(this.tb_LayerthNum.Text.Trim());
                    List<GoodsSiteModel> gsList = bllGoodsSite.GetLayerGs(rowth, layerth, storageAreaID);
                    if (gsList != null)
                    {
                        for (int i = 0; i < gsList.Count; i++)
                        {
                            Int64 manaTaskID = CreateManaTask(this.cb_MultiTaskType.Text, gsList[i].DeviceID);
                            if (CreateControlTask(manaTaskID, this.cb_MultiTaskType.Text, gsList[i].DeviceID))
                            {
                                string trayIDListStr = GetTrayIDListByGSID(gsList[i].GoodsSiteID);
                                //AddLog("增加出库任务：" + this.cb_MultiTaskType.Text + "," + gsList[i].DeviceID);
                                AddLog("实用工具，增加出库任务：" + this.cb_MultiTaskType.Text + "," + gsList[i].DeviceID + "托盘号：" + trayIDListStr, EnumLogType.提示, EnumLogCategory.管理层日志);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("您输入的排数或层数数据有误,请重新输入！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("您输入的排数或层数数据有误！请重新输入！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        #endregion
        #region 补录装载信息
        private OCVPalletBll ocvPalletBll = new OCVPalletBll();
        private OCVBatteryBll ocvBatteryBll = new OCVBatteryBll();
        private TB_Tray_indexBll gxTrayBll = new TB_Tray_indexBll();
        private TB_Batch_IndexBll gxBatchBll = new TB_Batch_IndexBll();
        private TB_After_GradeDataBll gxBatteryBll = new TB_After_GradeDataBll();
        private void InitFillInfoUI()
        {
            this.comboBoxBatch_BL.Items.Clear();
            List<TB_Batch_IndexModel> batchList = gxBatchBll.GetModelList(" ");
            if (batchList == null || batchList.Count < 1)
            {
                return;
            }
            foreach (TB_Batch_IndexModel batch in batchList)
            {
                if (batch != null)
                {
                    this.comboBoxBatch_BL.Items.Add(batch.Tf_BatchID);
                }
                
            }
        }

        private void buttonExistCheck_Click(object sender, EventArgs e)
        {
            string dbSet = this.comboBoxDBset_BL.Text;
            if (string.IsNullOrWhiteSpace(dbSet))
            {
                MessageBox.Show("未设定入库类型");
                return;
            }
            if (string.IsNullOrWhiteSpace(this.textBoxTrayID_BL.Text))
            {
                MessageBox.Show("托盘id为空,请重新输入");
                return;
            }
            string palletID = this.textBoxTrayID_BL.Text.ToUpper().Trim();
            if (dbSet == "初入库")
            {
                if (palletBll.Exists(palletID))
                {
                    this.labelTipinfo_BL.Text = "该托盘装载信息已经存在";
                }
                else
                {
                    this.labelTipinfo_BL.Text = "该托盘装载信息不存在，可以补录";
                }
            }
            else if (dbSet == "二次分容入库")
            {
                if (gxTrayBll.Exists(palletID))
                {
                    this.labelTipinfo_BL.Text = "该托盘装载信息已经存在";
                }
                else
                {
                    this.labelTipinfo_BL.Text = "该托盘装载信息不存在，可以补录";
                }
            }
        }
        private void GetFillInfo(string palletID)
        {
            string dbSet = this.comboBoxDBset_BL.Text;
            if (string.IsNullOrWhiteSpace(dbSet))
            {
                MessageBox.Show("未设定入库类型");
                return;
            }
            if (dbSet == "初入库")
            {
                OCVPalletModel palletModel = ocvPalletBll.GetModel(palletID);
                if (palletModel == null)
                {

                    MessageBox.Show("提示：本地信息库里未找到该托盘号信息");
                    return;
                }
                this.labelTipinfo_BL.Text = "";
                this.comboBoxBatch_BL.Text = palletModel.batchID;
                this.textBoxCurrentBatch_BL.Text = palletModel.batchID;
                this.dataGridViewBatterys_BL.Rows.Clear();
                for (int i = 0; i < 4; i++)
                {
                    //if (i <3)
                    {
                        this.dataGridViewBatterys_BL.Rows.Add();
                    }
                    this.dataGridViewBatterys_BL.RowCount = this.dataGridViewBatterys_BL.Rows.Count;
                    DataGridViewRow dr = this.dataGridViewBatterys_BL.Rows[i];

                    //DataRow dr = dt.NewRow();
                    for (int j = 0; j < 12; j++)
                    {
                        dr.Cells[j].Value = "";

                    }
                }
                string strWhere = "palletID = '" + palletID + "'";
                // List<TB_After_GradeDataModel> batteryList = gxBatteryBll.GetModelList(strWhere);
                List<OCVBatteryModel> batteryList = ocvBatteryBll.GetModelList(strWhere);
                
                for (int i = 0; i < batteryList.Count(); i++)
                {
                    //TB_After_GradeDataModel battery = batteryList[i];
                    OCVBatteryModel battery = batteryList[i];
                    if (battery == null)
                    {
                        continue;
                    }
                    int rowIndex = (int)battery.rowIndex - 1;// (int)(battery.Tf_ChannelNo - 1) / 12;
                    int cowIndex = (int)battery.columnIndex - 1;// (int)battery.Tf_ChannelNo - rowIndex * 12 - 1;
                    DataGridViewRow dr = this.dataGridViewBatterys_BL.Rows[rowIndex];
                    dr.Cells[cowIndex].Value = battery.batteryID;// battery.Tf_CellSn;

                }
            }
            else if (dbSet == "二次分容入库")
            {
                TB_Tray_indexModel trayModel= gxTrayBll.GetModel(palletID);
                if (trayModel == null)
                {
                    MessageBox.Show("提示：国轩托盘信息库里未找到该托盘号信息");
                    return;
                }
                this.labelTipinfo_BL.Text = "";
                this.comboBoxBatch_BL.Text = trayModel.Tf_BatchID;
                this.textBoxCurrentBatch_BL.Text = trayModel.Tf_BatchID;
                if (this.dataGridViewBatterys_BL.Rows.Count > 0)
                {
                    this.dataGridViewBatterys_BL.Rows.Clear();
                }
               
                for (int i = 0; i < 4; i++)
                {
                    if (i < 3)
                    {
                        this.dataGridViewBatterys_BL.Rows.Add();
                    }
                    this.dataGridViewBatterys_BL.RowCount = this.dataGridViewBatterys_BL.Rows.Count;
                    DataGridViewRow dr = this.dataGridViewBatterys_BL.Rows[i];

                    //DataRow dr = dt.NewRow();
                    for (int j = 0; j < 12; j++)
                    {
                        dr.Cells[j].Value = "";

                    }
                }

                string strWhere = " Tf_TrayId = '" + palletID + "' and Tf_BatchID='" + trayModel.Tf_BatchID+"' ";
                List<TB_After_GradeDataModel> batteryList = gxBatteryBll.GetModelList(strWhere);
                for (int i = 0; i < batteryList.Count(); i++)
                {
                    TB_After_GradeDataModel battery = batteryList[i];
                    if (battery == null)
                    {
                        continue;
                    }
                    int rowIndex =  (int)(battery.Tf_ChannelNo - 1) / 12;
                    int cowIndex = (int)battery.Tf_ChannelNo - rowIndex * 12 - 1;
                    DataGridViewRow dr = this.dataGridViewBatterys_BL.Rows[rowIndex];
                    dr.Cells[cowIndex].Value =  battery.Tf_CellSn;

                }
            }
            else
            {
                MessageBox.Show("入库类型设置错误");
                return;
            }
   
       
            this.dataGridViewBatterys_BL.BeginEdit(false);
            this.dataGridViewBatterys_BL.CurrentCell = null;
           
        }
        private void buttonGetFillInfo_BL_Click(object sender, EventArgs e)
        {
            try
            {
                GetFillInfo(this.textBoxTrayID_BL.Text);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("异常：" + ex.Message);
            }
            
        }

        private void buttonAddFillinfo_BL_Click(object sender, EventArgs e)
        {
            ExeAddFillInfo();
        }
        private void ExeAddFillInfo()
        {
            if (string.IsNullOrWhiteSpace(this.textBoxTrayID_BL.Text))
            {
                MessageBox.Show("托盘id为空,请重新输入");
                return;
            }
            if (this.textBoxTrayID_BL.Text.Length != 9 || this.textBoxTrayID_BL.Text.Substring(0, 2) != "TP")
            {
                MessageBox.Show("托盘条码输入错误，检查是否以TP开始，后面7位数字");
                return;
            }
            int a = 0;
            if (!int.TryParse(this.textBoxTrayID_BL.Text.Substring(2,7),out a))
            {
                MessageBox.Show("托盘条码输入错误，检查是否以TP开始，后面7位数字");
                return;
            }
            string palletID = this.textBoxTrayID_BL.Text.ToUpper().Trim();
           
            string batchID = this.comboBoxBatch_BL.Text;
            if(string.IsNullOrWhiteSpace(batchID))
            {
                MessageBox.Show("请选择批次信息");
                return;
            }
            string dbSet = this.comboBoxDBset_BL.Text;
            if (string.IsNullOrWhiteSpace(dbSet))
            {
                MessageBox.Show("未设定入库类型");
                return;
            }
            try
            {
                List<string> batterList = new List<string>();
                string[] batteryIDS = new string[48];
                int index = 0;
                for (int i = 0; i < Math.Min(4, this.dataGridViewBatterys_BL.RowCount); i++)
                {
                    DataGridViewRow rw = this.dataGridViewBatterys_BL.Rows[i];
                    if (rw == null)
                    {
                        index += 12;
                        continue;
                    }
                    for (int j = 0; j < 12; j++)
                    {
                        if (rw.Cells[j].Value == null)
                        {
                            batteryIDS[index] = string.Empty;
                        }
                        else
                        {
                            batteryIDS[index] = rw.Cells[j].Value.ToString().Trim();
                            //由12位条码改为13位，modify by zwx,2015-07-22
                            if (batteryIDS[index].Length > 13)
                            {
                                batteryIDS[index] = batteryIDS[index].Substring(0, 13);
                            }
                        }
                        index++;
                    }
                }
                string reStr = "";
                if (dbSet == "初入库")
                {
                    //if (gxTrayBll.Exists(palletID))
                    //{
                    //    MessageBox.Show("该托盘装载信息已经存在");
                    //    return;
                    //}
                    if (RecordFillInfoToLocal(palletID, batchID, batteryIDS, ref reStr))
                    {
                        this.textBoxCurrentBatch_BL.Text = batchID;
                        MessageBox.Show("装载信息成功补录!");
                        this.dataGridViewBatterys_BL.Rows.Clear();
                        this.comboBoxDBset_BL.Text = "";
                        palletTraceBll.AddHistoryEvent(palletID, EnumOCVProcessStatus.A1库老化.ToString(), "手动装载，等待首次入A1库", MainPresenter.userNameStr);
                    }
                    else
                    {
                        MessageBox.Show("装载信息成功失败," + reStr, "录入错误");
                    }
                }
                else if (dbSet == "二次分容入库")
                {
                    //先解绑
                    TrayUninstall(palletID,ref reStr);
                    RecordFillInfoToLocal(palletID, batchID, batteryIDS, ref reStr);
                    bool batchNumEnable = false;
                    if (this.checkBoxBatchNumEnable_BL.Checked)
                    {
                        batchNumEnable = true;
                    }
                    else
                    {
                        batchNumEnable = false;
                    }
                    if (UploadFillInfoToGX(palletID, batchID, batteryIDS, batchNumEnable,ref reStr))
                    {
                        this.textBoxCurrentBatch_BL.Text = batchID;
                        MessageBox.Show("装载信息成功补录!");
                        this.dataGridViewBatterys_BL.Rows.Clear();
                        this.comboBoxDBset_BL.Text = "";
                        palletTraceBll.AddHistoryEvent(palletID, EnumOCVProcessStatus.A1库老化.ToString(), "手动装载，等待二次分容入A1库", MainPresenter.userNameStr);
                    }
                    else
                    {
                        MessageBox.Show("装载信息成功失败," + reStr, "录入错误");
                    }
                }
                else
                {
                    MessageBox.Show("入库类型设置错误");
                    return;
                }
               
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("补录装载信息出现异常：" + ex.Message + "," + ex.StackTrace);
            }
            
        }

        private void buttonModifyFillinfo_Click(object sender, EventArgs e)
        {

        }
        private void buttonClear_BL_Click(object sender, EventArgs e)
        {
            this.dataGridViewBatterys_BL.AllowUserToAddRows = true;
            this.dataGridViewBatterys_BL.Rows.Clear();
            
        }
        /// <summary>
        /// 记录到本地
        /// </summary>
        /// <returns></returns>
        private bool RecordFillInfoToLocal(string palletID, string batchID, string[] batteryIDs,ref string reStr)
        {
            //  1、	查询批次信息表(TB_Batch_Index)得到批次编号和批次类型
            // 2、	插入托盘信息到托盘信息表(TB_Tray_index)中
            // 3、	更新批次信息表(TB_Batch_Index)中托盘数量和电池总数量
            // 4、	插入电池信息到电池信息表(TB_After_GradeData)
            reStr = "";
            
            try
            {
         

                TB_Batch_IndexModel batchModel = gxBatchBll.GetModel(batchID);
                if (batchModel == null)
                {
                    reStr = "批次：" + batchID + "不存在";
                    return false;
                }
               
               
                //1 检查托盘信息
                if (ocvPalletBll.Exists(palletID))
                {
                    reStr = "托盘：" + palletID + "已经存在";
                    return false;
                }
               
                //2 坚持查电芯数据是否合格
                for (int i = 0; i < batteryIDs.Count()-1; i++)
                {
                    int row1 = i / 12 + 1;
                    int col1 = i - (row1 - 1) * 12 + 1;
                    if(string.IsNullOrWhiteSpace(batteryIDs[i]))
                    {
                        continue;
                    }
                    if (batteryIDs[i].Length < 12) //电芯条码升级为13位，,2015-07-23
                    {
                        reStr = "输入电芯条码错误：第" + (i + 1).ToString() + "个电芯条码不足12位";
                        return false;
                    }
                    for (int j = i + 1; j < batteryIDs.Count(); j++)
                    {
                        int row2 = j / 12 + 1;
                        int col2 = j - (row2 - 1) * 12 + 1;
                        if (batteryIDs[i] == batteryIDs[j])
                        {

                            reStr = "输入电芯条码有重复：第[" + row1.ToString() + "行,"+col1.ToString()+"列]和[" + row2.ToString() + "行,"+col2.ToString()+"列]电芯条码有重复";
                            return false;
                        }
                    }
                }
                //只录入本地数据库，在A1库入库口处再调出数据上传到国轩数据库
                //TB_Tray_indexModel trayModel = new TB_Tray_indexModel();
                //trayModel.Tf_BatchID = batchID;
                //trayModel.Tf_Batchtype = batchModel.Tf_Batchtype;
                //trayModel.Tf_CellCount = 0;
                //trayModel.Tf_TrayId = palletID;
                //trayModel.tf_CheckInTime = System.DateTime.Now;
                //gxTrayBll.Add(trayModel);
                //3插入电池信息
                //int batteryCount = 0;
                OCVPalletModel palletModel = new OCVPalletModel();
                palletModel.palletID = palletID;
                palletModel.batchID = batchID;
                palletModel.loadInTime = System.DateTime.Now;
                palletModel.processStatus = EnumOCVProcessStatus.A1库老化.ToString();
                ocvPalletBll.Add(palletModel);

                for (int i = 0; i < batteryIDs.Count(); i++)
                {
                    if (string.IsNullOrWhiteSpace(batteryIDs[i]) || batteryIDs[i].Length < 12) //电芯条码升级为13位，modify by zwx,2015-07-23
                    {
                        continue;
                    }
                    //TB_After_GradeDataModel batteryModel = new TB_After_GradeDataModel();
                    //batteryModel.Tf_BatchID = batchID;
                    //batteryModel.Tf_Batchtype = batchModel.Tf_Batchtype;
                    //batteryModel.Tf_TrayId = palletID;
                    //batteryModel.Tf_ChannelNo = (i + 1);
                    //batteryModel.Tf_CellSn = batteryIDs[i];
                    //if (gxBatteryBll.Add(batteryModel))
                    //{
                    //    batteryCount++;
                    //}

                    OCVBatteryModel battery = new OCVBatteryModel();
                    battery.batteryID = batteryIDs[i];
                    battery.checkResult = "良品"; //初始值假设为合格品
                    battery.rowIndex = i / 12 + 1;
                    battery.columnIndex = i - (battery.rowIndex - 1) * 12 + 1;
                    battery.hasBattery = true;
                    battery.palletID = palletID;
                    battery.positionCode = (i + 1);
                    ocvBatteryBll.Add(battery);

                }
                //trayModel.Tf_CellCount = batteryCount;
                //if (!gxTrayBll.Update(trayModel))
                //{
                //    reStr = "更新托盘数据表出现错误";
                //    ocvPalletBll.Delete(palletID);
                //    string strWhere = "Tf_TrayId='" + palletID + "' and Tf_BatchID='" + batchID + "' ";
                //    List<TB_After_GradeDataModel> batteryList = gxBatteryBll.GetModelList(strWhere);
                //    foreach (TB_After_GradeDataModel battery in batteryList)
                //    {
                //        if (battery == null)
                //        {
                //            continue;
                //        }
                //        gxBatteryBll.Delete(battery.Tf_BatchID, battery.Tf_TrayId, battery.Tf_CellSn);
                //    }
                //    gxTrayBll.Delete(palletID);
                //    return false;
                //}

                ////2 更新批次信息
                //batchModel.Tf_TrayCount++;
                //batchModel.Tf_CellCount += trayModel.Tf_CellCount;
                //gxBatchBll.Update(batchModel);
                return true;
            }
            catch (System.Exception ex)
            {
                ocvPalletBll.Delete(palletID);
                //string strWhere = "Tf_TrayId='"+palletID+"' and Tf_BatchID='"+batchID+"' ";
                //List<TB_After_GradeDataModel> batteryList = gxBatteryBll.GetModelList(strWhere);
                //foreach (TB_After_GradeDataModel battery in batteryList)
                //{
                //    if (battery == null)
                //    {
                //        continue;
                //    }
                //    gxBatteryBll.Delete(battery.Tf_BatchID, battery.Tf_TrayId, battery.Tf_CellSn);
                //}
                //gxTrayBll.Delete(palletID);
                MessageBox.Show("录入数据库出现异常，可能有重复的电芯条码，请检查,"+ex.Message);
                return false;
            }
          

        }

        /// <summary>
        /// 上传电芯数据到客户的数据库系统
        /// </summary>
        /// <param name="palletID"></param>
        /// <param name="batchID"></param>
        /// <param name="batteryIDs"></param>
        /// <param name="reStr"></param>
        /// <returns></returns>
        private bool UploadFillInfoToGX(string palletID, string batchID, string[] batteryIDs,bool batchNumEnable, ref string reStr)
        {
            //  1、	查询批次信息表(TB_Batch_Index)得到批次编号和批次类型
            // 2、	插入托盘信息到托盘信息表(TB_Tray_index)中
            // 3、	更新批次信息表(TB_Batch_Index)中托盘数量和电池总数量
            // 4、	插入电池信息到电池信息表(TB_After_GradeData)
            reStr = "";

            try
            {
                TB_Batch_IndexModel batchModel = gxBatchBll.GetModel(batchID);
                if (batchModel == null)
                {
                    reStr = "批次：" + batchID + "不存在";
                    return false;
                }
                //1 检查托盘信息
                if (gxTrayBll.Exists(palletID))
                {
                    reStr = "托盘：" + palletID + "已经存在";
                    return false;
                }

                //2 坚持查电芯数据是否合格
                for (int i = 0; i < batteryIDs.Count() - 1; i++)
                {
                    int row1 = i / 12 + 1;
                    int col1 = i - (row1 - 1) * 12 + 1;
                    if (string.IsNullOrWhiteSpace(batteryIDs[i]))
                    {
                        continue;
                    }
                    if (batteryIDs[i].Length < 12) //modify by zwx,2015-07-23
                    {
                        reStr = "输入电芯条码错误：第" + (i + 1).ToString() + "个电芯条码不足12位";
                        return false;
                    }
                    for (int j = i + 1; j < batteryIDs.Count(); j++)
                    {
                        int row2 = j / 12 + 1;
                        int col2 = j - (row2 - 1) * 12 + 1;
                        if (batteryIDs[i] == batteryIDs[j])
                        {

                            reStr = "输入电芯条码有重复：第[" + row1.ToString() + "行," + col1.ToString() + "列]和[" + row2.ToString() + "行," + col2.ToString() + "列]电芯条码有重复";
                            return false;
                        }
                    }
                }
                
                TB_Tray_indexModel trayModel = new TB_Tray_indexModel();
                trayModel.Tf_BatchID = batchID;
                trayModel.Tf_Batchtype = batchModel.Tf_Batchtype;
                trayModel.Tf_CellCount = 0;
                trayModel.Tf_TrayId = palletID;
                trayModel.tf_CheckInTime = System.DateTime.Now;
                gxTrayBll.Add(trayModel);
                //3插入电池信息

                int batteryCount = 0;
                for (int i = 0; i < batteryIDs.Count(); i++)
                {
                    if (string.IsNullOrWhiteSpace(batteryIDs[i]) || batteryIDs[i].Length < 12) //modify by zwx,2015-07-23
                    {
                        continue;
                    }
                    //若已存在电芯，更新其绑定的托盘号
                    List<TB_After_GradeDataModel> batteryList = gxBatteryBll.GetModelList("Tf_CellSn = '" + batteryIDs[i] + "' and Tf_BatchID='" + batchID+"' ");// modify by zwx,2015-12-13
                    if (batteryList != null && batteryList.Count > 0)
                    {
                        TB_After_GradeDataModel battery = batteryList[0];
                        if (battery != null)
                        {
                            string oldTrayID = battery.Tf_TrayId;
                            battery.Tf_TrayId = palletID;
                            battery.Tf_BatchID = batchID;
                            battery.Tf_ChannelNo = (i + 1);
                            battery.Tf_Pick = null;
                            battery.Tf_Tag = 0; 
                            gxBatteryBll.Update(battery, oldTrayID);
                            batteryCount++;
                        }
                    }
                    else
                    {
                        TB_After_GradeDataModel batteryModel = new TB_After_GradeDataModel();
                        batteryModel.Tf_BatchID = batchID;
                        batteryModel.Tf_Batchtype = batchModel.Tf_Batchtype;
                        batteryModel.Tf_TrayId = palletID;
                        batteryModel.Tf_ChannelNo = (i + 1);
                        batteryModel.Tf_CellSn = batteryIDs[i];
                        if (gxBatteryBll.Add(batteryModel))
                        {
                            batteryCount++;
                        }
                    }
                   
                }
                trayModel.Tf_CellCount = batteryCount;
                if (!gxTrayBll.Update(trayModel))
                {
                    reStr = "更新托盘数据表出现错误";
                    ocvPalletBll.Delete(palletID);
                    string strWhere = "Tf_TrayId='" + palletID + "' and Tf_BatchID='" + batchID + "' ";
                    List<TB_After_GradeDataModel> batteryList = gxBatteryBll.GetModelList(strWhere);
                    foreach (TB_After_GradeDataModel battery in batteryList)
                    {
                        if (battery == null)
                        {
                            continue;
                        }
                        gxBatteryBll.Delete(battery.Tf_BatchID, battery.Tf_TrayId, battery.Tf_CellSn);
                    }
                    gxTrayBll.Delete(palletID);
                    return false;
                }

                //2 更新批次信息
                batchModel.Tf_TrayCount++;
                if (batchNumEnable)
                {
                    batchModel.Tf_CellCount += trayModel.Tf_CellCount;
                }
               
                gxBatchBll.Update(batchModel);
                return true;
            }
            catch (System.Exception ex)
            {

                string strWhere = "Tf_TrayId='" + palletID + "' and Tf_BatchID='" + batchID + "' ";
                List<TB_After_GradeDataModel> batteryList = gxBatteryBll.GetModelList(strWhere);
                foreach (TB_After_GradeDataModel battery in batteryList)
                {
                    if (battery == null)
                    {
                        continue;
                    }
                    gxBatteryBll.Delete(battery.Tf_BatchID, battery.Tf_TrayId, battery.Tf_CellSn);
                }
                gxTrayBll.Delete(palletID);
                MessageBox.Show("录入数据库出现异常，可能有重复的电芯条码，请检查," + ex.Message);
                return false;
            }


        }
        private void dataGridViewBatterys_BL_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData) //激活回车键
        {
            if (this.dataGridViewBatterys_BL.Rows.Count>4)
            {
                 this.dataGridViewBatterys_BL.AllowUserToAddRows = false;
                
            }
            if (keyData == Keys.Enter)    //监听回车事件   
            {

                if (this.dataGridViewBatterys_BL.IsCurrentCellInEditMode)
                {
                    if (this.dataGridViewBatterys_BL.CurrentRow.Index<3 && this.dataGridViewBatterys_BL.SelectedCells[0].ColumnIndex == 11)
                    {
                        int rowIndex = this.dataGridViewBatterys_BL.CurrentRow.Index + 1;
                        if(rowIndex>=this.dataGridViewBatterys_BL.RowCount)
                        {
                            this.dataGridViewBatterys_BL.Rows.Add();
                            
                        }
                        if (rowIndex >= this.dataGridViewBatterys_BL.RowCount)
                        {
                            return false;
                        }
                        this.dataGridViewBatterys_BL.CurrentCell = this.dataGridViewBatterys_BL[0,rowIndex ];
                        this.dataGridViewBatterys_BL.CurrentCell.Selected = true;
                    }
                    else
                    {
                       // SendKeys.Send("{Up}");
                        SendKeys.Send("{Tab}");
                        this.dataGridViewBatterys_BL.BeginEdit(true);
                        return true;
                    }

                }
                
            }
           
           return base.ProcessCmdKey(ref msg, keyData);
            
        }
        private void dataGridViewBatterys_BL_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                SendKeys.Send("{Tab}");
                this.dataGridViewBatterys_BL.BeginEdit(true);
                e.Handled = true;
            }
        }
        private void dataGridViewBatterys_BL_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            this.dataGridViewBatterys_BL.BeginEdit(true);
            this.dataGridViewBatterys_BL.CurrentCell = this.dataGridViewBatterys_BL.Rows[e.RowIndex].Cells[e.ColumnIndex];
            //this.dataGridViewBatterys_BL.EditMode = DataGridViewEditMode.EditProgrammatically;//.EditOnKeystroke;//.EditOnEnter;

            //this.dataGridViewBatterys_BL.CurrentCell



        }

        private void comboBoxDBset_BL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBoxDBset_BL.Text == "二次分容入库")
            {
                this.checkBoxBatchNumEnable_BL.Visible = true;
            }
            else
            {
                this.checkBoxBatchNumEnable_BL.Visible = false;
            }
        }

        private void buttonMultiLoad_BL_Click(object sender, EventArgs e)
        {
            MultiLoad_BL();
        }
        private void MultiLoad_BL()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "txt files(*.txt)|*.txt|All files (*.*)|*.*";
            dlg.FilterIndex = 2;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string fName = dlg.FileName;
                if (!File.Exists(fName))
                {
                    MessageBox.Show("文件不存在：" + fName);
                    return;
                }
                FileStream fs = new FileStream(fName, FileMode.Open);
                StreamReader sr = new StreamReader(fs, Encoding.Default);
                string palletSource = null;
                int count = 0;
                do 
                {
                    palletSource = sr.ReadLine();
                
                
                string[] pallets = SplitStringArray(palletSource, new string[] { ",", "，", "-", ":", " ","\t" });
                if (pallets == null || pallets.Count() < 1)
                {
                    //AddLog("托盘信息为空！");
                    continue;
                }
               
                for (int i = 0; i < pallets.Count(); i++)
                {
                    string palletID = pallets[i].Trim();

                    if (string.IsNullOrWhiteSpace(palletID))
                    {
                        continue;
                    }
                    palletID = "TP" + palletID.PadLeft(7, '0');
                    if (ocvPalletBll.Exists(palletID))
                    {
                        AddLog("本地数据库已经存在装载托盘：" + palletID);
                        continue;
                    }
                    TB_Tray_indexModel trayModel = gxTrayBll.GetModel(palletID);
                    if (trayModel == null)
                    {
                        AddLog("错误提示：远程服务器信息库里未找到该托盘号信息:" + palletID);
                        continue;
                    }
                    string strWhere = " Tf_TrayId = '" + palletID + "'";
                    List<TB_After_GradeDataModel> batteryModels = gxBatteryBll.GetModelList(strWhere);
                    List<string> batterList = new List<string>();
                    string[] batteryIDS = new string[48];
                    foreach (TB_After_GradeDataModel batteryModel in batteryModels)
                    {
                        if (batteryModel == null)
                        {
                            continue;
                        }
                        int index = (int)batteryModel.Tf_ChannelNo - 1;
                        if (index < 0 || index > 47)
                        {
                            continue;
                        }
                        batteryIDS[index] = batteryModel.Tf_CellSn;
                    }
                    string reStr = "";
                    string batchID = trayModel.Tf_BatchID;
                    if (RecordFillInfoToLocal(palletID, batchID, batteryIDS, ref reStr))
                    {
                        this.textBoxCurrentBatch_BL.Text = batchID;
                        AddLog("装载信息成功补录:" + palletID);
                        this.dataGridViewBatterys_BL.Rows.Clear();
                        this.comboBoxDBset_BL.Text = "";
                        count++;
                    }
                    else
                    {
                        AddLog("装载信息成功失败," + reStr);
                    }
                }
              } while (palletSource != null);
                AddLog("总计录入：" + count.ToString() + "筐电芯数据");
            }
        }
        #endregion
        #region 托盘解绑
        private void buttonTrayUninstall_Click(object sender, EventArgs e)
        {
            string palletID = this.textBoxTrayID_BL.Text.ToUpper().Trim();
            if (string.IsNullOrWhiteSpace(palletID))
            {
                MessageBox.Show("托盘号为空!");
                return;
            }
            if (DialogResult.No == PopAskBox("提示", "确实要解绑该托盘：" + palletID + "吗？"))
            {
                return;
            }
            string reStr = "";
            TrayUninstall(palletID, ref reStr);
           
            MessageBox.Show(reStr);
            this.dataGridViewBatterys_BL.Rows.Clear();
            
        }
        /// <summary>
        /// 托盘解绑
        /// </summary>
        /// <param name="trayID"></param>
        private bool TrayUninstall(string  trayID,ref string reStr)
        {
            reStr = "解绑托盘"+trayID+"成功";
            //服务器注销托盘
            string strWhere = string.Format("Tf_TrayId='{0}' and tf_traystat=1", trayID);
            List<TB_Tray_indexModel> trayList = gxTrayBll.GetModelList(strWhere);
            if (trayList != null && trayList.Count > 0)
            {
                TB_Tray_indexModel tray = trayList[0];
                if (tray != null)
                {
                    tray.tf_traystat = 0;
                    if (!gxTrayBll.Update(tray))
                    {
                        reStr = "解绑"+trayID+"失败，更新国轩托盘信息表失败";
                        return false;
                    }
                }
            }
            //删除本地
            ocvPalletBll.Delete(trayID);
            return true;
        }
        #endregion
        #region 入库口缓存信息
        private void comboBoxPortin_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshPortinTrays();
        }

        private void buttonRefreshTrays_portin_Click(object sender, EventArgs e)
        {
            RefreshPortinTrays();
        }
        private void RefreshPortinTrays()
        {
            string portName = "";
            switch (this.comboBoxPortin.Text)
            {
                   
                case "A1库入口":
                 {
                     portName = "2002";
                     break;
                 }
                case "分容后入库口":
                {
                    portName = "2004";
                    break;
                }
                case "B1库入库口":
                {
                    portName = "2006";
                    break;
                }
                default:
                    {
                        MessageBox.Show("错误的入口选项");
                        return;
                        
                    }
                    
            }
            string palletIDs = "";
            foreach (string palletID in ECAMWCS.PalletInputDeque[portName])
            {
                palletIDs += palletID;
                palletIDs += ",";
            }
            this.richTextBoxPortIn.Text = palletIDs;

        }
        private void buttonSaveTrays_portin_Click(object sender, EventArgs e)
        {

            SaveTrays_portin();
        }
        private void SaveTrays_portin()
        {
            string portName = "";
            switch (this.comboBoxPortin.Text)
            {

                case "A1库入口":
                    {
                        portName = "2002";
                        break;
                    }
                case "分容后入库口":
                    {
                        portName = "2004";
                        break;
                    }
                case "B1库入库口":
                    {
                        portName = "2006";
                        break;
                    }
                default:
                    {
                        MessageBox.Show("错误的入口选项");
                        return;

                    }

            }
            string trays = this.richTextBoxPortIn.Text;
            if (string.IsNullOrWhiteSpace(trays))
            {
                
                ECAMWCS.PalletInputDeque[portName].Clear();
                ctlManager.SavePalletInputQueue();
                MessageBox.Show("入库口托盘信息已保存！");
                return;
            }
            string[] trayList = SplitStringArray(trays, new string[] { "-", ",", ":" });
            //if (portName == "2006")
            //{
            //    if (trayList.Count() > 6)
            //    {
            //        MessageBox.Show("该入口缓存托盘最多允许6个，请重新输入");
            //        return;
            //    }
                
            //}
            //else
            //{
            //    if (trayList.Count() > 2)
            //    {
            //        MessageBox.Show("该入口缓存托盘最多允许2个，请重新输入");
            //        return;
            //    }
            //}
            for (int i = 0; i < trayList.Count(); i++)
            {
               // if (!gxTrayBll.Exists(trayList[i]))
                trayList[i] = trayList[i].ToUpper();
                if (!ocvPalletBll.Exists(trayList[i]))
                {
                    MessageBox.Show("数据库中不存在托盘：" + trayList[i]);
                    return;
                }
                ECAMWCS.PalletInputDeque[portName].Enqueue(trayList[i]);
            }
            ECAMWCS.PalletInputDeque[portName].Clear();
            for (int i = 0; i < trayList.Count(); i++)
            {
                
                ECAMWCS.PalletInputDeque[portName].Enqueue(trayList[i]);
            }
            ctlManager.SavePalletInputQueue();
            MessageBox.Show("入库口托盘信息已保存！");
        }
        private void buttonClearTrays_portin_Click(object sender, EventArgs e)
        {
            ClearTrays_portin();
        }
        void ClearTrays_portin()
        {
            string portName = "";
            switch (this.comboBoxPortin.Text)
            {

                case "A1库入口":
                    {
                        portName = "2002";
                        break;
                    }
                case "分容后入库口":
                    {
                        portName = "2004";
                        break;
                    }
                case "B1库入库口":
                    {
                        portName = "2006";
                        break;
                    }
                default:
                    {
                        MessageBox.Show("错误的入口选项");
                        return;

                    }

            }
            DialogResult re = MessageBox.Show("确定要清除入库口的缓存数据吗?", "提示", MessageBoxButtons.YesNo);
            if (re == DialogResult.No)
            {
                return;
            }
            ECAMWCS.PalletInputDeque[portName].Clear();
            ctlManager.SavePalletInputQueue();

        }
        #endregion
        #region 读卡记录
        private void buttonQuery_RDRecord_Click(object sender, EventArgs e)
        {
            RDRecordQuery();
        }
        private void RDRecordQuery()
        {
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append(" ");
            int conditionNum = 0;
            if (this.checkBoxPalletSet_RDRecord.Checked)
            {
                if(string.IsNullOrWhiteSpace(this.textBoxPallet_RDRecord.Text))
                {
                    MessageBox.Show("输入的托盘信息为空,请重新输入!");
                    return;
                }
                strWhere.Append(" readingContent='" + this.textBoxPallet_RDRecord.Text + "' ");
                conditionNum++;
            }
            if (this.checkBoxReaderSet_RDRecord.Checked)
            {
                int readerID = 0;
                if(string.IsNullOrWhiteSpace(this.comboBoxReader_RDRecord.Text))
                {
                    MessageBox.Show("读卡器未选定，请重新输入!");
                    return;
                }

                switch (this.comboBoxReader_RDRecord.Text)
                {
                    case "机械手1装载区":
                        {
                            readerID = 1;
                            break;
                        }
                    case "A1库入口区":
                        {
                            readerID = 2;
                            break;
                        }
                    case "A1库分容入口区":
                        {
                            readerID = 3;
                            break;
                        }
                    case "一次检测区":
                        {
                            readerID = 4;
                            break;
                        }
                    case "机械手2分拣区":
                        {
                            readerID = 5;
                            break;
                        }
                    case "B1库入口区":
                        {
                            readerID = 6;
                            break;
                        }
                    case "二次检测区":
                        {
                            readerID = 7;
                            break;
                        }
                    case "机械手3分拣区":
                        {
                            readerID = 8;
                            break;
                        }
                    default:
                        {
                            MessageBox.Show("无效的读卡设备设定");
                            return;
                        }
                }
                if (conditionNum > 0)
                {
                    strWhere.Append(" and rfidReaderID=" + readerID.ToString());
                }
                else
                {
                    strWhere.Append(" rfidReaderID=" + readerID.ToString());
                }
                conditionNum++;
            }
            if (this.checkBoxTimespanSet_RDRecord.Checked)
            {
                string startTimeStr = this.dateTimePickerStart_RDRecord.Value.ToString("yyyy-MM-dd 00:00:00");
                string endTimeStr = this.dateTimePickerEnd_RDRecord.Value.ToString("yyyy-MM-dd 23:59:59");
                if (conditionNum > 0)
                {
                    strWhere.Append(" and readingTime>= '" + startTimeStr + "' and readingTime<= '" + endTimeStr + "' ");
                }
                else
                {
                    strWhere.Append(" readingTime>= '" + startTimeStr + "' and readingTime<= '" + endTimeStr + "' ");
                }
            }
            DataSet ds = rfidRecordBll.GetList(strWhere.ToString());
            if (ds != null && ds.Tables[0] != null)
            {
                ds.Tables[0].Columns["readingSerialNo"].ColumnName = "流水号";
                ds.Tables[0].Columns["rfidReaderID"].ColumnName = "读卡器ID";
                ds.Tables[0].Columns["readerName"].ColumnName = "读卡器名称";
                ds.Tables[0].Columns["readingContent"].ColumnName = "读卡内容";
                ds.Tables[0].Columns["readingTime"].ColumnName = "读卡时间";
            }
            this.dataGridView_RDRecord.DataSource = ds.Tables[0];
            this.labelSum_RDRecord.Text = "合计：" + ds.Tables[0].Rows.Count.ToString();
            for (int i = 0; i < this.dataGridView_RDRecord.Columns.Count; i++)
            {
                this.dataGridView_RDRecord.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                this.dataGridView_RDRecord.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            this.dataGridView_RDRecord.Columns["读卡时间"].SortMode = DataGridViewColumnSortMode.Automatic;
            this.dataGridView_RDRecord.Columns["读卡时间"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
        }
        #endregion
        private void buttonQuitTest_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        public string[] SplitStringArray(string srcStrs, string[] splitStr)
        {
            if (srcStrs == null || srcStrs == string.Empty || splitStr == null || splitStr.Count() == 0)
            {
                return null;
            }
            return srcStrs.Split(splitStr, StringSplitOptions.RemoveEmptyEntries);
        }
        public DialogResult PopAskBox(string title, string content)
        {
            DialogResult re = MessageBox.Show(content,title, MessageBoxButtons.YesNo);
            return re;
        }
        #region 手动入库直接修改数据库
        private void bt_add_Click(object sender, EventArgs e)
        {
            if (this.cb_DBStoreHouse.Text == "")
            {
                MessageBox.Show("请选择库房！！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!CheckGsName(this.tb_DBGSPos.Text.Trim()))
            {
                MessageBox.Show("输入的货位位置格式不正确！！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (this.cb_DBProductStatus.Text == "")
            {
                MessageBox.Show("请选择物料状态！！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (this.tb_DBFrameCode.Text == "")
            {
                MessageBox.Show("请输入托盘条码！！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            bool isTrayIDExist = IsAllTrayIDExist(tb_DBFrameCode.Text);
            if (isTrayIDExist == false)
            {
                MessageBox.Show("输入的条码在数据库不存在请重新输入！！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //==============================生成库存首先删除原有库存
            if (PopAskBox("信息提示", "手动入库会删除原有库存信息，您确定要入库么？") == System.Windows.Forms.DialogResult.Yes)
            {
                EnumStoreHouse houseName = EnumStoreHouse.A1库房;
                string[] rcl = this.tb_DBGSPos.Text.Split('-');
                if (this.cb_DBStoreHouse.Text == EnumStoreHouse.A1库房.ToString())
                {
                    houseName = EnumStoreHouse.A1库房;
                }
                else if (this.cb_DBStoreHouse.Text == EnumStoreHouse.B1库房.ToString())
                {
                    houseName = EnumStoreHouse.B1库房;
                }
                //EnumStoreHouse houseName = (EnumStoreHouse)Enum.Parse(typeof(EnumStoreHouse), this.cb_DBStoreHouse.Text);
                if (rcl.Length == 3)
                {
                    string goodSiteName = rcl[0] + "排" + rcl[1] + "列" + rcl[2] + "层";
                    List<StockListModel> oldStockList = bllStockList.GetStockList (houseName, goodSiteName);
                    if (oldStockList != null && oldStockList.Count>0)
                    {
                        View_QueryStockListModel viewStockListModel = bllViewStock.GetModelByStockListID(oldStockList[0].StockListID);

                        if (viewStockListModel != null && viewStockListModel.GoodsSiteRunStatus != EnumGSRunStatus.任务完成.ToString())
                        {
                            MessageBox.Show("货位:"+goodSiteName+"处于锁定状态，不允许手动入库，请等待货位完成；若确定此货位一直处于锁定状态，处于异常情况可以到《货位状态》功能模块将此货位的状态，更改为无货，待用状态，然后再手动入库!", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        for (int i = 0; i < oldStockList.Count; i++)
                        {
                            bllStock.Delete(oldStockList[i].StockID);//已经做级联
                        }
                    }
                }

            }
            else
            {
                return;
            }
            //=========================================

            GoodsSiteModel gsModel = null;
            char[] splitStr = new char[1]{'-'};
            string[] RCL = this.tb_DBGSPos.Text.Trim().Split(splitStr);
            if (this.cb_DBStoreHouse.Text == EnumStoreHouse.A1库房.ToString())
            {
                gsModel = bllGoodsSite.GetGoodsSite(EnumStoreHouse.A1库房, int.Parse(RCL[0]), int.Parse(RCL[1]), int.Parse(RCL[2]));
            }
            else if(this.cb_DBStoreHouse.Text == EnumStoreHouse.B1库房.ToString()) 
            {
                gsModel = bllGoodsSite.GetGoodsSite(EnumStoreHouse.B1库房, int.Parse(RCL[0]), int.Parse(RCL[1]), int.Parse(RCL[2]));
            }
            //Hashtable updateGsStatusHs = bllGoodsSite.GetUpdateModelHs(EnumGSStoreStatus.有货.ToString()
            //               , EnumGSRunStatus.任务完成.ToString(), EnumTaskCategory.入库.ToString(), gsModel.GoodsSiteID);
            bllGoodsSite.UpdateGoodsSiteStatus(EnumGSStoreStatus.有货.ToString()
                           , EnumGSRunStatus.任务完成.ToString(), EnumTaskCategory.入库.ToString(), gsModel.GoodsSiteID);
            if (gsModel == null)
            {
                MessageBox.Show("您所输入的货位无效！！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
          
            //生成库存
            StockModel stockModel = new StockModel();
            stockModel.FullTraySign = EnumFullTraySign.是.ToString();
            stockModel.GoodsSiteID = gsModel.GoodsSiteID;
            stockModel.TrayCode = ctlTaskIFBll.GetNewTaskCode();//为料框编码
            long stockID = bllStock.Add(stockModel);

            long stockListID = 0;
            StockListModel stockListModel = CreateStockListModel(this.cb_DBStoreHouse.Text.Trim(), this.cb_DBProductStatus.Text.Trim()
                , stockModel.TrayCode, this.tb_DBFrameCode.Text.Trim(), gsModel, long.Parse(stockModel.TrayCode)
                , stockID,this.dtp_DBInHoustTime.Value,ref stockListID);

            if (stockListModel == null)
            {
                bllStock.Delete(stockModel.StockID);
                bllGoodsSite.UpdateGoodsSiteStatus(EnumGSStoreStatus.空货位.ToString()
                             , EnumGSRunStatus.待用.ToString(), EnumTaskCategory.出入库.ToString(), gsModel.GoodsSiteID);
                return;
            }
            CreateStockDetail(stockListModel, this.tb_DBFrameCode.Text.Trim());
            MessageBox.Show("入库成功！");
            AddLog("实用工具：手动入库，手动入库成功，库房：" + this.cb_DBStoreHouse.Text + "，货位位置：" + this.tb_DBGSPos.Text+
            ",物料状态:" + cb_DBProductStatus.Text + ",入库时间：" + this.dtp_DBInHoustTime.Value.ToString()
            + ",托盘条码：" + this.tb_DBFrameCode.Text, EnumLogType.提示, EnumLogCategory.管理层日志);
        }

        /// <summary>
        /// 作者:np
        /// 时间:2014年6月10日
        /// 内容:判断传来的码数据库中是否存在
        /// </summary>
        /// <param name="applyParameter"></param>
        /// <returns></returns>
        private bool IsAllTrayIDExist(string applyParameter)
        {
            bool isExist = true;
            string[] trayIDs = SplitStringArray(applyParameter);
            if (trayIDs != null)
            {
                for (int i = 0; i < trayIDs.Length; i++)
                {
                    TB_Tray_indexModel trayModel = bllTrayIndex.GetModel(trayIDs[i]);
                    if (trayModel == null)
                    {
                        isExist = false;
                        break;
                    }
                }
            }
            return isExist;
        }

        private string[] SplitStringArray(string srcStrs)
        {
            string[] splitStrArr = new string[2] { ",", "，" };
            if (srcStrs == null || srcStrs == string.Empty || splitStrArr == null || splitStrArr.Count() == 0)
            {
                return null;
            }
            return srcStrs.Split(splitStrArr, StringSplitOptions.RemoveEmptyEntries);
        }
        /// <summary>
        /// 作者:np
        /// 时间:2014年6月4日
        /// 内容:创建库存详细，一次将一个货位所有的料框都记录的详细表里
        /// </summary>
        /// <param name="stockListID"></param>
        /// <param name="trayIDsStr"></param>
        private void CreateStockDetail(StockListModel stockListModel, string trayIDsStr)
        {
            
            StockDetailModel stockDetailModel = new StockDetailModel();
            stockDetailModel.StockListID = stockListModel.StockListID;
            string[] trayIDs = SplitStringArray(trayIDsStr,new string[2] {",","，"});


            if (trayIDs != null)
            {
                if (stockListModel.StoreHouseName == EnumStoreHouse.A1库房.ToString())
                {
                    for (int i = 0; i < Math.Min(trayIDs.Length, 2); i++)
                    {
                        stockDetailModel.TrayID = trayIDs[i];
                        bllStockDetail.Add(stockDetailModel);
                    }
                }
                else if (stockListModel.StoreHouseName == EnumStoreHouse.B1库房.ToString())
                {
                    for (int i = 0; i < Math.Min(trayIDs.Length, 6); i++)
                    {
                        stockDetailModel.TrayID = trayIDs[i];
                        bllStockDetail.Add(stockDetailModel);
                    }
                }

            }
        }

        /// <summary>
        /// 生成库存列表
        /// </summary>
        /// <param name="taskType"></param>
        /// <param name="taskInterface"></param>
        /// <param name="stockID"></param>
        /// <param name="stockListID"></param>
        /// <returns></returns>
        private StockListModel CreateStockListModel(string houseName, string productStatus,string taskCode,string  interParam
            , GoodsSiteModel gsModel, long manaTaskID, long stockID, DateTime updateTime,ref long stockListID)
        {
            //生成库存列表

            StockListModel stockListModel = new StockListModel();
            stockListModel.ManaTaskID = manaTaskID;
             
            stockListModel.StoreHouseName = houseName;
            string procuctCode = "dx";
            stockListModel.ProductStatus = productStatus;
            stockListModel.ProductCode = procuctCode; // 产品号 默认为1
            stockListModel.ProductNum = 48;//默认为满箱
                  
            stockListModel.ProductName ="电芯";

            stockListModel.ProductFrameCode = taskCode;//为料框编码
            string[] productBatchNums = SplitStringArray(interParam,new string[2]{",","，"});
            if (productBatchNums != null && productBatchNums.Length > 0)//解析产品批次号
            {
                TB_Tray_indexModel trayIndexModel = bllTrayIndex.GetModel(productBatchNums[0]);
                if (trayIndexModel == null)
                {
                    MessageBox.Show("您输入的托盘号码在数据库中不存在！");
                    return null;
                }
                stockListModel.ProductBatchNum = trayIndexModel.Tf_BatchID;
            }
            
            stockListModel.GoodsSiteName = gsModel.GoodsSiteName;
            stockListModel.InHouseTime = DateTime.Now;
            stockListModel.UpdateTime = updateTime;
            stockListModel.StockID = stockID;
            stockListID = bllStockList.Add(stockListModel);
            stockListModel.StockListID = stockListID;
            return stockListModel;
        }

        private void IniDBProductStatus()
        {
            this.cb_DBProductStatus.Items.Clear();
            this.cb_DBProductStatus.Items.Add(EnumProductStatus.A1库静置10小时.ToString());
            this.cb_DBProductStatus.Items.Add(EnumProductStatus.B1库静置10天.ToString());
            this.cb_DBProductStatus.Items.Add(EnumProductStatus.A1库老化3天.ToString());
            this.cb_DBProductStatus.Items.Add(EnumProductStatus.空料框.ToString());
           
            if (this.cb_DBProductStatus.Items.Count > 0)
            {
                this.cb_DBProductStatus.SelectedIndex = 0;
            }
        }

       #endregion


        private void dataGridViewBatterys_BL_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }
       
    }
}
