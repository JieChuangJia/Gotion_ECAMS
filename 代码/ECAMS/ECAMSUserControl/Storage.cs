using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections;

namespace ECAMSUserControl
{
  
    public partial class Storage
    {
        #region 全局变量
        /// <summary>
        /// 记录或设置表格值
        /// </summary>
        private DataTable gridValueDatatable = new DataTable();

        
        /// <summary>
        /// 线程
        /// </summary>
        private static object threadObj = new object();
       
        #endregion

        #region 初始化
        public Storage()
        {
          
        }
        #endregion

        #region 表格属性
        /// <summary>
        /// 数据源data[排][列][层]
        /// </summary>
        private Dictionary<int ,Dictionary<int ,Dictionary<int ,Positions>>> data
            = new Dictionary<int,Dictionary<int,Dictionary<int,Positions>>>();
        public Dictionary<int, Dictionary<int, Dictionary<int, Positions>>> Data
        {
            get { return this.data; }
            set { this.data = value; }

        }

        private int queryRowth;
        public int QueryRowth
        {
            get { return this.queryRowth; }
            set { this.queryRowth = value; }
        }

        /// <summary>
        /// 排数
        /// </summary>
        private int rows;
        public int Rows
        {
            get { return this.rows; }
            set { this.rows = value; }
        }

        /// <summary>
        /// 列数
        /// </summary>
        private int columns;
        public int Columns
        {
            get { return this.columns; }
            set { this.columns = value; }
        }

        /// <summary>
        /// 层数
        /// </summary>
        private int layers;
        public int Layers
        {
            get { return this.layers; }
            set { this.layers = value; }
        }

        /// <summary>
        /// 仓库宽度
        /// </summary>
        private int width;
        public int Width
        {
            get { return this.width; }
            set { this.width = value; }
        }

        /// <summary>
        /// 仓库高度
        /// </summary>
        private int height;
        public int Height
        {
            get { return this.height; }
            set { this.height = value; }
        }

        /// <summary>
        /// 列间距
        /// </summary>
        private int columnInterval = 5;
        public int ColumnInterval
        {
            get { return this.columnInterval; }
            set { this.columnInterval = value; }
        }
        
        /// <summary>
        /// 层间距
        /// </summary>
        private int layerInterval = 5;
        public int LayerInterval
        {
            get { return this.layerInterval; }
            set { this.layerInterval = value; }
        }

        /// <summary>
        /// 仓库起点
        /// </summary>
        private Point startPoint = new Point(70,50);
        public Point StartPoint
        {
            get { return this.startPoint; }
            set { this.startPoint = value; }
        }

        /// <summary>
        /// 画笔
        /// </summary>
        private Pen pen = new Pen(Brushes.Orange, 2);
        public Pen Pen
        {
            get { return this.pen; }
            set { this.pen = value; }
        }

        /// <summary>
        /// 字体
        /// </summary>
        private Font storageFont = new Font("宋体",12,FontStyle.Bold);
        public Font StorageFont
        {
            get { return this.storageFont; }
            set { this.storageFont = value; }
        }

        /// <summary>
        /// 表格数据源
        /// </summary>
        private DataTable dataSource;
        public DataTable DataSource
        {
            get { return this.dataSource; }
            set
            {
                this.dataSource = value;
                if (this.dataSource != null)
                {
                    Positions pos = new Positions();
                    int controlWidth = this.columns * (pos.Width + this.columnInterval) + startPoint.X + 30;
                    int controlHeigt = this.layers * (pos.Height + this.layerInterval) + startPoint.Y + 55;

                    this.StorageSize = new Size(controlWidth, controlHeigt);
                    SetValue(this.queryRowth, this.dataSource);
               
                }
            }
        }

        /// <summary>
        /// 根据货位的数量自动算出空间的大小
        /// </summary>
        public Size StorageSize
        {
            get;
            set;
        }

        #endregion

        #region 货仓方法
        /// <summary>
        /// 任务锁定时绘制
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="pos"></param>
        public void DrawSelect(Graphics graphics, Positions pos)
        {
            Pen linePen = new System.Drawing.Pen(Brushes.SkyBlue, 4);

            if (pos.Visible == true)
            {
                this.DrawRect(graphics, linePen, pos);
            }

        }

        /// <summary>
        /// 绘制每一个货位
        /// </summary>
        /// <param name="graphics"></param>
        public void DrawStorage(Graphics graphics)
        {
            for (int i = 0; i < this.data.Keys.Count; i++)//排
            {
                for (int j = 0; j < this.data[i].Count; j++)//列
                {
                    for (int k = 0; k < this.data[i][j].Count; k++)//层
                    {
                        Positions pos = this.data[i][j][k];
                        if (pos.Visible == true)
                        {
                            this.DrawRect(graphics, this.Pen, pos);
                            this.FillRect(graphics, pos.PosRect, pos.Color);
                            this.DrawStrRect(graphics, pos);
                        }
                    }
                }
            }
            DrawCoordinate(graphics);
        }
     
        #endregion

        #region 私有方法
        /// <summary>
        /// 写字符串
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="rect"></param>
        /// <param name="posPoint"></param>
        /// <param name="drawStr"></param>
        private void DrawStrRect(Graphics graphics,Positions pos)
        {
            Font drawFont = new Font("宋体", 10,FontStyle.Bold);
            SizeF strzSize = graphics.MeasureString(pos.PosText, drawFont);
            int pointX = (pos.Width -(int)strzSize.Width)/2 + pos.Location.X;
            int pointY = (pos.Height - (int)strzSize.Height) / 2 + pos.Location.Y+2;
            
            graphics.DrawString(pos.PosText, drawFont, Brushes.Silver, new Point(pointX,pointY));
        }

        /// <summary>
        /// 填充矩形
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="rect"></param>
        /// <param name="brush"></param>
        private void FillRect(Graphics graphics, Rectangle rect,Color color)
        {
            SolidBrush brush = new SolidBrush(color);
            graphics.FillRectangle(brush, rect);
        }

        /// <summary>
        /// 绘制矩形
        /// </summary>
        /// <param name="pen"></param>
        /// <param name="rect"></param>
        private void DrawRect(Graphics graphics,Pen pen,Positions pos)
        {
            graphics.DrawRectangle(pen, pos.PosRect);

            Pen linePen = new System.Drawing.Pen(Brushes.Silver, 1);
            Point endPoint1 = new Point(pos.PosRect.Location.X + pos.Width, pos.PosRect.Location.Y + pos.Height);
            Point startPoint2 = new Point(pos.PosRect.Location.X + pos.Width, pos.PosRect.Location.Y);
            Point endPoint2 = new Point(pos.PosRect.Location.X, pos.PosRect.Location.Y + pos.Height);

            graphics.DrawLine(linePen, pos.PosRect.Location, endPoint1);
            graphics.DrawLine(linePen, startPoint2, endPoint2);

            linePen.Dispose();

        }

        /// <summary>
        /// 绘制列数、层数的标尺
        /// </summary>
        /// <param name="graphics"></param>
        private void DrawCoordinate(Graphics graphics)
        {
            if (this.data.Count > 0)
            {
                //写层数
                for (int k = 0; k < this.data[0][0].Count; k++)
                {
                    Positions pos = this.data[0][0][k];
                    Point pt = new Point(pos.Location.X - 45, pos.Location.Y);
                    graphics.DrawString((k + 1).ToString(), this.storageFont, Brushes.Silver, pt);
                }

                //画纵坐标系箭头
                Positions posFirstY = this.data[0][0][0];
                Positions posEndY = this.data[0][0][data[0][0].Count - 1];
                Point startPtY = new Point(posFirstY.Location.X - 10, posFirstY.Location.Y + posFirstY.Height + 10);
                Point endPtY = new Point(posEndY.Location.X - 10, posEndY.Location.Y - 20);
                Point arrowsEndPtY1 = new Point(posEndY.Location.X - 15, posEndY.Location.Y - 10);
                Point arrowsEndPtY2 = new Point(posEndY.Location.X - 5, posEndY.Location.Y - 10);

                graphics.DrawLine(new Pen(Brushes.Silver, 2), startPtY, endPtY);
                graphics.DrawLine(new Pen(Brushes.Silver, 2), endPtY, arrowsEndPtY1);
                graphics.DrawLine(new Pen(Brushes.Silver, 2), endPtY, arrowsEndPtY2);

                //写字 层数 固定位置
                Point stringPtY1 = new Point(posFirstY.Location.X - 65, posFirstY.Location.Y -60);
                Point stringPtY2 = new Point(stringPtY1.X, stringPtY1.Y + 30);
                graphics.DrawString("层", this.storageFont, Brushes.Silver, stringPtY1);
                graphics.DrawString("数", this.storageFont, Brushes.Silver, stringPtY2);

                //写列数
                for (int k = 0; k < this.data[0].Count; k++)
                {
                    Positions pos = this.data[0][k][0];//计算第一层就可以
                    Point pt = new Point(pos.Location.X, pos.Location.Y + 40);
                    graphics.DrawString((k + 1).ToString(), this.storageFont, Brushes.Silver, pt);
                }

                //画横坐标系箭头
                Positions posFirstX = this.data[0][0][0];
                Positions posEndX = this.data[0][this.data[0].Count - 1][0];
                Point startPtX = new Point(posFirstX.Location.X - 10, posFirstX.Location.Y + posFirstX.Height + 10);
                Point endPtX = new Point(posEndX.Location.X + posEndX.Width + 20, posEndX.Location.Y + posFirstX.Height + 10);
                Point arrowsEndPtX1 = new Point(endPtX.X - 10, endPtX.Y - 5);
                Point arrowsEndPtX2 = new Point(endPtX.X - 10, endPtX.Y + 5);

                graphics.DrawLine(new Pen(Brushes.Silver, 2), startPtX, endPtX);
                graphics.DrawLine(new Pen(Brushes.Silver, 2), endPtX, arrowsEndPtX1);
                graphics.DrawLine(new Pen(Brushes.Silver, 2), endPtX, arrowsEndPtX2);

                //写 列汉字固定位置
                Point stringPtX1 = new Point(posFirstX.Location.X + 60,posEndX.Location.Y + posEndX.Height + 40);
                Point stringPtX2 = new Point(stringPtX1.X + 30, stringPtX1.Y);
                graphics.DrawString("列", this.storageFont, Brushes.Silver, stringPtX1);
                graphics.DrawString("数", this.storageFont, Brushes.Silver, stringPtX2);
            }
        }

        /// <summary>
        /// 赋值，这里只可以查看某一排的数据
        /// 并且按排列顺序存储
        /// </summary>
        /// <param name="rowth"></param>
        /// <param name="dt"></param>
        private void SetValue(int rowth, DataTable dt)
        {
            if (dt != null)
            {
                this.data.Clear();
                Dictionary<int, Dictionary<int, Positions>> dicColumn = new Dictionary<int, Dictionary<int, Positions>>();

                for (int i = 0; i < this.columns; i++)//列
                {
                    Dictionary<int, Positions> dicLayer = new Dictionary<int, Positions>();
                    for (int j = this.layers - 1; j >= 0; j--)//层
                    {
                        Positions pos = new Positions();
                        pos.Rowth = rowth; //排、列、层 都加1目的是从1开始计数
                        pos.Columnth = i + 1;
                        pos.Layer = this.layers - j;

                        DataRow[] dr = dt.Select("GoodsSiteRow=" + pos.Rowth + " and GoodsSiteColumn="
                            + pos.Columnth + " and GoodsSiteLayer =" + pos.Layer);
                        if (dr != null && dr.Count() > 0)
                        {
                            pos.GoodsSiteID = int.Parse(dr[0]["GoodsSiteID"].ToString());
                            if (dr[0]["GoodsSiteStoreStatus"].ToString() == "有货"
                                && dr[0]["GoodsSiteRunStatus"].ToString() == "任务完成"
                                && dr[0]["GoodsSiteInOutType"].ToString() == "入库")
                            {

                                pos.StoreStatus = "有货";
                                pos.RunStatus = "任务完成";
                                pos.Color = Color.Green;
                            }
                            else if (dr[0]["GoodsSiteStoreStatus"].ToString() == "空料框"
                                && dr[0]["GoodsSiteRunStatus"].ToString() == "任务完成"
                                && dr[0]["GoodsSiteInOutType"].ToString() == "入库")
                            {
                                pos.StoreStatus = "空料框";
                                pos.RunStatus = "任务完成";
                                pos.Color = Color.Yellow;
                              
                            }
                            else if (dr[0]["GoodsSiteStoreStatus"].ToString() == "有货"
                                && dr[0]["GoodsSiteRunStatus"].ToString() == "任务锁定"
                                 && dr[0]["GoodsSiteInOutType"].ToString() == "入库")
                            {
                                pos.StoreStatus = "有货";
                                pos.RunStatus = "任务锁定";
                                pos.Color = Color.Blue;
                                pos.PosText = "入";
                            }
                            else if (dr[0]["GoodsSiteStoreStatus"].ToString() == "有货"
                                && dr[0]["GoodsSiteRunStatus"].ToString() == "任务锁定"
                                && dr[0]["GoodsSiteInOutType"].ToString() == "出库")
                            {
                                pos.StoreStatus = "有货";
                                pos.RunStatus = "任务锁定";
                                pos.Color = Color.Blue;
                                pos.PosText = "出";
                            }
                            else if (dr[0]["GoodsSiteStoreStatus"].ToString() == "空料框"
                                && dr[0]["GoodsSiteRunStatus"].ToString() == "任务锁定"
                                && dr[0]["GoodsSiteInOutType"].ToString() == "出库")
                            {
                                pos.StoreStatus = "空料框";
                                pos.RunStatus = "任务锁定";
                                pos.Color = Color.Yellow;
                                pos.PosText = "出";
                            }
                            else if (dr[0]["GoodsSiteStoreStatus"].ToString() == "空料框"
                           && dr[0]["GoodsSiteRunStatus"].ToString() == "任务锁定"
                           && dr[0]["GoodsSiteInOutType"].ToString() == "入库")
                            {
                                pos.StoreStatus = "空料框";
                                pos.RunStatus = "任务锁定";
                                pos.Color = Color.Yellow;
                                pos.PosText = "入";
                            }
                            else if (dr[0]["GoodsSiteRunStatus"].ToString() == "异常")
                            {
                                pos.RunStatus = "异常";
                                pos.Color = Color.Red;
                            }
                            else
                            {
                                pos.RunStatus = dr[0]["GoodsSiteRunStatus"].ToString();
                                pos.StoreStatus = "空货位";
                                pos.Color = Color.Transparent;
                            }
                        }

                        Point location = new Point();

                        location.X = i * pos.Width + i * this.columnInterval + this.startPoint.X;
                        location.Y = j * pos.Height + j * this.layerInterval + this.startPoint.Y;

                        Rectangle posRect = new Rectangle(location, new Size(pos.Width, pos.Height));
                        pos.PosRect = posRect;
                        pos.Location = location;
                        pos.Visible = true;
                        dicLayer.Add(this.layers - j - 1, pos);
                    }
                    dicColumn.Add(i, dicLayer);
                }
                this.data.Add(0, dicColumn);
            }
            if (dt.Rows.Count > 0)
            {
                int logicAreaID = int.Parse(dt.Rows[0]["LogicStoreAreaID"].ToString());
                SetPositionsVisible(logicAreaID, rowth);
            }
        }

        /// <summary>
        /// 设置仓位显隐
        /// </summary>
        private void SetPositionsVisible(int logicAreaID,int rowth)
        {
            if (this.data != null)
            {
                if (logicAreaID == 4)//a1库房
                {
                    if (rowth == 1)
                    {
                        this.data[0][0][0].Visible = false;
                        this.data[0][0][1].Visible = false;
                        this.data[0][0][2].Visible = false;
                        this.data[0][0][3].Visible = false;

                        this.data[0][8][0].Visible = false;
                        this.data[0][8][1].Visible = false;
                        this.data[0][8][2].Visible = false;
                        this.data[0][8][3].Visible = false;

                        this.data[0][10][0].Visible = false;
                        this.data[0][10][1].Visible = false;
                        this.data[0][10][2].Visible = false;
                        this.data[0][10][3].Visible = false;
                    }
                    else if(rowth == 2)
                    {
                        this.data[0][0][0].Visible = false;
                        this.data[0][0][1].Visible = false;
                        this.data[0][0][2].Visible = false;
                        this.data[0][0][3].Visible = false;
                    }
                }
                else if (logicAreaID == 2)//b1库房
                {
                    if (rowth == 1)
                    {
                        this.data[0][12][0].Visible = false;
                        this.data[0][12][1].Visible = false;
                        this.data[0][12][2].Visible = false;
                        this.data[0][12][3].Visible = false;

                        //this.data[0][13][0].Visible = false;
                        //this.data[0][13][1].Visible = false;

                        this.data[0][15][0].Visible = false;
                        this.data[0][15][1].Visible = false;

                        //this.data[0][16][0].Visible = false;
                        //this.data[0][16][1].Visible = false;

                        //this.data[0][35][0].Visible = false;
                        //this.data[0][35][1].Visible = false;

                        this.data[0][36][0].Visible = false;
                        this.data[0][36][1].Visible = false;

                        //this.data[0][41][0].Visible = false;
                        //this.data[0][41][1].Visible = false;

                        this.data[0][42][0].Visible = false;
                        this.data[0][42][1].Visible = false;
                    }
                    else if (rowth == 2)
                    {
                        
                    }
                }
                
            }
        }
        #endregion
    }
}
