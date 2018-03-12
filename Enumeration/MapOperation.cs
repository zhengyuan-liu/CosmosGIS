using System.ComponentModel;

namespace CosmosGIS.Enumeration
{
    [Description("地图操作类型")]
    enum MapOperation
    {
        [Description("选择")]
        SelectElement,
        [Description("放大")]
        ZoomIn,
        [Description("缩小")]
        ZoomOut,
        [Description("漫游")]
        Pan,
        [Description("选择要素")]
        SelectFeatures,
        [Description("编辑要素")]
        Edit,
        [Description("编辑要素顶点")]
        EditVertices,
        [Description("创建要素")]
        CreateFeatures,
        [Description("识别要素")]
        Identify
    }
}
