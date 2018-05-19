using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using ECAMSModel;
using ECAMSDataAccess;

namespace PLCControl
{
    /// <summary>
    /// 巷道式堆垛机
    /// </summary>
    public class ECAMSStacker:ECAMSDevBase
    {
        #region 私有数据
      
        /// <summary>
        /// 出入库任务计时器（计数实现），作为任务调度的参考，先按计数大小取任务，再按先出后入的原则取任务。
        /// </summary>
        private IDictionary<EnumTaskName, Int64> taskCounterDic = new Dictionary<EnumTaskName, Int64>();
        private IrfidRW rfidRWA1 = null;
        private IrfidRW rfidRWA2 = null;
        private IrfidRW rfidRWB1 = null;
        private OCVPalletBll ocvPalletBll = new OCVPalletBll();
        private OCVBatteryBll ocvBatteryBll = new OCVBatteryBll();
        private TB_Tray_indexBll gxPalletBll = new TB_Tray_indexBll();
        private TB_Batch_IndexBll gxBatchBll = new TB_Batch_IndexBll();
        private TB_After_GradeDataBll gxBatteryBll = new TB_After_GradeDataBll();
        private View_StockListDetailBll viewStockListBll = new View_StockListDetailBll();
        #endregion

        public ECAMSStacker(ECAMSDataAccess.DeviceModel devModel, IPlcRW plcRW, DeviceBll devBll, ControlInterfaceBll ctlInterfaceBll, ControlTaskBll ctlTaskBll, LogBll logBll)
            : base(devModel, plcRW, devBll, ctlInterfaceBll, ctlTaskBll, logBll)
        {
            switch (devModel.DeviceID)
            {
                case "1001":
                    {
                        devName = "A1库堆垛机";
                        break;
                    }
                case "1002":
                    {
                        devName = "B1库堆垛机";
                        break;
                    }
                default:
                    break;
            }
            rfidRWA1 = ECAMWCS.rfidRWDic[2];
            rfidRWA2 = ECAMWCS.rfidRWDic[3];
            rfidRWB1 = ECAMWCS.rfidRWDic[6];
            if (devModel.DeviceID == "1001")
            {
                taskCounterDic[EnumTaskName.电芯出库_A1] = 0;
                taskCounterDic[EnumTaskName.分容出库_A1] = 0;
                taskCounterDic[EnumTaskName.分容入库_A1] = 0;
                taskCounterDic[EnumTaskName.电芯入库_A1] = 0;
            }
            else if (devModel.DeviceID == "1002")
            {
                taskCounterDic[EnumTaskName.电芯出库_B1] = 0;
                taskCounterDic[EnumTaskName.空料框出库] = 0;
                taskCounterDic[EnumTaskName.电芯入库_B1] = 0;
                taskCounterDic[EnumTaskName.空料框入库] = 0;
                taskCounterDic[EnumTaskName.空料框直接返线] = 0;
            }

           
         
        }
        #region 数据
        #endregion
        #region 重写虚函数
        protected override void AllocDevComAddrsDB1()
        {
            base.AllocDevComAddrsDB1();
            int db1ID = this.dicCommuDataDB1.Count();
            if (!this.dicCommuDataDB1.Keys.Contains(db1ID))
            {
                return;
            }
            DevCommDatatype commLastObj = this.dicCommuDataDB1[db1ID];
            if (commLastObj == null)
            {
                return;
            }
            int plcAddrStart = int.Parse(commLastObj.DataAddr.Substring(1)) + 1;

            //配置排号
            string dbName = "D";
            DevCommDatatype commData = null;
            db1ID++;
            commData = new DevCommDatatype();
            commData.CommuID = db1ID++;
            commData.CommuMethod = EnumCommMethod.PLC_MIT_COMMU;
            commData.DataByteLen = 2;
            commData.DataDescription = "库位排号";
            commData.DataTypeDef = EnumCommuDataType.DEVCOM_short;
            commData.Val = 0;
            commData.DataAddr = dbName + plcAddrStart.ToString();//
            plcAddrStart ++;
            dicCommuDataDB1[commData.CommuID] = commData;

            //配置列号
            commData = new DevCommDatatype();
            commData.CommuID = db1ID++;
            commData.CommuMethod = EnumCommMethod.PLC_MIT_COMMU;
            commData.DataByteLen = 2;
            commData.DataDescription = "库位列号";
            commData.DataTypeDef = EnumCommuDataType.DEVCOM_short;
            commData.Val = 0;
            commData.DataAddr = dbName + plcAddrStart.ToString();//
            plcAddrStart ++;
            dicCommuDataDB1[commData.CommuID] = commData;

            //配置层号
            commData = new DevCommDatatype();
            commData.CommuID = db1ID++;
            commData.CommuMethod = EnumCommMethod.PLC_MIT_COMMU;
            commData.DataByteLen = 2;
            commData.DataDescription = "库位层号";
            commData.DataTypeDef = EnumCommuDataType.DEVCOM_short;
            commData.Val = 0;
            commData.DataAddr = dbName + plcAddrStart.ToString();//
            plcAddrStart ++;
            dicCommuDataDB1[commData.CommuID] = commData;

        }
        protected override void AllocDevComAddrsDB2()
        {
            base.AllocDevComAddrsDB2();
            int db2ID = this.dicCommuDataDB2.Count();
            if (!this.dicCommuDataDB1.Keys.Contains(db2ID))
            {
                return;
            }
            DevCommDatatype commLastObj = this.dicCommuDataDB2[db2ID];
            if (commLastObj == null)
            {
                return;
            }
            int plcAddrStart = int.Parse(commLastObj.DataAddr.Substring(1)) + 1;

            
            string dbName = "D";
            DevCommDatatype commData = null;
            db2ID++;


            //1 配置小车当前列号
            commData = new DevCommDatatype();
            commData.CommuID = db2ID++;
            commData.CommuMethod = EnumCommMethod.PLC_MIT_COMMU;
            commData.DataByteLen = 2;
            commData.DataDescription = "小车当前列号";
            commData.DataTypeDef = EnumCommuDataType.DEVCOM_short;
            commData.Val = 0;
            commData.DataAddr = dbName + plcAddrStart.ToString();
            plcAddrStart ++;
            dicCommuDataDB2[commData.CommuID] = commData;

            //2 配置小车当前层号
            commData = new DevCommDatatype();
            commData.CommuID = db2ID++;
            commData.CommuMethod = EnumCommMethod.PLC_MIT_COMMU;
            commData.DataByteLen = 2;
            commData.DataDescription = "小车当前层号";
            commData.DataTypeDef = EnumCommuDataType.DEVCOM_short;
            commData.Val = 0;
            commData.DataAddr = dbName + plcAddrStart.ToString();
            plcAddrStart ++;
            dicCommuDataDB2[commData.CommuID] = commData;

            //// 出入库口状态
            //commData = new DevCommDatatype();
            //commData.CommuID = db2ID++;
            //commData.CommuMethod = EnumCommMethod.PLC_MIT_COMMU;
            //commData.DataByteLen = 2;
            //if (this.devModel.DeviceID == "1001")
            //{
            //    commData.DataDescription = "A1库入口状态";
            //}
            //else
            //{
            //    commData.DataDescription = "B1库入口状态";
            //}
            //commData.DataTypeDef = EnumCommuDataType.DEVCOM_short;
            //commData.Val = 0;
            //commData.DataAddr = dbName + plcAddrStart.ToString();
            //plcAddrStart ++;
            //dicCommuDataDB2[commData.CommuID] = commData;

            //commData = new DevCommDatatype();
            //commData.CommuID = db2ID++;
            //commData.CommuMethod = EnumCommMethod.PLC_MIT_COMMU;
            //commData.DataByteLen = 2;
            //if (this.devModel.DeviceID == "1001")
            //{
            //    commData.DataDescription = "A1库出口状态";
            //}
            //else
            //{
            //    commData.DataDescription = "B1库出口状态";
            //}
            //commData.DataTypeDef = EnumCommuDataType.DEVCOM_short;
            //commData.Val = 0;
            //commData.DataAddr = dbName + plcAddrStart.ToString();
            //plcAddrStart ++;
            //dicCommuDataDB2[commData.CommuID] = commData;

            //commData = new DevCommDatatype();
            //commData.CommuID = db2ID++;
            //commData.CommuMethod = EnumCommMethod.PLC_MIT_COMMU;
            //commData.DataByteLen = 2;
            //if (this.devModel.DeviceID == "1001")
            //{
            //    commData.DataDescription = "A1库分容入口状态";
            //}
            //else
            //{
            //    commData.DataDescription = "B1库空料框入口状态";
            //}
            //commData.DataTypeDef = EnumCommuDataType.DEVCOM_short;
            //commData.Val = 0;
            //commData.DataAddr = dbName + plcAddrStart.ToString();
            //plcAddrStart ++;
            //dicCommuDataDB2[commData.CommuID] = commData;

            //commData = new DevCommDatatype();
            //commData.CommuID = db2ID++;
            //commData.CommuMethod = EnumCommMethod.PLC_MIT_COMMU;
            //commData.DataByteLen = 2;
            //if (this.devModel.DeviceID == "1001")
            //{
            //    commData.DataDescription = "A1库分容出口状态";
            //}
            //else
            //{
            //    commData.DataDescription = "B1库二次OCV检测出口状态";
            //}
            //commData.DataTypeDef = EnumCommuDataType.DEVCOM_short;
            //commData.Val = 0;
            //commData.DataAddr = dbName + plcAddrStart.ToString();
            //plcAddrStart ++;
            //dicCommuDataDB2[commData.CommuID] = commData;


        }
        public override bool DevStatusRestore(ref string errStr)
        {
            //系统启动后，首先读取控制任务列表，检查和机器人状态是否有冲突
            if (this.devModel.DeviceID == "1001")
            {
                string[] taskTypeArray = new string[] { EnumTaskName.电芯入库_A1.ToString(), EnumTaskName.分容入库_A1.ToString(), EnumTaskName.分容出库_A1.ToString(), EnumTaskName.电芯出库_A1.ToString() };
                for (int i = 0; i < taskTypeArray.Count(); i++)
                {
                    currentTask = ctlTaskBll.GetRunningTask(taskTypeArray[i]);
                    if (currentTask != null)
                    {
                        break;
                    }
                }
            }
            else if (this.devModel.DeviceID == "1002")
            {
                string[] taskTypeArray = new string[] { EnumTaskName.电芯入库_B1.ToString(), EnumTaskName.空料框入库.ToString(), EnumTaskName.电芯出库_B1.ToString(), EnumTaskName.空料框出库.ToString() };
                for (int i = 0; i < taskTypeArray.Count(); i++)
                {
                    currentTask = ctlTaskBll.GetRunningTask(taskTypeArray[i]);
                    if (currentTask != null)
                    {
                        break;
                    }
                }
            }
            if (currentTask != null)
            {
                if(!int.TryParse(currentTask.TaskPhase,out currentTaskPhase))
                {
                    errStr = "设备" + devModel.DeviceID + "恢复任务步号失败,参数解析错误";
                    currentTaskPhase = 0;
                    return false;
                }
               
            }
            errStr = "设备" + devModel.DeviceID + " 启动后一致性检查通过";
           
            return base.DevStatusRestore(ref errStr);
        } 
        public override bool ExeBusiness()
        {
            //if (!ExeBusinesAtInPut())
            //{
            //    AddLog("入库口业务流程出现错误", EnumLogType.错误);
            //    return false;
            //}
            if (this.devModel.DeviceID == "1001")
            {
                
                TaskRequire_1001();
            }
            else if (this.devModel.DeviceID == "1002")
            {
                TaskRequire_1002();
            }
            if (!base.ExeBusiness())
            {
                //AddLog("Test: base.ExeBusiness() 返回错误", EnumLogType.提示);
                return false;
            }

            #region 2 从控制任务列表中取新任务
            if (this.currentTask == null && this.devModel.DeviceStatus == EnumDevStatus.空闲.ToString())
            {
                //两种出库任务，两种入库任务，按一定规则取任务
                //ControlTaskModel ctlTaskModel = null;
               
                if (this.devModel.DeviceID == "1001")
                {
                    taskCounterDic[EnumTaskName.电芯入库_A1]++;
                    taskCounterDic[EnumTaskName.分容入库_A1]++;
                    taskCounterDic[EnumTaskName.分容出库_A1]++;
                    taskCounterDic[EnumTaskName.电芯出库_A1]++;

                    //A1库堆垛机执行任务
                    currentTask = Stacker1001_GetTask();
                  
                    
                }
                else if (this.devModel.DeviceID == "1002")
                {
                    taskCounterDic[EnumTaskName.电芯入库_B1]++;
                    taskCounterDic[EnumTaskName.空料框入库]++;
                    taskCounterDic[EnumTaskName.电芯出库_B1]++;
                    taskCounterDic[EnumTaskName.空料框出库]++;
                    taskCounterDic[EnumTaskName.空料框直接返线]++;
                    //B1库堆垛机执行任务
                    currentTask = Stacker1002_GetTask();
                  
                }
            }
            #endregion
            #region 3任务通信
            switch (currentTaskPhase)
            {
                case 0:
                    {
                        currentTaskDescribe = "即将开始任务通信";
                        if (currentTask != null && this.devModel.DeviceStatus == EnumDevStatus.空闲.ToString())
                        {
                            startWriteParam = true; //信号置位：开始写入
                            //plcDataDb1[0] = 1; 
                            if (DevCmdCommit())
                            {
                                currentTaskPhase++;
                                currentTask.TaskPhase = currentTaskPhase.ToString();
                                currentTask.TaskStatus = EnumTaskStatus.执行中.ToString();
                                ctlTaskBll.Update(currentTask);
                                currentTaskDescribe = "任务开始启动,等待‘允许接收’信号";
                                currentTaskStartTime = System.DateTime.Now;
                            }
                            else
                            {
                                AddLog("给设备" + this.devModel.DeviceID + "发送命令失败", EnumLogType.错误);
                            }
                        }
                        break;
                    }
                case 1:
                    {
                        ////准备写入数据
                        if (!recvTaskEnable)
                        {
                            //只有DB2的允许写入信号处于置位状态才允许继续下面的流程
                            break;
                        }
                        if (!WriteTaskParam(currentTask))
                        {
                            AddLog(devName+"写入任务参数失败", EnumLogType.错误);
                        }
                        else
                        {
                            if (!DevCmdCommit())
                            {
                                AddLog(devName+"发送命令失败", EnumLogType.错误);
                                return false;
                            }
                            currentTaskPhase++;
                            currentTask.TaskPhase = currentTaskPhase.ToString();
                            ctlTaskBll.Update(currentTask);
                            //if (ECAMWCS.DebugMode)
                            //{
                            ////    AddLog("任务：" + currentTask.TaskTypeName.ToString() + "参数已发送", EnumLogType.调试信息);
                            //}
                            currentTaskDescribe = "任务参数开始发送";
                           
                        }
                        break;
                    }
                case 2:
                    {
                        //信号置位：参数已发送
                       
                        writeCompleted = true;
                        if (DevCmdCommit())
                        {
                            currentTaskPhase++;
                            currentTask.TaskPhase = currentTaskPhase.ToString();
                            ctlTaskBll.Update(currentTask);
                            currentTaskDescribe = "参数已经发送完成，等待‘PLC取数据完成'";
                        }
                        else
                        {
                            AddLog(devName+"发送命令失败", EnumLogType.错误);
                        }
                        break;
                    }
                case 3:
                    {
                        //plc取任务数据完成
                        if (paramRecvOK)
                        {
                            //PLC取数据完成信号置位后，将PC的开始写入和写入完成信号复位

                            startWriteParam = false;
                            writeCompleted = false;
                            if (DevCmdCommit())
                            {
                                currentTaskPhase++;
                                currentTask.TaskPhase = currentTaskPhase.ToString();
                                ctlTaskBll.Update(currentTask);
                                currentTaskDescribe = "PLC已经成功接收到任务参数,等待'任务完成'";
                            }
                            else
                            {
                                AddLog(devName+"发送命令失败", EnumLogType.错误);
                            }
                        }
                        
                        break;
                    }
                case 4:
                    {
                        if (taskCompleted)
                        {
                            currentTaskPhase++;
                            currentTask.TaskPhase = currentTaskPhase.ToString();
                            currentTask.TaskStatus = EnumTaskStatus.已完成.ToString();
                            ctlTaskBll.Update(currentTask);
                            currentTaskDescribe = "PLC任务已经执行完成";
                            RecordPalletHisEvent(currentTask);
                              
                        }

                        break;
                    }
                case 5:
                    {
                        //信号置位：任务完成信息收到

                        taskCompletedReq = 2;
                        if (DevCmdCommit())
                        {
                            currentTaskPhase++;
                            currentTask.TaskPhase = currentTaskPhase.ToString();
                            ctlTaskBll.Update(currentTask);

                            currentTaskDescribe = "任务完成信息已经成功接收，等待PLC复位'任务完成’信号";
                        }
                        else
                        {
                            AddLog(devName+"发送命令失败", EnumLogType.错误);
                        }
                        break;
                    }
                case 6:
                    {
                        //任务完成信号复位
                        if (!taskCompleted)
                        {
                            //任务完成信号复位
                            DevCmdReset();
                            if (DevCmdCommit())
                            {
                                currentTaskPhase++;
                                currentTask.TaskPhase = currentTaskPhase.ToString();
                                ctlTaskBll.Update(currentTask);
                                currentTaskDescribe = "任务结束，PLC 任务完成信号已经复位，设备通信DB1区已经清零";
                            }
                            else
                            {
                                AddLog(devName+"发送命令失败", EnumLogType.错误);
                            }
                        }
                        break;
                    }
                case 7:
                    {

                        if (ECAMWCS.DebugMode)
                        {
                            AddLog(devName + "当前任务" + "结束", EnumLogType.调试信息);
                        }
                        currentTask = null;
                        currentTaskPhase = 0;
                        taskElapseCounter = 0;
                        currentTaskDescribe = "任务通信结束，等待执行下一个任务";
                        break;
                    }
                default:
                    break;
            }
            #endregion
            return true;
        }

        #endregion
        #region 私有
        /// <summary>
        /// A1库堆垛机取新的控制任务
        /// </summary>
        /// <returns></returns>
        private ControlTaskModel Stacker1001_GetTask()
        {
            ECAMSTransPort port2003 = WcsManager.GetDev("2003") as ECAMSTransPort;
            ECAMSTransPort port2005 = WcsManager.GetDev("2005") as ECAMSTransPort;
            int portOut2003Enable = 1;// 
            if (port2003.ReadDB2())
            {
                portOut2003Enable = int.Parse(port2003.DicCommuDataDB2[1].Val.ToString());
            }
            int portOut2005Enable = 1;// 
            if (port2005.ReadDB2())
            {
                portOut2005Enable = int.Parse(port2005.DicCommuDataDB2[1].Val.ToString());
            }
            //根据任务计数器大小排序
            EnumTaskName[] taskCounterSort = new EnumTaskName[taskCounterDic.Count];
            for (int i = 0; i < taskCounterSort.Count(); i++)
            {
                taskCounterSort[i] = taskCounterDic.ElementAt(i).Key;
            }
            //冒泡排序
            for (int i = 0; i < taskCounterSort.Count()-1; i++)
            {
                for (int j = i + 1; j < taskCounterSort.Count(); j++)
                {
                    if (taskCounterDic[taskCounterSort[i]] < taskCounterDic[taskCounterSort[j]])
                    {
                        //
                        EnumTaskName tempTaskType = taskCounterSort[i];
                        taskCounterSort[i] = taskCounterSort[j];
                        taskCounterSort[j] = tempTaskType;
                    }
                }

            }
            ControlTaskModel ctlTaskModel = null;
            //取调度任务的规则，按照任务下达的先后顺序执行，当该时刻的任务执行条件不具备时，则取下一个任务，
            for (int i = 0; i < taskCounterSort.Count(); i++)
            {
                EnumTaskName taskTypeToRun = taskCounterSort[i];
                //检查任务执行条件是否具备
                switch (taskTypeToRun)
                {
                    case EnumTaskName.电芯入库_A1:
                        {
                            ctlTaskModel = ctlTaskBll.GetTaskToRun(taskTypeToRun.ToString());
                            if (ctlTaskModel != null)
                            {
                                taskCounterDic[taskTypeToRun] = 0;
                                return ctlTaskModel;
                            }
                            
                            break;
                        }
                    case EnumTaskName.分容入库_A1:
                        {
                            ctlTaskModel = ctlTaskBll.GetTaskToRun(taskTypeToRun.ToString());
                            if (ctlTaskModel != null)
                            {
                                taskCounterDic[taskTypeToRun] = 0;
                                return ctlTaskModel;
                            }
                            break;
                        }
                    case EnumTaskName.分容出库_A1:
                        {
                            if(portOut2003Enable == 2)
                            {
                                ctlTaskModel = ctlTaskBll.GetTaskToRun(taskTypeToRun.ToString());
                                if (ctlTaskModel != null)
                                {
                                    taskCounterDic[taskTypeToRun] = 0;
                                    return ctlTaskModel;
                                }
                            }
                          
                            break;
                        }
                    case EnumTaskName.电芯出库_A1:
                        {
                           
                            if (portOut2005Enable == 2)
                            {
                               
                                ctlTaskModel = ctlTaskBll.GetTaskToRun(taskTypeToRun.ToString());
                                if (ctlTaskModel != null)
                                {
                                    taskCounterDic[taskTypeToRun] = 0;
                                    return ctlTaskModel;
                                }
                            }
                            break;
                        }
                    default:
                        break;
                }
               
            }
            return ctlTaskModel;
        }

        /// <summary>
        /// B1库堆垛机取新的控制任务
        /// </summary>
        /// <returns></returns>
        private ControlTaskModel Stacker1002_GetTask()
        {
            ECAMSTransPort port2007 = WcsManager.GetDev("2007") as ECAMSTransPort;
            //ECAMSTransPort port2008 = WcsManager.GetDev("2008") as ECAMSTransPort;
            ECAMSTransPort port2009 = WcsManager.GetDev("2009") as ECAMSTransPort;
            int portOut2007Enable = 1;
            if (port2007.ReadDB2())
            {
               portOut2007Enable= int.Parse(port2007.DicCommuDataDB2[1].Val.ToString());
            }
           
            //int portIn2008 = int.Parse(port2008.DicCommuDataDB2[1].Val.ToString());
            int portOut2009Enable = 1;//
            if (port2009.ReadDB2())
            {
                portOut2009Enable = int.Parse(port2009.DicCommuDataDB2[1].Val.ToString());
            }
           
            //根据任务计数器大小排序
            EnumTaskName[] taskCounterSort = new EnumTaskName[taskCounterDic.Count];
            for (int i = 0; i < taskCounterSort.Count(); i++)
            {
                taskCounterSort[i] = taskCounterDic.ElementAt(i).Key;
            }
            //冒泡排序
            for (int i = 0; i < taskCounterSort.Count() - 1; i++)
            {
                for (int j = i + 1; j < taskCounterSort.Count(); j++)
                {
                    if (taskCounterDic[taskCounterSort[i]] < taskCounterDic[taskCounterSort[j]])
                    {
                        //
                        EnumTaskName tempTaskType = taskCounterSort[i];
                        taskCounterSort[i] = taskCounterSort[j];
                        taskCounterSort[j] = tempTaskType;
                    }
                }

            }
            ControlTaskModel ctlTaskModel = null;
            //取调度任务的规则，按照任务下达的先后顺序执行，当该时刻的任务执行条件不具备时，则取下一个任务，
            for (int i = 0; i < taskCounterSort.Count(); i++)
            {
                EnumTaskName taskTypeToRun = taskCounterSort[i];
                //检查任务执行条件是否具备
                switch (taskTypeToRun)
                {
                    case EnumTaskName.电芯入库_B1:
                        {
                            ctlTaskModel = ctlTaskBll.GetTaskToRun(taskTypeToRun.ToString());
                            if (ctlTaskModel != null)
                            {
                                taskCounterDic[taskTypeToRun] = 0;
                                return ctlTaskModel;
                            }

                            break;
                        }
                    case EnumTaskName.空料框入库:
                        {
                            ctlTaskModel = ctlTaskBll.GetTaskToRun(taskTypeToRun.ToString());
                            if (ctlTaskModel != null)
                            {
                                taskCounterDic[taskTypeToRun] = 0;
                                return ctlTaskModel;
                            }
                            break;
                        }
                    case EnumTaskName.电芯出库_B1:
                        {
                            //ECAMSTransPort devPort = WcsManager.GetDev("2007") as ECAMSTransPort;
                            //if (devPort != null && devPort.PortStatus == EnumTransPortStatus.EMPTY)
                          //  byte outEnable = byte.Parse(dicCommuDataDB2[11].Val.ToString());
                            if(portOut2007Enable == 2)
                            {
                                ctlTaskModel = ctlTaskBll.GetTaskToRun(taskTypeToRun.ToString());
                                if (ctlTaskModel != null)
                                {
                                    taskCounterDic[taskTypeToRun] = 0;
                                    return ctlTaskModel;
                                }
                            }

                            break;
                        }
                    case EnumTaskName.空料框出库:
                        {
                            //ECAMSTransPort devPort = WcsManager.GetDev("2009") as ECAMSTransPort;
                            //if (devPort != null && devPort.PortStatus == EnumTransPortStatus.EMPTY)
                           // byte outEnable = byte.Parse(dicCommuDataDB2[9].Val.ToString());
                            if (portOut2009Enable == 2)
                            {
                                ctlTaskModel = ctlTaskBll.GetTaskToRun(taskTypeToRun.ToString());
                                if (ctlTaskModel != null)
                                {
                                    taskCounterDic[taskTypeToRun] = 0;
                                    return ctlTaskModel;
                                }
                            }
                            break;
                        }
                    //case EnumTaskName.空料框直接返线:
                    //    {
                    //        //byte inputEnable = byte.Parse(dicCommuDataDB2[10].Val.ToString());
                    //       // byte outEnable = byte.Parse(dicCommuDataDB2[9].Val.ToString());
                    //        if (portIn2008 == 2 && portOut2009Enable == 2)
                    //        {
                    //            ctlTaskModel = ctlTaskBll.GetTaskToRun(taskTypeToRun.ToString());
                    //            if (ctlTaskModel != null)
                    //            {
                    //                taskCounterDic[taskTypeToRun] = 0;
                    //                return ctlTaskModel;
                    //            }
                    //        }
                    //        break;
                    //    }
                    default:
                        break;
                }


            }
            return ctlTaskModel;
        }

        /// <summary>
        /// 根据设备编号，解析仓位的排（x），列（y），层（z),例如1-2-3
        /// </summary>
        /// <param name="devCode"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        private bool ParseXYZFromDev(string devCode, ref byte x, ref byte y, ref byte z)
        {
           
            string[] splitStr = new string[] { ",", ";", ":", "-", "|" };
            string[] strArray = devCode.Split(splitStr, StringSplitOptions.RemoveEmptyEntries);
            if (strArray.Count() != 3)
            {
                return false;
            }
            if (!byte.TryParse(strArray[0], out x))
            {
                return false;
            }
            if (!byte.TryParse(strArray[1], out y))
            {
                return false;
            }
            if (!byte.TryParse(strArray[2], out z))
            {
                return false;
            }
            return true;
        }
        private bool WriteTaskParam(ControlTaskModel ctlTask)
        {
            if (ctlTask == null)
            {
                return false;
            }
            byte taskType = 0;

            if (ctlTask.TaskTypeName == EnumTaskName.电芯入库_A1.ToString())
            {
                taskType = 5;
            }
            else if (ctlTask.TaskTypeName == EnumTaskName.分容出库_A1.ToString())
            {
                taskType = 7;
            }
            else if (ctlTask.TaskTypeName == EnumTaskName.分容入库_A1.ToString())
            {
                taskType = 6;
            }
            else if (ctlTask.TaskTypeName == EnumTaskName.电芯出库_A1.ToString())
            {
                taskType = 8;
            }
            else if (ctlTask.TaskTypeName == EnumTaskName.电芯入库_B1.ToString())
            {
                taskType = 9;
            }
            else if (ctlTask.TaskTypeName == EnumTaskName.电芯出库_B1.ToString())
            {
                taskType = 10;
            }
            else if (ctlTask.TaskTypeName == EnumTaskName.空料框入库.ToString())
            {
                taskType = 11;
            }
            else if (ctlTask.TaskTypeName == EnumTaskName.空料框出库.ToString())
            {
                taskType = 12;
            }
            else if (ctlTask.TaskTypeName == EnumTaskName.空料框直接返线.ToString())
            {
                taskType = 13;
            }
            else
            {
                AddLog("任务种类：" + ctlTask.TaskTypeName.ToString() + "不存在", EnumLogType.错误);
                return false;
            }

            //plcDataDb1[3] = taskType; 
            this.taskTypeSnd = taskType;
           
          
            byte x = 0, y = 0, z = 0;
            string xyzToParse = "";
            if (ctlTask.TaskType == EnumTaskCategory.入库.ToString())
            {
                xyzToParse = ctlTask.TargetDevice;
            }
            else if (ctlTask.TaskType == EnumTaskCategory.出库.ToString())
            {
                xyzToParse = ctlTask.StartDevice;
            }
            else
            {
                AddLog("给设备" + this.devModel.DeviceID + " 发送任务参数失败,参数解析错误1", EnumLogType.错误);
                return false;
            }
            
            if (ctlTask.TaskTypeName != EnumTaskName.空料框直接返线.ToString())
            {
                if (!ParseXYZFromDev(xyzToParse, ref x, ref y, ref z))
                {
                    AddLog("给设备" + this.devModel.DeviceID + " 发送任务参数失败,参数解析错误2", EnumLogType.错误);
                    return false;
                }
                this.dicCommuDataDB1[5].Val = x; //排
                this.dicCommuDataDB1[6].Val = y; //列
                this.dicCommuDataDB1[7].Val = z; //层
                currentTask.TaskParameter = x.ToString() + "," + y.ToString() + "," + z.ToString();
                ctlTaskBll.Update(ctlTask);
            }
            AddLog(devName + ",成功发送" + ctlTask.TaskTypeName + "参数,货位：" + x.ToString() + "-" + y.ToString() + "-" + z.ToString(), EnumLogType.提示);
            return true;
        }

        /// <summary>
        /// 根据控制任务，记录托盘的历史事件
        /// </summary>
        /// <param name="ctlTask"></param>
        /// <returns></returns>
        private bool RecordPalletHisEvent(ControlTaskModel ctlTask)
        {
            if (ctlTask == null || string.IsNullOrWhiteSpace(ctlTask.TaskParameter))
            {
                return false;
            }
          
            byte x=0, y=0, z = 0;
            if (!ParseXYZFromDev(ctlTask.TaskParameter, ref x, ref y, ref z))
            {
               
                return false;
            }
            List<string> palletIDS = new List<string>();
            if(this.devModel.DeviceID == "1001")
            {
                palletIDS = viewStockListBll.GetStockTrayList(EnumStoreHouse.A1库房, x, y, z);
            }
            else if (this.devModel.DeviceID == "1002")
            {
                palletIDS = viewStockListBll.GetStockTrayList(EnumStoreHouse.B1库房, x, y, z);
            }
            else
            {
                return false;
            }
            if (palletIDS == null || palletIDS.Count() <= 0)
            {
                return false;
            }
            foreach (string palletID in palletIDS)
            {
                if (string.IsNullOrWhiteSpace(palletID))
                {
                    continue;

                }
                if(ctlTask.TaskTypeName == EnumTaskName.电芯入库_A1.ToString())
                {
                    palletTraceBll.AddHistoryEvent(palletID, EnumOCVProcessStatus.A1库老化.ToString(), "进入A1库开始老化，下一步：等待分容出A1库", ECAMWCS.userName);
                }
                else if (ctlTask.TaskTypeName == EnumTaskName.分容出库_A1.ToString())
                {
                    palletTraceBll.AddHistoryEvent(palletID, EnumOCVProcessStatus.分容出库.ToString(), "正常分容出库，下一步：等待分容后再入A1库", ECAMWCS.userName);
                }
                else if (ctlTask.TaskTypeName == EnumTaskName.分容入库_A1.ToString())
                {
                    palletTraceBll.AddHistoryEvent(palletID, EnumOCVProcessStatus.A1库老化.ToString(), "分容入库，下一步：等待出A1库", ECAMWCS.userName);
                }
                else if (ctlTask.TaskTypeName == EnumTaskName.电芯出库_A1.ToString())
                {
                    palletTraceBll.AddHistoryEvent(palletID, EnumOCVProcessStatus.正在一次OCV检测.ToString(), "已出A1库，等待OCV3检测和一次分拣", ECAMWCS.userName);
                }
                else if (ctlTask.TaskTypeName == EnumTaskName.电芯入库_B1.ToString())
                {
                    palletTraceBll.AddHistoryEvent(palletID, EnumOCVProcessStatus.B1库静置10天.ToString(), "已入B1库，等待静置时间到出库", ECAMWCS.userName);
                }
                else if (ctlTask.TaskTypeName == EnumTaskName.电芯出库_B1.ToString())
                {
                    palletTraceBll.AddHistoryEvent(palletID, EnumOCVProcessStatus.正在二次OCV检测.ToString(), "已出B1库，等待出库做OCV4检测和二次分拣", ECAMWCS.userName);
                }
                else
                {
                   continue;
                }

            }
            return true;
        }
        private void TaskRequire_1001_Input_1()
        {
            ECAMSTransPort port2002 = WcsManager.GetDev("2002") as ECAMSTransPort;
            if (!port2002.ReadDB2())
            {
                return;
            }
            //1 入库任务申请
            int reqIn = int.Parse(port2002.DicCommuDataDB2[1].Val.ToString());//byte.Parse(dicCommuDataDB2[8].Val.ToString());
            if (reqIn == 2)
            {
                string strWhere = "InterfaceType='" + EnumTaskCategory.入库.ToString() + "' and DeviceCode='2002' and InterfaceStatus='" +
                    EnumCtrlInterStatus.未生成.ToString() + "' ";
                List<ControlInterfaceModel> taskApplyList = ctlInterfaceBll.GetModelList(strWhere);

                strWhere = "TaskTypeName='" + EnumTaskName.电芯入库_A1.ToString() + "' and TaskStatus<>'" + EnumTaskStatus.已完成.ToString() + "' ";
                List<ControlTaskModel> existTaskList = ctlTaskBll.GetModelList(strWhere);

                //if ((taskApplyList == null || taskApplyList.Count()<1) && (existTaskList == null || existTaskList.Count() < 1))
                if (SysExistUnCompletedTask(EnumTaskName.电芯入库_A1, EnumTaskCategory.入库, this.devModel.DeviceID))
                {
                    return;
                }
                //当前任务列表中无任务可执行，申请新的任务
                string taskCode = ctlInterfaceBll.GetNewTaskCode();
                if (ctlInterfaceBll.ExistsTask(taskCode))
                {
                    AddLog("系统错误，任务码在接口表重复：" + ECAMWCS.GetCurSourceFileName() + ",行：" + ECAMWCS.GetLineNum().ToString(), EnumLogType.错误);
                    return;
                }

                //有入库请求，先判断当前入库任务是否正在执行或待执行的

                //读入口处待入库的料框数量
                Queue<string> palletQueue = ECAMWCS.PalletInputDeque["2002"];

                string palletIDList = "";
                int palletNum = int.Parse(port2002.DicCommuDataDB2[2].Val.ToString());
                if (palletNum < 1 || palletNum > 2)
                {
                    string errStr = port2002.devName +"PLC 返回的托盘数量错误："+palletNum.ToString()+"，任务申请失败!";
                    AddLog(errStr, EnumLogType.错误);
                    return;
                }
                if (palletNum > palletQueue.Count)
                {
                    string errStr = port2002.devName + "PLC 返回的托盘数量错误：" + palletNum.ToString() + ", 入口缓存数量不足："+palletQueue.Count.ToString()+"，请修正缓存，任务申请失败!";
                    AddLog(errStr, EnumLogType.错误);
                    return;
                }
                int inputNum = palletNum;//Math.Min(palletNum, palletQueue.Count);
               
               // int inputNum = palletQueue.Count;// Math.Min(palletNum, palletQueue.Count);
                //if (inputNum <= 0)
                //{
                //    port2002.DicCommuDataDB1[3].Val = 3;
                //    port2002.DevCmdCommit();
                //    AddLog("A1库入口处缓存的料框数据为空，‘电芯入库_A1’任务生成失败", EnumLogType.错误);
                //    return;
                //}
                List<string> palletList = palletQueue.ToList();

                for (int i = 0; i < inputNum; i++)
                {
                    string palletID = palletList[i];// palletQueue.Dequeue();
                    //上传到远程数据库
                    //上传装载信息到国轩数据库
                    OCVPalletModel palletModel = ocvPalletBll.GetModel(palletID);
                    if (palletModel == null)
                    {
                        AddLog(devName + ",装载错误，本地数据库不存在托盘号：" + palletID, EnumLogType.错误);

                        return;
                    }
                    if (gxPalletBll.Exists(palletID))
                    {
                        AddLog(devName + ",装载错误,国轩数据库中托盘号已存在：" + palletID, EnumLogType.错误);
                        return;
                    }
                    List<OCVBatteryModel> batteryList = ocvBatteryBll.GetModelList(" palletID='" + palletID + "' ");
                    if (batteryList == null || batteryList.Count <= 0)
                    {
                        AddLog(devName + ",装载错误,托盘绑定的电芯为空，托盘号：" + palletID, EnumLogType.错误);

                        return;
                    }
                    //解析批次信息
                    string batchID = palletModel.batchID;
                    if (string.IsNullOrWhiteSpace(batchID))
                    {
                        AddLog(devName + ",装载错误，托盘号:" + palletID + ",批次为空：" + batchID, EnumLogType.错误);
                        return;
                    }
                    if (!gxBatchBll.Exists(batchID))
                    {
                        AddLog(devName + ",装载错误，托盘号:" + palletID + ",不存在该批次：" + batchID, EnumLogType.错误);
                        return;
                    }
                }
                for (int i = 0; i < inputNum; i++)
                {
                    string reStr = "";
                    string palletID = palletQueue.Dequeue();
                    OCVPalletModel palletModel = ocvPalletBll.GetModel(palletID);
                    if (palletModel == null)
                    {
                        return;
                    }
                    string batchID = palletModel.batchID;
                    if (string.IsNullOrWhiteSpace(batchID))
                    {
                        return;
                    }
                    List<OCVBatteryModel> batteryList = ocvBatteryBll.GetModelList(" palletID='" + palletID + "' ");
                    if (batteryList == null || batteryList.Count <= 0)
                    {
                        return;
                    }
                    if (!UploadBatteryInfo(palletID, batchID, batteryList, ref reStr))
                    {
                        AddLog(devName + "装载错误，上传到国轩数据库失败,托盘号:" + palletID + "," + reStr, EnumLogType.错误);
                        return;
                    }
                    palletIDList += (palletID + ",");
                }
                if (string.IsNullOrWhiteSpace(palletIDList))
                {
                    return;
                }
                palletIDList = palletIDList.Substring(0, palletIDList.Length - 1);
                ControlInterfaceModel taskReq = new ControlInterfaceModel();
                taskReq.CreateTime = System.DateTime.Now;
                taskReq.DeviceCode = "2002";
                taskReq.InterfaceParameter = palletIDList;
                taskReq.InterfaceStatus = EnumCtrlInterStatus.未生成.ToString();
                taskReq.InterfaceType = EnumTaskCategory.入库.ToString();
                taskReq.Remarks = "电芯入库_A1";
                taskReq.TaskCode = taskCode;
                ctlInterfaceBll.Add(taskReq);
                // enableRequireNewTask = false;
                if (ECAMWCS.DebugMode)
                {
                    string logInfo = "PLC申请新的'电芯入库_A1'任务，托盘号："+palletIDList;
                    AddLog(logInfo, EnumLogType.调试信息);
                }
                port2002.DicCommuDataDB1[3].Val = 2;
                port2002.DevCmdCommit();   
            }
            else if (reqIn == 1)
            {
                port2002.DicCommuDataDB1[3].Val = 1;
                port2002.DevCmdCommit();
            }
        }
        private void TaskRequire_1001_Input_2()
        {
            //2 分容后再入库任务申请
            ECAMSTransPort port2004 = WcsManager.GetDev("2004") as ECAMSTransPort;
            if (!port2004.ReadDB2())
            {
                return;
            }
            int reqIn = int.Parse(port2004.DicCommuDataDB2[1].Val.ToString());//byte.Parse(dicCommuDataDB2[10].Val.ToString());
            if (reqIn == 2)
            {

                //有入库请求，先判断当前入库任务是否正在执行或待执行的

                if (SysExistUnCompletedTask(EnumTaskName.分容入库_A1, EnumTaskCategory.入库, this.devModel.DeviceID))
                {
                    return;
                }

                string taskCode = ctlInterfaceBll.GetNewTaskCode();
                if (ctlInterfaceBll.ExistsTask(taskCode))
                {
                    AddLog("系统错误，任务码在接口表重复：" + ECAMWCS.GetCurSourceFileName() + ",行：" + ECAMWCS.GetLineNum().ToString(), EnumLogType.错误);
                    return;
                }


                Queue<string> palletQueue = ECAMWCS.PalletInputDeque["2004"];

                string palletIDList = "";
               // int inputNum = palletQueue.Count;// Math.Min(palletNum, palletQueue.Count);
                int palletNum = int.Parse(port2004.DicCommuDataDB2[2].Val.ToString());
               
                if (palletNum < 1 || palletNum > 2)
                {
                    string errStr = port2004.devName + "PLC 返回的托盘数量错误：" + palletNum.ToString() + "，申请错误失败!";
                    AddLog(errStr, EnumLogType.错误);
                    return;
                }
                if (palletNum > palletQueue.Count)
                {
                    string errStr = port2004.devName + "PLC 返回的托盘数量错误：" + palletNum.ToString() + ", 入口缓存数量不足：" + palletQueue.Count.ToString() + "，请修正缓存，任务申请失败!";
                    AddLog(errStr, EnumLogType.错误);
                    return;
                }
                int inputNum = palletNum;//Math.Min(palletNum, palletQueue.Count);
                //if (inputNum <= 0)
                //{
                //    port2004.DicCommuDataDB1[3].Val = 3;
                //    port2004.DevCmdCommit();
                //    AddLog("A1库分入口处缓存的料框数据为空，‘分容后入A1库’任务生成失败", EnumLogType.错误);
                //    return;
                //}


                for (int i = 0; i < inputNum; i++)
                {
                    string id = palletQueue.Dequeue();
                    palletIDList += (id + ",");
                }
                palletIDList = palletIDList.Substring(0, palletIDList.Length - 1);
                ControlInterfaceModel taskReq = new ControlInterfaceModel();
                taskReq.CreateTime = System.DateTime.Now;
                taskReq.DeviceCode = "2004";
                taskReq.InterfaceParameter = palletIDList;
                taskReq.InterfaceStatus = EnumCtrlInterStatus.未生成.ToString();
                taskReq.InterfaceType = EnumTaskCategory.入库.ToString();
                taskReq.Remarks = "电芯分容后重新入A1库";
                taskReq.TaskCode = taskCode;
                ctlInterfaceBll.Add(taskReq);

                if (ECAMWCS.DebugMode)
                {
                    string logInfo = "PLC申请‘分容后入A1库'任务,托盘号：" + palletIDList;
                    AddLog(logInfo, EnumLogType.调试信息);
                }
                port2004.DicCommuDataDB1[3].Val = 2;
                port2004.DevCmdCommit();
 
            }
            else if (reqIn == 1)
            {
                port2004.DicCommuDataDB1[3].Val = 1;
                port2004.DevCmdCommit();
            }
        }
        private void TaskRequire_1001()
        {
            TaskRequire_1001_Input_1();
            TaskRequire_1001_Input_2();

        }
        private void TaskRequire_1002_Input_1()
        {
            //1 入库任务申请
            ECAMSTransPort port2006 = WcsManager.GetDev("2006") as ECAMSTransPort;
            if (!port2006.ReadDB2())
            {
                return;
            }
            int reqIn = int.Parse(port2006.DicCommuDataDB2[1].Val.ToString());//byte.Parse(dicCommuDataDB2[8].Val.ToString());
            if (reqIn == 2)
            {
                //有入库请求，先判断当前入库任务是否正在执行或待执行的

                if (SysExistUnCompletedTask(EnumTaskName.电芯入库_B1, EnumTaskCategory.入库, "2006"))
                {
                    return;
                }

                //先读卡，查询批次，添加到任务接口的参数字段
                // rfidRWB1
                string taskCode = ctlInterfaceBll.GetNewTaskCode();
                if (ctlInterfaceBll.ExistsTask(taskCode))
                {
                    AddLog("系统错误，任务码在接口表重复：" + ECAMWCS.GetCurSourceFileName() + ",行：" + ECAMWCS.GetLineNum().ToString(), EnumLogType.错误);
                    return;
                }

                Queue<string> palletQueue = ECAMWCS.PalletInputDeque["2006"];
                
                string palletIDList = "";
                int palletNum = int.Parse(port2006.DicCommuDataDB2[2].Val.ToString());
               
                if (palletNum < 1 || palletNum > 6)
                {
                    string errStr = port2006.devName + "PLC 返回的托盘数量错误：" + palletNum.ToString() + "，申请错误失败!";
                    AddLog(errStr, EnumLogType.错误);
                    return;
                }
                if (palletNum > palletQueue.Count)
                {
                    string errStr = port2006.devName + "PLC 返回的托盘数量错误：" + palletNum.ToString() + ", 入口缓存数量不足：" + palletQueue.Count.ToString() + "，请修正缓存，任务申请失败!";
                    AddLog(errStr, EnumLogType.错误);
                    return;
                }
                int inputNum = palletNum;//Math.Min(palletNum, palletQueue.Count);

                //int inputNum = Math.Min(palletNum, palletQueue.Count);
                //if (inputNum <= 0)
                //{
                //    port2006.DicCommuDataDB1[3].Val = 3;
                //    port2006.DevCmdCommit();
                //    AddLog("B1库入口处托盘数据为空，‘电芯入库_B1’任务生成失败", EnumLogType.错误);
                //    return;
                //}
                for (int i = 0; i < inputNum; i++)
                {
                    string id = palletQueue.Dequeue();
                    palletIDList += (id + ",");
                }
                palletIDList = palletIDList.Substring(0, palletIDList.Length - 1);
                ControlInterfaceModel taskReq = new ControlInterfaceModel();
                taskReq.CreateTime = System.DateTime.Now;
                taskReq.DeviceCode = "2006";
                taskReq.InterfaceParameter = palletIDList;
                taskReq.InterfaceStatus = EnumCtrlInterStatus.未生成.ToString();
                taskReq.InterfaceType = EnumTaskCategory.入库.ToString();
                taskReq.Remarks = "电芯入库_B1";
                taskReq.TaskCode = taskCode;
                ctlInterfaceBll.Add(taskReq);

                if (ECAMWCS.DebugMode)
                {
                    string logInfo = "PLC申请 '电芯入库_B1' 任务,托盘号："+palletIDList;
                    AddLog(logInfo, EnumLogType.调试信息);
                }
                port2006.DicCommuDataDB1[3].Val = 2;
                port2006.DevCmdCommit();
                 
            }
            else if (reqIn == 1)
            {
                port2006.DicCommuDataDB1[3].Val = 1;
                port2006.DevCmdCommit();
            }
        }
        private void TaskRequire_1002_Input_2()
        {
            ECAMSTransPort port2008 = WcsManager.GetDev("2008") as ECAMSTransPort;
            if (!port2008.ReadDB2())
            {
                return;
            }
            int reqIn = int.Parse(port2008.DicCommuDataDB2[1].Val.ToString());
            if (reqIn == 2)
            {
                //3生成空料框入库任务
                if (SysExistUnCompletedTask(EnumTaskName.空料框入库, EnumTaskCategory.入库, "2008"))
                {
                    return;
                }

                string taskCode = ctlInterfaceBll.GetNewTaskCode();
                if (ctlInterfaceBll.ExistsTask(taskCode))
                {
                    AddLog("系统错误，任务码在接口表重复：" + ECAMWCS.GetCurSourceFileName() + ",行：" + ECAMWCS.GetLineNum().ToString(), EnumLogType.错误);
                    return;
                }

                ControlInterfaceModel taskReq = new ControlInterfaceModel();
                taskReq.CreateTime = System.DateTime.Now;
                taskReq.DeviceCode = "2008";
                taskReq.InterfaceParameter = string.Empty;
                taskReq.InterfaceStatus = EnumCtrlInterStatus.未生成.ToString();
                taskReq.InterfaceType = EnumTaskCategory.入库.ToString();
                taskReq.Remarks = "空料框入库_B1";
                taskReq.TaskCode = taskCode;
                ctlInterfaceBll.Add(taskReq);

                if (ECAMWCS.DebugMode)
                {
                  
                    AddLog("B1库PLC申请 ‘空料框入库_B1' 任务", EnumLogType.调试信息);
                }
                port2008.DicCommuDataDB1[1].Val = 2;
                port2008.DevCmdCommit();
                
            }
            else if (reqIn == 1)
            {
                port2008.DicCommuDataDB1[1].Val = 1;
                port2008.DevCmdCommit();
            }
        }
        private void TaskRequire_1002_OutputEmptyPallet()
        {
            //2 空料框出库申请
            ECAMSTransPort port2009 = WcsManager.GetDev("2009") as ECAMSTransPort;
            if (!port2009.ReadDB2())
            {
                return;
            }
            int reqOut = int.Parse(port2009.DicCommuDataDB2[1].Val.ToString());//byte.Parse(dicCommuDataDB2[9].Val.ToString());
            if (reqOut == 2)
            {
                if (SysExistUnCompletedTask(EnumTaskName.空料框出库, EnumTaskCategory.出库, "2009"))
                {
                    return;
                }

                string taskCode = ctlInterfaceBll.GetNewTaskCode();
                if (ctlInterfaceBll.ExistsTask(taskCode))
                {
                    AddLog("系统错误，任务码在接口表重复：" + ECAMWCS.GetCurSourceFileName() + ",行：" + ECAMWCS.GetLineNum().ToString(), EnumLogType.错误);
                    return;
                }

                ControlInterfaceModel taskReq = new ControlInterfaceModel();
                taskReq.CreateTime = System.DateTime.Now;
                taskReq.DeviceCode = "2009";
                taskReq.InterfaceParameter = string.Empty;
                taskReq.InterfaceStatus = EnumCtrlInterStatus.未生成.ToString();
                taskReq.InterfaceType = EnumTaskCategory.出库.ToString();
                taskReq.Remarks = "空料框出库";
                taskReq.TaskCode = taskCode;
                ctlInterfaceBll.Add(taskReq);

                if (ECAMWCS.DebugMode)
                {
                    AddLog("B1库PLC申请 '空料框出库' 任务", EnumLogType.调试信息);
                }
                
                
            }
        }
        private void TaskRequire_1002()
        {
            TaskRequire_1002_Input_1();
            TaskRequire_1002_OutputEmptyPallet();
            TaskRequire_1002_Input_2();

          

          
           
        }

        
        /// <summary>
        /// 入库口流程业务逻辑，判断是否同批次，若同批次则放行
        /// </summary>
        /// <returns></returns>
        public bool ExeBusinesAtInPut()
        {
            
            string[] devList = null;
            IrfidRW[] readerList = null;
            if (this.devModel.DeviceID == "1001")
            {
                devList = new string[]{"2002","2004"};
                readerList = new IrfidRW[] { rfidRWA1, rfidRWA2};
            }
            else if (this.devModel.DeviceID == "1002")
            {
                devList = new string[] { "2006" };
                readerList = new IrfidRW[] {rfidRWB1 };
            }
            for (int i = 0; i < devList.Count(); i++)
            {
                string devID = devList[i];
                ECAMSTransPort port = WcsManager.GetDev(devList[i]) as ECAMSTransPort;
                if (!port.ReadDB2())
                {
                    continue;
                }
                //if (!port.RefreshStatusOK)
                //{
                //    continue;
                //}
                IDictionary<int, DevCommDatatype> db2 = port.DicCommuDataDB2;
                IDictionary<int, DevCommDatatype> db1 = port.DicCommuDataDB1;
                int readRfidReq = int.Parse(db2[3].Val.ToString()); //读卡请求
                int readRfidStat = int.Parse(db1[2].Val.ToString());//读卡状态，DB1
                int rfidRecvOK = int.Parse(db2[4].Val.ToString()); //收到读卡结果，应答
                int batchInfo = int.Parse(db1[1].Val.ToString());//前后是否同一批次信息
                //db1[1].Val= 1; //test
                if (rfidRecvOK == 2)
                {
                    //PLC收到读卡信息,DB1复位
                    db1[1].Val = 0;
                    db1[2].Val = 1;
                    port.DevCmdCommit();
                }
                else if (readRfidReq == 1)
                {
                    //读卡请求复位后，将DB1复位
                    db1[1].Val = 0;
                    db1[2].Val = 1;
                    port.DevCmdCommit();
                }
                else if (readRfidReq == 2)
                {
                    //有扫码请求
                    if (readRfidStat != 2)
                    {
                        //扫码,有请求后，若读卡状态已完成，不再读卡
                        byte[] recvByteArray = null;
                        string palletID = readerList[i].ReadPalletID(ref recvByteArray);
                        if (string.IsNullOrWhiteSpace(palletID))
                        {
                            db1[2].Val = 3;//扫码失败
                            AddLog(port.devName + "读卡失败", EnumLogType.提示);
                            continue;
                        }
                        //读卡记录先保存
                        RfidRdRecordModel rfidRecord = new RfidRdRecordModel();
                        rfidRecord.readingContent = palletID;
                        rfidRecord.readingTime = DateTime.Parse(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        rfidRecord.rfidReaderID = readerList[i].ReaderID;
                        rfidRecord.readerName = port.devName;
                        rfidRecordBll.Add(rfidRecord);

                        //
                        OCVPalletModel palletModel = ocvPalletBll.GetModel(palletID);
                        if (palletModel == null)
                        {
                            AddLog(port.devName + ",不可识别的托盘号：" + palletID, EnumLogType.错误);
                            db1[2].Val = 4;
                            port.DevCmdCommit();
                            continue;
                        }

                        if (devID == "2002")
                        {
                            List<OCVBatteryModel> batteryList = ocvBatteryBll.GetModelList(" palletID='" + palletID + "' ");
                            if (batteryList == null || batteryList.Count <= 0)
                            {
                                AddLog(port.devName + ",托盘绑定的电芯为空，托盘号：" + palletID, EnumLogType.错误);
                                db1[2].Val = 6;
                                port.DevCmdCommit();
                                continue;
                            }
                            //解析批次信息
                            string batchID = "";
                            batchID = palletModel.batchID;
                            if(string.IsNullOrWhiteSpace(batchID))
                            {
                                AddLog(port.devName + ",托盘号:" + palletID + ",批次信息为空：" + batchID, EnumLogType.错误);
                                db1[2].Val = 5;
                                port.DevCmdCommit();
                                continue;
                            }
                            if(!gxBatchBll.Exists(batchID))
                            {
                                AddLog(port.devName + ",托盘号:" + palletID + ",不存在该批次：" + batchID, EnumLogType.错误);
                                db1[2].Val = 5;
                                port.DevCmdCommit();
                                continue;
                            }

                        }
                        else if (devID == "2004")
                        {

                            if(!gxPalletBll.Exists(palletID))
                            {
                                AddLog(port.devName + ",国轩数据库不存在的托盘号：" + palletID, EnumLogType.错误);
                                db1[2].Val = 4;
                                port.DevCmdCommit();
                                continue;
                            }
                        }
                        else if (devID == "2006")
                        {
                            if (!gxPalletBll.Exists(palletID))
                            {
                                AddLog(port.devName + ",国轩数据库不存在的托盘号：" + palletID, EnumLogType.错误);
                                db1[2].Val = 4;
                                port.DevCmdCommit();
                                continue;
                            }
                        }
                      
                        //扫码成功,加入到队列
                        Queue<string> palletList = ECAMWCS.PalletInputDeque[devID];
                        bool repeatRead = false;
                        foreach (string existID in palletList)
                        {
                            if (string.IsNullOrWhiteSpace(existID))
                            {
                                continue;
                            }
                            if (palletID == existID)
                            {
                                repeatRead = true;
                            }
                        }
                        if (repeatRead)
                        {
                            db1[2].Val = 7;
                            port.DevCmdCommit();
                            AddLog(port.devName + "重复读卡，托盘号:" + palletID, EnumLogType.错误);
                            continue;
                        }
                        if (palletList.Count == 0)
                        {
                            //之前的列表为空
                            db1[1].Val = 2; //认为同一个批次，放行
                            port.DevCmdCommit();
                            Thread.Sleep(200);
                            if (ECAMWCS.DebugMode)
                            {
                                AddLog(port.devName + ":和上一筐同一个批次,之前入口队列缓存为空", EnumLogType.提示);
                            }
                      
                            palletList.Enqueue(palletID);
                            
                        }
                        else
                        {
                            string lastPalletID = palletList.Last().Trim();
                            //判断和上一筐是否同一个批次
                         
                            OCVPalletModel lastPallet = ocvPalletBll.GetModel(lastPalletID);
                            if (lastPallet == null)
                            {
                                 palletList.Dequeue();
                                db1[1].Val = 1;
                                port.DevCmdCommit();
                                Thread.Sleep(200);
                            }
                            else
                            {
                                string batchID = palletModel.batchID;
                                string lastBatchID = lastPallet.batchID;
                                palletList.Enqueue(palletID);  
                                if (batchID == lastBatchID)
                                {
                                    db1[1].Val = 2;
                                    port.DevCmdCommit();
                                    Thread.Sleep(200);
                                    if (ECAMWCS.DebugMode)
                                    {
                                        AddLog(port.devName + ":和上一筐同一个批次，" + lastPallet.palletID + ":" + palletModel.palletID, EnumLogType.提示);
                                    }
                                   
                                     
                                }
                                else
                                {
                                    if (ECAMWCS.DebugMode)
                                    {
                                        AddLog(port.devName + ":和上一筐不是同一个批次," + lastPallet.palletID + ":" + palletModel.palletID, EnumLogType.提示);
                                    }
                                    
                                    db1[1].Val = 1;
                                    port.DevCmdCommit();
                                    Thread.Sleep(200);
                                }
                            }
                        }
                        db1[2].Val = 2; //扫码完成
                        //if ((devID == "2002" || devID == "2004") && (palletList.Count > 2))
                        //{
                        //    //若超出缓存最大数量，则队首出队
                        //    palletList.Dequeue();
                        //}
                        //if (devID == "2006" && palletList.Count > 6)
                        //{
                        //    palletList.Dequeue();
                        //}
                        port.DevCmdCommit();
                        
                    }
                }
               
            }
            return true;
        }

        /// <summary>
        /// 上传电芯数据到国轩的数据库系统
        /// </summary>
        /// <returns></returns>
        private bool UploadBatteryInfo(string palletID, string batchID, List<OCVBatteryModel> batteryList, ref string reStr)
        {
            //  1、	查询批次信息表(TB_Batch_Index)得到批次编号和批次类型
            // 2、	插入托盘信息到托盘信息表(TB_Tray_index)中
            // 3、	更新批次信息表(TB_Batch_Index)中托盘数量和电池总数量
            // 4、	插入电池信息到电池信息表(TB_After_GradeData)
            TB_Batch_IndexModel batchModel = gxBatchBll.GetModel(batchID);
            if (batchModel == null)
            {
                reStr = "批次：" + batchID + "不存在";
                return false;
            }
            //3插入电池信息
            try
            {

                //1 检查托盘信息
                if (gxPalletBll.Exists(palletID))
                {
                    reStr = "托盘：" + palletID + "已经存在";
                    return false;
                }
                //2 坚持查电芯数据是否合格
                for (int i = 0; i <batteryList.Count() - 1; i++)
                {
                    OCVBatteryModel battery = batteryList[i];
                    int row1 = (int)battery.rowIndex;
                    int col1 = (int)battery.columnIndex;
                    if (string.IsNullOrWhiteSpace(battery.batteryID))
                    {
                        continue;
                    }
                    //if (battery.batteryID.Length != 12)
                    //{
                    //    reStr = "电芯条码错误：第" + battery.positionCode.ToString() + "个电芯条码不足12位";
                    //    return false;
                    //}
                    for (int j = i + 1; j < batteryList.Count(); j++)
                    {
                        OCVBatteryModel battery2 = batteryList[j];
                        int row2 = (int)battery2.rowIndex;
                        int col2 = (int)battery2.columnIndex;
                        if (battery2.batteryID == battery.batteryID)
                        {

                            reStr = "电芯条码有重复：第[" + row1.ToString() + "行," + col1.ToString() + "列]和[" + row2.ToString() + "行," + col2.ToString() + "列]电芯条码有重复";
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
                gxPalletBll.Add(trayModel);

                int batteryCount = 0;
                for (int i = 0; i < batteryList.Count(); i++)
                {
                    OCVBatteryModel battery = batteryList[i];
                    //if (string.IsNullOrWhiteSpace(battery.batteryID) || battery.batteryID.Length != 12)
                    //{
                    //    continue;
                    //}
                    string batteryID = battery.batteryID;
                    if (gxBatteryBll.Exists(batchID, palletID, batteryID))
                    {
                        continue;
                    }
                    TB_After_GradeDataModel batteryModel = new TB_After_GradeDataModel();
                    batteryModel.Tf_BatchID = batchID;
                    batteryModel.Tf_Batchtype = batchModel.Tf_Batchtype;
                    batteryModel.Tf_TrayId = palletID;
                    batteryModel.Tf_ChannelNo = battery.positionCode;
                    batteryModel.Tf_CellSn = batteryID;
                    if (gxBatteryBll.Add(batteryModel))
                    {
                        batteryCount++;
                    }

                }
                trayModel.Tf_CellCount = batteryCount;
                if (!gxPalletBll.Update(trayModel))
                {
                    reStr = "更新托盘数据表出现错误";
                    ocvPalletBll.Delete(palletID);
                    string strWhere = "Tf_TrayId='" + palletID + "' and Tf_BatchID='" + batchID + "' ";
                    List<TB_After_GradeDataModel> gxBatteryList = gxBatteryBll.GetModelList(strWhere);
                    foreach (TB_After_GradeDataModel battery in gxBatteryList)
                    {
                        if (battery == null)
                        {
                            continue;
                        }
                        gxBatteryBll.Delete(battery.Tf_BatchID, battery.Tf_TrayId, battery.Tf_CellSn);
                    }
                    gxPalletBll.Delete(palletID);
                    return false;
                }
                //2 更新批次信息
                batchModel.Tf_TrayCount++;
                batchModel.Tf_CellCount += trayModel.Tf_CellCount;
                if (!gxBatchBll.Update(batchModel))
                {
                    reStr = "更新批次信息失败";
                    return false;
                }
                return true;

            }
            catch (System.Exception ex)
            {
                ocvPalletBll.Delete(palletID);
                string strWhere = "Tf_TrayId='" + palletID + "' and Tf_BatchID='" + batchID + "' ";
                List<TB_After_GradeDataModel> gxBatteryList = gxBatteryBll.GetModelList(strWhere);
                foreach (TB_After_GradeDataModel battery in gxBatteryList)
                {
                    if (battery == null)
                    {
                        continue;
                    }
                    gxBatteryBll.Delete(battery.Tf_BatchID, battery.Tf_TrayId, battery.Tf_CellSn);
                }
                gxPalletBll.Delete(palletID);
                AddLog(devName + "装载流程出现异常，可能有重复的电芯条码，请检查," + ex.Message + "," + ex.StackTrace, EnumLogType.错误);
                return false;
            }

        }
        #endregion

    }
}
