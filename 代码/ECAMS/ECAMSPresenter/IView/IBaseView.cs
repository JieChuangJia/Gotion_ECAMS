using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECAMSPresenter
{
    /// <summary>
    /// 基础逻辑所有view接口都要继承它
    /// </summary>
    public interface IBaseView
    {
        /// <summary>
        /// 获取逻辑
        /// </summary>
        /// <returns></returns>
        object GetPresenter(Type presenter);
        /// <summary>
        /// 作者:np
        /// 时间:2014年8月25日
        /// 内容:触发窗体doevent事件
        /// </summary>
        void FormDoEvent();
     
        /// <summary>
        /// 作者:np
        /// 时间:2014年8月24日
        /// 内容:打开进度条窗体
        /// </summary>
        bool OpenProgressBar();
        /// <summary>
        /// 作者:np
        /// 时间:2014年8月24日
        /// 内容:关闭进度条窗体
        /// </summary>
        void CloseProgressBar();
        ///// <summary>
        ///// 作者:np
        ///// 时间:2014年8月24日
        ///// 内容:设置进度条值
        ///// </summary>
        ///// <param name="value"></param>
        //void SetProgressBarValue(int value);
        ///// <summary>
        ///// 作者:np
        ///// 时间:2014年8月24日
        ///// 内容:设置进度条最大值
        ///// </summary>
        ///// <param name="value"></param>
        //void SetProgressBarMaxValue(int value);

    }
   
}
