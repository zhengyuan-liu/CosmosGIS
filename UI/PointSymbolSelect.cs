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

namespace CosmosGIS.UI
{
    public partial class PointSymbolSelect : UserControl
    {
        PointSymbol pointsymbol = new PointSymbol();
        int selectindex = 0;

        public PointSymbolSelect(PointSymbol symbol)
        {
            pointsymbol = symbol; //change to copy
            InitializeComponent();
            PointSymbolSelect_Load();
            pointsymbol = new PointSymbol();
        }

        /// <summary>
        /// 按照选项展示符号
        /// </summary>
        private void ShowSymbol()
        {
            if (CB_size.SelectedItem != null)
            {
                pointsymbol.SetColor(btn_color.BackColor);
                pointsymbol.Size = (float)Convert.ToDouble(CB_size.SelectedItem.ToString());
            }

            Bitmap newimg = new Bitmap(PB_show.Width, PB_show.Height);
            Graphics g = Graphics.FromImage(newimg);
            g.DrawImage(pointsymbol.GetBitmap(), new PointF((PB_show.Width - 48) / 2, (PB_show.Height - 24) / 2));
            PB_show.Image = newimg;
        }

        /// <summary>
        /// 加入图片
        /// </summary>
        private void listview_Load()
        {
            for( int i=0;i<9;i++)
            {
                PointSymbol newsymbol = new PointSymbol(i+1);
                newsymbol.SetColor(Color.Black);
                newsymbol.Size = 7;
                Label newlabel = new Label();                
                Bitmap bit = newsymbol.GetBitmap();
                newlabel.Image = bit;
                newlabel.Size = new Size(60, 60);
                newlabel.Location = new Point(10+80* (int)(i / 3), 20+80 * (int)(i % 3));
                SymbolListView.Controls.Add(newlabel);
            }
        }

        /// <summary>
        /// 选择项发生变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            pointsymbol.StyleIndex = SymbolListView.SelectedIndices[0];
            ShowSymbol();
        }

        /// <summary>
        /// 根据符号进行初始化设置
        /// </summary>
        private void PointSymbolSelect_Load()
        {
            btn_color.BackColor = pointsymbol.GetColor();
            ShowSymbol();
            listview_Load();
            CB_size_Load();
            foreach (Control c in this.SymbolListView.Controls)
            {
                c.MouseClick += C_DoubleClick;
            }
        }

        private void C_DoubleClick(object sender, EventArgs e)
        {
            ((Label)SymbolListView.Controls[selectindex]).BorderStyle = BorderStyle.None;
            ((Label)sender).BorderStyle = BorderStyle.FixedSingle;
            selectindex = SymbolListView.Controls.IndexOf((Control)sender);
            pointsymbol.StyleIndex =  selectindex+ 1;
            ShowSymbol();
        }

        private void Yes_Click(object sender, EventArgs e)
        {
            this.ParentForm.DialogResult = DialogResult.OK;
            ((SymbolSelect)this.ParentForm).NewSymbol = pointsymbol;
            //加赋值
            this.ParentForm.Close();
            //this.ParentForm.Dispose();
        }

        private void btn_color_Click(object sender, EventArgs e)
        {
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                btn_color.BackColor = colorDialog1.Color;
                pointsymbol.SetColor(colorDialog1.Color);
                ShowSymbol();

            }
        }

        private void CB_size_Load()
        {
            CB_size.BeginUpdate();            //让控件一次刷新而不逐条刷新
            //添加字段
            for(int i=3;i<15;i++)
            {
                CB_size.Items.Add(i);
            }
            CB_size.SelectedItem = (int)pointsymbol.Size;
            CB_size.EndUpdate();
            CB_size.Refresh();
            
        }

        private void CB_size_SelectedIndexChanged(object sender, EventArgs e)
        {
            pointsymbol.Size =(float)(Convert.ToDouble( CB_size.SelectedItem));
            ShowSymbol();

        }

        private void PointSymbolSelect_Load(object sender, EventArgs e)
        {

        }
    }
}
