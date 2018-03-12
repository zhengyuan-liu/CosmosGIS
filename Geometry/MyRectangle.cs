namespace CosmosGIS.Geometry
{
    /// <summary>
    /// 矩形
    /// </summary>
    class MyRectangle
    {
        #region 属性
        /// <summary>
        /// 最小X坐标
        /// </summary>
        internal double MinX { get; set; }
        /// <summary>
        /// /最大X坐标
        /// </summary>
        internal double MaxX { get; set; }
        /// <summary>
        /// 最小Y坐标
        /// </summary>
        internal double MinY { get; set; }
        /// <summary>
        /// 最大Y坐标
        /// </summary>
        internal double MaxY { get; set; }
        /// <summary>
        /// 矩形的宽度
        /// </summary>
        internal double Width { get; set; }
        /// <summary>
        /// 矩形的高度
        /// </summary>
        internal double Height { get; set; }
        #endregion

        #region Constuctors
        /// <summary>
        /// 基于坐标范围的构造函数
        /// </summary>
        public MyRectangle(double minX, double maxX, double minY, double maxY)
        {
            this.MinX = minX;
            this.MaxX = maxX;
            this.MinY = minY;
            this.MaxY = maxY;
            Width = maxX - minX;
            Height = maxY - minY;
        }
        #endregion
    }
}
