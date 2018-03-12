namespace CosmosGIS.GeoDataSet
{
    public class Field
    {
        #region Members
        /// <summary>
        /// //字段名称
        /// </summary>
        private string _Name;
        /// <summary>
        /// //字段属性
        /// </summary>
        private ValueTypeConstant _ValueType;
        #endregion

        #region Attributes
        /// <summary>
        /// //只读字段名称属性
        /// </summary>
        public string Name { get { return _Name; } }
        /// <summary>
        /// //只读字段属性属性
        /// </summary>
        public ValueTypeConstant ValueType { get { return _ValueType; } }
        #endregion

        #region Constructors
        /// <summary>
        /// ///构造函数
        /// </summary>
        /// <param name="name">字段名称</param>
        /// <param name="valueType">字段值类型</param>
        public Field(string name,ValueTypeConstant valueType)
        {
            this._Name = name;
            this._ValueType = valueType;
        }
        #endregion
    }
}