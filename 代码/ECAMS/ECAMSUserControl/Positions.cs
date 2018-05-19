using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Drawing;

namespace ECAMSUserControl
{
    [Serializable]
    public class Positions
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public Positions()
        { 
        }

        /// <summary>
        /// 单元格默认为显示
        /// </summary>
        private bool visible = true;
        public bool Visible
        {
            get { return this.visible; }
            set { this.visible = value; }
        }

        /// <summary>
        /// 排
        /// </summary>
        private int rowth;
        public int Rowth
        {
            get { return this.rowth; }
            set { this.rowth = value; }
        }

        /// <summary>
        /// 列
        /// </summary>
        private int columnth;
        public int Columnth
        {
            get { return this.columnth; }
            set { this.columnth = value; }
        }

        /// <summary>
        /// 层
        /// </summary>
        private int layer;
        public int Layer
        {
            get { return this.layer; }
            set { this.layer = value; }
        }

        /// <summary>
        /// 单元格高度
        /// </summary>
        private int height=20;
        public int Height
        {
            get { return this.height; }
            set { this.height = value; }
        }

        /// <summary>
        /// 单元格宽度
        /// </summary>
        private int width = 25;
        public int Width
        {
            get { return this.width; }
            set { this.width = value; }
        }

        /// <summary>
        /// 存储状态
        /// </summary>
        private string storeStatus;
        public string StoreStatus
        {
            get { return this.storeStatus; }
            set { this.storeStatus = value; }
        }

        private int goodsSiteID;
        public int GoodsSiteID
        {
            get { return this.goodsSiteID; }
            set { this.goodsSiteID = value; }
        }

        /// <summary>
        /// 单元格框
        /// </summary>
        private Rectangle posRect;
        public Rectangle PosRect
        {
            get { return this.posRect; }
            set { this.posRect = value; }
        }

        /// <summary>
        /// 任务完成状态
        /// </summary>
        private string runStatus;
        public string RunStatus
        {
            get { return this.runStatus; }
            set { this.runStatus = value; }
        }

   
        /// <summary>
        /// 单元格颜色
        /// </summary>
        private Color color = Color.Transparent;
        public Color Color
        {
            get{return this.color;}
            set{this.color = value;}
        }

        /// <summary>
        ///获取或设置单元格位置（左上角坐标点）
        /// </summary>
        private Point location;
        public Point Location
        {
            get { return this.location; }
            set { this.location = value; }
        }

        /// <summary>
        /// 单元格显示的字符串
        /// </summary>
        private string posText;
        public string PosText
        {
            get { return this.posText; }
            set { this.posText = value; }
        }

        /// <summary>
        /// 复制对象只复制数据结构不复制引用
        /// </summary>
        /// <returns></returns>
        public Positions Copy()
        {
            if (!this.GetType().IsSerializable)
            {
                throw new ArgumentException("对象不可以实例化！");

            }
            IFormatter formater = new BinaryFormatter();
            using (Stream stream = new MemoryStream())
            {
                formater.Serialize(stream, this);
                stream.Seek(0, SeekOrigin.Begin);
                return (Positions)formater.Deserialize(stream);
            }
        }
      
    }
}
