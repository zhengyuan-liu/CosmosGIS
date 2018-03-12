using System;
using System.Collections.Generic;

namespace CosmosGIS.GeoDataSet
{
    /// <summary>
    /// //字段集合对象
    /// </summary>
    public class Fields
    {
        #region Members
        /// <summary>
        /// //字段集合
        /// </summary>
        private List<Field> _Fields = new List<Field>();
        #endregion

        #region Constructors
        /// <summary>
        /// //构造函数
        /// </summary>
        internal Fields() { }
        #endregion

        #region Attributes
        /// <summary>
        /// //字段集合个数
        /// </summary>
        public int Count {
            get { return _Fields.Count; }
        }
        /// <summary>
        /// //字段索引器
        /// </summary>
        /// <param name="index">索引号</param>
        /// <returns></returns>
        public Field this[int index]{
            get { return _Fields[index]; }
        }
        #endregion

        #region Methods 
        /// <summary>
        /// //在末尾追加指定字段
        /// </summary>
        /// <param name="field">追加的指定字段</param>
        public void Append(Field field)
        {
            if (field != null)
            {
                _Fields.Add(field);
                FieldAppended(this, field);
            }
            else
                throw new Exception("Invalid Fields.");
        }
        /// <summary>
        /// //删除指定索引号的字段
        /// </summary>
        /// <param name="index"></param>
        public void Delete(int index)
        {
            Field sField = _Fields[index];
            _Fields.RemoveAt(index);
            FieldDeleted(this, index, sField);
        }
        #endregion

        #region Events
        /// <summary>
        /// //添加字段的委托
        /// </summary>
        /// <param name="sender">发布委托的对象</param>
        /// <param name="field">字段</param>
        internal delegate void FieldAppendHandle(object sender, Field field);
        /// <summary>
        /// //字段被加入
        /// </summary>
        internal event FieldAppendHandle FieldAppended;
        /// <summary>
        /// //删除字段的委托
        /// </summary>
        /// <param name="sender">发布委托的对象</param>
        /// <param name="index">被删除字段索引</param>
        internal delegate void FieldDeleteHandle(object sender, int index, Field field);
        /// <summary>
        /// //字段被删除
        /// </summary>
        internal event FieldDeleteHandle FieldDeleted;
        #endregion
    }
}
