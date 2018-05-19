using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PLCControl
{
    /// <summary>
    /// RFID读写接口
    /// </summary>
    public interface IrfidRW
    {
        bool IsOpened { get; }
        /// <summary>
        /// 连接
        /// </summary>
        /// <returns></returns>
        bool Connect();

        /// <summary>
        /// 断开
        /// </summary>
        /// <returns></returns>
        bool Discconnect();

        /// <summary>
        /// 开始查询
        /// </summary>
        /// <returns></returns>
       // bool BeginInv();

        /// <summary>
        /// 当前查询到的标签列表
        /// </summary>
        /// <returns></returns>
        //IList<string> GetInvTags();

        /// <summary>
        /// 写入数据
        /// </summary>
        /// <param name="strData"></param>
        /// <returns></returns>
        bool WriteSBlock(byte[] bytesData);

        /// <summary>
        /// 读rfid数据，获得唯一结果
        /// </summary>
        /// <returns></returns>
        byte[] ReadSBlock(byte blockIndex, ref byte[] recvByteArray);

        /// <summary>
        /// 读多个块
        /// </summary>
        /// <param name="blockStart"></param>
        /// <param name="blockNum"></param>
        /// <returns></returns>
        byte[] ReadRfidMultiBlock(byte blockStart, byte blockNum);

        /// <summary>
        /// 写多块
        /// </summary>
        /// <param name="blockIndex"></param>
        /// <param name="bytesData"></param>
        /// <returns></returns>
        bool WriteMBlock(byte blockIndex, byte[] bytesData);

        /// <summary>
        /// 读托盘号
        /// </summary>
        /// <returns></returns>
        string ReadPalletID(ref byte[] recvByteArray);
        byte ReaderID { get; set; }
    }
}
