using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace CosmosGIS.LayoutClass
{
    class CompassSymbol
    {
        private Point lefttop;
        private double width;
        private double height;
        private string path;

        public Point LeftTop { get { return lefttop; } set { lefttop = value;} }//TODO
        public double Width { get { return width; } set { width = value; } }//TODO
        public double Height { get { return height; } set { width = value; } }
        public Label Box = new Label();
        //public PictureBox Box = new PictureBox();
        //TODO
        public CompassSymbol()
        {            
            Image newimage = Image.FromFile(@"D:\Cosmos v4\Cosmos\Resources\compass.jpg");
            Bitmap newbitmap = new Bitmap(newimage, 100, 100);
            //PictureBox newbox = new PictureBox();
            Box.AutoSize = false;
            Box.Size = newbitmap.Size;
            Box.Image = newbitmap;
            Box.Left = 300;
            Box.Top = 100;
            Box.BackColor = Color.Transparent;
            Box.Name = "CompassSymbol";
         }
        /// <summary>
        /// 根据已有情况重绘
        /// </summary>
        /// <param name="from"></param>
        /// <param name="location"></param>
        /// <param name="resizescale"></param>
        public CompassSymbol(CompassSymbol from,Point location, double resizescale)
        {
            Image newimage = Image.FromFile(@"D:\Cosmos v4\Cosmos\Resources\compass.jpg");
            Bitmap newbitmap = new Bitmap(newimage, (int)(from.Box.Width*resizescale),(int)(from.Box.Height*resizescale));
            //PictureBox newbox = new PictureBox();
            Box.AutoSize = false;
            Box.Size = newbitmap.Size;
            Box.Image = newbitmap;
            Box.Location = location;
            Box.BackColor = Color.Transparent;
            Box.Name = "CompassSymbol";
        }

        public void ResetSize(Point newlocation,Size newsize)
        {
            this.Box.Location = newlocation;
            Bitmap newbit = new Bitmap(this.Box.Image, newsize.Width, newsize.Height);
            this.Box.Image = newbit;
            this.Box.Size = newsize;
        }
    }
}
