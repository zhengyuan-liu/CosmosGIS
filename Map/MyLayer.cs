using System;
using System.Drawing;
using System.Data;
using System.IO;
using System.Collections.Generic;
using CosmosGIS.Property;
using CosmosGIS.FileReader;
using CosmosGIS.Geometry;
using CosmosGIS.Grid;
using CosmosGIS.MyRenderer;
using CosmosGIS.MySymbol;

namespace CosmosGIS.Map
{
    class MyLayer
    {
        #region Members

        #region 渲染
        /// <summary>
        /// fid到Symbol的映射
        /// </summary>
        private Dictionary<int, Symbol> fid2Symbol;
        /// <summary>
        /// fid到注记文本的映射
        /// </summary>
        private Dictionary<int, string> fid2Text;

        /// <summary>
        /// //地图渲染类型
        /// </summary>
        internal RendererType rendertype;

        /// <summary>
        /// //地图渲染
        /// </summary>
        private Renderer renderer;

        internal Renderer Renderer
        {
            get { return renderer; }
            set 
            { 
                renderer = value;
                if (renderer.GetType() != typeof(SimpleRenderer))  //不是简单渲染
                {
                    string field = renderer.Field;
                    DataTable dt = spaceData.Property.SqlQuery("Select fid," + field + " from " + layerName);
                    fid2Symbol = new Dictionary<int, Symbol>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        int fid = int.Parse(dt.Rows[i].ItemArray[0].ToString());
                        string strValue = dt.Rows[i].ItemArray[1].ToString();
                        Symbol symbol = renderer.FindSymbol(strValue);
                        fid2Symbol.Add(fid, symbol);
                    }
                }

            }
        }

        /// <summary>
        /// //注记渲染
        /// </summary>
        private TextRenderer textrender;

        internal TextRenderer Textrender
        {
            get { return textrender; }
            set 
            {
                textrender = value;
                string field = textrender.Field;
                DataTable dt = spaceData.Property.SqlQuery("Select fid," + field + " from " + layerName);
                fid2Text = new Dictionary<int, string>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int fid = int.Parse(dt.Rows[i].ItemArray[0].ToString());
                    string strValue = dt.Rows[i].ItemArray[1].ToString();
                    fid2Text.Add(fid, strValue);
                }
            }
        }

        /// <summary>
        /// //是否存在注记渲染
        /// </summary>
        private bool hastext;
        /// <summary>
        /// 图层路径
        /// </summary>
        private string filepath;

        #endregion

        /// <summary>
        /// 图层中要素类型
        /// </summary>
        private MySpaceDataType dataType;

        /// <summary>
        /// 空间数据
        /// </summary>
        private MySpaceData spaceData;
                                                
        /// <summary>
        /// 图层名称
        /// </summary>
        private string layerName;
        /// <summary>
        /// 图层描述信息
        /// </summary>
        private string description;   //??

        /// <summary>
        /// 选中要素ID
        /// </summary>
        internal List<int> selectionIDs = new List<int>();

        #endregion

        #region Attributes
        /// <summary>
        /// 图层名称
        /// </summary>
        public string LayerName { get { return layerName; } }
        /// <summary>
        /// 图层描述信息
        /// </summary>
        public string Description { get { return description; }  }
        /// <summary>
        /// //图层是否可见
        /// </summary>
        public bool Visible { get; set; }
        /// <summary>
        /// 图层是否含有注记
        /// </summary>
        public bool HasText { get { return hastext; } set { hastext = value; } }
        /// <summary>
        /// 图层是否可以执行选择操作
        /// </summary>
        public bool Selectable { get; set; }

        internal void SelectByIds(List<int> ids)
        {
            selectionIDs.Clear();
            selectionIDs.AddRange(ids);
            spaceData.SelectByIds(selectionIDs);
        }

        /// <summary>
        /// 图层的空间数据类型
        /// </summary>
        public MySpaceDataType DataType { get { return dataType; } }
        /// <summary>
        /// 空间数据
        /// </summary>
        public MySpaceData SpaceData{get { return spaceData; }}
        /// <summary>
        /// 路径记录
        /// </summary>
        public string FilePath { get { return filepath; } set { filepath = value; } }
        #endregion

        #region Constructors
        /// <summary>
        /// 基于文件路径的构造函数
        /// </summary>
        internal MyLayer(string layerpath)
        {
            if (Path.GetExtension(layerpath) == ".shp")
            {
                Shapefile shp = new Shapefile(layerpath);
                layerName = shp.Name;
                dataType = shp.DataType;
                spaceData = new MySpaceData(shp);
                renderer = new SimpleRenderer(dataType);
                rendertype = RendererType.SimpleRenderer;
                hastext = false;
                Visible = true;
                filepath = shp.FilePath;
            }
            else
            {
                CosmosGisVector cgv = new CosmosGisVector(layerpath);
                layerName = cgv.Name;
                dataType = cgv.SpaceDataType;
                spaceData = new MySpaceData(cgv);
                renderer = new SimpleRenderer(dataType);
                rendertype = RendererType.SimpleRenderer;
                hastext = false;
                Visible = true;
                filepath = cgv.FilePath;
            }
            /*///TODO
            spaceData = new MySpaceData("");
            layerName = Path.GetFileNameWithoutExtension(fileName);
            dataType = spaceData.DataType;
            this.Visible = true;*/
        }
        /// <summary>
        /// 基于Shapefile的构造函数
        /// </summary>
        /// <param name="shp">Shapefile</param>
        internal MyLayer(Shapefile shp)
        {
            layerName = shp.Name;
            dataType = shp.DataType;
            spaceData = new MySpaceData(shp);
            renderer =new  SimpleRenderer(dataType);
            rendertype = RendererType.SimpleRenderer;
            hastext = false;
            Visible = true;
            filepath = shp.FilePath;
        }
        /// <summary>
        /// 基于CosmosGisVector的构造函数
        /// </summary>
        /// <param name="cgv">CosmosGisVector</param>
        internal MyLayer(CosmosGisVector cgv)
        {
            layerName = cgv.Name;
            dataType = cgv.SpaceDataType;
            spaceData = new MySpaceData(cgv);
            renderer = new SimpleRenderer(dataType);
            rendertype = RendererType.SimpleRenderer;
            hastext = false;
            Visible = true;
            filepath = cgv.FilePath;
        }
        internal MyLayer(MyGrid grid)
        {
            layerName = grid.Name;
            dataType = MySpaceDataType.MyGrid;
            spaceData = new MySpaceData(grid);
            Visible = true;
            filepath = grid.FilePath;
        }
        #endregion

        #region Methods
        internal MyProperty GetFeaturePorperties()
        {
            return spaceData.Property;
        }
        /// <summary>
        /// 绘制要素
        /// </summary>
        /// <param name="g">Graphics对象</param>
        /// <param name="bounds">画布大小</param>
        /// <param name="centerPos">中心经纬度</param>
        /// <param name="scale">比例尺</param>
        internal void DrawFeature(Graphics g, Rectangle bounds, PointF centerPos, double scale)
        {
            if (dataType != MySpaceDataType.MyGrid)
            {
                if (renderer.GetType() == typeof(SimpleRenderer))
                    spaceData.DrawFeature(g, bounds, centerPos, scale, renderer.FindSymbol(""));
                else
                    spaceData.DrawFeature(g, bounds, centerPos, scale, fid2Symbol);
                if (hastext == true)
                    spaceData.DrawLabel(g, bounds, centerPos, scale, fid2Text, textrender.TextSymol);
            }
            else
                spaceData.DrawRaster(g, bounds, centerPos, scale);
        }
        /// <summary>
        /// 矩形盒查询
        /// </summary>
        /// <param name="box">矩形盒</param>
        internal void SelectByBox(MyRectangle box)
        {
            selectionIDs.Clear();
            selectionIDs.AddRange(spaceData.SelectByBox(box));
        }
        /// <summary>
        /// 点选
        /// </summary>
        /// <param name="mouseLocation">鼠标点选位置</param>
        /// <param name="bounds">屏幕范围</param>
        /// <param name="centerPos">屏幕中心点经纬度</param>
        /// <param name="scale">比例尺</param>
        internal DataTable SelectByPoint(Point mouseLocation, Rectangle bounds, PointF centerPos, double scale)
        {
            DataTable dt = spaceData.SelectByPoint(mouseLocation, bounds, centerPos, scale);
            selectionIDs.Clear();
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                    selectionIDs.Add(int.Parse(dt.Rows[i][0].ToString()));
            }
            return dt;
        }
        /// <summary>
        /// 点选选中要素的顶点
        /// </summary>
        /// <param name="mouseLocation"></param>
        /// <param name="bounds"></param>
        /// <param name="centerPos"></param>
        /// <param name="scale"></param>
        internal void SelectVertex(Point mouseLocation, Rectangle bounds, PointF centerPos, double scale)
        {
            spaceData.SelectVertex(mouseLocation, bounds, centerPos, scale);
        }
        /// <summary>
        /// 移动选中节点至新的坐标(WGS84坐标系)
        /// </summary>
        /// <param name="newPos"></param>
        internal void MoveVertex(MyPoint newPos)
        {
            spaceData.MoveVertex(newPos);
        }
        /// <summary>
        /// 移动选中的要素
        /// </summary>
        /// <param name="deltaX"></param>
        /// <param name="deltaY"></param>
        /// <param name="bounds"></param>
        /// <param name="centerPos"></param>
        /// <param name="scale"></param>
        internal void MoveSelectedFeature(int deltaX, int deltaY, Rectangle bounds, PointF centerPos, double scale)
        {
            spaceData.MoveSelectedFeature(deltaX, deltaY, bounds, centerPos, scale);
        }
        /// <summary>
        /// 开始编辑
        /// </summary>
        internal void StartEditing()
        {
            spaceData.StartEditing();
        }
        /// <summary>
        /// 停止编辑
        /// </summary>
        internal void StopEditing()
        {
            spaceData.StopEditing();
        }
        /// <summary>
        /// 添加指定要素
        /// </summary>
        /// <param name="geo"></param>
        internal void AddFeature(object geo)
        {
            spaceData.AddFeature(geo);
        }
        /// <summary>
        /// 删除选中的要素
        /// </summary>
        internal void DeleteSelectedFeatures()
        {
            spaceData.DeleteSelectedFeatures();
        }
        /// <summary>
        /// 保存编辑
        /// </summary>
        internal void SaveEdit()
        {
            spaceData.SaveEdit();
        }
        #endregion
    }
}
