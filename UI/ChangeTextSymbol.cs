using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Text;

namespace CosmosGIS.UI
{
    public partial class ChangeTextSymbol : Form
    {
        public ChangeTextSymbol(string text)
        {
            InitializeComponent();
            InitializeLable();
            TextInitial(text);
        }

        Font newfont;
        Color newcolor;
        string text;
     
        public Font NewFont { get { return newfont; } }
        public Color NewColor { get { return newcolor; } }
        public string NewText { get { return text; } }


        private void TextInitial(string text)
        {
            richTextBox1.Text = text;
            CB_FontStyle.SelectedItem = "宋体";
            CB_FontSize.SelectedItem = 12;
            btn_SelectColor.BackColor = Color.Black;
        }
      
        private void btn_SelectColor_Click(object sender, System.EventArgs e)
        {
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                btn_SelectColor.BackColor = colorDialog1.Color;
                TextShow.ForeColor = colorDialog1.Color;
                newcolor = TextShow.ForeColor;
            }
        }
        
        private void btn_B_Click(object sender, System.EventArgs e)
        {
            if (btn_B.BackColor == Color.White)
            {
                btn_B.BackColor = Color.Gray;
                if (TextShow.Font.Italic == true)
                    TextShow.Font = new Font(TextShow.Font, FontStyle.Bold | FontStyle.Italic);
                else
                    TextShow.Font = new Font(TextShow.Font, FontStyle.Bold);
            }
            else
            {
                btn_B.BackColor = Color.White;
                if (TextShow.Font.Italic == true)
                    TextShow.Font = new Font(TextShow.Font, FontStyle.Italic);
                else
                    TextShow.Font = new Font(TextShow.Font, FontStyle.Regular);

            }
            newfont = TextShow.Font;
        }
        
        private void btn_I_Click(object sender, System.EventArgs e)
        {
            if (btn_I.BackColor == Color.White)
            {
                btn_I.BackColor = Color.Gray;
                if (TextShow.Font.Bold == true)
                    TextShow.Font = new Font(TextShow.Font, FontStyle.Italic | FontStyle.Bold);
                else
                    TextShow.Font = new Font(TextShow.Font, FontStyle.Italic);
            }
            else
            {
                btn_I.BackColor = Color.White;
                if (TextShow.Font.Bold == true)
                    TextShow.Font = new Font(TextShow.Font, FontStyle.Bold);
                else
                    TextShow.Font = new Font(TextShow.Font, FontStyle.Regular);
            }
            newfont = TextShow.Font;
        }

        private void CB_FontStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CB_FontSize.SelectedItem != null)
            {
                TextShow.Font = new Font(CB_FontStyle.SelectedItem.ToString(), (float)Convert.ToDouble(CB_FontSize.SelectedItem.ToString()), TextShow.Font.Style);
                newfont = TextShow.Font;
            }
        }

        private void CB_FontSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CB_FontStyle.SelectedItem != null)
            {
                TextShow.Font = new Font(CB_FontStyle.SelectedItem.ToString(), (float)Convert.ToDouble(CB_FontSize.SelectedItem.ToString()), TextShow.Font.Style);
                newfont = TextShow.Font;
            }
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            text = richTextBox1.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
            this.Dispose();
        }


        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }


        //显示字段列表
        private void InitializeLable()
        {

            for (int i = 5; i < 64; i++)
            {
                CB_FontSize.Items.Add(i);
            }

            InstalledFontCollection MyFont = new InstalledFontCollection();
            FontFamily[] MyFontFamilies = MyFont.Families;
            int Count = MyFontFamilies.Length;
            for (int i = 0; i < Count; i++)
            {
                CB_FontStyle.Items.Add(MyFontFamilies[i].Name);
            }
        }


    }
}
