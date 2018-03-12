namespace CosmosGIS.UI
{
    partial class ExportMapForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.Label lblResolutionTitle;
            System.Windows.Forms.Label lblDpi;
            System.Windows.Forms.Label lblWidthTitle;
            System.Windows.Forms.Label lblHeightTitle;
            System.Windows.Forms.Label lblWidthUnit;
            System.Windows.Forms.Label lblHeightUnit;
            System.Windows.Forms.Label lblExportPath;
            System.Windows.Forms.Button btnBrowse;
            System.Windows.Forms.Button btnSave;
            System.Windows.Forms.Button btnCancel;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExportMapForm));
            this.numericUpDownDpi = new System.Windows.Forms.NumericUpDown();
            this.tbWidth = new System.Windows.Forms.TextBox();
            this.tbHeight = new System.Windows.Forms.TextBox();
            this.tbExportPath = new System.Windows.Forms.TextBox();
            lblResolutionTitle = new System.Windows.Forms.Label();
            lblDpi = new System.Windows.Forms.Label();
            lblWidthTitle = new System.Windows.Forms.Label();
            lblHeightTitle = new System.Windows.Forms.Label();
            lblWidthUnit = new System.Windows.Forms.Label();
            lblHeightUnit = new System.Windows.Forms.Label();
            lblExportPath = new System.Windows.Forms.Label();
            btnBrowse = new System.Windows.Forms.Button();
            btnSave = new System.Windows.Forms.Button();
            btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDpi)).BeginInit();
            this.SuspendLayout();
            // 
            // lblResolutionTitle
            // 
            lblResolutionTitle.AutoSize = true;
            lblResolutionTitle.Location = new System.Drawing.Point(12, 16);
            lblResolutionTitle.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            lblResolutionTitle.Name = "lblResolutionTitle";
            lblResolutionTitle.Size = new System.Drawing.Size(71, 12);
            lblResolutionTitle.TabIndex = 0;
            lblResolutionTitle.Text = "Resolution:";
            // 
            // numericUpDownDpi
            // 
            this.numericUpDownDpi.Location = new System.Drawing.Point(84, 12);
            this.numericUpDownDpi.Margin = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.numericUpDownDpi.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.numericUpDownDpi.Name = "numericUpDownDpi";
            this.numericUpDownDpi.Size = new System.Drawing.Size(73, 17);
            this.numericUpDownDpi.TabIndex = 1;
            this.numericUpDownDpi.Value = new decimal(new int[] {
            96,
            0,
            0,
            0});
            this.numericUpDownDpi.ValueChanged += new System.EventHandler(this.numericUpDownDpi_ValueChanged);
            // 
            // lblDpi
            // 
            lblDpi.AutoSize = true;
            lblDpi.Location = new System.Drawing.Point(163, 16);
            lblDpi.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            lblDpi.Name = "lblDpi";
            lblDpi.Size = new System.Drawing.Size(23, 12);
            lblDpi.TabIndex = 2;
            lblDpi.Text = "DPI";
            // 
            // lblWidthTitle
            // 
            lblWidthTitle.AutoSize = true;
            lblWidthTitle.Location = new System.Drawing.Point(12, 47);
            lblWidthTitle.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            lblWidthTitle.Name = "lblWidthTitle";
            lblWidthTitle.Size = new System.Drawing.Size(41, 12);
            lblWidthTitle.TabIndex = 3;
            lblWidthTitle.Text = "Width:";
            // 
            // lblHeightTitle
            // 
            lblHeightTitle.AutoSize = true;
            lblHeightTitle.Location = new System.Drawing.Point(12, 74);
            lblHeightTitle.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            lblHeightTitle.Name = "lblHeightTitle";
            lblHeightTitle.Size = new System.Drawing.Size(47, 12);
            lblHeightTitle.TabIndex = 4;
            lblHeightTitle.Text = "Height:";
            // 
            // tbWidth
            // 
            this.tbWidth.Location = new System.Drawing.Point(84, 42);
            this.tbWidth.Margin = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tbWidth.Name = "tbWidth";
            this.tbWidth.ReadOnly = true;
            this.tbWidth.Size = new System.Drawing.Size(57, 17);
            this.tbWidth.TabIndex = 5;
            // 
            // tbHeight
            // 
            this.tbHeight.Location = new System.Drawing.Point(84, 72);
            this.tbHeight.Margin = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tbHeight.Name = "tbHeight";
            this.tbHeight.ReadOnly = true;
            this.tbHeight.Size = new System.Drawing.Size(57, 17);
            this.tbHeight.TabIndex = 6;
            // 
            // lblWidthUnit
            // 
            lblWidthUnit.AutoSize = true;
            lblWidthUnit.Location = new System.Drawing.Point(163, 45);
            lblWidthUnit.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            lblWidthUnit.Name = "lblWidthUnit";
            lblWidthUnit.Size = new System.Drawing.Size(35, 12);
            lblWidthUnit.TabIndex = 7;
            lblWidthUnit.Text = "Pixel";
            // 
            // lblHeightUnit
            // 
            lblHeightUnit.AutoSize = true;
            lblHeightUnit.Location = new System.Drawing.Point(163, 75);
            lblHeightUnit.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            lblHeightUnit.Name = "lblHeightUnit";
            lblHeightUnit.Size = new System.Drawing.Size(35, 12);
            lblHeightUnit.TabIndex = 8;
            lblHeightUnit.Text = "Pixel";
            // 
            // lblExportPath
            // 
            lblExportPath.AutoSize = true;
            lblExportPath.Location = new System.Drawing.Point(12, 106);
            lblExportPath.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            lblExportPath.Name = "lblExportPath";
            lblExportPath.Size = new System.Drawing.Size(59, 12);
            lblExportPath.TabIndex = 9;
            lblExportPath.Text = "导出路径:";
            // 
            // tbExportPath
            // 
            this.tbExportPath.Location = new System.Drawing.Point(12, 129);
            this.tbExportPath.Margin = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tbExportPath.Name = "tbExportPath";
            this.tbExportPath.ReadOnly = true;
            this.tbExportPath.Size = new System.Drawing.Size(297, 17);
            this.tbExportPath.TabIndex = 10;
            // 
            // btnBrowse
            // 
            btnBrowse.Location = new System.Drawing.Point(319, 129);
            btnBrowse.Margin = new System.Windows.Forms.Padding(3, 3, 3, 3);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.Size = new System.Drawing.Size(55, 21);
            btnBrowse.TabIndex = 11;
            btnBrowse.Text = "浏览";
            btnBrowse.UseVisualStyleBackColor = true;
            btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // btnSave
            // 
            btnSave.Location = new System.Drawing.Point(256, 156);
            btnSave.Margin = new System.Windows.Forms.Padding(3, 3, 3, 3);
            btnSave.Name = "btnSave";
            btnSave.Size = new System.Drawing.Size(55, 21);
            btnSave.TabIndex = 12;
            btnSave.Text = "保存";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            btnCancel.Location = new System.Drawing.Point(319, 156);
            btnCancel.Margin = new System.Windows.Forms.Padding(3, 3, 3, 3);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new System.Drawing.Size(55, 21);
            btnCancel.TabIndex = 13;
            btnCancel.Text = "取消";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ExportMapForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(386, 186);
            this.Controls.Add(btnCancel);
            this.Controls.Add(btnSave);
            this.Controls.Add(btnBrowse);
            this.Controls.Add(this.tbExportPath);
            this.Controls.Add(lblExportPath);
            this.Controls.Add(lblHeightUnit);
            this.Controls.Add(lblWidthUnit);
            this.Controls.Add(this.tbHeight);
            this.Controls.Add(this.tbWidth);
            this.Controls.Add(lblHeightTitle);
            this.Controls.Add(lblWidthTitle);
            this.Controls.Add(lblDpi);
            this.Controls.Add(this.numericUpDownDpi);
            this.Controls.Add(lblResolutionTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.ShowIcon = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExportMapForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Export Map";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDpi)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox tbWidth;
        private System.Windows.Forms.TextBox tbHeight;
        private System.Windows.Forms.TextBox tbExportPath;
        private System.Windows.Forms.NumericUpDown numericUpDownDpi;
    }
}