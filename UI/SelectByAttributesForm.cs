using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Data.OleDb;
using CosmosGIS.Map;
using CosmosGIS.Property;

namespace CosmosGIS.UI
{
    public partial class SelectByAttributesForm : Form
    {
        #region Members
        /// <summary>
        /// //地图
        /// </summary>
        private MyMap map;
        /// <summary>
        /// 选定图层属性
        /// </summary>
        private MyProperty property;
        #endregion

        #region Properties
        /// <summary>
        /// 搜索结果表
        /// </summary>
        internal DataTable ResultTable { get; set; }
        /// <summary>
        /// SQL语句
        /// </summary>
        public string SQLstring { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public SelectByAttributesForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 菜单入口的构造函数
        /// </summary>
        internal SelectByAttributesForm(MyMap map)
        {
            InitializeComponent();
            this.map = map;
            listBoxFields.Items.Clear();
            lblSQL.Text = "Select * From";
            for (int i = 0; i < map.LayerNum; i++)
                comboBoxLayers.Items.Add(map.GetLayerName(i));
        }
        /// <summary>
        /// 属性表入口的构造函数
        /// </summary>
        internal SelectByAttributesForm(MyProperty property)
        {
            InitializeComponent();
            this.property = property;
            listBoxFields.Items.Clear();
            Dictionary<string, Type>.KeyCollection keyColl = property.Fields.Keys;
            foreach (string s in keyColl)
            {
                listBoxFields.Items.Add(s);
            }
            lblSQL.Text = "Select * From " + property.FeatureName + " Where ";
            comboBoxLayers.Enabled = false;
        }
        #endregion

        #region Events
        /// <summary>
        /// //操作符按钮点击事件
        /// </summary>
        private void btnOperator_Click(object sender, EventArgs e)
        {
            textBoxSQL.Text += " " + ((Button)sender).Text + " ";
        }
        /// <summary>
        /// 选择图层改变事件
        /// </summary>
        private void comboBoxLayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBoxFields.Items.Clear();
            listBoxUniqueValue.Items.Clear();
            btnGetUniqueValues.Enabled = false;
            if (map.Layers[comboBoxLayers.SelectedIndex].DataType != MySpaceDataType.MyGrid)
            {
                property = map.Layers[comboBoxLayers.SelectedIndex].SpaceData.Property;
                Dictionary<string, Type>.KeyCollection keyColl = property.Fields.Keys;
                foreach (string s in keyColl)
                {
                    listBoxFields.Items.Add(s);
                }
                lblSQL.Text = "Select * From " + property.FeatureName + " Where ";
            }
            else
            {
                MessageBox.Show("不能对栅格图层进行操作!");
                listBoxFields.SelectedIndex = -1;
            }
        }
        /// <summary>
        /// 选择属性索引改变
        /// </summary>
        private void listBoxFields_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxFields.SelectedIndex != -1)
                btnGetUniqueValues.Enabled = true;
            else
                btnGetUniqueValues.Enabled = false;
        }
        /// <summary>
        /// 属性名称获取
        /// </summary>
        private void listBoxFields_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listBoxFields.SelectedIndex != -1)
                textBoxSQL.Text += listBoxFields.Items[listBoxFields.SelectedIndex];
        }
        /// <summary>
        /// 获取唯一值按钮
        /// </summary>
        private void btnGetUniqueValues_Click(object sender, EventArgs e)
        {
            listBoxUniqueValue.Items.Clear();
            string sqlQuery = "Select Distinct " + listBoxFields.Items[listBoxFields.SelectedIndex] + " From " + property.FeatureName;
            DataTable dt = property.SqlQuery(sqlQuery);
            foreach (DataRow row in dt.Rows)
                listBoxUniqueValue.Items.Add(row.ItemArray[0]);
        }
        /// <summary>
        /// 属性唯一值选择
        /// </summary>
        private void listBoxUniqueValue_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string fieldName = listBoxFields.Items[listBoxFields.SelectedIndex].ToString();
            if (property.Fields[fieldName] == typeof(string))
                textBoxSQL.Text += "'" + listBoxUniqueValue.Items[listBoxUniqueValue.SelectedIndex] + "'";
            else
                textBoxSQL.Text += listBoxUniqueValue.Items[listBoxUniqueValue.SelectedIndex];
        }
        /// <summary>
        /// 确定按钮
        /// </summary>
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (comboBoxLayers.SelectedIndex != -1 && map.Layers[comboBoxLayers.SelectedIndex].DataType != MySpaceDataType.MyGrid)
            {
                try
                {
                    SQLstring = lblSQL.Text + " " + textBoxSQL.Text;
                    ResultTable = property.SqlQuery(SQLstring);
                    List<int> ids = new List<int>();
                    for (int i = 0; i < ResultTable.Rows.Count; i++)
                        ids.Add(int.Parse(ResultTable.Rows[i][0].ToString()));
                    map.SelectByIds(ids, comboBoxLayers.SelectedIndex);
                    ResultTable.TableName = comboBoxLayers.SelectedItem.ToString();
                    if (SelectionChanged != null)
                        SelectionChanged(ids, comboBoxLayers.SelectedIndex);
                    Dispose();
                }
                catch (OleDbException)
                {
                    MessageBox.Show("SQL语句格式错误", "CosmosGIS", MessageBoxButtons.OK, MessageBoxIcon.Error); 
                }
            }
            else
            {
                MessageBox.Show("图层属性数据不能为空", "CosmosGIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// 应用按钮
        /// </summary>
        private void btnApply_Click(object sender, EventArgs e)
        {
            if (comboBoxLayers.SelectedIndex != -1 && map.Layers[comboBoxLayers.SelectedIndex].DataType != MySpaceDataType.MyGrid)
            {
                try
                {
                    SQLstring = lblSQL.Text + " " + textBoxSQL.Text;
                    ResultTable = property.SqlQuery(SQLstring);
                    List<int> ids = new List<int>();
                    for (int i = 0; i < ResultTable.Rows.Count; i++)
                        ids.Add(int.Parse(ResultTable.Rows[i][0].ToString()));
                    map.SelectByIds(ids, comboBoxLayers.SelectedIndex);
                    ResultTable.TableName = comboBoxLayers.SelectedItem.ToString();
                    if (SelectionChanged != null)
                        SelectionChanged(ids, comboBoxLayers.SelectedIndex);
                }
                catch (OleDbException)
                {
                    MessageBox.Show("SQL语句格式错误", "CosmosGIS", MessageBoxButtons.OK, MessageBoxIcon.Error);  //待完善
                }
            }
            else
            {
                MessageBox.Show("图层属性数据不能为空", "CosmosGIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// 取消按钮
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Dispose();
        }
        /// <summary>
        /// //选中改变
        /// </summary>
        public delegate void SelectionChangedDelegate(List<int> ids, int layerIndex);
        public event SelectionChangedDelegate SelectionChanged;
        #endregion
    }
}
