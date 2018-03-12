using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace CosmosGIS.LayoutClass
{
    class ScaleSymbol
    {
        private Point lefttop;
        private double width;
        private double height;
        private string path;
        private double scalevalue;
        private double scalebarlenghth;

        public double Scale { get { return scalevalue; } }
        public double ScaleBarLenghth { get { return scalebarlenghth; } }
        public Point LeftTop { get { return lefttop; } set { lefttop = value;} }//TODO
        public double Width { get { return width; } set { width = value; } }//TODO
        public double Height { get { return height; } set { width = value; } }
        public PictureBox Box = new PictureBox();
        //TODO
        public ScaleSymbol(double scale,Point location)
        {
            Bitmap symbol =new Bitmap(100,100);
            scalevalue = scale;
            Graphics g = Graphics.FromImage(symbol);
            //要加单位，现在不确定
            string line = (scale * 20).ToString();
            Pen newpen = new Pen(Color.Black,2);
            scalebarlenghth = 20;
            g.DrawString(line, new Font("宋体", 8), Brushes.Black, new PointF(0, 60));
            Point[] points = new Point[4] { new Point(40, 40), new Point(40,50), new Point(60,50), new Point(60,40) };
            g.DrawLines(newpen, points);
           
            Box.AutoSize = false;
            Box.Size = symbol.Size;
            Box.Image = symbol;
            Box.Location = location;
            Box.Name = "ScaleSymbol";
            
         }

        /// <summary>
        /// 根据已有标签重设大小
        /// </summary>
        /// <param name="from"></param>
        /// <param name="location"></param>
        /// <param name="resizescale"></param>
        public ScaleSymbol(ScaleSymbol from,double scale,Point location,double resizescale)
        {
            Bitmap symbol = new Bitmap((int)(from.Box.Width*resizescale),(int)(from.Box.Height*resizescale));
            scalevalue =scale;
            Graphics g = Graphics.FromImage(symbol);
            //要加单位，现在不确定
            string line = (scalevalue* 20).ToString();
            Pen newpen = new Pen(Color.Black, 2);
            scalebarlenghth = from.ScaleBarLenghth;
            g.DrawString(line, new Font("宋体", (int)(12 * symbol.Width / 100)), Brushes.Black, new PointF((int)(symbol.Width * 0), (int)(symbol.Height * 0.6)));

            Point[] points = new Point[4] { new Point((int)(symbol.Width*0.4),(int)(symbol.Height*0.4)), new Point((int)(symbol.Width * 0.4), (int)(symbol.Height * 0.5)), new Point((int)(symbol.Width * 0.6), (int)(symbol.Height * 0.5)), new Point((int)(symbol.Width*0.6),(int)(symbol.Height*0.4)) };
            g.DrawLines(newpen, points);
            Box.AutoSize = false;
            Box.Size = symbol.Size;
            Box.Image = symbol;
            Box.Location = location;
            Box.Name = "ScaleSymbol";
        }

        public void ResetSize(Point newlocation, Size newsize)
        {
            this.Box.Location = newlocation;
            Bitmap newbit = new Bitmap(this.Box.Image, newsize.Width, newsize.Height);
            this.Box.Image = newbit;
            this.Box.Size = newsize;
        }

    }
}
