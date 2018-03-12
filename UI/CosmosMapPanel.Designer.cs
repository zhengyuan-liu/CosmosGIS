namespace CosmosGIS.UI
{
    partial class CosmosMapPanel
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CosmosMapPanel));
            this.treeViewLayers = new System.Windows.Forms.TreeView();
            this.imgListLayers = new System.Windows.Forms.ImageList(this.components);
            this.ctxtMenuLayers = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.AddLayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DelLayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpTBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PropertyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.opFileDlgLayers = new System.Windows.Forms.OpenFileDialog();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripDataView = new System.Windows.Forms.ToolStripButton();
            this.toolStripLayoutView = new System.Windows.Forms.ToolStripButton();
            this.PanelShow = new System.Windows.Forms.Panel();
            this.picBoxMap = new System.Windows.Forms.PictureBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StatusStripLblCoordinate = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusStripLblScale = new System.Windows.Forms.ToolStripStatusLabel();
            this.ctxtMenuLayers.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.PanelShow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxMap)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeViewLayers
            // 
            this.treeViewLayers.AllowDrop = true;
            this.treeViewLayers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.treeViewLayers.CheckBoxes = true;
            this.treeViewLayers.ImageIndex = 0;
            this.treeViewLayers.ImageList = this.imgListLayers;
            this.treeViewLayers.Location = new System.Drawing.Point(0, 0);
            this.treeViewLayers.Margin = new System.Windows.Forms.Padding(2);
            this.treeViewLayers.Name = "treeViewLayers";
            this.treeViewLayers.SelectedImageIndex = 0;
            this.treeViewLayers.ShowLines = false;
            this.treeViewLayers.ShowPlusMinus = false;
            this.treeViewLayers.Size = new System.Drawing.Size(127, 402);
            this.treeViewLayers.TabIndex = 0;
            this.treeViewLayers.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeViewLayers_AfterCheck);
            this.treeViewLayers.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.treeViewLayers_AfterCollapse);
            this.treeViewLayers.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.treeViewLayers_ItemDrag);
            this.treeViewLayers.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewLayers_NodeMouseClick);
            this.treeViewLayers.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewLayers_NodeMouseDoubleClick);
            this.treeViewLayers.DragDrop += new System.Windows.Forms.DragEventHandler(this.treeViewLayers_DragDrop);
            this.treeViewLayers.DragEnter += new System.Windows.Forms.DragEventHandler(this.treeViewLayers_DragEnter);
            // 
            // imgListLayers
            // 
            this.imgListLayers.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgListLayers.ImageStream")));
            this.imgListLayers.TransparentColor = System.Drawing.Color.Transparent;
            this.imgListLayers.Images.SetKeyName(0, "map.ico");
            this.imgListLayers.Images.SetKeyName(1, "DataFrame16.png");
            this.imgListLayers.Images.SetKeyName(2, "Layer_LYR_File24.png");
            // 
            // ctxtMenuLayers
            // 
            this.ctxtMenuLayers.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ctxtMenuLayers.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddLayerToolStripMenuItem,
            this.DelLayerToolStripMenuItem,
            this.OpTBToolStripMenuItem,
            this.PropertyToolStripMenuItem});
            this.ctxtMenuLayers.Name = "ctxtMenuLayers";
            this.ctxtMenuLayers.Size = new System.Drawing.Size(137, 92);
            // 
            // AddLayerToolStripMenuItem
            // 
            this.AddLayerToolStripMenuItem.Name = "AddLayerToolStripMenuItem";
            this.AddLayerToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.AddLayerToolStripMenuItem.Text = "增加图层";
            this.AddLayerToolStripMenuItem.Click += new System.EventHandler(this.AddLayerToolStripMenuItem_Click);
            // 
            // DelLayerToolStripMenuItem
            // 
            this.DelLayerToolStripMenuItem.Name = "DelLayerToolStripMenuItem";
            this.DelLayerToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.DelLayerToolStripMenuItem.Text = "删除图层";
            this.DelLayerToolStripMenuItem.Click += new System.EventHandler(this.DelLayerToolStripMenuItem_Click);
            // 
            // OpTBToolStripMenuItem
            // 
            this.OpTBToolStripMenuItem.Name = "OpTBToolStripMenuItem";
            this.OpTBToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.OpTBToolStripMenuItem.Text = "打开属性表";
            this.OpTBToolStripMenuItem.Click += new System.EventHandler(this.OpTBToolStripMenuItem_Click);
            // 
            // PropertyToolStripMenuItem
            // 
            this.PropertyToolStripMenuItem.Name = "PropertyToolStripMenuItem";
            this.PropertyToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.PropertyToolStripMenuItem.Text = "属性";
            this.PropertyToolStripMenuItem.Click += new System.EventHandler(this.PropertyToolStripMenuItem_Click);
            // 
            // opFileDlgLayers
            // 
            this.opFileDlgLayers.FileName = "openFileDialog1";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDataView,
            this.toolStripLayoutView});
            this.toolStrip1.Location = new System.Drawing.Point(125, 375);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(89, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripDataView
            // 
            this.toolStripDataView.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDataView.Image = global::CosmosGIS.Properties.Resources.Map16;
            this.toolStripDataView.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDataView.Name = "toolStripDataView";
            this.toolStripDataView.Size = new System.Drawing.Size(23, 22);
            this.toolStripDataView.Text = "Data View";
            this.toolStripDataView.Click += new System.EventHandler(this.toolStripDataView_Click);
            // 
            // toolStripLayoutView
            // 
            this.toolStripLayoutView.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripLayoutView.Image = global::CosmosGIS.Properties.Resources.Layout16;
            this.toolStripLayoutView.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripLayoutView.Name = "toolStripLayoutView";
            this.toolStripLayoutView.Size = new System.Drawing.Size(23, 22);
            this.toolStripLayoutView.Text = "Layout View";
            this.toolStripLayoutView.Click += new System.EventHandler(this.toolStripLayoutView_Click);
            // 
            // PanelShow
            // 
            this.PanelShow.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelShow.BackColor = System.Drawing.Color.Transparent;
            this.PanelShow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelShow.Controls.Add(this.picBoxMap);
            this.PanelShow.Location = new System.Drawing.Point(125, 0);
            this.PanelShow.Name = "PanelShow";
            this.PanelShow.Size = new System.Drawing.Size(474, 370);
            this.PanelShow.TabIndex = 3;
            // 
            // picBoxMap
            // 
            this.picBoxMap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picBoxMap.BackColor = System.Drawing.Color.White;
            this.picBoxMap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBoxMap.Cursor = System.Windows.Forms.Cursors.Default;
            this.picBoxMap.Location = new System.Drawing.Point(0, 0);
            this.picBoxMap.Margin = new System.Windows.Forms.Padding(2);
            this.picBoxMap.Name = "picBoxMap";
            this.picBoxMap.Size = new System.Drawing.Size(473, 369);
            this.picBoxMap.TabIndex = 1;
            this.picBoxMap.TabStop = false;
            this.picBoxMap.Click += new System.EventHandler(this.picBoxMap_Click);
            this.picBoxMap.DoubleClick += new System.EventHandler(this.picBoxMap_DoubleClick);
            this.picBoxMap.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picBoxMap_MouseDown);
            this.picBoxMap.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picBoxMap_MouseMove);
            this.picBoxMap.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picBoxMap_MouseUp);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusStripLblCoordinate,
            this.StatusStripLblScale});
            this.statusStrip1.Location = new System.Drawing.Point(417, 376);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(182, 26);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // StatusStripLblCoordinate
            // 
            this.StatusStripLblCoordinate.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.StatusStripLblCoordinate.Name = "StatusStripLblCoordinate";
            this.StatusStripLblCoordinate.Size = new System.Drawing.Size(98, 21);
            this.StatusStripLblCoordinate.Text = "116.111,39.444";
            // 
            // StatusStripLblScale
            // 
            this.StatusStripLblScale.Name = "StatusStripLblScale";
            this.StatusStripLblScale.Size = new System.Drawing.Size(67, 21);
            this.StatusStripLblScale.Text = "0.0000001";
            // 
            // CosmosMapPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.PanelShow);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.treeViewLayers);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "CosmosMapPanel";
            this.Size = new System.Drawing.Size(600, 400);
            this.Load += new System.EventHandler(this.CosmosMapPanel_Load);
            this.Resize += new System.EventHandler(this.CosmosMapPanel_Resize);
            this.ctxtMenuLayers.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.PanelShow.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBoxMap)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeViewLayers;
        private System.Windows.Forms.ImageList imgListLayers;
        private System.Windows.Forms.ToolStripMenuItem AddLayerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DelLayerToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog opFileDlgLayers;
        private System.Windows.Forms.ContextMenuStrip ctxtMenuLayers;
        private System.Windows.Forms.ToolStripMenuItem PropertyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OpTBToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripDataView;
        private System.Windows.Forms.ToolStripButton toolStripLayoutView;
        private System.Windows.Forms.Panel PanelShow;
        private System.Windows.Forms.PictureBox picBoxMap;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel StatusStripLblCoordinate;
        private System.Windows.Forms.ToolStripStatusLabel StatusStripLblScale;
    }
}
