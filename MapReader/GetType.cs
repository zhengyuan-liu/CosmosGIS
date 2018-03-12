using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosGIS.MapReader
{
    class GetType
    {
        public Map.MySpaceDataType SpaceDataType(string typename)
        {
            switch(typename)
            {
                case "MyPoint":               
                    return Map.MySpaceDataType.MyPoint;
                case "MyPolyLine":
                    return Map.MySpaceDataType.MyPolyLine;
                case "MyPolygon":
                    return Map.MySpaceDataType.MyPolygon;
                case　"MyGrid":
                    return Map.MySpaceDataType.MyGrid;
            }
            //TODO 防错(现在为随意选择的一个选项)
            return Map.MySpaceDataType.MyGrid;
        }
        public MyRenderer.RendererType RendererType(string typename)
        {
            switch(typename)
            {
                case "SimpleRenderer":
                    return MyRenderer.RendererType.SimpleRenderer;
                case "UniqueValueRenderer":
                    return MyRenderer.RendererType.UniqueValueRenderer;
                case "ClassBreakRenderer":
                    return MyRenderer.RendererType.ClassBreakRenderer;
                default:
                    return MyRenderer.RendererType.SimpleRenderer;
            }
        }
    }
}
