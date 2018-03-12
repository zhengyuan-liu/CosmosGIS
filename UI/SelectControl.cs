using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CosmosGIS.LayoutClass;

namespace CosmosGIS.UI
{
    public partial class SelectControl : UserControl
    {
        int size = 8;

        public int GetSize { get { return size; } }

        public SelectControl()
        {
            InitializeComponent();
            this.Refresh();
        }

        public SelectControl(int width,int height)
        {
            
            InitializeComponent();
            this.Width = width;
            this.Height = height;
            this.Refresh();
        }


        

        private Point downpoint;
        private void Lable_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                downpoint = e.Location;
                ((Control)sender).BackColor = Color.Gray;               
            }
        }

        

        private void Lable_MouseMove(object sender, MouseEventArgs e)
        {
            int dx = e.X - downpoint.X;
            int dy = e.Y - downpoint.Y;
            if (e.Button == MouseButtons.Left)
            {
                if (((Control)sender) == this.LT)
                {
                    
                    this.Location =new Point(this.Location.X + dx, this.Location.Y + dy);
                    this.Width -= dx;
                    this.Height -= dy;
                }
                else if (((Control)sender) == this.RT)
                {
                    this.Location = new Point(this.Location.X, this.Location.Y + dy);
                    this.Width += dx;
                    this.Height -= dy;
                }
                else if (((Control)sender) == this.LB)
                {
                    this.Location = new Point(this.Location.X + dx, this.Location.Y);
                    this.Width -=dx;
                    this.Height += dy;
                }
                else if (((Control)sender) == this.RB)
                {
                    this.Width += dx;
                    this.Height += dy;
                }
                else if (((Control)sender) == this.TC)
                {
                    this.Location = new Point(this.Location.X, this.Location.Y + dy);
                    this.Height -= dy;
                }
                else if (((Control)sender) == this.BC)
                {
                    this.Height += dy;
                }
                else if (((Control)sender) == this.LC)
                {
                    this.Location = new Point(this.Location.X + dx, this.Location.Y );
                    this.Width -= dx;
                }
                else if (((Control)sender) == this.RC)
                {
                    this.Width += dx;
                }
                this.Refresh();
            }
        }

        private void Lable_MouseUp(object sender, MouseEventArgs e)
        {
            ((Control)sender).BackColor = SystemColors.ControlLightLight;
            this.Parent.Controls[0].Location = new Point(this.Location.X + size, this.Location.Y + size);
            if (this.Parent.Controls[0] is PictureBox)
            {
                PictureBox spicbox = (PictureBox)this.Parent.Controls[0];
                this.Parent.Controls[0].Width = this.Width - 2 * size;
                this.Parent.Controls[0].Height = this.Height - 2 * size;
                Bitmap newbit = new Bitmap(((PictureBox)this.Parent.Controls[0]).Image);
                ((PictureBox)this.Parent.Controls[0]).Image = newbit;

            }
            if (this.Parent.Controls[0] is Label)
            {

                if (((Label)this.Parent.Controls[0]).Text == "")
                {
                    double kw = (double)(this.Width - 2 * size) / (double)(Parent.Controls[0].Width);
                    double kh = (double)(this.Height - 2 * size) / (double)(Parent.Controls[0].Height);
                    double k = Math.Min(kw, kh);
                    Bitmap sbit = new Bitmap(((Label)(Parent.Controls[0])).Image, (int)(Parent.Controls[0].Width * k), (int)(Parent.Controls[0].Height * k));
                    ((Label)(Parent.Controls[0])).Image = sbit;
                    this.Parent.Controls[0].Size = sbit.Size;
                }
                else
                {
                    this.Parent.Controls[0].Width = this.Width - 2 * size;
                    this.Parent.Controls[0].Height = this.Height - 2 * size;
                }
            }

        }

        private void Lable_MouseEnter(object sender, EventArgs e)
        {
            if (((Control)sender) == this.LT)
                {
                    this.Cursor = Cursors.SizeNWSE;
                }
            else if (((Control)sender) == this.RT)
                {
                    this.Cursor = Cursors.SizeNESW;
                }
            else if (((Control)sender) == this.LB)
                {
                    this.Cursor = Cursors.SizeNESW;
                }
            else if (((Control)sender) == this.RB)
                {
                    this.Cursor = Cursors.SizeNWSE;
                }
            else if (((Control)sender) == this.TC)
                {
                    this.Cursor = Cursors.SizeNS;
                }
            else if (((Control)sender) == this.BC)
                {
                    this.Cursor = Cursors.SizeNS;
                }
            else if (((Control)sender) == this.LC)
                {
                    this.Cursor = Cursors.SizeWE;
                }
            else if (((Control)sender) == this.RC)
                {
                    this.Cursor = Cursors.SizeWE;
                }
        }


        public override void Refresh()
        {
            Rect.Location = new Point(size / 2,size/2);
            Rect.Size = new Size(this.Width - size, this.Height - size);
            LT.Location = new Point(0, 0);
            TC.Location = new Point((this.Width - size) / 2, 0);
            RT.Location = new Point((this.Width - size), 0);
            LC.Location = new Point(0, (this.Height - size) / 2);
            RC.Location = new Point(this.Width - size, (this.Height - size) / 2);
            LB.Location = new Point(0, this.Height - size);
            BC.Location = new Point((this.Width - size) / 2, this.Height - size);
            RB.Location = new Point(this.Width - size, this.Height - size);
            this.Update();
        }

        
    }
}
