using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.OleDb;
using ADOX;
using CosmosGIS.Geometry;
using CosmosGIS.Map;

namespace CosmosGIS.FileReader
{
    class CosmosGisVector
    {
        #region 字段
        /// <summary>
        /// CGV文件完整路径
        /// </summary>
        string filePath;
        /// <summary>
        /// 属性文件路径
        /// </summary>
        string attrFilePath;
        /// <summary>
        /// 空间数据类型
        /// </summary>
        MySpaceDataType spaceDataType;
        int spaceDataTypeID;
        double xmin;
        double xmax;
        double ymin;
        double ymax;
        /// <summary>
        /// 要素个数
        /// </summary>
        int count;

        List<MyPoint> points;
        List<MyMultiPolyline> polylines;
        List<MyMultiPolygon> polygons;
        #endregion

        #region 属性
        /// <summary>
        /// 要素名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 文件完整路径
        /// </summary>
        public string FilePath
        {
            get { return filePath; }
            set { filePath = value; }
        }
        /// <summary>
        /// 属性文件路径
        /// </summary>
        public string AttrFilePath
        {
            get { return attrFilePath; }
        }
        public double MinX
        {
            get { return xmin; }
        }
        public double MinY
        {
            get { return ymin; }
        }
        public double MaxX
        {
            get { return xmax; }
        }
        public double MaxY
        {
            get { return ymax; }
        }
        /// <summary>
        /// 要素个数
        /// </summary>
        public double FeatureCount
        {
            get { return count; }
        }
        /// <summary>
        /// 要素类型
        /// </summary>
        public MySpaceDataType SpaceDataType
        {
            get { return spaceDataType; }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// 基于文件路径的构造函数
        /// </summary>
        /// <param name="path">文件路径</param>
        public CosmosGisVector(string path)
        {
            if (Path.GetExtension(path) != ".cgv")
                throw new IOException("文件拓展名不正确！");
            this.filePath = path;
            this.Name = Path.GetFileNameWithoutExtension(path);
            this.attrFilePath = Path.GetDirectoryName(path) + "\\" + Name + ".mdb";
            using (StreamReader sr = new StreamReader(new FileStream(path, FileMode.Open)))
            {
                string strLine = sr.ReadLine();  //读文件头
                string[] data = strLine.Split(' ');
                spaceDataTypeID = int.Parse(data[0]);
                xmin = Double.Parse(data[1]);
                xmax = Double.Parse(data[2]);
                ymin = Double.Parse(data[3]);
                ymax = Double.Parse(data[4]);
                count = int.Parse(data[5]);
                switch (spaceDataTypeID)
                {
                    case 0:
                        spaceDataType = MySpaceDataType.MyPoint;
                        ReadPointCgv(sr);
                        break;
                    case 1:
                        spaceDataType = MySpaceDataType.MyPolyLine;
                        ReadMultiPolylineCgv(sr);
                        break;
                    case 2:
                        spaceDataType = MySpaceDataType.MyPolygon;
                        ReadMultiPolygonCgv(sr);
                        break;
                }
            }
        }
        #endregion

        #region 私有函数
        static private int GetSpaceDataTypeID(MySpaceDataType spaceDataType)
        {
            int id = -1;
            switch (spaceDataType)
            {
                case MySpaceDataType.MyPoint:
                    id = 0;
                    break;
                case MySpaceDataType.MyPolyLine:
                    id = 1;
                    break;
                case MySpaceDataType.MyPolygon:
                    id = 2;
                    break;
            }
            return id;
        }
        private void ReadPointCgv(StreamReader sr)
        {
            string strLine;
            string[] data;
            points = new List<MyPoint>();
            for (int i = 0; i < count; i++)
            {
                strLine = sr.ReadLine();
                data = strLine.Split(' ');
                int id = int.Parse(data[0]);
                double x = Double.Parse(data[1]);
                double y = Double.Parse(data[2]);
                points.Add(new MyPoint(x, y, id));
            }
        }
        private void ReadMultiPolylineCgv(StreamReader sr)
        {
            polylines = new List<MyMultiPolyline>();
            for (int i = 0; i < count; i++)
                polylines.Add(new MyMultiPolyline(sr));
        }

        private void ReadMultiPolygonCgv(StreamReader sr)
        {
            polygons = new List<MyMultiPolygon>();
            for (int i = 0; i < count; i++)
                polygons.Add(new MyMultiPolygon(sr));
        }
        private void WritePointCgv(StreamWriter sw)
        {
            string strLine;
            for (int i = 0; i < count; i++)
            {
                strLine = points[i].FID.ToString() + " " + points[i].X.ToString() + " " + points[i].Y.ToString();
                sw.WriteLine(strLine);
            }
        }
        private void WriteMultiPolylineCgv(StreamWriter sw)
        {
            for (int i = 0; i < count; i++)
                polylines[i].WriteCgv(sw);
        }

        private void WriteMultiPolygonCgv(StreamWriter sw)
        {
            for (int i = 0; i < count; i++)
                polygons[i].WriteCgv(sw);
        }
        #endregion

        #region 方法
        public void Save(string saveFilePath)
        {
            using (StreamWriter sw = new StreamWriter(saveFilePath, false))
            {
                string title = spaceDataTypeID.ToString() + " " + xmin.ToString() + " " + xmax.ToString() 
                              + " " + ymin.ToString() + " " + ymax.ToString() + " " + count.ToString();  //文件头
                sw.WriteLine(title);
                switch(spaceDataTypeID)
                {
                    case 0:
                        WritePointCgv(sw);
                        break;
                    case 1:
                        WriteMultiPolylineCgv(sw);
                        break;
                    case 2:
                        WriteMultiPolygonCgv(sw);
                        break;
                }
            }
        }
        /// <summary>
        /// 在指定路径下创建指定类型的空白CGV文件
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="spaceDataType">要素类型</param>
        public static void CreateEmptyCgv(string path, MySpaceDataType spaceDataType)
        {
            using (StreamWriter sw = new StreamWriter(path, false))
            {
                string title = GetSpaceDataTypeID(spaceDataType).ToString() + " 10000000 -10000000 10000000 -10000000 0";  //文件头
                sw.WriteLine(title);
            }
            string mdbFilePath = Path.GetDirectoryName(path) + "\\" + Path.GetFileNameWithoutExtension(path) + ".mdb";
            ADOX.Catalog catalog = new Catalog();
            catalog.Create(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + mdbFilePath);  //此语句在数据库已经存在时会报错，待完善

            string connectString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + mdbFilePath;
            OleDbConnection cnn = new OleDbConnection(connectString);
            cnn.Open();
            OleDbCommand command = new OleDbCommand("Create Table " + Path.GetFileNameWithoutExtension(path) + "(FID integer primary key);", cnn);
            command.ExecuteNonQuery();
            cnn.Close();
            cnn.Dispose();
        }

        public GeometryFeature GetGeometryFeature()
        {
            GeometryFeature geoFeature = null;
            switch (spaceDataType)
            {
                case MySpaceDataType.MyPoint:
                    geoFeature = new PointFeature(points, xmin, xmax, ymin, ymax);
                    break;
                case MySpaceDataType.MyPolyLine:
                    geoFeature = new PolylineFeature(polylines, xmin, xmax, ymin, ymax);
                    break;
                case MySpaceDataType.MyPolygon:
                    geoFeature = new PolygonFeature(polygons, xmin, xmax, ymin, ymax);
                    break;
                default:
                    break;
            }
            return geoFeature;
        }
        #endregion
    }
}
