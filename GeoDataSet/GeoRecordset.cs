using System;

namespace CosmosGIS.GeoDataSet
{ 
    /// <summary>
    /// //地理数据集类
    /// </summary>
public class GeoRecordset
{
        #region Members
        /// <summary>
        /// //字段集合对象
        /// </summary>
        private Fields _Fields = new Fields();
        /// <summary>
        /// //记录集合对象
        /// </summary>
        private Records _Records = new Records();
        #endregion

        #region Constructors
        /// <summary>
        /// //构造函数
        /// </summary>
        public GeoRecordset()
        {
            _Fields.FieldAppended += new Fields.FieldAppendHandle(_Fields_FileAppended);
            _Fields.FieldDeleted += new Fields.FieldDeleteHandle(_Fields_FileDeleted);
            _Records.ToNewRecord += new Records.ToNewRecordHandle(_Records_ToNewRecord);
        }
        #endregion

        #region Attributes
        /// <summary>
        /// //获取字段集合对象
        /// </summary>
        public Fields Fields {
            get { return _Fields; }
        }
        /// <summary>
        /// //获取记录集合对象
        /// </summary>
        public Records Records {
            get { return _Records; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// //从文件中读取
        /// </summary>
        /// <param name="FileName">读取文件名称</param>
        /// <returns></returns>
        public bool Open(string fileName)
        {
            Field sField = new Field("ID", ValueTypeConstant.dbInt);
            _Fields.Append(sField);
            sField = new Field("Geomery", ValueTypeConstant.dbMultiPolyline);
            _Fields.Append(sField);
            sField = new Field("名称", ValueTypeConstant.dbText);
            _Fields.Append(sField);
            sField = new Field("人口", ValueTypeConstant.dbInt);
            _Fields.Append(sField);
            sField = new Field("GDP", ValueTypeConstant.dbDouble);
            _Fields.Append(sField);
            int sRecordCount = 100;
            Random rand = new Random();
            for (int i = 0; i < sRecordCount; i++)
            {
                Record sRecord = _Records.NewRecord();
                sRecord.SetValue(0, i);
                sRecord.SetValue(1, null);
                sRecord.SetValue(2, "北京");
                sRecord.SetValue(3, rand.Next(10000));
                sRecord.SetValue(4, Math.Round(1000 * rand.NextDouble(), 3));
            }
            return true;
        }
        /// <summary>
        /// //将数据保存到指定文件
        /// </summary>
        /// <param name="fileName">保存文件名</param>
        /// <returns></returns>
        public bool Save(string fileName)
        {
            return true;
        }
        /// <summary>
        /// //根据SQL文本进行选择
        /// </summary>
        /// <param name="sqlText">SQL文本</param>
        /// <returns></returns>
        public GeoRecordset SelectByText(string sqlText)
        {
            return null;
        }
        #endregion

        #region Events
        #endregion

        #region Events
        /// <summary>
        /// //当有字段被加入时的处理事件
        /// </summary>
        private void _Records_ToNewRecord(object sender, Record record)
        {
            int sFieldCount = _Fields.Count;
            Record sRecord = record;
            for (int i = 0; i < sFieldCount; i++)
            {
                Field sField = _Fields[i];
                if (sField.ValueType == ValueTypeConstant.dbPoint)
                    sRecord.Append(null);
                else if (sField.ValueType == ValueTypeConstant.dbMulitPolygon)
                    sRecord.Append(null);
                else if (sField.ValueType == ValueTypeConstant.dbMulitPolygon)
                    sRecord.Append(null);
                else if (sField.ValueType == ValueTypeConstant.dbDouble)
                    sRecord.Append((int)0);
                else if (sField.ValueType == ValueTypeConstant.dbInt)
                    sRecord.Append((double)0);
                else if (sField.ValueType == ValueTypeConstant.dbText)
                    sRecord.Append(string.Empty);
                else
                    sRecord.Append(null);
            }
        }
        /// <summary>
        /// //当有字段被删除时的处理事件
        /// </summary>
        private void _Fields_FileDeleted(object sender, int index, Field field)
        {
            int sRecordCount = _Records.Count;
            for (int i = 0; i < sRecordCount; i++)
            {
                Record sRecord = _Records.GetItem(i);
                sRecord.Delete(index);
            }
        }
        /// <summary>
        /// //当有新纪录时的处理事件
        /// </summary>
        private void _Fields_FileAppended(object sender, Field field)
        {
            int sRecordCount = _Records.Count;
            Field sField = field;
            for (int i = 0; i < sRecordCount; i++)
            {
                Record sRecord = _Records.GetItem(i);
                if (sField.ValueType == ValueTypeConstant.dbPoint)
                    sRecord.Append(null);
                else if (sField.ValueType == ValueTypeConstant.dbMulitPolygon)
                    sRecord.Append(null);
                else if (sField.ValueType == ValueTypeConstant.dbMulitPolygon)
                    sRecord.Append(null);
                else if (sField.ValueType == ValueTypeConstant.dbDouble)
                    sRecord.Append((int)0);
                else if (sField.ValueType == ValueTypeConstant.dbInt)
                    sRecord.Append((double)0);
                else if (sField.ValueType == ValueTypeConstant.dbText)
                    sRecord.Append(string.Empty);
                else
                    sRecord.Append(null);
            }
        }
        #endregion
    }
}
