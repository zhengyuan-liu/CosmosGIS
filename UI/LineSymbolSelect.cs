using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CosmosGIS.MySymbol;
using System.Drawing.Drawing2D;

namespace CosmosGIS.UI
{
    public partial class LineSymbolSelect : UserControl
    {
        LineSymbol symbol;
        LineSymbol layersymbol;
        int selectindex=0;

        public LineSymbolSelect(LineSymbol nowsymbol)
        {
            symbol = nowsymbol;
            layersymbol = nowsymbol;
            InitializeComponent();
            LineSymbolSelect_Load();
            symbol = new LineSymbol();
        }

        /// <summary>
        /// 根据符号进行初始化设置
        /// </summary>
        private void LineSymbolSelect_Load()
        {
            btn_color.BackColor = symbol.GetColor();
            ShowSymbol();
            listview_Load();
            CB_size_Load();
            foreach (Control c in this.SymbolListView.Controls)
            {
                c.MouseClick += C_DoubleClick;
            }
        }

        /// <summary>
        /// 线形设置
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private DashStyle SetDashStyle(int index)
        {
            switch(index)
            {
                case 2:
                    return DashStyle.Dash;
                case 3:
                    return DashStyle.DashDot;
                case 4:
                    return DashStyle.DashDotDot;
                case 5:
                    return DashStyle.Dot;
                default:
                    return DashStyle.Solid;

            }
        }

        /// <summary>
        /// 按照选项展示符号
        /// </summary>
        private void ShowSymbol()
        {
            if (CB_size.SelectedItem != null)
            {
                symbol.SetColor(btn_color.BackColor);
                symbol.Size = (float)Convert.ToDouble(CB_size.SelectedItem.ToString());
            }

            Bitmap newimg = new Bitmap(PB_show.Width, PB_show.Height);
            Graphics g = Graphics.FromImage(newimg);
            g.DrawImage(symbol.GetBitmap(), new PointF((PB_show.Width - 48) / 2, (PB_show.Height - 24) / 2));
            PB_show.Image = newimg;
        }

        /// <summary>
        /// 加入图片
        /// </summary>
        private void listview_Load()
        {
            for (int i = 0; i < 5; i++)
            {
                LineSymbol newsymbol = new LineSymbol();
                newsymbol.Style = SetDashStyle(i + 1);
                newsymbol.SetColor(Color.Black);
                Label newlabel = new Label();
                Bitmap bit = newsymbol.GetBitmap();
                newlabel.Image = bit;
                newlabel.Size = new Size(60, 60);
                newlabel.Location = new Point( 10 + 70 * (int)(i % 5),10);
                SymbolListView.Controls.Add(newlabel);
            }
        }

        private void Yes_Click(object sender, EventArgs e)
        {
            layersymbol = symbol;
            ((SymbolSelect)this.ParentForm).NewSymbol = symbol;
            this.ParentForm.DialogResult = DialogResult.OK;
            //加赋值
            this.ParentForm.Close();
        }

        private void btn_color_Click(object sender, EventArgs e)
        {
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                btn_color.BackColor = colorDialog1.Color;
                symbol.SetColor(colorDialog1.Color);
                ShowSymbol();

            }
        }

        private void CB_size_Load()
        {
            CB_size.BeginUpdate();            //让控件一次刷新而不逐条刷新
            //添加字段
            for (int i = 1; i < 12; i++)
            {
                CB_size.Items.Add((double)(i*0.5));
            }
            CB_size.SelectedItem = (double)symbol.Size;
            CB_size.EndUpdate();
            CB_size.Refresh();

        }

        private void CB_size_SelectedIndexChanged(object sender, EventArgs e)
        {
            symbol.Size = (float)(Convert.ToDouble(CB_size.SelectedItem));
            ShowSymbol();

        }

        private void C_DoubleClick(object sender, EventArgs e)
        {
            ((Label)SymbolListView.Controls[selectindex]).BorderStyle = BorderStyle.None;
            ((Label)sender).BorderStyle = BorderStyle.FixedSingle;
            selectindex = SymbolListView.Controls.IndexOf((Control)sender);
            symbol.Style = SetDashStyle(selectindex+1);
            ShowSymbol();
        }

        private void SymbolListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectindex = SymbolListView.SelectedIndices[0];
            symbol.Style = SetDashStyle(selectindex+1);
            ShowSymbol();
        }
    }
}
