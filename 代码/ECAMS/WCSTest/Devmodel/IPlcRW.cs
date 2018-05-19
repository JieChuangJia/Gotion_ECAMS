using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCSTest
{
    /// <summary>
    /// plc 读写功能的接口
    /// </summary>
    public interface IPlcRW
    {
        /// <summary>
        /// 读PLC 数据
        /// </summary>
        /// <param name="addr"></param>
        /// <param name="?"></param>
        /// <returns></returns>
        bool ReadDB(string addr,ref int val);

        /// <summary>
        /// 写PLC数据
        /// </summary>
        /// <param name="addr"></param>
        /// <param name="?"></param>
        /// <returns></returns>
        bool WriteDB(string addr,int val);
    }
}
