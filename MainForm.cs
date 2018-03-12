using System;
using System.Drawing;
using System.Windows.Forms;
using CosmosGIS.UI;
using CosmosGIS.FileReader;
using System.IO;
using System.Collections.Generic;
using CosmosGIS.MapReader;
using CosmosGIS.Grid;

namespace CosmosGIS
{
    /// <summary>
    /// 主界面，容器和菜单
    /// </summary>
    public partial class MainForm : Form
    {
        #region Members
        /// <summary>
        /// //地图文件路径
        /// </summary>
        private string mapPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Cosmos\\DefaultMaps\\无标题.cgm";
        /// <summary>
        /// 默认数据库路径
        /// </summary>
        private string databasePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Cosmos\\DefaultDatabases";
        /// <summary>
        /// 正在编辑的图层索引号
        /// </summary>
        private int editingLayerIndex = -1;
        #endregion

        #region Constructors
        /// <summary>
        /// //默认构造函数
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            if (Properties.Settings.Default.ShowGettingStart)
                Shown += new EventHandler(NewToolStripMenuItem_Click);
            string MapPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Cosmos\\DefaultMaps\\无标题.cgm";
            string DatabasePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Cosmos\\DefaultDatabases";
            if (!Directory.Exists(Path.GetDirectoryName(MapPath)))
                Directory.CreateDirectory(Path.GetDirectoryName(MapPath));
            if (!File.Exists(mapPath))
                CGM.CreateEmptyCgv(mapPath);
            if (!Directory.Exists(DatabasePath))
                Directory.CreateDirectory(DatabasePath);
        }
        #endregion

        #region FileMenuEvent
        /// <summary>
        /// //新建
        /// </summary>
        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GettingStartedForm form = new GettingStartedForm();
            form.StartPosition = FormStartPosition.CenterParent;
            if (form.ShowDialog() == DialogResult.OK)
            {
                mapPath = form.MapPath;
                databasePath = form.DatabasePath;
                cosmosMapPanel.GenerateMapFromCGM(mapPath);
            }
            form.Dispose();
        }
        /// <summary>
        /// //打开
        /// </summary>
        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Cosmos\\DefaultMaps";
            openFileDialog.Filter = "地图文件|*.cgm";
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                mapPath = openFileDialog.FileName;
                cosmosMapPanel.GenerateMapFromCGM(mapPath);
            }
            openFileDialog.Dispose();
        }
        /// <summary>
        /// //保存
        /// </summary>
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                cosmosMapPanel.SaveCGM(mapPath);
                MessageBox.Show("保存成功", "CosmosGIS", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "出现错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// 另存为
        /// </summary>
        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter= "地图文件|*.cgm";          
            sfd.FileName = "无标题";
            sfd.DefaultExt = "cgm";
            sfd.AddExtension = true;
            sfd.RestoreDirectory = true;
            if (sfd.ShowDialog(this) == DialogResult.OK)
            {
                mapPath = sfd.FileName;
                try
                {
                    cosmosMapPanel.SaveCGM(mapPath);
                    MessageBox.Show("保存成功", "CosmosGIS", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "出现错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            sfd.Dispose();
        }
        /// <summary>
        /// 导入数据
        /// </summary>
        private void AddDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "导入数据";
            ofd.Filter = "地图矢量文件(*.cgv)|*.cgv|Shapefile(*.shp)|*.shp|栅格文件(*.bmp;*.png;*.jpg)|*.bmp;*.png;*.jpg";
            ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\CosmosCosmos\\DefaultDatabases";
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog(this) == DialogResult.OK)
            {
                string fileName = ofd.FileName;
                if (ofd.FilterIndex == 1)  //CGV
                {
                    CosmosGisVector cgv = new CosmosGisVector(fileName);
                    cosmosMapPanel.AddLayer(cgv);
                }
                else if (ofd.FilterIndex == 2)  //shp
                {
                    Shapefile shp = new Shapefile(fileName);
                    cosmosMapPanel.AddLayer(shp);
                }
                else  //栅格文件
                {
                    MyGrid grid = new MyGrid(fileName);
                    cosmosMapPanel.AddLayer(grid);
                }
            }
            ofd.Dispose();
        }
        
        /// <summary>
        /// 导出地图
        /// </summary>
        private void ExportMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportMapForm exportMapFrom = new ExportMapForm(cosmosMapPanel.MapImg.Width, cosmosMapPanel.MapImg.Height);
            if(exportMapFrom.ShowDialog(this)== DialogResult.OK)
            {
                if (cosmosMapPanel.IsEdit == true)
                {
                    Bitmap bmp = new Bitmap(exportMapFrom.ImgWidth, exportMapFrom.ImgHeight);
                    cosmosMapPanel.SaveMapImg(bmp, exportMapFrom.FilePath, exportMapFrom.ImgFormat);
                }
                else
                    cosmosMapPanel.SaveSpecialMap(exportMapFrom.FilePath);
            }
            exportMapFrom.Dispose();
        }
        /// <summary>
        /// 退出
        /// </summary>
        private void ExitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

        #region EditMenuEvent
        /// <summary>
        /// 新建CGV要素
        /// </summary>
        private void NewCGVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateNewCGVDialog newCgvDialog = new CreateNewCGVDialog(databasePath);
            if (newCgvDialog.ShowDialog(this) == DialogResult.OK)
            {
                CosmosGisVector.CreateEmptyCgv(newCgvDialog.FilePath, newCgvDialog.SpaceDataType);
                CosmosGisVector emptyCgv = new CosmosGisVector(newCgvDialog.FilePath);
                cosmosMapPanel.AddLayer(emptyCgv);
            }
            newCgvDialog.Dispose();
        }
        #endregion

        #region ViewMenuEvent
        /// <summary>
        /// 进入Data视图
        /// </summary>
        private void dataViewMenuItem_Click(object sender, EventArgs e)
        {
            cosmosMapPanel.toolStripDataView_Click(sender, e);
        }
        /// <summary>
        /// 进入Layout视图
        /// </summary>
        private void layoutViewMenuItem_Click(object sender, EventArgs e)
        {
            cosmosMapPanel.toolStripLayoutView_Click(sender, e);
        }
        /// <summary>
        /// 放大
        /// </summary>
        private void ZoomInMenuItem_Click(object sender, EventArgs e)
        {
            cosmosMapPanel.ZoomInMode();
        }
        /// <summary>
        /// 缩小
        /// </summary>
        private void ZoomOutMenuItem_Click(object sender, EventArgs e)
        {
            cosmosMapPanel.ZoomOutMode();
        }
        /// <summary>
        /// 漫游
        /// </summary>
        private void PanMenuItem_Click(object sender, EventArgs e)
        {
            cosmosMapPanel.PanMode();
        }
        /// <summary>
        /// 显示全图
        /// </summary>
        private void fullExtentMenuItem_Click(object sender, EventArgs e)
        {
            cosmosMapPanel.FullExtent();
        }
        #endregion

        #region InsertMenuEvent
        /// <summary>
        /// 标题
        /// </summary>
        private void titleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cosmosMapPanel.AddNewTitle();
        }
        /// <summary>
        /// //文本
        /// </summary>
        private void textToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cosmosMapPanel.AddText();
        }
        /// <summary>
        /// 图廓
        /// </summary>
        private void neatlineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cosmosMapPanel.ChangeNeatLine();
        }
        /// <summary>
        /// 图例
        /// </summary>
        private void legendToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cosmosMapPanel.AddLegendSymbol();
        }
        /// <summary>
        /// 指南针
        /// </summary>
        private void northArrowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cosmosMapPanel.AddNewCompass();
        }
        /// <summary>
        /// 比例尺
        /// </summary>
        private void scaleBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cosmosMapPanel.AddScaleBar();
        }
        #endregion

        #region ChoiceMenuEvent
        /// <summary>
        /// 根据属性进行条件查询
        /// </summary>
        private void selectByAttributesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < OwnedForms.Length; i++)
                if (OwnedForms[i].GetType() == typeof(SelectByAttributesForm))
                {
                    Form form = OwnedForms[i];
                    RemoveOwnedForm(form);
                    form.Dispose();
                }
            SelectByAttributesForm sbaForm = new SelectByAttributesForm(cosmosMapPanel.Map);
            sbaForm.SelectionChanged += new SelectByAttributesForm.SelectionChangedDelegate(sbaForm_SelectionChanged);
            sbaForm.Show(this);
        }
        private void sbaForm_SelectionChanged(List<int> ids, int layerIndex)
        {
            cosmosMapPanel.changeSelectionFeatures(ids, layerIndex);
        }
        /// <summary>
        /// 清空选择
        /// </summary>
        private void clearSelectedFeatureMenuItem_Click(object sender, EventArgs e)
        {
            cosmosMapPanel.ClearSelection();
        }
        #endregion

        #region ToolMenuEvent
        private void toolStripButtonSelectElements_Click(object sender, EventArgs e)
        {
            cosmosMapPanel.SelectElementMode();
        }
        /// <summary>
        /// Identify
        /// </summary>
        private void identifyMenuItem_Click(object sender, System.EventArgs e)
        {
            cosmosMapPanel.IdentifyMode();
        }
        /// <summary>
        /// 将指定图层输出为CGV文件
        /// </summary>
        private void toCGVMenuItem_Click(object sender, EventArgs e)
        {
            ToCgvForm cgvForm = new ToCgvForm(cosmosMapPanel.Map, databasePath);
            cgvForm.StartPosition = FormStartPosition.CenterParent;
            cgvForm.Show();
        }
        #endregion

        #region GeneralToolMenuEvent
        /// <summary>
        /// 通用工具
        /// </summary>
        private void toolStripButtonSelectFeatures_Click(object sender, EventArgs e)
        {
            cosmosMapPanel.SelectFeaturesMode();
        }
        #endregion

        #region EditToolEvent
        /// <summary>
        /// 开始编辑
        /// </summary>
        private void startEditingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartEditingForm sef = new StartEditingForm(cosmosMapPanel.Map);
            if (sef.ShowDialog(this) == DialogResult.OK)
            {
                editingLayerIndex = sef.SelectedLayerIndex;
                cosmosMapPanel.StartEditMode(editingLayerIndex);
                stopEditingToolStripMenuItem.Enabled = true;
                saveEditsToolStripMenuItem.Enabled = true;
                toolStripButtonEditTool.Enabled = true;
                toolStripBtnEditVertices.Enabled = true;
                toolStripButtonCreateFeatures.Enabled = true;
                toolStripBtnDeleteFeature.Enabled = true;
                startEditingToolStripMenuItem.Enabled = false;
            }
        }
        /// <summary>
        /// 停止编辑
        /// </summary>
        private void stopEditingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("是否保存编辑？", "保存", MessageBoxButtons.YesNoCancel);
            if (result == DialogResult.Cancel)  //用户选择取消
                return;
            if (result == DialogResult.Yes)  //用户选择保存
                cosmosMapPanel.SaveEdit();
            cosmosMapPanel.EndEditMode();
            startEditingToolStripMenuItem.Enabled = true;
            stopEditingToolStripMenuItem.Enabled = false;
            saveEditsToolStripMenuItem.Enabled = false;
            toolStripButtonEditTool.Enabled = false;
            toolStripBtnEditVertices.Enabled = false;
            toolStripButtonCreateFeatures.Enabled = false;
            toolStripBtnDeleteFeature.Enabled = false;
        }
        /// <summary>
        /// 保存编辑
        /// </summary>
        private void saveEditsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cosmosMapPanel.SaveEdit();
        }
        /// <summary>
        /// 编辑要素
        /// </summary>
        private void toolStripButtonEditTool_Click(object sender, EventArgs e)
        {
            cosmosMapPanel.EditMode(editingLayerIndex);
        }
        /// <summary>
        /// 编辑顶点
        /// </summary>
        private void toolStripBtnEditVertices_Click(object sender, EventArgs e)
        {
            cosmosMapPanel.EditVerticesMode();
        }
        /// <summary>
        /// 创建要素
        /// </summary>
        private void toolStripButtonCreateFeatures_Click(object sender, EventArgs e)
        {
            cosmosMapPanel.CreateFeatureMode(editingLayerIndex);
        }
        /// <summary>
        /// 删除选中的要素
        /// </summary>
        private void toolStripBtnDeleteFeature_Click(object sender, EventArgs e)
        {
            cosmosMapPanel.DeleteFeature(editingLayerIndex);
        }
        #endregion


    }
}
