namespace CosmosGIS.UI
{
    partial class Layout
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.纸张方向ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.横向ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.纵向ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.放大ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.缩小ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.纸张方向ToolStripMenuItem,
            this.放大ToolStripMenuItem,
            this.缩小ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(166, 94);
            // 
            // 纸张方向ToolStripMenuItem
            // 
            this.纸张方向ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.横向ToolStripMenuItem,
            this.纵向ToolStripMenuItem});
            this.纸张方向ToolStripMenuItem.Name = "纸张方向ToolStripMenuItem";
            this.纸张方向ToolStripMenuItem.Size = new System.Drawing.Size(165, 30);
            this.纸张方向ToolStripMenuItem.Text = "纸张方向";
            // 
            // 横向ToolStripMenuItem
            // 
            this.横向ToolStripMenuItem.Name = "横向ToolStripMenuItem";
            this.横向ToolStripMenuItem.Size = new System.Drawing.Size(129, 30);
            this.横向ToolStripMenuItem.Text = "横向";
            this.横向ToolStripMenuItem.Click += new System.EventHandler(this.横向ToolStripMenuItem_Click);
            // 
            // 纵向ToolStripMenuItem
            // 
            this.纵向ToolStripMenuItem.Name = "纵向ToolStripMenuItem";
            this.纵向ToolStripMenuItem.Size = new System.Drawing.Size(129, 30);
            this.纵向ToolStripMenuItem.Text = "纵向";
            this.纵向ToolStripMenuItem.Click += new System.EventHandler(this.纵向ToolStripMenuItem_Click);
            // 
            // 放大ToolStripMenuItem
            // 
            this.放大ToolStripMenuItem.Name = "放大ToolStripMenuItem";
            this.放大ToolStripMenuItem.Size = new System.Drawing.Size(165, 30);
            this.放大ToolStripMenuItem.Text = "放大";
            this.放大ToolStripMenuItem.Click += new System.EventHandler(this.放大ToolStripMenuItem_Click);
            // 
            // 缩小ToolStripMenuItem
            // 
            this.缩小ToolStripMenuItem.Name = "缩小ToolStripMenuItem";
            this.缩小ToolStripMenuItem.Size = new System.Drawing.Size(165, 30);
            this.缩小ToolStripMenuItem.Text = "缩小";
            this.缩小ToolStripMenuItem.Click += new System.EventHandler(this.缩小ToolStripMenuItem_Click);
            // 
            // Layout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightBlue;
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Layout";
            this.Size = new System.Drawing.Size(1153, 554);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Layout_KeyUp);
            this.Resize += new System.EventHandler(this.Layout_Resize);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 纸张方向ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 横向ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 纵向ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 放大ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 缩小ToolStripMenuItem;
    }
}
