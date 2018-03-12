using System.Windows.Forms;
using CosmosGIS.Property;
using System.Data;
using CosmosGIS.Map;
using System.Collections.Generic;
using System;

namespace CosmosGIS.UI
{
    /// <summary>
    /// //要素属性窗口
    /// </summary>
    public partial class PropertyTableForm : Form
    {
        #region Members
        /// <summary>
        /// //空间数据属性对象
        /// </summary>
        private MyProperty property;
        /// <summary>
        /// 图层选中要素
        /// </summary>
        private List<int> selectionIDs;
        /// <summary>
        /// //空间属性所有记录属性表
        /// </summary>
        private DataTable totalRecords;
        /// <summary>
        /// //显示的空间属性记录数目
        /// </summary>
        private int ShowedRecordNum = 0;
        /// <summary>
        /// 图层索引        
        /// </summary>
        private int layerIndex = 0;
        private MyLayer layer;
        #endregion

        #region Contructors
        /// <summary>
        /// //唯一构造函数，必须传进来一个属性对象
        /// </summary>
        internal PropertyTableForm(MyLayer layer,int layerIndex)
        {
            InitializeComponent();
            selectionIDs = layer.selectionIDs;
            this.layerIndex = layerIndex;
            this.layer = layer;
            property = layer.GetFeaturePorperties();      
        }
        #endregion

        #region SystemMethods
        /// <summary>
        /// //重写消息处理机制，忽略窗口移动的消息
        /// </summary>
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0xA1 && m.WParam.ToInt32() == 2)
                return;
            base.WndProc(ref m);
        }
        #endregion

        #region Events
        /// <summary>
        /// //加载属性表
        /// </summary>
        private void PropertyPanel_Shown(object sender, System.EventArgs e)
        {
            totalRecords = property.GetProperties();
            dataGridView.DataSource = totalRecords.Copy();
            for (int i = 0; i < dataGridView.ColumnCount; i++)
                dataGridView.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            lblFeatureName.Text = property.FeatureName;
            ShowedRecordNum = totalRecords.Rows.Count;
            SetSelections();
        }
        /// <summary>
        /// //添加字段按钮点击事件
        /// </summary>
        private void toolStripButtonAddField_Click(object sender, System.EventArgs e)
        {
            AddColumnForm form = new AddColumnForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                property.AddField(form.FieldName, form.FieldType);
                DataGridViewTextBoxColumn txtBoxCol = new DataGridViewTextBoxColumn();
                txtBoxCol.SortMode = DataGridViewColumnSortMode.NotSortable;
                txtBoxCol.HeaderText = form.FieldName;
                txtBoxCol.Name = form.FieldName;
                txtBoxCol.ValueType = form.FieldType;
                dataGridView.Columns.Add(txtBoxCol);
                totalRecords.Columns.Add(new DataColumn(form.FieldName, form.FieldType));
            }
        }
        /// <summary>
        /// //根据属性选择要素
        /// </summary>
        private void toolStripSelectByAttribute_Click(object sender, System.EventArgs e)
        {
            SelectByAttributesForm form = new SelectByAttributesForm(property);
            if (form.ShowDialog() == DialogResult.OK)
            {
                dataGridView.DataSource = form.ResultTable;  //待完善
            }
            form.Dispose();
        }
        /// <summary>
        /// //反选要素按钮
        /// </summary>
        private void toolStripSwitchSelection_Click(object sender, System.EventArgs e)
        {
            for (int i = 0; i < dataGridView.Rows.Count; i++)
                dataGridView.Rows[i].Selected = !dataGridView.Rows[i].Selected;
            int index = dataGridView.Rows.Count;
            for (int i = 0; i < dataGridView.SelectedRows.Count; i++)
                if (dataGridView.SelectedRows[i].Index < index)
                    index = dataGridView.SelectedRows[i].Index;
            if (index != dataGridView.Rows.Count)
                dataGridView.FirstDisplayedScrollingRowIndex = index;
            List<int> ids = new List<int>();
            for (int i = 0; i < dataGridView.SelectedRows.Count; i++)
                ids.Add(int.Parse(dataGridView.SelectedRows[i].Cells[0].Value.ToString()));
            ids.Sort();
            layer.SelectByIds(ids);
            if (SelectFeatureChanged != null)
                SelectFeatureChanged(ids, layerIndex);
            toolStripLblStatus.Text = "(" + dataGridView.SelectedRows.Count.ToString() + " out of " + ShowedRecordNum.ToString() + " Selected)";
        }
        /// <summary>
        /// //清除所有选择
        /// </summary>
        private void toolStripCearSelection_Click(object sender, System.EventArgs e)
        {
            dataGridView.ClearSelection();
            List<int> ids = new List<int>();
            layer.SelectByIds(ids);
            if (SelectFeatureChanged != null)
                SelectFeatureChanged(ids, layerIndex);
            toolStripLblStatus.Text = "(0 out of " + ShowedRecordNum.ToString() + " Selected)";
        }
        /// <summary>
        /// //指向第一个记录
        /// </summary>
        private void toolStripStartRecord_Click(object sender, System.EventArgs e)
        {
            dataGridView.ClearSelection();
            if (dataGridView.Rows.Count > 1)
            {
                dataGridView.Rows[0].Selected = true;
                dataGridView.FirstDisplayedScrollingRowIndex = 0;
                if (dataGridView.SelectedRows.Count != 0)
                {
                    List<int> ids = new List<int>();
                    for (int i = 0; i < dataGridView.SelectedRows.Count; i++)
                        ids.Add(int.Parse(dataGridView.SelectedRows[i].Cells[0].Value.ToString()));
                    ids.Sort();
                    layer.SelectByIds(ids);
                    if (SelectFeatureChanged != null)
                        SelectFeatureChanged(ids, layerIndex);
                }
                toolStripLblStatus.Text = "(1 out of " + ShowedRecordNum.ToString() + " Selected)";
            }
        }
        /// <summary>
        /// //指向前一个记录
        /// </summary>
        private void toolStripPreviewRecord_Click(object sender, System.EventArgs e)
        {
            if(dataGridView.SelectedRows.Count!=0)
            {
                int index = dataGridView.Rows.Count;
                for (int i = 0; i < dataGridView.SelectedRows.Count; i++)
                    if (dataGridView.SelectedRows[i].Index < index)
                        index = dataGridView.SelectedRows[i].Index;
                dataGridView.FirstDisplayedScrollingRowIndex = index;
                dataGridView.ClearSelection();
                if (index != 0)
                    index--;
                dataGridView.Rows[index].Selected = true;
                if (dataGridView.SelectedRows.Count != 0)
                {
                    List<int> ids = new List<int>();
                    for (int i = 0; i < dataGridView.SelectedRows.Count; i++)
                        ids.Add(int.Parse(dataGridView.SelectedRows[i].Cells[0].Value.ToString()));
                    ids.Sort();
                    layer.SelectByIds(ids);
                    if (SelectFeatureChanged != null)
                        SelectFeatureChanged(ids, layerIndex);
                }
                dataGridView.FirstDisplayedScrollingRowIndex = index;
                toolStripLblStatus.Text = "(1 out of " + ShowedRecordNum.ToString() + " Selected)";
            }
        }
        /// <summary>
        /// //指向下一个记录
        /// </summary>
        private void toolStripNextRecord_Click(object sender, System.EventArgs e)
        {
            if (dataGridView.SelectedRows.Count != 0)
            {
                int index = 0;
                for (int i = 0; i < dataGridView.SelectedRows.Count; i++)
                    if (dataGridView.SelectedRows[i].Index > index)
                        index = dataGridView.SelectedRows[i].Index;
                dataGridView.FirstDisplayedScrollingRowIndex = index;
                dataGridView.ClearSelection();
                if (index != dataGridView.Rows.Count - 1)
                    index++;
                dataGridView.Rows[index].Selected = true;
                if (dataGridView.SelectedRows.Count != 0)
                {
                    List<int> ids = new List<int>();
                    for (int i = 0; i < dataGridView.SelectedRows.Count; i++)
                        ids.Add(int.Parse(dataGridView.SelectedRows[i].Cells[0].Value.ToString()));
                    ids.Sort();
                    layer.SelectByIds(ids);
                    if (SelectFeatureChanged != null)
                        SelectFeatureChanged(ids, layerIndex);
                }
                dataGridView.FirstDisplayedScrollingRowIndex = index;
                toolStripLblStatus.Text = "(1 out of " + ShowedRecordNum.ToString() + " Selected)";
            }
        }
        /// <summary>
        /// //指向最后一个记录
        /// </summary>
        private void toolStripEndRecord_Click(object sender, System.EventArgs e)
        {
            dataGridView.ClearSelection();
            if (dataGridView.Rows.Count > 1)
            {
                dataGridView.Rows[dataGridView.Rows.Count - 1].Selected = true;
                if (dataGridView.SelectedRows.Count != 0)
                {
                    List<int> ids = new List<int>();
                    for (int i = 0; i < dataGridView.SelectedRows.Count; i++)
                        ids.Add(int.Parse(dataGridView.SelectedRows[i].Cells[0].Value.ToString()));
                    ids.Sort();
                    layer.SelectByIds(ids);
                    if (SelectFeatureChanged != null)
                        SelectFeatureChanged(ids, layerIndex);
                }
                dataGridView.FirstDisplayedScrollingRowIndex = dataGridView.Rows.Count - 1;
                toolStripLblStatus.Text = "(1 out of " + ShowedRecordNum.ToString() + " Selected)";
            }
        }
        /// <summary>
        /// //显示所有数据
        /// </summary>
        private void toolStripButtonShowAllRecords_Click(object sender, System.EventArgs e)
        {
            DataTable dt = (DataTable)dataGridView.DataSource;
            dt.Dispose();
            dataGridView.DataSource = totalRecords.Copy();
            ShowedRecordNum = totalRecords.Rows.Count;
            toolStripLblStatus.Text = "(" + dataGridView.SelectedRows.Count.ToString() + " out of " + ShowedRecordNum.ToString() + " Selected)";
        }
        /// <summary>
        /// //显示选择的要素
        /// </summary>
        private void toolStripButtonShowSelectedRecords_Click(object sender, System.EventArgs e)
        {
            for (int i = 0; i < dataGridView.Rows.Count;)
                if (!dataGridView.Rows[i].Selected)
                    dataGridView.Rows.RemoveAt(i);
                else
                    i++;
            ShowedRecordNum = dataGridView.Rows.Count;
            toolStripLblStatus.Text = "(" + ShowedRecordNum.ToString() + " out of " + ShowedRecordNum.ToString() + " Selected)";
        }
        /// <summary>
        /// //键盘敲击事件
        /// </summary>
        private void PropertyTableForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 8 && (dataGridView.SelectedRows.Count != 0 || dataGridView.SelectedColumns.Count != 0))
                DeleteSelection();
            else if (e.KeyChar == 13 && toolStripTxtBoxPage.Focused)
                HandlePageText();
            else if (dataGridView.SelectedCells.Contains(dataGridView.CurrentCell) && dataGridView.SelectedCells.Count == 1)
            {
                string newValue = string.Empty;
                string oldValue = dataGridView.CurrentCell.Value.ToString();
                if (e.KeyChar == 8)
                    newValue = oldValue.Length == 0 ? string.Empty : oldValue.Substring(0, oldValue.Length - 1);
                else
                    newValue = oldValue + e.KeyChar;
                int index = dataGridView.CurrentCell.ColumnIndex;
                if (index == 0)
                    return;
                Type type = dataGridView.Columns[index].ValueType;
                if (type == typeof(int))
                {
                    if (newValue == string.Empty)
                        dataGridView.CurrentCell.Value = DBNull.Value;
                    else
                    {
                        int itemp;
                        if (int.TryParse(newValue, out itemp))
                            dataGridView.CurrentCell.Value = itemp;
                    }
                }
                else if (type == typeof(string))
                {
                    dataGridView.CurrentCell.Value = newValue;
                }
                else if (type == typeof(double))
                {
                    if (newValue != string.Empty && newValue[newValue.Length - 1] == '.' && e.KeyChar == 8)
                        newValue = newValue.Substring(0, newValue.Length - 1);
                    else if (newValue != string.Empty && newValue[newValue.Length - 1] == '.' && e.KeyChar != 8)
                        newValue += "001";
                    else
                    {
                        int tempindex = newValue.IndexOf(".001");
                        if (tempindex != -1)
                            newValue = newValue.Substring(0, tempindex) + "." + newValue[newValue.Length - 1];
                    }
                    if (newValue == string.Empty)
                        dataGridView.CurrentCell.Value = DBNull.Value;
                    double iftemp;
                    if (double.TryParse(newValue, out iftemp))
                        dataGridView.CurrentCell.Value = iftemp;
                }
            }
        }
        /// <summary>
        /// //选择行或者列
        /// </summary>
        private void dataGridView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                dataGridView.ClearSelection();
                if (e.RowIndex == -1 && e.ColumnIndex != -1)
                {
                    dataGridView.SelectionMode = DataGridViewSelectionMode.ColumnHeaderSelect;
                    dataGridView.Rows[e.ColumnIndex].Selected = true;
                }
                else if (e.RowIndex != -1 && e.ColumnIndex == -1)
                    dataGridView.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
            }
        }
        /// <summary>
        /// //DataGridView鼠标弹起事件
        /// </summary>
        private void dataGridView_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            toolStripLblStatus.Text = "(" + dataGridView.SelectedRows.Count.ToString() + " out of " + ShowedRecordNum.ToString() + " Selected)";
            if (dataGridView.SelectedRows.Count != 0)
            {
                List<int> ids = new List<int>();
                for (int i = 0; i < dataGridView.SelectedRows.Count; i++)
                    ids.Add(int.Parse(dataGridView.SelectedRows[i].Cells[0].Value.ToString()));
                ids.Sort();
                layer.SelectByIds(ids);
                if (SelectFeatureChanged != null)
                    SelectFeatureChanged(ids, layerIndex);
            }
        }

        public delegate void SelectFeatureChangedDelegate(List<int> ids, int layerIndex);
        public SelectFeatureChangedDelegate SelectFeatureChanged;
        /// <summary>
        /// //在退出窗口时保存属性表
        /// </summary>
        private void PropertyPanel_FormClosing(object sender, FormClosingEventArgs e)
        {
            property.SavaProperties(totalRecords);
            if (ClosingFormCompleted != null)
                ClosingFormCompleted(Width);
        }
        /// <summary>
        /// //关闭窗口事件
        /// </summary>
        public delegate void ClosingFormDelegate(int width);
        public ClosingFormDelegate ClosingFormCompleted;
        #endregion

        #region Methods
        /// <summary>
        /// //删除行或者列
        /// </summary>
        private void DeleteSelection()
        {
            DataTable dt = (DataTable)dataGridView.DataSource;
            if (dataGridView.SelectedColumns.Count != 0)
            {
                if (MessageBox.Show("确认删除列", "Cosmos GIS", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.OK)
                {
                    for (int i = 0; i < dataGridView.Columns.Count;)
                        if (dataGridView.Columns[i].Selected)
                        {
                            dt.Columns.RemoveAt(i);
                            totalRecords.Columns.RemoveAt(i);
                        }
                        else
                            i++;
                    dataGridView.ClearSelection();
                }
            }          
        }
        /// <summary>
        /// //处理页码文本
        /// </summary>
        private void HandlePageText()
        {
            string text = toolStripTxtBoxPage.Text;
            int pagenum;
            if (!int.TryParse(text, out pagenum))
            {
                MessageBox.Show("请输入数字");
                toolStripTxtBoxPage.Text = dataGridView.Rows.Count == 0 ? "0" : "1";
            }
            else
            {
                if (pagenum > 0 && pagenum <= dataGridView.Rows.Count)
                    dataGridView.Rows[pagenum - 1].Selected = true;
                else
                {
                    MessageBox.Show("没有找到该记录");
                    toolStripTxtBoxPage.Text = dataGridView.Rows.Count == 0 ? "0" : "1";
                }
            }
        }
        public void SetSelections(List<int> ids=null)
        {
            if(ids!=null)
            {
                selectionIDs.Clear();
                selectionIDs.AddRange(ids);
            }
            dataGridView.ClearSelection();
            selectionIDs.Sort();
            int i = 0, j = 0;
            for (i = 0; i < selectionIDs.Count; i++)
                for (; j < dataGridView.RowCount; j++)
                    if (int.Parse(dataGridView.Rows[j].Cells[0].Value.ToString()) == selectionIDs[i])
                    {
                        dataGridView.Rows[j].Selected = true;
                        dataGridView.FirstDisplayedScrollingRowIndex = j;
                        break;
                    }
            toolStripLblStatus.Text = "(" + dataGridView.SelectedRows.Count.ToString() + " out of " + ShowedRecordNum.ToString() + " Selected)";
        }
        #endregion
    }
}
