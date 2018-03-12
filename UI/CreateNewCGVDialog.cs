using System;
using System.Windows.Forms;
using System.IO;
using CosmosGIS.Map;

namespace CosmosGIS.UI
{
    public partial class CreateNewCGVDialog : Form
    {
        #region Members
        /// <summary>
        /// 要素名称
        /// </summary>
        private string featureName = "New_CGV";
        /// <summary>
        /// 要素文件路径
        /// </summary>
        private string featurePath;
        /// <summary>
        /// 要素空间数据类型
        /// </summary>
        private MySpaceDataType spaceDataType = MySpaceDataType.MyPoint;
        #endregion

        #region Attributes
        /// <summary>
        /// 新建CGV文件的名称
        /// </summary>
        public string FeatureName
        {
            get { return featureName; }
        }
        /// <summary>
        /// 新建CGV文件的路径
        /// </summary>
        public string FilePath
        {
            get { return featurePath + "\\" + featureName + ".cgv"; }
        }
        /// <summary>
        /// 要素类型
        /// </summary>
        public MySpaceDataType SpaceDataType
        {
            get { return spaceDataType; }
        }
        #endregion

        #region Constructors
        public CreateNewCGVDialog(string databasePath)
        {
            InitializeComponent();
            comboBoxType.SelectedIndex = 0;
            featurePath = databasePath;
            tbName.Text = FeatureName;
            tbPath.Text = FilePath;
        }
        #endregion

        #region Events
        /// <summary>
        /// //文件名改变
        /// </summary>
        private void tbName_TextChanged(object sender, EventArgs e)
        {
            featureName = tbName.Text;
            tbPath.Text = FilePath;
        }
        /// <summary>
        /// 浏览
        /// </summary>
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Create New CGV";
            sfd.FileName = FeatureName;
            sfd.Filter = "CGV文件(*.cgv)|*.cgv";
            sfd.AddExtension = true;
            if (sfd.ShowDialog(this) == DialogResult.OK)
            {
                featurePath = Path.GetDirectoryName(sfd.FileName);
                featureName = Path.GetFileNameWithoutExtension(sfd.FileName);
                tbName.Text = FeatureName;
                tbPath.Text = FilePath;
            }
        }
        /// <summary>
        /// 确定
        /// </summary>
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (tbPath.Text == string.Empty)
            {
                MessageBox.Show("路径不能为空!", "CosmosGIS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DialogResult = DialogResult.OK;
        }
        /// <summary>
        /// 取消
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
        /// <summary>
        /// //选择空间数据类型
        /// </summary>
        private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBoxType.SelectedIndex)
            {
                case 0:
                    spaceDataType = MySpaceDataType.MyPoint;
                    break;
                case 1:
                    spaceDataType = MySpaceDataType.MyPolyLine;
                    break;
                case 2:
                    spaceDataType = MySpaceDataType.MyPolygon;
                    break;
            }
        }
        #endregion
    }
}
