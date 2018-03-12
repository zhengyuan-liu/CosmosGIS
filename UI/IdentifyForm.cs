using System.Data;
using System.Windows.Forms;
using CosmosGIS.Geometry;
using CosmosGIS.Map;
using System;
using System.Drawing;

namespace CosmosGIS.UI
{
    /// <summary>
    /// //Identify面板
    /// </summary>
    public partial class IdentifyForm : Form
    {
        #region Members
        /// <summary>
        /// //鼠标点位置
        /// </summary>
        private Point mousePoint;
        #endregion

        #region Constructors
        /// <summary>
        /// 默认构造函数
        /// </summary>
        internal IdentifyForm(MyMap map,Point mousePoint)
        {
            InitializeComponent();
            this.mousePoint = mousePoint;
            for (int i = 0; i < map.LayerNum; i++)
                comboBoxLayers.Items.Add(map.GetLayerName(i));
            comboBoxLayers.Items.Add("<Top-most layer>");
            comboBoxLayers.SelectedIndex = map.LayerNum;
        }
        #endregion

        #region Events
        /// <summary>
        /// //窗口显示事件
        /// </summary>
        private void IdentifyForm_Shown(object sender, EventArgs e)
        {
            if (IdentifyLayerChanged != null)
                IdentifyLayerChanged(this, 0, mousePoint);
        }
        /// <summary>
        /// //窗口关闭事件
        /// </summary>
        private void IdentifyForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Dispose();
        }
        /// <summary>
        /// //图层改变事件
        /// </summary>
        private void comboBoxLayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = 0;
            if (comboBoxLayers.SelectedIndex == comboBoxLayers.Items.Count - 1)
                index = 0;
            else
                index = comboBoxLayers.SelectedIndex;
            if (IdentifyLayerChanged != null)
                IdentifyLayerChanged(this, index, mousePoint);
        }
        public delegate void IdentifyLayerFeatureDelegate(object form, int index, Point mousePoint);
        public event IdentifyLayerFeatureDelegate IdentifyLayerChanged;
        #endregion

        #region Methods
        /// <summary>
        /// 更新数据
        /// </summary>
        internal void UpdateData(DataTable dt, MyPoint mousePos)
        {
            txtLocation.Text = mousePos.X.ToString() + "," + mousePos.Y.ToString();
            dataGridView.DataSource = dt;
            int index = 0;
            if (comboBoxLayers.SelectedIndex == comboBoxLayers.Items.Count - 1)
                index = 0;
            else
                index = comboBoxLayers.SelectedIndex;
            treeViewFeature.Nodes.Clear();
            treeViewFeature.Nodes.Add(comboBoxLayers.Items[index].ToString());
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                    treeViewFeature.Nodes[0].Nodes.Add("FID" + dt.Rows[i][0].ToString());
                this.toolStripStatusLabel.Text = "Identified " + dt.Rows.Count.ToString() + " feature";
            }
            else
                this.toolStripStatusLabel.Text = "Identified 0 feature";
            treeViewFeature.ExpandAll();          
        }
        #endregion
    }
}
