using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCSTest
{
    public class FillPalletTask
    {
        public byte TaskType { get; set; }
        public Int16 TaskCode { get; set; }

    }
    public class TestBusinessFillPallet : TestBusinessBase
    {
        #region 数据接口
        public XYZGriper Grisper5001 { get; set; }
        /// <summary>
        /// 当前A1库的任务
        /// </summary>
        private FillPalletTask currentTask = null;

        private int exeTaskCounter = 0;

        /// <summary>
        /// 当前任务执行阶段
        /// </summary>
        private int currentTaskPhase = 0;

        #endregion
        #region 公共接口
        public override bool ExeBusiness()
        {
            if (Grisper5001.DB1[0] == 1)
            {
                Grisper5001.DB2[0] = 0;
            }
            switch (currentTaskPhase)
            {
                case 0:
                    {
                        exeTaskCounter = 0;
                        if (Grisper5001.DB1[1] == 1)
                        {
                            //信号置位：允许接收

                            Grisper5001.DB2[2] = 1;
                            if (Grisper5001.DevStatusCommit())
                            {
                                currentTaskPhase++;
                            }

                        }
                        break;
                    }
                case 1:
                    {
                        //PC写入数据完成
                        if (Grisper5001.DB1[2] == 1)
                        {
                            //信号复位：允许接收
                            Grisper5001.DB2[2] = 0;

                            if (Grisper5001.DevStatusCommit())
                            {
                                currentTaskPhase++;
                            }
                        }
                        break;
                    }
                case 2:
                    {
                        //开始取数据，取完后信号置位：取数据完成
                        currentTask = new FillPalletTask();
                        currentTask.TaskType = Grisper5001.DB1[4];
                        currentTask.TaskCode = (Int16)(Grisper5001.DB1[5] + (Grisper5001.DB1[6] << 8));
                        AddLog("成功接收任务，参数已保存,任务开始启动");
                        Grisper5001.DB2[3] = 1;
                        Grisper5001.DB2[1] = 1; //设备处于工作状态
                        if (Grisper5001.DevStatusCommit())
                        {
                            currentTaskPhase++;
                        }
                        break;
                    }
                case 3:
                    {
                        if (Grisper5001.DB1[2] == 0)
                        {
                            //信号复位：取数据完成信号
                            Grisper5001.DB2[3] = 0;
                            if (Grisper5001.DevStatusCommit())
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
                        if (exeTaskCounter > 5)
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

                        Grisper5001.DB2[5] = (byte)(currentTask.TaskCode & 0xff);
                        Grisper5001.DB2[6] = (byte)(currentTask.TaskCode >> 8);
                        //条码扫描结果
                        for (int i = 0; i < 48; i++)
                        {
                            Grisper5001.DB2[7 + 2 * i] = (byte)(i + 1);
                            Grisper5001.DB2[7 + 2 * i + 1] = 0;
                        }
                        if (Grisper5001.DevStatusCommit())
                        {
                            currentTaskPhase++;
                            AddLog("任务完成，返回任务号：" + currentTask.TaskCode);
                        }
                        break;
                    }
                case 6:
                    {
                        Grisper5001.DB2[4] = 1;
                        if (Grisper5001.DevStatusCommit())
                        {
                            currentTaskPhase++;
                        }
                        break;
                    }
                case 7:
                    {
                        if (Grisper5001.DB1[3] == 1)
                        {
                            Array.Clear(Grisper5001.DB2, 0, Grisper5001.DB2.Count());
                            Grisper5001.DB2[1] = 1;
                            if (Grisper5001.DevStatusCommit())
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
        public void DevReset()
        {
            Array.Clear(this.Grisper5001.DB2, 0, this.Grisper5001.DB2.Count());
            this.currentTaskPhase = 0;
        }
        public void FillPalletTaskNew()
        {
            this.Grisper5001.DB2[0] = 1;
            this.Grisper5001.DevStatusCommit();
        }
        #endregion
    }
}
