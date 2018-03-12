namespace CosmosGIS.Geometry
{
    /// <summary>
    /// 点
    /// </summary>
    class MyPoint
    {
        #region Members
        /// <summary>
        /// //点的横纵坐标
        /// </summary>
        private double x, y;
        /// <summary>
        /// 该要素是否被选中
        /// </summary>
        private bool selected = false;
        /// <summary>
        /// 要素ID，绑定属性数据与空间数据用
        /// </summary>
        private int fid;
        #endregion

        #region 属性
        public double X
        {
            get { return x; }
            set { x = value; }
        }
        public double Y
        {
            get { return y; }
            set { y = value; }
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
        #endregion

        #region Constructors

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public MyPoint() { }

        /// <summary>
        /// 基于横纵坐标的构造函数
        /// </summary>
        /// <param name="x">横坐标</param>
        /// <param name="y">纵坐标</param>
        public MyPoint(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// 基于XY坐标和id的构造函数，用于点要素图层
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="id"></param>
        public MyPoint(double x, double y,int id)
        {
            this.x = x;
            this.y = y;
            this.fid = id;
        }
        #endregion
    }
}
