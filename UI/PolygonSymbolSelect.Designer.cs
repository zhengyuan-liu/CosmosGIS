namespace CosmosGIS.UI
{
    partial class PolygonSymbolSelect
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
            this.PB_show = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_color = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.HasBoundary = new System.Windows.Forms.CheckBox();
            this.CB_size = new System.Windows.Forms.ComboBox();
            this.SymbolListView = new System.Windows.Forms.ListView();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_boundcolor = new System.Windows.Forms.Button();
            this.Yes = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PB_show)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.HasBoundary);
            this.groupBox1.Controls.Add(this.PB_show);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btn_color);
            this.groupBox1.Location = new System.Drawing.Point(21, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(438, 147);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "预览";
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(234, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 18);
            this.label1.TabIndex = 6;
            this.label1.Text = "填充颜色";
            // 
            // btn_color
            // 
            this.btn_color.Location = new System.Drawing.Point(320, 39);
            this.btn_color.Name = "btn_color";
            this.btn_color.Size = new System.Drawing.Size(51, 28);
            this.btn_color.TabIndex = 7;
            this.btn_color.UseVisualStyleBackColor = true;
            this.btn_color.Click += new System.EventHandler(this.btn_color_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.CB_size);
            this.groupBox2.Controls.Add(this.SymbolListView);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.btn_boundcolor);
            this.groupBox2.Location = new System.Drawing.Point(21, 180);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(437, 200);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "边界设置";
            // 
            // HasBoundary
            // 
            this.HasBoundary.AutoSize = true;
            this.HasBoundary.Location = new System.Drawing.Point(237, 98);
            this.HasBoundary.Name = "HasBoundary";
            this.HasBoundary.Size = new System.Drawing.Size(124, 22);
            this.HasBoundary.TabIndex = 14;
            this.HasBoundary.Text = "是否有边框";
            this.HasBoundary.UseVisualStyleBackColor = true;
            this.HasBoundary.CheckedChanged += new System.EventHandler(this.HasBoundary_CheckedChanged);
            // 
            // CB_size
            // 
            this.CB_size.FormattingEnabled = true;
            this.CB_size.Location = new System.Drawing.Point(347, 32);
            this.CB_size.Name = "CB_size";
            this.CB_size.Size = new System.Drawing.Size(59, 26);
            this.CB_size.TabIndex = 13;
            this.CB_size.SelectedIndexChanged += new System.EventHandler(this.CB_size_SelectedIndexChanged);
            // 
            // SymbolListView
            // 
            this.SymbolListView.Location = new System.Drawing.Point(23, 77);
            this.SymbolListView.Name = "SymbolListView";
            this.SymbolListView.Size = new System.Drawing.Size(400, 107);
            this.SymbolListView.TabIndex = 1;
            this.SymbolListView.UseCompatibleStateImageBehavior = false;
            this.SymbolListView.SelectedIndexChanged += new System.EventHandler(this.SymbolListView_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(178, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 18);
            this.label3.TabIndex = 10;
            this.label3.Text = "颜色";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(297, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 18);
            this.label2.TabIndex = 12;
            this.label2.Text = "大小";
            // 
            // btn_boundcolor
            // 
            this.btn_boundcolor.Location = new System.Drawing.Point(228, 32);
            this.btn_boundcolor.Name = "btn_boundcolor";
            this.btn_boundcolor.Size = new System.Drawing.Size(51, 28);
            this.btn_boundcolor.TabIndex = 11;
            this.btn_boundcolor.UseVisualStyleBackColor = true;
            this.btn_boundcolor.Click += new System.EventHandler(this.btn_boundcolor_Click);
            // 
            // Yes
            // 
            this.Yes.Location = new System.Drawing.Point(373, 396);
            this.Yes.Name = "Yes";
            this.Yes.Size = new System.Drawing.Size(86, 34);
            this.Yes.TabIndex = 5;
            this.Yes.Text = "确定";
            this.Yes.UseVisualStyleBackColor = true;
            this.Yes.Click += new System.EventHandler(this.Yes_Click);
            // 
            // PolygonSymbolSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Yes);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "PolygonSymbolSelect";
            this.Size = new System.Drawing.Size(480, 480);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PB_show)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox PB_show;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_color;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView SymbolListView;
        private System.Windows.Forms.ComboBox CB_size;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_boundcolor;
        private System.Windows.Forms.CheckBox HasBoundary;
        private System.Windows.Forms.Button Yes;
        private System.Windows.Forms.ColorDialog colorDialog1;
    }
}
