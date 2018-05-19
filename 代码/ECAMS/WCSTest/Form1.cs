using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ECAMSModel;
namespace WCSTest
{
    public partial class Form1 : Form,ITestHouseInView
    {
        private AssistTool assistTool = new AssistTool();
       
        private TestBusinessManager testBusinessManager = new TestBusinessManager();
        public delegate void DelegateAddLog(string log);
       
        private string currentHouseInTypeTest = string.Empty;
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonAllocPLCDB_Click(object sender, EventArgs e)
        {
           // assistTool.FillDB2("5001");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.comboBoxHouseInTasks.Items.Add(EnumTaskName.电芯入库_A1.ToString());
            this.comboBoxHouseInTasks.Items.Add(EnumTaskName.分容入库_A1.ToString());
            this.comboBoxHouseInTasks.Items.Add(EnumTaskName.电芯入库_B1.ToString());
            this.comboBoxHouseInTasks.Items.Add(EnumTaskName.空料框入库.ToString());
            this.comboBoxHouseInTasks.Items.Add(EnumTaskName.空料框出库.ToString());
            this.comboBoxHouseInTasks.SelectedIndex = 0;
            this.comboBoxHouse.SelectedIndex = 0;
            this.comboBoxGripTask.SelectedIndex = 0;
            currentHouseInTypeTest = this.comboBoxHouseInTasks.SelectedItem.ToString();
            

          
            //dbDataForm.Hide();
            string reStr = string.Empty;
            //if (!wcs.WCSInit(ref reStr))
            //{
            //    AddLog(reStr);
            //}
            //wcs.AttachLogHandler(LogEventHandler);
            if (!testBusinessManager.InitTestManager(ref reStr))
            {
                AddLog(reStr);
            }
            testBusinessManager.AttachLogHandler(LogEventHandler);
            testBusinessManager.WarehouseInOutTest.view = this;
         
        }

        private void buttonWcsStart_Click(object sender, EventArgs e)
        {
            this.buttonStop.Enabled = true;
            this.buttonWcsStart.Enabled = false;
            string reStr = string.Empty;
            //if (wcs.WCSStart(ref reStr))
            //{
            //    AddLog("WCS start ok");
            //}
            //else
            //{
            //    AddLog("WCS start failed,"+reStr);
            //}

            if (testBusinessManager.StartTest(ref reStr))
            {
                AddLog("PLC业务模拟启动成功");
            }
            else
            {
                AddLog("PLC业务模拟启动失败"+reStr);
            }
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            this.buttonStop.Enabled = false;
            this.buttonWcsStart.Enabled = true;
            string reStr = string.Empty;
            //if (wcs.WCSStop(ref reStr))
            //{
            //    AddLog("WCS stop ok");
            //}
            //else
            //{
            //    AddLog("WCS stop failed,"+reStr);
            //}
            testBusinessManager.StopTest(ref reStr);
            
             AddLog("PLC业务模拟停止");
            
        }
        private void LogEventHandler(object sender, LogEventArgs e)
        {
            string logStr = e.LogContent + "  " + e.LogTime.ToString();
            AddLog(logStr);
        }

        private void delegateAddLog(string log)
        {
            this.richTextBox1.Text += (log + "\r\n");
        }
        /// <summary>
        /// 增加一条日志
        /// </summary>
        /// <param name="log"></param>
        private void AddLog(string log)
        {
            if (this.richTextBox1.InvokeRequired)
            {
                DelegateAddLog delegateObj= new DelegateAddLog(delegateAddLog);
                this.Invoke(delegateObj,new object[]{log});
            }
            else
            {
                this.richTextBox1.Text += (log + "\r\n");
            }
        }

        private void buttonClearLog_Click(object sender, EventArgs e)
        {
            this.richTextBox1.Text = "";
        }

    
        private void buttonDevClear_Click(object sender, EventArgs e)
        {
            testBusinessManager.DevcomClear(this.textBoxDevID.Text);
        }

        #region ITestHouseInView接口实现
        /// <summary>
        /// 刷新A1入库相关设备的DB1
        /// </summary>
        /// <param name="strData"></param>
        public void UpdateA1DB1(string strData)
        {

        }
        public void UpdateA1DB2(string strData)
        {

        }

        /// <summary>
        /// 刷新B1入库相关设备的DB1
        /// </summary>
        /// <param name="strData"></param>
        public void UpdateB1DB1(string strData)
        {

        }
        public void UpdateB1DB2(string strData)
        {

        }
        #endregion
        #region 入库模拟UI事件
        private void radioButton_2002_full_CheckedChanged(object sender, EventArgs e)
        {
            currentHouseInTypeTest = this.comboBoxHouseInTasks.SelectedItem.ToString();
            bool portFull = false;
            if (this.radioButton_2002_full.Checked)
            {
                portFull = true;
               
            }
            else
            {
                portFull = false;
               
            }
            if (currentHouseInTypeTest == EnumTaskName.电芯入库_A1.ToString())
            {
                testBusinessManager.WarehouseInOutTest.SetTransPortFull(2002, portFull);
            }
            else if (currentHouseInTypeTest == EnumTaskName.分容入库_A1.ToString())
            {
                testBusinessManager.WarehouseInOutTest.SetTransPortFull(2004, portFull);
            }
            else if (currentHouseInTypeTest == EnumTaskName.电芯入库_B1.ToString())
            {
                testBusinessManager.WarehouseInOutTest.SetTransPortFull(2006, portFull);
            }
            else if (currentHouseInTypeTest == EnumTaskName.空料框入库.ToString())
            {
                testBusinessManager.WarehouseInOutTest.SetTransPortFull(2008, portFull);
            }
        }
        private void buttonHouseInA1NewTask_Click(object sender, EventArgs e)
        {
            if (this.comboBoxHouse.SelectedItem.ToString() == "A1")
            {
                if (this.comboBoxHouseInTasks.SelectedItem.ToString() == EnumTaskName.电芯入库_A1.ToString())
                {
                    testBusinessManager.WarehouseInOutTest.A1HouseInFirstTaskNew();
                    AddLog("申请电芯入库任务!");
                }
                else if (this.comboBoxHouseInTasks.SelectedItem.ToString() == EnumTaskName.分容入库_A1.ToString())
                {
                    testBusinessManager.WarehouseInOutTest.A1HouseInSecondTaskNew();
                    AddLog("申请分容入库任务");
                }

            }
            else if (this.comboBoxHouse.SelectedItem.ToString() == "B1")
            {
                if (this.comboBoxHouseInTasks.SelectedItem.ToString() == EnumTaskName.电芯入库_B1.ToString())
                {
                    testBusinessManager.WarehouseInOutTest.B1HouseInFirstTaskNew();
                    AddLog("申请电芯入B1库任务");
                }
                else if (this.comboBoxHouseInTasks.SelectedItem.ToString() == EnumTaskName.空料框入库.ToString())
                {
                    testBusinessManager.WarehouseInOutTest.B1HouseInSecondTaskNew();
                    AddLog("申请空料框入库任务");
                }
                else if (this.comboBoxHouseInTasks.SelectedItem.ToString() == EnumTaskName.空料框出库.ToString())
                {
                    testBusinessManager.WarehouseInOutTest.TransPort2009.DB2[0] = 1;
                    AddLog("申请空料框出库任务");
                }
            }
            AddLog("任务生成");
        }
        private void buttonHouseInA1TaskReset_Click(object sender, EventArgs e)
        {
            if (this.comboBoxHouse.SelectedItem.ToString() == "A1")
            {
                testBusinessManager.WarehouseInOutTest.A1HouseInSecondTaskClear();
            }
            else if (this.comboBoxHouse.SelectedItem.ToString() == "B1")
            {
                testBusinessManager.WarehouseInOutTest.B1HouseInSecondTaskClear();
            }
        }
        private void buttonResetStacker1001_Click(object sender, EventArgs e)
        {
            if (this.comboBoxHouse.SelectedItem.ToString() == "A1")
            {
                testBusinessManager.WarehouseInOutTest.StackerReset(1001);
            }
           else if (this.comboBoxHouse.SelectedItem.ToString() == "B1")
            {
                testBusinessManager.WarehouseInOutTest.StackerReset(1002);
            }
        }
        private void buttonDevDB_Click(object sender, EventArgs e)
        {
            
        }
      
        #endregion  
        #region 组盘模拟事件
        private void button5001Reset_Click(object sender, EventArgs e)
        {
            testBusinessManager.FillPalletTest.DevReset();
        }

        private void button5001NewTask_Click(object sender, EventArgs e)
        {
            testBusinessManager.FillPalletTest.FillPalletTaskNew();
        }
        #endregion

        #region 分拣模拟UI事件

        private void buttonGenerateGrispData_Click(object sender, EventArgs e)
        {
            int palletID = int.Parse(this.textBoxPalletID.Text);
            string taskType = this.comboBoxGripTask.Text;
            testBusinessManager.GrispTest1.GenerateTaskData(taskType, palletID);
            AddLog(taskType + "任务模拟数据生成完成");
        }

        private void buttonApplyGripTask_Click(object sender, EventArgs e)
        {
            string taskType = this.comboBoxGripTask.Text;
            if (taskType == "一次分拣")
            {
                testBusinessManager.GrispTest1.GrispTaskNew(taskType);
            }
            else if (taskType == "二次分拣")
            {
                testBusinessManager.GrispTest2.GrispTaskNew(taskType);
            }

            AddLog(taskType + "已申请");
        }
        #endregion
    }
}
