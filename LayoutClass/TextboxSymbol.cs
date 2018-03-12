using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace CosmosGIS.LayoutClass
{
    class TextboxSymbol
    {
        #region 字段
        public Label textbox;
        #endregion

        #region 属性
        /// <summary>
        /// 设置文本框左上角的点位
        /// </summary>
        public Point LeftTop { get { return textbox.Location; } set { textbox.Location = value; } }
        /// <summary>
        /// 设置文本框内容
        /// </summary>
        public string Text { get { return textbox.Text; } set { textbox.Text = value; } }
        /// <summary>
        /// 字体风格
        /// </summary>
        public Font TextStyle { get { return textbox.Font; } set { textbox.Font = value; } }
        /// <summary>
        /// 字体颜色
        /// </summary>
        public Color TextColor { get { return textbox.ForeColor; } set { textbox.ForeColor = value; } }
        

        #endregion

        #region 构造函数
        public TextboxSymbol()
        {
            textbox = new Label();
            textbox.AutoSize = true;
            textbox.Location = new Point(10,10);
            textbox.Name = "TextboxSymbol";
        }
        public TextboxSymbol(Point location,string text)
        {
            textbox = new Label();
            
            textbox.Font = new Font("宋体", 10, FontStyle.Bold);
            textbox.Location = location;
            textbox.Text = text;
            textbox.Name = "TextboxSymbol";
            textbox.AutoSize = true;
        }
        public TextboxSymbol(Point location,Font font,Color color, string text)
        {
            textbox = new Label();
            textbox.Font = font;
            textbox.ForeColor = color;
            textbox.Location = location;
            textbox.Text = text;
            textbox.Tag = "TextboxSymbol";
            textbox.AutoSize = true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="from"></param>
        /// <param name="location"></param>
        /// <param name="resizescale">新的大小比上旧的大小</param>
        public TextboxSymbol(TextboxSymbol from,Point location,double resizescale)
        {
            textbox = new Label();
            textbox.Name = "TextboxSymbol";           
            textbox.Size = new Size((int)(from.textbox.Width * resizescale), (int)(from.textbox.Height * resizescale));
            textbox.Font = new Font(from.TextStyle.Name,(float)(from.TextStyle.Size*resizescale),from.TextStyle.Style);
            textbox.ForeColor = from.TextColor;
            textbox.Location = location;
            textbox.Text = from.Text;
            textbox.AutoSize = true;
        }
        #endregion

        #region 方法
        /// <summary>
        /// 显示文本框
        /// </summary>
        /// <param name="e"></param>
        public void Show(PictureBox e)
        {
            e.Controls.Add(textbox);
        }

        public void ResetSize(Point newlocation, Size newsize)
        {
        this.textbox.Location = newlocation;
        this.textbox.Size = newsize;
        }

        #endregion
    }
}
