using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace CosmosGIS.UI
{
    /// <summary>
    /// //地图缩略图空间
    /// </summary>
    public partial class BriefMapControl : UserControl
    {
        #region Attributes
        /// <summary>
        /// //cgm地图文件位置
        /// </summary>
        private string cgmFilePath;
        /// <summary>
        /// //是否需要保存
        /// </summary>
        private bool needSave;
        #endregion

        #region Constructors
        /// <summary>
        /// //新建地图
        /// </summary>
        public BriefMapControl()
        {
            InitializeComponent();
            cgmFilePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Cosmos\\DefaultMaps\\无标题.cgm";
            lblName.Text = Path.GetFileNameWithoutExtension(cgmFilePath);
            needSave = true;
            BackColor = lblName.BackColor = SystemColors.ActiveCaption;
        }
        /// <summary>
        /// //根据文件打开地图
        /// </summary>
        public BriefMapControl(string cgmFilePath)
        {
            InitializeComponent();
            this.cgmFilePath = cgmFilePath;
            lblName.Text = Path.GetFileNameWithoutExtension(cgmFilePath);
            needSave = false;
        }
        #endregion

        #region Events
        /// <summary>
        /// //改变背景颜色
        /// </summary>
        public void ChangeBackColor_Click(object sender, EventArgs e)
        {
            BackColor = lblName.BackColor = SystemColors.ActiveCaption;
            if (RecoverBackColorEvent != null)
                RecoverBackColorEvent(cgmFilePath);
        }
        /// <summary>
        /// //确认选择地图双击事件
        /// </summary>
        private void GetSelectMap_DoubleClick(object sender, EventArgs e)
        {
            if (GetSelectMapEvent != null)
                GetSelectMapEvent(cgmFilePath, needSave);
        }
        /// <summary>
        /// //恢复其他地图缩略控件背景颜色
        /// </summary>
        public delegate void RecoverBackColorDelegate(string cgmFilePath);
        public event RecoverBackColorDelegate RecoverBackColorEvent;
        /// <summary>
        /// //确认选择地图文件事件委托
        /// </summary>
        public delegate void GetSelectMapDelegate(string cgmFilePath, bool needSave);
        public event GetSelectMapDelegate GetSelectMapEvent;
        #endregion

        #region Members
        /// <summary>
        /// //恢复背景颜色
        /// </summary>
        public void SetBackColor(Color color)
        {
            BackColor = lblName.BackColor = color;
        }
        #endregion
    }
}
