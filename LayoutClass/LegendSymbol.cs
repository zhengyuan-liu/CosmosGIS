using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using CosmosGIS.MyRenderer;

namespace CosmosGIS.LayoutClass
{
    /// <summary>
    /// 
    /// </summary>
    class LegendSymbol
    {
        #region 字段
        private Point lefttop;
        private double width;
        private double height;
        #endregion

        #region 属性
        public Point LeftTop { get { return lefttop; } set { lefttop = value; } }//TODO
        public double Width { get { return width; } set { width = value; } }//TODO
        public double Height { get { return height; } set { width = value; } }
        public Label Box = new Label();
        internal double ResizeScale { get; set; }
        #endregion

        public LegendSymbol(Map.MyMap map,Point location)
        {
            Box.Name = "LegendSymbol";
            SetLegend(map, 0.8);
            ResizeScale = 1;
            Box.Location = location;
        }

        public LegendSymbol(Map.MyMap map, double scale,Point location)
        {
            SetLegend(map, scale);
            Box.Name = "LegendSymbol";
            ResizeScale = scale;
            Box.Location = location;
        }

        /// <summary>
        /// 根据已有控件重绘
        /// </summary>
        /// <param name="map"></param>
        /// <param name=""></param>
        public LegendSymbol(Map.MyMap map, LegendSymbol from, Point location, double resizescale2formal)
        {
            ResizeScale = resizescale2formal;
            SetLegend(map, ResizeScale);
            Box.Name = "LegendSymbol";
            Box.Location = location;
        }

        /// <summary>
        /// 绘制图例
        /// </summary>
        /// <param name="map"></param>
        /// <param name="scale"></param>
        private void SetLegend(Map.MyMap map, double resizescale)
        {
            int layercount = map.LayerNum;
            int dis = (int)(resizescale * 10);
            int layerdis = (int)(resizescale * 20);
            int symbolcount = 0;
            int visiblelayercount = 0;
            List<int> visiblelayers = new List<int>();
            Box.BackColor = Color.Transparent;

            for (int i = 0; i < layercount; i++)
            {
                if (map.Layers[i].Visible == true)
                {
                    visiblelayercount++;
                    symbolcount += map.Layers[i].Renderer.SymbolCount;
                    visiblelayers.Add(i);
                }
            }
            Bitmap newbitmap = new Bitmap((int)(resizescale * 150), (int)(resizescale * (symbolcount * dis + (visiblelayercount + 2) * layerdis + (symbolcount + visiblelayercount + 1) * 24)));
            Graphics image = Graphics.FromImage(newbitmap);
            Point start = new Point(0, 0);
            image.DrawString("图例", new Font("黑体", (int)(resizescale * 15)), Brushes.Black, start);
            start.Y = (layerdis + (int)(resizescale * 15));
            for (int i = 0; i < visiblelayercount; i++)
            {
                start = LegendOfLayer(image, map.Layers[visiblelayers[i]], start, resizescale);
            }

            Box.AutoSize = false;
            Box.Size = new Size((int)(newbitmap.Width * resizescale), (int)(newbitmap.Height * resizescale));
            // Bitmap sbit = new Bitmap(newbitmap, new Size((int)(newbitmap.Width * scale), (int)(newbitmap.Height * scale)));
            newbitmap.MakeTransparent(Color.White);
            Box.Image = newbitmap;
            Box.Left = 100;
            Box.Top = 30;
        }

        /// <summary>
        /// 画单个图层方法
        /// </summary>
        /// <param name="g"></param>
        /// <param name="layer"></param>
        /// <param name="start"></param>
        /// <param name="scale"></param>
        /// <returns></returns>
        private Point LegendOfLayer(Graphics g, Map.MyLayer layer, Point start, double resizescale)
        {
            int layerdis = (int)(20 * resizescale);
            int leftdis = (int)(resizescale * 8);
            int columdis = (int)(resizescale * 12);
            int rowdis = (int)(resizescale * 10);
            Point nowstart = new Point(start.X + leftdis, start.Y + layerdis);
            g.DrawString(layer.LayerName, new Font("黑体", (float)(resizescale * 10)), Brushes.Black, nowstart);
            nowstart.Y += ((int)(resizescale * 10) + rowdis);
            Font regularfont = new Font("宋体", (float)(resizescale * 9));
            int symbolcount = layer.Renderer.SymbolCount;
            switch (layer.rendertype)
            {
                case MyRenderer.RendererType.UniqueValueRenderer:
                    UniqueValueRenderer urenderer = (UniqueValueRenderer)layer.Renderer;
                    for (int i = 0; i < symbolcount; i++)
                    {
                        Bitmap autoubit = urenderer.FindSymbolByIndex(i).GetBitmap();
                        Image uimg = new Bitmap(autoubit, new Size((int)(resizescale * autoubit.Width), (int)(resizescale * autoubit.Height)));
                        g.DrawImage(uimg, nowstart);
                        g.DrawString(urenderer.FindValue(i), regularfont, Brushes.Black, new Point(nowstart.X + (int)(resizescale * 42) + columdis, nowstart.Y + (int)(resizescale * 6)));
                        nowstart.Y += (int)(resizescale * 24) + rowdis;
                    }
                    break;
                case MyRenderer.RendererType.ClassBreakRenderer:
                    ClassBreakRenderer crenderer = (ClassBreakRenderer)layer.Renderer;
                    for (int i = 0; i < symbolcount; i++)
                    {
                        Bitmap autocbit = crenderer.FindSymbolByIndex(i).GetBitmap();
                        Image cimg = new Bitmap(autocbit, new Size((int)(resizescale * autocbit.Width), (int)(resizescale * autocbit.Height)));
                        g.DrawImage(cimg, nowstart);

                        string value;
                        if (i == 0)
                            value = crenderer.MinValue.ToString() + " - " + crenderer.Breaks[i].ToString();
                        else if (i != crenderer.BreakCount)
                            value = crenderer.Breaks[i - 1].ToString() + " - " + crenderer.Breaks[i].ToString();
                        else
                            value = crenderer.Breaks[i - 1].ToString() + " - " + crenderer.MaxValue.ToString();

                        g.DrawString(value, regularfont, Brushes.Black, new Point((nowstart.X + (int)(resizescale * 42) + columdis), (nowstart.Y + (int)(resizescale * 6))));

                        nowstart.Y += (int)(resizescale * 24) + rowdis;
                    }
                    break;
                default:
                    SimpleRenderer srenderer = (SimpleRenderer)layer.Renderer;
                    Bitmap autosbit = srenderer.SymbolStyle.GetBitmap();
                    Image simg = new Bitmap(autosbit, new Size((int)(resizescale * autosbit.Width), (int)(resizescale * autosbit.Height)));
                    g.DrawImage(simg, nowstart);

                    g.DrawString(layer.LayerName, regularfont, Brushes.Black, new Point((nowstart.X + (int)(resizescale * 42) + columdis), (nowstart.Y + (int)(resizescale * 6))));
                    nowstart.Y += ((int)(resizescale * 24) + rowdis);
                    break;
            }
            return nowstart;
        }

        /*
        /// <summary>
        /// 绘制图例
        /// </summary>
        /// <param name="map"></param>
        /// <param name="scale"></param>
        private void SetLegend(Map.MyMap map,double scale)
        {
            int layercount = map.LayerNum;
            int dis = 10 ;
            int layerdis = 20;
            int symbolcount = 0;
            int visiblelayercount = 0;
            List<int> visiblelayers = new List<int>();
            Box.BackColor = Color.Transparent;

            for (int i = 0; i < layercount; i++)
            {
                if (map.Layers[i].Visible == true)
                {
                    visiblelayercount++;
                    symbolcount += map.Layers[i].Renderer.SymbolCount;
                    visiblelayers.Add(i);
                }
            }
            Bitmap newbitmap = new Bitmap(150,(symbolcount * dis + (visiblelayercount + 2) * layerdis + (symbolcount + visiblelayercount + 1) * 24));
            Graphics image = Graphics.FromImage(newbitmap);
            Point start = new Point(0, 0);
            image.DrawString("图例", new Font("黑体",15), Brushes.Black, start);
            start.Y = (layerdis + 15);
            for (int i = 0; i < visiblelayercount; i++)
            {
                start = LegendOfLayer(image, map.Layers[visiblelayers[i]], start,scale);
            }

            Box.AutoSize = false;
            Box.Size = new Size((int)(newbitmap.Width * scale), (int)(newbitmap.Height * scale));
            Bitmap sbit = new Bitmap(newbitmap, new Size((int)(newbitmap.Width * scale), (int)(newbitmap.Height * scale)));
            sbit.MakeTransparent(Color.White);
            Box.Image = sbit;
            Box.Left = 100;
            Box.Top = 30;
        }

        /// <summary>
        /// 画单个图层方法
        /// </summary>
        /// <param name="g"></param>
        /// <param name="layer"></param>
        /// <param name="start"></param>
        /// <param name="scale"></param>
        /// <returns></returns>
        private Point LegendOfLayer(Graphics g, Map.MyLayer layer,Point start,double scale)
        {
            int layerdis = 20;
            int leftdis = 8;
            int columdis = 12;
            int rowdis = 10;
            Point nowstart = new Point(start.X + leftdis, start.Y + layerdis);
            g.DrawString(layer.LayerName, new Font("黑体",10), Brushes.Black, nowstart);
            nowstart.Y +=(10+ rowdis);
            Font regularfont = new Font("宋体", 9);
            int symbolcount = layer.Renderer.SymbolCount;
            switch (layer.rendertype)
            {
                case MyRenderer.RendererType.UniqueValueRenderer:
                    UniqueValueRenderer urenderer = (UniqueValueRenderer)layer.Renderer;
                    for (int i = 0; i < symbolcount; i++)
                    {
                        Bitmap autoubit = urenderer.FindSymbolByIndex(i).GetBitmap();
                        Image uimg = new Bitmap( autoubit,new Size(autoubit.Width,autoubit.Height));
                        g.DrawImage(uimg, nowstart);                        
                        g.DrawString(urenderer.FindValue(i), regularfont, Brushes.Black, new Point(nowstart.X + 42 + columdis, nowstart.Y+6 ));
                        nowstart.Y += 24 + rowdis;
                    }
                    break;
                case MyRenderer.RendererType.ClassBreakRenderer:
                    ClassBreakRenderer crenderer = (ClassBreakRenderer)layer.Renderer;
                    for (int i = 0; i < symbolcount; i++)
                    {
                        Bitmap autocbit = crenderer.FindSymbolByIndex(i).GetBitmap();
                        Image cimg = new Bitmap(autocbit, new Size(autocbit.Width , autocbit.Height));
                        g.DrawImage(cimg, nowstart);                        

                        string value;
                        if (i == 0)
                            value = crenderer.MinValue.ToString() + " - " + crenderer.Breaks[i].ToString();
                        else if (i != crenderer.BreakCount)
                            value = crenderer.Breaks[i - 1].ToString() + " - " + crenderer.Breaks[i].ToString();
                        else
                            value= crenderer.Breaks[i - 1].ToString() + " - " + crenderer.MaxValue.ToString();

                        g.DrawString(value, regularfont, Brushes.Black, new Point((nowstart.X + 42 + columdis), (nowstart.Y+6)));

                        nowstart.Y += 24 + rowdis;
                    }
                    break;
                default:
                    SimpleRenderer srenderer = (SimpleRenderer)layer.Renderer;
                    Bitmap autosbit = srenderer.SymbolStyle.GetBitmap();
                    Image simg = new Bitmap(autosbit, new Size(autosbit.Width , autosbit.Height));
                    g.DrawImage(simg, nowstart);
                    
                    g.DrawString(layer.LayerName, regularfont, Brushes.Black, new Point((nowstart.X + 42 + columdis), (nowstart.Y + 6)));
                    nowstart.Y += (24 + rowdis);
                    break;
            }
            return nowstart;
        }
    }
    */
    }
}
