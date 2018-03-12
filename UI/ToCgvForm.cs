using System;
using System.Windows.Forms;
using CosmosGIS.Map;
using System.Threading;

namespace CosmosGIS.UI
{
    /// <summary>
    /// 将指定图层转换为cgv文件
    /// </summary>
    public partial class ToCgvForm : Form
    {
        #region Members
        /// <summary>
        /// //地图
        /// </summary>
        private MyMap map;
        /// <summary>
        /// 文件保存路径
        /// </summary>
        private string cgvDirectory;
        #endregion

        #region Contructors
        /// <summary>
        /// //默认构造函数
        /// </summary>
        internal ToCgvForm(MyMap map,string path)
        {
            InitializeComponent();
            this.map = map;
            for (int i = 0; i < map.LayerNum; i++)
                comboBoxLayers.Items.Add(map.Layers[i].LayerName);
            if (map.LayerNum != 0)
            {
                comboBoxLayers.SelectedIndex = 0;
                cgvDirectory = path;
                tbPath.Text = cgvDirectory + "\\" + comboBoxLayers.SelectedItem.ToString() + ".cgv";
            }
        }
        #endregion

        #region Events
        /// <summary>
        /// 浏览
        /// </summary>
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "输出路径";
            sfd.InitialDirectory = cgvDirectory;
            sfd.FileName = comboBoxLayers.SelectedItem.ToString();
            sfd.RestoreDirectory = true;
            sfd.AddExtension = true;
            sfd.Filter = "CGV文件(*.cgv)|*.cgv";
            if (sfd.ShowDialog(this) == DialogResult.OK)
                tbPath.Text = sfd.FileName;
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
            Thread thread = new Thread(new ParameterizedThreadStart(map.Layers[comboBoxLayers.SelectedIndex].SpaceData.SaveCGV));
            thread.Start(tbPath.Text);
            Dispose();
        }
        /// <summary>
        /// 取消
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Dispose();
        }
        /// <summary>
        /// 选择图层索引改变
        /// </summary>
        private void comboBoxLayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (map.Layers[comboBoxLayers.SelectedIndex].DataType != MySpaceDataType.MyGrid)
            {
                btnOK.Enabled = true;
                if (comboBoxLayers.SelectedIndex != -1)
                {
                    btnBrowse.Enabled = true;
                    tbPath.Text = cgvDirectory + "\\" + comboBoxLayers.SelectedItem.ToString() + ".cgv";
                }
                else
                    btnBrowse.Enabled = false;
            }
            else
            {
                MessageBox.Show("不能导出栅格图层!");
                btnOK.Enabled = false;
            }
        }
        #endregion
    }
}
