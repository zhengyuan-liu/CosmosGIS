using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using CosmosGIS.Geometry;
using CosmosGIS.Map;

namespace CosmosGIS.FileReader
{
    enum ShapeType
    {
        NullShape, Point, Polyline, Polygon, MultiPoint, PointZ, PolyLineZ, PolygonZ, MultiPointZ, PointM, PolyLineM, PolygonM, MultiPointM, MultiPatch
    }

    class Shapefile
    {
        #region 字段
        string fileName;
        string filePath;
        string attrFilePath;
        MySpaceDataType dataType;
        PointF centerPos;

        int fileCode;
        int fileLength;   //文件长度，单位：字节
        int version;      //版本号
        int shapeTypeID;  //几何类型
        ShapeType shapeType;
        double xmin, ymin, xmax, ymax, zmin, zmax, mmin, mmax;
        int byteCount = 0;  //已经读取的字节数
        List<MyPoint> points;
        List<MyMultiPolyline> polylines;
        List<MyMultiPolygon> polygons;
        #endregion

        #region Properties
        public string Name
        {
            get { return fileName; }
        }
        public string FilePath
        {
            get { return filePath; }
        }
        public string AttrFilePath
        { 
            get { return attrFilePath; } 
        }
        public MySpaceDataType DataType
        {
            get { return dataType; }
        }
        public double Xmin
        {
            get { return xmin; }
        }
        public double Ymin
        {
            get { return ymin; }
        }
        public double Xmax
        {
            get { return xmax; }
        }
        public double Ymax
        {
            get { return ymax; }
        }
        /// <summary>
        /// 中心经纬度坐标
        /// </summary>
        internal PointF CenterPos
        {
            get { return centerPos; }
        }
        #endregion

        #region Constructor
        public Shapefile(string filePath)
        {
            this.filePath = filePath;
            fileName = Path.GetFileNameWithoutExtension(filePath);
            attrFilePath = Path.GetDirectoryName(filePath) + "\\" + fileName + ".mdb";
            FileStream fs = new FileStream(filePath, FileMode.Open);
            BinaryReader br = new BinaryReader(fs);

            //读取文件头
            fileCode = ReverseByte(br.ReadInt32());
            for (int i = 1; i <= 5; i++)
                br.ReadInt32();
            fileLength = ReverseByte(br.ReadInt32()) * 2;
            version = br.ReadInt32();
            shapeTypeID = br.ReadInt32();
            xmin = br.ReadDouble();
            ymin = br.ReadDouble();
            xmax = br.ReadDouble();
            ymax = br.ReadDouble();
            zmin = br.ReadDouble();
            zmax = br.ReadDouble();
            mmin = br.ReadDouble();
            mmax = br.ReadDouble();
            byteCount += 100;

            float xcenter = ((float)xmin + (float)xmax) / 2;
            float ycenter = ((float)ymin + (float)ymax) / 2;
            centerPos = new PointF(xcenter, ycenter);

            switch (shapeTypeID)
            {
                case 1:
                    {
                        shapeType = ShapeType.Point;
                        ReadPointShp(br);
                        dataType = MySpaceDataType.MyPoint;
                        break;
                    }
                case 3:
                    {
                        shapeType = ShapeType.Polyline;
                        ReadPolyLineShp(br);
                        dataType = MySpaceDataType.MyPolyLine;
                        break;
                    }
                case 5:
                    {
                        shapeType = ShapeType.Polygon;
                        ReadPolygonShp(br);
                        dataType = MySpaceDataType.MyPolygon;
                        break;
                    }
                default:
                    {
                        shapeType = ShapeType.NullShape;
                        break;
                    }
            }
        }
        #endregion

        #region 私有函数
        private void ReadPointShp(BinaryReader br)
        {
            points = new List<MyPoint>();
            int recordNumber;  //记录号
            int contentLength;  //坐标记录长度
            int shapeType;  //几何类型
            double x, y;
            int id = 0;
            while (byteCount < fileLength)
            {
                recordNumber = ReverseByte(br.ReadInt32());
                contentLength = ReverseByte(br.ReadInt32());
                shapeType = br.ReadInt32();
                x = br.ReadDouble();
                y = br.ReadDouble();
                MyPoint p = new MyPoint(x, y, id);
                id++;
                points.Add(p);
                byteCount += 28;
            }
        }
        private void ReadPolyLineShp(BinaryReader br)
        {
            polylines = new List<MyMultiPolyline>();
            int id = 0;
            while (byteCount < fileLength)
            {
                MyMultiPolyline temp = new MyMultiPolyline(br,id);
                byteCount += temp.ByteCount;
                polylines.Add(temp);
                id++;
            }
        }
        private void ReadPolygonShp(BinaryReader br)
        {
            int id = 0;
            polygons = new List<MyMultiPolygon>();
            while (byteCount < fileLength)
            {
                MyMultiPolygon temp = new MyMultiPolygon(br, id);
                byteCount += temp.ByteCount;
                polygons.Add(temp);
                id++;
            }
        }
        static void ExchangeByte(ref byte b1, ref byte b2)
        {
            byte temp;
            temp = b1;
            b1 = b2;
            b2 = temp;
        }
        #endregion

        /// <summary>
        /// 大尾整数转小尾整数
        /// </summary>
        /// <param name="big">大尾整数</param>
        /// <returns>小尾整数</returns>
        public static int ReverseByte(int big)
        {
            byte[] bytes = BitConverter.GetBytes(big);
            ExchangeByte(ref bytes[0], ref bytes[3]);
            ExchangeByte(ref bytes[1], ref bytes[2]);
            int little = BitConverter.ToInt32(bytes, 0);
            return little;
        }

        public GeometryFeature GetGeometryFeature()
        {
            GeometryFeature geoFeature = null;
            switch (shapeType)
            {
                case ShapeType.Point:
                    geoFeature = new PointFeature(points, xmin, xmax, ymin, ymax);
                    break;
                case ShapeType.Polyline:
                    geoFeature = new PolylineFeature(polylines, xmin, xmax, ymin, ymax);
                    break;
                case ShapeType.Polygon:
                    geoFeature = new PolygonFeature(polygons, xmin, xmax, ymin, ymax);
                    break;
                default:
                    break;
            }
            return geoFeature;
        }
    }
}
