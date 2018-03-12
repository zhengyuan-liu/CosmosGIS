using System;
using System.Collections.Generic;
using System.Drawing;
using CosmosGIS.Geometry;
using CosmosGIS.Property;
using CosmosGIS.FileReader;
using System.Data;

namespace CosmosGIS.Map
{
    /// <summary>
    /// //地图对象
    /// </summary>
    class MyMap
    {
        #region Members

        /// <summary>
        /// //该地图集的坐标系统
        /// </summary>
        private MyCoordinates coordinates;

        /// <summary>
        /// //该地图集和图层集合
        /// </summary>
        private List<MyLayer> mapLayers = new List<MyLayer>();
        /// <summary>
        /// //该地图集的比例尺
        /// </summary>
        private double mapScale = 1;

        /// <summary>
        /// //地图的basemap
        /// </summary>
        private Bitmap baseMap = null;

        #endregion

        #region Properties
        /// <summary>
        /// //该地图集的坐标系统
        /// </summary>
        private MyCoordinates GeoCoordinate = MyCoordinates.WGS1984;
        /// <summary>
        /// 图层数目
        /// </summary>
        public int LayerNum { get { return mapLayers.Count; } }
        /// <summary>
        /// 地图集的名称
        /// </summary>
        public string MapName { get; set; }
        /// <summary>
        /// 图层
        /// </summary>
        internal List<MyLayer> Layers
        {
            get { return mapLayers; }
            set { mapLayers = value; }
        }
        /// <summary>
        /// 地图是否有basemap
        /// </summary>
        public bool hasBaseMap {
            get {
                if (baseMap == null)
                    return false;
                else
                    return true;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// //构造函数
        /// </summary>
        internal MyMap()
        {
            MapName = "NewDataFrame";
        }
        internal MyMap(string sname, MyCoordinates scoordinates, double smyscale)
        {
            MapName = sname;
            coordinates = scoordinates;
            mapScale = smyscale;
        }
        #endregion

        #region Methods
        /// <summary>
        /// 改变地图指定图层的显示与隐藏
        /// </summary>
        /// <param name="index">指定索引号</param>
        internal void ChangeSelectedLayerVisible(int index)
        {
            mapLayers[index].Visible = !mapLayers[index].Visible;
        }
        /// <summary>
        /// 获取地图指定图层的名称
        /// </summary>
        /// <param name="i">指定索引号</param>
        /// <returns>指定地图指定图层的名称</returns>
        internal string GetLayerName(int i)
        {
            return mapLayers[i].LayerName;
        }
        /// <summary>
        /// 获取地图指定图层的空间数据类型
        /// </summary>
        /// <param name="index">指定索引号</param>
        /// <returns>指定地图指定图层的空间数据类型</returns>
        internal MySpaceDataType GetSpaceDataType(int index)
        {
            return mapLayers[index].DataType;
        }
        /// <summary>
        /// 更改图层顺序
        /// </summary>
        /// <param name="index1"></param>
        /// <param name="index2"></param>
        internal void ChangeLayerIndex(int index1, int index2)
        {
            MyLayer tpLayer = mapLayers[index1];
            mapLayers[index1] = mapLayers[index2];
            mapLayers[index2] = tpLayer;
        }

        internal void SelectByIds(List<int> ids,int index)
        {
            mapLayers[index].SelectByIds(ids);
        }
        /// <summary>
        /// 增加图层
        /// </summary>
        internal void AddLayer(MyLayer layer)
        {
            mapLayers.Insert(0, layer);
        }
        /// <summary>
        /// 删除图层
        /// </summary>
        /// <param name="index"></param>
        internal void Deletelayer(int index)
        {
            mapLayers.RemoveAt(index);
        }
        /// <summary>
        /// 画图
        /// </summary>
        /// <param name="g">Graphics对象</param>
        /// <param name="bounds">画布大小</param>
        /// <param name="centerPos">中心经纬度</param>
        /// <param name="scale">比例尺</param>
        internal void DrawMap(Graphics g, Rectangle bounds, PointF centerPos, double scale)
        {
            for (int i = mapLayers.Count - 1; i >= 0; i--)
                if (mapLayers[i].Visible)
                    mapLayers[i].DrawFeature(g, bounds, centerPos, scale);
        }
        /// <summary>
        /// 矩形盒选择
        /// </summary>
        /// <param name="box">矩形盒</param>
        internal void SelectByBox(MyRectangle box)
        {
            for (int i = 0; i < mapLayers.Count; i++)
                if (mapLayers[i].Visible==true)
                    mapLayers[i].SelectByBox(box);
        }
        /// <summary>
        /// 在所有图层实施点选
        /// </summary>
        /// <param name="mouseLocation"></param>
        /// <param name="bounds"></param>
        /// <param name="centerPos"></param>
        /// <param name="scale"></param>
        internal void SelectByPoint(Point mouseLocation, Rectangle bounds, PointF centerPos, double scale)
        {
            for (int i = 0; i < mapLayers.Count; i++)
                if (mapLayers[i].Visible == true)
                    mapLayers[i].SelectByPoint(mouseLocation, bounds, centerPos, scale);
        }
        /// <summary>
        /// 在指定索引号的图层添加要素
        /// </summary>
        /// <param name="geo">待添加要素</param>
        /// <param name="index">指定图层的索引号</param>
        internal void AddFeature(object geo,int index)
        {
            mapLayers[index].AddFeature(geo);
        }
        /// <summary>
        /// 获取地图的最小外包矩形
        /// </summary>
        /// <returns></returns>
        internal MyRectangle GetMBR()
        {
            MyRectangle mbr = mapLayers[0].SpaceData.GetMBR();  //无图层时会报错
            for (int i = 1; i < LayerNum; i++)
                mbr = GeometryTools.GetMBR(mbr, mapLayers[i].SpaceData.GetMBR());
            return mbr;
        }
        #endregion
    }
}
