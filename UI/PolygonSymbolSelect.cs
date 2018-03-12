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
    public partial class PolygonSymbolSelect : UserControl
    {
        PolygonSymbol polygonsymbol;
        PolygonSymbol layersymbol;
        int selectindex;

        public PolygonSymbolSelect(PolygonSymbol nowsymbol)
        {
            polygonsymbol = nowsymbol;
            layersymbol = nowsymbol;
            InitializeComponent();
            PolygonSymbolSelect_Load();
            polygonsymbol = new PolygonSymbol();
        }

        /// <summary>
        /// 线形设置
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private DashStyle SetDashStyle(int index)
        {
            switch (index)
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
                polygonsymbol.SetColor(btn_color.BackColor);
                polygonsymbol.LineStyle.SetColor(btn_color.BackColor);
                if (HasBoundary.Checked == true)
                {
                    polygonsymbol.LineStyle.SetColor(btn_boundcolor.BackColor);
                    polygonsymbol.LineStyle.Size = (float)Convert.ToDouble(CB_size.SelectedItem.ToString());
                }
            }
            Bitmap newimg = new Bitmap(PB_show.Width, PB_show.Height);
            Graphics g = Graphics.FromImage(newimg);
            g.DrawImage(polygonsymbol.GetBitmap(), new PointF((PB_show.Width - 48) / 2, (PB_show.Height - 24) / 2));
            PB_show.Image = newimg;
        }

        private void SymbolListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectindex = SymbolListView.SelectedIndices[0];
            if (HasBoundary.Checked == true)
            {
                polygonsymbol.LineStyle.Style = SetDashStyle(selectindex + 1);
            }
            ShowSymbol();
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
                newlabel.Location = new Point(10 + 70 * (int)(i % 5), 10);
                SymbolListView.Controls.Add(newlabel);
            }
        }

        private void PolygonSymbolSelect_Load()
        {
            btn_color.BackColor = polygonsymbol.GetColor();
            ShowSymbol();
            listview_Load();
            CB_size_Load();
            if(polygonsymbol.GetColor()!=polygonsymbol.LineStyle.GetColor())
                HasBoundary.Checked = true;
            foreach (Control c in this.SymbolListView.Controls)
            {
                c.MouseClick += C_DoubleClick;
            }
        }

        private void btn_color_Click(object sender, EventArgs e)
        {
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                btn_color.BackColor = colorDialog1.Color;
                polygonsymbol.SetColor(colorDialog1.Color);
                if(HasBoundary.Checked!=true)
                {
                    polygonsymbol.LineStyle.SetColor(colorDialog1.Color);
                }
                ShowSymbol();

            }
        }

        private void btn_boundcolor_Click(object sender, EventArgs e)
        {
            if (HasBoundary.Checked == true)
            {
                if (this.colorDialog1.ShowDialog() == DialogResult.OK)
                {
                    btn_boundcolor.BackColor = colorDialog1.Color;
                    polygonsymbol.LineStyle.SetColor(colorDialog1.Color);
                    ShowSymbol();

                }
            }
        }

        private void C_DoubleClick(object sender, EventArgs e)
        {
            if (HasBoundary.Checked == true)
            {
                ((Label)SymbolListView.Controls[selectindex]).BorderStyle = BorderStyle.None;
                ((Label)sender).BorderStyle = BorderStyle.FixedSingle;
                selectindex = SymbolListView.Controls.IndexOf((Control)sender);
                polygonsymbol.LineStyle.Style = SetDashStyle(selectindex + 1);
                ShowSymbol();
            }
        }

        private void CB_size_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (HasBoundary.Checked == true)
            {
                polygonsymbol.LineStyle.Size = (float)(Convert.ToDouble(CB_size.SelectedItem));
                ShowSymbol();
            }
        }

        private void CB_size_Load()
        {
            CB_size.BeginUpdate();            //让控件一次刷新而不逐条刷新
            //添加字段
            for (int i = 1; i < 12; i++)
            {
                CB_size.Items.Add((double)(i * 0.5));
            }
            CB_size.SelectedItem = (double)polygonsymbol.LineStyle.Size;
            CB_size.EndUpdate();
            CB_size.Refresh();

        }

        private void Yes_Click(object sender, EventArgs e)
        {
            layersymbol = polygonsymbol;
            ((SymbolSelect)this.ParentForm).NewSymbol = polygonsymbol;
            this.ParentForm.DialogResult = DialogResult.OK;
            //加赋值
            this.ParentForm.Close();
        }

        private void groupbox2_Load()
        {

            ShowSymbol();
        }

        private void HasBoundary_CheckedChanged(object sender, EventArgs e)
        {
            if(HasBoundary.Checked==true)
            {
                groupBox2.Enabled = true;
                ShowSymbol();
            }
            else
            {
                groupBox2.Enabled = false;
                polygonsymbol.LineStyle.SetColor(polygonsymbol.GetColor());
            }
        }
    }
}
