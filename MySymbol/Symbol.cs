using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace CosmosGIS.MySymbol
{
 
    //CHENG
    
    public class Symbol
    {
        #region 字段

        string name;    //符号标签
        int red;        //红色通道值
        int blue;       //蓝色通道值
        int green;      //绿色通道值
        protected SymbolType type;//符号类型

        #endregion

        #region 属性

        public string Name { get { return name; }  }
        public int R { get { return red; } set { red = value; } }
        public int G { get { return green; } set { green = value; } }
        public int B { get { return blue; } set { blue = value; } }
        public SymbolType Type { get { return type; } }
        static Random newradom = new Random((int)DateTime.Now.Ticks);
        #endregion

        #region 构造函数

        public Symbol() { }

        #endregion

        #region 方法

        /// <summary>
        /// 使用已有颜色设置RGB
        /// </summary>
        /// <param name="newcolor"></param>
        public void SetColor(Color newcolor)
        {
            red = newcolor.R;
            blue = newcolor.B;
            green = newcolor.G;
        }

        /// <summary>
        /// 获得符号颜色
        /// </summary>
        /// <returns></returns>
        public Color GetColor()
        {
            Color color=Color.FromArgb(red,green,blue);
            return color;
        }

        /// <summary>
        /// 根据符号获得位图
        /// </summary>
        /// <returns></returns>
        internal virtual Bitmap GetBitmap()
        {
            Bitmap sBitmap = new Bitmap(48, 24);
            return sBitmap;
        }

        internal void SingleRadomColor()
        {
        
        //    Random ran = new Random();
          //  R = ran.Next(0, 256);
           // G = ran.Next(R, 256);
            //B = ran.Next(0, R);

            Color newcolor = Color.FromArgb(255, newradom.Next(0, 256), newradom.Next(0, 256), newradom.Next(0, 256));
            this.SetColor(newcolor);
        }

        #endregion
    }
/// <summary>
/// 点状符号类
/// </summary>
    public class PointSymbol:Symbol
    {
        #region 字段

        int styleindex; //索引号
        float size;    //符号尺度
        float xoffset; //x方向偏移量
        float yoffset; //y方向偏移量

        #endregion

        #region 属性

        public int StyleIndex { get { return styleindex; } set { styleindex = value; } }
        public float Size { get { return size; } set { size = value; } }
        public float Xoffset { get { return xoffset; } set { xoffset = value; } }
        public float Yoffset { get { return yoffset; } set { yoffset = value; } }

        #endregion

        #region 构造函数

        public PointSymbol()
        {
            type = SymbolType.PointSymbol;
            //Random index=new Random();
            styleindex = 2;
            size = 5;
            xoffset = 0;
            yoffset = 0;
            SingleRadomColor();
        }

        public PointSymbol(int index)
        {
            type = SymbolType.PointSymbol;
            styleindex = index;
            size = 5;
            xoffset = 0;
            yoffset = 0;
            SingleRadomColor();
            //  Random ran = new Random();
            // R = ran.Next(0, 256);
            //G = ran.Next(R, 256);
            //B = ran.Next(0, R);
        }

        #endregion

        #region 方法

        /// <summary>
        /// 重载获取位图
        /// </summary>
        /// <returns></returns>
        internal override Bitmap GetBitmap()
        {
            Bitmap sBitmap = new Bitmap(48, 24);
            Graphics g = Graphics.FromImage(sBitmap);
            DrawSymbol(g,new PointF(24,12),new PointF(0,0));
            return sBitmap;
        }

        /// <summary>
        /// 绘制点图层（外部调用函数）
        /// </summary>
        /// <param name="g"></param>
        /// <param name="drawpoint"></param>
        public void GetSymbol(Graphics g,PointF drawpoint)
        {
            DrawSymbol(g, drawpoint, new PointF(xoffset, yoffset));
        }

       /// <summary>
       /// 绘制点符号
       /// </summary>
       /// <param name="g"></param>
       /// <param name="drawpoint"></param>
       /// <param name="offset"></param>
        private void DrawSymbol(Graphics g,PointF drawpoint,PointF offset)
        {
            PointF center=new PointF(drawpoint.X+offset.X,drawpoint.Y+offset.Y);
            PointF lefttop=new PointF(center.X-size,center.Y-size);
            Pen newpen = new Pen(this.GetColor(),(float)1.5);
            Brush newbrush = new SolidBrush(this.GetColor());
            RectangleF newRec=new RectangleF(lefttop,new SizeF(size*2,size*2));
            PointF[] newtran=new PointF[3]{new PointF(center.X,center.Y-size),new PointF((center.X-size*(float)Math.Cos(Math.PI/6)),center.Y+size/2),new PointF((center.X+size*(float)Math.Cos(Math.PI/6)),center.Y+size/2)};
            switch(styleindex)
            {
                case 1:
                    g.DrawEllipse(newpen, newRec);
                    break;
                case 2:
                    g.FillEllipse(newbrush, newRec);
                    break;
                case 3:
                    g.DrawRectangle(newpen,lefttop.X,lefttop.Y,size*2,size*2);
                    break;
                case 4:
                    g.FillRectangle(newbrush, newRec);
                    break;
                case 5:
                    g.DrawPolygon(newpen, newtran);
                    break;
                case 6:
                    g.FillPolygon(newbrush, newtran);
                    break;
                case 7:
                    g.DrawEllipse(newpen, newRec);
                    g.FillEllipse(newbrush, new RectangleF((float)(center.X - 0.5), (float)(center.Y - 0.5), (float)1.0, (float)1.0));
                    break;
                case 8:
                    g.DrawEllipse(newpen, newRec);
                    g.DrawEllipse(newpen, new RectangleF((center.X - size / 3), (center.Y - size / 3), size*2/3, size*2/3));
                    break;
                case 9:
                    g.FillEllipse(newbrush, newRec);
                    g.FillEllipse(Brushes.White, new RectangleF((center.X - size / 2), (center.Y - size / 2), size, size));
                    break;
            }
            newpen.Dispose();
            newbrush.Dispose();
        }
        #endregion
    }


    /// <summary>
    /// 线状符号类
    /// </summary>
    public class LineSymbol : Symbol
    {
        #region 字段

        float size;    //符号尺度
        DashStyle style;//虚实情况
        float[] dashpattern = new float[2];//虚实长度比值 

        #endregion

        #region 属性
        //虚实部分长度配合
        public float[] DashPattern { get { return dashpattern; } set { dashpattern = value; } }
        //线宽
        public float  Size { get { return size; } set { size = value; } }
        //虚实情况
        public DashStyle Style { get { return style; } set { style = value; } }
        //获取绘制线符号的画笔
        public Pen GetPen 
        { 
            get 
            { 
                Pen newpen = new Pen(this.GetColor(),size);
                //newpen.DashPattern = DashPattern;
                newpen.DashStyle = style;
                return newpen;
            } 
        }
        #endregion

        #region 构造函数

        public LineSymbol() 
        {
            type = SymbolType.LineSymbol;
            style = DashStyle.Solid;
            dashpattern = new float[2] { 0, 0 };
            size = 3;
            SingleRadomColor();
        }

        #endregion

        #region 方法

        /// <summary>
        /// 重载获取位图
        /// </summary>
        /// <returns></returns>
        internal override Bitmap GetBitmap()
        {
            Bitmap sBitmap = new Bitmap(48, 24);
            Graphics g = Graphics.FromImage(sBitmap);
            Pen sPen = GetPen;
            g.DrawLine(sPen, new Point(4, 12), new Point(44, 12));
            return sBitmap;
        }
        #endregion

    }

    /// <summary>
    /// 面状符号类
    /// </summary>
    public class PolygonSymbol : Symbol
    {
        #region 字段

        int styleindex; //索引号
        LineSymbol linestyle;   //边界线状符号类型

        #endregion

        #region 属性

        public int StyleIndex { get { return styleindex; } set { styleindex = value; } }
        public LineSymbol LineStyle { get { return linestyle; } set { linestyle = value; } }
        
        #endregion

        #region 构造函数

        public PolygonSymbol() 
        {
            type = SymbolType.PolygonSymbol;
            styleindex = 0;
            SingleRadomColor();
            linestyle = new LineSymbol();
        }
        #endregion

        #region 方法

        /// <summary>
        /// 重载获取位图
        /// </summary>
        /// <returns></returns>
        internal override Bitmap GetBitmap()
        {
            Bitmap sBitmap = new Bitmap(48, 24);
            Graphics g = Graphics.FromImage(sBitmap);
            Pen sPen = null;
            SolidBrush sBrush = null;
            Rectangle sRect;
            sBrush = new SolidBrush(this.GetColor());
            sPen =LineStyle.GetPen;
            sRect = new Rectangle(4, 6, 40, 12);
            g.FillRectangle(sBrush, sRect);
            g.DrawRectangle(sPen, sRect);                    
            return sBitmap;
        }
        #endregion

    }

    /// <summary>
    /// 文字注记类
    /// </summary>
    public class TextSymbol
    {
        #region 字段
        LetterStyle textstyle;//	获取或设置字体类型
        double angle;//获取或设置旋转角度（顺时针），默认弧度
        double xoffset;//获取或设置注记位置的X方向偏移
        double yoffset;//	获取或设置注记位置的Y方向偏移
        bool usebackground;//获取或设置是否使用背景色
        Color backgroundcolor;//获取或设置背景色彩
        int backgroundstyle;//	获取或设置背景样式
        bool usebgboundary;//获取或设置背景是否使用带边框
        //LineSymbol bgboundarystyle;//获取或设置边框样式

        #endregion

        #region 属性
        public LetterStyle TextStyle { get { return textstyle; } set { textstyle = value; } }
        public double Angle { get { return angle; } set { angle = value; } }
        public double Xoffset { get { return xoffset; } set { xoffset = value; } }
        public double Yoffset { get { return yoffset; } set { yoffset = value; } }
        public bool UseBackground { get { return usebackground; } set { usebackground = value; } }
        public Color BackgroundColor { get { return backgroundcolor; } set { backgroundcolor = value; } }
        public int BackgroundStyle { get; set; }
        public bool UseBgBoundary { get; set; }
        //public LineSymbol BgBoundaryStyle { get; set; }
        #endregion

        #region 构造函数
        public TextSymbol()
        {
            textstyle = new LetterStyle();
            angle = 0;
            xoffset = 0;
            yoffset = 0;
            usebackground = false;
        }


        #endregion

        #region 方法
        #region 点的注记
        public static void PointLabel(PointF point, Graphics g, TextSymbol symbol, string text)
        {
            PointF LT = new PointF((float)(point.X + symbol.Xoffset), (float)(point.Y + symbol.Yoffset));
            Brush mybrush = new SolidBrush(symbol.TextStyle.TextColor);
            g.DrawString(text, symbol.TextStyle.TextFont, mybrush, LT);
        }

        #endregion

        #region 线的注记
        public static void LineLabel(PointF[] point, Graphics g, TextSymbol symbol, string text)
        {
            PointF[] result = SetLineLabelLoc(point, text.Length);
            for (int i = 0; i < text.Length; i++)
            {
                PointLabel(result[i], g, symbol, text[i].ToString());
            }
        }

        private static PointF[] SetLineLabelLoc(PointF[] linepoint, int wordcount)
        {
            //起始点到以后每点的累计距离
            List<double> adddis = new List<double>();
            //结果数组
            List<PointF> result = new List<PointF>();
            int count = linepoint.Length;
            adddis.Add(TwoPointDis(linepoint[0], linepoint[1]));
            for (int i = 1; i < count - 1; i++)
            {
                adddis.Add(adddis[i - 1] + TwoPointDis(linepoint[i], linepoint[i + 1]));
            }
            double k = adddis[count - 2] / (wordcount + 1);
            double labelnow = k;

            for (int i = 0; i < wordcount; i++)
            {
                while (labelnow < adddis[0])
                {
                    result.Add(GetSinglePoint(linepoint[0], linepoint[1], labelnow));
                    labelnow += k;
                    i++;
                }
                for (int j = 1; j < adddis.Count; j++)
                {
                    while (adddis[j - 1] <= labelnow && adddis[j] > labelnow)
                    {
                        result.Add(GetSinglePoint(linepoint[j], linepoint[j + 1], labelnow - adddis[j - 1]));
                        labelnow += k;
                        i++;
                    }
                }
            }
            return result.ToArray();
        }

        private static double TwoPointDis(PointF a, PointF b)
        {
            double dis = Math.Sqrt((a.X - b.X) * (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y));
            return dis;
        }

        /// <summary>
        /// 获得某线段上某距离点位
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="dis"></param>
        /// <returns></returns>
        private static PointF GetSinglePoint(PointF a, PointF b, double dis)
        {
            float dx = b.X - a.X;
            float dy = b.Y - a.Y;
            float k = (float)(dis / TwoPointDis(a, b));
            return (new PointF(a.X + k * dx, a.Y + k * dy));
        }

        #endregion

        #region 面的注记
        public static void PolygonLabel(PointF[] point, Graphics g, TextSymbol symbol, string text)
        {
            PointF[] result = SetPolygonLabelLoc(point, symbol.TextStyle.TextSize, text.Length);
            for (int i = 0; i < text.Length; i++)
            {
                PointLabel(result[i], g, symbol, text[i].ToString());
            }
        }

        private static PointF[] SetPolygonLabelLoc(PointF[] polypoint, double size, int wordcount)
        {
            double labellength = (wordcount * 2 - 1) * size;
            PointF center = GetCenter(polypoint);
            PointF LT = new PointF((float)(center.X - labellength / 2), center.Y);
            List<PointF> result = new List<PointF>();
            result.Add(LT);
            for (int i = 1; i < wordcount; i++)
            {
                result.Add(new PointF((float)(LT.X + size * 2 * i), LT.Y));
            }
            return result.ToArray();
        }


        private static PointF GetCenter(PointF[] polypoint)
        {
            float minx = 1000000;
            float maxx = -100;
            float miny2;
            float maxy2;
            float centerx;
            float centery;
            for (int i = 0; i < polypoint.Length; i++)
            {
                if (minx > polypoint[i].X)
                    minx = polypoint[i].X;
                if (maxx < polypoint[i].X)
                    maxx = polypoint[i].X;
            }
            float midlex = (maxx + minx) / 2;
            List<float> midley = new List<float>();
            for (int i = 0; i < polypoint.Length - 1; i++)
            {
                if (midlex > polypoint[i].X && midlex < polypoint[i + 1].X)
                {
                    float dx = polypoint[i + 1].X - polypoint[i].X;
                    float dy = polypoint[i + 1].Y - polypoint[i].Y;
                    float disx = midlex - polypoint[i].X;
                    midley.Add(polypoint[i].Y + dy * disx / dx);
                }
                if (midlex < polypoint[i].X && midlex > polypoint[i + 1].X)
                {
                    float dx = polypoint[i + 1].X - polypoint[i].X;
                    float dy = polypoint[i + 1].Y - polypoint[i].Y;
                    float disx = midlex - polypoint[i].X;
                    midley.Add(polypoint[i].Y + dy * disx / dx);
                }
            }
            //防孔 todo
            midley.Sort();
            if (midley.Count != 0)
            {
                miny2 = midley[0];
                maxy2 = midley[midley.Count - 1];
                centerx = midlex;
                centery = (miny2 + maxy2) / 2;
                return (new PointF(centerx, centery));
            }
            else
                return (new PointF());

        }
        #endregion
        #endregion


    }

    /// <summary>
    /// 字体设置类
    /// </summary>
    public class LetterStyle
    {
        #region 字段
        Color textcolor;        //颜色
        Font textstyle;         // 样式

        #endregion

        #region 属性
        public Color TextColor { get { return textcolor; } set { textcolor = value; } }
        public string TextStyle { get { return textstyle.Name; } set { textstyle = new Font(value, textstyle.Size, textstyle.Style); } }
        public double TextSize { get { return textstyle.Size; } set { textstyle = new Font(textstyle.Name, (float)value, textstyle.Style); } }
        public bool IsBold
        {
            get { return textstyle.Bold; }
            set
            {
                if (value != IsBold)
                {
                    if (value == true && textstyle.Italic == true)
                        textstyle = new Font(textstyle.Name, textstyle.Size, FontStyle.Italic | FontStyle.Bold);
                    else if (value == true)
                        textstyle = new Font(textstyle.Name, textstyle.Size, FontStyle.Bold);
                    else if (value == false && textstyle.Italic == true)
                        textstyle = new Font(textstyle.Name, textstyle.Size, FontStyle.Italic);
                    else
                        textstyle = new Font(textstyle.Name, textstyle.Size, FontStyle.Regular);

                }
            }
        }
        public bool IsItalic
        {
            get { return textstyle.Italic; }
            set
            {
                if (value != IsItalic)
                {
                    if (value == true && textstyle.Bold == true)
                        textstyle = new Font(textstyle.Name, textstyle.Size, FontStyle.Italic | FontStyle.Bold);
                    else if (value == true)
                        textstyle = new Font(textstyle.Name, textstyle.Size, FontStyle.Italic);
                    else if (value == false && textstyle.Bold == true)
                        textstyle = new Font(textstyle.Name, textstyle.Size, FontStyle.Bold);
                    else
                        textstyle = new Font(textstyle.Name, textstyle.Size, FontStyle.Regular);

                }
            }
        }
        public Font TextFont { get { return textstyle; } set { textstyle = value; } }
        #endregion

        #region 构造函数
        public LetterStyle()
        {
            textcolor = Color.Black;
            textstyle = new Font("Arial", 8F);
        }
        public LetterStyle(Color color, Font font)
        {
            textcolor = color;
            textstyle = font;
        }
        #endregion

        #region 方法

        #endregion
    }

}