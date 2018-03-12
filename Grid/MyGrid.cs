using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using CosmosGIS.Projection;
using CosmosGIS.Geometry;

namespace CosmosGIS.Grid
{
    /// <summary>
    /// //栅格空间数据对象
    /// </summary>
    class MyGrid
    {
        #region 字段

        private string name;
        /// <summary>
        /// 栅格文件路径
        /// </summary>
        private string filePath;
        /// <summary>
        /// 栅格配置文件路径
        /// </summary>
        string configFilePath;

        Bitmap mapBmp;

        /// <summary>
        /// 最小X坐标
        /// </summary>
        double minX;
        /// <summary>
        /// 最大X坐标
        /// </summary>
        double maxX;
        /// 最小Y坐标
        /// </summary>
        double minY;
        /// <summary>
        /// 最大Y坐标
        /// </summary>
        double maxY;

        #endregion

        #region 属性
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        /// <summary>
        /// 最小横坐标
        /// </summary>
        public double MinX { get { return minX; } }
        /// <summary>
        /// 最大横坐标
        /// </summary>
        public double MaxX { get { return maxX; } }
        /// <summary>
        /// 最小纵坐标
        /// </summary>
        public double MinY { get { return minY; } }
        /// <summary>
        /// 最大纵坐标
        /// </summary>
        public double MaxY { get { return maxY; } }
        /// <summary>
        /// 栅格文件路径
        /// </summary>
        public string FilePath
        {
            get { return filePath; }
            set { filePath = value; }
        }
        #endregion

        #region Constructor
        public MyGrid(string path)
        {
            this.filePath = path;
            this.mapBmp = new Bitmap(path);
            this.name = Path.GetFileNameWithoutExtension(path);
            this.configFilePath = Path.GetDirectoryName(path) + "\\" + name + ".gbd";
            try
            {
                using (StreamReader sr = new StreamReader(new FileStream(configFilePath, FileMode.Open)))
                {
                    string strLine = sr.ReadLine();
                    string[] data = strLine.Split(' ');
                    this.minX = double.Parse(data[0]);
                    this.maxX = double.Parse(data[1]);
                    this.minY = double.Parse(data[2]);
                    this.maxY = double.Parse(data[3]);
                }
            }
            catch
            {
                MessageBox.Show("丢失配置文件或配置文件格式不正确！");
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// 绘制栅格数据
        /// </summary>
        /// <param name="g"></param>
        /// <param name="bounds"></param>
        /// <param name="centerPos"></param>
        /// <param name="scale"></param>
        internal void DrawSpaceData(Graphics g, Rectangle bounds, PointF centerPos, double scale)
        {
            double x1 = ETCProjection.Longitude2X(minX);
            double x2 = ETCProjection.Longitude2X(maxX);
            double y1 = ETCProjection.Latitude2Y(minY);
            double y2 = ETCProjection.Latitude2Y(maxY);

            double width = (x2 - x1) / scale;
            double height = (y2 - y1) / scale;

            PointF centerXY = ETCProjection.LngLat2XY(centerPos);

            float dx = (centerXY.X - (float)x1) / (float)scale;
            float dy = (centerXY.Y - (float)y2) / (float)scale;
            RectangleF boundary = new RectangleF((float)bounds.Width * 0.5F - dx, (float)bounds.Height * 0.5F + dy, (float)width, (float)height);
            g.DrawImage(mapBmp, boundary);
        }
        #endregion
    }
}
