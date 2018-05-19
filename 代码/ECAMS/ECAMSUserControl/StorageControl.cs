using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace ECAMSUserControl
{
    public partial class StorageControl : UserControl
    {
        #region 全局变量
        /// <summary>
        /// 表示是否任务锁定货仓
        /// </summary>
        public Positions selectPositions = null;
        public Storage storage = new Storage();
        #endregion

        #region 初始化
        public StorageControl()
        {
            InitializeComponent(); 
        }

        private void GoodsSiteControl_Load(object sender, EventArgs e)
        {
            //DataTable dt = new DataTable();
            //dt.Columns.Add();
            //dt.Columns.Add();
            //dt.Columns.Add();
            //dt.Columns.Add();
            //dt.Columns.Add();
            //dt.Columns.Add();
            //dt.Columns.Add();
            //dt.Columns.Add();
            //dt.Columns.Add();
            //dt.Columns.Add();
            //dt.Columns.Add();
            //dt.Columns.Add();
            //for (int i = 0; i < 20; i++)
            //{
            //    dt.Rows.Add();
            //    dt.Rows[i][0] = 1;
            //    dt.Rows[i][1] = 1;
            //    dt.Rows[i][2] = 1;
            //    dt.Rows[i][3] = 1;
            //    dt.Rows[i][4] = 1;
            //    dt.Rows[i][5] = 1;
            //}
            //this.DataSource = dt;

            //int widthTemp = this.Size.Width;
            //int heightTemp = this.Size.Height;
            //if (widthTemp < this.storage.StorageSize.Width)
            //{
            //    widthTemp = this.storage.StorageSize.Width;
            //}

            //if (heightTemp < this.storage.StorageSize.Height)
            //{
            //    heightTemp = this.storage.StorageSize.Height;
            //}

            //this.AutoScrollMinSize = this.Size;
        }
        #endregion
       
        #region UI事件
        /// <summary>
        /// 自绘
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GoodsSiteControl_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias; //SmoothingMode.HighQuality;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighSpeed;
            e.Graphics.TranslateTransform(this.AutoScrollPosition.X, this.AutoScrollPosition.Y);
            storage.DrawStorage(e.Graphics);//可以考虑只刷新当前可见区域
            if (this.selectPositions != null)
            {
                storage.DrawSelect(e.Graphics, this.selectPositions);
            }        
            e.Dispose();
        }

        /// <summary>
        /// 鼠标点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GoodsSiteControl_MouseClick(object sender, MouseEventArgs e)
        {
            this.Invalidate();//重绘一次
            Point pt = new Point(e.X -this.AutoScrollPosition.X, e.Y - this.AutoScrollPosition.Y);
            Positions pos = GetPostionsByPt(pt);

            if (pos != null)
            {
                if (this.PositionsClick != null)
                {
                    this.selectPositions = pos;
                    ClickPositionsEventArgs positionsArgs = new ClickPositionsEventArgs();
                    positionsArgs.Positions = pos;
                    PositionsClick.Invoke(this, positionsArgs);
                }
            }
        }
        #endregion

        #region 自定义事件
        [Description("货架单元格的鼠标事件:单击鼠标事件"), Category("Mouse")]
        public event ClickPositionsEventHandler PositionsClick;
        #endregion

        #region 事件委托
        public delegate void ClickPositionsEventHandler(object sender, ClickPositionsEventArgs e);
        public class ClickPositionsEventArgs : EventArgs
        {
            public Positions Positions { get; set; }
        }
        #endregion

        #region 属性

        private int queryRowth;
        public int QueryRowth
        {
            get{return this.queryRowth;}
            set
            {
                queryRowth = value;
                this.storage.QueryRowth = queryRowth;
            }
        }

        private int columns = 0;
        public int Columns
        {
            get { return this.columns; }
            set { this.columns = value;
            this.storage.Columns = this.columns;
            }
        }

        private int layers = 0;
        public int Layers
        {
            get { return this.layers; }
            set { this.layers = value;
            this.storage.Layers = this.layers;
            }
        }

        private DataTable dataSource;
        /// <summary>
        /// 数据源要包括层、列、排信息
        /// </summary>
        public DataTable DataSource
        {
            get { return this.dataSource; }
            set { this.dataSource = value;
            this.storage.DataSource = this.dataSource;
            SetScrollSize();
            this.Invalidate();
            }
        }
        #endregion

        #region 方法
        public Positions GetPositionsByRcl(int columnth, int layerth)
        {
            try
            {
                if (this.storage.Data != null)
                {
                    Positions pos = this.storage.Data[0][columnth][layerth];
                    return pos;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 获取仓位通过坐标点
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        private Positions GetPostionsByPt(Point pt)
        {   Positions pos = null;
            for (int i = 0; i < this.storage.Data.Keys.Count; i++)
            {
                for (int j = 0; j < this.storage.Data[i].Count; j++)
                {
                    for (int k = 0; k < this.storage.Data[i][j].Count; k++)
                    {
                       
                        if (this.storage.Data[i][j][k].PosRect.Contains(pt))
                        {
                            pos = this.storage.Data[i][j][k];
                            break;
                        }
                    }
                }
            }
            return pos;
        }

        /// <summary>
        /// 设置滚动范围根据列数和层数
        /// </summary>
        private void SetScrollSize()
        {
            int widthTemp = this.Size.Width;
            int heightTemp = this.Size.Height;
            if (widthTemp < this.storage.StorageSize.Width)
            {
                widthTemp = this.storage.StorageSize.Width;
            }

            if (heightTemp < this.storage.StorageSize.Height)
            {
                heightTemp = this.storage.StorageSize.Height;
            }
            this.AutoScrollMinSize = new Size(widthTemp, heightTemp);
        }
        #endregion

    }
}
