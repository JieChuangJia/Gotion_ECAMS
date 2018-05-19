using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace ECAMS
{
    public partial class BaseView : Form
    {
        public static IList<object> allPresenterList = new List<object>();

        public BaseView()
        {
            InitializeComponent();
            _presenter = this.CreatePresenter();
        }

        private object _presenter;
        public object presenter
        {
            get
            {
                return _presenter;
            }
            private set { }
        }
        /// <summary>
        /// 作者:np
        /// 时间:2014年8月24日
        /// 内容:当前进度条线程
        /// </summary>
        private Thread currentprogressBarThread = null;
        private BackgroundWorker backGroundWorker;// 申明后台对象
        /// <summary>
        /// 作者:np
        /// 时间:2014年8月24日
        /// 内容:当前进度条窗体
        /// </summary>
        private ProgressBarView progressBarView = null;
        private static bool isOpened = false;
        private delegate void SetProgressBarValueInvoke(int value);
        private delegate void SetProgressBarMaxValueInvoke(int value);
        private delegate void CloseProgressBarInvoke();
        #region 实现接口的方法
        /// <summary>
        /// 作者:np
        /// 时间:2014年8月24日
        /// 内容:打开进度条
        /// </summary>
        public bool OpenProgressBar()
        {
            try
            {
                if (isOpened)
                {
                    return false;
                }
                if (backGroundWorker == null)
                {
                    backGroundWorker = new BackgroundWorker();
                    backGroundWorker.WorkerReportsProgress = true;
                    backGroundWorker.DoWork += DoWork;
                    backGroundWorker.RunWorkerAsync();
                }
                else
                {
                    this.progressBarView.Visible = true;
                    isOpened = true;
                    this.progressBarView.Refresh();
                }

                //if (currentprogressBarThread == null)
                //{
                //    Thread progressBarThread = new Thread(new ThreadStart(OpenProgressBarView));
                //    progressBarThread.IsBackground = true;
                //    progressBarThread.Start();
                //    currentprogressBarThread = progressBarThread;
                //    isOpened = true;
                //}
                //else
                //{
                //    this.progressBarView.Visible = true;
                //    isOpened = true;
                //    this.progressBarView.Refresh();
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show("打开失败：" + ex.Message, "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;

        }
        /// <summary>
        /// 作者:np
        /// 时间:2014年8月24日
        /// 内容:关闭进度条
        /// </summary>
        public void CloseProgressBar()
        {
            try
            {
                if (progressBarView != null)
                {
                    if (this.progressBarView.InvokeRequired)
                    {
                        CloseProgressBarInvoke cpbi = new CloseProgressBarInvoke(CloseProgressBar);
                        this.progressBarView.Invoke(cpbi);
                    }
                    else
                    {
                        this.progressBarView.Visible = false;
                        isOpened = false;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("进度条关闭失败：" + ex.Message, "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

            //this.currentprogressBarThread.Abort();
            //this.currentprogressBarThread = null; 
        }        /// <summary>
        ///// 作者:np
        ///// 时间:2014年8月24日
        ///// 内容:设置进度条值
        ///// </summary>
        ///// <param name="value"></param>
        //public void SetProgressBarValue(int value)
        //{
        //    try
        //    {
        //        if (progressBarView != null)
        //        {
        //            if (this.progressBarView.InvokeRequired)
        //            {
        //                SetProgressBarValueInvoke spbvi = new SetProgressBarValueInvoke(SetProgressBarValue);
        //                this.progressBarView.Invoke(spbvi, new object[1] { value });
        //            }
        //            else
        //            {
        //                this.progressBarView.GetProcessBar().Value = value;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("进度条设置值失败：" + ex.Message, "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //}
        ///// <summary>
        ///// 作者:np
        ///// 时间:2014年8月24日
        ///// 内容:设置进度条最大值
        ///// </summary>
        ///// <param name="value"></param>
        //public void SetProgressBarMaxValue(int value)
        //{
        //    try
        //    {
        //        if (progressBarView != null)
        //        {
        //            if (this.progressBarView.InvokeRequired)
        //            {
        //                SetProgressBarMaxValueInvoke spbmvi = new SetProgressBarMaxValueInvoke(SetProgressBarMaxValue);
        //                this.progressBarView.Invoke(spbmvi, new object[1] { value });
        //            }
        //            else
        //            {
        //                this.progressBarView.GetProcessBar().Maximum = value;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("进度条设置最大值失败：" + ex.Message, "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //}

        /// <summary>
        /// 作者:np
        /// 时间:2014年8月25日
        /// 内容:触发窗体doevent事件
        /// </summary>
        public void FormDoEvent()
        {
            Application.DoEvents();
        }

        /// <summary>
        /// 作者:np
        /// 时间:2014年8月25日
        /// 内容:设置窗体是否可用
        /// </summary>
        /// <param name="status"></param>
        public void SetFormEnabled(bool status)
        {
            this.Enabled = status;
        }
        #endregion

        /// <summary>
        /// 必须由具体子类实现 
        /// </summary>
        /// <returns></returns>
        protected virtual object CreatePresenter()
        {
            if (LicenseManager.CurrentContext.UsageMode == LicenseUsageMode.Designtime)
            {
                return null;
            }
            else
            {
                //若子类未实现此虚函数，则会抛出异常。
                throw new NotImplementedException(string.Format("{0} must override the CreatePresenter method.", this.GetType().FullName));
            }
        }

       /// <summary>
       /// 关闭窗体时移除逻辑
       /// </summary>
       /// <param name="e"></param>
        protected override void OnClosed(EventArgs e)  
        {
            allPresenterList.Remove(_presenter);
            base.OnClosed(e);
        }

        private void OpenProgressBarView()
        {
            ProgressBarView pbView = new ProgressBarView();
            progressBarView = pbView;
            pbView.ShowDialog();
        }

        private void DoWork(object sender, EventArgs e)
        {
            ProgressBarView pbView = new ProgressBarView();
            progressBarView = pbView;
            pbView.ShowDialog();
        }
    }
}
