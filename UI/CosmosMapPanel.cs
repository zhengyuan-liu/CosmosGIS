using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using CosmosGIS.Map;
using CosmosGIS.Projection;
using CosmosGIS.FileReader;
using CosmosGIS.Enumeration;
using CosmosGIS.Geometry;
using CosmosGIS.Grid;

namespace CosmosGIS.UI
{
    /// <summary>
    /// 地图面板
    /// </summary>
    public partial class CosmosMapPanel : UserControl
    {
        #region Members
        /// <summary>
        /// 默认地图对象
        /// </summary>
        private MyMap myMap = new MyMap();
        /// <summary>
        /// 显示的地图内存图
        /// </summary>
        private Bitmap mapImg;
        /// <summary>
        /// 标记鼠标上一次位置
        /// </summary>
        private Point mouseOldLoc = new Point();
        /// <summary>
        /// 现用比例尺索引号
        /// </summary>
        private int scaleIndex = maxZoomLevel;
        /// <summary>
        /// 比例尺选择选项，只读
        /// </summary>
        private readonly double[] scaleChoice = new double[maxZoomLevel * 2 + 1];
        /// <summary>
        /// 屏幕中心的经纬度坐标
        /// </summary>
        private PointF centerLngLat = new PointF();
        /// <summary>
        /// 屏幕中心的投影坐标系坐标
        /// </summary>
        private PointF centerXY = new PointF();
        /// <summary>
        /// 投影后的坐标和屏幕坐标系的比值
        /// </summary>
        private double ratio = 1;
        static private int maxZoomLevel = 20;   //最大缩放级别
        static private double zoom = 1.2;  //每次缩放比
        /// <summary>
        /// 地图操作类型
        /// </summary>
        MapOperation operationType = MapOperation.SelectElement;
        //鼠标光标
        private Cursor cursorZoomIn;
        private Cursor cursorZoomOut;
        private Cursor cursorPan;

        private Point startPoint = new Point();  //记录数按下时的位置，用于拉框
        private Point trackingPoint;  //绘制点图层时正在输入的点，屏幕坐标系
        /// <summary>
        /// 绘制线图层或面图层时正在输入的点集，屏幕坐标系
        /// </summary>
        private List<Point> trackingPoints = new List<Point>();
        private List<MyPoint> wgsPoints = new List<MyPoint>();  //绘制线图层或面图层时正在输入的点集，WGS84坐标系
        /// <summary>
        /// 区分是点击事件还是MouseDown、MouseUp事件
        /// </summary>
        bool isClick = false;
        private PropertyTableForm propertyPanel = null;
        #endregion

        #region Constructors
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public CosmosMapPanel()
        {
            InitializeComponent();
            scaleChoice[maxZoomLevel] = 1;
            for (int i = 1; i <= maxZoomLevel; i++)
            {
                scaleChoice[maxZoomLevel - i] = scaleChoice[maxZoomLevel - i + 1] / zoom;
                scaleChoice[maxZoomLevel + i] = scaleChoice[maxZoomLevel + i - 1] * zoom;
            }
            this.picBoxMap.Cursor = cursorPan;  //默认地图为漫游状态
            EditingIndex = -1;
            cursorZoomIn = Cursors.Hand;
            cursorZoomOut = Cursors.Hand;
            cursorPan = Cursors.Hand;
        }
        #endregion

        #region Properties
        internal MyMap Map 
        { 
            get { return myMap; } 
        }
        internal Bitmap MapImg
        {
            get { return mapImg; }
        }
        /// <summary>
        /// 屏幕中心的经纬度坐标
        /// </summary>
        public PointF CenterPos
        {
            get { return centerLngLat; }
            set { centerLngLat = value; }
        }
        public double MapScale
        {
            get { return ratio * scaleChoice[scaleIndex] / 0.0254 * 96; }
        }
        /// <summary>
        /// 比例尺
        /// </summary>
        public double Ratio
        {
            get { return ratio; }
            set { ratio = value; }
        }
        /// <summary>
        /// 正在编辑图层的索引号
        /// </summary>
        public int EditingIndex { get; set; }
        #endregion

        #region 其他事件
        /// <summary>
        /// 窗口大小变化响应的事件
        /// </summary>
        private void CosmosMapPanel_Resize(object sender, EventArgs e)
        {
            if (picBoxMap.Bounds.Width != 0)
            {
                mapImg = new Bitmap(picBoxMap.Bounds.Width, picBoxMap.Bounds.Height);
                UpdateMapImg();
            }
            foreach (Control control in PanelShow.Controls)
            {
                if (control.GetType() == typeof(PropertyTableForm))
                {
                    control.Height = PanelShow.Height;
                    control.Location = new Point(PanelShow.Width - control.Width, 0);
                    picBoxMap.Width = PanelShow.Width - control.Width;
                    UpdateMapImg();
                }
            }
        }
        /// <summary>
        /// 窗口加载完执行的事件
        /// </summary>
        private void CosmosMapPanel_Load(object sender, EventArgs e)
        {
            GenerateNewMap();
        }
        /// <summary>
        /// //Check ListView时发生的事件，点选Root不发生改变，点选ChildNode使Layer改变其可见与否
        /// </summary>
        private void treeViewLayers_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Parent == null && !e.Node.Checked)            //Map根节点的不可取消显示
                e.Node.Checked = true;
            else
                myMap.ChangeSelectedLayerVisible(e.Node.Index);            //改变空间数据的显示与否
            UpdateMapImg();                //更新视图
        }
        /// <summary>
        /// //TreeView的节点双击后会自动折叠展开，避免这种情况的发生，响应折叠节点后的事件
        /// </summary>
        private void treeViewLayers_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            treeViewLayers.ExpandAll();
        }
        #endregion

        #region FOROTHER
        /// <summary>
        /// //点击节点后的Map和Layer属性设置框
        /// </summary>


        private void treeViewLayers_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            //TODO(楚云)
            ///设置某一节点的显示样式
            int index = e.Node.Index;                ///所选节点的索引 
            ////////应该是打开一个窗口进行显示样式的选择
            //MySpaceDataType type = myMap.GetSpaceDataType(index);                        
            //MyGeoStyle style = GetGeoStyle();
            //Color color = GetGeoColor();
            ////////
            //myMap.SetGeoStyle(index, style, color);      //设置所选节点的索引
        }
        /// <summary>
        /// //属性
        /// </summary>
        private void PropertyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayerPropertiesForm lpForm = new LayerPropertiesForm(myMap.Layers[treeViewLayers.SelectedNode.Index]);
            lpForm.Show(this);
            ////同上一个函数，打开一个新窗口进行样式的设置，应该还可以提供相关属性的显示，
        }
        #endregion

        #region LayerMenuEvent
        private void idForm_IdentifyLayerChanged(object sender, int index, Point mousePoint)
        {
            DataTable dt = null;
            if (myMap.LayerNum > index)
                dt = SelectByPoint(mousePoint, index);
            ((IdentifyForm)sender).UpdateData(dt, ScreenToWGS84(mousePoint));
            List<int> ids = new List<int>();
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                    ids.Add(int.Parse(dt.Rows[i][0].ToString()));
            }
            changeSelectionFeatures(ids, index);
            UpdateMapImg();  //重绘
        }
        /// <summary>
        /// 删除图层
        /// </summary>
        private void DelLayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            treeViewLayers.SelectedNode.ContextMenuStrip = null;
            myMap.Deletelayer(treeViewLayers.SelectedNode.Index);
            treeViewLayers.Nodes[0].Nodes.RemoveAt(treeViewLayers.SelectedNode.Index);
            UpdateMapImg();
        }
        /// <summary>
        /// 添加图层
        /// </summary>
        private void AddLayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            treeViewLayers.SelectedNode.ContextMenuStrip = null;
            if (opFileDlgLayers.ShowDialog() == DialogResult.OK)
            {
                myMap.AddLayer(new MyLayer(opFileDlgLayers.FileName));
                TreeNode layerNode = new TreeNode(Path.GetFileNameWithoutExtension(opFileDlgLayers.FileName), 2, 2);
                layerNode.Checked = true;
                treeViewLayers.Nodes[0].Nodes.Insert(0, layerNode);
                treeViewLayers.ExpandAll();
                UpdateMapImg();
            }
        }
        /// <summary>
        /// 用来显示右键菜单
        /// </summary>
        private void treeViewLayers_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.Node.Parent == null)
                {
                    DelLayerToolStripMenuItem.Enabled = false;
                    OpTBToolStripMenuItem.Enabled = false;
                    PropertyToolStripMenuItem.Enabled = false;
                    AddLayerToolStripMenuItem.Enabled = true;
                }
                else
                {
                    AddLayerToolStripMenuItem.Enabled = false;
                    OpTBToolStripMenuItem.Enabled = true;
                    PropertyToolStripMenuItem.Enabled = true;
                    DelLayerToolStripMenuItem.Enabled = true;
                }
                e.Node.ContextMenuStrip = ctxtMenuLayers;
                treeViewLayers.SelectedNode = e.Node;
            }
        }
        /// <summary>
        /// 打开属性表
        /// </summary>
        private void OpTBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (PanelShow.Controls.Count > 1)
                RecoverPanelShow(PanelShow.Controls[PanelShow.Controls.Count - 1].Width);
            treeViewLayers.SelectedNode.ContextMenuStrip = null;
            int layerIndex = treeViewLayers.SelectedNode.Index;
            MyLayer layer = myMap.Layers[layerIndex];
            if (layer.DataType !=  MySpaceDataType.MyGrid)
            {
                propertyPanel = new PropertyTableForm(layer,layerIndex);
                propertyPanel.TopLevel = false;
                PanelShow.Controls.Add(propertyPanel);
                propertyPanel.Height = PanelShow.Height;
                propertyPanel.Location = new Point(PanelShow.Width - propertyPanel.Width, 0);
                propertyPanel.ClosingFormCompleted += new PropertyTableForm.ClosingFormDelegate(RecoverPanelShow);
                propertyPanel.SelectFeatureChanged += new PropertyTableForm.SelectFeatureChangedDelegate(changeSelectionFeatures);
                picBoxMap.Width = PanelShow.Width - propertyPanel.Width;
                propertyPanel.Show();
                UpdateMapImg();
            }
        }
        private void RecoverPanelShow(int width)
        {
            Control control = PanelShow.Controls[PanelShow.Controls.Count - 1];
            if (control.GetType() == typeof(PropertyTableForm))
            {
                PanelShow.Controls.Remove(control);
                picBoxMap.Width += width;
                UpdateMapImg();
            }
            control.Dispose();
            propertyPanel = null;
        }
        #endregion

        #region MouseEvent
        /// <summary>
        /// 实现地图的缩放，鼠标滑轮滚动响应的事件
        /// </summary>
        private void picBoxMap_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0 && scaleIndex < scaleChoice.Length - 1)
                scaleIndex++;
            else if (e.Delta < 0 && scaleIndex > 0)
                scaleIndex--;
            UpdateMapImg();
        }
        /// <summary>
        /// 地图鼠标按下事件
        /// </summary>
        private void picBoxMap_MouseDown(object sender, MouseEventArgs e)
        {
            isClick = true;
            switch (operationType)
            {
                case MapOperation.SelectElement:
                    break;
                case MapOperation.ZoomIn:
                    break;
                case MapOperation.ZoomOut:
                    break;
                case MapOperation.Pan:  //漫游
                    mouseOldLoc = e.Location;
                    break;
                case MapOperation.SelectFeatures:  //选择要素
                    startPoint = e.Location;
                    break;
                case MapOperation.Edit:            //编辑要素
                    mouseOldLoc = e.Location;
                    SelectByPoint(e.Location, EditingIndex);
                    UpdateMapImg();  //重绘
                    break;
                case MapOperation.EditVertices:  //编辑要素顶点
                    myMap.Layers[EditingIndex].SelectVertex(e.Location, picBoxMap.Bounds, centerLngLat, ratio * scaleChoice[scaleIndex]);
                    break;
                case MapOperation.CreateFeatures:  //创建要素
                    if (e.Button == MouseButtons.Left && e.Clicks == 1)
                    {
                        UpdateMapImg();
                        picBoxMap.Refresh();
                        trackingPoint = new Point(e.Location.X, e.Location.Y);
                        switch (myMap.Layers[EditingIndex].DataType)
                        {
                            case MySpaceDataType.MyPoint:
                                myMap.AddFeature(ScreenToWGS84(trackingPoint), EditingIndex);
                                DrawTrackingFeature();  //点要素在这里画编辑的图形
                                break;
                            case MySpaceDataType.MyPolyLine:
                                trackingPoints.Add(trackingPoint);
                                wgsPoints.Add(ScreenToWGS84(trackingPoint));
                                break;
                            case MySpaceDataType.MyPolygon:
                                trackingPoints.Add(trackingPoint);
                                wgsPoints.Add(ScreenToWGS84(trackingPoint));
                                break;
                        }
                    }
                    break;
                case MapOperation.Identify:
                    break;
            }
        }

        internal void changeSelectionFeatures(List<int> ids, int layerIndex)
        {
            if (propertyPanel != null)
                propertyPanel.SetSelections(ids);
            UpdateMapImg();
        }

        /// <summary>
        /// 地图鼠标移动事件
        /// </summary>
        private void picBoxMap_MouseMove(object sender, MouseEventArgs e)
        {
            isClick = false;
            switch (operationType)
            {
                case MapOperation.SelectElement:
                    break;
                case MapOperation.ZoomIn:
                    break;
                case MapOperation.ZoomOut:
                    break;
                case MapOperation.Pan:  //漫游
                    if (e.Button == MouseButtons.Left)
                    {
                        int deltaX = e.X - mouseOldLoc.X;
                        int deltaY = e.Y - mouseOldLoc.Y;
                        centerXY = ETCProjection.LngLat2XY(centerLngLat);
                        centerXY.X -= deltaX * (float)ratio * (float)scaleChoice[scaleIndex];
                        centerXY.Y += deltaY * (float)ratio * (float)scaleChoice[scaleIndex];  //由于屏幕坐标系是左上坐标系，和地理坐标系相反，所以这里应该是+
                        centerLngLat = ETCProjection.XY2LngLat(centerXY);
                        mouseOldLoc = e.Location;
                        UpdateMapImg();
                    }
                    break;
                case MapOperation.SelectFeatures:  //选择要素
                    if (e.Button == MouseButtons.Left)
                    {
                        picBoxMap.Refresh();
                        int minX = Math.Min(startPoint.X, e.Location.X);
                        int maxX = Math.Max(startPoint.X, e.Location.X);
                        int minY = Math.Min(startPoint.Y, e.Location.Y);
                        int maxY = Math.Max(startPoint.Y, e.Location.Y);
                        DrawSelectBox(minX, minY, maxX - minX, maxY - minY);  //画选择盒
                    }
                    break;
                case MapOperation.Edit:  //编辑要素
                    if (e.Button == System.Windows.Forms.MouseButtons.Left)
                    {
                        int deltaX = e.X - mouseOldLoc.X;
                        int deltaY = e.Y - mouseOldLoc.Y;
                        myMap.Layers[EditingIndex].MoveSelectedFeature(deltaX, deltaY, picBoxMap.Bounds, centerLngLat, ratio * scaleChoice[scaleIndex]);
                        mouseOldLoc = e.Location;
                        UpdateMapImg();
                    }
                    break;
                case MapOperation.EditVertices:  //编辑要素顶点
                    if (e.Button == System.Windows.Forms.MouseButtons.Left)
                    {
                        picBoxMap.Cursor = Cursors.SizeAll;
                        myMap.Layers[EditingIndex].MoveVertex(ScreenToWGS84(e.Location));
                        UpdateMapImg();
                    }
                    break;
                case MapOperation.CreateFeatures:  //创建要素
                    if (trackingPoints.Count > 0)
                    {
                        picBoxMap.Refresh();
                        DrawTrackingFeature();
                        DrawRubberBand(e.Location);  //画橡皮筋
                    }
                    break;
            }
            //更新鼠标当前位置对应地理坐标
            MyPoint mouseLngLat = ScreenToWGS84(e.Location);
            StatusStripLblCoordinate.Text = mouseLngLat.X.ToString() + "," + mouseLngLat.Y.ToString();
            StatusStripLblScale.Text = MapScale.ToString();
        }
        /// <summary>
        /// 地图鼠标抬起事件
        /// </summary>
        private void picBoxMap_MouseUp(object sender, MouseEventArgs e)
        {
            switch (operationType)
            {
                case MapOperation.SelectElement:
                    break;
                case MapOperation.ZoomIn:
                    break;
                case MapOperation.ZoomOut:
                    break;
                case MapOperation.Pan:
                    break;
                case MapOperation.SelectFeatures:
                    if (isClick == false)
                    {
                        double minX = Math.Min(startPoint.X, e.Location.X);
                        double maxX = Math.Max(startPoint.X, e.Location.X);
                        if (maxX - minX <= 1)
                        {
                            picBoxMap_Click(sender, e);
                            return;
                        }
                        //屏幕坐标系是左上坐标系，所以纵坐标需要变换
                        double minY = Math.Min(picBoxMap.Height - startPoint.Y, picBoxMap.Height - e.Location.Y);
                        double maxY = Math.Max(picBoxMap.Height - startPoint.Y, picBoxMap.Height - e.Location.Y);
                        if (maxY - minY <= 1)
                        {
                            picBoxMap_Click(sender, e);
                            return;
                        }
                        double centerScreenX = (double)picBoxMap.Width / 2;  //屏幕坐标系中心点X坐标
                        double centerScreenY = (double)picBoxMap.Height / 2;  //屏幕坐标系中心点Y坐标

                        double x1 = centerXY.X - (centerScreenX - minX) * ratio * scaleChoice[scaleIndex];
                        double x2 = centerXY.X - (centerScreenX - maxX) * ratio * scaleChoice[scaleIndex];
                        double y1 = centerXY.Y - (centerScreenY - minY) * ratio * scaleChoice[scaleIndex];
                        double y2 = centerXY.Y - (centerScreenY - maxY) * ratio * scaleChoice[scaleIndex];
                        MyRectangle box = new MyRectangle(x1, x2, y1, y2);
                        SelectByBox(box);
                        UpdateMapImg();
                        if (propertyPanel != null)
                            propertyPanel.SetSelections();
                    }
                    break;
                case MapOperation.Edit:  //编辑要素                  
                    UpdateMapImg();
                    break;
                case MapOperation.EditVertices:  //编辑要素顶点
                    picBoxMap.Cursor = Cursors.Arrow;
                    break;
                case MapOperation.Identify:
                    break;
            }
        }        
        /// <summary>
        /// 地图点击事件
        /// </summary>
        private void picBoxMap_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            switch (operationType)
            {
                case MapOperation.SelectElement:
                    break;
                case MapOperation.ZoomIn:  //放大
                    if (scaleIndex > 0)
                        scaleIndex--;
                    UpdateMapImg();
                    break;
                case MapOperation.ZoomOut:  //缩小
                    if (scaleIndex < scaleChoice.Length - 1)
                        scaleIndex++;
                    UpdateMapImg();
                    break;
                case MapOperation.Pan:
                    break;
                case MapOperation.SelectFeatures:  //选择要素模式
                    if (isClick == true)
                    {
                        SelectByPoint(me.Location);  //在所有图层点选要素
                        UpdateMapImg();  //重绘
                        if (propertyPanel != null)
                            propertyPanel.SetSelections();
                    }
                    break;
                case MapOperation.Edit:  //编辑要素模式
                    break;
                case MapOperation.Identify:
                    for (int i = 0; i < ParentForm.OwnedForms.Length; i++)
                        if (ParentForm.OwnedForms[i].GetType() == typeof(IdentifyForm))
                        {
                            Form form = ParentForm.OwnedForms[i];
                            ParentForm.RemoveOwnedForm(form);
                            form.Dispose();
                        }
                    IdentifyForm idForm = new IdentifyForm(myMap, me.Location);
                    idForm.IdentifyLayerChanged += new IdentifyForm.IdentifyLayerFeatureDelegate(idForm_IdentifyLayerChanged);
                    idForm.Show(ParentForm);
                    break;
            }
        }
        /// <summary>
        /// 地图鼠标双击事件
        /// </summary>
        private void picBoxMap_DoubleClick(object sender, EventArgs e)
        {
            switch (operationType)
            {
                case MapOperation.CreateFeatures:  //创建要素
                    if (myMap.Layers[EditingIndex].DataType == MySpaceDataType.MyPolyLine)  //编辑的是线图层
                    {
                        if (trackingPoints.Count >= 2)
                        {
                            MyMultiPolyline newPolyline = new MyMultiPolyline(wgsPoints);
                            myMap.AddFeature(newPolyline, EditingIndex);
                        }
                    }
                    else if (myMap.Layers[EditingIndex].DataType == MySpaceDataType.MyPolygon)   //编辑的是面图层
                    {
                        if (trackingPoints.Count >= 3)
                        {
                            MyMultiPolygon newPolygon = new MyMultiPolygon(wgsPoints);
                            myMap.AddFeature(newPolygon, EditingIndex);
                        }
                    }
                    UpdateMapImg();
                    trackingPoints.Clear();
                    wgsPoints.Clear();
                    break;
            }
        }
        #endregion

        #region 私有函数
        /// <summary>
        /// 更新地图显示
        /// </summary>
        private void UpdateMapImg()
        {
            Graphics g = Graphics.FromImage(mapImg);
            g.Clear(Color.White);
            myMap.DrawMap(g, picBoxMap.Bounds, centerLngLat, ratio * scaleChoice[scaleIndex]);
            picBoxMap.Image = mapImg;
            g.Dispose();
        }
        /// <summary>
        /// 地理坐标系转屏幕坐标系
        /// </summary>
        /// <param name="geoPos"></param>
        /// <returns></returns>
        private Point GeoToScreenCoordinate(Point geoPos)
        {
            ///TODO
            throw new Exception();
        }
        /// <summary>
        /// 屏幕坐标系转地理坐标系
        /// </summary>
        /// <param name="screenPos"></param>
        /// <returns></returns>
        private MyPoint ScreenToWGS84(Point screenPoint)
        {
            screenPoint.Y = picBoxMap.Height - screenPoint.Y;
            double centerScreenX = (double)picBoxMap.Width / 2;  //屏幕坐标系中心点X坐标
            double centerScreenY = (double)picBoxMap.Height / 2;  //屏幕坐标系中心点Y坐标
            double x = centerXY.X - (centerScreenX - screenPoint.X) * ratio * scaleChoice[scaleIndex];  //投影坐标系目标点X坐标
            double y = centerXY.Y - (centerScreenY - screenPoint.Y) * ratio * scaleChoice[scaleIndex];  //投影坐标系目标点Y坐标
            MyPoint xy = new MyPoint(x, y);  //投影坐标系下的目标点
            MyPoint lnglat = ETCProjection.XY2LngLat(xy);  //WGS84坐标系下的目标点
            return lnglat;
        }
        /// <summary>
        /// 矩形盒选择
        /// </summary>
        /// <param name="box">矩形盒</param>
        private void SelectByBox(MyRectangle box)
        {
            myMap.SelectByBox(box);
        }
        /// <summary>
        /// 在所有图层实施点选
        /// </summary>
        /// <returns></returns>
        private void SelectByPoint(Point mouseLocation)
        {
            myMap.SelectByPoint(mouseLocation, picBoxMap.Bounds, centerLngLat, ratio * scaleChoice[scaleIndex]);
        }
        /// <summary>
        /// 在指定图层实施点选
        /// </summary>
        private DataTable SelectByPoint(Point mouseLocation,int layerIndex)
        {
            return myMap.Layers[layerIndex].SelectByPoint(mouseLocation, picBoxMap.Bounds, centerLngLat, ratio * scaleChoice[scaleIndex]);
        }
        /// <summary>
        /// 画选择矩形盒
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        private void DrawSelectBox(int x, int y, int width, int height)
        {
            Color selectBoxColor = Color.DarkGray;  //选择盒颜色
            float selectBoxWidth = 1.5F;  //选择盒宽度，单位像素
            Pen boxPen = new Pen(selectBoxColor, selectBoxWidth);
            Graphics g = picBoxMap.CreateGraphics();
            g.DrawRectangle(boxPen, x, y, width, height);
            g.Dispose();
        }
        /// <summary>
        /// 画橡皮筋
        /// </summary>
        /// <param name="mouseLocation">鼠标位置</param>
        private void DrawRubberBand(Point mouseLocation)
        {
            Color color = Color.Green;  //橡皮筋颜色
            float width = 2F;  //橡皮筋宽度，单位像素
            Pen pen = new Pen(color, width);
            Graphics g = picBoxMap.CreateGraphics();
            int count = trackingPoints.Count;
            if (myMap.Layers[EditingIndex].DataType != MySpaceDataType.MyPoint && count > 0)
            {
                g.DrawLine(pen, mouseLocation, trackingPoints[count - 1]);
            }
            else if (myMap.Layers[EditingIndex].DataType == MySpaceDataType.MyPolygon && count > 1)
            {
                g.DrawLine(pen, mouseLocation, trackingPoints[count - 1]);
                g.DrawLine(pen, mouseLocation, trackingPoints[0]);
            }
            pen.Dispose();
            g.Dispose();
        }
        private void DrawTrackingFeature()
        {
            int vertexSize = 6;  //正在描绘点集符号的大小，单位像素
            Brush vertexBrush = Brushes.Green;  
            Graphics g = picBoxMap.CreateGraphics();
            for (int i = 0; i < trackingPoints.Count; i++)
            {
                Rectangle rect = new Rectangle(trackingPoints[i].X - vertexSize / 2, trackingPoints[i].Y - vertexSize / 2, vertexSize, vertexSize);
                g.FillRectangle(vertexBrush, rect);
            }
            if (trackingPoints.Count > 1)
            {
                Color color = Color.Green;  //描绘线颜色
                float width = 2F;  //描绘线宽度，单位像素
                Pen pen = new Pen(color, width);
                g.DrawLines(pen, trackingPoints.ToArray());
            }

            int trackingSize = 8;  //描绘顶点符号的大小，单位像素
            Brush trackingBrush = Brushes.Cyan;
            Rectangle symbol = new Rectangle(trackingPoint.X - trackingSize / 2, trackingPoint.Y - trackingSize / 2, trackingSize, trackingSize);
            g.FillEllipse(trackingBrush, symbol);
            g.Dispose();
        }
        #endregion

        #region 地图模式切换
        /// <summary>
        /// 使地图面板进入正常鼠标模式
        /// </summary>
        public void SelectElementMode()
        {
            operationType = MapOperation.SelectElement;
            picBoxMap.Cursor = Cursors.Hand;
        }
        /// <summary>
        /// 使地图面板进入ZoomIn模式
        /// </summary>
        public void ZoomInMode()
        {
            operationType = MapOperation.ZoomIn;
            picBoxMap.Cursor = cursorZoomIn;
        }
        /// <summary>
        /// 使地图面板进入ZoomOut模式
        /// </summary>
        public void ZoomOutMode()
        {
            operationType = MapOperation.ZoomOut;
            picBoxMap.Cursor = cursorZoomOut;
        }
        /// <summary>
        /// 使地图面板进入漫游模式
        /// </summary>
        public void PanMode()
        {
            operationType = MapOperation.Pan;
            picBoxMap.Cursor = Cursors.Hand;
        }
        /// <summary>
        /// 使地图面板进入选择要素模式
        /// </summary>
        public void SelectFeaturesMode()
        {
            operationType = MapOperation.SelectFeatures;
            picBoxMap.Cursor = Cursors.Arrow;
        }
        /// <summary>
        /// 使地图面板进入编辑要素模式
        /// </summary>
        public void StartEditMode(int index)
        {
            operationType = MapOperation.Edit;
            picBoxMap.Cursor = Cursors.Arrow;
            EditingIndex = index;
            myMap.Layers[EditingIndex].StartEditing();
        }
        /// <summary>
        /// 使地图面板进入编辑要素模式
        /// </summary>
        public void EditMode(int index)
        {
            operationType = MapOperation.Edit;
            picBoxMap.Cursor = Cursors.Arrow;
            EditingIndex = index;
        }
        /// <summary>
        /// 使地图面板结束编辑模式
        /// </summary>
        public void EndEditMode()
        {
            operationType = MapOperation.SelectElement;  //回到SelectElement模式
            picBoxMap.Cursor = Cursors.Default;
            myMap.Layers[EditingIndex].StopEditing();
            UpdateMapImg();
        }
        /// <summary>
        /// 使地图进入编辑节点模式
        /// </summary>
        internal void EditVerticesMode()
        {
            operationType = MapOperation.EditVertices;
            picBoxMap.Cursor = Cursors.Arrow;
        }
        /// <summary>
        /// 使地图面板进入创建要素模式
        /// </summary>
        /// <param name="index">当前编辑图层的索引号</param>
        public void CreateFeatureMode(int index)
        {
            operationType = MapOperation.CreateFeatures;
            picBoxMap.Cursor = Cursors.Cross;
            EditingIndex = index;
            ClearSelection();
        }
        /// <summary>
        /// 使地图面板进入Identify模式
        /// </summary>
        internal void IdentifyMode()
        {
            operationType = MapOperation.Identify;
            picBoxMap.Cursor = Cursors.Help;
        }
        #endregion

        #region 公开方法
        /// <summary>
        /// 构造一个新的地图
        /// </summary>
        internal void GenerateNewMap()
        {
            treeViewLayers.Nodes.Clear();
            myMap = new MyMap();
            TreeNode mapNode = new TreeNode(myMap.MapName, 1, 1);       //添加根节点，Map节点
            mapNode.Checked = true;
            treeViewLayers.Nodes.Add(mapNode);
            treeViewLayers.ExpandAll();                 //打开所有节点
            picBoxMap.MouseWheel += new MouseEventHandler(picBoxMap_MouseWheel);          //添加鼠标滑轮缩放地图
            mapImg = new Bitmap(picBoxMap.Bounds.Width, picBoxMap.Bounds.Height);           //构造地图内存图
        }
        /// <summary>
        /// 从CGM地图文件中复原地图
        /// </summary>
        internal void GenerateMapFromCGM(string FileName)
        {
            treeViewLayers.Nodes.Clear();

            MapReader.CGM newCGM = new MapReader.CGM(FileName);
            myMap = newCGM.MyMap;
            ratio = newCGM.Ratio;
            scaleIndex = newCGM.ScaleIndex;
            centerLngLat = newCGM.CenterPos;
            centerXY = ETCProjection.LngLat2XY(centerLngLat);  //同步更新屏幕中心点投影坐标系坐标

            int i = 0;
            TreeNode mapNode = new TreeNode(myMap.MapName, 1, 1);       //添加根节点，Map节点
            mapNode.Checked = true;
            treeViewLayers.Nodes.Add(mapNode);
            for (i = 0; i < myMap.LayerNum; i++)                  //添加空间数据对象节点
            {
                TreeNode layerNode = new TreeNode(myMap.GetLayerName(i), 2, 2);
                layerNode.Checked = true;
                treeViewLayers.Nodes[0].Nodes.Add(layerNode);
            }
            if (myMap.hasBaseMap)
            {
                TreeNode basemapNode = new TreeNode(myMap.GetLayerName(i), 1, 1);   //添加basemap对象节点       
                basemapNode.Checked = true;
                treeViewLayers.Nodes[0].Nodes.Add(basemapNode);
            }
            treeViewLayers.ExpandAll();                 //打开所有节点
            picBoxMap.MouseWheel += new MouseEventHandler(picBoxMap_MouseWheel);          //添加鼠标滑轮缩放地图
            mapImg = new Bitmap(picBoxMap.Bounds.Width, picBoxMap.Bounds.Height);           //构造地图内存图
            UpdateMapImg();
        }

        internal void SaveCGM(string path)
        {
            MapReader.CGM newCGM = new MapReader.CGM(myMap, centerLngLat, ratio, scaleIndex);
            newCGM.Save(path);
          
        }
        /// <summary>
        /// 增加Shapefile图层
        /// </summary>
        /// <param name="shp"></param>
        internal void AddLayer(Shapefile shp)
        {
            myMap.AddLayer(new MyLayer(shp));
            TreeNode layerNode = new TreeNode(shp.Name, 2, 2);
            layerNode.Checked = true;
            centerLngLat = shp.CenterPos;
            centerXY = ETCProjection.LngLat2XY(centerLngLat);  //同步更新屏幕中心点投影坐标系坐标
            treeViewLayers.Nodes[0].Nodes.Insert(0, layerNode);
            treeViewLayers.ExpandAll();
            double x = ETCProjection.Longitude2X(shp.Xmax) - ETCProjection.Longitude2X(shp.Xmin);
            double y = ETCProjection.Latitude2Y(shp.Ymax) - ETCProjection.Latitude2Y(shp.Ymin);
            double scale1 = x / Width;
            double scale2 = y / Height;
            Ratio = Math.Max(scale1, scale2);
            scaleIndex = maxZoomLevel;
            UpdateMapImg();
        }
        /// <summary>
        /// 增加CGV图层
        /// </summary>
        /// <param name="cgv"></param>
        internal void AddLayer(CosmosGisVector cgv)
        {
            myMap.AddLayer(new MyLayer(cgv));
            TreeNode layerNode = new TreeNode(cgv.Name, 2, 2);
            layerNode.Checked = true;
            treeViewLayers.Nodes[0].Nodes.Insert(0, layerNode);
            treeViewLayers.ExpandAll();
            if (cgv.FeatureCount != 0)
            {
                centerLngLat = new PointF(((float)cgv.MaxX + (float)cgv.MinX) * 0.5F, ((float)cgv.MaxY + (float)cgv.MinY) * 0.5F);
                centerXY = ETCProjection.LngLat2XY(centerLngLat);  //同步更新屏幕中心点投影坐标系坐标
                double x = ETCProjection.Longitude2X(cgv.MaxX) - ETCProjection.Longitude2X(cgv.MinX);
                double y = ETCProjection.Latitude2Y(cgv.MaxY) - ETCProjection.Latitude2Y(cgv.MinY);
                double scale1 = x / Width;
                double scale2 = y / Height;
                Ratio = Math.Max(scale1, scale2);
                scaleIndex = maxZoomLevel;
                UpdateMapImg();
            }     
        }
        /// <summary>
        /// 增加栅格图层
        /// </summary>
        /// <param name="grid"></param>
        internal void AddLayer(MyGrid grid)
        {
            myMap.AddLayer(new MyLayer(grid));
            TreeNode layerNode = new TreeNode(grid.Name, 2, 2);
            layerNode.Checked = true;
            treeViewLayers.Nodes[0].Nodes.Insert(0, layerNode);
            treeViewLayers.ExpandAll();
            centerLngLat = new PointF(((float)grid.MaxX + (float)grid.MinX) * 0.5F, ((float)grid.MaxY + (float)grid.MinY) * 0.5F);
            centerXY = ETCProjection.LngLat2XY(centerLngLat);  //同步更新屏幕中心点投影坐标系坐标
            double x = ETCProjection.Longitude2X(grid.MaxX) - ETCProjection.Longitude2X(grid.MinX);
            double y = ETCProjection.Latitude2Y(grid.MaxY) - ETCProjection.Latitude2Y(grid.MinY);
            double scale1 = x / Width;
            double scale2 = y / Height;
            Ratio = Math.Max(scale1, scale2);
            scaleIndex = maxZoomLevel;
            UpdateMapImg();
        }
        /// <summary>
        /// 保存编辑
        /// </summary>
        internal void SaveEdit()
        {
            myMap.Layers[EditingIndex].SaveEdit();
            picBoxMap.Refresh();  //刷新，去掉trackingPoint的symbol
            UpdateMapImg();       //重绘，显示编辑完成后的地图
        }
        /// <summary>
        /// 删除正在编辑图层中被选中的要素
        /// </summary>
        /// <param name="index"></param>
        internal void DeleteFeature(int index)
        {
            myMap.Layers[EditingIndex].DeleteSelectedFeatures();
            UpdateMapImg();       //重绘，显示删除要素后的地图
        }
        /// <summary>
        /// 保存地图图像
        /// </summary>
        /// <param name="filename">文件路径</param>
        /// <param name="strFormat">保存格式</param>
        internal void SaveMapImg(Bitmap bitmap, string filename, ImageFormat format)
        {
            double zoom = (double)bitmap.Width / mapImg.Width;
            Graphics g = Graphics.FromImage(bitmap);
            g.Clear(Color.White);
            Rectangle bounds = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            myMap.DrawMap(g, bounds, centerLngLat, ratio * scaleChoice[scaleIndex] / zoom);
            g.Dispose();
            bitmap.Save(filename, format);
        }
        internal void FullExtent()
        {
            if(myMap.LayerNum!=0)
            {
                MyRectangle mbr = myMap.GetMBR();
                centerLngLat = new PointF(((float)mbr.MaxX + (float)mbr.MinX) * 0.5F, ((float)mbr.MaxY + (float)mbr.MinY) * 0.5F);
                double x = ETCProjection.Longitude2X(mbr.MaxX) - ETCProjection.Longitude2X(mbr.MinX);
                double y = ETCProjection.Latitude2Y(mbr.MaxY) - ETCProjection.Latitude2Y(mbr.MinY);
                double scale1 = x / Width;
                double scale2 = y / Height;
                Ratio = Math.Max(scale1, scale2);
                scaleIndex = maxZoomLevel;
                UpdateMapImg();
            }
        }
        /// <summary>
        /// 清空选择
        /// </summary>
        internal void ClearSelection()
        {
            for (int i = 0; i < myMap.LayerNum; i++)
                myMap.Layers[i].SpaceData.ClearSelection();
            UpdateMapImg();
        }
        #endregion

        #region NOTRECTIFY(这部分是在网上找的，不太熟悉，不要修改)
        /// <summary>
        /// ///用来实现移动图层
        /// </summary>
        private void treeViewLayers_DragDrop(object sender, DragEventArgs e)
        {       
            Point targetPos = ((TreeView)sender).PointToClient(new Point(e.X, e.Y));
            TreeNode targetNode = this.treeViewLayers.GetNodeAt(targetPos);           
            if (targetNode.Nodes.Count != 0)
                return;
            TreeNode moveNode = (TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode");
            TreeNode NewMoveNode = (TreeNode)moveNode.Clone();
            targetNode.Parent.Nodes.Insert(targetNode.Index, NewMoveNode);        
            myMap.ChangeLayerIndex(moveNode.Index - 1, targetNode.Index - 1);
            treeViewLayers.SelectedNode = NewMoveNode;
            targetNode.Expand();
            moveNode.Remove();
            UpdateMapImg();        
        }
        private void treeViewLayers_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                DoDragDrop(e.Item, DragDropEffects.Move);
        }
        private void treeViewLayers_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("System.Windows.Forms.TreeNode"))
                e.Effect = DragDropEffects.Move;
            else
                e.Effect = DragDropEffects.None;
        }
        #endregion

        #region 视图切换与Layout视图相关方法

        Layout layoutcontrol = null;
        private bool isedit = true;
        public bool IsEdit { get { return isedit; } }
        /// <summary>
        /// 进入Data视图
        /// </summary>
        internal void toolStripDataView_Click(object sender, EventArgs e)
        {
            isedit = true;
            if (PanelShow.Controls.Contains(layoutcontrol))
            {
                //PanelShow.Controls.Remove(layoutcontrol);

                layoutcontrol.LayoutHide();
                layoutcontrol.Visible = false;
            }
            picBoxMap.Visible = true;
        }
        /// <summary>
        /// 进入Layout视图
        /// </summary>
        internal void toolStripLayoutView_Click(object sender, EventArgs e)
        {
            isedit = false;
            Bitmap newimage = new Bitmap((Bitmap)picBoxMap.Image, picBoxMap.Size);
            if (layoutcontrol == null)
            {
                layoutcontrol = new Layout(this.Map, this.MapScale, newimage, picBoxMap.Size, picBoxMap.Location);
                PanelShow.Controls.Add(layoutcontrol);

            }
            else
            {
                layoutcontrol.UpdateMap(newimage);
                layoutcontrol.Update();
                layoutcontrol.Visible = true;
            }
            picBoxMap.Visible = false;
        }
        /// <summary>
        /// 将控件加入Layout视图
        /// </summary>
        /// <param name="sender"></param>
        public void AddLayout(Control sender)
        {
            if (layoutcontrol != null)
                layoutcontrol.Add(sender);
        }

        /// <summary>
        ///加指北针
        /// </summary>
        public void AddNewCompass()
        {
            if (layoutcontrol != null)
                layoutcontrol.AddNewCompass();
        }

        /// <summary>
        /// 加标题
        /// </summary>
        public void AddNewTitle()
        {
            if (layoutcontrol != null)
                layoutcontrol.AddNewTitle();
        }


        /// <summary>
        /// 加文本
        /// </summary>
        public void AddText()
        {
            if (layoutcontrol != null)
                layoutcontrol.AddText();
        }

        /// <summary>
        /// 图例
        /// </summary>
        public void AddLegendSymbol()
        {
            if (layoutcontrol != null)
                layoutcontrol.AddLegendSymbol();
        }
        /// <summary>
        /// 比例尺
        /// </summary>
        public void AddScaleBar()
        {
            if (layoutcontrol != null)
                layoutcontrol.AddScaleBar();
        }
        /// <summary>
        /// 更改图廓
        /// </summary>
        public void ChangeNeatLine()
        {
            if (layoutcontrol != null)
                layoutcontrol.ChangeNeatLine();
        }
        /// <summary>
        /// 保存专题地图
        /// </summary>
        /// <param name="path"></param>
        public void SaveSpecialMap(string path)
        {
            if (layoutcontrol != null)
                layoutcontrol.SaveMap(path);
        }
        #endregion
    }
}
