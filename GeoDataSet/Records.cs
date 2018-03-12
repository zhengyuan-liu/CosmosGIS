using System;
using System.Collections.Generic;

namespace CosmosGIS.GeoDataSet
{
    /// <summary>
    /// //记录集合对象
    /// </summary>
    public class Records
    {
        #region Members
        /// <summary>
        /// //记录集合
        /// </summary>
        private List<Record> _Records = new List<Record>();
        #endregion

        #region Attributes
        /// <summary>
        /// //记录集合数目
        /// </summary>
        public int Count {
            get { return _Records.Count; } }
        #endregion

        #region Constructors
        /// <summary>
        /// //构造函数
        /// </summary>
        internal Records()
        {

        }
        #endregion
        #region Methods
        /// <summary>
        /// //获取指定索引号的记录
        /// </summary>
        /// <param name="index">索引值</param>
        /// <returns></returns>
        public Record GetItem(int index)
        {
            return _Records[index];
        }
        /// <summary>
        /// //新建一条记录，并返回该记录
        /// </summary>
        /// <returns></returns>
        public Record NewRecord()
        {
            Record sRecord = new Record();
            ToNewRecord(this, sRecord);
            _Records.Add(sRecord);
            return sRecord;
        }
        /// <summary>
        /// //向记录集合的末位添加指定记录
        /// </summary>
        /// <param name="record">要添加的指定记录</param>
        internal void Add(Record record)
        {
            if (record != null)
                _Records.Add(record);
            else
                throw new Exception("Invalid record.");
        }
        /// <summary>
        /// //删除指定索引号的记录
        /// </summary>
        /// <param name="index">指定索引号</param>
        internal void Delete(int index)
        {
            _Records.RemoveAt(index);
        }
        #endregion

        #region Events
        /// <summary>
        /// //新建记录的委托
        /// </summary>
        /// <param name="sender">发布委托的对象</param>
        /// <param name="record">新建记录</param>
        internal delegate void ToNewRecordHandle(object sender, Record record);
        /// <summary>
        /// //新建记录的事件
        /// </summary>
        internal event ToNewRecordHandle ToNewRecord;
        #endregion
    }
}
