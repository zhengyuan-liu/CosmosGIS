using System;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace CosmosGIS.UI
{
    public partial class ExportMapForm : Form
    {
        #region Members
        /// <summary>
        /// 改动前的dpi
        /// </summary>
        private int DPI = 96;
        /// <summary>
        /// 保存图像宽度
        /// </summary>
        private double imgWidth;
        /// <summary>
        /// 保存图像高度
        /// </summary>
        private double imgHeight;
        /// <summary>
        /// 保存图像格式
        /// </summary>
        private ImageFormat imgFormat = ImageFormat.Png;
        /// <summary>
        /// 保存文件路径
        /// </summary>
        private string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\无标题.png";
        #endregion

        #region Attributes
        /// <summary>
        /// 获取图像宽度
        /// </summary>
        internal int ImgWidth { get { return (int)imgWidth; } }
        /// <summary>
        /// 获取图像高度
        /// </summary>
        internal int ImgHeight { get { return (int)imgHeight; } }
        /// <summary>
        /// 获取保存格式
        /// </summary>
        internal ImageFormat ImgFormat { get { return imgFormat; } }
        /// <summary>
        /// 获取保存文件路径
        /// </summary>
        internal string FilePath { get { return filePath; } }
        #endregion

        #region Constructor
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public ExportMapForm(int width = 0, int height = 0)
        {
            InitializeComponent();
            imgWidth = width;
            imgHeight = height;
            tbWidth.Text = ImgWidth.ToString();
            tbHeight.Text = ImgHeight.ToString();
            tbExportPath.Text = filePath;
        }
        #endregion

        #region Events
        /// <summary>
        /// dpi改变事件
        /// </summary>
        private void numericUpDownDpi_ValueChanged(object sender, EventArgs e)
        {
            imgWidth = imgWidth * (int)numericUpDownDpi.Value/ DPI;
            imgHeight = imgHeight * (int)numericUpDownDpi.Value / DPI;
            tbWidth.Text = ImgWidth.ToString();
            tbHeight.Text = ImgHeight.ToString();
            DPI = (int)numericUpDownDpi.Value;
        }
        /// <summary>
        /// 浏览按钮
        /// </summary>
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            sfd.FileName = "无标题.png";
            sfd.Title = "导出地图";
            sfd.Filter = "PNG(*.png)|*.png|JPEG(*.jpg)|*.jpg|BMP(*.bmp)|*.bmp|TIFF(*.tiff)|*.tiff";
            if (sfd.ShowDialog(this) == DialogResult.OK)
            {
                if (sfd.FilterIndex == 1)
                    imgFormat = ImageFormat.Png;
                else if (sfd.FilterIndex == 2)
                    imgFormat = ImageFormat.Jpeg;
                else if (sfd.FilterIndex == 3)
                    imgFormat = ImageFormat.Bmp;
                else if (sfd.FilterIndex == 4)
                    imgFormat = ImageFormat.Tiff;            
                tbExportPath.Text = filePath = sfd.FileName;
            }
        }
        /// <summary>
        /// 保存按钮
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (filePath != string.Empty)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else
                MessageBox.Show("导出路径不能为空！");
        }
        /// <summary>
        /// 取消按钮
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
        #endregion
    }
}
