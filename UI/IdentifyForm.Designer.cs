using System;
using System.Windows.Forms;

namespace CosmosGIS.UI
{
    partial class IdentifyForm
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
            this.lblLayerTitle = new System.Windows.Forms.Label();
            this.comboBoxLayers = new System.Windows.Forms.ComboBox();
            this.treeViewFeature = new System.Windows.Forms.TreeView();
            this.lblLocationTile = new System.Windows.Forms.Label();
            this.txtLocation = new System.Windows.Forms.TextBox();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblLayerTitle
            // 
            this.lblLayerTitle.AutoSize = true;
            this.lblLayerTitle.Location = new System.Drawing.Point(8, 8);
            this.lblLayerTitle.Name = "lblLayerTitle";
            this.lblLayerTitle.Size = new System.Drawing.Size(83, 12);
            this.lblLayerTitle.TabIndex = 0;
            this.lblLayerTitle.Text = "Identify from";
            // 
            // comboBoxLayers
            // 
            this.comboBoxLayers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLayers.FormattingEnabled = true;
            this.comboBoxLayers.Location = new System.Drawing.Point(97, 5);
            this.comboBoxLayers.Name = "comboBoxLayers";
            this.comboBoxLayers.Size = new System.Drawing.Size(198, 20);
            this.comboBoxLayers.TabIndex = 1;
            this.comboBoxLayers.SelectedIndexChanged += new EventHandler(comboBoxLayers_SelectedIndexChanged);
            // 
            // treeViewFeature
            // 
            this.treeViewFeature.Location = new System.Drawing.Point(10, 32);
            this.treeViewFeature.Name = "treeViewFeature";
            this.treeViewFeature.Size = new System.Drawing.Size(284, 80);
            this.treeViewFeature.TabIndex = 2;
            // 
            // lblLocationTile
            // 
            this.lblLocationTile.AutoSize = true;
            this.lblLocationTile.Location = new System.Drawing.Point(12, 126);
            this.lblLocationTile.Name = "lblLocationTile";
            this.lblLocationTile.Size = new System.Drawing.Size(59, 12);
            this.lblLocationTile.TabIndex = 3;
            this.lblLocationTile.Text = "Location:";
            // 
            // txtLocation
            // 
            this.txtLocation.Location = new System.Drawing.Point(77, 122);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.ReadOnly = true;
            this.txtLocation.Size = new System.Drawing.Size(216, 21);
            this.txtLocation.TabIndex = 4;
            // 
            // dataGridView
            // 
            this.dataGridView.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(12, 149);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowTemplate.Height = 23;
            this.dataGridView.Size = new System.Drawing.Size(282, 139);
            this.dataGridView.TabIndex = 5;
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 296);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(307, 22);
            this.statusStrip.TabIndex = 6;
            this.statusStrip.Text = "statusStrip";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(119, 17);
            this.toolStripStatusLabel.Text = "Identified 0 feature";
            // 
            // IdentifyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(307, 318);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.txtLocation);
            this.Controls.Add(this.lblLocationTile);
            this.Controls.Add(this.treeViewFeature);
            this.Controls.Add(this.comboBoxLayers);
            this.Controls.Add(this.lblLayerTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "IdentifyForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Identify";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(IdentifyForm_FormClosed);
            this.Shown += new EventHandler(IdentifyForm_Shown);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private System.Windows.Forms.Label lblLayerTitle;
        private System.Windows.Forms.ComboBox comboBoxLayers;
        private System.Windows.Forms.TreeView treeViewFeature;
        private System.Windows.Forms.Label lblLocationTile;
        private System.Windows.Forms.TextBox txtLocation;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;

    }
}