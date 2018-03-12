using System.Collections.Generic;

namespace CosmosGIS.GeoDataSet
{
    /// <summary>
    /// //记录对象
    /// </summary>
    public class Record
    {
        #region Members
        /// <summary>
        /// //记录集合
        /// </summary>
        List<object> _Values = new List<object>();
        #endregion

        #region Constructors
        /// <summary>
        /// //构造函数
        /// </summary>
        internal Record()
        {

        }
        #endregion

        #region Attributes
        #endregion

        #region Methods
        /// <summary>
        /// //访问指定索引号的值
        /// </summary>
        /// <param name="index">指定索引号</param>
        /// <returns></returns>
        public object GetValue(int index)
        {
            return _Values[index];
        }
        /// <summary>
        /// //设置指定索引号的值
        /// </summary>
        /// <param name="index">索引号</param>
        /// <param name="value">字段值</param>
        public void SetValue(int index,object value)
        {
            _Values[index] = value;
        }
        /// <summary>
        /// //在末尾追加字段值
        /// </summary>
        /// <param name="obj">字段值</param>
        internal void Append(object obj)
        {
            _Values.Add(obj);
        }
        /// <summary>
        /// //删除指定索引号的字段值
        /// </summary>
        /// <param name="index">指定索引号</param>
        internal void Delete(int index)
        {
            _Values.RemoveAt(index);
        }
        #endregion
    }
}
