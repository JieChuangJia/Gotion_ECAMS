using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECAMSDataAccess;
using ECAMSModel;
namespace WCSTest
{
    public class HouseInOutTask
    {
        public byte TaskType { get; set; }
        public Int16 TaskCode { get; set; }
        public byte CellX { get; set; }
        public byte CellY { get; set; }
        public byte CellZ { get; set; }
    }
    public class TestWarehouseInOutBusiness:TestBusinessBase
    {
      
        #region 数据
        public ITestHouseInView view { get; set; }
        public Stacker Stacker1001
        {
            get;
            set;
        }
        public Stacker Stacker1002
        {
            get;
            set;
        }
        public TransPort TransPort2002
        {
            get;
            set;
        }
        public TransPort TransPort2004
        {
            get;
            set;
        }
        public TransPort TransPort2006
        {
            get;
            set;
        }
        public TransPort TransPort2008
        {
            get;
            set;
        }

        //public TransPort TransPort2003
        //{
        //    get;
        //    set;
        //}
        //public TransPort TransPort2005
        //{
        //    get;
        //    set;
        //}
        //public TransPort TransPort2007
        //{
        //    get;
        //    set;
        //}
        public TransPort TransPort2009
        {
            get;
            set;
        }
        /// <summary>
        /// 当前A1库的任务
        /// </summary>
        private HouseInOutTask currentTaskA1 = null;
        private HouseInOutTask currentTaskB1 = null;
        private int exeTaskCounterA1 = 0;
        private int exeTaskCounterB1 = 0;
        /// <summary>
        /// 当前任务执行阶段
        /// </summary>
        private int currentTaskPhaseA1 = 0;
        private int currentTaskPhaseB1 = 0;
        #endregion
        #region 公共接口
        //public override bool ExeBusiness()
        //{
        //    //解析db1数据，更新db2数据
        //    //1 A1入库
        //    if (TransPort2002.DB1[0] == 2)
        //    {
        //        TransPort2002.DB2[0] = 0;
        //    }
        //    if (Stacker1001.DB1[0] == 1)
        //    {
        //        //入库任务启动命令收到，保存当前任务参数，应答
        //        currentTaskA1 = new HouseInOutTask();
        //        currentTaskA1.TaskType = Stacker1001.DB1[1];
        //        currentTaskA1.TaskCode = (Int16)(Stacker1001.DB1[2] + (Stacker1001.DB1[3] << 8));
        //        currentTaskA1.CellX = Stacker1001.DB1[4];
        //        currentTaskA1.CellY = Stacker1001.DB1[5];
        //        currentTaskA1.CellZ = Stacker1001.DB1[6];
        //        Stacker1001.DB2[0] = 2;

        //    }
        //    if (currentTaskA1 != null && Stacker1001.DB2Last[0] == 2 && Stacker1001.DB1[0] == 0)
        //    {
        //        //DB1清零后开始执行任务
        //        exeTaskCounterA1 = 0;

        //        Stacker1001.DB2[0] = 1;
        //        Stacker1001.DB2[1] = 1;
        //        Stacker1001.DB2[2] = (byte)(currentTaskA1.TaskCode & 0xff);
        //        Stacker1001.DB2[3] = (byte)(currentTaskA1.TaskCode>>8);
        //        Stacker1001.DB2[4] = currentTaskA1.CellX;
        //        Stacker1001.DB2[5] = currentTaskA1.CellY;
        //        string logContent = string.Empty;
        //        if(currentTaskA1.TaskType == 5)
        //        {
        //            logContent = "A1库开始执行原始料框入库任务";
        //        }
        //        else if(currentTaskA1.TaskType == 7)
        //        {
        //            logContent = "A1库开始执行分容后再入库任务";
        //        }
        //        AddLog(logContent);
        //    }
        //    if (currentTaskA1!= null && Stacker1001.DB2[0] == 1)
        //    {
        //        exeTaskCounterA1++;
        //        string logContent=string.Empty;
        //        if(currentTaskA1.TaskType == 5)
        //        {
        //            logContent = "A1库正在执行原始料框入库任务,步骤:" + exeTaskCounterA1.ToString();
        //        }
        //        else if(currentTaskA1.TaskType == 7)
        //        {
        //            logContent = "A1库正在执行分容后再入库任务,步骤:" +exeTaskCounterA1.ToString();
        //        }
        //        AddLog(logContent);
        //    }
        //    //任务执行完毕，
        //    if (currentTaskA1 != null && exeTaskCounterA1 > 2)
        //    {
        //        Stacker1001.DB2[0] = 1;
        //        Stacker1001.DB2[1] = 2;
        //        currentTaskA1 = null;
        //        exeTaskCounterA1 = 0;
        //        AddLog("入库执行完毕");
        //    }
        //    if (Stacker1001.DB1[0] == 2)
        //    {
        //        Array.Clear(Stacker1001.DB2, 0, Stacker1001.DB2.Count());
        //    }
        //    //2 B1入库

        //    //3 刷新A1入库相关DB1，DB2数据，显示在View

        //    //4 刷新B1入库相关DB1，DB2数据，显示在View

        //    return base.ExeBusiness();
        //}
        public override bool ExeBusiness()
        {
            ExeStacker1001Business();
            ExeStacker1002Business();
            return base.ExeBusiness();
        }
        /// <summary>
        /// 设置站台处是否为有货状态
        /// </summary>
        /// <param name="full"></param>
        public void SetTransPortFull(int devID,bool full)
        {
            switch (devID)
            {
                case 2002:
                    {
                        if (full)
                        {
                            TransPort2002.DB2[1] = 1;
                        }
                        else
                        {
                            TransPort2002.DB2[1] = 0;
                        }
                        break;

                    }
                case 2004:
                    {
                        if (full)
                        {
                            TransPort2004.DB2[1] = 1;
                        }
                        else
                        {
                            TransPort2004.DB2[1] = 0;
                        }
                        break;
                    }
                case 2006:
                    {
                        if (full)
                        {
                            TransPort2006.DB2[1] = 1;
                        }
                        else
                        {
                            TransPort2006.DB2[1] = 0;
                        }
                        break;
                    }
                case 2008:
                    {
                        if (full)
                        {
                            TransPort2008.DB2[1] = 1;
                        }
                        else
                        {
                            TransPort2008.DB2[1] = 0;
                        }
                        break;
                    }
                default:
                    break;
            }
        }

        public void A1HouseInFirstTaskNew()
        {
            TransPort2002.DB2[0] = 1;
            
        }
        public void A1HouseInFirstTaskClear()
        {
            TransPort2002.DB2[0] = 0;
        }
        public void A1HouseInSecondTaskNew()
        {
            TransPort2004.DB2[0] = 1;
        }
        public void A1HouseInSecondTaskClear()
        {
            TransPort2004.DB2[0] = 0;
        }
        public void B1HouseInFirstTaskNew()
        {
            TransPort2006.DB2[0] = 1;
        }
        public void B1HouseInFirstTaskClear()
        {
            TransPort2006.DB2[0] = 0;
        }
        public void B1HouseInSecondTaskNew()
        {
            TransPort2008.DB2[0] = 1;
        }
        public void B1HouseInSecondTaskClear()
        {
            TransPort2008.DB2[0] = 0;
        }
        public void StackerReset(int devID)
        {
            if (devID == 1001)
            {
                Array.Clear(Stacker1001.DB2, 0, Stacker1001.DB2.Count());
               
                Stacker1001.DevStatusCommit();

                
            }
            else if (devID == 1002)
            {
                Array.Clear(Stacker1002.DB2, 0, Stacker1001.DB2.Count());
               
                Stacker1002.DevStatusCommit();
            }
        }
        #endregion
        #region 私有
        private void ExeStacker1001Business()
        {
            if (TransPort2002.DB1[0] == 1)
            {
                //任务已经生成
                TransPort2002.DB2[0] = 0;
            }
            if (TransPort2004.DB1[0] == 1)
            {
                TransPort2004.DB2[0] = 0;
            }
            //if (TransPort2003.DB1[0] == 1)
            //{
            //    TransPort2003.DB2[0] = 0;
            //}
            //if (TransPort2005.DB1[0] == 1)
            //{
            //    TransPort2005.DB2[0] = 0;
            //}
            switch (currentTaskPhaseA1)
            {
                case 0:
                    {
                        exeTaskCounterA1 = 0;
                        if (Stacker1001.DB1[1] == 1)
                        {
                            //信号置位：允许接收

                            Stacker1001.DB2[2] = 1;
                            if (Stacker1001.DevStatusCommit())
                            {
                                currentTaskPhaseA1++;
                            }

                        }
                        break;
                    }
                case 1:
                    {
                        //PC写入数据完成
                        if (Stacker1001.DB1[2]  == 1)
                        {
                            //信号复位：允许接收
                            Stacker1001.DB2[2] = 0;

                            if (Stacker1001.DevStatusCommit())
                            {
                                currentTaskPhaseA1++;
                            }
                        }
                        break;
                    }
                case 2:
                    {
                        //开始取数据，取完后信号置位：取数据完成
                        currentTaskA1 = new HouseInOutTask();
                        currentTaskA1.TaskType = Stacker1001.DB1[4];
                        currentTaskA1.TaskCode = (Int16)(Stacker1001.DB1[5] + (Stacker1001.DB1[6] << 8));
                        currentTaskA1.CellX = Stacker1001.DB1[7];
                        currentTaskA1.CellY = Stacker1001.DB1[8];
                        currentTaskA1.CellZ = Stacker1001.DB1[9];
                        AddLog("成功接收任务，参数已保存,任务类型码："+currentTaskA1.TaskType.ToString());
                        Stacker1001.DB2[3] = 1;
                        Stacker1001.DB2[1] = 2; //设备处于工作状态
                        if (Stacker1001.DevStatusCommit())
                        {
                            currentTaskPhaseA1++;
                        }


                        break;
                    }
                case 3:
                    {
                        if (Stacker1001.DB1[2]  == 0)
                        {
                            //信号复位：取数据完成信号
                            Stacker1001.DB2[3] = 0;
                            if (Stacker1001.DevStatusCommit())
                            {
                                currentTaskPhaseA1++;
                            }
                        }
                        exeTaskCounterA1++;
                        AddLog("任务类型号"+currentTaskA1.TaskType.ToString()+"，任务执行中，执行到第 " + exeTaskCounterA1 + " 步");

                        break;
                    }
                case 4:
                    {
                        if (exeTaskCounterA1 > 5)
                        {
                            exeTaskCounterA1 = 0;
                            currentTaskPhaseA1++;

                        }
                        else
                        {
                            exeTaskCounterA1++;
                            AddLog("任务执行中，执行到第 " + exeTaskCounterA1 + " 步");
                        }
                        break;
                    }
                case 5:
                    {

                        Stacker1001.DB2[5] = (byte)(currentTaskA1.TaskCode & 0xff);
                        Stacker1001.DB2[6] = (byte)(currentTaskA1.TaskCode >> 8);
                        if (Stacker1001.DevStatusCommit())
                        {
                            currentTaskPhaseA1++;
                            AddLog("任务完成，返回任务号：" + currentTaskA1.TaskCode);
                        }
                        break;
                    }
                case 6:
                    {
                        //PLC任务完成
                        Stacker1001.DB2[4] = 1;
                        if (Stacker1001.DevStatusCommit())
                        {
                            currentTaskPhaseA1++;
                        }
                        break;
                    }
                case 7:
                    {
                        if (Stacker1001.DB1[3] == 1)
                        {
                            Array.Clear(Stacker1001.DB2, 0, Stacker1001.DB2.Count());
                            Stacker1001.DB2[1] = 1; //空闲
                            if (Stacker1001.DevStatusCommit())
                            {
                                currentTaskPhaseA1++;
                            }

                        }
                        break;
                    }
                case 8:
                    {
                        AddLog("任务" + currentTaskA1.TaskCode + "通信结束");
                        currentTaskA1 = null;
                        currentTaskPhaseA1 = 0;
                        break;
                    }
                default:
                    break;
            }
        }
        private void ExeStacker1002Business()
        {
            if (TransPort2006.DB1[0] == 1)
            {
                TransPort2006.DB2[0] = 0;
            }
            if (TransPort2008.DB1[0] == 1)
            {
                TransPort2008.DB2[0] = 0;
            }
            //if (TransPort2007.DB1[0] == 1)
            //{
            //    TransPort2007.DB2[0] = 0;
            //}
            if (TransPort2009.DB1[0] == 1)
            {
                TransPort2009.DB2[0] = 0;
            }
            switch (currentTaskPhaseB1)
            {
                case 0:
                    {
                        exeTaskCounterB1 = 0;
                        if (Stacker1002.DB1[1] == 1)
                        {
                            //信号置位：允许接收

                            Stacker1002.DB2[2] = 1;
                            if (Stacker1002.DevStatusCommit())
                            {
                                currentTaskPhaseB1++;
                            }

                        }
                        break;
                    }
                case 1:
                    {
                        if (Stacker1002.DB1[2]  == 1)
                        {
                            //信号复位：允许接收
                            Stacker1002.DB2[2] = 0;

                            if (Stacker1002.DevStatusCommit())
                            {
                                currentTaskPhaseB1++;
                            }
                        }
                        break;
                    }
                case 2:
                    {
                        //开始取数据，取完后信号置位：取数据完成
                        currentTaskB1 = new HouseInOutTask();
                        currentTaskB1.TaskType = Stacker1002.DB1[4];
                        currentTaskB1.TaskCode = (Int16)(Stacker1002.DB1[5] + (Stacker1002.DB1[6] << 8));
                        currentTaskB1.CellX = Stacker1001.DB1[7];
                        currentTaskB1.CellY = Stacker1001.DB1[8];
                        currentTaskB1.CellZ = Stacker1001.DB1[9];
                        AddLog("成功接收任务，参数已保存,任务开始启动");
                        Stacker1002.DB2[3] = 1;
                        Stacker1002.DB2[1] = 2; //设备处于工作状态
                        if (Stacker1002.DevStatusCommit())
                        {
                            currentTaskPhaseB1++;
                        }


                        break;
                    }
                case 3:
                    {
                        if (Stacker1002.DB1[2]  == 0)
                        {
                            //信号复位：取数据完成信号
                            Stacker1002.DB2[2] = 0;
                            if (Stacker1002.DevStatusCommit())
                            {
                                currentTaskPhaseB1++;
                            }
                        }
                        exeTaskCounterB1++;
                        AddLog("任务类型号" + currentTaskB1.TaskType.ToString() + "，任务执行中，执行到第 " + exeTaskCounterB1 + " 步");

                        break;
                    }
                case 4:
                    {
                        if (exeTaskCounterB1 > 5)
                        {
                            exeTaskCounterB1 = 0;
                            currentTaskPhaseB1++;

                        }
                        else
                        {
                            exeTaskCounterB1++;
                            AddLog("任务类型号" + currentTaskB1.TaskType.ToString() + "，任务执行中，执行到第 " + exeTaskCounterB1 + " 步");
                        }
                        break;
                    }
                case 5:
                    {

                        Stacker1002.DB2[5] = (byte)(currentTaskB1.TaskCode & 0xff);
                        Stacker1002.DB2[6] = (byte)(currentTaskB1.TaskCode >> 8);
                       
                        if (Stacker1002.DevStatusCommit())
                        {
                            currentTaskPhaseB1++;
                            AddLog("任务完成，返回任务号：" + currentTaskB1.TaskCode);
                        }
                        break;
                    }
                case 6:
                    {
                        Stacker1002.DB2[4] = 1;
                        if (Stacker1002.DevStatusCommit())
                        {
                            currentTaskPhaseB1++;
                        }
                        break;
                    }
                case 7:
                    {
                        if (Stacker1002.DB1[3] == 1)
                        {
                            Array.Clear(Stacker1002.DB2, 0, Stacker1001.DB2.Count());
                            Stacker1002.DB2[1] = 1; //空闲
                            if (Stacker1002.DevStatusCommit())
                            {
                                currentTaskPhaseB1++;
                            }

                        }
                        break;
                    }
                case 8:
                    {
                        AddLog("任务" + currentTaskB1.TaskCode + "通信结束");
                        currentTaskB1 = null;
                        currentTaskPhaseB1 = 0;
                        break;
                    }
                default:
                    break;
            }
        }
        #endregion

    }
}
