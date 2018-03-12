using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using CosmosGIS.Projection;

namespace CosmosGIS.Geometry
{
    static class GeometryTools
    {
        /// <summary>
        /// 判断一个点是否位于矩形盒内
        /// </summary>
        /// <param name="point"></param>
        /// <param name="box"></param>
        /// <returns></returns>
        public static bool IsPointInBox(MyPoint point, MyRectangle box)
        {
            if (point.X < box.MinX || point.X > box.MaxX)
                return false;
            else if (point.Y < box.MinY || point.Y > box.MaxY)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 判断一条Polyline是否完全位于矩形盒内
        /// </summary>
        /// <param name="polyline"></param>
        /// <param name="box"></param>
        /// <returns></returns>
        public static bool IsPolylineCompleteInBox(MyMultiPolyline polyline, MyRectangle box)
        {          
            int sPointCount = polyline.PointCount;
            for (int i = 0; i < sPointCount; i++)
            {
                MyPoint pointXY = ETCProjection.LngLat2XY(polyline.Points[i]);  //投影坐标系下的坐标
                if (IsPointInBox(pointXY, box) == false)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// 判断一个多边形是否完全位于矩形盒内
        /// </summary>
        /// <param name="polygon"></param>
        /// <param name="box"></param>
        /// <returns></returns>
        public static bool IsPolygonCompleteInBox(MyMultiPolygon polygon, MyRectangle box)
        {
            int sPointCount = polygon.PointCount;
            for (int i = 0; i < sPointCount; i++)
            {
                MyPoint pointXY = ETCProjection.LngLat2XY(polygon.Points[i]);  //投影坐标系下的坐标
                if (IsPointInBox(pointXY, box) == false)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// 判断指定点是否位于指定圆内
        /// </summary>
        /// <param name="target">指定点</param>
        /// <param name="center">指定圆的圆心</param>
        /// <param name="radius">指定圆的半径</param>
        /// <returns></returns>
        public static bool IsPointInCircle(MyPoint target,MyPoint center,double radius)
        {
            if ((target.X - center.X) * (target.X - center.X) + (target.Y - center.Y) * (target.Y - center.Y) < radius * radius)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 判断指定点是否位于线段缓冲区内
        /// </summary>
        /// <param name="target">指定点</param>
        /// <param name="point1">线段端点1</param>
        /// <param name="point2">线段端点2</param>
        /// <param name="bufferWidth">缓冲区宽度</param>
        /// <returns></returns>
        public static bool IsPointInLineBuffer(MyPoint target, MyPoint point1, MyPoint point2, double bufferWidth)
        {
            double x = target.X, y = target.Y, x1 = point1.X, y1 = point1.Y, x2 = point2.X, y2 = point2.Y;
            double cross = (x2 - x1) * (x - x1) + (y2 - y1) * (y - y1);
            if (cross <= 0)
                if ((x - x1) * (x - x1) + (y - y1) * (y - y1) < bufferWidth * bufferWidth)
                    return true;
                else
                    return false;

            double d2 = (x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1);
            if (cross >= d2)
                if ((x - x2) * (x - x2) + (y - y2) * (y - y2) < bufferWidth * bufferWidth)
                    return true;
                else
                    return false;

            double r = cross / d2;
            double px = x1 + (x2 - x1) * r;
            double py = y1 + (y2 - y1) * r;
            if ((x - px) * (x - px) + (y - py) * (y - py) < bufferWidth * bufferWidth)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 判断指定点是否位于折线缓冲区内
        /// </summary>
        /// <param name="target"></param>
        /// <param name="points"></param>
        /// <param name="bufferWidth"></param>
        /// <returns></returns>
        public static bool IsPointInPolylineBuffer(MyPoint target, MyPoint[] points, double bufferWidth)
        {
            for (int i = 0; i < points.Length - 1; i++)
                if (IsPointInLineBuffer(target, points[i], points[i + 1], bufferWidth) == true)
                    return true;
            return false;
        }

        /// <summary>
        /// 判断指定点向右射线与指定线段是否相交
        /// </summary>
        /// <param name="target"></param>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        private static bool IsIntersect(MyPoint target, MyPoint p1, MyPoint p2)
        {    
            double x1 = p1.X, y1 = p1.Y, x2 = p2.X, y2 = p2.Y;
            if (target.Y > Math.Max(y1, y2) || target.Y < Math.Min(y1, y2))
                return false;
            if (y1 == y2)
                return false;
            double k = (x2 - x1) / (y2 - y1);
            double x = k * (target.Y - y1) + x1;
            if (x < target.X)  //交点在射线左边
                return false;
            if (x < Math.Min(x1, x2) || x > Math.Max(x1, x2))
                return false;
            return true;
        }

        /// <summary>
        /// 判断指定点是否位于指定多边形内，我这里只是考虑了最简单的情况，待完善
        /// </summary>
        /// <param name="target"></param>
        /// <param name="points"></param>
        /// <returns></returns>
        public static bool IsPointInPolygon(MyPoint target, MyPoint[] points)
        {
            bool result = false;
            if (IsPointInPolygonMBR(target, points) == false)
                return false;
            for (int i = 0; i < points.Length - 1; i++)
            {
                if (IsIntersect(target, points[i], points[i + 1]) == true)
                    result = !result;
            }
            if (IsIntersect(target, points[points.Length - 1], points[0]) == true)
                result = !result;
            return result;
        }

        public static bool IsPointInPolygonMBR(MyPoint target, MyPoint[] points)
        {
            double minx = double.MaxValue, maxx = double.MinValue, miny = double.MaxValue, maxy = double.MinValue;
            for (int i = 0; i < points.Length;i++ )
            {
                if (points[i].X < minx)
                    minx = points[i].X;
                if (points[i].X > maxx)
                    maxx = points[i].X;
                if (points[i].Y < miny)
                    miny = points[i].Y;
                if (points[i].Y > maxy)
                    maxy = points[i].Y;
            }
            if (target.X < minx || target.X > maxx)
                return false;
            else if (target.Y < miny || target.Y > maxy)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 获取两个矩形的最小外包矩形
        /// </summary>
        /// <param name="mbr1"></param>
        /// <param name="mbr2"></param>
        /// <returns>两个矩形的最小外包矩形</returns>
        public static MyRectangle GetMBR(MyRectangle mbr1, MyRectangle mbr2)
        {
            double minx = Math.Min(mbr1.MinX, mbr2.MinX);
            double maxx = Math.Min(mbr1.MaxX, mbr2.MaxX);
            double miny = Math.Min(mbr1.MinY, mbr2.MinY);
            double maxy = Math.Min(mbr1.MaxY, mbr2.MaxY);
            return new MyRectangle(minx, maxx, miny, maxy);
        }

        public static bool IsPointsPartInRectangle(PointF[] points,MyRectangle rect)
        {
            for (int i = 0; i < points.Length; i++)
                if (IsPointInBox(new MyPoint(points[i].X, points[i].Y), rect) == true)
                    return true;
            return false;
        }

        /// <summary>
        /// 返回一组点的最小外包矩形
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public static RectangleF GetMBR(PointF[] points)
        {
            float minx = float.MaxValue, maxx = float.MinValue, miny = float.MaxValue, maxy = float.MinValue;
            for (int i = 0; i < points.Length; i++)
            {
                if (points[i].X < minx)
                    minx = points[i].X;
                if (points[i].X > maxx)
                    maxx = points[i].X;
                if (points[i].Y < miny)
                    miny = points[i].Y;
                if (points[i].Y > maxy)
                    maxy = points[i].Y;

            }
            return new RectangleF(minx, miny, maxx - minx, maxy - miny);
        }
    }
}
