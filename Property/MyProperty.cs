using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;

namespace CosmosGIS.Property
{
    /// <summary>
    /// //要素属性类
    /// </summary>
    public class MyProperty
    {
        #region Members

        /// <summary>
        /// 要素字段
        /// </summary>
        private Dictionary<string, Type> fields = new Dictionary<string, Type>();

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        private string connectString = "";

        /// <summary>
        /// 要素名称
        /// </summary>
        private string featureName;

        #endregion

        #region Attributes
        /// <summary>
        /// 字段数目
        /// </summary>
        public int FieldNum { get { return fields.Count; } }
        /// <summary>
        /// 获取指定索引字段
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Type this[string name] { get { return fields[name]; }}
        /// <summary>
        /// 获取要素名称
        /// </summary>
        public string FeatureName { get { return featureName; } }
        public Dictionary<string, Type> Fields
        {
            get { return fields; }
            set { fields = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public MyProperty(string filename)
        {
            SetConnectString(filename);
            featureName = Path.GetFileNameWithoutExtension(filename);
            
            //获取所有字段和类型
            DataTable dt = new DataTable();
            OleDbConnection con = new OleDbConnection(connectString);
            con.Open();
            dt = con.GetOleDbSchemaTable(OleDbSchemaGuid.Columns, new object[] { null, null, featureName, null });
            int n = dt.Rows.Count;
            List<string> list = new List<string>();
            List<Type> types = new List<Type>();
            string[] strTable = new string[n];
            int index_name = dt.Columns.IndexOf("COLUMN_NAME");
            int index_type = dt.Columns.IndexOf("DATA_TYPE");
            for (int i = 0; i < n; i++)
            {
                DataRow dr = dt.Rows[i];
                string fieldName = dr.ItemArray.GetValue(index_name).ToString();
                int value = (int)dr.ItemArray.GetValue(index_type);
                switch (value)
                {
                    case 3:  //int32
                        fields.Add(fieldName, typeof(int));
                        break;
                    case 5:  //double
                        fields.Add(fieldName, typeof(double));
                        break;
                    case 11:  //bool
                        fields.Add(fieldName, typeof(bool));
                        break;
                    case 129:  //char
                        fields.Add(fieldName, typeof(char));
                        break;
                    case 130:  //string
                        fields.Add(fieldName, typeof(string));
                        break;
                }
            }
            con.Close();
            con.Dispose();
        }
        #endregion

        #region Methods
        /// <summary>
        /// 添加新字段
        /// </summary>
        /// <param name="type"></param>
        public void AddField(string name,Type type)
        {
            fields.Add(name, type);
        }
        /// <summary>
        /// 删除字段
        /// </summary>
        /// <param name="index"></param>
        public void DelField(string name)
        {
            if (fields.ContainsKey(name))
                fields.Remove(name);
        }
        

        /// <summary>
        /// 修改指定索引字段的名字
        /// </summary>
        /// <param name="oldName"></param>
        /// <param name="name"></param>
        public void RectifyField(string oldName, string newName)
        {
            if (fields.ContainsKey(oldName))
            {
                Type tpType = fields[oldName];
                fields.Remove(oldName);
                fields.Add(newName, tpType);
            }
        }
        /// <summary>
        /// 设置连接数据库字符串
        /// </summary>
        /// <param name="dataSource"></param>
        /// <param name="userid"></param>
        /// <param name="password"></param>
        /// <param name="server"></param>
        public void SetConnectString(string dataSource)
        {
            connectString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + dataSource;
        }
        /// <summary>
        /// 获取属性列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetProperties()
        {
            OleDbConnection con = new OleDbConnection(connectString);
            con.Open();
            string command = "SELECT * FROM " + featureName;
            OleDbDataAdapter adp = new OleDbDataAdapter(command, con);
            DataTable tb = new DataTable(); 
            adp.Fill(tb);
            adp.Dispose();
            con.Close();
            con.Dispose();
            return tb;
        }
        /// <summary>
        /// SQL条件查询
        /// </summary>
        /// <param name="sqlSentence">SQL语句字符串</param>
        /// <returns></returns>
        public DataTable SqlQuery(string sqlSentence)
        {
            OleDbConnection con = new OleDbConnection(connectString);
            con.Open();
            OleDbDataAdapter adp = new OleDbDataAdapter(sqlSentence, con);
            try
            {
                DataTable tb = new DataTable();
                adp.Fill(tb);
                return tb;
            }
            catch (OleDbException e)
            {
                throw e;
            }
            finally
            {
                adp.Dispose();
                con.Close();
                con.Dispose();
            }
        }
        /// <summary>
        /// //保存属性表
        /// </summary>
        internal void SavaProperties(DataTable dataSource)
        {
            using (OleDbConnection cnn = new OleDbConnection(connectString))
            {
                cnn.Open();
                //删除原表
                string sql = "Drop table " + featureName + ";";
                OleDbCommand command = new OleDbCommand(sql, cnn);
                command.ExecuteNonQuery();

                //创建新表，添加FID字段并在FID字段上添加主键
                sql = "Create table " + featureName + "(FID integer primary key);";
                command = new OleDbCommand(sql, cnn);
                command.ExecuteNonQuery();

                foreach (DataColumn column in dataSource.Columns)
                {
                    if (String.Equals(column.ColumnName, "fid", StringComparison.OrdinalIgnoreCase))
                        continue;
                    sql = "Alter table " + featureName + " Add " + column.ColumnName;
                    Type type = column.DataType;
                    if (type == typeof(int))
                        sql += " integer;";
                    else if (type == typeof(double))
                        sql += " double;";
                    else if (type == typeof(bool))
                        sql += " bit;";
                    else if (type == typeof(char))
                        sql += " char;";
                    else if (type == typeof(string))
                        sql += " string;";
                    else if (type == typeof(DateTime))
                        sql += " DateTime;";
                    command = new OleDbCommand(sql, cnn);
                    command.ExecuteNonQuery();
                }

                //逐行插入数据
                foreach (DataRow row in dataSource.Rows)
                {
                    sql = "Insert into " + featureName + " Values(";
                    string sql2 = "";
                    for (int i = 0; i < row.ItemArray.Length; i++)
                    {
                        sql2 = row.ItemArray[i].ToString();
                        if (sql2 == "")  //空值处理
                            sql2 = "null";
                        else if (dataSource.Columns[i].DataType == typeof(string))
                            sql2 = "'" + sql2 + "'";
                        if (i != 0)
                            sql2 = "," + sql2;
                        sql += sql2;
                    }
                    sql += ");";
                    command = new OleDbCommand(sql, cnn);
                    command.ExecuteNonQuery();
                }               
            }
        }
        /// <summary>
        /// //获取指定索引号的要素属性表
        /// </summary>
        /// <param name="item2"></param>
        /// <returns></returns>
        internal DataTable GetProperties(int item2)
        {
            //TODO
            throw new NotImplementedException();
        }
        /// <summary>
        /// 开始编辑
        /// </summary>
        internal void StartEditing()
        {
            using (OleDbConnection cnn = new OleDbConnection(connectString))
            {
                cnn.Open();
                //创建数据表副本copy
                string sql = "select * into copy from " + featureName + ";";
                OleDbCommand command = new OleDbCommand(sql, cnn);
                command.ExecuteNonQuery();
                //给copy表加上主键FID
                sql = "Alter table copy add primary key(FID);";
                command = new OleDbCommand(sql, cnn);
                command.ExecuteNonQuery();
            }
        }
        /// <summary>
        /// 取消编辑
        /// </summary>
        internal void CancelEditing()
        {
            OleDbConnection cnn = new OleDbConnection(connectString);
            cnn.Open();
            //删除修改过的原始表
            string sql = "Drop table " + featureName + ";";
            OleDbCommand command = new OleDbCommand(sql, cnn);
            command.ExecuteNonQuery();
            //通过副本恢复原始表
            sql = "Select * into " + featureName + " from copy;";
            command = new OleDbCommand(sql, cnn);
            command.ExecuteNonQuery();
            //给原始表加上主键FID
            sql = "Alter table " + featureName + " add primary key(FID);";
            command = new OleDbCommand(sql, cnn);
            command.ExecuteNonQuery();
            //删除副本
            sql = "Drop table copy;";
            command = new OleDbCommand(sql, cnn);
            command.ExecuteNonQuery();
            cnn.Close();
            cnn.Dispose();
        }
        /// <summary>
        /// 保存编辑
        /// </summary>
        internal void SaveEdit()
        {
            OleDbConnection cnn = new OleDbConnection(connectString);
            cnn.Open();
            //删除副本
            string sql = "Drop table copy;";
            OleDbCommand command = new OleDbCommand(sql, cnn);
            command.ExecuteNonQuery();
            //重新创建数据表副本copy
            sql = "select * into copy from " + featureName + ";";
            command = new OleDbCommand(sql, cnn);
            command.ExecuteNonQuery();
            //给copy表加上主键FID
            sql = "Alter table copy add primary key(FID);";
            command = new OleDbCommand(sql, cnn);
            command.ExecuteNonQuery();
            cnn.Close();
            cnn.Dispose();
        }
        /// <summary>
        /// 添加新纪录
        /// </summary>
        /// <param name="fid"></param>
        public void AddNewRecord(int fid)
        {
            OleDbConnection cnn = new OleDbConnection(connectString);
            cnn.Open();
            string value = " Values (" + fid.ToString();
            for (int i = 1; i < fields.Count; i++)
                value += ",null";
            value += ");";
            OleDbCommand command = new OleDbCommand("Insert Into " + featureName + value, cnn);
            command.ExecuteNonQuery();
            cnn.Close();
            cnn.Dispose();
        }
        /// <summary>
        /// 删除指定记录
        /// </summary>
        /// <param name="fid"></param>
        public void DeleteRecord(int fid)
        {
            OleDbConnection cnn = new OleDbConnection(connectString);
            cnn.Open();
            string sql = "Delete From " + featureName + " Where FID = " + fid;
            OleDbCommand command = new OleDbCommand(sql, cnn);
            command.ExecuteNonQuery();
            cnn.Close();
            cnn.Dispose();
        }
        #endregion
    }
}
