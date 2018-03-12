using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace CosmosGIS.MySymbol
{
    class EnumColor
    {
        List<Color> systemcolor = new List<Color>();

        public EnumColor()
        {
            string[] colors = (string[])Enum.GetNames(typeof(KnownColor));
            for (int i = 0; i < colors.Length; i++)
                systemcolor.Add(Color.FromName(colors[i]));
        }

        public Color GetSystemcolor(int index)
        {
            if(index<systemcolor.Count)
            {
                return systemcolor[index];
            }
            else
            {
                return GetSystemcolor((int)(index / 2));
            }
        }

        public Bitmap GetLinearGradient(int start,int end )
        {
            Color startcolor = GetSystemcolor(start);
            Color endcolor = GetSystemcolor(end);
            Rectangle Rec = new Rectangle(new Point(0, 0), new Size(200, 20));
            LinearGradientBrush LGB = new LinearGradientBrush(Rec, startcolor, endcolor, LinearGradientMode.Horizontal);
            Bitmap LinearGradient = new Bitmap(200, 20);
            Graphics g = Graphics.FromImage(LinearGradient);
            g.FillRectangle(LGB, Rec);
            LGB.Dispose();
            return LinearGradient;
        }
    }
}
