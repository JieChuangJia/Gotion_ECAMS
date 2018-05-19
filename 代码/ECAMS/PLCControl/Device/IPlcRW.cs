using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECAMSModel;
namespace PLCControl 
{
    /// <summary>
    /// PLC自动重连事件参数
    /// </summary>
    public class PlcReLinkArgs:EventArgs
    {
        /// <summary>
        /// PLC连接信息，包括IP,端口号，
        /// </summary>
        public string StrConn { get; set; }

        /// <summary>
        /// PLC 分配的ID，自定义
        /// </summary>
        public int PlcID { get; set; }
    }
    /// <summary>
    /// plc 读写功能的接口
    /// </summary>
    public interface IPlcRW
    {
        /// <summary>
        /// PLC id，自定义
        /// </summary>
        int PlcID { get; set; }
        /// <summary>
        /// 是否处于连接状态
        /// </summary>
        bool IsConnect { get; }

        int StationNumber { get; set; }
        /// <summary>
        /// 连接PLC
        /// </summary>
        /// <param name="plcAddr">plc地址</param>
        /// <param name="reStr"></param>
        /// <returns></returns>
        bool ConnectPLC(string plcAddr, ref string reStr); 

        /// <summary>
        /// 关闭连接
        /// </summary>
        /// <returns></returns>
        bool CloseConnect();

        /// <summary>
        /// 读PLC 数据
        /// </summary>
        /// <param name="addr"></param>
        /// <param name="?"></param>
        /// <returns></returns>
        bool ReadDB(string addr,ref int val);

        bool ReadMultiDB(string addr, int blockNum, ref short[] vals);

        /// <summary>
        /// 写PLC数据
        /// </summary>
        /// <param name="addr"></param>
        /// <param name="?"></param>
        /// <returns></returns>
        bool WriteDB(string addr,int val);
        bool WriteMultiDB(string addr, int blockNum, short[] vals);

        void Init();
        void Exit();
        event EventHandler<PlcReLinkArgs> eventLinkLost;
        event EventHandler<LogEventArgs> eventLogDisp;
    }
}
