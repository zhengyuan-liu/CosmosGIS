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
     partial class Layout : UserControl
    {

        #region 字段
        /// <summary>
        /// 记录鼠标按下位置
        /// </summary>
        private Point downpoint;   
        
        //选择框     
        SelectControl selectbox;

        /// <summary>
        /// 是否处于选择状态
        /// </summary>
        bool selectmode = false;
        /// <summary>
        /// A4纸放大缩小的中心点位
        /// </summary>
        Point center_Resize;
        /// <summary>
        /// 图片框
        /// </summary>
        PictureBox PB_background;
        /// <summary>
        /// 装载地图的框体
        /// </summary>
        PictureBox mapimg;
        /// <summary>
        /// A4纸尺寸
        /// </summary>
        Size basesize = new Size(210, 297);
        /// <summary>
        /// 常规pictruebox尺寸,基础尺寸2倍
        /// </summary>
        Size size_boxregular = new Size(420, 594);
        /// <summary>
        /// 放大缩小比例尺,采用常规尺寸时为1
        /// </summary>
        double boxscale=1;
        /// <summary>
        /// 两个界面的比例传输
        /// </summary>
        double ratio2screen;
        /// <summary>
        /// 与真实世界比例尺
        /// </summary>
        double real2map;
        /// <summary>
        /// 编辑界面图片
        /// </summary>
        Bitmap mapbitmap;       
        /// <summary>
        /// 引用map的信息
        /// </summary>
        Map.MyMap thismap;

        /// <summary>
        /// 图廓
        /// </summary>
        Label NeatLine;

        #endregion


        #region 构造函数

        public Layout(Map.MyMap map,double datascale, Bitmap mapbit,Size size,Point location)
        {
            boxscale = 1;
            mapbitmap = mapbit;
            InitializeComponent();
            thismap = map;
            this.Width = size.Width;
            this.Height = size.Height;
            this.Location = location;
            InitializePB_background();
            UpdateMapBackground();
            real2map = datascale * ratio2screen;    
            Update();
            
        }

        #endregion

        #region 初始化设置
        /// <summary>
        /// 以固定的比例初始化A4纸图片框
        /// </summary>
        private void InitializePB_background()
        {
            PB_background = new PictureBox();
            PB_background.Name = "SPECIAL";
            PB_background.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            PB_background.Size = size_boxregular;
            PB_background.Location = new Point((this.Width - PB_background.Width) / 2, (this.Height - PB_background.Height) / 2);
            PB_background.MouseUp += new MouseEventHandler(this.Background_MouseUp);
            PB_background.PreviewKeyDown += PB_background_PreviewKeyDown;
            this.Controls.Add(PB_background);
        }
        #endregion

        #region  刷新事件

        /// <summary>
        /// 刷新A4纸框内的图像
        /// </summary>
        private void UpdateMapBackground()
        {
            //初始化bitmap
            double kw = (double)mapbitmap.Width / (double)(PB_background.Width*0.8);
            double kh = (double)mapbitmap.Height / (double)(PB_background.Height*0.8);
            ratio2screen = Math.Max(kw, kh);    //即地图在两个视图中的切换缩小比例
            Bitmap sbit = new Bitmap(mapbitmap, (int)(mapbitmap.Width / ratio2screen), (int)(mapbitmap.Height / ratio2screen));

            //传入地图
            if (mapimg == null)
            {
                mapimg = new PictureBox();
                mapimg.BackColor = Color.Transparent;
                mapimg.SizeMode = PictureBoxSizeMode.CenterImage;
                this.Add(mapimg);
            }

            sbit.MakeTransparent(Color.White);
            mapimg.Size = sbit.Size;
            mapimg.Image = sbit;
            mapimg.Name = "Map";
            mapimg.Location = new Point((PB_background.Width - mapimg.Width) / 2, (PB_background.Height - mapimg.Height) / 2);
        }

        
        #endregion

        #region 控件事件

        private void Control_MouseUp(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        private void Control_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                selectmode = true;
                downpoint = e.Location;
                int size = 8;//获得边框矩形大小
                Point selectLocation = new Point(((Control)sender).Location.X - size, ((Control)sender).Location.Y - size);
                this.PB_background.Controls.SetChildIndex((Control)sender, 0);
                if (selectbox == null)
                {
                    selectbox = new SelectControl(((Control)sender).Width + 2 * size, ((Control)sender).Height + 2 * size);
                    selectbox.Location = selectLocation;
                    this.PB_background.Controls.Add(selectbox);
                }
                else
                {                    
                    if (selectbox.Location != selectLocation)
                    {
                        this.PB_background.Controls.Remove(selectbox);
                        selectbox.Dispose();
                        selectbox = new SelectControl(((Control)sender).Width + 2 * size, ((Control)sender).Height + 2 * size);
                        selectbox.Location = selectLocation;
                        this.PB_background.Controls.Add(selectbox);
                        this.PB_background.Controls.SetChildIndex(selectbox, 1);
                    }
                }
                this.PB_background.Refresh();
            }
            
        }

        private void Control_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Cursor = Cursors.SizeAll;
                ((Control)sender).Location = new Point(((Control)sender).Location.X + e.X - downpoint.X, ((Control)sender).Location.Y + e.Y - downpoint.Y);
                selectbox.Location = new Point(selectbox.Location.X + e.X - downpoint.X, selectbox.Location.Y + e.Y - downpoint.Y);
            }
            PB_background.Refresh();
        }

        private void Control_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if ((Control)sender is Label)
            {
                if (((Label)sender).Text != "")
                {
                    Label lb= (Label)sender;
                    ChangeTextSymbol change = new ChangeTextSymbol(((Label)sender).Text);
                    change.ShowDialog();
                   if (change.DialogResult==DialogResult.OK)
                    {
                        lb.Text = change.NewText;
                        lb.ForeColor = change.NewColor;
                        lb.Font = change.NewFont;
                        lb.AutoSize = true;
                        selectbox.Size = new Size(lb.Width + 16, lb. Height + 16);
                       
                        selectbox.Refresh();
                    }
                }
            }
        }
        #endregion

        #region pictruebox事件

        /// <summary>
        /// 删除事件TODO
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PB_background_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (Keys.Delete == e.KeyCode && selectmode == true)
            {
                PB_background.Controls.RemoveAt(0);
            }
        }

        /// <summary>
        /// 鼠标弹起事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Background_MouseUp(object sender, MouseEventArgs e)
        {
            if (selectmode == true)
            {

                if (selectbox != null)
                {
                    PB_background .Controls.Remove(selectbox);
                    selectbox.Dispose();
                    selectbox = null;
                }
                selectmode = false;
            }
        }
        private void PB_background_KeyUp(object sender, KeyEventArgs e)
        {
            if (Keys.Delete == e.KeyCode && selectmode == true)
            {
                PB_background.Controls.RemoveAt(0);
            }
        }

        private void Layout_KeyUp(object sender, KeyEventArgs e)
        {
            if (Keys.Delete == e.KeyCode && selectmode == true && PB_background.Focused == true)
            {
                PB_background.Controls.RemoveAt(0);
            }
        }
        /// <summary>
        /// 实现地图的缩放，鼠标滑轮滚动响应的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picBoxMap_MouseWheel(object sender, MouseEventArgs e)
        {
            /*
            if (e.Delta > 0 && scaleIndex < scaleChoice.Length - 1)
                scaleIndex++;
            else if (e.Delta < 0 && scaleIndex > 0)
                scaleIndex--;
            UpdateMapImg();
            
            */
            //??
        }

        /// <summary>
        /// 纸张方向选择横向
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 横向ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (PB_background.Width < PB_background.Height)
            {
                PB_background.Size = new Size(PB_background.Height, PB_background.Width);
                PB_background.Location = new Point((this.Width - PB_background.Width) / 2, (this.Height - PB_background.Height) / 2);
            }
            if (NeatLine != null)
                SetNeatline();
        }
        /// <summary>
        /// 纸张方向选择纵向
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 纵向ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (PB_background.Width > PB_background.Height)
            {
                PB_background.Size = new Size(PB_background.Height, PB_background.Width);
                PB_background.Location = new Point((this.Width - PB_background.Width) / 2, (this.Height - PB_background.Height) / 2);
            }
            if (NeatLine != null)
                SetNeatline();
        }
        /// <summary>
        /// 缩小A4纸
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 缩小ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (selectbox != null)
            {
                PB_background.Controls.Remove(selectbox);
                selectbox.Dispose();
                selectbox = null;
            }
            selectmode = false;
            boxscale = boxscale * 0.8;
            ResizeAllControl();
            //todo
        }
        /// <summary>
        /// 放大A4纸
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 放大ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (selectbox != null)
            {
                PB_background.Controls.Remove(selectbox);
                selectbox.Dispose();
                selectbox = null;
            }
            selectmode = false;
            boxscale = boxscale * 1.2;
            ResizeAllControl();
            //todo
        }
        #endregion

        #region 内部函数
        /// <summary>
        /// 设置图廓
        /// </summary>
        private void SetNeatline()
        {
            if (NeatLine == null)
            {
                NeatLine = new Label();
                NeatLine.Name = "NeatLine";
                NeatLine.Size = new Size((int)(PB_background.Width - 6 * boxscale), (int)(PB_background.Height - 6 * boxscale));
                NeatLine.Location = new Point((int)(3 * boxscale), (int)(3 * boxscale));
                NeatLine.BorderStyle = BorderStyle.FixedSingle;
                PB_background.Controls.Add(NeatLine);
            }
            else
            {
                NeatLine.Size = new Size((int)(PB_background.Width - 6 * boxscale), (int)(PB_background.Height - 6 * boxscale));
                NeatLine.Location = new Point((int)(3 * boxscale), (int)(3 * boxscale));
                NeatLine.BorderStyle = BorderStyle.FixedSingle;          
            }
        }


        /// <summary>
        /// 将内部转成图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private Bitmap flowLayoutPanel1_Paint()
        {
            //Rectangle newrectangle = new Rectangle(new Point(100, 100), new Size(300, 400));
            Bitmap newmap = new Bitmap(PB_background.Width, PB_background.Height);
            // Graphics g = this.CreateGraphics();
            Graphics g = Graphics.FromImage(newmap);
            g.FillRectangle(Brushes.White, 0, 0, newmap.Width, newmap.Height);
            foreach(Control s in this.PB_background.Controls)
            {
                if(s.Name== "TextboxSymbol")
                {
                    Label label = (Label)s;
                    string text = label.Text;
                    Font newfont = label.Font;
                    Brush newbrush = new SolidBrush(label.ForeColor);
                    g.DrawString(text, newfont, newbrush, s.Location);
                }
                else if(s is PictureBox)
                {
                    Point LTdis = new Point(((PictureBox)s).Width - ((PictureBox)s).Image.Width, ((PictureBox)s).Height - ((PictureBox)s).Image.Height);
                    g.DrawImage(((PictureBox)s).Image, new PointF(s.Location.X - (LTdis.X / 2), s.Location.Y - (LTdis.Y / 2)));
                }
                else if (s is Label)
                {
                    Point LTdis = new Point(((Label)s).Width - ((Label)s).Image.Width, ((Label)s).Height - ((Label)s).Image.Height);
                    g.DrawImage(((Label)s).Image, new PointF(s.Location.X - (LTdis.X / 2), s.Location.Y - (LTdis.Y / 2)));
                }
            }
            return newmap;
        }

        /// <summary>
        /// 放大缩小事件重绘事件TODO
        /// </summary>
        private void ResizeAllControl()
        {
            double resizescale;
            if (PB_background.Width < PB_background.Height)
                resizescale = (double)((size_boxregular.Width * boxscale) / PB_background.Size.Width);
            else
                resizescale = (double)((size_boxregular.Height * boxscale) / PB_background.Size.Width);
            PB_background.Size = new Size((int)(PB_background.Width * resizescale), (int)(PB_background.Height * resizescale));
            PB_background.Location = new Point((this.Width - PB_background.Width) / 2, (this.Height - PB_background.Height) / 2);

            foreach (Control c in this.PB_background.Controls)
            {
                if(c.Name=="Map")
                {
                    c.Location = ComputeResizeLocation(c.Location,resizescale);
                    Bitmap newmap = new Bitmap(mapbitmap, new Size((int)(mapbitmap.Width * boxscale / ratio2screen), (int)(mapbitmap.Height * boxscale / ratio2screen)));
                    c.Size = newmap.Size;
                    ((PictureBox)c).Image = newmap;
                }
                if(c.Name== "TextboxSymbol")
                {
                    TextboxSymbol from = new TextboxSymbol();
                    from.textbox = (Label)c;
                    Point newlocation = ComputeResizeLocation(c.Location,resizescale);
                    TextboxSymbol newtextsymbol = new TextboxSymbol(from, newlocation, resizescale);
                    PB_background.Controls.Remove(c);
                    this.Add(newtextsymbol.textbox);
                }

                if (c.Name == "ScaleSymbol")
                {
                    ScaleSymbol from = new ScaleSymbol(real2map, new Point((int)(PB_background.Width * 0.7), (int)(PB_background.Height * 0.7)));
                    from.Box = (PictureBox)c;
                    Point newlocation = ComputeResizeLocation(c.Location,resizescale);
                    ScaleSymbol newsymbol = new ScaleSymbol(from, real2map, newlocation, resizescale);
                    PB_background.Controls.Remove(c);
                    this.Add(newsymbol.Box);
                }
                if (c.Name == "CompassSymbol")
                {
                    CompassSymbol from = new CompassSymbol();
                    from.Box = (Label)c;
                    Point newlocation = ComputeResizeLocation(c.Location, resizescale);
                    CompassSymbol newsymbol = new CompassSymbol(from, newlocation, resizescale);
                    PB_background.Controls.Remove(c);
                    this.Add(newsymbol.Box);
                }
                if (c.Name == "LegendSymbol")
                {
                    LegendSymbol from = new LegendSymbol(thismap, new Point((int)(PB_background.Width * 0.7), (int)(PB_background.Height * 0.5)));
                    from.Box = (Label)c;
                    Point newlocation = ComputeResizeLocation(c.Location, resizescale);
                    LegendSymbol newsymbol = new LegendSymbol(thismap, from, newlocation, boxscale);
                    PB_background.Controls.Remove(c);
                    this.Add(newsymbol.Box);
                }
                if(c.Name== "NeatLine")
                {
                    SetNeatline();
                }
            }
        }

        
        private Point ComputeResizeLocation(Point from,double resizescale)
        {
            Point newpoint = new Point((int)(from.X *resizescale), (int)(from.Y * resizescale));
            return newpoint;
        }

        #endregion

        #region 外部接口
        /// <summary>
        /// 添加控件
        /// </summary>
        /// <param name="sender"></param>
        public void Add(Control sender)
        {
            this.PB_background.Controls.Add(sender);
            this.PB_background.Controls.SetChildIndex(sender, 0);
            sender.MouseMove += new MouseEventHandler(this.Control_MouseMove);
            sender.MouseDown += new MouseEventHandler(this.Control_MouseDown);
            sender.MouseUp += new MouseEventHandler(this.Control_MouseUp);
            sender.KeyDown += Sender_KeyDown;
            sender.MouseDoubleClick += Control_MouseDoubleClick;
        }

        private void Sender_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode== Keys.Delete && selectmode == true)
            {
                PB_background.Controls.RemoveAt(0);
                PB_background.Controls.Remove(selectbox);
                selectbox.Dispose();
                selectbox = null;
                selectmode = false;
            }
        }

        /// <summary>
        ///加指北针
        /// </summary>
        public void AddNewCompass()
        {
            LayoutClass.CompassSymbol from = new LayoutClass.CompassSymbol();
            Point location = ComputeResizeLocation(from.Box.Location, boxscale);
            CompassSymbol newcompass = new CompassSymbol(from,location, boxscale);
           this.Add(newcompass.Box);
        }

        /// <summary>
        /// 加标题
        /// </summary>
        public void AddNewTitle()
        {
            LayoutClass.TextboxSymbol from= new LayoutClass.TextboxSymbol(new Point(30, 40), new Font("宋体", 30), Color.Black, "请输入标题");  //??
            Point location = ComputeResizeLocation(from.textbox.Location, boxscale);
            TextboxSymbol text = new TextboxSymbol(from, location, boxscale);
            this.Add(text.textbox);
        }
 
        
        /// <summary>
        /// 加文本
        /// </summary>
        public void AddText()
        {
            LayoutClass.TextboxSymbol from = new LayoutClass.TextboxSymbol(new Point(100, 100), "文本");  //??
            Point location = ComputeResizeLocation(from.textbox.Location, boxscale);
            TextboxSymbol text = new TextboxSymbol(from, location, boxscale);
            this.Add(text.textbox);
        }
        
        /// <summary>
        /// 图例
        /// </summary>
        public void AddLegendSymbol()
        {
            CosmosGIS.LayoutClass.LegendSymbol from = new CosmosGIS.LayoutClass.LegendSymbol(thismap, new Point((int)(PB_background.Width * 0.7), (int)(PB_background.Height * 0.5)));
            Point location = ComputeResizeLocation(from.Box.Location, boxscale);
            LegendSymbol newlengend = new LegendSymbol(thismap, from, location, boxscale);
            this.Add(newlengend.Box);
        }
        
        /// <summary>
        /// 比例尺
        /// </summary>
        public void AddScaleBar()
        {
            LayoutClass.ScaleSymbol from = new LayoutClass.ScaleSymbol(real2map,new Point((int)(PB_background.Width*0.7),(int)(PB_background.Height*0.7)));
            Point location = ComputeResizeLocation(from.Box.Location, boxscale);
            ScaleSymbol newscal = new ScaleSymbol(from, real2map, location, boxscale);
            this.Add(newscal.Box);
        }

        /// <summary>
        /// 保存专题图
        /// </summary>
        /// <param name="filepath"></param>
        public void SaveMap(string filepath)
        {
            Bitmap map = flowLayoutPanel1_Paint();
            map.Save(filepath);
        }

        /// <summary>
        /// 隐藏视图
        /// </summary>
        public void LayoutHide()
        {
            if (selectbox != null)
                selectbox.Dispose();
        }
       

        /// <summary>
        /// 外部更新地图图像
        /// </summary>
        /// <param name="mapbit"></param>
        public void UpdateMap(Bitmap mapbit)
        {
            mapbitmap = mapbit;
            UpdateMapBackground();
        }

        /// <summary>
        /// 更改图廓选项
        /// </summary>
        public void ChangeNeatLine()
        {
            if (NeatLine == null)
                SetNeatline();
            else
            {
                PB_background.Controls.Remove(NeatLine);
                NeatLine = null;
            }
        }

        #endregion

     

        

       

        private void Layout_Resize(object sender, EventArgs e)
        {
           /*
            * for (int i = 0; i < Controls.Count ; i++)
            {
                if (Controls[i].Name== "SPECIAL")
                {
                    Control control = Controls[i];
                   // Controls.RemoveAt(i);
                   // control.Dispose();
                }
            }
            */
           
        }
    }
}
