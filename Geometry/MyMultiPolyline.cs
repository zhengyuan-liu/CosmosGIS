using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using CosmosGIS.FileReader;

namespace CosmosGIS.Geometry
{
    /// <summary>
    /// 单个复合折线
    /// </summary>
    class MyMultiPolyline
    {
        #region Member
        /// <summary>
        /// 记录所有坐标点的List
        /// </summary>
        private List<MyPoint> points = new List<MyPoint>();
        /// <summary>
        /// 此MultiPolyline包含的子Polyline的个数
        /// </summary>
        int polylineCount;
        /// <summary>
        /// 每个子Polyline的第一个坐标点在points中的位置
        /// </summary>
        public int[] firstIndex;
        /// <summary>
        /// 该要素是否被选中
        /// </summary>
        private bool selected = false;
        /// <summary>
        /// 要素ID，绑定属性数据与空间数据用
        /// </summary>
        private int fid;
        /// <summary>
        /// 最小X坐标
        /// </summary>
        double minX = double.MaxValue;
        /// <summary>
        /// 最大X坐标
        /// </summary>
        double maxX = double.MinValue;
        /// <summary>
        /// 最小Y坐标
        /// </summary>
        double minY = double.MaxValue;
        /// <summary>
        /// 最大Y坐标
        /// </summary>
        double maxY = double.MinValue;

        #region Shapefile读取相关字段
        int numPoints;  // 当前线目标所包含的顶点个数
        int byteCount = 0;
        #endregion

        #endregion

        #region 属性
        /// <summary>
        /// 顶点集合
        /// </summary>
        public List<MyPoint> Points
        {
            get { return points; }
            set { points = value; }
        }
        /// <summary>
        /// 顶点个数
        /// </summary>
        public int PointCount
        {
            get { return points.Count; }
        }
        /// <summary>
        /// 要素ID
        /// </summary>
        public int FID
        {
            get { return fid; }
            set { fid = value; }
        }
        /// <summary>
        /// 该要素是否被选中
        /// </summary>
        public bool Selected
        {
            get { return selected; }
            set { selected = value; }
        }
        /// <summary>
        /// 最小X坐标
        /// </summary>
        public double MinX { get { return minX; } }
        /// <summary>
        /// 最大X坐标
        /// </summary>
        public double MaxX { get { return maxX; } }
        /// <summary>
        /// 最小Y坐标
        /// </summary>
        public double MinY { get { return minY; } }
        /// <summary>
        /// 最大Y坐标
        /// </summary>
        public double MaxY { get { return maxY; } }
        /// <summary>
        /// 字节数
        /// </summary>
        public int ByteCount
        {
            get { return this.byteCount; }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public MyMultiPolyline() { }

        /// <summary>
        /// 基于点集的构造函数
        /// </summary>
        /// <param name="points"></param>
        public MyMultiPolyline(List<MyPoint> points) 
        {
            for (int i = 0; i < points.Count; i++)
            {               
                double x = points[i].X;
                double y = points[i].Y;
                this.points.Add(new MyPoint(x, y));
                if (x < minX)
                    minX = x;
                if (x > maxX)
                    maxX = x;
                if (y < minY)
                    minY = y;
                if (y > maxY)
                    maxY = y;
            }
            this.polylineCount = 1;
            firstIndex = new int[1];
            firstIndex[0] = 0;
        }

        /// <summary>
        /// 基于二进制流(Shapefile)的构造函数
        /// </summary>
        /// <param name="br">二进制流(Shapefile)</param>
        public MyMultiPolyline(BinaryReader br,int id)
        {
            this.fid = id;

            int recordNumber;
            int contentLength;  //坐标记录长度
            int shapeType;  //几何类型

            //读取记录头
            recordNumber = Shapefile.ReverseByte(br.ReadInt32());
            contentLength = Shapefile.ReverseByte(br.ReadInt32());
            byteCount += 8;
            //读取记录内容
            shapeType = br.ReadInt32();
            minX = br.ReadDouble();
            minY = br.ReadDouble();
            maxX = br.ReadDouble();
            maxY = br.ReadDouble();
            polylineCount = br.ReadInt32();
            numPoints = br.ReadInt32();
            byteCount += 44;
            firstIndex = new int[polylineCount];
            for (int i = 0; i < polylineCount; i++)
                firstIndex[i] = br.ReadInt32();
            byteCount += 4 * polylineCount;
            double x, y;
            for (int i = 0; i < numPoints; i++)
            {
                x = br.ReadDouble();
                y = br.ReadDouble();
                MyPoint p = new MyPoint(x, y);
                points.Add(p);
            }
            byteCount += 16 * numPoints;
        }
        /// <summary>
        /// 基于文本流(CosmosGisVector文件)的构造函数
        /// </summary>
        /// <param name="sr">文本流(CosmosGisVector文件)</param>
        public MyMultiPolyline(StreamReader sr)
        {  
            string strLine;
            string[] data;
            strLine = sr.ReadLine();
            data = strLine.Split(' ');
            fid = Int32.Parse(data[0]);
            polylineCount = Int32.Parse(data[1]);
            firstIndex = new int[polylineCount];
            int count = 0;
            //循环读取子线段
            for (int i = 0; i < polylineCount; i++)
            {
                firstIndex[i] = count;
                strLine = sr.ReadLine();
                data = strLine.Split(' ');
                int n = Int32.Parse(data[0]);  //子线段中点的个数
                count += n;
                //循环读取子线段上的点
                for (int j = 0; j < n; j++)
                {
                    double x = Double.Parse(data[2 * j + 1]);
                    double y = Double.Parse(data[2 * j + 2]);
                    points.Add(new MyPoint(x, y));
                    if (x < minX)
                        minX = x;
                    if (x > maxX)
                        maxX = x;
                    if (y < minY)
                        minY = y;
                    if (y > maxY)
                        maxY = y;
                }
            }
        }
        #endregion

        #region 方法
        public void WriteCgv(StreamWriter sw)
        {
            sw.WriteLine(fid.ToString() + " " + polylineCount.ToString());
            List<int> temp = firstIndex.ToList();
            temp.Add(PointCount);
            //循环写入子线段信息
            for (int i = 0; i < polylineCount; i++)
            {
                int n = temp[i + 1] - temp[i];  //子线段中点的个数
                string strLine = n.ToString();
                for (int j = 0; j < n; j++)
                    strLine += " " + points[j + temp[i]].X.ToString() + " " + points[j + temp[i]].Y.ToString();
                sw.WriteLine(strLine);
            }
        }
        /// <summary>
        /// 返回一个副本
        /// </summary>
        /// <returns></returns>
        public MyMultiPolyline Clone()
        {
            MyMultiPolyline copy = new MyMultiPolyline();
            copy.polylineCount = this.polylineCount;
            copy.selected = this.selected;
            for (int i = 0; i < this.points.Count; i++)
                copy.points.Add(new MyPoint(this.points[i].X, this.points[i].Y));
            copy.firstIndex = new int[this.firstIndex.Length];
            for (int i = 0; i < this.firstIndex.Length; i++)
                copy.firstIndex[i] = this.firstIndex[i];
            return copy;
        }
        #endregion
    }
}
