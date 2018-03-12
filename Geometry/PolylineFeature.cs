using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using CosmosGIS.Map;
using CosmosGIS.Projection;
using CosmosGIS.MySymbol;
using CosmosGIS.MyRenderer;

namespace CosmosGIS.Geometry
{
    /// <summary>
    /// 线要素,对应一个图层的矢量数据
    /// </summary>
    class PolylineFeature : GeometryFeature
    {
        #region Members
        /// <summary>
        /// 复合折线集合
        /// </summary>
        private List<MyMultiPolyline> polylines = new List<MyMultiPolyline>();
        /// <summary>
        /// 副本
        /// </summary>
        private List<MyMultiPolyline> copy;
        #endregion

        #region 属性
        /// <summary>
        /// 最大FID
        /// </summary>
        public override int MaxFID
        {
            get { return polylines[polylines.Count - 1].FID; }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public PolylineFeature() { }
        /// <summary>
        /// 基于MultiPolyline集合的构造函数
        /// </summary>
        /// <param name="polylines"></param>
        /// <param name="xmin"></param>
        /// <param name="xmax"></param>
        /// <param name="ymin"></param>
        /// <param name="ymax"></param>
        public PolylineFeature(List<MyMultiPolyline> polylines, double xmin, double xmax, double ymin, double ymax)
        {
            this.polylines = polylines;
            this.maxX = xmax;
            this.minX = xmin;
            this.maxY = ymax;
            this.minY = ymin;
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 调整minX,maxX,minY,maxY
        /// </summary>
        private void SetMaxMin()
        {
            minX = double.MaxValue;
            maxX = double.MinValue;
            minY = double.MaxValue;
            maxY = double.MinValue;
            for (int i = 0; i < polylines.Count; i++)
            {
                if (polylines[i].MinX < minX)
                    minX = polylines[i].MinX;
                if (polylines[i].MaxX > maxX)
                    maxX = polylines[i].MaxX;
                if (polylines[i].MinY < minY)
                    minY = polylines[i].MinY;
                if (polylines[i].MaxY > maxY)
                    maxY = polylines[i].MaxY;
            }
        }
        #endregion

        #region Methods

        /// <summary>
        /// 获取线要素数量
        /// </summary>
        /// <returns>线要素数量</returns>
        internal override int GetGeoNum()
        {
            return polylines.Count;
        }

        /// <summary>
        /// 添加折线
        /// </summary>
        /// <param name="geo">指定空间对象</param>
        /// <returns>是否成功添加</returns>
        internal override bool AddGeoObj(object geo)
        {
            try
            {
                MyMultiPolyline l = (MyMultiPolyline)geo;
                if (polylines.Count == 0)
                    l.FID = 0;
                else
                    l.FID = polylines[polylines.Count - 1].FID + 1;
                polylines.Add(l);

                //调整minx,maxx,miny,maxy
                if (l.MaxX > this.maxX)
                    this.maxX = l.MaxX;
                if (l.MinX < this.minX)
                    this.minX = l.MinX;
                if (l.MaxY > this.maxY)
                    this.maxY = l.MaxY;
                if (l.MinY < this.minY)
                    this.minY = l.MinY;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 删除指定空间对象
        /// </summary>
        /// <param name="index">指定索引号</param>
        /// <returns>是否成功删除</returns>
        internal override List<int> DeleteSelectedFeatures()
        {
            List<int> fid = new List<int>();
            for (int i = polylines.Count - 1; i >= 0; i--)
                if (polylines[i].Selected == true)
                {
                    fid.Add(polylines[i].FID);
                    polylines.RemoveAt(i);                    
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
            for (int i = 0; i < polylines.Count; i++)
            {
                if (polylines[i].Selected == true)
                {
                    for (int j = 0; j < polylines[i].PointCount; j++)
                    {
                        MyPoint pointXY = ETCProjection.LngLat2XY(polylines[i].Points[j]);
                        pointXY.X += scale * deltaX;
                        pointXY.Y -= scale * deltaY;
                        polylines[i].Points[j] = ETCProjection.XY2LngLat(pointXY);
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
                string title = "1 " + minX.ToString() + " " + maxX.ToString() + " " + minY.ToString() + " " + maxY.ToString() + " " + polylines.Count.ToString();  //文件头
                sw.WriteLine(title);
                for (int i = 0; i < polylines.Count; i++)
                    polylines[i].WriteCgv(sw);
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
            LineSymbol ls = (LineSymbol)symbol;
            Pen mypen = ls.GetPen;

            MyPoint xyCenter = ETCProjection.LngLat2XY(new MyPoint(centerPos.X, centerPos.Y));
            double xmin = xyCenter.X - scale * bounds.Width / 2;
            double xmax = xyCenter.X + scale * bounds.Width / 2;
            double ymin = xyCenter.Y - scale * bounds.Height / 2;
            double ymax = xyCenter.Y + scale * bounds.Height / 2;
            for (int i = 0; i < polylines.Count; i++)  //对于每一条多线
            {
                List<int> parts = new List<int>(polylines[i].firstIndex);
                parts.Add(polylines[i].PointCount);
                for (int k = 0; k < polylines[i].firstIndex.Length; k++)  //对于每一条多线的子线段
                {
                    List<PointF> pointfs = new List<PointF>();
                    for (int j = parts[k]; j < parts[k + 1]; j++)
                    {
                        MyPoint xyProjection = ETCProjection.LngLat2XY(polylines[i].Points[j]);
                        double xScreen = bounds.Width * (xyProjection.X - xmin) / (xmax - xmin);
                        double yScreen = bounds.Height * (xyProjection.Y - ymin) / (ymax - ymin);
                        PointF p = new PointF((float)xScreen, bounds.Height - (float)yScreen);
                        pointfs.Add(p);
                    }
                    if (GeometryTools.IsPointsPartInRectangle(pointfs.ToArray(), new MyRectangle(bounds.X, bounds.Width, bounds.Y, bounds.Height)) == true)
                    {
                        if (polylines[i].Selected == false)
                            g.DrawLines(mypen, pointfs.ToArray());
                        else
                            g.DrawLines(selectedPen, pointfs.ToArray());
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
            for (int i = 0; i < polylines.Count; i++)
            {
                //从symbol中获取样式
                LineSymbol ls = (LineSymbol)fid2Symbol[polylines[i].FID];
                Pen mypen = ls.GetPen;

                List<int> parts = new List<int>(polylines[i].firstIndex);
                parts.Add(polylines[i].PointCount);
                for (int k = 0; k < polylines[i].firstIndex.Length; k++)  //对于每一条多线的子线段
                {
                    List<PointF> pointfs = new List<PointF>();
                    for (int j = parts[k]; j < parts[k + 1]; j++)
                    {
                        MyPoint xyProjection = ETCProjection.LngLat2XY(polylines[i].Points[j]);
                        double xScreen = bounds.Width * (xyProjection.X - xmin) / (xmax - xmin);
                        double yScreen = bounds.Height * (xyProjection.Y - ymin) / (ymax - ymin);
                        PointF p = new PointF((float)xScreen, bounds.Height - (float)yScreen);
                        pointfs.Add(p);
                    }
                    if (GeometryTools.IsPointsPartInRectangle(pointfs.ToArray(), new MyRectangle(bounds.X, bounds.Width, bounds.Y, bounds.Height)) == true)
                    {
                        if (polylines[i].Selected == false)
                            g.DrawLines(mypen, pointfs.ToArray());
                        else
                            g.DrawLines(selectedPen, pointfs.ToArray());
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
            for (int i = 0; i < polylines.Count; i++)
            {
                string text = fid2Text[polylines[i].FID];
                List<PointF> pointfs = new List<PointF>();
                for (int j = 0; j < polylines[i].PointCount; j++)
                {
                    MyPoint xyProjection = ETCProjection.LngLat2XY(polylines[i].Points[j]);
                    double xScreen = bounds.Width * (xyProjection.X - xmin) / (xmax - xmin);
                    double yScreen = bounds.Height * (xyProjection.Y - ymin) / (ymax - ymin);
                    PointF p = new PointF((float)xScreen, bounds.Height - (float)yScreen);
                    pointfs.Add(p);
                }
                TextSymbol.LineLabel(pointfs.ToArray(), g, textSymbol, text);  //此处注记样式不能修改。待完善
            }
        }
        /// <summary>
        /// 根据矩形盒选择要素，矩形盒坐标位于投影坐标系
        /// </summary>
        /// <param name="box">矩形盒</param>
        internal override List<int> SelectByBox(MyRectangle box)
        {
            List<int> selectedID = new List<int>();
            for (int i = 0; i < polylines.Count; i++)
            {
                polylines[i].Selected = GeometryTools.IsPolylineCompleteInBox(polylines[i], box);
                if (polylines[i].Selected == true)
                    selectedID.Add(i);
            }
            return selectedID;
        }
        /// <summary>
        /// 点选
        /// </summary>
        /// <param name="mouseLocation">鼠标点选位置</param>
        /// <param name="bounds"></param>
        /// <param name="centerPos"></param>
        /// <param name="scale"></param>
        /// <returns>被选中要素FID的List</returns>
        internal override List<int> SelectByPoint(Point mouseLocation, Rectangle bounds, PointF centerPos, double scale)
        {
            List<int> selectedID = new List<int>();
            MyPoint xyCenter = ETCProjection.LngLat2XY(new MyPoint(centerPos.X, centerPos.Y));
            double xmin = xyCenter.X - scale * bounds.Width / 2;
            double xmax = xyCenter.X + scale * bounds.Width / 2;
            double ymin = xyCenter.Y - scale * bounds.Height / 2;
            double ymax = xyCenter.Y + scale * bounds.Height / 2;
            for (int i = 0; i < polylines.Count; i++)
            {
                MyPoint[] screenPoints = new MyPoint[polylines[i].PointCount];
                for (int j = 0; j < polylines[i].PointCount; j++)
                {
                    MyPoint xyProjection = ETCProjection.LngLat2XY(polylines[i].Points[j]);
                    double xScreen = bounds.Width * (xyProjection.X - xmin) / (xmax - xmin);
                    double yScreen = bounds.Height * (xyProjection.Y - ymin) / (ymax - ymin);
                    MyPoint screenPoint = new MyPoint(xScreen, bounds.Height - yScreen);
                    screenPoints[j] = screenPoint;
                }
                polylines[i].Selected = GeometryTools.IsPointInPolylineBuffer(new MyPoint(mouseLocation.X, mouseLocation.Y), screenPoints, margin);
                if (polylines[i].Selected == true)
                    selectedID.Add(polylines[i].FID);
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
            for (int i = 0; i < polylines.Count; i++)
            {
                if (polylines[i].FID == fids[j])
                {
                    polylines[i].Selected = true;
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
            for (int i = 0; i < polylines.Count; i++)
                polylines[i].Selected = false;
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
            for (int i = 0; i < polylines.Count; i++)
            {
                if (polylines[i].Selected == true)  //polylines[i]被选中
                {
                    for (int j = 0; j < polylines[i].PointCount; j++)
                    {
                        MyPoint xyProjection = ETCProjection.LngLat2XY(polylines[i].Points[j]);
                        double xScreen = bounds.Width * (xyProjection.X - xmin) / (xmax - xmin);
                        double yScreen = bounds.Height * (xyProjection.Y - ymin) / (ymax - ymin);
                        MyPoint screen = new MyPoint(xScreen, bounds.Height - yScreen);  //polylines[i].Points[j]在屏幕坐标系的位置
                        polylines[i].Points[j].Selected = GeometryTools.IsPointInCircle(new MyPoint(mouseLocation.X, mouseLocation.Y), screen, margin);  //判断polylines[i].Points[j]是否被选中并修改Selected属性的值
                    }
                }
            }
        }
        /// <summary>
        /// 移动选中节点至新的位置(WGS84坐标系)
        /// </summary>
        /// <param name="newPos"></param>
        internal override void MoveVertex(MyPoint newPos)
        {
            for (int i = 0; i < polylines.Count; i++)
            {
                if (polylines[i].Selected == true)  //polylines[i]被选中
                {
                    for (int j = 0; j < polylines[i].PointCount; j++)
                    {
                        if (polylines[i].Points[j].Selected == true)
                        {
                            polylines[i].Points[j] = newPos;
                            polylines[i].Points[j].Selected = true;
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
            copy = new List<MyMultiPolyline>();
            for (int i = 0; i < polylines.Count; i++)
                copy.Add(polylines[i].Clone());
        }
        /// <summary>
        /// 取消编辑（用副本覆盖原始数据）
        /// </summary>
        internal override void CancelEdit()
        {
            if (copy != null)
                polylines = copy;
            ClearSelection();  //清除选择
        }
        /// <summary>
        /// 保存编辑（更新副本，并写回文件）
        /// </summary>
        internal override void SaveEdit(string path)
        {
            copy.Clear();
            for (int i = 0; i < polylines.Count; i++)
                copy.Add(polylines[i].Clone());

            string filePath = Path.GetDirectoryName(path) + "\\" + Path.GetFileNameWithoutExtension(path) + ".cgv";
            SaveSpaceData(filePath);
        }
        #endregion
    }
}
