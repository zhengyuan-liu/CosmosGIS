using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CosmosGIS.UI
{
    public partial class LinerColorSelect : Form
    {
        public LinerColorSelect(Bitmap bit)
        {
            InitializeComponent();
            listview_Load(bit);
        }

        int selectindex=0;
        public Bitmap result;

        private void button1_Click(object sender, EventArgs e)
        {
            result = new Bitmap(((Label)listView1.Controls[selectindex]).Image);
            this.DialogResult = DialogResult.OK;
        }


        /// <summary>
        /// 加入图片
        /// </summary>
        private void listview_Load(Bitmap bit)
        {
            listView1.Controls.Clear();//清除
            Label newlabel = new Label();
            newlabel.Image = bit;
            newlabel.Size = new Size(250, 20);
            newlabel.BorderStyle = BorderStyle.FixedSingle;
            newlabel.Location = new Point(10, 10);
            listView1.Controls.Add(newlabel);
            Random ran = new Random();
            MySymbol.EnumColor newenumcolor = new MySymbol.EnumColor();
            for (int i = 1; i < 16; i++)
            {
                newlabel = new Label();
                Bitmap newbit = newenumcolor.GetLinearGradient(ran.Next(), ran.Next());
                newlabel.Image = newbit;
                newlabel.Size = new Size(250,20);
                newlabel.Location = new Point(10, 10 + (i * 30));
                listView1.Controls.Add(newlabel);                
            }
            foreach (Control c in this.listView1.Controls)
            {
                c.MouseClick += C_DoubleClick;
            }
        }

        private void C_DoubleClick(object sender, EventArgs e)
        {
            ((Label)listView1.Controls[selectindex]).BorderStyle = BorderStyle.None;
            ((Label)sender).BorderStyle = BorderStyle.FixedSingle;
            selectindex = listView1.Controls.IndexOf((Control)sender);
         }
    }
}
