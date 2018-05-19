using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECAMSModel;
using ECAMSDataAccess;

namespace WCSTest
{
    /// <summary>
    /// 巷道式堆垛机
    /// </summary>
    public class Stacker:DevBase
    {
        #region 私有数据
        /// <summary>
        /// 是否允许申请新的任务
        /// </summary>
        private bool enableRequireNewTask = true;
        private ControlTaskModel currentTask = null;

        /// <summary>
        /// 出入库任务计时器（计数实现），作为任务调度的参考，先按计数大小取任务，再按先出后入的原则取任务。
        /// </summary>
        private IDictionary<EnumTaskName, Int64> taskCounterDic = new Dictionary<EnumTaskName, Int64>();
        #endregion

        public Stacker(ECAMSDataAccess.DeviceModel devModel, IPlcRW plcRW, DeviceBll devBll)
            : base(devModel, plcRW, devBll)
        {
           
           
        }
        #region 数据
        #endregion
        #region 重写虚函数
       
      
  
        #endregion
        #region 私有
        
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
        #endregion

    }
}
