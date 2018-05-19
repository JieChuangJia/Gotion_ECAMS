using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PLCControl
{
    public class rfidRWSim:IrfidRW
    {
        private byte readerID = 1;
        private bool isOpened = true;
        public string simReadRfid = null;
        public bool IsOpened
        {
            get
            {
                return isOpened;
            }
        }
        /// <summary>
        /// 连接
        /// </summary>
        /// <returns></returns>
        public bool Connect() 
        {
            return true;
        }

        /// <summary>
        /// 断开
        /// </summary>
        /// <returns></returns>
        public bool Discconnect()
        {
            return true;
        }
        public bool WriteSBlock(byte[] bytesData)
        {
            return true;
        }
        public byte[] ReadSBlock(byte blockIndex, ref byte[] recvByteArray)
        {
            byte[] reBytes = new byte[4];
            return reBytes;//"0001234567";

        }
        public byte[] ReadRfidMultiBlock(byte blockStart, byte blockNum)
        {
            byte[] reBytes = new byte[4 * blockNum];
            return reBytes;
        }
        public bool WriteMBlock(byte blockIndex, byte[] bytesData)
        {
            return true;
        }
        public string ReadPalletID(ref byte[] recvByteArray)
        {
            return simReadRfid;
        }
        public byte ReaderID 
        {
            get
            {
                return readerID;
            }
            set
            {
                readerID = value;
            }
        }
    }
}
