
using System;

namespace CosmosGIS.UI
{
    partial class CreateNewCGVDialog
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
            System.Windows.Forms.Button btnCancel;
            System.Windows.Forms.Button btnOK;
            System.Windows.Forms.Button btnBrowse;
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.comboBoxType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tbPath = new System.Windows.Forms.TextBox();
            btnCancel = new System.Windows.Forms.Button();
            btnOK = new System.Windows.Forms.Button();
            btnBrowse = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            btnCancel.Location = new System.Drawing.Point(318, 136);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new System.Drawing.Size(62, 21);
            btnCancel.TabIndex = 17;
            btnCancel.Text = "取消";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            btnOK.Location = new System.Drawing.Point(247, 136);
            btnOK.Name = "btnOK";
            btnOK.Size = new System.Drawing.Size(62, 21);
            btnOK.TabIndex = 16;
            btnOK.Text = "确定";
            btnOK.UseVisualStyleBackColor = true;
            btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnBrowse
            // 
            btnBrowse.Location = new System.Drawing.Point(318, 109);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.Size = new System.Drawing.Size(62, 21);
            btnBrowse.TabIndex = 15;
            btnBrowse.Text = "浏览...";
            btnBrowse.UseVisualStyleBackColor = true;
            btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "Feature Type:";
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(208, 15);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(172, 21);
            this.tbName.TabIndex = 2;
            this.tbName.TextChanged += new System.EventHandler(tbName_TextChanged);
            // 
            // comboBoxType
            // 
            this.comboBoxType.FormattingEnabled = true;
            this.comboBoxType.Items.AddRange(new object[] {
            "Point",
            "MultiPolyline",
            "MultiPolygon"});
            this.comboBoxType.Location = new System.Drawing.Point(208, 44);
            this.comboBoxType.Name = "comboBoxType";
            this.comboBoxType.Size = new System.Drawing.Size(172, 20);
            this.comboBoxType.TabIndex = 3;
            this.comboBoxType.SelectedIndexChanged += new System.EventHandler(this.comboBoxType_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(179, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "Geographic Coordinate System:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(208, 73);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(172, 21);
            this.textBox1.TabIndex = 5;
            this.textBox1.Text = "WGS_1984";
            // 
            // tbPath
            // 
            this.tbPath.Location = new System.Drawing.Point(12, 109);
            this.tbPath.Name = "tbPath";
            this.tbPath.ReadOnly = true;
            this.tbPath.Size = new System.Drawing.Size(297, 21);
            this.tbPath.TabIndex = 14;
            // 
            // CreateNewCGVDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 167);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Controls.Add(btnCancel);
            this.Controls.Add(btnOK);
            this.Controls.Add(btnBrowse);
            this.Controls.Add(this.tbPath);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBoxType);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreateNewCGVDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Create New CGV";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.ComboBox comboBoxType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox tbPath;
    }
}