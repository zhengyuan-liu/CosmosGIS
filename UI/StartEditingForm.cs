using System;
using System.Windows.Forms;
using CosmosGIS.Map;

namespace CosmosGIS.UI
{
    public partial class StartEditingForm : Form
    {
        #region Members
        /// <summary>
        /// 选中的图层索引
        /// </summary>
        int selectedLayerIndex = -1;
        MyMap map;
        #endregion

        #region Constructors
        /// <summary>
        /// 唯一构造函数，必须传入Map
        /// </summary>
        internal StartEditingForm(MyMap map)
        {
            InitializeComponent();
            this.map = map;
            for (int i = 0; i < map.LayerNum; i++)
                listBoxLayers.Items.Add(map.Layers[i].LayerName);              
        }
        #endregion

        #region Attributes
        /// <summary>
        /// 用户选择的要编辑图层的索引号
        /// </summary>
        public int SelectedLayerIndex
        {
            get { return selectedLayerIndex; }
            set { selectedLayerIndex = value; }
        }
        #endregion

        #region Events
        /// <summary>
        /// 选择图层发生改变
        /// </summary>
        private void listBoxLayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxLayers.SelectedIndex != -1 && map.Layers[listBoxLayers.SelectedIndex].DataType != MySpaceDataType.MyGrid)  //不能编辑栅格图层
            {
                if (btnOK.Enabled == false && listBoxLayers.SelectedIndex != -1)
                    btnOK.Enabled = true;
                selectedLayerIndex = listBoxLayers.SelectedIndex;
            }
            else
                MessageBox.Show("不能编辑栅格图层!");
        }
        /// <summary>
        /// 确定
        /// </summary>
        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
        /// <summary>
        /// 取消
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
        #endregion
    }
}
