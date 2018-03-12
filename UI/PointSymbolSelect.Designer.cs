namespace CosmosGIS.UI
{
    partial class PointSymbolSelect
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.SymbolListView = new System.Windows.Forms.ListView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.CB_size = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_color = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.PB_show = new System.Windows.Forms.PictureBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.Yes = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PB_show)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.SymbolListView);
            this.groupBox1.Location = new System.Drawing.Point(20, 19);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(278, 423);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "符号样式";
            // 
            // SymbolListView
            // 
            this.SymbolListView.Location = new System.Drawing.Point(17, 27);
            this.SymbolListView.Name = "SymbolListView";
            this.SymbolListView.Size = new System.Drawing.Size(240, 380);
            this.SymbolListView.TabIndex = 0;
            this.SymbolListView.UseCompatibleStateImageBehavior = false;
            this.SymbolListView.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.CB_size);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.btn_color);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.PB_show);
            this.groupBox2.Location = new System.Drawing.Point(312, 19);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(162, 266);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "预览";
            // 
            // CB_size
            // 
            this.CB_size.FormattingEnabled = true;
            this.CB_size.Location = new System.Drawing.Point(80, 202);
            this.CB_size.Name = "CB_size";
            this.CB_size.Size = new System.Drawing.Size(59, 26);
            this.CB_size.TabIndex = 4;
            this.CB_size.SelectedIndexChanged += new System.EventHandler(this.CB_size_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 205);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 18);
            this.label2.TabIndex = 3;
            this.label2.Text = "大小";
            // 
            // btn_color
            // 
            this.btn_color.Location = new System.Drawing.Point(80, 159);
            this.btn_color.Name = "btn_color";
            this.btn_color.Size = new System.Drawing.Size(51, 28);
            this.btn_color.TabIndex = 2;
            this.btn_color.UseVisualStyleBackColor = true;
            this.btn_color.Click += new System.EventHandler(this.btn_color_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 164);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "颜色";
            // 
            // PB_show
            // 
            this.PB_show.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PB_show.Location = new System.Drawing.Point(19, 39);
            this.PB_show.Name = "PB_show";
            this.PB_show.Size = new System.Drawing.Size(130, 93);
            this.PB_show.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PB_show.TabIndex = 0;
            this.PB_show.TabStop = false;
            // 
            // Yes
            // 
            this.Yes.Location = new System.Drawing.Point(375, 392);
            this.Yes.Name = "Yes";
            this.Yes.Size = new System.Drawing.Size(86, 34);
            this.Yes.TabIndex = 3;
            this.Yes.Text = "确定";
            this.Yes.UseVisualStyleBackColor = true;
            this.Yes.Click += new System.EventHandler(this.Yes_Click);
            // 
            // PointSymbolSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Yes);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "PointSymbolSelect";
            this.Size = new System.Drawing.Size(498, 462);
            this.Load += new System.EventHandler(this.PointSymbolSelect_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PB_show)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ListView SymbolListView;
        private System.Windows.Forms.PictureBox PB_show;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox CB_size;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_color;
        private System.Windows.Forms.Button Yes;
        private System.Windows.Forms.ColorDialog colorDialog1;
    }
}
