using System;

namespace CosmosGIS.UI
{
    partial class GettingStartedForm
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
            System.Windows.Forms.Label lblDbTitle;
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("New Maps");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Existing Maps");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("浏览更多");
            this.treeView = new System.Windows.Forms.TreeView();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tbLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.comboBoxDatabasePath = new System.Windows.Forms.ComboBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblMapPath = new System.Windows.Forms.Label();
            System.Windows.Forms.CheckBox checkBoxShow = new System.Windows.Forms.CheckBox();
            lblDbTitle = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblDbTitle
            // 
            lblDbTitle.AutoSize = true;
            lblDbTitle.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            lblDbTitle.Location = new System.Drawing.Point(3, 4);
            lblDbTitle.Name = "lblDbTitle";
            lblDbTitle.Size = new System.Drawing.Size(151, 15);
            lblDbTitle.TabIndex = 8;
            lblDbTitle.Text = "此地图的默认地理数据库：";
            // 
            // treeView
            // 
            this.treeView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeView.Location = new System.Drawing.Point(3, 3);
            this.treeView.Margin = new System.Windows.Forms.Padding(3);
            this.treeView.Name = "treeView";
            treeNode1.Name = "NodeNewMaps";
            treeNode1.Text = "New Maps";
            treeNode2.BackColor = System.Drawing.Color.White;
            treeNode2.Name = "NodeExistingMaps";
            treeNode2.Text = "Existing Maps";
            treeNode3.Name = "NodeMoreMaps";
            treeNode3.Text = "浏览更多";
            this.treeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3});
            this.treeView.Size = new System.Drawing.Size(115, 251);
            this.treeView.TabIndex = 0;
            this.treeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_NodeMouseClick);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(543, 20);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(3);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(60, 20);
            this.btnSearch.TabIndex = 3;
            this.btnSearch.Text = "浏览...";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(478, 6);
            this.btnOK.Margin = new System.Windows.Forms.Padding(3);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(60, 20);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(543, 6);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(60, 20);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // CheckBoxShow
            // 
            checkBoxShow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            checkBoxShow.Location = new System.Drawing.Point(6, 6);
            checkBoxShow.Margin = new System.Windows.Forms.Padding(3);
            checkBoxShow.Text = "不再显示";
            checkBoxShow.Checked = !Properties.Settings.Default.ShowGettingStart;
            checkBoxShow.CheckedChanged += new System.EventHandler(checkBoxShow_CheckedChanged);
            // 
            // tbLayoutPanel
            // 
            this.tbLayoutPanel.BackColor = System.Drawing.Color.White;
            this.tbLayoutPanel.ColumnCount = 3;
            this.tbLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tbLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tbLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tbLayoutPanel.Location = new System.Drawing.Point(122, 3);
            this.tbLayoutPanel.Margin = new System.Windows.Forms.Padding(3);
            this.tbLayoutPanel.Name = "tbLayoutPanel";
            this.tbLayoutPanel.RowCount = 2;
            this.tbLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tbLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tbLayoutPanel.Size = new System.Drawing.Size(484, 251);
            this.tbLayoutPanel.TabIndex = 7;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(checkBoxShow);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 321);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(612, 34);
            this.panel1.TabIndex = 9;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.comboBoxDatabasePath);
            this.panel2.Controls.Add(lblDbTitle);
            this.panel2.Controls.Add(this.btnSearch);
            this.panel2.Location = new System.Drawing.Point(0, 276);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(612, 46);
            this.panel2.TabIndex = 10;
            // 
            // comboBoxDatabasePath
            // 
            this.comboBoxDatabasePath.FormattingEnabled = true;
            this.comboBoxDatabasePath.Location = new System.Drawing.Point(3, 21);
            this.comboBoxDatabasePath.Name = "comboBoxDatabasePath";
            this.comboBoxDatabasePath.Size = new System.Drawing.Size(534, 16);
            this.comboBoxDatabasePath.TabIndex = 9;
            this.comboBoxDatabasePath.SelectedIndexChanged += new System.EventHandler(this.comboBoxDatabasePath_SelectedIndexChanged);
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.lblMapPath);
            this.panel3.Controls.Add(this.treeView);
            this.panel3.Controls.Add(this.tbLayoutPanel);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(612, 277);
            this.panel3.TabIndex = 11;
            // 
            // lblMapPath
            // 
            this.lblMapPath.AutoSize = true;
            this.lblMapPath.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMapPath.Location = new System.Drawing.Point(3, 256);
            this.lblMapPath.Name = "lblMapPath";
            this.lblMapPath.Size = new System.Drawing.Size(38, 15);
            this.lblMapPath.TabIndex = 8;
            this.lblMapPath.Text = "dfakj ";
            // 
            // GettingStartedForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(612, 355);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GettingStartedForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "CosmosGIS - Getting Started";
            this.Load += new System.EventHandler(this.GettingStartedForm_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GettingStartedForm_KeyPress);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TableLayoutPanel tbLayoutPanel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblMapPath;
        private System.Windows.Forms.ComboBox comboBoxDatabasePath;
    }
}