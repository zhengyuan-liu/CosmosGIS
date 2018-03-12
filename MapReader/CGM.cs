using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Data;
using CosmosGIS.Map;
using CosmosGIS.MyRenderer;
using CosmosGIS.MySymbol;
using CosmosGIS.FileReader;


namespace CosmosGIS.MapReader
{
    //CHENG
    public class CGM
    {
        #region 字段
        private string _filepath;//CGM储存路径       
        private Map.MyMap _map = null;//地图文件
        private Map.MyCoordinates _Geocoordinate;//地理坐标系
        private Map.MyCoordinates _Procoordinate;//投影坐标系
        private PointF _centerpos;//中心点位
        private double _ratio = 1;//比例尺
        private int _scaleindex = 1;//比例尺索引号
        #endregion

        #region 属性
        internal Map.MyCoordinates GeoCoordinate { get { return _Geocoordinate; } set { _Geocoordinate = value; } }//地理坐标系
        internal Map.MyCoordinates ProCoordinate { get { return _Procoordinate; } set { _Procoordinate = value; } }//投影坐标系
        public string FilePath { get { return _filepath; } set { _filepath = value; } } //储存路径
        internal Map.MyMap MyMap { get { return _map; } set { _map = value; } }//地图文件
        internal PointF CenterPos { get { return _centerpos; } set { _centerpos = value; } }//中心点位
        internal double Ratio { get { return _ratio; } set { _ratio = value; } }
        internal int ScaleIndex { get { return _scaleindex; } set { _scaleindex = value; } }
        #endregion

        #region 构造函数
        public CGM(string path)
        {
            if (Path.GetExtension(path) != ".cgm")
                throw new IOException("文件拓展名不正确！");
            _filepath = path;
            Open();
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="newmap">地图文件</param>
        /// <param name="centerpos">中心点位</param>
        /// <param name="ratio">比例尺</param>
        /// <param name="scaleindex">比例尺索引</param>
        internal CGM(Map.MyMap newmap, PointF centerpos, double ratio, int scaleindex)
        {
            _map = newmap;
            //TODO坐标系的记录
            _centerpos = centerpos;
            _ratio = ratio;
            _scaleindex = scaleindex;
        }

        #endregion

        #region 函数

        public void Open()
        {
            FileStream fs = new FileStream(_filepath, FileMode.Open);
            StreamReader sr = new StreamReader(fs);

            string line = sr.ReadLine();
            string[] words = line.Split(' ');
            _Geocoordinate = Map.MyCoordinates.WGS1984;//由于不存在null选项
            _Procoordinate = Map.MyCoordinates.WGS1984;//由于不存在null选项
            //记录地理坐标系
            switch (words[1])
            {
                case "WGS1984":
                    _Geocoordinate = Map.MyCoordinates.WGS1984;
                    break;
            }
            //记录投影坐标系
            switch (words[2])
            {
                case "WGS1984":
                    _Geocoordinate = Map.MyCoordinates.WGS1984;
                    break;
            }
            //生成地图
            _map = new Map.MyMap(words[0], _Geocoordinate, Convert.ToDouble(words[3]));
            _ratio = Convert.ToDouble(words[3]);
            _scaleindex = Convert.ToInt32(words[4]);
            //显示中心点位
            _centerpos = new PointF((float)Convert.ToDouble(words[5]),(float) Convert.ToDouble(words[6]));
            //图层数量
            int layercount = Convert.ToInt32(words[7]);

            for (int i = 0; i < layercount; i++)
            {
                string layerpath = sr.ReadLine();

                Map.MyLayer newlayer = null;
                if (Path.GetExtension(layerpath) == ".shp")
                {
                    //TODO先只可读SHP
                    Shapefile newshp = new Shapefile(layerpath);
                   newlayer = new MyLayer(newshp);
                }
                else
                {
                    CosmosGisVector newcgv = new CosmosGisVector(layerpath);
                    newlayer = new MyLayer(newcgv);
                }
                newlayer.FilePath = layerpath;
                //设置是否可见
                string visible = sr.ReadLine();
                if (visible == "True")
                    newlayer.Visible = true;
                else
                    newlayer.Visible = false;

                //获取渲染方式 
                string layerline = sr.ReadLine();
                RendererType newrendertyppe = new GetType().RendererType(layerline.Split(' ')[0]);
                newlayer.rendertype = newrendertyppe;
                switch (newrendertyppe)
                {
                    //唯一值渲染
                    case RendererType.UniqueValueRenderer:
                        string field_uvr = layerline.Split(' ')[1];
                        //TODO 获得属性唯一值
                        DataTable dt_uvr = newlayer.SpaceData.Property.SqlQuery("Select Distinct "+field_uvr+" From " + newlayer.LayerName);
                        List<string> values_uvr = new List<string>();
                        for (int j = 0; j < dt_uvr.Rows.Count;j++ )
                        {
                            values_uvr.Add(dt_uvr.Rows[j][field_uvr].ToString());
                        }
                            //List<string> values = new List<string>() { "1", "2", "3", "3" ,"3","3","7"};
                            newlayer.Renderer = new UniqueValueRenderer(newlayer.DataType, field_uvr, values_uvr);
                        break;
                    //分级渲染
                    case RendererType.ClassBreakRenderer:
                        string field_cbr = layerline.Split(' ')[1];
                        DataTable dt_cbr = newlayer.SpaceData.Property.SqlQuery("Select Distinct "+field_cbr+" From " + newlayer.LayerName);
                        List<double> values_cbr = new List<double>();
                        for (int j = 0; j < dt_cbr.Rows.Count;j++ )
                        {
                            values_cbr.Add(Convert.ToDouble( dt_cbr.Rows[j][field_cbr]));
                        }
                       
                        //List<double> breaks = new List<double>() { 1, 2, 3 };
                        newlayer.Renderer = new ClassBreakRenderer(newlayer.DataType, field_cbr, values_cbr);
                        break;
                    //简单渲染
                    default:
                        newlayer.Renderer = new SimpleRenderer(newlayer.DataType);
                        break;

                }

                //判断是否有注记信息
                string hastext = sr.ReadLine();
                if (hastext == "True")
                {
                    newlayer.HasText = true;                
                    newlayer.Textrender = new TextRenderer(sr.ReadLine());                    
                }
                else
                    newlayer.HasText = false;

                _map.AddLayer(newlayer);
            }
            sr.Close();
            fs.Close();
        }

        public void Save(string save_path)
        {
            _filepath = save_path;
            FileStream fs = new FileStream(_filepath, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);

            string line = _map.MapName + " " + _Geocoordinate.ToString() + " " + _Procoordinate.ToString() + " "
                            + _ratio.ToString("F2") + " " + _scaleindex.ToString() + " " + CenterPos.X.ToString() + " " + CenterPos.Y.ToString() + " " + _map.LayerNum.ToString();
            sw.WriteLine(line);
            //对每一图层
            for (int i = 0; i < _map.LayerNum; i++)
            {
                sw.WriteLine(_map.Layers[i].FilePath);
                sw.WriteLine(_map.Layers[i].Visible.ToString());

                switch (_map.Layers[i].rendertype)
                {
                    //唯一值渲染
                    case RendererType.UniqueValueRenderer:
                        UniqueValueRenderer Urenderer = (UniqueValueRenderer)_map.Layers[i].Renderer;
                        string filename_uvr = _map.Layers[i].rendertype.ToString() + " " + Urenderer.Field;
                        sw.WriteLine(filename_uvr);
                        break;
                    //分级渲染
                    case RendererType.ClassBreakRenderer:
                        ClassBreakRenderer Crenderer = (ClassBreakRenderer)_map.Layers[i].Renderer;
                        string filename_cbr = _map.Layers[i].rendertype.ToString() + " " + Crenderer.Field;
                        sw.WriteLine(filename_cbr);
                        break;
                    //简单渲染
                    default:
                        sw.WriteLine(_map.Layers[i].rendertype.ToString());
                        break;

                }

                //判断是否有注记信息
                bool hastext = _map.Layers[i].HasText;
                sw.WriteLine(hastext.ToString());
                if (hastext == true)
                {
                    sw.WriteLine(_map.Layers[i].Textrender.Field);
                    
                }

            }
            sw.Close();
            fs.Close();
        }

        /// <summary>
        /// 在指定路径下创建指定类型的空白CGV文件
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="spaceDataType">要素类型</param>
        public static void CreateEmptyCgv(string path)
        {
            CGM emptycgm = new CGM(new Map.MyMap(), new PointF(), 1, 1);
            emptycgm.GeoCoordinate = MyCoordinates.None;
            emptycgm.ProCoordinate = MyCoordinates.None;
            emptycgm.Save(path);
        }
        #endregion
    }
}
