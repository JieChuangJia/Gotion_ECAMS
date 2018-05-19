using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;
namespace SygoleHFReaderIF
{
    /// <summary>
    /// 返回状态枚举
    /// </summary>
    public enum EnumREStatus
    {
        RW_SUCCESS,
        RW_NO_RESPONSE, //在指定的时间内未返回应答
        RW_COMPORT_CLOSED,
        RW_NONE_USERCFG,
        RW_CMD_FAILED, //执行失败
        RW_BCC_ERROR, 
        RW_BLOCK_SIZE_ERROR,
        RW_FAILURE,
        RW_GPO_PORT_ERROR,
        RW_NOTAG_ERROR,
        RW_PARAM_ERROR,
        RW_TIMEOUT_ERROR //上位机发送的实际数据长度，不足指令中“数据长度”字段描述的长度
    }
    public enum EnumCmdCode
    {
        CMD_NONE = 0,
        CMD_READ_SBLOCK = 0x20,
        CMD_READ_MBLOCK = 0x23,
        CMD_WRITE_SBLOCK = 0x21,
        CMD_WRITE_MBLOCK =0x24,
        CMD_READ_USERCFG = 0xAE
    }
    public class ComRwBytesEventArgs : EventArgs
    {
        public byte[] buf { get; set; }
    }
    public class RFPackage
    {
        public byte Header = 0x1b;
        public byte CmdCode = 0x00;
        public byte ReaderID = 0x00;
        public byte DataStatus = 0x00;
        public byte AddrMode = 0x00; 
        public byte[] dataBuf = null;
        public byte dataLen = 0;
        public byte Bcc = 0;
        public byte[] Package2Bytes()
        {
            int len = 7+dataLen;
            
            byte[] bytesStream = new byte[len];
            int byteIndex = 0;
            bytesStream[byteIndex++] = Header;
            bytesStream[byteIndex++] = CmdCode;
            bytesStream[byteIndex++] = ReaderID;
            bytesStream[byteIndex++] = DataStatus;
            bytesStream[byteIndex++] = AddrMode;
            bytesStream[byteIndex++] = dataLen;
            for (int i = 0; i < dataLen; i++)
            {
                bytesStream[byteIndex++] = dataBuf[i];
            }
            bytesStream[byteIndex++] = Bcc;
            return bytesStream;
        }
        public bool ByteStream2Package(byte[] byteStream)
        {
            if(byteStream == null)
            {
                return false;
            }
            this.Header = byteStream[0];
            this.CmdCode = byteStream[1];
            this.ReaderID = byteStream[2];
            this.DataStatus = byteStream[3];
            this.AddrMode = byteStream[4];
            this.dataLen = byteStream[5];
            if(byteStream.Count()<(7+this.dataLen))
            {
                return false;
            }
            this.dataBuf = new byte[this.dataLen];
            Array.Copy(byteStream,6,this.dataBuf,0,this.dataLen);
            this.Bcc = byteStream[6+this.dataLen];
            return true;
        }
    }
    public class UserConfig
    {
        public byte AddrMode = 0; //0:交互式，1: 自动寻卡模式
        public byte ReaderID = 1; //读卡器ID
        public byte RFChipPower = 1; //功率，0：半功率，1：全功率
        public bool NeedBcc = true; //是否需要BCC校验
        public byte ComPortType = 1; //0:232,1:485
        public byte BlockBytes = 4; //单块大小，默认8字节
        public byte AvailableType = 0; //指令有效时间，0：持续有效，1s，1：及时有效，50ms
       
    }
    public class HFReaderIF
    {
        public HFReaderIF()
        {

        }
        #region 数据区
        private SerialPort serialPort;

       // private UserConfig userCfg;
        private const int recvMax = 1024;
        private const int recvTimeOut = 2000;//发送出去之后，等待接收完毕，之间的最大时间间隔
        private byte[] recvBuffer = new byte[recvMax];
        private int recvBytes = 0;
        private AutoResetEvent recvAutoEvent = new AutoResetEvent(true);
        private bool responseOk = false;
        private bool recvBegin = false;

        /// <summary>
        /// 当前发送出去的命令
        /// </summary>
        private byte currentSndCmd = 0;

        /// <summary>
        /// 根据当前发送的命令，生成期望接收到的字节长度
        /// </summary>
        private byte expectedRecvLen = 0;

        private Thread recvThread = null;
        private bool recvExit = false;
       
        private bool isConnect = false;
        private object sendLock = new object();

        public string ComPort{get;set;}
        public bool IsConnect
        {
            get
            {
                return isConnect;
            }
        }
        //public UserConfig UserCfg
        //{
        //    get
        //    {
        //        return userCfg;
        //    }
        //}
        #endregion
        #region 公开接口

        /// <summary>
        /// 连接读卡器,并初始化
        /// </summary>
        /// <param name="portName"></param>
        /// <param name="reStr"></param>
        /// <returns></returns>
        public bool OpenComport(ref string reStr)
        {
            reStr = "通信端口打开！";
            if (this.ComPort == null || this.ComPort == string.Empty)
            {
                reStr = "通信端口未设定";
                return false;
            }
            try
            {
                if (serialPort == null)
                {
                    serialPort = new SerialPort();
                    serialPort.PortName = this.ComPort;
                    serialPort.DataBits = 8;
                    serialPort.StopBits = StopBits.One;
                    serialPort.Parity = Parity.None;
                    serialPort.ReceivedBytesThreshold = 1;
                    serialPort.BaudRate = 9600;
                   
                    
                }
                else if(serialPort.IsOpen)
                {
                    serialPort.Close();
                }
                
                serialPort.Open();
                if(serialPort.IsOpen)
                {
                    isConnect = true;
                     recvAutoEvent = new AutoResetEvent(false);
                    if (recvThread != null)
                    {
                        recvExit = true;
                    }
                    Thread.Sleep(300);
                    recvExit = false;
                    recvThread = new Thread(new ThreadStart(ComRecvProc));
                    recvThread.IsBackground = true;
                    recvThread.Priority = ThreadPriority.Highest;
                    recvThread.Name = "高频读卡器串口接收线程";
                    recvThread.Start();

                    ////先读取设备配置
                    //EnumREStatus reStauts = ReadUserCfg(0,ref this.userCfg);
                    //if(reStauts != EnumREStatus.RW_SUCCESS || this.userCfg == null)
                    //{
                    //    return false;
                    //}
                    return true;
                }
                else
                {
                    isConnect = false;
                    reStr = "通讯端口打开失败";
                    return  false;
                }
            }
            catch (System.Exception ex)
            {
                 reStr = "端口打开失败,遇到异常，"+ex.Message+","+ex.StackTrace;
                return false;
            }
        }
        public void CloseReader()
        {
            if (serialPort != null && isConnect)
            {
                serialPort.Close();
            }
            
            recvExit = true;
            isConnect = false;
            
            Array.Clear(recvBuffer, 0, recvBuffer.Count());
        }
        public EnumREStatus ReadUserCfg(byte readerID,ref UserConfig cfg)
        {
            cfg = null;
            lock (sendLock)
            {
                EnumREStatus reStatus = EnumREStatus.RW_SUCCESS;
                //发送命令
                RFPackage package = new RFPackage();
                package.CmdCode = 0xAE;
                package.ReaderID = readerID;
                package.DataStatus = 0x00;
                package.AddrMode = 0x00;
                package.dataLen = 0;
                package.dataBuf = null;
                package.Bcc = 0x00;
                byte[] byteStream = package.Package2Bytes();
                //发送之前清空接收缓冲区
                Array.Clear(recvBuffer, 0, recvBuffer.Count());
                recvBytes = 0;
                recvBegin = false;
                responseOk = false;
                this.currentSndCmd = package.CmdCode;
                this.expectedRecvLen = 14;
                serialPort.Write(byteStream, 0, byteStream.Count());
                recvAutoEvent.WaitOne(recvTimeOut);
                //查询接收状态
                if (!responseOk)
                {
                    reStatus = EnumREStatus.RW_NO_RESPONSE;
                    return reStatus;
                }
                
                {
                    package = new RFPackage();
                    if (package.ByteStream2Package(recvBuffer))
                    {
                        reStatus = RePackageCheck(package);
                        if (reStatus != EnumREStatus.RW_SUCCESS)
                        {
                            return reStatus;
                        }
                        if (package.DataStatus != 0x00)
                        {
                            return EnumREStatus.RW_FAILURE;
                        }
                        cfg = new UserConfig();

                        cfg.AddrMode = package.dataBuf[0];
                        cfg.ReaderID = package.dataBuf[1];
                        cfg.RFChipPower = package.dataBuf[2];
                        if (package.dataBuf[3] > 0)
                        {
                            cfg.NeedBcc = true;
                        }
                        else
                        {
                            cfg.NeedBcc = false;
                        }
                        cfg.ComPortType = package.dataBuf[4];
                        cfg.BlockBytes = package.dataBuf[5];
                        cfg.AvailableType = package.dataBuf[6];
                    }
                }

                return reStatus; 
            }
            
        }
        /// <summary>
        /// 读单块
        /// </summary>
        /// <param name="ReaderID"></param>
        /// <param name="BlockIndex">块号，从0开始编号</param>
        /// <param name="BlockDatas">接收数据缓存</param>
        /// <param name="len"></param>
        /// <returns></returns>
        public EnumREStatus ReadSBlock(byte ReaderID, UserConfig userCfg,byte BlockIndex, ref byte[] BlockDatas, ref byte len,ref byte[] recvByteArray)
        {
            lock (sendLock)
            {
                if (userCfg == null)
                {
                    return EnumREStatus.RW_NONE_USERCFG;
                }
                EnumREStatus reStatus = EnumREStatus.RW_SUCCESS;
                //发送命令
                RFPackage package = new RFPackage();
                package.CmdCode = 0x20;
                package.ReaderID = ReaderID;
                package.DataStatus = 0x00;
                package.AddrMode = userCfg.AddrMode;
                package.dataLen = 0x01;
                package.dataBuf = new byte[1];
                package.dataBuf[0] = BlockIndex;
                package.Bcc = GetBCC(package.dataBuf,(byte)package.dataBuf.Count());
                byte[] byteStream = package.Package2Bytes();

                //发送之前清空接收缓冲区
                Array.Clear(recvBuffer, 0, recvBuffer.Count());
                recvBytes = 0;
                recvBegin = false;
                responseOk = false;
                this.currentSndCmd = package.CmdCode;
                this.expectedRecvLen = 11;// (byte)(7 + this.userCfg.BlockBytes);
                serialPort.Write(byteStream, 0, byteStream.Count());
                recvAutoEvent.WaitOne(recvTimeOut);
                if (recvBytes > 0)
                {
                    recvByteArray = new byte[recvBytes];
                    Array.Copy(recvBuffer, recvByteArray, recvBytes);
                }
                //查询接收状态
                if (!responseOk)
                {
                    reStatus = EnumREStatus.RW_NO_RESPONSE;
                    return reStatus;
                }
                package = new RFPackage();
                if (package.ByteStream2Package(recvBuffer))
                {
                    reStatus = RePackageCheck(package);
                    if (reStatus != EnumREStatus.RW_SUCCESS)
                    {
                        return reStatus;
                    }
                    if (package.DataStatus != 0x00)
                    {
                        return EnumREStatus.RW_CMD_FAILED;
                    }
                    len = package.dataLen;
                    BlockDatas = new byte[len];
                    Array.Copy(package.dataBuf, BlockDatas, len);
                }
                return reStatus;
            }
           
        }

        /// <summary>
        /// 写单独块
        /// </summary>
        /// <param name="ReaderID"></param>
        /// <param name="BlockIndex"></param>
        /// <param name="sndData"></param>
        /// <param name="dataLen"></param>
        /// <returns></returns>
        public EnumREStatus WriteSBlock(byte ReaderID, UserConfig userCfg, byte BlockIndex, byte[] sndData, byte dataLen)
        {
            lock (sendLock)
            {
                if (userCfg == null)
                {
                    return EnumREStatus.RW_NONE_USERCFG;
                }
                EnumREStatus reStatus = EnumREStatus.RW_SUCCESS;
                //发送命令
                RFPackage package = new RFPackage();
                package.CmdCode = 0x21;
                package.ReaderID = ReaderID;
                package.DataStatus = 0x00;
                package.AddrMode = userCfg.AddrMode;
                package.dataLen = (byte)(dataLen+1);
                package.dataBuf = new byte[package.dataLen];
                package.dataBuf[0] = BlockIndex;
                Array.Copy(sndData,0,package.dataBuf,1,dataLen);
                package.Bcc = GetBCC(package.dataBuf, package.dataLen);

                byte[] byteStream = package.Package2Bytes();
                //发送之前清空接收缓冲区
                Array.Clear(recvBuffer, 0, recvBuffer.Count());
                recvBytes = 0;
                recvBegin = false;
                responseOk = false;
                this.currentSndCmd = package.CmdCode;
                this.expectedRecvLen = 7;
                serialPort.Write(byteStream, 0, byteStream.Count());
                recvAutoEvent.WaitOne(recvTimeOut);
                //查询接收状态
                if (!responseOk)
                {
                    reStatus = EnumREStatus.RW_NO_RESPONSE;
                    return reStatus;
                }
                package = new RFPackage();
                if (package.ByteStream2Package(recvBuffer))
                {
                    reStatus = RePackageCheck(package);
                    if (reStatus != EnumREStatus.RW_SUCCESS)
                    {
                        return reStatus;
                    }
                    if (package.DataStatus != 0x00)
                    {
                        return EnumREStatus.RW_CMD_FAILED;
                    }
                }
                return reStatus;
            }
        }

        /// <summary>
        /// 读多个块
        /// </summary>
        /// <param name="ReaderID"></param>
        /// <param name="userCfg"></param>
        /// <param name="BlockIndex"></param>
        /// <param name="BlockNums"></param>
        /// <param name="BlockDatas"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public EnumREStatus ReadMBlock(byte ReaderID, UserConfig userCfg, byte BlockIndex, byte BlockNums, ref byte[] BlockDatas, ref byte len)
        {
            lock (sendLock)
            {
                if (userCfg == null)
                {
                    return EnumREStatus.RW_NONE_USERCFG;
                }
                EnumREStatus reStatus = EnumREStatus.RW_SUCCESS;
                //发送命令
                RFPackage package = new RFPackage();
                package.CmdCode = 0x23;
                package.ReaderID = ReaderID;
                package.DataStatus = 0x00;
                package.AddrMode = userCfg.AddrMode;
                package.dataLen = 0x02;
                package.dataBuf = new byte[package.dataLen];
                package.dataBuf[0] = BlockIndex;
                package.dataBuf[1] = BlockNums;
                package.Bcc = GetBCC(package.dataBuf, (byte)package.dataBuf.Count());
                byte[] byteStream = package.Package2Bytes();

                //发送之前清空接收缓冲区
                Array.Clear(recvBuffer, 0, recvBuffer.Count());
                recvBytes = 0;
                recvBegin = false;
                responseOk = false;
                this.currentSndCmd = package.CmdCode;
                this.expectedRecvLen = (byte)(7 + BlockNums*userCfg.BlockBytes);
                serialPort.Write(byteStream, 0, byteStream.Count());
                recvAutoEvent.WaitOne(recvTimeOut);
                //查询接收状态
                if (!responseOk)
                {
                    reStatus = EnumREStatus.RW_NO_RESPONSE;
                    return reStatus;
                }
                package = new RFPackage();
                if (package.ByteStream2Package(recvBuffer))
                {
                    reStatus = RePackageCheck(package);
                    if (reStatus != EnumREStatus.RW_SUCCESS)
                    {
                        return reStatus;
                    }
                    if (package.DataStatus != 0x00)
                    {
                        return EnumREStatus.RW_CMD_FAILED;
                    }
                    len = package.dataLen;
                    BlockDatas = new byte[len];
                    Array.Copy(package.dataBuf, BlockDatas, len);
                }
                return reStatus;
            }
        }
        public EnumREStatus WriteMBlock(byte ReaderID, UserConfig userCfg, byte BlockIndex, byte[] sndData, byte dataLen)
        {
            lock (sendLock)
            {
                if (userCfg == null)
                {
                    return EnumREStatus.RW_NONE_USERCFG;
                }
                if (dataLen > (userCfg.BlockBytes * 8))
                {
                    return EnumREStatus.RW_PARAM_ERROR;
                }
                byte blockNums = (byte)(dataLen/userCfg.BlockBytes);
                EnumREStatus reStatus = EnumREStatus.RW_SUCCESS;
                //发送命令
                RFPackage package = new RFPackage();
                package.CmdCode = 0x24;
                package.ReaderID = ReaderID;
                package.DataStatus = 0x00;
                package.AddrMode = userCfg.AddrMode;
                package.dataLen = (byte)(2+dataLen);
                package.dataBuf = new byte[package.dataLen];
                package.dataBuf[0] = BlockIndex;
                package.dataBuf[1] = blockNums;
                Array.Copy(sndData, 0, package.dataBuf, 2, dataLen);
                package.Bcc = GetBCC(package.dataBuf, (byte)package.dataBuf.Count());
                byte[] byteStream = package.Package2Bytes();

                //发送之前清空接收缓冲区
                Array.Clear(recvBuffer, 0, recvBuffer.Count());
                recvBytes = 0;
                recvBegin = false;
                responseOk = false;
                this.currentSndCmd = package.CmdCode;
                this.expectedRecvLen = 7;
                serialPort.Write(byteStream, 0, byteStream.Count());
                recvAutoEvent.WaitOne(recvTimeOut);

                //查询接收状态
                if (!responseOk)
                {
                    reStatus = EnumREStatus.RW_NO_RESPONSE;
                    return reStatus;
                }
                package = new RFPackage();
                if (package.ByteStream2Package(recvBuffer))
                {
                    reStatus = RePackageCheck(package);
                    if (reStatus != EnumREStatus.RW_SUCCESS)
                    {
                        return reStatus;
                    }
                    if (package.DataStatus != 0x00)
                    {
                        return EnumREStatus.RW_CMD_FAILED;
                    }
                   
                }

                return reStatus;
            }
        }
        public EnumREStatus GetReStatusByCode(byte errCode)
        {
            EnumREStatus re= EnumREStatus.RW_SUCCESS;
            switch (errCode)
            {
                case 0x00:
                    {
                        re = EnumREStatus.RW_SUCCESS;
                        break;
                    }
                case 0x80:
                    {
                        re = EnumREStatus.RW_FAILURE;
                        break;
                    }
                case 0x90:
                    {
                        re = EnumREStatus.RW_NOTAG_ERROR;
                        break;
                    }
                case 0xA0:
                    {
                        re = EnumREStatus.RW_BCC_ERROR;
                        break;
                    }
                case 0xB0:
                    {
                        re = EnumREStatus.RW_BLOCK_SIZE_ERROR;
                        break;
                    }
                case 0xC0:
                    {
                        re = EnumREStatus.RW_TIMEOUT_ERROR;
                        break;
                    }
                case 0xD0:
                    {
                        re = EnumREStatus.RW_GPO_PORT_ERROR;
                        break;
                    }
                default:
                    {
                        re = EnumREStatus.RW_FAILURE;
                        break;
                    }
            }
            return re;
        }
        #endregion
        #region 私有功能接口
        private void ComRecvProc()
        {
            byte[] buf = new byte[128];
            while (!recvExit)
            {
             
                if (!serialPort.IsOpen)
                {
                    continue;
                }
                int recvLen = 0;
                try
                {
                    if (serialPort.IsOpen)
                    {
                        recvLen = serialPort.Read(buf, 0, 128);
                    }
                    else
                    {
                        continue;
                    }
                }
                catch (System.Exception ex)
                {
                    //serialPort.Close();
                    isConnect = false;
                }

                //开始解析接收到的数据
                for (int i = 0; i < recvLen; i++)
                {
                    if (recvBytes >= recvMax)
                    {
                        break;
                    }
                    if (!recvBegin && (buf[i] == 0x1B))
                    {
                        //收到包头
                        Array.Clear(recvBuffer, 0, recvBuffer.Count());
                        recvBytes = 0;
                        recvBegin = true;
                    }
                    if(recvBegin)
                    {
                        recvBuffer[recvBytes++] = buf[i];
                    }
                   //当接收到的字节长度达到期望值后，解析
                    if(recvBytes>= expectedRecvLen)
                    {
                        //解析数据包
                        
                        recvBegin = false;
                       
                        recvBytes = 0;
                        responseOk = true;
                        recvAutoEvent.Set();
                       
                    }
                }

            }
        }

        byte GetBCC(byte[] pData, byte size)
        {

            byte bcc = 0;
            for (int i = 0; i < size; i++)
            {
                bcc ^= pData[i];
            }
            return bcc;
        }
        EnumREStatus RePackageCheck(RFPackage package)
        {
            EnumREStatus reStatus = EnumREStatus.RW_SUCCESS;
            if (package.CmdCode != currentSndCmd)
            {
                reStatus = EnumREStatus.RW_NO_RESPONSE;
                return reStatus;
            }

            reStatus = GetReStatusByCode(package.DataStatus);
            if (reStatus == EnumREStatus.RW_SUCCESS)
            {
                byte bcc = GetBCC(package.dataBuf, package.dataLen);
                if (bcc != package.Bcc)
                {
                    reStatus = EnumREStatus.RW_BCC_ERROR;
                    return reStatus;
                }

            }
            return reStatus;
        }
        #endregion
    }
}
