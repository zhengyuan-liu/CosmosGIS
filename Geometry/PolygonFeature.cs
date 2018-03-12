using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using CosmosGIS.Map;
using CosmosGIS.Projection;
using CosmosGIS.MySymbol;

namespace CosmosGIS.Geometry
{
    /// <summary>
    /// 多边形要素,对应一个图层的矢量数据
    /// </summary>
    class PolygonFeature : GeometryFeature
    {
        #region Members
        /// <summary>
        /// 复合多边形集合
        /// </summary>
        private List<MyMultiPolygon> polygons = new List<MyMultiPolygon>();
        /// <summary>
        /// polygons的副本，编辑要素时用
        /// </summary>
        private List<MyMultiPolygon> copy = null;
        #endregion

        #region 属性
        /// <summary>
        /// 最大FID
        /// </summary>
        public override int MaxFID
        {
            get { return polygons[polygons.Count - 1].FID; }
        }
        #endregion

        #region Constructor

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public PolygonFeature() { }

        /// <summary>
        /// 基于MultiPolygon集合的构造函数
        /// </summary>
        /// <param name="polygons"></param>
        /// <param name="xmin"></param>
        /// <param name="xmax"></param>
        /// <param name="ymin"></param>
        /// <param name="ymax"></param>
        public PolygonFeature(List<MyMultiPolygon> polygons, double xmin, double xmax, double ymin, double ymax)
        {
            this.polygons = polygons;
            this.maxX = xmax;
            this.minX = xmin;
            this.maxY = ymax;
            this.minY = ymin;
        }
        #endregion

        #region 私有函数
        /// <summary>
        /// 调整minX,maxX,minY,maxY
        /// </summary>
        private void SetMaxMin()
        {
            minX = double.MaxValue;
            maxX = double.MinValue;
            minY = double.MaxValue;
            maxY = double.MinValue;
            for (int i = 0; i < polygons.Count; i++)
            {
                if (polygons[i].MinX < minX)
                    minX = polygons[i].MinX;
                if (polygons[i].MaxX > maxX)
                    maxX = polygons[i].MaxX;
                if (polygons[i].MinY < minY)
                    minY = polygons[i].MinY;
                if (polygons[i].MaxY > maxY)
                    maxY = polygons[i].MaxY;
            }
        }
        #endregion

        #region Methods

        /// <summary>
        /// 获取多边形数量
        /// </summary>
        /// <returns>多边形数量</returns>
        internal override int GetGeoNum()
        {
            return polygons.Count;
        }

        /// <summary>
        /// 添加指定复合多边形对象
        /// </summary>
        /// <param name="geo">复合多边形对象</param>
        /// <returns>是否成功添加</returns>
        internal override bool AddGeoObj(object geo)
        {
            try
            {
                MyMultiPolygon polygon = (MyMultiPolygon)geo;
                if (polygons.Count == 0)
                    polygon.FID = 0;
                else
                    polygon.FID = polygons[polygons.Count - 1].FID + 1;
                polygons.Add(polygon);

                //调整minx,maxx,miny,maxy
                if (polygon.MaxX > this.maxX)
                    this.maxX = polygon.MaxX;
                if (polygon.MinX < this.minX)
                    this.minX = polygon.MinX;
                if (polygon.MaxY > this.maxY)
                    this.maxY = polygon.MaxY;
                if (polygon.MinY < this.minY)
                    this.minY = polygon.MinY;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 删除指定索引号的复合多边形
        /// </summary>
        /// <param name="index">指定索引号</param>
        /// <returns>是否成功删除</returns>
        internal override List<int> DeleteSelectedFeatures()
        {
            List<int> fid = new List<int>();
            for (int i = polygons.Count - 1; i >= 0; i--)
                if (polygons[i].Selected == true)
                {
                    fid.Add(polygons[i].FID);
                    polygons.RemoveAt(i);                   
                }
            SetMaxMin();
            return fid;
        }

        /// <summary>
        /// 移动选中的要素
        /// </summary>
        /// <param name="deltaX">X方向移动量，屏幕坐标系</param>
        /// <param name="deltaY">y方向移动量，屏幕坐标系</param>
        /// <param name="bounds"></param>
        /// <param name="centerPos"></param>
        /// <param name="scale"></param>
        internal override void MoveSelectedFeature(int deltaX, int deltaY, Rectangle bounds, PointF centerPos, double scale)
        {
            for (int i = 0; i < polygons.Count; i++)
            {
                if (polygons[i].Selected == true)
                {
                    for (int j = 0; j < polygons[i].PointCount; j++)
                    {
                        MyPoint pointXY = ETCProjection.LngLat2XY(polygons[i].Points[j]);
                        pointXY.X += scale * deltaX;
                        pointXY.Y -= scale * deltaY;
                        polygons[i].Points[j] = ETCProjection.XY2LngLat(pointXY);
                    }
                }
            }
            SetMaxMin();
        }

        /// <summary>
        /// 以CGV格式向文件中保存空间数据
        /// </summary>
        /// <param name="filePath">文件路径</param>
        internal override void SaveSpaceData(string filePath)
        {
            using (StreamWriter sw = new StreamWriter(new FileStream(filePath, FileMode.Create)))
            {
                string title = "2 " + minX.ToString() + " " + maxX.ToString() + " " + minY.ToString() + " " + maxY.ToString() + " " + polygons.Count.ToString();  //文件头
                sw.WriteLine(title);
                for (int i = 0; i < polygons.Count; i++)
                    polygons[i].WriteCgv(sw);
            }
        }

        /// <summary>
        /// 简单渲染时的绘制要素
        /// </summary>
        /// <param name="g"></param>
        /// <param name="bounds"></param>
        /// <param name="centerPos"></param>
        /// <param name="scale"></param>
        /// <param name="symbol"></param>
        internal override void DrawSpaceData(Graphics g, Rectangle bounds, PointF centerPos, double scale, Symbol symbol)
        {
            //选中要素的样式
            Pen selectedPen = new Pen(Color.Cyan);
            selectedPen.Width = 2.5F;
            //从symbol中获取样式
            PolygonSymbol ps = (PolygonSymbol)symbol;
            Pen mypen = ps.LineStyle.GetPen;
            Brush mybrush = new SolidBrush(ps.GetColor());

            MyPoint xyCenter = ETCProjection.LngLat2XY(new MyPoint(centerPos.X, centerPos.Y));
            double xmin = xyCenter.X - scale * bounds.Width / 2;
            double xmax = xyCenter.X + scale * bounds.Width / 2;
            double ymin = xyCenter.Y - scale * bounds.Height / 2;
            double ymax = xyCenter.Y + scale * bounds.Height / 2;
            for (int i = 0; i < polygons.Count; i++)
            {
                List<int> parts = new List<int>(polygons[i].firstIndex);
                parts.Add(polygons[i].PointCount);
                for (int k = 0; k < polygons[i].firstIndex.Length; k++)  //对于每一个MultiPolygon的子多边形
                {
                    List<PointF> pointfs = new List<PointF>();
                    for (int j = parts[k]; j < parts[k + 1]; j++)
                    {
                        MyPoint xyProjection = ETCProjection.LngLat2XY(polygons[i].Points[j]);
                        double xScreen = bounds.Width * (xyProjection.X - xmin) / (xmax - xmin);
                        double yScreen = bounds.Height * (xyProjection.Y - ymin) / (ymax - ymin);
                        PointF p = new PointF((float)xScreen, bounds.Height - (float)yScreen);
                        pointfs.Add(p);
                    }
                    if (GeometryTools.IsPointsPartInRectangle(pointfs.ToArray(), new MyRectangle(bounds.X, bounds.Width, bounds.Y, bounds.Height)) == true)
                    {
                        g.DrawPolygon(mypen, pointfs.ToArray());
                        g.FillPolygon(mybrush, pointfs.ToArray());                        
                    }
                }
            }
            for (int i = 0; i < polygons.Count; i++)
            {
                if (polygons[i].Selected == true)
                {
                    List<int> parts = new List<int>(polygons[i].firstIndex);
                    parts.Add(polygons[i].PointCount);
                    for (int k = 0; k < polygons[i].firstIndex.Length; k++)  //对于每一个MultiPolygon的子多边形
                    {
                        List<PointF> pointfs = new List<PointF>();
                        for (int j = parts[k]; j < parts[k + 1]; j++)
                        {
                            MyPoint xyProjection = ETCProjection.LngLat2XY(polygons[i].Points[j]);
                            double xScreen = bounds.Width * (xyProjection.X - xmin) / (xmax - xmin);
                            double yScreen = bounds.Height * (xyProjection.Y - ymin) / (ymax - ymin);
                            PointF p = new PointF((float)xScreen, bounds.Height - (float)yScreen);
                            pointfs.Add(p);
                        }
                        if (GeometryTools.IsPointsPartInRectangle(pointfs.ToArray(), new MyRectangle(bounds.X, bounds.Width, bounds.Y, bounds.Height)) == true)
                            g.DrawPolygon(selectedPen, pointfs.ToArray());  //画选中多边形的轮廓
                    }
                }
            }
        }
        /// <summary>
        /// 唯一值渲染和分级渲染时的绘制要素
        /// </summary>
        /// <param name="g"></param>
        /// <param name="bounds"></param>
        /// <param name="centerPos"></param>
        /// <param name="scale"></param>
        /// <param name="fid2Symbol"></param>
        internal override void DrawSpaceData(Graphics g, Rectangle bounds, PointF centerPos, double scale, Dictionary<int, Symbol> fid2Symbol)
        {
            //选中要素的样式
            Pen selectedPen = new Pen(Color.Cyan);
            selectedPen.Width = 2.5F;

            MyPoint xyCenter = ETCProjection.LngLat2XY(new MyPoint(centerPos.X, centerPos.Y));
            double xmin = xyCenter.X - scale * bounds.Width / 2;
            double xmax = xyCenter.X + scale * bounds.Width / 2;
            double ymin = xyCenter.Y - scale * bounds.Height / 2;
            double ymax = xyCenter.Y + scale * bounds.Height / 2;
            for (int i = 0; i < polygons.Count; i++)
            {
                //从symbol中获取样式
                PolygonSymbol ps = (PolygonSymbol)fid2Symbol[polygons[i].FID];
                Pen mypen = ps.LineStyle.GetPen;
                Brush mybrush = new SolidBrush(ps.GetColor());

                List<int> parts = new List<int>(polygons[i].firstIndex);
                parts.Add(polygons[i].PointCount);
                for (int k = 0; k < polygons[i].firstIndex.Length; k++)  //对于每一个MultiPolygon的子多边形
                {
                    List<PointF> pointfs = new List<PointF>();
                    for (int j = parts[k]; j < parts[k + 1]; j++)  //对于每一个MultiPolygon的子多边形的每一个点
                    {
                        MyPoint xyProjection = ETCProjection.LngLat2XY(polygons[i].Points[j]);
                        double xScreen = bounds.Width * (xyProjection.X - xmin) / (xmax - xmin);
                        double yScreen = bounds.Height * (xyProjection.Y - ymin) / (ymax - ymin);
                        PointF p = new PointF((float)xScreen, bounds.Height - (float)yScreen);
                        pointfs.Add(p);
                    }
                    if (GeometryTools.IsPointsPartInRectangle(pointfs.ToArray(), new MyRectangle(bounds.X, bounds.Width, bounds.Y, bounds.Height)) == true)
                    {
                        g.DrawPolygon(mypen, pointfs.ToArray());
                        g.FillPolygon(mybrush, pointfs.ToArray());
                    }
                }
            }
            for (int i = 0; i < polygons.Count; i++)
            {
                if (polygons[i].Selected == true)
                {
                    List<int> parts = new List<int>(polygons[i].firstIndex);
                    parts.Add(polygons[i].PointCount);
                    for (int k = 0; k < polygons[i].firstIndex.Length; k++)  //对于每一个MultiPolygon的子多边形
                    {
                        List<PointF> pointfs = new List<PointF>();
                        for (int j = parts[k]; j < parts[k + 1]; j++)
                        {
                            MyPoint xyProjection = ETCProjection.LngLat2XY(polygons[i].Points[j]);
                            double xScreen = bounds.Width * (xyProjection.X - xmin) / (xmax - xmin);
                            double yScreen = bounds.Height * (xyProjection.Y - ymin) / (ymax - ymin);
                            PointF p = new PointF((float)xScreen, bounds.Height - (float)yScreen);
                            pointfs.Add(p);
                        }
                        if (GeometryTools.IsPointsPartInRectangle(pointfs.ToArray(), new MyRectangle(bounds.X, bounds.Width, bounds.Y, bounds.Height)) == true)
                            g.DrawPolygon(selectedPen, pointfs.ToArray());  //画选中多边形的轮廓
                    }
                }
            }
        }
        /// <summary>
        /// 画注记
        /// </summary>
        /// <param name="g"></param>
        /// <param name="bounds"></param>
        /// <param name="centerPos"></param>
        /// <param name="scale"></param>
        /// <param name="fid2Text"></param>
        internal override void DrawLabel(Graphics g, Rectangle bounds, PointF centerPos, double scale, Dictionary<int, string> fid2Text, TextSymbol textSymbol)
        {
            MyPoint xyCenter = ETCProjection.LngLat2XY(new MyPoint(centerPos.X, centerPos.Y));
            double xmin = xyCenter.X - scale * bounds.Width / 2;
            double xmax = xyCenter.X + scale * bounds.Width / 2;
            double ymin = xyCenter.Y - scale * bounds.Height / 2;
            double ymax = xyCenter.Y + scale * bounds.Height / 2;
            for (int i = 0; i < polygons.Count; i++)
            {
                List<PointF> pointfs = new List<PointF>();
                for (int j = 0; j < polygons[i].PointCount; j++)
                {
                    MyPoint xyProjection = ETCProjection.LngLat2XY(polygons[i].Points[j]);
                    double xScreen = bounds.Width * (xyProjection.X - xmin) / (xmax - xmin);
                    double yScreen = bounds.Height * (xyProjection.Y - ymin) / (ymax - ymin);
                    PointF p = new PointF((float)xScreen, bounds.Height - (float)yScreen);
                    pointfs.Add(p);
                }
                RectangleF mbr = GeometryTools.GetMBR(pointfs.ToArray());
                string text = fid2Text[polygons[i].FID];
                if (Math.Max(mbr.Width, mbr.Height) > textSymbol.TextStyle.TextSize * text.Length)
                    TextSymbol.PolygonLabel(pointfs.ToArray(), g, textSymbol, text);
            }
        }
        /// <summary>
        /// 根据矩形盒选择要素，矩形盒坐标位于投影坐标系
        /// </summary>
        /// <param name="box">矩形盒</param>
        internal override List<int> SelectByBox(MyRectangle box)
        {
            List<int> selectedID = new List<int>();
            for (int i = 0; i < polygons.Count; i++)
            {
                polygons[i].Selected = GeometryTools.IsPolygonCompleteInBox(polygons[i], box);
                if (polygons[i].Selected == true)
                    selectedID.Add(i);
            }
            return selectedID;
        }
        /// <summary>
        /// 点选
        /// </summary>
        /// <returns>被选中要素FID的List</returns>
        internal override List<int> SelectByPoint(Point mouseLocation, Rectangle bounds, PointF centerPos, double scale)
        {
            List<int> selectedID = new List<int>();
            MyPoint xyCenter = ETCProjection.LngLat2XY(new MyPoint(centerPos.X, centerPos.Y));
            double xmin = xyCenter.X - scale * bounds.Width / 2;
            double xmax = xyCenter.X + scale * bounds.Width / 2;
            double ymin = xyCenter.Y - scale * bounds.Height / 2;
            double ymax = xyCenter.Y + scale * bounds.Height / 2;
            for (int i = 0; i < polygons.Count; i++)
            {
                MyPoint[] screenPoints = new MyPoint[polygons[i].PointCount];
                for (int j = 0; j < polygons[i].PointCount; j++)
                {
                    MyPoint xyProjection = ETCProjection.LngLat2XY(polygons[i].Points[j]);
                    double xScreen = bounds.Width * (xyProjection.X - xmin) / (xmax - xmin);
                    double yScreen = bounds.Height * (xyProjection.Y - ymin) / (ymax - ymin);
                    MyPoint screen = new MyPoint(xScreen, bounds.Height - yScreen);
                    screenPoints[j] = screen;
                }
                polygons[i].Selected = GeometryTools.IsPointInPolygon(new MyPoint(mouseLocation.X, mouseLocation.Y), screenPoints);
                if (polygons[i].Selected == true)
                    selectedID.Add(i);
            }
            return selectedID;
        }
        /// <summary>
        /// 通过fid选择要素
        /// </summary>
        /// <param name="fids"></param>
        internal override void SelectByFids(int[] fids)
        {
            ClearSelection();
            if (fids.Length == 0)
                return;
            int j = 0;
            for (int i = 0; i < polygons.Count; i++)
            {
                if (polygons[i].FID == fids[j])
                {
                    polygons[i].Selected = true;
                    j++;
                    if (j == fids.Length)
                        break;
                }
            }
        }
        /// <summary>
        /// 清空选择
        /// </summary>
        internal override void ClearSelection()
        {
            for (int i = 0; i < polygons.Count; i++)
                polygons[i].Selected = false;
        }
        /// <summary>
        /// 点选选中要素的顶点
        /// </summary>
        /// <param name="mouseLocation"></param>
        /// <param name="bounds"></param>
        /// <param name="centerPos"></param>
        /// <param name="scale"></param>
        internal override void SelectVertex(Point mouseLocation, Rectangle bounds, PointF centerPos, double scale)
        {
            MyPoint xyCenter = ETCProjection.LngLat2XY(new MyPoint(centerPos.X, centerPos.Y));
            double xmin = xyCenter.X - scale * bounds.Width / 2;
            double xmax = xyCenter.X + scale * bounds.Width / 2;
            double ymin = xyCenter.Y - scale * bounds.Height / 2;
            double ymax = xyCenter.Y + scale * bounds.Height / 2;
            for (int i = 0; i < polygons.Count; i++)
            {
                if (polygons[i].Selected == true)  //polylines[i]被选中
                {
                    for (int j = 0; j < polygons[i].PointCount; j++)
                    {
                        MyPoint xyProjection = ETCProjection.LngLat2XY(polygons[i].Points[j]);
                        double xScreen = bounds.Width * (xyProjection.X - xmin) / (xmax - xmin);
                        double yScreen = bounds.Height * (xyProjection.Y - ymin) / (ymax - ymin);
                        MyPoint screen = new MyPoint(xScreen, bounds.Height - yScreen);  //polygons[i].Points[j]在屏幕坐标系的位置
                        polygons[i].Points[j].Selected = GeometryTools.IsPointInCircle(new MyPoint(mouseLocation.X, mouseLocation.Y), screen, margin);  //判断polylines[i].Points[j]是否被选中并修改Selected属性的值
                    }
                }
            }
        }
        /// <summary>
        /// 移动选中顶点至新的位置(WGS84坐标系)
        /// </summary>
        /// <param name="newPos"></param>
        internal override void MoveVertex(MyPoint newPos)
        {
            for (int i = 0; i < polygons.Count; i++)
            {
                if (polygons[i].Selected == true)  //polylines[i]被选中
                {
                    for (int j = 0; j < polygons[i].PointCount; j++)
                    {
                        if (polygons[i].Points[j].Selected == true)
                        {
                            polygons[i].Points[j] = newPos;
                            polygons[i].Points[j].Selected = true;
                        }
                    }
                }
            }
            SetMaxMin();
        }
        /// <summary>
        /// 开始编辑（创建副本，然后在原始数据上改）
        /// </summary>
        internal override void StartEditing()
        {
            copy = new List<MyMultiPolygon>();
            for (int i = 0; i < polygons.Count; i++)
                copy.Add(polygons[i].Clone());
        }
        /// <summary>
        /// 取消编辑（用副本覆盖原始数据）
        /// </summary>
        internal override void CancelEdit()
        {
            if (copy != null)
                polygons = copy;
            ClearSelection();
        }
        /// <summary>
        /// 保存编辑（释放副本，并写回文件）
        /// </summary>
        internal override void SaveEdit(string path)
        {
            copy = null;
            string filePath = Path.GetDirectoryName(path) + "\\" + Path.GetFileNameWithoutExtension(path) + ".cgv";
            SaveSpaceData(filePath);
        }
        #endregion
    }
}
