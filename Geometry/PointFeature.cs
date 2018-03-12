using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using CosmosGIS.Map;
using CosmosGIS.Projection;
using CosmosGIS.MyRenderer;
using CosmosGIS.MySymbol;

namespace CosmosGIS.Geometry
{
    class PointFeature : GeometryFeature
    {
        #region Members
        /// <summary>
        /// 点要素集合,对应一个图层的空间矢量数据
        /// </summary>
        List<MyPoint> myPoints = new List<MyPoint>();
        /// <summary>
        /// 点要素集合的副本
        /// </summary>
        List<MyPoint> copy;
        #endregion

        #region 属性
        /// <summary>
        /// 最大FID
        /// </summary>
        public override int MaxFID
        {
            get { return myPoints[myPoints.Count - 1].FID; }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public PointFeature() { }
        /// <summary>
        /// 基于点集的构造函数
        /// </summary>
        /// <param name="points"></param>
        /// <param name="xmin"></param>
        /// <param name="xmax"></param>
        /// <param name="ymin"></param>
        /// <param name="ymax"></param>
        public PointFeature(List<MyPoint> points, double xmin, double xmax, double ymin, double ymax)
        {
            this.myPoints = points;
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
            for (int i = 0; i < myPoints.Count; i++)
            {
                if (myPoints[i].X < minX)
                    minX = myPoints[i].X;
                if (myPoints[i].X > maxX)
                    maxX = myPoints[i].X;
                if (myPoints[i].Y < minY)
                    minY = myPoints[i].Y;
                if (myPoints[i].Y > maxY)
                    maxY = myPoints[i].Y;
            }
        }
        #endregion

        #region Methods

        /// <summary>
        /// 获取点的数量
        /// </summary>
        /// <returns>点的数量</returns>
        internal override int GetGeoNum()
        {
            return myPoints.Count;
        }

        /// <summary>
        /// 添加指定点
        /// </summary>
        /// <param name="geo">指定点</param>
        /// <returns>是否成功添加</returns>
        internal override bool AddGeoObj(object geo)
        {
            try
            {
                MyPoint p = (MyPoint)geo;
                if (myPoints.Count == 0)
                    p.FID = 0;
                else
                    p.FID = myPoints[myPoints.Count - 1].FID + 1;
                myPoints.Add(p);

                //调整minx,maxx,miny,maxy
                if (p.X > this.maxX)
                    this.maxX = p.X;
                if (p.X < this.minX)
                    this.minX = p.X;
                if (p.Y > this.maxY)
                    this.maxY = p.Y;
                if (p.Y < this.minY)
                    this.minY = p.Y;
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 删除选中的要素
        /// </summary>
        /// <returns></returns>
        internal override List<int> DeleteSelectedFeatures()
        {
            List<int> fid = new List<int>();
            for (int i = myPoints.Count - 1; i >= 0; i--)
                if (myPoints[i].Selected == true)
                {
                    fid.Add(myPoints[i].FID);
                    myPoints.RemoveAt(i);                   
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
            for (int i = 0; i < myPoints.Count; i++)
            {
                if(myPoints[i].Selected==true)
                {
                    int id = myPoints[i].FID;
                    MyPoint pointXY = ETCProjection.LngLat2XY(myPoints[i]);
                    pointXY.X += scale * deltaX;
                    pointXY.Y -= scale * deltaY;
                    myPoints[i] = ETCProjection.XY2LngLat(pointXY);
                    myPoints[i].FID = id;
                    myPoints[i].Selected = true;
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
                string title = "0 " + minX.ToString() + " " + maxX.ToString() + " " + minY.ToString() + " " + maxY.ToString() + " " + myPoints.Count.ToString();  //文件头
                sw.WriteLine(title);
                string strLine;
                for (int i = 0; i < myPoints.Count; i++)
                {
                    strLine = myPoints[i].FID.ToString() + " " + myPoints[i].X.ToString() + " " + myPoints[i].Y.ToString();
                    sw.WriteLine(strLine);
                }
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
            Brush selectedBrush = new SolidBrush(Color.Cyan);
            PointSymbol ps = (PointSymbol)symbol;

            MyPoint xyCenter = ETCProjection.LngLat2XY(new MyPoint(centerPos.X, centerPos.Y));
            double xmin = xyCenter.X - scale * bounds.Width / 2;
            double xmax = xyCenter.X + scale * bounds.Width / 2;
            double ymin = xyCenter.Y - scale * bounds.Height / 2;
            double ymax = xyCenter.Y + scale * bounds.Height / 2;
            for (int i = 0; i < myPoints.Count; i++)
            {
                MyPoint xyProjection = ETCProjection.LngLat2XY(myPoints[i]);
                double xScreen = bounds.Width * (xyProjection.X - xmin) / (xmax - xmin);
                double yScreen = bounds.Height * (xyProjection.Y - ymin) / (ymax - ymin);
                if (GeometryTools.IsPointInBox(new MyPoint(xScreen, bounds.Height - yScreen), new MyRectangle(bounds.X, bounds.Width, bounds.Y, bounds.Height)) == true)
                {
                    if (myPoints[i].Selected == true)
                    {
                        Rectangle rect = new Rectangle((int)xScreen - 5, bounds.Height - (int)yScreen - 5, 10, 10);
                        g.FillEllipse(selectedBrush, rect);
                    }
                    else
                        ps.GetSymbol(g, new PointF((float)xScreen, bounds.Height - (float)yScreen));
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
            Brush selectedBrush = new SolidBrush(Color.Cyan);

            MyPoint xyCenter = ETCProjection.LngLat2XY(new MyPoint(centerPos.X, centerPos.Y));
            double xmin = xyCenter.X - scale * bounds.Width / 2;
            double xmax = xyCenter.X + scale * bounds.Width / 2;
            double ymin = xyCenter.Y - scale * bounds.Height / 2;
            double ymax = xyCenter.Y + scale * bounds.Height / 2;
            for (int i = 0; i < myPoints.Count; i++)
            {
                //从symbol中获取样式
                PointSymbol ps = (PointSymbol)fid2Symbol[myPoints[i].FID];
                MyPoint xyProjection = ETCProjection.LngLat2XY(myPoints[i]);
                double xScreen = bounds.Width * (xyProjection.X - xmin) / (xmax - xmin);
                double yScreen = bounds.Height * (xyProjection.Y - ymin) / (ymax - ymin);
                if (GeometryTools.IsPointInBox(new MyPoint(xScreen, yScreen), new MyRectangle(bounds.X, bounds.Width, bounds.Y, bounds.Height)) == true)
                {
                    if (myPoints[i].Selected == true)
                    {
                        Rectangle rect = new Rectangle((int)xScreen - 5, bounds.Height - (int)yScreen - 5, 10, 10);
                        g.FillEllipse(selectedBrush, rect);
                    }
                    else
                        ps.GetSymbol(g, new PointF((float)xScreen, bounds.Height - (float)yScreen));
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
            for (int i = 0; i < myPoints.Count; i++)
            {
                string text = fid2Text[myPoints[i].FID];
                MyPoint xyProjection = ETCProjection.LngLat2XY(myPoints[i]);
                double xScreen = bounds.Width * (xyProjection.X - xmin) / (xmax - xmin);
                double yScreen = bounds.Height * (xyProjection.Y - ymin) / (ymax - ymin);
                TextSymbol.PointLabel(new PointF((float)xScreen, bounds.Height - (float)yScreen), g, textSymbol, text);  //此处注记样式不能修改。待完善
            }
        }
        /// <summary>
        /// 根据矩形盒选择要素，矩形盒坐标位于投影坐标系
        /// </summary>
        /// <param name="box">矩形盒</param>
        internal override List<int> SelectByBox(MyRectangle box)
        {
            List<int> selectedID = new List<int>();
            for (int i = 0; i < myPoints.Count; i++)
            {
                MyPoint pointXY = ETCProjection.LngLat2XY(myPoints[i]);  //投影坐标系下的坐标
                myPoints[i].Selected = GeometryTools.IsPointInBox(pointXY, box);
                if (myPoints[i].Selected == true)
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
            for (int i = 0; i < myPoints.Count; i++)
            {
                MyPoint xyProjection = ETCProjection.LngLat2XY(myPoints[i]);
                double xScreen = bounds.Width * (xyProjection.X - xmin) / (xmax - xmin);
                double yScreen = bounds.Height - bounds.Height * (xyProjection.Y - ymin) / (ymax - ymin);
                MyPoint screen = new MyPoint(xScreen, yScreen);
                myPoints[i].Selected = GeometryTools.IsPointInCircle(new MyPoint(mouseLocation.X, mouseLocation.Y), screen, margin);
                if (myPoints[i].Selected == true)
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
            for(int i=0;i<myPoints.Count;i++)
            {
                if (myPoints[i].FID == fids[j])
                {
                    myPoints[i].Selected = true;
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
            for (int i = 0; i < myPoints.Count; i++)
                myPoints[i].Selected = false;
        }
        /// <summary>
        /// 点选选中要素的顶点，此方法对点要素图层无效
        /// </summary>
        /// <param name="mouseLocation"></param>
        /// <param name="bounds"></param>
        /// <param name="centerPos"></param>
        /// <param name="scale"></param>
        internal override void SelectVertex(Point mouseLocation, Rectangle bounds, PointF centerPos, double scale)
        {

        }
        /// <summary>
        /// 移动选中顶点至新的位置(WGS84坐标系)，此方法对点要素图层无效
        /// </summary>
        /// <param name="newPos"></param>
        internal override void MoveVertex(MyPoint newPos)
        {

        }
        /// <summary>
        /// 开始编辑（创建副本，然后在原始数据上改）
        /// </summary>
        internal override void StartEditing()
        {
            copy = new List<MyPoint>();
            for (int i = 0; i < myPoints.Count; i++)
                copy.Add(new MyPoint(myPoints[i].X, myPoints[i].Y, myPoints[i].FID));
        }
        /// <summary>
        /// 取消编辑（用副本覆盖原始数据）
        /// </summary>
        internal override void CancelEdit()
        {
            myPoints = copy;          
            ClearSelection();  //清除选择
        }
        /// <summary>
        /// 保存编辑（更新副本，并写回文件）
        /// </summary>
        internal override void SaveEdit(string path)
        {
            copy.Clear();
            for (int i = 0; i < myPoints.Count; i++)
                copy.Add(new MyPoint(myPoints[i].X, myPoints[i].Y, myPoints[i].FID));

            string filePath = Path.GetDirectoryName(path) + "\\" + Path.GetFileNameWithoutExtension(path) + ".cgv";
            SaveSpaceData(filePath);
        }
        #endregion
    }
}
