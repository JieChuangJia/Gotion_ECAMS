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
namespace DeviceAssist
{
    public partial class DeviceAssistForm : Form
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
        PLCRW plcRwObj = null;
        PLCRWNet plcRwObj2 = null;
        IPlcRW plcRwIF = null;
        PlcRW485BD plcFx485 = null;
        SgrfidRW rfidRW = null;

        private Thread rfidWorkingThread = null;
        private int rfidRWInterval = 100;
        private bool exitRunning = false;
        private bool pauseFlag = false;
        private Int64 rwCounts = 0;
        private Int64 rwFaileCounts = 0;

        private int makeCardCount = 0;

       

        #endregion
        public DeviceAssistForm()
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
        private void buttonClearLog_Click(object sender, EventArgs e)
        {
            this.richTextBoxLog.Clear();
        }
        #endregion
        private void Form1_Load(object sender, EventArgs e)
        {
            string jxDB = ConfigurationManager.AppSettings["JXDataBase"];
            string jxDBUserPwd = ConfigurationManager.AppSettings["JXDataBaseUserPwd"];
            jxDBUserPwd = EncAndDec.Decode(jxDBUserPwd, "zwx", "xwz");
            if (string.IsNullOrEmpty(jxDBUserPwd))
            {
                MessageBox.Show("数据库连接信息错误");
                return;
            }
            PubConstant.ConnectionString = jxDB + jxDBUserPwd;

            this.comboBoxPlcObjList.SelectedIndex = 0;
           
           
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

            plcFx485 = new PlcRW485BD();
            plcFx485.StationNumber = 1;
            HFReaderIF readerIF = new HFReaderIF();
            rfidRW = new SgrfidRW(1);
            rfidRW.ReaderIF = readerIF;
            this.comboBoxComports.Items.Clear();

            string[] ports = System.IO.Ports.SerialPort.GetPortNames();
            foreach (string port in ports)
            {
                this.comboBoxComports.Items.Add(port);
                this.comboBoxFXComs.Items.Add(port);

            }
            if (ports != null && ports.Count() > 0)
            {
                this.comboBoxComports.Text = ports[0];
                this.comboBoxFXComs.Text = ports[0];
            }
            makeCardCount = makeCardBll.GetRecordCount(" ");
            this.labelMakeCardCount.Text = "计数：" + makeCardCount.ToString();

        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            exitRunning = true;
            rfidRW.Discconnect();
        }
        #region Q 系列PLC 通信测试
        private void buttonConnectPlc_Click(object sender, EventArgs e)
        {
            string PlcIP = this.textBoxPlcIP.Text;//ConfigurationManager.AppSettings["plcIP"];
            string PlcPort = this.textBoxPlcPort.Text;// ConfigurationManager.AppSettings["plcPort"];
            string plcAddr = PlcIP + ":" + PlcPort;
            string reStr = "";

            if (plcRwIF.ConnectPLC(plcAddr, ref reStr))
            {
                this.buttonClosePlc.Enabled = true;
                this.buttonReadPlc.Enabled = true;
                this.buttonWritePlc.Enabled = true;
            }
            AddLog(reStr);
            
        
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
            if (strArray == null || strArray.Count() < 1)
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

   
        private void comboBoxPlcObjList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBoxPlcObjList.Text == "PLC控件")
            {
                plcRwIF = plcRwObj;

            }
            else if (this.comboBoxPlcObjList.Text == "重新实现MC协议")
            {
                plcRwIF = plcRwObj2;

            }
        }
        #endregion   
        #region FX系列PLC测试
        private void buttonFXOpen_Click(object sender, EventArgs e)
        {
            string reStr = "";
            if(!plcFx485.ConnectPLC(this.comboBoxFXComs.Text,ref reStr))
            {
                AddLog("打开串口失败,返回错误码:" + reStr);
            }
            else
            {
                
                AddLog("串口已打开");
            }
        }
        private void buttonFXClose_Click(object sender, EventArgs e)
        {
           plcFx485.CloseConnect();
           AddLog("串口关闭");
        }
        private void buttonReadMZone_Click(object sender, EventArgs e)
        {
            try
            {
                ReadMZone();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
          
        }
        private void ReadMZone()
        {
            plcFx485.PlcStationNumber = byte.Parse(this.textBoxPLCStation.Text);
            string addr = this.textBoxMZoneAddr.Text;
            int val = 0;
            if (!plcFx485.ExeBitRead(addr, ref val))
            {
                AddLog("读地址" + addr + "失败");
                return;
            }
           
            this.textBoxPLCBitVal.Text = val.ToString();
        }
        private void buttonWriteMZone_Click(object sender, EventArgs e)
        {
            try
            {
                WriteMZone();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }
        private void WriteMZone()
        {
            plcFx485.PlcStationNumber = byte.Parse(this.textBoxPLCStation.Text);
            string addr = this.textBoxMZoneAddr.Text;
            short val = 0;
            if (!short.TryParse(this.textBoxPLCBitVal.Text, out val))
            {
                MessageBox.Show("数值输入有误");
                return;
            }
            if(!plcFx485.ExeBitWrite(addr, val))
            {
                AddLog("写地址" + addr + "失败，");
                return;
            }
            else
            {
                AddLog("写地址" + addr + "成功");
            }
        }
        private void buttonReadSBlock_Click(object sender, EventArgs e)
        {
            try
            {
                ReadFx485SBlock();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }
        private void ReadFx485SBlock()
        {
            this.textBoxPlcBlockVal.Text = "";
            plcFx485.PlcStationNumber = byte.Parse(this.textBoxPLCStation.Text);
            string addr = this.textBoxDZoneAddr.Text;
            int val = 0; 
            if (!plcFx485.ReadDB(addr, ref val))
            {
                AddLog("读地址" + addr + "失败");
                return;
            }
            this.textBoxPlcBlockVal.Text = val.ToString();
        }
        private void buttonReadMBlock_Click(object sender, EventArgs e)
        {
            try
            {
                ReadFx485MBlock();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
           
        }
        private void ReadFx485MBlock()
        {
            this.richTextBoxMBlock.Text = "";
            plcFx485.PlcStationNumber = byte.Parse(this.textBoxPLCStation.Text);
            string addr = this.textBoxDZoneAddr.Text;
            short[] vals = null;
            int blockNum = int.Parse(this.textBoxFxBlockNum.Text);
            if (!plcFx485.ReadMultiDB(addr, blockNum, ref vals))
            {
                AddLog("批量读寄存器失败");
                return;
            }
            for (int i = 0; i < blockNum; i++)
            {
                this.richTextBoxMBlock.Text += (vals[i].ToString() + ",");
            }
        }

        private void WriteFx485SBlock()
        {
            
            plcFx485.PlcStationNumber = byte.Parse(this.textBoxPLCStation.Text);
            string addr = this.textBoxDZoneAddr.Text;
            int val = int.Parse(this.textBoxPlcBlockVal.Text);
            if (plcFx485.WriteDB(addr, val))
            {
                AddLog("写地址" + addr + "成功");
            }
            else
            {
                AddLog("写地址" + addr + "失败");
            }
        }
        private void buttonWriteSBlock_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(this.textBoxPlcBlockVal.Text))
                {
                    MessageBox.Show("数据为空");
                    return;
                }
                WriteFx485SBlock();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }
        private void WriteFx485MBlock()
        {
            string addrStart = this.textBoxDZoneAddr.Text;
            int blockNum = int.Parse(textBoxFxBlockNum.Text);
            string[] splitStr = new string[] { ",", ":", "-", ";" };
            string strVals = this.richTextBoxMultiDBVal.Text;
            string[] strArray = strVals.Split(splitStr, StringSplitOptions.RemoveEmptyEntries);
            if (strArray == null || strArray.Count() < 1)
            {
                MessageBox.Show("输入数据错误");
                return;
            }
            short[] vals = new short[strArray.Count()];
            for (int i = 0; i < vals.Count(); i++)
            {
                vals[i] = short.Parse(strArray[i]);
            }
            if (plcFx485.WriteMultiDB(addrStart, blockNum, vals))
            {
                AddLog("批量写入成功");
            }
            else
            {
                AddLog("批量写入失败");
            }
        }

        private void buttonFxWriteMBlock_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(this.richTextBoxMultiDBVal.Text))
                {
                    MessageBox.Show("数据为空");
                    return;
                }
                WriteFx485MBlock();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
           
        }
        #endregion
        #region 读卡测试
        private void buttonOpenComport_Click(object sender, EventArgs e)
        {
            // HFReaderIF readerIF = new HFReaderIF();
            //     rfidRW.ReaderIF = readerIF;
            rfidRW.ReaderIF.ComPort = this.comboBoxComports.Text;
            rfidRW.ReaderID = byte.Parse(this.textBoxReaderID.Text);
            string reStr = "";
            if (!rfidRW.ReaderIF.OpenComport(ref reStr))
            {
                AddLog(reStr);
                return;
            }
            AddLog("通信端口已经打开");
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
                byte blockNum = byte.Parse(this.textBoxRfidBlockNum.Text);
                // byte[] bytesData = rfidRW.ReadSBlock(blockStart);
                byte[] bytesData = rfidRW.ReadRfidMultiBlock(blockStart, blockNum);
                if (bytesData == null || bytesData.Count() < 4)
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
                //if (rfidRW.WriteSBlock(bytesSnd))
                byte blockStart = byte.Parse(this.textBoxRfidBlockStart.Text);
                if (rfidRW.WriteMBlock(blockStart, bytesSnd))
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
        private void buttonReadCfg_Click(object sender, EventArgs e)
        {
            rfidRW.ReaderID = byte.Parse(this.textBoxReaderID.Text);
            if (!rfidRW.Connect())
            {
                MessageBox.Show("连接读卡器，读取配置信息失败");
            }
            else
            {
                MessageBox.Show("连接读卡器，读取配置信息成功");
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
            //PLCRW plcRwObj2 = new PLCRW();
            //string reStr = "";
            //plcRwObj2.ConnectPLC("192.168.1.121:7000", ref reStr);
            while (!exitRunning)
            {
                if (pauseFlag)
                {
                    continue;
                }
                try
                {
                    Thread.Sleep(rfidRWInterval);
                    //byte[] rfidData = this.rfidRW.ReadSBlock(0);
                    //rwCounts++;
                    //if (rfidData == null || rfidData.Count()<4)
                    //{
                    //    rwFaileCounts++;
                    //}
                    //Thread.Sleep(rfidRWInterval);
                    //byte[] rfidDataWrite = new byte[4] { 1, 2, 3, 4 };

                    //rwCounts++;
                    //if (!this.rfidRW.WriteSBlock(rfidDataWrite))
                    //{
                    //    rwFaileCounts++;
                    //}

                    //RefreshRWCounts();
                    //int val = 0;
                    //plcRwObj2.ReadDB("D3000", ref val);

                }
                catch (System.Exception ex)
                {
                    AddLog(ex.Message + "," + ex.StackTrace);
                }
            }
        }
        private void buttonStartRW_Click(object sender, EventArgs e)
        {
            if (rfidWorkingThread.ThreadState == (ThreadState.Unstarted))//| ThreadState.Background
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

        //发卡
        private void buttonWriteID_Click(object sender, EventArgs e)
        {
            try
            {
                string idCheck = this.textBoxWriteIDRepeat.Text;

                if (idCheck == null || idCheck == string.Empty)
                {
                    MessageBox.Show("id为空，请重新输入");
                    return;
                }
                idCheck ="TP"+ idCheck.PadLeft(7, '0');
                if (makeCardBll.Exists(idCheck))
                {
                    MessageBox.Show("该ID已经发过卡，请换一张重新发卡");
                    return;
                }
                if (this.textBoxWriteIDRepeat.Text != this.textBoxWriteID.Text)
                {
                    MessageBox.Show("两次输入不一致，请确认");
                    return;
                }
                rfidRW.ReaderID = byte.Parse(this.textBoxReaderID.Text);
                uint rfidID = uint.Parse(this.textBoxWriteID.Text);
                byte[] byteArray = BitConverter.GetBytes(rfidID);
                if (byteArray != null && byteArray.Count() > 0)
                {
                    this.textBoxWriteID.SelectAll();
                    if (!rfidRW.WriteSBlock(byteArray))
                    {
                        this.labelIDRWResult.Text = "发卡失败!";
                        this.labelIDRWResult.BackColor = Color.Red;
                        MessageBox.Show("发卡失败");

                        return;
                    }
                    byte[] recvByteArray = null;
                    string readPalletID = rfidRW.ReadPalletID(ref recvByteArray);
                    if (string.IsNullOrEmpty(readPalletID) || (readPalletID != idCheck))
                    {
                        string faildInfo = "发卡失败!发卡后回读结果不一致";
                        this.labelIDRWResult.Text = faildInfo;
                        this.labelIDRWResult.BackColor = Color.Red;
                        MessageBox.Show(faildInfo);
                        return;
                    }
                    MakeCardRecordModel cardModel = new MakeCardRecordModel();
                    cardModel.cardID = idCheck;
                    cardModel.makedTime = System.DateTime.Now;

                    if (makeCardBll.Add(cardModel) <= 0)
                    {
                        MessageBox.Show("发卡信息录入数据库失败");
                        return;
                    }
                    makeCardCount++;
                    this.labelMakeCardCount.Text = "计数：" + makeCardCount.ToString();
                    this.labelIDRWResult.Text = "发卡成功!";
                    this.labelIDRWResult.BackColor = Color.Green;

                }

            }
            catch (System.Exception ex)
            {
                MessageBox.Show("发卡失败，错误信息：" + ex.Message);
            }

        }

        //回读
        private void buttonReadID_Click(object sender, EventArgs e)
        {
           

            this.textBoxReadID.Text = string.Empty;
            rfidRW.ReaderID = byte.Parse(this.textBoxReaderID.Text);
            byte[] recvByteArray = null;
            string palletID = rfidRW.ReadPalletID(ref recvByteArray);
            //byte[] bytesArray = rfidRW.ReadSBlock(0);
            if (string.IsNullOrWhiteSpace(palletID))
            {
                MessageBox.Show("回读失败");
                this.labelIDRWResult.Text = "读卡失败";
                this.labelIDRWResult.BackColor = Color.Red;
                string strByteArray = "";

               
                if(recvByteArray != null && recvByteArray.Count()>0)
                {
                    for(int i=0;i<recvByteArray.Count();i++)
                    {
                        strByteArray += (" " + recvByteArray[i].ToString("X").PadLeft(2, '0'));
                    }
                    
                }

                AddLog("读卡失败，返回原始数据：" + strByteArray);
                return;
            }
            else
            {
                this.labelIDRWResult.Text = "读卡成功";
                this.labelIDRWResult.BackColor = Color.Green;
            }
           
            this.textBoxReadID.Text = palletID;
        }
        private void textBoxWriteID_Click(object sender, EventArgs e)
        {
            this.textBoxWriteID.SelectAll();
        }
        private void textBoxWriteIDRepeat_Click(object sender, EventArgs e)
        {
            this.textBoxWriteIDRepeat.SelectAll();
        }


        private void textBoxWriteID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)//如果输入的是回车键
            {
                this.buttonWriteID_Click(sender, e);//触发button事件
            }
        }
        private void buttonGetMakeRecord_Click(object sender, EventArgs e)
        {
            string idCheck = this.textBoxWriteID.Text;

            if (idCheck == null || idCheck == string.Empty)
            {
                MessageBox.Show("id为空，请重新输入");
                return;
            }
            idCheck = "TP" + idCheck.PadLeft(7, '0');
            List<MakeCardRecordModel> makeCardList = makeCardBll.GetModelList("cardID = '" + idCheck + "' ");
            if (makeCardList == null || makeCardList.Count == 0)
            {
                MessageBox.Show("发卡记录为空");
                return;
            }
            MakeCardRecordModel record= makeCardList[0];
            if (record == null)
            {
                MessageBox.Show("发卡记录为空");
                return;
            }
            string recordInfo = string.Format("发卡记录：\n托盘号:{0}\n发卡时间：{1}\n", record.cardID, record.makedTime);
            this.richTextBoxMakeRecord.Text = recordInfo;
        }

        private void buttonDelMakeRecord_Click(object sender, EventArgs e)
        {
            string idCheck = this.textBoxWriteID.Text;

            if (idCheck == null || idCheck == string.Empty)
            {
                MessageBox.Show("id为空，请重新输入");
                return;
            }
            idCheck = "TP" + idCheck.PadLeft(7, '0');
            List<MakeCardRecordModel> makeCardList = makeCardBll.GetModelList("cardID = '" + idCheck + "' ");
            if (makeCardList == null || makeCardList.Count == 0)
            {
                MessageBox.Show("发卡记录为空");
                return;
            }
            MakeCardRecordModel record = makeCardList[0];
            if (record == null)
            {
                MessageBox.Show("发卡记录为空");
            }
            makeCardBll.Delete(record.serialNo);
            MessageBox.Show("发卡记录已删除");
        }
        #endregion

       


    }
}
