using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace CosmosGIS.UI
{
    /// <summary>
    /// //开始选择cgm界面
    /// </summary>
    public partial class GettingStartedForm : Form
    {
        #region Members
        /// <summary>
        /// //配置文件
        /// </summary>
        private readonly string configFile = Directory.GetParent(Application.StartupPath) + "\\cosmos.config";
        /// <summary>
        /// //地图文件集合
        /// </summary>
        private List<string> maps = new List<string>();
        /// <summary>
        /// //地理数据库文件集合
        /// </summary>
        private List<string> databases = new List<string>();
        #endregion

        #region Attributes
        /// <summary>
        /// //地图文件位置
        /// </summary>
        public string MapPath{ get; set; }
        /// <summary>
        /// //地理数据库位置
        /// </summary>
        public string DatabasePath { get; set; }
        #endregion

        #region Contructors
        /// <summary>
        /// //默认构造函数
        /// </summary>
        public GettingStartedForm()
        {
            InitializeComponent();
        }
        #endregion

        #region Events
        /// <summary>
        /// //窗口加载时触发
        /// </summary>
        private void GettingStartedForm_Load(object sender, EventArgs e)
        {
            MapPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Cosmos\\DefaultMaps\\无标题.cgm";
            DatabasePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Cosmos\\DefaultDatabases";
            maps.Add(MapPath);
            databases.Add(DatabasePath);
            comboBoxDatabasePath.Items.Add(DatabasePath);
            ReadConfiguration();
            AddBriefMapControls();
            lblMapPath.Text = MapPath;
            comboBoxDatabasePath.Text = DatabasePath;
        }
        /// <summary>
        /// //恢复其他缩略图控件背景颜色
        /// </summary>
        private void BriefMap_RecoverBackColor(string cgmFilePath)
        {
            for (int i = 0; i < maps.Count; i++)
                if (maps[i] != cgmFilePath)
                    ((BriefMapControl)tbLayoutPanel.Controls[i]).SetBackColor(System.Drawing.Color.White);
                else
                {
                    ((BriefMapControl)tbLayoutPanel.Controls[i]).SetBackColor(System.Drawing.SystemColors.ActiveCaption);
                    treeView.SelectedNode = null;
                }
            lblMapPath.Text = MapPath = cgmFilePath;
        }
        /// <summary>
        /// //选择地图控件
        /// </summary>
        private void BriefMap_GetSelectMap(string cgmFilePath, bool needSave)
        {
            MapPath = cgmFilePath;
            if (!needSave)
            {
                maps.Remove(cgmFilePath);
                maps.Insert(1, MapPath);
            }
            btnOK_Click(null, null);
        }
        /// <summary>
        /// //根据树节点选择地图文件
        /// </summary>
        private void treeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Level == 0 && e.Node.Index == 2)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Cosmos\\DefaultMaps";
                openFileDialog.Filter = "地图文件|*.cgm";
                openFileDialog.RestoreDirectory = true;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    MapPath = openFileDialog.FileName;
                    int index = maps.IndexOf(MapPath);
                    if (index != -1)
                    {
                        maps.RemoveAt(index);
                        treeView.Nodes[1].Nodes.RemoveAt(index - 1);
                    }
                    maps.Insert(1, MapPath);
                    treeView.Nodes[1].Nodes.Insert(0, new TreeNode(Path.GetFileNameWithoutExtension(MapPath)));
                    if (maps.Count > 6)
                    {
                        maps.RemoveAt(6);
                        treeView.Nodes[1].Nodes.RemoveAt(5);
                    }
                    AddBriefMapControls();
                    lblMapPath.Text = MapPath;
                    BriefMap_RecoverBackColor(MapPath);
                }
            }
            else if (e.Node.Level == 1)
            {
                int index = 0;
                if (e.Node.Parent == treeView.Nodes[0])
                    index = 0;
                else
                    index = e.Node.Index + 1;
                ((BriefMapControl)tbLayoutPanel.Controls[index]).ChangeBackColor_Click(sender, e);
            }
            treeView.ExpandAll();
        }
        /// <summary>
        /// //地理数据库文本框输入改变事件
        /// </summary>
        private void comboBoxDatabasePath_TextChanged(object sender, EventArgs e)
        {
            DatabasePath = comboBoxDatabasePath.Text;
        }
        /// <summary>
        /// //键盘敲击事件，处理文本输入完成后敲击回车
        /// </summary>
        private void GettingStartedForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (comboBoxDatabasePath.Focused && e.KeyChar == 13)
            {
                if (Directory.Exists(comboBoxDatabasePath.Text))
                {
                    DatabasePath = comboBoxDatabasePath.Text;
                    int index = databases.IndexOf(DatabasePath);
                    if (index != -1)
                    {
                        databases.RemoveAt(index);
                        comboBoxDatabasePath.Items.RemoveAt(index);
                    }
                    databases.Insert(1, DatabasePath);
                    comboBoxDatabasePath.Items.Insert(1, DatabasePath);
                    comboBoxDatabasePath.SelectedIndex = 1;
                    if (databases.Count > 6)
                    {
                        databases.RemoveAt(6);
                        comboBoxDatabasePath.Items.RemoveAt(6);
                    }
                }
                else
                    MessageBox.Show("无法打开所选数据库", "新建文档", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        /// <summary>
        /// //选择地理数据库按钮事件
        /// </summary>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择地理数据库";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                DatabasePath = dialog.SelectedPath;
                int index = databases.IndexOf(DatabasePath);
                if (index != -1)
                {
                    databases.RemoveAt(index);
                    comboBoxDatabasePath.Items.RemoveAt(index);
                }
                databases.Insert(1, DatabasePath);
                comboBoxDatabasePath.Items.Insert(1, DatabasePath);
                comboBoxDatabasePath.SelectedIndex = 1;
                if (databases.Count > 6)
                {
                    databases.RemoveAt(6);
                    comboBoxDatabasePath.Items.RemoveAt(6);
                }
            }
        }
        /// <summary>
        /// //地理数据库选择项改变事件
        /// </summary>
        private void comboBoxDatabasePath_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = comboBoxDatabasePath.SelectedIndex;
            if (index > 1)
            {
                DatabasePath = databases[index];
                databases.RemoveAt(index);
                comboBoxDatabasePath.Items.RemoveAt(index);
                databases.Insert(1, DatabasePath);
                comboBoxDatabasePath.Items.Insert(1, DatabasePath);
                comboBoxDatabasePath.SelectedIndex = 1;
            }
        }
        /// <summary>
        /// //确认选择按钮点击事件
        /// </summary>
        private void btnOK_Click(object sender, System.EventArgs e)
        {
            if (Directory.Exists(comboBoxDatabasePath.Text))
            {
                DialogResult = DialogResult.OK;
                SaveConfig();
                Close();
            }
            else
                MessageBox.Show("无法打开所选数据库", "新建文档", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        /// <summary>
        /// //取消选择按钮点击事件
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            MapPath = maps[0];
            DatabasePath = databases[0];
            Close();
        }
        /// <summary>
        /// 用户选择是否开始显示
        /// </summary>
        private void checkBoxShow_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.ShowGettingStart = !((CheckBox)sender).Checked;
            Properties.Settings.Default.Save();
        }
        #endregion

        #region Methods
        /// <summary>
        /// //读取配置文件
        /// </summary>
        private void ReadConfiguration()
        {
            if (!File.Exists(configFile))
                return;
            StreamReader sr = new StreamReader(configFile);
            string[] contents = sr.ReadToEnd().Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            sr.Dispose();
            string[] mapFiles = contents[0].Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            treeView.Nodes[0].Nodes.Add(new TreeNode(Path.GetFileNameWithoutExtension(maps[0])));
            for (int i = 0; i < mapFiles.Length; i++)
            {
                maps.Add(mapFiles[i]);
                treeView.Nodes[1].Nodes.Add(new TreeNode(Path.GetFileNameWithoutExtension(mapFiles[i])));
            }
            treeView.ExpandAll();
            string[] dbs = contents[1].Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < dbs.Length; i++)
            {
                databases.Add(dbs[i]);
                comboBoxDatabasePath.Items.Add(dbs[i]);
            }
        }
        /// <summary>
        /// //保存配置文件
        /// </summary>
        private void SaveConfig()
        {
            StreamWriter sw = new StreamWriter(configFile, false);
            for (int i = 1; i < maps.Count; i++)
                sw.Write(maps[i] + "|");
            sw.Write("\n");
            for (int i = 1; i < databases.Count; i++)
                sw.Write(databases[i] + "|");
            sw.Dispose();
        }
        /// <summary>
        /// //添加缩略地图控件集合
        /// </summary>
        private void AddBriefMapControls()
        {
            tbLayoutPanel.Controls.Clear();
            for (int i = 0; i < maps.Count; i++)
            {
                BriefMapControl control;
                if (i == 0)
                    control = new BriefMapControl();
                else
                    control = new BriefMapControl(maps[i]);
                control.Dock = DockStyle.Fill;
                control.RecoverBackColorEvent += new BriefMapControl.RecoverBackColorDelegate(BriefMap_RecoverBackColor);
                control.GetSelectMapEvent += new BriefMapControl.GetSelectMapDelegate(BriefMap_GetSelectMap);
                tbLayoutPanel.Controls.Add(control, i % 3, i / 3);
            }        
        }
        #endregion
    }
}