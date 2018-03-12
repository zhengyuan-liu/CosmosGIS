using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CosmosGIS.Geometry;
using System.Drawing;

namespace CosmosGIS.Projection
{
    /// <summary>
    /// 等距切圆柱投影 Equidistant Tangent Cylinder Projection
    /// </summary>
    class ETCProjection
    {
        static double a = 6378137, b = 6356752;  //WGS84椭球体参数
        static double alpha = a;
        static double e2 = (a * a - b * b) / (a * a);
        static double e = Math.Sqrt(e2);
        static double center = 110;  //中央经线经度,角度制

        /// <summary>
        /// 经纬度坐标转XY坐标, 经度对应X，纬度对应Y
        /// </summary>
        /// <param name="coordinate"></param>
        /// <returns></returns>
        public static MyPoint LngLat2XY(MyPoint coordinate)
        {
            double x = Longitude2X(coordinate.X);
            double y = Latitude2Y(coordinate.Y);
            int id = coordinate.FID;
            return new MyPoint(x, y, id);
        }

        /// <summary>
        /// 经纬度坐标转XY坐标, 经度对应X，纬度对应Y
        /// </summary>
        /// <param name="coordinate">经纬度坐标</param>
        /// <returns>XY坐标</returns>
        public static PointF LngLat2XY(PointF coordinate)
        {
            float x = (float)Longitude2X(coordinate.X);
            float y = (float)Latitude2Y(coordinate.Y);
            return new PointF(x, y);
        }

        /// <summary>
        /// XY坐标转经纬度坐标, X对应经度，Y对应纬度
        /// </summary>
        /// <param name="coordinate">XY坐标</param>
        /// <returns>经纬度坐标</returns>
        public static MyPoint XY2LngLat(MyPoint coordinate)
        {
            double lng = X2Longitude(coordinate.X);
            double lat = Y2Latitude(coordinate.Y);
            int id = coordinate.FID;
            return new MyPoint(lng, lat, id);
        }
        /// <summary>
        /// XY坐标转经纬度坐标, X对应经度，Y对应纬度
        /// </summary>
        /// <param name="coordinate">XY坐标</param>
        /// <returns>经纬度坐标</returns>
        public static PointF XY2LngLat(PointF coordinate)
        {
            float lng = (float)X2Longitude(coordinate.X);
            float lat = (float)Y2Latitude(coordinate.Y);
            return new PointF(lng, lat);
        }

        public static double Latitude2Y(double latitude)
        {
            double laRad = Deg2Rad(latitude);
            double y = 0.5 * a * (1 - e2) / e
                   * (e * Math.Sin(laRad) / (1 - e2 * Math.Sin(laRad) * Math.Sin(laRad))
                   + Math.Log((1 + e * Math.Sin(laRad)) / Math.Sqrt(1 - e2 * Math.Sin(laRad) * Math.Sin(laRad)), Math.E));
            return y;
        }

        public static double Longitude2X(double longitude)
        {
            double lamda = Deg2Rad(longitude - center);  //弧度制经差
            double x = alpha * lamda;
            return x;
        }

        public static double Y2Latitude(double y)
        {
            double laRad = BisectionMethod(y, 10e-7);
            double latitude = Rad2Deg(laRad);
            return latitude;
        }

        public static double X2Longitude(double x)
        {
            double longitude = Rad2Deg(x / alpha) + center;
            return longitude;
        }

        /// <summary>
        /// 角度制转弧度制
        /// </summary>
        /// <param name="degree">角度</param>
        /// <returns>弧度</returns>
        static double Deg2Rad(double degree)
        {
            return degree / 180 * Math.PI;
        }

        /// <summary>
        /// 弧度制转角度制
        /// </summary>
        /// <param name="radian">弧度</param>
        /// <returns>角度</returns>
        static double Rad2Deg(double radian)
        {
            return radian * 180 / Math.PI;
        }

        /// <summary>
        /// 二分法解方程
        /// </summary>
        /// <param name="y0"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        static double BisectionMethod(double y0, double error)
        {
            double lower = 0;            //最小纬度0N
            double upper = Math.PI / 2;  //最大纬度90N
            double mid = -1;

            while ((upper - lower) > error)
            {
                mid = (lower + upper) / 2;
                if (F(mid, y0) * F(lower, y0) < 0)
                    upper = mid;
                else if (F(mid, y0) * F(upper, y0) < 0)
                    lower = mid;
                else
                    return mid;
            }
            return mid;
        }

        static double F(double x)  //x为弧度制
        {
            double y = 0.5 * a * (1 - e2) / e
                   * (e * Math.Sin(x) / (1 - e2 * Math.Sin(x) * Math.Sin(x))
                   + Math.Log((1 + e * Math.Sin(x)) / Math.Sqrt(1 - e2 * Math.Sin(x) * Math.Sin(x)), Math.E));
            return y;
        }

        static double F(double x, double y0)  //x为弧度制
        {
            double phi = 0.5 * a * (1 - e2) / e
                   * (e * Math.Sin(x) / (1 - e2 * Math.Sin(x) * Math.Sin(x))
                   + Math.Log((1 + e * Math.Sin(x)) / Math.Sqrt(1 - e2 * Math.Sin(x) * Math.Sin(x)), Math.E)) - y0;
            return phi;
        }
    }
}
