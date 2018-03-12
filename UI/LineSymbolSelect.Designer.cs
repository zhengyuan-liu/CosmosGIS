namespace CosmosGIS.UI
{
    partial class LineSymbolSelect
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Yes = new System.Windows.Forms.Button();
            this.CB_size = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_color = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.PB_show = new System.Windows.Forms.PictureBox();
            this.SymbolListView = new System.Windows.Forms.ListView();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PB_show)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.CB_size);
            this.groupBox1.Controls.Add(this.PB_show);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btn_color);
            this.groupBox1.Location = new System.Drawing.Point(19, 26);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(438, 147);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "预览";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.SymbolListView);
            this.groupBox2.Location = new System.Drawing.Point(19, 200);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(437, 159);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "线形设置";
            // 
            // Yes
            // 
            this.Yes.Location = new System.Drawing.Point(370, 390);
            this.Yes.Name = "Yes";
            this.Yes.Size = new System.Drawing.Size(86, 34);
            this.Yes.TabIndex = 4;
            this.Yes.Text = "确定";
            this.Yes.UseVisualStyleBackColor = true;
            this.Yes.Click += new System.EventHandler(this.Yes_Click);
            // 
            // CB_size
            // 
            this.CB_size.FormattingEnabled = true;
            this.CB_size.Location = new System.Drawing.Point(298, 82);
            this.CB_size.Name = "CB_size";
            this.CB_size.Size = new System.Drawing.Size(59, 26);
            this.CB_size.TabIndex = 9;
            this.CB_size.SelectedIndexChanged += new System.EventHandler(this.CB_size_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(234, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 18);
            this.label2.TabIndex = 8;
            this.label2.Text = "大小";
            // 
            // btn_color
            // 
            this.btn_color.Location = new System.Drawing.Point(298, 39);
            this.btn_color.Name = "btn_color";
            this.btn_color.Size = new System.Drawing.Size(51, 28);
            this.btn_color.TabIndex = 7;
            this.btn_color.UseVisualStyleBackColor = true;
            this.btn_color.Click += new System.EventHandler(this.btn_color_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(234, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 18);
            this.label1.TabIndex = 6;
            this.label1.Text = "颜色";
            // 
            // PB_show
            // 
            this.PB_show.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PB_show.Location = new System.Drawing.Point(43, 27);
            this.PB_show.Name = "PB_show";
            this.PB_show.Size = new System.Drawing.Size(130, 93);
            this.PB_show.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PB_show.TabIndex = 5;
            this.PB_show.TabStop = false;
            // 
            // SymbolListView
            // 
            this.SymbolListView.Location = new System.Drawing.Point(17, 27);
            this.SymbolListView.Name = "SymbolListView";
            this.SymbolListView.Size = new System.Drawing.Size(400, 107);
            this.SymbolListView.TabIndex = 1;
            this.SymbolListView.UseCompatibleStateImageBehavior = false;
            this.SymbolListView.SelectedIndexChanged += new System.EventHandler(this.SymbolListView_SelectedIndexChanged);
            // 
            // LineSymbolSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Yes);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "LineSymbolSelect";
            this.Size = new System.Drawing.Size(480, 480);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PB_show)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button Yes;
        private System.Windows.Forms.ComboBox CB_size;
        private System.Windows.Forms.PictureBox PB_show;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_color;
        private System.Windows.Forms.ListView SymbolListView;
        private System.Windows.Forms.ColorDialog colorDialog1;
    }
}
