using System.IO;
using System.Collections.Generic;
using System.Drawing;
using CosmosGIS.Map;
using CosmosGIS.MySymbol;

namespace CosmosGIS.Geometry
{
    /// <summary>
    /// 空间对象类的基类,对应一个图层的空间数据
    /// </summary>
    abstract class GeometryFeature
    {
        #region Members
        /// <summary>
        /// 点选误差限
        /// </summary>
        protected double margin = 5;
        /// <summary>
        /// 最小X坐标
        /// </summary>
        protected double minX = double.MaxValue;
        /// <summary>
        /// 最大X坐标
        /// </summary>
        protected double maxX = double.MinValue;
        /// <summary>
        /// 最小Y坐标
        /// </summary>
        protected double minY = double.MaxValue;
        /// <summary>
        /// 最大Y坐标
        /// </summary>
        protected double maxY = double.MinValue;
        #endregion

        #region Attributes
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
        /// 最大FID，抽象属性
        /// </summary>
        public abstract int MaxFID { get; }
        #endregion

        #region Methods
        /// <summary>
        /// 获取空间对象数量
        /// </summary>
        /// <returns>空间对象数量</returns>
        abstract internal int GetGeoNum();

        /// <summary>
        /// 添加指定空间对象
        /// </summary>
        /// <param name="geo">指定空间对象</param>
        /// <returns></returns>
        internal abstract bool AddGeoObj(object geo);

        /// <summary>
        /// 删除选中的要素
        /// </summary>
        /// <returns></returns>
        internal abstract List<int> DeleteSelectedFeatures();

        /// <summary>
        /// 移动选中的要素
        /// </summary>
        /// <param name="deltaX">X方向移动量，屏幕坐标系</param>
        /// <param name="deltaY">y方向移动量，屏幕坐标系</param>
        /// <param name="bounds"></param>
        /// <param name="centerPos"></param>
        /// <param name="scale"></param>
        internal abstract void MoveSelectedFeature(int deltaX, int deltaY, Rectangle bounds, PointF centerPos, double scale);

        /// <summary>
        /// 以CGV格式向文件中保存空间数据
        /// </summary>
        /// <param name="filePath">文件路径</param>
        internal abstract void SaveSpaceData(string filePath);

        /// <summary>
        /// 简单渲染的绘制要素
        /// </summary>
        /// <param name="g"></param>
        /// <param name="bounds"></param>
        /// <param name="centerPos"></param>
        /// <param name="scale"></param>
        /// <param name="symbol"></param>
        internal abstract void DrawSpaceData(Graphics g, Rectangle bounds, PointF centerPos, double scale, Symbol symbol);

        /// <summary>
        /// 唯一值渲染和分级渲染时的绘制要素
        /// </summary>
        /// <param name="g"></param>
        /// <param name="bounds"></param>
        /// <param name="centerPos"></param>
        /// <param name="scale"></param>
        /// <param name="fid2Symbol"></param>
        internal abstract void DrawSpaceData(Graphics g, Rectangle bounds, PointF centerPos, double scale, Dictionary<int, Symbol> fid2Symbol);
        /// <summary>
        /// 画注记
        /// </summary>
        /// <param name="g"></param>
        /// <param name="bounds"></param>
        /// <param name="centerPos"></param>
        /// <param name="scale"></param>
        /// <param name="fid2Symbol"></param>
        internal abstract void DrawLabel(Graphics g, Rectangle bounds, PointF centerPos, double scale, Dictionary<int, string> fid2Text, TextSymbol textSymbol);
        /// <summary>
        /// 根据矩形盒选择要素
        /// </summary>
        /// <param name="box">矩形盒</param>
        internal abstract List<int> SelectByBox(MyRectangle box);
        /// <summary>
        /// 点选
        /// </summary>
        /// <param name="mouseLocation">鼠标点选位置</param>
        /// <param name="bounds"></param>
        /// <param name="centerPos"></param>
        /// <param name="scale"></param>
        /// <returns>被选中要素FID的List</returns>
        internal abstract List<int> SelectByPoint(Point mouseLocation, Rectangle bounds, PointF centerPos, double scale);
        /// <summary>
        /// 通过fid选择要素
        /// </summary>
        /// <param name="fids"></param>
        internal abstract void SelectByFids(int[] fids);
        internal abstract void ClearSelection();
        /// <summary>
        /// 点选选中要素的顶点，此方法对点要素图层无效
        /// </summary>
        /// <param name="mouseLocation"></param>
        /// <param name="bounds"></param>
        /// <param name="centerPos"></param>
        /// <param name="scale"></param>
        internal abstract void SelectVertex(Point mouseLocation, Rectangle bounds, PointF centerPos, double scale);
        /// <summary>
        /// 移动选中顶点至新的位置(WGS84坐标系)，此方法对点要素图层无效
        /// </summary>
        /// <param name="newPos"></param>
        internal abstract void MoveVertex(MyPoint newPos);
        /// <summary>
        /// 开始编辑
        /// </summary>
        internal abstract void StartEditing();
        /// <summary>
        /// 取消编辑
        /// </summary>
        internal abstract void CancelEdit();
        /// <summary>
        /// 保存编辑
        /// </summary>
        internal abstract void SaveEdit(string path);
        #endregion
    }
}
