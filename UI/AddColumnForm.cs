using System;
using System.Windows.Forms;

namespace CosmosGIS.UI
{
    /// <summary>
    /// //新增字段窗口
    /// </summary>
    public partial class AddColumnForm : Form
    {
        #region Members
        /// <summary>
        /// //字段名
        /// </summary>
        private string fieldName;
        /// <summary>
        /// //类型索引
        /// </summary>
        private int typeIndex = 0;
        /// <summary>
        /// //类型TODO（根据实际需要修改，还要去面板修改）
        /// </summary>
        private Type[] fieldTypes = { typeof(int), typeof(double), typeof(string), typeof(DateTime), typeof(char), typeof(bool) };
        #endregion

        #region Attributes
        /// <summary>
        /// //获取字段名称
        /// </summary>
        public string FieldName { get { return fieldName; } }
        /// <summary>
        /// //获取字段类型
        /// </summary>
        public Type FieldType { get { return fieldTypes[typeIndex]; } }
        #endregion

        #region Constructors
        /// <summary>
        /// //构造函数
        /// </summary>
        public AddColumnForm()
        {
            InitializeComponent();
            comboBoxType.SelectedIndex = 0;
        }
        #endregion

        #region Events
        /// <summary>
        /// //关闭窗口，OK按钮响应事件，DialogResult结果为OK
        /// </summary>
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtBoxName.Text.Trim() == "")
                MessageBox.Show("字段名称不能为空！");
            else
            {
                DialogResult = DialogResult.OK;
                Dispose();
            }
        }
        /// <summary>
        /// //关闭窗口，Cancel按钮响应事件，DialogResult结果为Cancel
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Dispose();
        }
        /// <summary>
        /// //更改类型选择
        /// </summary>
        private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            typeIndex = comboBoxType.SelectedIndex;
        }
        /// <summary>
        /// ///更改字段名称
        /// </summary>
        private void txtBoxName_TextChanged(object sender, EventArgs e)
        {
            fieldName = txtBoxName.Text;
        }
        #endregion
    }
}
