using System;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using System.Data.OleDb;
using System.IO;
using CosmosGIS.Geometry;
using CosmosGIS.Grid;
using CosmosGIS.Property;
using CosmosGIS.FileReader;
using CosmosGIS.MySymbol;

namespace CosmosGIS.Map
{
    public class MySpaceData
    {
        #region Members
        /// <summary>
        /// 数据名称
        /// </summary>
        private string dataName;
        /// <summary>
        /// 空间数据文件路径
        /// </summary>
        private string filePath;
        /// <summary>
        /// 栅格数据
        /// </summary>
        private MyGrid myGrid = null;
        /// <summary>
        /// 矢量数据
        /// </summary>
        private GeometryFeature geoFeature = null;
        /// <summary>
        /// 空间数据类型
        /// </summary>
        private MySpaceDataType dataType;
        /// <summary>
        /// 属性数据
        /// </summary>
        private MyProperty property;
        #endregion

        #region Properties
        /// <summary>
        /// //获取空间对象数据类型
        /// </summary>
        public MySpaceDataType DataType { get { return dataType; } }
        /// <summary>
        /// //获取空间数据名称
        /// </summary>
        public string DataName { get { return dataName; } }
        /// <summary>
        /// 属性数据
        /// </summary>
        internal MyProperty Property { get { return property; } }
        /// <summary>
        /// 矢量数据
        /// </summary>
        internal MyRectangle Bound 
        { 
            get 
            {
                if (geoFeature != null)
                    return new MyRectangle(geoFeature.MinX, geoFeature.MaxX, geoFeature.MinY, geoFeature.MaxY);
                else if (myGrid != null)
                    return new MyRectangle(myGrid.MinX, myGrid.MaxX, myGrid.MinY, myGrid.MaxY);
                else
                    return new MyRectangle(0, 0, 0, 0);
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// 基于Shapefile的构造函数
        /// </summary>
        /// <param name="shp"></param>
        internal MySpaceData(Shapefile shp)
        {
            filePath = shp.AttrFilePath;
            dataName = shp.Name;
            dataType = shp.DataType;
            geoFeature = shp.GetGeometryFeature();
            property = new MyProperty(shp.AttrFilePath);
        }
        /// <summary>
        /// 基于CosmosGisVector的构造函数
        /// </summary>
        /// <param name="cgv"></param>
        internal MySpaceData(CosmosGisVector cgv)
        {
            filePath = cgv.AttrFilePath;
            dataName = cgv.Name;
            dataType = cgv.SpaceDataType;
            geoFeature = cgv.GetGeometryFeature();
            property = new MyProperty(cgv.AttrFilePath);
        }
        /// <summary>
        /// 基于栅格数据的构造函数
        /// </summary>
        /// <param name="grid"></param>
        internal MySpaceData(MyGrid grid)
        {
            myGrid = grid;
            dataName = grid.Name;
            dataType = MySpaceDataType.MyGrid;
        }
        #endregion

        #region Methods
        /// <summary>
        /// 绘制要素数据
        /// </summary>
        /// <param name="g">Graphics对象</param>
        /// <param name="bounds">画布大小</param>
        /// <param name="centerPos">中心经纬度</param>
        /// <param name="scale">比例尺</param>
        internal void DrawFeature(Graphics g, Rectangle bounds, PointF centerPos, double scale, Symbol symbol)
        {
            if (geoFeature != null)
                geoFeature.DrawSpaceData(g, bounds, centerPos, scale, symbol);        
        }
        /// <summary>
        /// 绘制栅格数据
        /// </summary>
        /// <param name="g"></param>
        /// <param name="bounds"></param>
        /// <param name="centerPos"></param>
        /// <param name="scale"></param>
        internal void DrawRaster(Graphics g, Rectangle bounds, PointF centerPos, double scale)
        {
            if (myGrid != null)
                myGrid.DrawSpaceData(g, bounds, centerPos, scale);
        }
        internal void SelectByIds(List<int> selectionIDs)
        {
            if (geoFeature != null)
                geoFeature.SelectByFids(selectionIDs.ToArray());
        }

        internal void DrawLabel(Graphics g, Rectangle bounds, PointF centerPos, double scale,Dictionary<int,string> fid2Text, TextSymbol textSymbol)
        {
            if (geoFeature != null)
                geoFeature.DrawLabel(g, bounds, centerPos, scale, fid2Text, textSymbol);
        }
        /// <summary>
        /// 绘制要素
        /// </summary>
        /// <param name="g">Graphics对象</param>
        /// <param name="bounds">画布大小</param>
        /// <param name="centerPos">中心经纬度</param>
        /// <param name="scale">比例尺</param>
        internal void DrawFeature(Graphics g, Rectangle bounds, PointF centerPos, double scale, Dictionary<int,Symbol> fid2Symbol)
        {
            if (geoFeature != null)
                geoFeature.DrawSpaceData(g, bounds, centerPos, scale, fid2Symbol);
        }
        internal List<int> SelectByBox(MyRectangle box)
        {
            if (geoFeature != null)
                return geoFeature.SelectByBox(box);
            else
                return new List<int>();
        }
        /// <summary>
        /// 点选
        /// </summary>
        /// <param name="mouseLocation"></param>
        /// <param name="bounds"></param>
        /// <param name="centerPos"></param>
        /// <param name="scale"></param>
        /// <returns></returns>
        internal DataTable SelectByPoint(Point mouseLocation, Rectangle bounds, PointF centerPos, double scale)
        {
            if (dataType == MySpaceDataType.MyGrid)  //栅格图层不选择
                return null; 
                
            List<int> fids = geoFeature.SelectByPoint(mouseLocation, bounds, centerPos, scale);
            if (fids.Count == 0)  //没有要素被选中
                return null;

            string set = "(";
            for (int i = 0; i < fids.Count; i++)
                if (i == 0)
                    set += fids[i].ToString();
                else
                    set += "," + fids[i].ToString();
            string sql = "Select * From " + dataName + " Where fid IN" + set + ");";
            return property.SqlQuery(sql);
        }
        internal void ClearSelection()
        {
            if (geoFeature != null)
                geoFeature.ClearSelection();
        }

        #region 编辑要素
        /// <summary>
        /// 开始编辑
        /// </summary>
        internal void StartEditing()
        {
            geoFeature.StartEditing();
            property.StartEditing();
        }
        /// <summary>
        /// 停止编辑
        /// </summary>
        internal void StopEditing()
        {
            geoFeature.CancelEdit();
            property.CancelEditing();
        }
        /// <summary>
        /// 添加指定要素
        /// </summary>
        /// <param name="geo"></param>
        internal void AddFeature(object geo)
        {
            geoFeature.AddGeoObj(geo);
            property.AddNewRecord(geoFeature.MaxFID);
        }
        /// <summary>
        /// 删除选中的要素
        /// </summary>
        internal void DeleteSelectedFeatures()
        {
            List<int> fid = geoFeature.DeleteSelectedFeatures();
            for (int i = 0; i < fid.Count; i++)
                property.DeleteRecord(fid[i]);
        }
        /// <summary>
        /// 保存编辑
        /// </summary>
        internal void SaveEdit()
        {
            geoFeature.SaveEdit(filePath);
            property.SaveEdit();
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
            geoFeature.MoveSelectedFeature(deltaX, deltaY, bounds, centerPos, scale);
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
            geoFeature.SelectVertex(mouseLocation, bounds, centerPos, scale);
        }
        /// <summary>
        /// 移动选中节点至新的坐标(WGS84坐标系)
        /// </summary>
        /// <param name="newPos"></param>
        internal void MoveVertex(MyPoint newPos)
        {
            geoFeature.MoveVertex(newPos);
        }
        #endregion

        internal MyRectangle GetMBR()
        {
            if (geoFeature != null)
                return new MyRectangle(geoFeature.MinX, geoFeature.MaxX, geoFeature.MinY, geoFeature.MaxY);
            else
                return new MyRectangle(myGrid.MinX, myGrid.MaxX, myGrid.MinY, myGrid.MaxY);
        }
        /// <summary>
        /// 在指定路径下保存为CGV文件
        /// </summary>
        internal void SaveCGV(object obj)
        {
            string path = (string)obj;
            geoFeature.SaveSpaceData(path);

            string newMdbPath = Path.GetDirectoryName(path) + "\\" + Path.GetFileNameWithoutExtension(path) + ".mdb";
            string oldMdbPath = Path.GetDirectoryName(filePath) + "\\" + dataName + ".mdb";
            if (oldMdbPath != newMdbPath)
                File.Copy(oldMdbPath, newMdbPath, true);
            string connectString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + newMdbPath;
            if (Path.GetFileNameWithoutExtension(path) != dataName)
            {
                OleDbConnection con = new OleDbConnection(connectString);
                con.Open();
                string sql = "SELECT * INTO " + Path.GetFileNameWithoutExtension(path) + " FROM " + dataName;
                OleDbCommand command = new OleDbCommand(sql, con);
                command.ExecuteNonQuery();
                sql = "DROP TABLE " + dataName;
                command.CommandText = sql;
                command.ExecuteNonQuery();
                command.Dispose();
                con.Close();
                con.Dispose();
            }
        }
        #endregion
    }
}
