using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECAMSModel;
using ECAMSDataAccess;
namespace WCSTest
{
    public class GrispTask
    {
        public byte TaskType { get; set; }
        public Int16 TaskCode { get; set; }
        public int palletID { get; set; }
        public byte[] ocvResult { get; set; }
    }
    public class TestBusinessGrisp : TestBusinessBase
    {
        #region 私有数据
        public XYZGriper Grisper { get; set; }
       // public XYZGriper Grisper5003 { get; set; }
        /// <summary>
        /// 当前A1库的任务
        /// </summary>
        private GrispTask currentTask = null;

        private int exeTaskCounter = 0;

        /// <summary>
        /// 当前任务执行阶段
        /// </summary>
        private int currentTaskPhase = 0;
        private OCVBatteryBll batteryBll = new OCVBatteryBll();
        private OCVPalletBll palletBll = new OCVPalletBll();
        #endregion
        #region 公共接口
        public override bool ExeBusiness()
        {
            if (Grisper.DB1[0] == 1)
            {
                Grisper.DB2[0] = 0;
            }
           
           
            switch (currentTaskPhase)
            {
                case 0:
                    {
                        exeTaskCounter = 0;
                        if (Grisper.DB1[1] == 1)
                        {
                            //信号置位：允许接收

                            Grisper.DB2[2] = 1;
                            if (Grisper.DevStatusCommit())
                            {
                                currentTaskPhase++;
                            }

                        }
                       
                        break;
                    }
                case 1:
                    {
                        //PC写入数据完成
                        if (Grisper.DB1[2] == 1)
                        {
                            //信号复位：允许接收
                            Grisper.DB2[2] = 0;

                            if (Grisper.DevStatusCommit())
                            {
                                currentTaskPhase++;
                            }
                        }
                        break;
                    }
                case 2:
                    {
                        //开始取数据，取完后信号置位：取数据完成
                        currentTask = new GrispTask();
                        currentTask.TaskType = Grisper.DB1[4];
                        currentTask.TaskCode = (Int16)(Grisper.DB1[5] + (Grisper.DB1[6] << 8));
                        string ocvResult = "";
                        for (int i = 7; i < 13; i++)
                        {
                            ocvResult += Grisper.DB1[i].ToString() + ",";
                        }
                        AddLog("产品不合格品参数：" + ocvResult);
                        AddLog("成功接收任务，参数已保存,任务开始启动");
                        Grisper.DB2[3] = 1;
                        Grisper.DB2[1] = 1; //设备处于工作状态
                        if (Grisper.DevStatusCommit())
                        {
                            currentTaskPhase++;
                        }
                        break;
                    }
                case 3:
                    {
                        if (Grisper.DB1[2] == 0)
                        {
                            //信号复位：取数据完成信号
                            Grisper.DB2[3] = 0;
                            if (Grisper.DevStatusCommit())
                            {
                                currentTaskPhase++;
                            }
                        }
                        exeTaskCounter++;
                        AddLog("任务类型号" + currentTask.TaskType.ToString() + "，任务执行中，执行到第 " + exeTaskCounter + " 步");
                        break;
                    }
                case 4:
                    {
                        if (exeTaskCounter > 3)
                        {
                            exeTaskCounter = 0;
                            currentTaskPhase++;

                        }
                        else
                        {
                            exeTaskCounter++;
                            AddLog("任务类型号" + currentTask.TaskType.ToString() + "，任务执行中，执行到第 " + exeTaskCounter + " 步");
                        }
                        break;
                    }
                case 5:
                    {

                        Grisper.DB2[5] = (byte)(currentTask.TaskCode & 0xff);
                        Grisper.DB2[6] = (byte)(currentTask.TaskCode >> 8);
                        if (Grisper.DevStatusCommit())
                        {
                            currentTaskPhase++;
                            AddLog("任务完成，返回任务号：" + currentTask.TaskCode);
                        }
                       
                        break;
                    }
                case 6:
                    {
                        Grisper.DB2[4] = 1;
                        if (Grisper.DevStatusCommit())
                        {
                            currentTaskPhase++;
                        }
                        break;
                    }
                case 7:
                    {
                        if (Grisper.DB1[3] == 1)
                        {
                            Array.Clear(Grisper.DB2, 0, Grisper.DB2.Count());
                            Grisper.DB2[1] = 1;//
                            if (Grisper.DevStatusCommit())
                            {
                                currentTaskPhase++;
                            }

                        }
                        break;
                    }
                case 8:
                    {
                        AddLog("任务" + currentTask.TaskCode + "通信结束");
                        currentTask = null;
                        currentTaskPhase = 0;
                        break;
                    }
                default:
                    break;
            }
            return base.ExeBusiness();
        }
        public void GrispTaskNew(string taskType)
        {
            
                this.Grisper.DB2[0] = 1;
                this.Grisper.DevStatusCommit();
           
        }
        public void GenerateTaskData(string taskType,int palletID)
        {
            if(taskType == "一次分拣")
            {
                OCVPalletModel model = palletBll.GetModel(palletID);
                model.processStatus = EnumOCVProcessStatus.一次OCV检测完成.ToString();
                palletBll.Update(model);
                for (int i = 1; i < 49; i++)
                {
                    OCVBatteryModel battery = batteryBll.GetModel(i.ToString(), palletID);
                    if (i % 2 > 0)
                    {
                        battery.hasBattery = true;
                        battery.checkResult = EnumOCVCheckResult.不良品.ToString();
                    }
                    else
                    {
                        battery.hasBattery = false;
                        battery.checkResult = EnumOCVCheckResult.良品.ToString();
                    }
                    batteryBll.Update(battery);
                }
            }
            else if (taskType == "二次分拣")
            {
                OCVPalletModel model = palletBll.GetModel(palletID);
                model.processStatus = EnumOCVProcessStatus.二次OCV检测完成.ToString();
                palletBll.Update(model);
                for (int i = 1; i < 49; i++)
                {
                    OCVBatteryModel battery = batteryBll.GetModel(i.ToString(), palletID);
                    if (i % 2 > 0)
                    {
                        
                        battery.checkResult = EnumOCVCheckResult.不良品.ToString();
                    }
                    else
                    {
                        
                        battery.checkResult = EnumOCVCheckResult.良品.ToString();
                    }
                    if (i % 4 == 0)
                    {
                        battery.hasBattery = true;
                    }
                    else
                    {
                        battery.hasBattery = false;
                    }
                    batteryBll.Update(battery);
                }
            }
            
        }
        public void DevReset(string devID)
        {
            
            Array.Clear(this.Grisper.DB2, 0, this.Grisper.DB2.Count());
           
            this.currentTaskPhase = 0;
        }
        #endregion
    }
}
