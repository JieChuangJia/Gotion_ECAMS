using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using ECAMSModel;
using ECAMSDataAccess;

namespace PLCControl
{
    public class ECAMSXYZGriper : ECAMSDevBase
    {
        #region 私有数据

        //private const int PALLETONLINE_MAX = 100000; //在线料框最大理论数量
        //private const int batteryCodeLen = 13; //电芯条码长度
        private OCVPalletBll ocvPalletBll = new OCVPalletBll();
        private OCVBatteryBll ocvBatteryBll = new OCVBatteryBll();
        private TB_Tray_indexBll gxPalletBll = new TB_Tray_indexBll();
        private TB_Batch_IndexBll gxBatchBll = new TB_Batch_IndexBll();
        private TB_After_GradeDataBll gxBatteryBll = new TB_After_GradeDataBll();
        private IrfidRW rfidRWOf5001 = null;
        private IrfidRW rfidRWOf5002 = null;
        private IrfidRW rfidRWOf5003 = null;
        
        //测试用数据,当前托盘号
      //  private int palletIDTest = 2;
        #endregion
        public ECAMSXYZGriper(ECAMSDataAccess.DeviceModel devModel, IPlcRW plcRW, DeviceBll devBll, ControlInterfaceBll ctlInterfaceBll, ControlTaskBll ctlTaskBll, LogBll logBll)
            : base(devModel, plcRW, devBll, ctlInterfaceBll, ctlTaskBll, logBll)
        {
            rfidRWOf5001 = ECAMWCS.rfidRWDic[1];
            rfidRWOf5002 = ECAMWCS.rfidRWDic[5];
            rfidRWOf5003 = ECAMWCS.rfidRWDic[8];
            switch(devModel.DeviceID)
            {
                case "5001":
                    {
                        devName = "机械手1";
                        break;
                    }
                case "5002":
                    {
                        devName = "机械手2";
                        break;
                    }
                case "5003":
                    {
                        devName = "机械手3";
                        break;
                    }
                default:
                    break;
            }
        }
        #region 重写虚函数
        protected override void CommuDataToDevStatusDB2()
        {
            base.CommuDataToDevStatusDB2();
            if (this.devModel.DeviceID == "5001")
            {
                ////取电芯条码
                
                //for (int i = 0; i < 48; i++)
                //{
               

                //}
            }
        }
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
            string dbName = "D";
            DevCommDatatype commData = null;
            db1ID++;
            int plcAddrStart = int.Parse(commLastObj.DataAddr.Substring(1)) + 1;

            if (this.devModel.DeviceID == "5002" || this.devModel.DeviceID == "5003")
            {
                for (int i = 0; i < 48; i++)
                {
                    commData = new DevCommDatatype();
                    commData.CommuID = db1ID++;
                    commData.CommuMethod = EnumCommMethod.PLC_MIT_COMMU;
                    commData.DataByteLen = 2;
                    commData.DataDescription = "电芯"+(i+1).ToString() +"合格信息,1:合格，2：不合格，3：此处无电芯";
                    commData.DataTypeDef = EnumCommuDataType.DEVCOM_short;
                    commData.Val = 0;
                    commData.DataAddr = dbName + plcAddrStart.ToString();//
                    plcAddrStart ++;
                    dicCommuDataDB1[commData.CommuID] = commData;
                }
            }
            commData = new DevCommDatatype();
            commData.CommuID = db1ID++;
            commData.CommuMethod = EnumCommMethod.PLC_MIT_COMMU;
            commData.DataByteLen = 2;
            commData.DataDescription = "读卡状态";
            commData.DataTypeDef = EnumCommuDataType.DEVCOM_short;
            commData.Val = 1;
            commData.DataAddr = dbName + plcAddrStart.ToString();//
            plcAddrStart++;
            dicCommuDataDB1[commData.CommuID] = commData;
            if(this.devModel.DeviceID == "5001" )
            {
                for (int i = 0; i < 48; i++)
                {
                    commData = new DevCommDatatype();
                    commData.CommuID = db1ID++;
                    commData.CommuMethod = EnumCommMethod.PLC_MIT_COMMU;
                    commData.DataByteLen = 2;
                    commData.DataDescription = "电芯" + (i + 1).ToString() + "合格信息,0:正常,1:重码，2：混批，3：电芯码空";
                    commData.DataTypeDef = EnumCommuDataType.DEVCOM_short;
                    commData.Val = 0;
                    commData.DataAddr = dbName + plcAddrStart.ToString();//
                    plcAddrStart++;
                    dicCommuDataDB1[commData.CommuID] = commData;
                }
            }
        }
        protected override void AllocDevComAddrsDB2()
        {
            base.AllocDevComAddrsDB2();
            int db2ID = this.dicCommuDataDB2.Count();
            if (!this.dicCommuDataDB2.Keys.Contains(db2ID))
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
            commData = new DevCommDatatype();
            commData.CommuID = db2ID++;
            commData.CommuMethod = EnumCommMethod.PLC_MIT_COMMU;
            commData.DataByteLen = 2;
            if (this.devModel.DeviceID == "5001")
            {
                commData.DataDescription = "2：装箱请求，1：无请求";
            }
            else
            {
                commData.DataDescription = "2：分拣请求，1：无请求";
            }
           
            commData.DataTypeDef = EnumCommuDataType.DEVCOM_short;
            commData.Val = 0;
            commData.DataAddr = dbName + plcAddrStart.ToString();//
            plcAddrStart ++;
            dicCommuDataDB2[commData.CommuID] = commData;
            if (this.devModel.DeviceID == "5001")
            {
                
                for (int i = 0; i < 48; i++)
                {
                    for (int j = 0; j < 7; j++)
                    {
                        commData = new DevCommDatatype();
                        commData.CommuID = db2ID++;
                        commData.CommuMethod = EnumCommMethod.PLC_MIT_COMMU;
                        commData.DataByteLen = 2;
                        commData.DataDescription = "电芯" + (i + 1).ToString() + "条码:["+(j+1).ToString()+"]";
                        commData.DataTypeDef = EnumCommuDataType.DEVCOM_short;
                        commData.Val = 0;
                        commData.DataAddr = dbName + plcAddrStart.ToString();//
                        plcAddrStart ++;
                        dicCommuDataDB2[commData.CommuID] = commData;
                    }
                    
                }
            }
            //else
            //{
            //    commData = new DevCommDatatype();
            //    commData.CommuID = db2ID++;
            //    commData.CommuMethod = EnumCommMethod.PLC_MIT_COMMU;
            //    commData.DataByteLen = 2;
            //    commData.DataDescription = "读卡请求";
            //    commData.DataTypeDef = EnumCommuDataType.DEVCOM_short;
            //    commData.Val = 1;
            //    commData.DataAddr = dbName + plcAddrStart.ToString();//
            //    plcAddrStart++;
            //    dicCommuDataDB2[commData.CommuID] = commData;
            //}
           
        }
        public override bool DevStatusRestore(ref string errStr)
        {
            if (this.devModel.DeviceID == "5001")
            {
                currentTask = ctlTaskBll.GetRunningTask(EnumTaskName.电芯装箱组盘.ToString());
            }
            else if (this.devModel.DeviceID == "5002")
            {
                currentTask = ctlTaskBll.GetRunningTask(EnumTaskName.电芯一次拣选.ToString());
            }
            else if (this.devModel.DeviceID == "5003")
            {
                currentTask = ctlTaskBll.GetRunningTask(EnumTaskName.电芯二次拣选.ToString());
            }
            if (currentTask != null)
            {
                if (!int.TryParse(currentTask.TaskPhase, out currentTaskPhase))
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
            
            if (this.devModel.DeviceID == "5001")
            {
                TaskRequire_5001();
            }
            else if (this.devModel.DeviceID == "5002")
            {
                TaskRequire_5002();
            }
            else if (this.devModel.DeviceID == "5003")
            {
                TaskRequire_5003();
            }
            if (!base.ExeBusiness())
            {
                return false;
            }
           
            bool re = false;
            if (devModel.DeviceID == "5001")
            {
                re = ExeBusinessFillPalete();
            }
            else if (devModel.DeviceID == "5002" || devModel.DeviceID == "5003")
            {
                re = ExeBusinessGrisp();
            }
            
          
            return re;
           
        }
        
        #endregion
        #region 私有功能函数
        /// <summary>
        /// 托盘解绑
        /// </summary>
        /// <param name="trayID"></param>
        private bool TrayUninstall(string trayID, ref string reStr)
        {
            reStr = "解绑托盘" + trayID + "成功";
            //服务器注销托盘
            string strWhere = string.Format("Tf_TrayId='{0}' and tf_traystat=1", trayID);
            List<TB_Tray_indexModel> trayList = gxPalletBll.GetModelList(strWhere);
            if (trayList != null && trayList.Count > 0)
            {
                TB_Tray_indexModel tray = trayList[0];
                if (tray != null)
                {
                    tray.tf_traystat = 0;
                    
                    if (!gxPalletBll.Update(tray))
                    {
                        reStr = "解绑" + trayID + "失败，更新国轩托盘信息表失败";
                        return false;
                    }
                }
            }
            //删除本地
            ocvPalletBll.Delete(trayID);
            return true;
        }
        /// <summary>
        /// 装箱组盘业务逻辑
        /// </summary>
        /// <returns></returns>
        private bool ExeBusinessFillPalete()
        {
            string reStr = "";
            #region 2从控制任务列表中取新任务，发送给PLC系统
            if (this.currentTask == null && this.devModel.DeviceStatus == EnumDevStatus.空闲.ToString())
            {
                //取新的任务
                currentTask = ctlTaskBll.GetTaskToRun(EnumTaskName.电芯装箱组盘.ToString());
               
            }
            #endregion
            #region 3任务通信
           
            //执行新任务
           // Int64 palletID = 0;
            switch (currentTaskPhase)
            {
                case 0:
                    {
                        if (currentTask != null && this.devModel.DeviceStatus == EnumDevStatus.空闲.ToString())
                        {
                            startWriteParam = true; //信号置位：开始写入
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
                                AddLog(devName+ "发送命令失败", EnumLogType.错误);
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
                        // 读托盘号
                        byte[] recvByteArray = null;
                        string rfidID = rfidRWOf5001.ReadPalletID(ref recvByteArray);
                        if (rfidID == null || rfidID == string.Empty)
                        {
                            AddLog(devName+"读卡失败", EnumLogType.错误);
                            dicCommuDataDB1[5].Val = 3; //
                            DevCmdCommit();
                            return false;
                        }
                        if (gxPalletBll.Exists(rfidID))
                        {
                            //托盘未解绑，
                            //AddLog(devName + ":托盘未解绑：" + rfidID, EnumLogType.错误);
                            //dicCommuDataDB1[5].Val = 4;
                            //return false;
                            string faildInfo = "";
                            if (!TrayUninstall(rfidID, ref faildInfo))
                            {
                                AddLog(devName + ":托盘未解绑,尝试解绑，失败：" + rfidID+","+faildInfo, EnumLogType.错误);
                                dicCommuDataDB1[5].Val = 4;
                                return false;
                            }
                        }
                        dicCommuDataDB1[5].Val = 2; //
                       
                       //记录读卡
                        RfidRdRecordModel rfidRecord = new RfidRdRecordModel();
                        rfidRecord.rfidReaderID = 1;
                        rfidRecord.readingContent = rfidID;
                        rfidRecord.readerName = "机械手1装载区";
                        rfidRecord.readingTime = System.DateTime.Now;
                        rfidRecordBll.Add(rfidRecord);
                        string palletID = rfidID;
                        
           
                        this.taskTypeSnd = 1; //
                        if (!DevCmdCommit())
                        {
                            AddLog(devName+" 发送任务参数失败,通信错误",EnumLogType.错误);
                            return false;
                        }
                        //将读到的托盘号存入任务参数，后面的时序会用到
                      
                        currentTask.TaskParameter = palletID;
                        currentTaskPhase++;
                        currentTask.TaskPhase = currentTaskPhase.ToString();
                        ctlTaskBll.Update(currentTask);
                        currentTaskDescribe = "任务参数开始发送";
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
                          
                        }
                        break;
                    }
                case 4:
                    {
                        if (taskCompleted)
                        {
                            //PLC信号置位，任务完成
                            //取48个电芯的条码,上传数据库
                            string palletID = currentTask.TaskParameter;
                            string batchID = string.Empty; 
                            string[] batteryIDS = new string[48];
                            int commID = 7;
                           // string DebugTemp = "托盘："+palletID+" 装载电芯：";
                            for (int i = 0; i < 48; i++)
                            {
                                batteryIDS[i] = "";
                                byte[] idBytes = new byte[13]; //由12位条码改为12/13位混用，modify by zwx,2015-07-22
                                byte[] byteArray = new byte[14];
                                for (int j = 0; j < 7; j++)
                                {
                                   
                                    int val = int.Parse(dicCommuDataDB2[commID].Val.ToString());
                                    byteArray[2 * j] = (byte)(val & 0xff);
                                    byteArray[2 * j + 1] = (byte)((val >> 8) & 0xff);
                                    commID++;
                                }
                                Array.Copy(byteArray, 1, idBytes, 0, 13); //由12位条码改为13位，modify by zwx,2015-07-22
                                string batteryID = System.Text.Encoding.UTF8.GetString(idBytes);
                                batteryID = batteryID.Trim();
                                batteryID = batteryID.TrimStart('\0');
                                batteryID = batteryID.TrimEnd('\0');
                                if (string.IsNullOrEmpty(batteryID) || batteryID.Length<12)
                                {
                                    AddLog(devName + "读取电芯条码错误,位置" + (i + 1).ToString() + ",读到的条码为空或不足12位：" + batteryID, EnumLogType.错误);
                                    this.dicCommuDataDB1[6 + i].Val = 3;
                                    continue;
                                    
                                }
                                //if (!System.Text.RegularExpressions.Regex.IsMatch(batteryID, @"^[a-zA-Z0-9-]{13,13}$")) //由12位条码改为13位，modify by zwx,2015-07-22
                                //{
                                //    continue;
                                //}
                                //if (string.IsNullOrWhiteSpace(batteryID) || batteryID.Length < batteryCodeLen)
                                //{
                                //    AddLog(devName+"读取电芯条码错误,位置"+(i+1).ToString()+",读到的条码为空或不足13位："+batteryID, EnumLogType.错误);
                                //    continue;
                                //}

                                if (batteryID.Length > 13)
                                {
                                    batteryID = batteryID.Substring(0, 13);
                                }
                                batteryIDS[i] = batteryID;
                               // DebugTemp += (batteryID + ";");
                               
                            }
                           // AddLog(DebugTemp, EnumLogType.调试信息); //

                            //判断是否重码
                            if(BarcodeRepetition(batteryIDS,ref reStr))
                            {
                                taskCompletedReq = 6;
                                currentTaskDescribe = "PLC任务完成:" + reStr;
                                AddLog(devName + string.Format("装载错误，托盘号:{0},{1}", palletID, reStr), EnumLogType.错误);
                                break;
                            }
                            //解析批次信息
                            int batParseRe = BatchParse(batteryIDS, ref batchID, ref reStr);
                            if(batParseRe>0)
                            {
                                if (batParseRe == 3)//批次为空
                                {
                                    taskCompletedReq = 4;
                                }
                                else if (batParseRe == 1)//批次不存在
                                {
                                    taskCompletedReq = 5;
                                }
                                else if (batParseRe == 2)　//混批
                                {
                                    taskCompletedReq = 7;
                                }
                                else
                                {
                                    taskCompletedReq = 5;
                                }
                                if (DevCmdCommit())
                                {
                                    currentTaskPhase = 6;
                                    currentTask.TaskPhase = currentTaskPhase.ToString();
                                    currentTask.TaskStatus = EnumTaskStatus.已完成.ToString();
                                    ctlTaskBll.Update(currentTask);
                                    currentTaskDescribe = "PLC任务完成:" + reStr;
                                    AddLog(devName + string.Format("装载错误，托盘号:{0},{1}",palletID,reStr), EnumLogType.错误);

                                }
                                break;
                            }

                            #region 原有批次判断逻辑
                            /*
                            for (int i = 0; i < 48; i++)
                            {
                                //if (!System.Text.RegularExpressions.Regex.IsMatch(batteryIDS[i], @"^[a-zA-Z0-9-]{13,13}$"))
                                //{
                                //    continue;
                                //}
                               // batchID = batteryIDS[i].Substring(2, 5);
                                if (string.IsNullOrEmpty(batteryIDS[i]) || batteryIDS[i].Length < 12)
                                {
                                    continue;
                                }
                                if (batteryIDS[i].Length == 12)
                                {
                                    batchID = batteryIDS[i].Substring(2, 5);
                                }
                                else
                                {
                                    batchID = batteryIDS[i].Substring(0, 7);
                                }
                               
                                if (gxBatchBll.Exists(batchID))
                                {
                                    break;
                                }
                            }
                            if (string.IsNullOrWhiteSpace(batchID))
                            {
                                taskCompletedReq = 4; //
                                if (DevCmdCommit())
                                {
                                    currentTaskPhase = 6;
                                    currentTask.TaskPhase = currentTaskPhase.ToString();
                                    currentTask.TaskStatus = EnumTaskStatus.已完成.ToString();
                                    ctlTaskBll.Update(currentTask);
                                    currentTaskDescribe = "PLC任务完成,料框电芯数据为空";
                                    AddLog(devName + ",装载错误，托盘号:" + palletID + ",电芯数据为空", EnumLogType.错误);

                                }
                               
                               
                                break;
                            }

                            if (!gxBatchBll.Exists(batchID))
                            {
                                taskCompletedReq = 5; //
                                if (DevCmdCommit())
                                {
                                    currentTaskPhase = 6;
                                    currentTask.TaskPhase = currentTaskPhase.ToString();
                                    currentTask.TaskStatus = EnumTaskStatus.已完成.ToString();
                                    ctlTaskBll.Update(currentTask);
                                    currentTaskDescribe = "PLC任务完成,批次:" + batchID + "不存在";
                                    AddLog(devName + ",装载错误，托盘号:" + palletID + ",不存在该批次：" + batchID, EnumLogType.错误);
                                }
                                break ;
                            }*/
                            #endregion
                          
                            if (ocvPalletBll.Exists(palletID))
                            {
                                if (!ocvPalletBll.Delete(palletID))
                                {
                                    AddLog(devName + ",上传电芯数据到本地数据库错误，删除已经存在的托盘信息失败，托盘ID：" + palletID, EnumLogType.错误);
                                    return false;
                                }
                            }
                            //上传到本地数据库
                            OCVPalletModel palletModel = new OCVPalletModel();
                            palletModel.palletID = palletID;
                            palletModel.batchID = batchID;
                            palletModel.loadInTime = System.DateTime.Now;
                            palletModel.processStatus = EnumOCVProcessStatus.托盘装载待入A1库.ToString();
                            ocvPalletBll.Add(palletModel);
                            for (int i = 0; i < 48; i++)
                            {
                                if (string.IsNullOrEmpty(batteryIDS[i]) || batteryIDS[i].Length < 12)
                                {
                                    continue;
                                }
                                string batteryID = batteryIDS[i];
                                if (ocvBatteryBll.Exists(batteryID, palletID))
                                {
                                    continue;
                                }
                                OCVBatteryModel battery = new OCVBatteryModel();
                                battery.batteryID = batteryID;
                                battery.checkResult = "良品"; //初始值假设为合格品
                                battery.rowIndex = i / 12 + 1;
                                battery.columnIndex = i - (battery.rowIndex - 1) * 12 + 1;
                                battery.hasBattery = true;
                                battery.palletID = currentTask.TaskParameter;
                                battery.positionCode = (i + 1);
                                if (!ocvBatteryBll.Add(battery))
                                {
                                    AddLog(devName + ",装载错误，托盘号:" + palletID + "增加电芯失败，条码：" + batteryID + ",位置：" + battery.positionCode.ToString(), EnumLogType.错误);
                                    continue;
                                }
                            }
                           
                            currentTaskPhase++;
                            currentTask.TaskPhase = currentTaskPhase.ToString();
                            currentTask.TaskStatus = EnumTaskStatus.已完成.ToString();

                            currentTask.TaskParameter = batchID; //任务完成，返回批次号
                            ctlTaskBll.Update(currentTask);
                            palletTraceBll.AddHistoryEvent(palletID, EnumOCVProcessStatus.托盘装载待入A1库.ToString(), "托盘绑定完成，下一步：等待入A1库",ECAMWCS.userName);

                            currentTaskDescribe = "PLC任务已经执行完成";
                            
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
                       // AddLog(devName + ",Test:发送‘任务完成应答信息:" + dicCommuDataDB2[3].Val.ToString(), EnumLogType.调试信息);
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
                           
                        }
                        break;
                    }
                case 7:
                    {
                        if (ECAMWCS.DebugMode)
                        {
                            AddLog(devName+"当前任务" + "结束", EnumLogType.调试信息);
                        }
                        currentTask = null;
                        currentTaskPhase = 0;
                        taskElapseCounter = 0;
                        currentTaskDescribe = "任务结束，等待执行下一个任务";
                        break;
                    }
            }
            #endregion
            return true;
        }

        /// <summary>
        /// 分拣业务逻辑
        /// </summary>
        /// <returns></returns>
        private bool ExeBusinessGrisp()
        {
           
            #region 2 从控制任务列表中取新任务，发送给PLC系统
            if (this.currentTask == null && this.devModel.DeviceStatus == EnumDevStatus.空闲.ToString())
            {
              
                if (this.devModel.DeviceID == "5002")
                {
                    this.currentTask = ctlTaskBll.GetTaskToRun(EnumTaskName.电芯一次拣选.ToString());

                }
                else if (this.devModel.DeviceID == "5003")
                {
                    this.currentTask = ctlTaskBll.GetTaskToRun(EnumTaskName.电芯二次拣选.ToString());
                }

               
            }
            #endregion
            #region 3 任务通信

            //任务通信
            switch (currentTaskPhase)
            {
                case 0:
                    {
                        if (currentTask != null && this.devModel.DeviceStatus == EnumDevStatus.空闲.ToString())
                        {
                            startWriteParam = true; //信号置位：开始写入
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
                                AddLog(devName+"发送命令失败", EnumLogType.调试信息);
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
                        IrfidRW rfidRWObj = null;
                        if (this.devModel.DeviceID == "5002")
                        {
                            rfidRWObj = rfidRWOf5002;
                        }
                        else if (this.devModel.DeviceID == "5003")
                        {
                            rfidRWObj = rfidRWOf5003;
                        }

                        byte[] recvByteArray = null;
                        string palletRfid = rfidRWObj.ReadPalletID(ref recvByteArray);
                        if (string.IsNullOrWhiteSpace(palletRfid))
                        {
                            AddLog(devName+"读RFID卡失败：", EnumLogType.错误);
                            this.dicCommuDataDB1[53].Val = 3;
                            DevCmdCommit();
                            return false;
                        }
                        RfidRdRecordModel rfidRecord = new RfidRdRecordModel();
                        rfidRecord.readingContent = palletRfid;
                        rfidRecord.readingTime = System.DateTime.Now;
                        if (this.devModel.DeviceID == "5002")
                        {
                            //读卡记录
                            rfidRecord.rfidReaderID = 5;
                            rfidRecord.readerName = "机械手2分拣区";

                        }
                        else if (this.devModel.DeviceID == "5003")
                        {
                            rfidRecord.rfidReaderID = 8;
                            rfidRecord.readerName = "机械手3分拣区";
                        }
                        rfidRecordBll.Add(rfidRecord);

                        this.taskTypeSnd = 2; ////任务类型分拣

                        byte[] ocvCheckRe = null;
                        if (devModel.DeviceID == "5002")
                        {
                            //一次分拣：剔除不良品
                            //查询OCV数据表，得到该料框内不良品位置信息
                            ocvCheckRe = ocvPalletBll.GetOcvCheckResult(palletRfid, EnumOCVProcessStatus.一次OCV检测完成.ToString());
                        }
                        else if (devModel.DeviceID == "5003")
                        {
                            //二次分拣：分拣良品和不良品
                            ocvCheckRe = ocvPalletBll.GetOcvCheckResult(palletRfid, EnumOCVProcessStatus.二次OCV检测完成.ToString());
                        }
                        if (ocvCheckRe == null)// || ocvCheckRe.Count() < 48)
                        {
                            AddLog(devName+"获取不良电芯信息失败,读取不良品数据结果为空,托盘："+palletRfid , EnumLogType.错误);
                            this.dicCommuDataDB1[53].Val= 5;
                            DevCmdCommit();
                            return false;
                            
                        }
                        if (ocvCheckRe.Count() < 48)
                        {
                            AddLog(devName + "获取不良电芯信息失败,结果不足48个，托盘："+palletRfid, EnumLogType.错误);
                            this.dicCommuDataDB1[53].Val = 5;
                            DevCmdCommit();
                            return false;
                        }
                        this.dicCommuDataDB1[53].Val = 2;
                        //Array.Copy(ocvCheckRe, 0, plcDataDb1, 7, 6);
                        for (int i = 0; i < 48; i++)
                        {
                            int commID = i + 5;
                            dicCommuDataDB1[commID].Val = ocvCheckRe[i];
                        }
                        if (!DevCmdCommit())
                        {

                            AddLog(devName+"发送任务参数失败", EnumLogType.调试信息);
                            return false;
                        }
                        currentTaskPhase++;
                        currentTask.TaskPhase = currentTaskPhase.ToString();
                        currentTask.TaskParameter = palletRfid;
                        ctlTaskBll.Update(currentTask);
                        currentTaskDescribe = "任务参数开始发送";
                        
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
                            string palletRfid = currentTask.TaskParameter;
                            if (devModel.DeviceID == "5002")
                            {
                                //更新托盘内电芯数量
                                TB_Tray_indexModel gxTray= gxPalletBll.GetModel(palletRfid);
                                if(gxTray != null)
                                {
                                    int lastCount = (int)gxTray.Tf_CellCount;
                                    int batteryCount = ocvPalletBll.GetBatteryCountAfterOCV3(palletRfid);
                                    if (batteryCount >= 0)
                                    {
                                        gxTray.Tf_CellCount = batteryCount;
                                        gxPalletBll.Update(gxTray);
                                         TB_Batch_IndexModel batchModel= gxBatchBll.GetModel(gxTray.Tf_BatchID);
                                         if (batchModel != null)
                                         {
                                             int batchDescreaseCount = lastCount - batteryCount;

                                             batchModel.Tf_CellCount -= batchDescreaseCount;
                                             gxBatchBll.Update(batchModel);
                                         }
                                        
                                    }
                                }
                                palletTraceBll.AddHistoryEvent(palletRfid, EnumOCVProcessStatus.一次分拣完成.ToString(), "一次分拣完成，下一步：等待入B1库", ECAMWCS.userName);
                            }
                            else if(devModel.DeviceID == "5003")
                            {
                                //删除OCV表内的料框组盘数据
                                palletTraceBll.AddHistoryEvent(palletRfid, EnumOCVProcessStatus.空置.ToString(), "二次分拣完成，托盘已注销，空置", ECAMWCS.userName);
                                ocvPalletBll.Delete(palletRfid);
                            }
                            
                            {
                                currentTaskPhase++;
                                currentTask.TaskPhase = currentTaskPhase.ToString();
                                currentTask.TaskStatus = EnumTaskStatus.已完成.ToString();
                                ctlTaskBll.Update(currentTask);
                                currentTaskDescribe = "PLC任务已经执行完成";
                            }
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
                            AddLog("给设备" + this.devModel.DeviceID + "发送命令失败", EnumLogType.错误);
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
                            AddLog(devName+"任务结束", EnumLogType.调试信息);
                        }
                        currentTask = null;
                        currentTaskPhase = 0;
                        taskElapseCounter = 0;
                        currentTaskDescribe = "任务结束，等待执行下一个任务";
                        break;

                    }
            }
            
            #endregion
            return true;
        }
        //private UInt32 GenerateBoxRfidCode()
        //{
        //    UInt32 rfidID = 0;
        //    Int64 currentMaxRfidCode = ocvPalletBll.GetMaxPalletID();
        //   // string newRfidCode = (currentMaxRfidCode + 1).ToString().PadLeft(10, '0');
        //    if (currentMaxRfidCode < PALLETONLINE_MAX)
        //    {
        //        rfidID = (UInt32)(currentMaxRfidCode + 1);
        //    }
        //    else
        //    {
        //        //若当前ID超出了最大设定值，则从1开始，需要检测是否存在当前的id
        //        rfidID = 1;
        //        while (ocvPalletBll.Exists(rfidID))
        //        {
        //            rfidID++;
        //        }
                
        //    }
        //    return rfidID;
        //}

        /// <summary>
        /// 解析任务参数字符串成字节数组，代表每个电芯是否合格，一个电芯用一位表示，一个字节代表8个电芯
        /// </summary>
        /// <param name="taskParameter"></param>
        /// <returns></returns>
        private bool ParseGrisptaskParameter(string taskParameter,out byte[] batteryStatus, out string boxRfidID)
        {
            batteryStatus = new byte[6];
            boxRfidID = "0";
            string[] splitStr = new string[] { ",", ";", ":", "-", "|" };
            string[] strArray = taskParameter.Split(splitStr, StringSplitOptions.RemoveEmptyEntries);
            if (strArray.Count() < 7)
            {
                return false;
            }
            boxRfidID = strArray[0];
            for (int i = 1; i < 7; i++)
            {
                if (!byte.TryParse(strArray[i], out batteryStatus[i]))
                {
                    return false;
                }
            }
            return true;
        }
        private void TaskRequire_5001()
        {
           
          
            byte reqIn = byte.Parse(dicCommuDataDB2[6].Val.ToString());
            if (reqIn == 2)
            {
                //有入库请求，先判断当前入库任务是否正在执行或待执行的
                //string strWhere = "InterfaceType='" + EnumTaskCategory.入库.ToString() + "' and DeviceCode='5001' and InterfaceStatus='" +
                //    EnumCtrlInterStatus.未生成.ToString() + "' ";
                //List<ControlInterfaceModel> taskApplyList = ctlInterfaceBll.GetModelList(strWhere);

               // strWhere = "TaskTypeName='"+EnumTaskName.电芯装箱组盘.ToString()+"' and TaskStatus<>'" + EnumTaskStatus.已完成.ToString() + "' ";
               // List<ControlTaskModel> existTaskList = ctlTaskBll.GetModelList(strWhere);
               
                //if ((taskApplyList == null || taskApplyList.Count() < 1) && (existTaskList == null || existTaskList.Count() < 1))
                if (!SysExistUnCompletedTask(EnumTaskName.电芯装箱组盘, EnumTaskCategory.入库, this.devModel.DeviceID))
                {
                    string taskCode = ctlInterfaceBll.GetNewTaskCode();
                    if (!ctlInterfaceBll.ExistsTask(taskCode))
                    {
                        ControlInterfaceModel taskReq = new ControlInterfaceModel();
                        taskReq.CreateTime = System.DateTime.Now;
                        taskReq.DeviceCode = "5001";
                        taskReq.InterfaceParameter = string.Empty;
                        taskReq.InterfaceStatus = EnumCtrlInterStatus.未生成.ToString();
                        taskReq.InterfaceType = EnumTaskCategory.入库.ToString();
                        taskReq.Remarks = "电芯装箱组盘";
                        taskReq.TaskCode = taskCode;
                        ctlInterfaceBll.Add(taskReq);

                        if (ECAMWCS.DebugMode)
                        {
                            AddLog(devName+"申请 '电芯装箱组盘' 任务", EnumLogType.调试信息);
                        }
                    }

                }
            }
        }
        private void TaskRequire_5002()
        {

            byte reqIn = byte.Parse(dicCommuDataDB2[6].Val.ToString());
            if (reqIn == 2)
            {
                //有入库请求，先判断当前入库任务是否正在执行或待执行的
                //string strWhere = "InterfaceType='" + EnumTaskCategory.入库.ToString() + "' and DeviceCode='5002' and InterfaceStatus='" +
                //    EnumCtrlInterStatus.未生成.ToString() + "' ";
                //List<ControlInterfaceModel> taskApplyList = ctlInterfaceBll.GetModelList(strWhere);

                //strWhere = "TaskTypeName='"+EnumTaskName.电芯一次拣选.ToString()+"' and TaskStatus<>'" + EnumTaskStatus.已完成.ToString() + "' ";
                //List<ControlTaskModel> existTaskList = ctlTaskBll.GetModelList(strWhere);
                //if ((taskApplyList == null || taskApplyList.Count() < 1) && (existTaskList == null || existTaskList.Count() < 1))
                if (!SysExistUnCompletedTask(EnumTaskName.电芯一次拣选, EnumTaskCategory.出库, this.devModel.DeviceID))
                {
                    string taskCode = ctlInterfaceBll.GetNewTaskCode();
                    if (!ctlInterfaceBll.ExistsTask(taskCode))
                    {
                        ControlInterfaceModel taskReq = new ControlInterfaceModel();
                        taskReq.CreateTime = System.DateTime.Now;
                        taskReq.DeviceCode = "5002";
                        taskReq.InterfaceParameter = string.Empty;
                        taskReq.InterfaceStatus = EnumCtrlInterStatus.未生成.ToString();
                        taskReq.InterfaceType = EnumTaskCategory.出库.ToString();
                        taskReq.Remarks = "电芯一次拣选";
                        taskReq.TaskCode = taskCode;
                        ctlInterfaceBll.Add(taskReq);

                        if (ECAMWCS.DebugMode)
                        {
                            AddLog(devName+"申请 '电芯一次拣选' 任务", EnumLogType.调试信息);
                        }
                    }
                }
            }
        }
        private void TaskRequire_5003()
        {
            byte reqIn = byte.Parse(dicCommuDataDB2[6].Val.ToString());
            if (reqIn == 2)
            {
                //有入库请求，先判断当前入库任务是否正在执行或待执行的
                //string strWhere = "InterfaceType='" + EnumTaskCategory.入库.ToString() + "' and DeviceCode='5003' and InterfaceStatus='" +
                //    EnumCtrlInterStatus.未生成.ToString() + "' ";
                //List<ControlInterfaceModel> taskApplyList = ctlInterfaceBll.GetModelList(strWhere);

               // strWhere = "TaskTypeName='" + EnumTaskName.电芯二次拣选.ToString() + "' and TaskStatus<>'" + EnumTaskStatus.已完成.ToString() + "' ";
                //List<ControlTaskModel> existTaskList = ctlTaskBll.GetModelList(strWhere);
               // if ((taskApplyList == null || taskApplyList.Count() < 1) && (existTaskList == null || existTaskList.Count() < 1))
                if (!SysExistUnCompletedTask(EnumTaskName.电芯二次拣选, EnumTaskCategory.出库, this.devModel.DeviceID))
                {
                    string taskCode = ctlInterfaceBll.GetNewTaskCode();
                    if (!ctlInterfaceBll.ExistsTask(taskCode))
                    {
                        ControlInterfaceModel taskReq = new ControlInterfaceModel();
                        taskReq.CreateTime = System.DateTime.Now;
                        taskReq.DeviceCode = "5003";
                        taskReq.InterfaceParameter = string.Empty;
                        taskReq.InterfaceStatus = EnumCtrlInterStatus.未生成.ToString();
                        taskReq.InterfaceType = EnumTaskCategory.出库.ToString();
                        taskReq.Remarks = "电芯二次拣选";
                        taskReq.TaskCode = taskCode;
                        ctlInterfaceBll.Add(taskReq);

                        if (ECAMWCS.DebugMode)
                        {
                            AddLog(devName+"申请 '电芯二次拣选' 任务", EnumLogType.调试信息);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 批次检测,如果有不同批，不存在的批次，均返回错误代码
        /// </summary>
        /// <param name="batteryIDS"></param>
        /// <param name="bat"></param>
        /// <returns>0：正常，1：批次不存在，2：存在不同批,3:批次为空,4:其它错误</returns>
        private int BatchParse(string[] batteryIDS, ref string batchID, ref string reStr)
        {
            int re = 0;
            try
            {
                batchID = "";
                string lastBatchID = "";
                bool batchMulti = false;
                for (int i = 0; i < batteryIDS.Count(); i++)
                {
                    //if (!System.Text.RegularExpressions.Regex.IsMatch(batteryIDS[i], @"^[a-zA-Z0-9-]{13,13}$"))
                    //{
                    //    continue;
                    //}
                    // batchID = batteryIDS[i].Substring(2, 5);
                    if (string.IsNullOrEmpty(batteryIDS[i]) || batteryIDS[i].Length < 12)
                    {
                        continue;
                    }
                    if (batteryIDS[i].Length == 12)
                    {
                        batchID = batteryIDS[i].Substring(2, 5);
                    }
                    else
                    {
                        batchID = batteryIDS[i].Substring(0, 7);
                    }
                    if(!string.IsNullOrWhiteSpace(batchID))
                    {
                        if (!string.IsNullOrWhiteSpace(lastBatchID))
                        {
                            if (batchID.ToUpper() != lastBatchID.ToUpper())
                            {
                                batchMulti = true;
                                //reStr = string.Format("存在不同批,批次1:{0},批次2：{1}", lastBatchID, batchID);
                                reStr = reStr + string.Format("{0},", i + 1);
                                this.dicCommuDataDB1[6 + i].Val = 2;
                            }
                        }
                        lastBatchID = batchID;
                    }
                    //if (gxBatchBll.Exists(batchID))
                    //{
                    //    break;
                    //}
                }
                if(batchMulti)
                {
                    re = 2;
                    reStr = "混批存在，位置：" + reStr;
                    return re;
                }
                if(string.IsNullOrWhiteSpace(batchID))
                {
                    re = 3;
                    return re;
                }
                if (!gxBatchBll.Exists(batchID))
                {
                    reStr = string.Format("批次:{0}不存在",  batchID);
                    re=1;
                    return re;
                }

                re = 0;
                return re;
            }
            catch (Exception ex)
            {
                reStr = ex.ToString();
                re = 4;
                return re;
            }
        }
        private bool BarcodeRepetition(string[] batteryIDS,ref string reStr)
        {
            reStr = "";
            int repeatCounter = 0;
            for (int i = 0; i < batteryIDS.Count() - 1; i++)
            {
                if (string.IsNullOrWhiteSpace(batteryIDS[i]))
                {
                    continue;
                }
                string batteryID = batteryIDS[i];
                for (int j = i + 1; j < batteryIDS.Count() - 1; j++)
                {
                    string targetBatteryID = batteryIDS[j];
                    if (string.IsNullOrWhiteSpace(targetBatteryID))
                    {
                        continue;
                    }
                    if (batteryID.ToUpper() == targetBatteryID.ToUpper())
                    {
                        //reStr = string.Format("第{0}个电芯跟第{1}个电芯重码，{2}", i + 1, j + 1, batteryID);
                        //return true;
                        reStr = reStr + string.Format("{0}:{1},", i + 1, j + 1);
                        repeatCounter++;
                        this.dicCommuDataDB1[6 + j].Val = 1;
                    }
                }
            }
            if (repeatCounter > 0)
            {
                reStr = "电芯码重复，位置标号：" + reStr;
                return true;
            }
            return false;
        }
        #endregion
    }
}
