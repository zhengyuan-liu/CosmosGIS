using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CosmosGIS.Map;
using CosmosGIS.MySymbol;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace CosmosGIS.MyRenderer
{
    //CHENG

    /// <summary>
    /// 渲染类基类
    /// </summary>
    public class Renderer
    {
        #region 字段
        protected MySpaceDataType datatype;//数据类型
        #endregion

        #region 属性
        MySpaceDataType DataType { get { return datatype; } }
        public virtual int SymbolCount { get; protected set; }
        public virtual string Field { get; set; }
        public virtual Type Fieldtype { get; set; }
        #endregion

        #region 构造函数
        public Renderer() { }
        public Renderer(MySpaceDataType datatype_value) { datatype = datatype_value; }
        #endregion

        #region 方法
        /// <summary>
        /// 根据要素的字段值获得对应符号
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual Symbol FindSymbol(string value) { return null; }

        /// <summary>
        /// 根据要素的FID获取要素对应符号
        /// </summary>
        /// <param name="fid"></param>
        /// <returns></returns>
        public virtual Symbol GetSymbolByID(int fid)
        {
            return null;
        }

        /// <summary>
        /// 复制渲染方式
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        public virtual void CopyRenderer(Renderer from)
        {
            this.datatype = from.DataType;
        }

        /// <summary>
        /// 创建要素
        /// </summary>
        /// <returns></returns>
        public Symbol CreatNewSymbol()
        {
            Symbol newsymbol;
            switch (datatype)
            {
                case MySpaceDataType.MyPoint:
                    newsymbol = new PointSymbol();
                    break;
                case MySpaceDataType.MyPolyLine:
                    newsymbol = new LineSymbol();
                    break;
                case MySpaceDataType.MyPolygon:
                    newsymbol = new PolygonSymbol();
                    break;
                default:
                    newsymbol = new Symbol();
                    break;
                //防错TODO&栅格文件渲染方式再议
            }
            return newsymbol;
        }
        #endregion

    }

    /// <summary>
    /// 简单渲染
    /// </summary>
    public class SimpleRenderer : Renderer
    {
        #region 字段
        Symbol symbolstyle;
        #endregion

        #region 属性
        public override int SymbolCount
        {
            get
            {
                return 1;
            }

            protected set
            {
               
            }
        }
        public Symbol SymbolStyle { get { return symbolstyle; } set { symbolstyle = value; } }
        #endregion

        #region 构造函数
        //public SimpleRenderer()        {  symbolstyle=new Symbol (); }

        public SimpleRenderer(MySpaceDataType datatype_value)
        {
            SymbolCount = 1;
            datatype = datatype_value;
            symbolstyle = CreatNewSymbol();
        }

        #endregion
        
        #region 方法

        public override Symbol FindSymbol(string value) { return symbolstyle; }

        public override void CopyRenderer(Renderer from)
        {
            this.symbolstyle = ((SimpleRenderer)from).SymbolStyle;
        }

        #endregion
    }

    /// <summary>
    /// 唯一值渲染
    /// </summary>
    public class UniqueValueRenderer : Renderer
    {
        #region 字段
        string field;           //所选渲染字段
        Type fieldtype;
        List<Symbol> symbolstyles; //符号列表
        List<string> values;       //唯一值列表  
        static Random ran = new Random();
        #endregion

        #region 属性
        public override string Field { get { return field; } set { field = value; } }
        public override Type Fieldtype
        {
            get { return fieldtype; }
            set { fieldtype = value; }
        }
        public override int SymbolCount { get { return symbolstyles.Count; } }
        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数，参数不能修改
        /// </summary>
        /// <param name="datatype_value">要素的类型</param>
        /// <param name="newfieldname">渲染字段名称</param>
        /// <param name="newvalues">所有该字段的值</param>
        public UniqueValueRenderer(MySpaceDataType datatype_value, string newfieldname, List<string> newvalues)
        {
            newvalues = newvalues.Distinct().ToList();
            datatype = datatype_value;
            field = newfieldname;
            values = newvalues;
            symbolstyles = new List<Symbol>();
            RandonSymbol();
            SymbolCount = symbolstyles.Count;
        }

        #endregion


        #region 方法
        //TODO 批量修改



        public string FindValue(int index) { return values[index]; }


        public override void CopyRenderer(Renderer from)
        {
            try
            {
                UniqueValueRenderer renderer = (UniqueValueRenderer)from;
                this.field = renderer.Field;
                int count = renderer.SymbolCount;
                for (int i = 0; i < count; i++)
                    this.SetSymbolByValue(values[i], renderer.FindSymbol(values[i]));
            }
            catch
            {
                
            }
        }


        /// <summary>
        /// 返回特定索引号的符号
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Symbol FindSymbolByIndex(int index) { return symbolstyles[index]; }

        /// <summary>
        /// 返回指定值的符号
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override Symbol FindSymbol(string value)
        {
            int index = values.FindIndex(x => x == value);
            return symbolstyles[index];
        }

        /// <summary>
        /// 按索引设置符号
        /// </summary>
        /// <param name="index"></param>
        /// <param name="newsymbol"></param>
        public void SetSymbol(int index, Symbol newsymbol) { symbolstyles[index] = newsymbol; }

        /// <summary>
        /// 按值匹配修改符号
        /// </summary>
        /// <param name="value"></param>
        /// <param name="newsymbol"></param>
        public void SetSymbolByValue(string value, Symbol newsymbol)
        {
            int index = values.FindIndex(x => x == value);
            symbolstyles[index] = newsymbol;
        }

        /// <summary>
        /// 添加新值符号
        /// </summary>
        /// <param name="newvalue"></param>
        public void AddSymbol(string newvalue)
        {
            int valuecount = values.Count;
            values.Add(newvalue);
            values = values.Distinct().ToList();
            if (values.Count == valuecount)        //若所加值已存在
            {
                return;
            }
            else//添加新值，新符号
            {
                symbolstyles.Add(CreatNewSymbol());
            }

        }

        /// <summary>
        /// 自动生成渲染符号列表
        /// </summary>
        private void RandonSymbol()
        {
            for (int i = 0; i < values.Count; i++)
            {
                symbolstyles.Add(CreatNewSymbol());
                symbolstyles[symbolstyles.Count - 1].SetColor(Color.FromArgb(255, ran.Next(0, 256), ran.Next(0, 256), ran.Next(0, 256)));
            }
        }

        #endregion
    }
        
    /// <summary>
    /// 分级渲染
    /// </summary>
    public class ClassBreakRenderer : Renderer
    {
        #region 字段
        string field;           //所选渲染字段
        System.Type fieldtype;
        List<Symbol> symbolstyles; //符号列表
        List<double> values;         //值列表
        List<double> breaks;       //分级断点列表       
        int symbolmode;           //模式：1、颜色分级；2、大小分级
        #endregion

        #region 属性
        public override string Field { get { return field; } set { field = value; } }
        public override int SymbolCount { get { return symbolstyles.Count; } }
        internal List<double> Value { get { return values; } }
        public int BreakCount { get { return breaks.Count; } }
        public List<double> Breaks { get { return breaks; } set { breaks = value; } }
        public int Mode { get { return symbolmode; } set { symbolmode = value; } }
        public double MinValue { get { values.Sort(); return values[0]; } }
        public double MaxValue { get { values.Sort(); return values[values.Count - 1]; } }
        #endregion

        #region 构造函数

        /// <summary>
        /// 分为三级的默认构造函数，分级方式为颜色分级
        /// </summary>
        /// <param name="datatype_value"></param>
        /// <param name="newfieldname"></param>
        /// <param name="newvalues"></param>
        public ClassBreakRenderer(MySpaceDataType datatype_value, string newfieldname, List<double> newvalues)
        {
            datatype = datatype_value;
            field = newfieldname;
            values = newvalues;
            values.Sort();
            BreakEqualByValue(3);
            symbolmode = 1;
            symbolstyles = new List<Symbol>();
            AutoSetColor(Color.Red, Color.Green);
            SymbolCount = symbolstyles.Count;
        }
        /// <summary>
        /// 分为三级的默认构造函数，分级方式可选择颜色或大小
        /// </summary>
        /// <param name="datatype_value"></param>
        /// <param name="newfieldname"></param>
        /// <param name="newvalues"></param>
        /// <param name="newmode"></param>
        public ClassBreakRenderer(MySpaceDataType datatype_value, string newfieldname, List<double> newvalues, int newmode)
        {
            datatype = datatype_value;
            field = newfieldname;
            values = newvalues;
            values.Sort();
            BreakEqualByValue(3);
            symbolmode = newmode;
            symbolstyles = new List<Symbol>();
            if (newmode == 1)
                AutoSetColor(Color.Red, Color.Green);
            else if (newmode == 2)
                AutoSetSize(0.5, (0.5 + BreakCount * 0.5));
            SymbolCount = symbolstyles.Count;
        }
        /// <summary>
        /// 自定义分级层数的颜色分级构造函数
        /// </summary>
        /// <param name="datatype_value"></param>
        /// <param name="newfieldname"></param>
        /// <param name="newvalues"></param>
        /// <param name="breakcount"></param>
        public ClassBreakRenderer(MySpaceDataType datatype_value, int breakcount, string newfieldname, List<double> newvalues)
        {
            datatype = datatype_value;
            field = newfieldname;
            values = newvalues;
            values.Sort();
            BreakEqualByValue(breakcount);
            symbolmode = 1;
            symbolstyles = new List<Symbol>();
            AutoSetColor(Color.Red, Color.Green);
            SymbolCount = symbolstyles.Count;
        }
        /// <summary>
        /// 自定义分级级数的分级方法，可选分级模式
        /// </summary>
        /// <param name="datatype_value"></param>
        /// <param name="newfieldname"></param>
        /// <param name="newvalues"></param>
        /// <param name="breakcount"></param>
        /// <param name="newmode"></param>
        public ClassBreakRenderer(MySpaceDataType datatype_value, int breakcount, string newfieldname, List<double> newvalues, int newmode)
        {
            datatype = datatype_value;
            field = newfieldname;
            values = newvalues;
            values.Sort();
            BreakEqualByValue(breakcount);
            symbolmode = newmode;
            symbolstyles = new List<Symbol>();
            if (newmode == 1)
                AutoSetColor(Color.Red, Color.Green);
            else if (newmode == 2)
                AutoSetSize(0.5, (0.5 + BreakCount * 0.5));
            SymbolCount = symbolstyles.Count;
        }

        #endregion

        #region 方法
        //TODO 批量修改

        public override void CopyRenderer(Renderer from)
        {
            try
            {
                ClassBreakRenderer renderer = (ClassBreakRenderer)from;
                int count = renderer.BreakCount + 1;
                breaks = renderer.Breaks;
                symbolmode = renderer.Mode;
                for (int i = 0; i < count; i++)
                    this.symbolstyles[i] = renderer.FindSymbolByIndex(i);
            }
            catch
            {

            }
        }

        /// <summary>
        /// 均分
        /// </summary>
        /// <param name="levelcount">等级数量</param>
        public void BreakEqualByValue(int levelcount)
        {
            breaks = new List<double>();
            double max = values[values.Count - 1];
            double min = values[0];
            double k = (max - min) / levelcount;
            for (int i = 1; i < levelcount; i++)
                breaks.Add(min + k * i);
        }

        /// <summary>
        /// 手动设置断点触发事件
        /// </summary>
        /// <param name="newbreaks"></param>
        public void ClassBreakChange(List<double> newbreaks)
        {
            breaks = newbreaks;
            symbolstyles.Clear();
            if (Mode == 1)
                AutoSetColor(Color.Red, Color.Green);
            else if (Mode == 2)
                if (BreakCount < 5)
                    AutoSetSize(2, (2 + BreakCount * 1));
                else
                    AutoSetSize(2, (2 + BreakCount * 0.5));
            SymbolCount = symbolstyles.Count;
        }
        /// <summary>
        /// 手动更改模式触发事件
        /// </summary>
        /// <param name="newmode"></param>
        public void ClassModeChange(int newmode)
        {
            symbolstyles.Clear();
            Mode = newmode;
            ClassBreakChange(breaks);
        }

        /// <summary>
        /// 返回特定索引号的符号
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Symbol FindSymbolByIndex(int index) { return symbolstyles[index]; }

        /// <summary>
        /// 返回指定值的符号
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override Symbol FindSymbol(string value)
        {
            double dvalue;
            try
            {
                dvalue = Convert.ToDouble(value);
                for (int i = 0; i < breaks.Count; i++)
                {
                    if (dvalue < breaks[i])
                        return symbolstyles[i];
                }
                return symbolstyles[breaks.Count];
            }
            catch
            {
                //todo
                return null;
            }
        }

        /// <summary>
        /// 按索引设置符号
        /// </summary>
        /// <param name="index"></param>
        /// <param name="newsymbol"></param>
        public void SetSymbol(int index, Symbol newsymbol) { symbolstyles[index] = newsymbol; }

        /// <summary>
        /// 按值匹配修改符号
        /// </summary>
        /// <param name="value"></param>
        /// <param name="newsymbol"></param>
        public void SetSymbolByValue(double value, Symbol newsymbol)
        {
            int index = 0;
            if (value >= breaks[breaks.Count - 1])
                index = breaks.Count;
            else
            {
                for (int i = 0; i < breaks.Count; i++)
                {
                    if (value < breaks[i])
                    {
                        index = i;
                        break;
                    }
                }
            }
            symbolstyles[index] = newsymbol;
        }


        /// <summary>
        /// 自动生成符号大小等级渲染符号列表
        /// </summary>
        /// <param name="minsize"></param>
        /// <param name="maxsize"></param>
        private void AutoSetSize(double minsize, double maxsize)
        {
            double k = (double)(maxsize - minsize) / (breaks.Count);        //平均每级的变化幅度
            for (int i = 0; i < breaks.Count + 1; i++)
            {
                if (datatype == MySpaceDataType.MyPoint)
                {
                    Symbol basesymbol = CreatNewSymbol();
                    PointSymbol newsymbol = (PointSymbol)basesymbol;
                    newsymbol.Size = (float)(minsize + i * k);
                    symbolstyles.Add(newsymbol);
                }
                if (datatype == MySpaceDataType.MyPolyLine)
                {
                    Symbol basesymbol = CreatNewSymbol();
                    LineSymbol newsymbol = (LineSymbol)basesymbol;
                    newsymbol.Size = (float)(minsize + i * k);
                    symbolstyles.Add(newsymbol);
                }
            }
        }


        /// <summary>
        /// 获取渐变色带
        /// </summary>
        /// <param name="startcolor"></param>
        /// <param name="endcolor"></param>
        /// <returns></returns>
        private Bitmap GetLinearGradient(Color startcolor, Color endcolor)
        {
            Rectangle Rec = new Rectangle(new Point(0, 0), new Size(100, 20));
            LinearGradientBrush LGB = new LinearGradientBrush(Rec, startcolor, endcolor, LinearGradientMode.Horizontal);
            Bitmap LinearGradient = new Bitmap(100, 20);
            Graphics g = Graphics.FromImage(LinearGradient);
            g.FillRectangle(LGB, Rec);
            LGB.Dispose();
            return LinearGradient;
        }

        public Bitmap ShowLinearGradient()
        {
            int count = symbolstyles.Count;
            Color startcolor = Color.FromArgb(symbolstyles[0].R, symbolstyles[0].G, symbolstyles[0].B);
            Color endcolor = Color.FromArgb(symbolstyles[count - 1].R, symbolstyles[count - 1].G, symbolstyles[count - 1].B);
            Rectangle Rec = new Rectangle(new Point(0, 0), new Size(200, 20));
            LinearGradientBrush LGB = new LinearGradientBrush(Rec, startcolor, endcolor, LinearGradientMode.Horizontal);
            Bitmap LinearGradient = new Bitmap(200, 20);
            Graphics g = Graphics.FromImage(LinearGradient);
            g.FillRectangle(LGB, Rec);
            LGB.Dispose();
            return LinearGradient;
        }

        public void SetColor(Color startcolor, Color endcolor)
        {
            AutoSetColor(startcolor, endcolor);
        }

        /// <summary>
        /// 自动生成颜色等级渲染符号列表
        /// </summary>
        /// <param name="startcolor"></param>
        /// <param name="endcolor"></param>
        private void AutoSetColor(Color startcolor, Color endcolor)
        {
            /*
            //使用HSB生成连续颜色
            float starthue = startcolor.GetHue();
            float startsat = startcolor.GetSaturation();
            float startbri = startcolor.GetBrightness();
            float endhue = endcolor.GetHue();
            float endsat = endcolor.GetSaturation();
            float endbri = endcolor.GetBrightness();

            float k_hue = (endhue - starthue) / BreakCount;
            float k_sat = (endsat - startsat) / BreakCount;
            float k_bri = (endbri - startbri) / BreakCount;
            for (int i = 0; i < breaks.Count + 1; i++)
            {
                Symbol basesymbol = CreatNewSymbol();
                Symbol newsymbol = basesymbol;
                newsymbol.SetColor(HSBtoColor(starthue + k_hue * i, startsat + k_sat * i, startbri + k_bri * i));
                symbolstyles.Add(newsymbol);
            }
            */
            Bitmap Linear = GetLinearGradient(startcolor, endcolor);
            float width = Linear.Width;
            float height = Linear.Height;
            float k = width / (breaks.Count + 2);
            symbolstyles.Clear();
            for (int i = 0; i < breaks.Count + 1; i++)
            {
                Symbol basesymbol = CreatNewSymbol();
                Symbol newsymbol = basesymbol;
                Color newcolor = Linear.GetPixel((int)((i + 1) * k), (int)height / 2);
                newsymbol.SetColor(newcolor);
                symbolstyles.Add(newsymbol);
            }
        }

        private Color HSBtoColor(float h, float s, float b)
        {
            int standar = (int)Math.Floor(h / 60) % 6;
            float f = h / 60 - standar;
            float p = b * (1 - s);
            float q = b * (1 - f * s);
            float t = b * (1 - (1 - f) * s);
            switch (standar)
            {
                case 0:
                    return Color.FromArgb((int)Math.Round(255 * b), (int)Math.Round(255 * t), (int)Math.Round(255 * p));
                case 1:
                    return Color.FromArgb((int)Math.Round(255 * q), (int)Math.Round(255 * b), (int)Math.Round(255 * p));
                case 2:
                    return Color.FromArgb((int)Math.Round(255 * p), (int)Math.Round(255 * b), (int)Math.Round(255 * t));
                case 3:
                    return Color.FromArgb((int)Math.Round(255 * p), (int)Math.Round(255 * q), (int)Math.Round(255 * b));
                case 4:
                    return Color.FromArgb((int)Math.Round(255 * t), (int)Math.Round(255 * p), (int)Math.Round(255 * b));
                default:
                    return Color.FromArgb((int)Math.Round(255 * b), (int)Math.Round(255 * p), (int)Math.Round(255 * q));
            }

        }

        #endregion
    }

    /// <summary>
    /// 注记渲染
    /// </summary>
    public class TextRenderer
    {
        #region 字段
        string field;
        TextSymbol textsymbol;
        string sql;
        #endregion

        #region 属性
        public string Field { get { return field; } set { field = value; } }
        public string SQL { get { return sql; } set { sql = value; } }
        public bool HasSQL { get { if (sql == null) return false; else return true; } }
        public TextSymbol TextSymol { get { return textsymbol; }set { textsymbol = value; } }
        //TODO 定位函数
        #endregion

        #region 构造函数
        public TextRenderer(TextSymbol symbol,String fieldvalue)
        {
            field = fieldvalue;
            //todo
            //field = "FID"; 
            textsymbol = symbol;
            sql = null;
        }

        public TextRenderer(String fieldvalue)
        {
            field = fieldvalue;
            //todo
            //field = "FID"; 
            textsymbol = new TextSymbol();
            sql = null;
        }

        public TextRenderer()
        {
            //field = fieldvalue;
            //todo
            field = "FID";
            textsymbol = new TextSymbol();
            sql = "";
        }
        #endregion

        #region 方法


        public void FeildTextShow()
        {

        }

        #endregion
    }
}
