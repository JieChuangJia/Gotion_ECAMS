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
//using Sygole.HFReader;
using SygoleHFReaderIF;
using ECAMSDataAccess;
using ECAMSModel;
using PLCControl;
namespace ECAMSPCSimTest
{
    public partial class Form1 : Form
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
        PLCRW plcRwObj = null;
        SgrfidRW rfidRW = null;

        private Thread rfidWorkingThread = null;
        private int rfidRWInterval = 100;
        private bool exitRunning = false;
        private bool pauseFlag = false;
        private Int64 rwCounts = 0;
        private Int64 rwFaileCounts = 0;

        #endregion
        public Form1()
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
                this.richTextBoxLog.Text += (log + "\r\n");
            }
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
            ctlManager = new ECAMWCS();
            ctlManager.AttachErrorHandler(ErrorEventHandler);
            ctlManager.AttachLogHandler(LogEventHandler);
            this.buttonStart.Enabled = false;
            this.buttonStop.Enabled = false;
          //  this.tabPage1.Enabled = false;
            devBll = new DeviceBll();
            ctlTaskBll = new ControlTaskBll();
            ctlTaskIFBll = new ControlInterfaceBll();
            palletBll = new OCVPalletBll();
            batteryBll = new OCVBatteryBll();
            manTaskBll = new ManageTaskBll();
            plcRwObj = new PLCRW();
            plcRwObj.eventLinkLost += PlcLostConnectHandler;
            HFReaderIF readerIF = new HFReaderIF();
            
            rfidRW = new SgrfidRW(1);

            this.comboBoxComports.Items.Clear();
            int i = 0;
            string[] ports = System.IO.Ports.SerialPort.GetPortNames();
            foreach (string port in ports)
            {
                this.comboBoxComports.Items.Add(port);
                i++;
            }
            if (ports != null && ports.Count() > 0)
            {
                this.comboBoxComports.Text = ports[0];
            }

            rfidWorkingThread = new Thread(new ThreadStart(SysWorkingProc));
            rfidWorkingThread.IsBackground = true;
            rfidWorkingThread.Name = "RFID读写卡测试线程";

        }
        #region 初始化，启停
        private void buttonInit_Click(object sender, EventArgs e)
        {
            string reStr = "";
            ctlManager.WCSInit(ref reStr);
            AddLog(reStr);
            this.buttonInit.Enabled = false;
            this.buttonStart.Enabled = true;
            this.buttonStop.Enabled = false;
            //this.tabControl1.Enabled = true;
            tabPage1.Enabled = true;
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            this.buttonStop.Enabled = true;
            this.buttonStart.Enabled = false;
            string reStr = "";
            ctlManager.WCSStart(ref reStr);
            AddLog(reStr);
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            this.buttonStart.Enabled = true;
            this.buttonStop.Enabled = false;
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
            if (comboBoxDtName.Text == "控制任务表")
            {
                this.buttonDtDelete.Visible = true;
                this.buttonDtModify.Visible = true;
            }
            else
            {
                this.buttonDtDelete.Visible = false;
                this.buttonDtModify.Visible = false;
            }
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
        #region 任务生成
        private void buttonGenerateTask_Click(object sender, EventArgs e)
        {
            string taskName = this.comboBoxTaskType.Text;
            ControlTaskModel task = new ControlTaskModel();
            task.TaskID = long.Parse(this.textBoxTaskManID.Text);
            //task.StartDevice = devID;
            
            task.TaskTypeName = taskName;
            task.ControlCode =ctlTaskIFBll.GetNewTaskCode();//.textBoxControlCode.Text;
            task.CreateTime = System.DateTime.Now;
            task.TaskStatus = EnumTaskStatus.待执行.ToString();
            task.CreateMode = EnumTaskMode.手动.ToString();
            task.TaskParameter = this.textBoxTaskParam.Text;
            task.TaskPhase = "0";
           
            if (taskName == EnumTaskName.电芯入库_A1.ToString())
            {
                task.StartArea = "装箱区";
                task.TargetArea = "老化区A1";
                task.StartDevice = "2002";
                task.TargetDevice = this.textBoxTaskParam.Text;
                task.TaskType = "入库";
                task.TaskTypeCode = 3;
                
            }
            else if (taskName == EnumTaskName.电芯出库_A1.ToString())
            {
                task.StartArea = this.textBoxTaskParam.Text;
                task.TargetArea = "一次检测区";
                task.StartDevice = "老化区A1";
                task.TargetDevice = "2005";
                task.TaskType = "出库";
                task.TaskTypeCode = 6;
            }
            else if (taskName == EnumTaskName.分容入库_A1.ToString())
            {
                task.StartArea = "装箱区";
                task.TargetArea = "老化区";
                task.StartDevice = "2004";
                task.TargetDevice = this.textBoxTaskParam.Text;
                task.TaskType = "入库";
                task.TaskTypeCode = 5;
            }
            else if (taskName == EnumTaskName.分容出库_A1.ToString())
            {
                task.StartArea = "老化区A1";
                task.TargetArea = "老化区";
                task.StartDevice = this.textBoxTaskParam.Text;
                task.TargetDevice = "2003";
                task.TaskType = "出库";
                task.TaskTypeCode = 4;
            }
            else if (taskName == EnumTaskName.电芯入库_B1.ToString())
            {
                task.StartArea = "装箱区";
                task.TargetArea = "老化区";
                task.StartDevice = "2006";
                task.TargetDevice = this.textBoxTaskParam.Text;
                task.TaskType = "入库";
                task.TaskTypeCode = 8;
            }
            else if (taskName == EnumTaskName.电芯出库_B1.ToString())
            {
                task.StartArea = "老化区A1";
                task.TargetArea = "老化区";
                task.StartDevice = this.textBoxTaskParam.Text;
                task.TargetDevice = "2007";
                task.TaskType = "出库";
                task.TaskTypeCode = 9;
            }
             else if(taskName ==EnumTaskName.空料框入库.ToString())
            {
                task.StartArea = "老化区A1";
                task.TargetArea = "老化区";
                task.StartDevice = "2008";
                task.TargetDevice = this.textBoxTaskParam.Text;
                task.TaskType = "入库";
                task.TaskTypeCode = 1;
            }
            else if (taskName == EnumTaskName.空料框出库.ToString())
            {
                task.StartArea = "老化区A1";
                task.TargetArea = "老化区";
                task.StartDevice = this.textBoxTaskParam.Text;
                task.TargetDevice = "2009";
                task.TaskType = "出库";
                task.TaskTypeCode = 2;
            }
            else if (taskName == EnumTaskName.电芯装箱组盘.ToString())
            {
                task.StartArea = "老化区A1";
                task.TargetArea = "老化区";
                task.StartDevice = "5001";
                task.TargetDevice = "2001";
                task.TaskType = "入库";
                task.TaskTypeCode = 11;
            }
            else if (taskName == EnumTaskName.电芯一次拣选.ToString())
            {
                task.StartArea = "老化区A1";
                task.TargetArea = "老化区";
                task.StartDevice = "4001";
                task.TargetDevice = "5002";
                task.TaskType = "出库";
                task.TaskTypeCode = 7;
            }
            else if (taskName == EnumTaskName.电芯二次拣选.ToString())
            {
                task.StartArea = "老化区A1";
                task.TargetArea = "老化区";
                task.StartDevice = "4002";
                task.TargetDevice = "5003";
                task.TaskType = "出库";
                task.TaskTypeCode = 7;
            }
            try
            {
                ctlTaskBll.Add(task);
                AddLog("增加一条控制任务：" + taskName);
            }
            catch (System.Exception ex)
            {
                AddLog("出现异常：" + ex.Message);
            }
           
        }
        public  string[] SplitStringArray(string srcStrs, string[] splitStr)
        {
            if (srcStrs == null || srcStrs == string.Empty || splitStr == null || splitStr.Count() == 0)
            {
                return null;
            }
            return srcStrs.Split(splitStr, StringSplitOptions.RemoveEmptyEntries);
        }
        #endregion
        #region 设备监控
        private void buttonRefreshDevStatus_Click(object sender, EventArgs e)
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
            }
            this.dataGridViewDevDB2.DataSource = dt2;
            for (int i = 0; i < this.dataGridViewDevDB2.Columns.Count; i++)
            {
                this.dataGridViewDevDB2.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            this.dataGridViewCurrentTask.DataSource = dtTask;
            for (int i = 0; i < this.dataGridViewDevDB2.Columns.Count; i++)
            {
                this.dataGridViewCurrentTask.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            //IList<string> db1ValList = dev.GetDB1Data();
            //IList<string> db2ValList = dev.GetDB2Data();
            //DataTable dt1 = new DataTable();
            //dt1.Columns.Add("索引");
            //dt1.Columns.Add("内容");
           
            
            //int index = 1;
            //foreach (string valStr in db1ValList)
            //{
            //    DataRow dr = dt1.NewRow();
            //    dr[0] = index;
            //    dr[1] = valStr;
            //    dt1.Rows.Add(dr);
            //    index++;
                
            //}
            //this.dataGridViewDevDB1.DataSource = dt1;
            // for(int i=0;i<this.dataGridViewDevDB1.Columns.Count;i++)
            //{
            //    this.dataGridViewDevDB1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            //}
            //index = 1;
            //DataTable dt2 = new DataTable();
            //dt2.Columns.Add("索引");
            //dt2.Columns.Add("内容");
         
            //foreach (string valStr in db2ValList)
            //{
            //    DataRow dr = dt2.NewRow();
            //    dr[0] = index;
            //    dr[1] = valStr;
            //    dt2.Rows.Add(dr);
            //    index++;
            //}
            //this.dataGridViewDevDB2.DataSource = dt2;
            //for (int i = 0; i < this.dataGridViewDevDB2.Columns.Count; i++)
            //{
            //    this.dataGridViewDevDB2.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            //}
            
        }
        private void buttonClearDevCmd_Click(object sender, EventArgs e)
        {
            string devID = this.comboBoxDevList.Text;
            ECAMSDevBase dev = ctlManager.GetDev(devID);
            if (dev.ClearDevCmd())
            {
                AddLog("设备:" + devID + " 发送命令清空");
            }
            if (dev.ClearRunningTask())
            {
                AddLog("设备"+ devID + " 清空当前任务");
            }
        }
        #endregion  
        #region PLC 通信测试
        private void buttonConnectPlc_Click(object sender, EventArgs e)
        {
            string PlcIP = ConfigurationManager.AppSettings["plcIP"];
            string PlcPort = ConfigurationManager.AppSettings["plcPort"];
            string plcAddr = PlcIP + ":" + PlcPort;
            string reStr = "";
            if (plcRwObj.ConnectPLC(plcAddr, ref reStr))
            {
                this.buttonClosePlc.Enabled = true;
                this.buttonReadPlc.Enabled = true;
                this.buttonWritePlc.Enabled = true;
            }
            AddLog(reStr);
        }
        private void buttonClosePlc_Click(object sender, EventArgs e)
        {
            if (plcRwObj.CloseConnect())
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
            if (plcRwObj.ConnectPLC(plcAddr, ref reStr))
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
            if (!plcRwObj.ReadDB(addr, ref val))
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
            if (!plcRwObj.WriteDB(addr, val))
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
            if (plcRwObj.WriteMultiDB(addrStart, blockNum, vals))
            {
                AddLog("批量写入成功");
            }
            else
            {
                AddLog("批量写入失败");
            }
        }

        private void buttonMultiReadPlc_Click(object sender, EventArgs e)
        {
            string addrStart = this.textBoxPlcAddrStart.Text;
            int blockNum = int.Parse(this.textBoxPlcBlockNum.Text);
            short[] reVals = null;
            if (plcRwObj.ReadMultiDB(addrStart, blockNum, ref reVals))
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
        #endregion   

       
        #region 读卡测试
        private void buttonOpenComport_Click(object sender, EventArgs e)
        {
            rfidRW.ReaderIF.ComPort = this.comboBoxComports.Text;
            string reStr = "";
            if (!rfidRW.ReaderIF.OpenComport(ref reStr))
            {
                AddLog(reStr);
                return;
            }
            if (rfidRW.Connect())
            {
                AddLog("读卡器已经连接上");
            }
            else
            {
                AddLog("读卡器连接失败");
            }
        }
        private void buttonClosePort_Click(object sender, EventArgs e)
        {
            if (rfidRW.Discconnect())
            {
                AddLog("读卡器已经关闭");
            }
           
        }
        private void buttonRfidReset_Click(object sender, EventArgs e)
        {
            //if (Status_enum.SUCCESS == rfidRW.reader.SoftReset(rfidRW.ReaderID))
            //{
            //    AddLog("读卡器软复位成功");
            //}
            //else
            //{
            //    AddLog("读卡器软复位失败");
            //}
        }
        private void buttonReadRfid_Click(object sender, EventArgs e)
        {
            try
            {
                byte blockStart = byte.Parse(this.textBoxRfidBlockStart.Text);
               // byte blockNum = byte.Parse(this.textBoxRfidBlockNum.Text);
                byte[] bytesData = rfidRW.ReadSBlock(blockStart);
                if (bytesData == null || bytesData.Count()<4)
                {
                    MessageBox.Show("读卡失败");
                    return;
                }
                string strData = SgrfidRW.bytes2hexString(bytesData, bytesData.Count(), 1);
                if (strData != null)
                {
                    this.textBoxRfidread.Text = strData;
                }
            }
            catch (System.Exception ex)
            {
                AddLog(ex.Message + "," + ex.StackTrace);
            }
            
        }
        private void buttonWriteRfid_Click(object sender, EventArgs e)
        {
            byte[] bytesSnd = SgrfidRW.String2Bytes(this.textBoxRfidWrite.Text, 0);
            if (bytesSnd != null)
            {
                if (rfidRW.WriteSBlock(bytesSnd))
                {
                    AddLog("rfid 数据写入成功");
                }
                else
                {
                    AddLog("rfid 数据写入失败");
                }
            }
            else
            {
                AddLog("rfid 数据写入失败");
            }

        }
        private void buttonRfidReadCfg_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    byte id = byte.Parse(this.textBoxRfidIDInv.Text);
            //    Sygole.HFReader.UserCfg cfg = rfidRW.GetUserCfg(id);
            //    if (cfg == null)
            //    {
            //        AddLog("查询读卡器配置失败");
            //        return;
            //    }
            //    this.rfidRW.ReaderID = cfg.ReaderID;
            //    this.textBoxRfidIDInv.Text = cfg.ReaderID.ToString();
            //    if (cfg.RFChipPower == RFChipPower_enum.FULL_POWER)
            //    {
            //        radioButtonFullPower.Checked = true;
            //    }
            //    else
            //    {
            //        radioButtonHalfPower.Checked = true;
            //    }
            //    if (cfg.BlockSize == BlockSize_enum.SIZE_8B)
            //    {
            //        radioButton8Bytes.Checked = true;
            //    }
            //    else
            //    {
            //        radioButton4Bytes.Checked = true;
            //    }
            //    if (cfg.AvailableTime == AvailableTime_enum.CONTINUANCE)
            //    {
            //        radioButtonContinue.Checked = true;
            //    }
            //    else
            //    {
            //        radioButtonRealTime.Checked = true;
            //    }
            //    if (cfg.CommPort == CommPort_enum.RS232)
            //    {
            //        radioButtonRS232.Checked = true;
            //    }
            //    else
            //    {
            //        radioButtonRS485.Checked = true;
            //    }
            //    AddLog("查询RFID用户配置成功");
            //}
            //catch (System.Exception ex)
            //{
            //    AddLog(ex.Message + "," + ex.StackTrace);
            //}
            
            

        }

        private void buttonRfidWriteCfg_Click(object sender, EventArgs e)
        {
            try
            {
                //UserCfg cfg = new UserCfg();

                //cfg.WorkMode = radioButtonOperMode.Checked ? WorkMode_enum.OPERATOR_MODE : WorkMode_enum.AUTO_MODE;
                //cfg.ReaderID = byte.Parse(textBoxRfidIDWrite.Text);
                //cfg.RFChipPower = radioButtonFullPower.Checked ? RFChipPower_enum.FULL_POWER : RFChipPower_enum.HALF_POWER;
                //cfg.NeedBCC = NeedBCC_enum.NEED_BCC;
                //cfg.BlockSize = radioButton8Bytes.Checked ? BlockSize_enum.SIZE_8B : BlockSize_enum.SIZE_4B;
                //cfg.AvailableTime = radioButtonContinue.Checked ? AvailableTime_enum.CONTINUANCE : AvailableTime_enum.REAL_TIME;
                //cfg.CommPort = radioButtonRS232.Checked ? CommPort_enum.RS232 : CommPort_enum.RS485;

                //if (rfidRW.SetUserCfg(cfg))
                //{
                //    AddLog("修改读卡器用户配置成功！");
                //}
                //else
                //{
                //    AddLog("修改读卡器用户配置失败！");
                //}
            }
            catch (System.Exception ex)
            {
                AddLog(ex.Message + "," + ex.StackTrace);
            }
        }
        private void SysWorkingProc()
        {
            while (!exitRunning)
            {
                if (pauseFlag)
                {
                    continue;
                }
                try
                {
                    Thread.Sleep(rfidRWInterval);
                    byte[] rfidData = this.rfidRW.ReadSBlock(0);
                    rwCounts++;
                    if (rfidData == null || rfidData.Count()<4)
                    {
                        rwFaileCounts++;
                    }
                    Thread.Sleep(rfidRWInterval);
                    byte[] rfidDataWrite = new byte[4] { 1, 2, 3, 4 };

                    rwCounts++;
                    if (!this.rfidRW.WriteSBlock(rfidDataWrite))
                    {
                        rwFaileCounts++;
                    }
                    
                    RefreshRWCounts();
                }
                catch (System.Exception ex)
                {
                    AddLog(ex.Message + "," + ex.StackTrace);
                }
            }
        }
        private void buttonStartRW_Click(object sender, EventArgs e)
        {
            if (rfidWorkingThread.ThreadState == (ThreadState.Unstarted | ThreadState.Background))
            {
                rfidWorkingThread.Start();
            }
            else
            {
                // Monitor.Exit(sysWorkingThreadLock);
                // sysWorkingThread.Resume();
                pauseFlag = false;
            }
            AddLog("自动读写卡启动");
        }

        private void buttonStopRW_Click(object sender, EventArgs e)
        {
            pauseFlag = true;
            AddLog("自动读写卡停止");
        }

        private void buttonClearRWCounts_Click(object sender, EventArgs e)
        {
            rwCounts = 0;
            rwFaileCounts = 0;
            this.textBoxrfidRWCounts.Text = "0";
            this.textBoxrfidRWFaileds.Text = "0";
        }
        private void delegateRefreshRWCounts()
        {
            this.textBoxrfidRWCounts.Text = rwCounts.ToString();
            this.textBoxrfidRWFaileds.Text = rwFaileCounts.ToString();
            if (rwCounts > 0)
            {
                float failRate = (float)rwFaileCounts / (float)rwCounts;
                this.textBoxrfidRWFailRate.Text = failRate.ToString();
            }
        }
        private void RefreshRWCounts()
        {
            DelegateRefeshRfid delegateRW = new DelegateRefeshRfid(delegateRefreshRWCounts);
            this.BeginInvoke(delegateRW, null);
        }
        #endregion

        private void comboBoxComports_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            exitRunning = true;
            rfidRW.Discconnect();
        }

      
    }
}
