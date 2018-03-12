using System;

namespace CosmosGIS.UI
{
    partial class PropertyTableForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PropertyTableForm));
            this.toolStripLayers = new System.Windows.Forms.ToolStrip();
            this.lblFeatureName = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButtonAddField = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSelectByAttribute = new System.Windows.Forms.ToolStripButton();
            this.toolStripSwitchSelection = new System.Windows.Forms.ToolStripButton();
            this.toolStripClearSelection = new System.Windows.Forms.ToolStripButton();
            this.toolStripStatus = new System.Windows.Forms.ToolStrip();
            this.toolStripStartRecord = new System.Windows.Forms.ToolStripButton();
            this.toolStripPreviewRecord = new System.Windows.Forms.ToolStripButton();
            this.toolStripTxtBoxPage = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripNextRecord = new System.Windows.Forms.ToolStripButton();
            this.toolStripEndRecord = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonShowAllRecords = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonShowSelectedRecords = new System.Windows.Forms.ToolStripButton();
            this.PanelData = new System.Windows.Forms.Panel();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLblStatus = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLayers.SuspendLayout();
            this.toolStripStatus.SuspendLayout();
            this.PanelData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripLayers
            // 
            this.toolStripLayers.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblFeatureName,
            this.toolStripButtonAddField,
            this.toolStripSeparator3,
            this.toolStripSelectByAttribute,
            this.toolStripSwitchSelection,
            this.toolStripClearSelection});
            this.toolStripLayers.Location = new System.Drawing.Point(0, 0);
            this.toolStripLayers.Name = "toolStripLayers";
            this.toolStripLayers.Padding = new System.Windows.Forms.Padding(0);
            this.toolStripLayers.Size = new System.Drawing.Size(285, 25);
            this.toolStripLayers.TabIndex = 2;
            this.toolStripLayers.Text = "toolStrip1";
            // 
            // lblFeatureName
            // 
            this.lblFeatureName.Name = "lblFeatureName";
            this.lblFeatureName.Size = new System.Drawing.Size(0, 22);
            // 
            // toolStripButtonAddField
            // 
            this.toolStripButtonAddField.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAddField.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAddField.Image")));
            this.toolStripButtonAddField.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAddField.Name = "toolStripButtonAddField";
            this.toolStripButtonAddField.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonAddField.Text = "Add Field";
            this.toolStripButtonAddField.Click += new System.EventHandler(this.toolStripButtonAddField_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSelectByAttribute
            // 
            this.toolStripSelectByAttribute.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSelectByAttribute.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSelectByAttribute.Image")));
            this.toolStripSelectByAttribute.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSelectByAttribute.Name = "toolStripSelectByAttribute";
            this.toolStripSelectByAttribute.Size = new System.Drawing.Size(23, 22);
            this.toolStripSelectByAttribute.Text = "Select By Attributes";
            this.toolStripSelectByAttribute.Click += new System.EventHandler(this.toolStripSelectByAttribute_Click);
            // 
            // toolStripSwitchSelection
            // 
            this.toolStripSwitchSelection.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSwitchSelection.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSwitchSelection.Image")));
            this.toolStripSwitchSelection.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSwitchSelection.Name = "toolStripSwitchSelection";
            this.toolStripSwitchSelection.Size = new System.Drawing.Size(23, 22);
            this.toolStripSwitchSelection.Text = "Switch Selection";
            this.toolStripSwitchSelection.Click += new System.EventHandler(this.toolStripSwitchSelection_Click);
            // 
            // toolStripClearSelection
            // 
            this.toolStripClearSelection.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripClearSelection.Image = ((System.Drawing.Image)(resources.GetObject("toolStripClearSelection.Image")));
            this.toolStripClearSelection.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripClearSelection.Name = "toolStripClearSelection";
            this.toolStripClearSelection.Size = new System.Drawing.Size(23, 22);
            this.toolStripClearSelection.Text = "Clear Selection";
            this.toolStripClearSelection.Click += new System.EventHandler(this.toolStripCearSelection_Click);
            // 
            // toolStripStatus
            // 
            this.toolStripStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.toolStripStatus.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStartRecord,
            this.toolStripPreviewRecord,
            this.toolStripTxtBoxPage,
            this.toolStripNextRecord,
            this.toolStripEndRecord,
            this.toolStripSeparator1,
            this.toolStripButtonShowAllRecords,
            this.toolStripButtonShowSelectedRecords});
            this.toolStripStatus.Location = new System.Drawing.Point(0, 314);
            this.toolStripStatus.Name = "toolStripStatus";
            this.toolStripStatus.Size = new System.Drawing.Size(198, 25);
            this.toolStripStatus.TabIndex = 0;
            this.toolStripStatus.Text = "toolStrip1";
            // 
            // toolStripStartRecord
            // 
            this.toolStripStartRecord.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripStartRecord.Image = ((System.Drawing.Image)(resources.GetObject("toolStripStartRecord.Image")));
            this.toolStripStartRecord.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripStartRecord.Name = "toolStripStartRecord";
            this.toolStripStartRecord.Size = new System.Drawing.Size(23, 22);
            this.toolStripStartRecord.Text = "Move to beginning of table";
            this.toolStripStartRecord.Click += new System.EventHandler(this.toolStripStartRecord_Click);
            // 
            // toolStripPreviewRecord
            // 
            this.toolStripPreviewRecord.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripPreviewRecord.Image = ((System.Drawing.Image)(resources.GetObject("toolStripPreviewRecord.Image")));
            this.toolStripPreviewRecord.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripPreviewRecord.Name = "toolStripPreviewRecord";
            this.toolStripPreviewRecord.Size = new System.Drawing.Size(23, 22);
            this.toolStripPreviewRecord.Text = "Move to the previous record";
            this.toolStripPreviewRecord.Click += new System.EventHandler(this.toolStripPreviewRecord_Click);
            // 
            // toolStripTxtBoxPage
            // 
            this.toolStripTxtBoxPage.Name = "toolStripTxtBoxPage";
            this.toolStripTxtBoxPage.Size = new System.Drawing.Size(40, 25);
            // 
            // toolStripNextRecord
            // 
            this.toolStripNextRecord.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripNextRecord.Image = ((System.Drawing.Image)(resources.GetObject("toolStripNextRecord.Image")));
            this.toolStripNextRecord.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripNextRecord.Name = "toolStripNextRecord";
            this.toolStripNextRecord.Size = new System.Drawing.Size(23, 22);
            this.toolStripNextRecord.Text = "Move to the next record";
            this.toolStripNextRecord.Click += new System.EventHandler(this.toolStripNextRecord_Click);
            // 
            // toolStripEndRecord
            // 
            this.toolStripEndRecord.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripEndRecord.Image = ((System.Drawing.Image)(resources.GetObject("toolStripEndRecord.Image")));
            this.toolStripEndRecord.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripEndRecord.Name = "toolStripEndRecord";
            this.toolStripEndRecord.Size = new System.Drawing.Size(23, 22);
            this.toolStripEndRecord.Text = "Move to end of table";
            this.toolStripEndRecord.Click += new System.EventHandler(this.toolStripEndRecord_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonShowAllRecords
            // 
            this.toolStripButtonShowAllRecords.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonShowAllRecords.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonShowAllRecords.Image")));
            this.toolStripButtonShowAllRecords.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonShowAllRecords.Name = "toolStripButtonShowAllRecords";
            this.toolStripButtonShowAllRecords.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonShowAllRecords.Text = "Show all records";
            this.toolStripButtonShowAllRecords.Click += new System.EventHandler(this.toolStripButtonShowAllRecords_Click);
            // 
            // toolStripButtonShowSelectedRecords
            // 
            this.toolStripButtonShowSelectedRecords.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonShowSelectedRecords.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonShowSelectedRecords.Image")));
            this.toolStripButtonShowSelectedRecords.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonShowSelectedRecords.Name = "toolStripButtonShowSelectedRecords";
            this.toolStripButtonShowSelectedRecords.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonShowSelectedRecords.Text = "Show selected records";
            this.toolStripButtonShowSelectedRecords.Click += new System.EventHandler(this.toolStripButtonShowSelectedRecords_Click);
            // 
            // PanelData
            // 
            this.PanelData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelData.Controls.Add(this.dataGridView);
            this.PanelData.Location = new System.Drawing.Point(0, 28);
            this.PanelData.Name = "PanelData";
            this.PanelData.Size = new System.Drawing.Size(285, 283);
            this.PanelData.TabIndex = 3;
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AllowUserToResizeRows = false;
            this.dataGridView.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.RowHeaderSelect;
            this.dataGridView.Location = new System.Drawing.Point(0, 0);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowTemplate.Height = 23;
            this.dataGridView.Size = new System.Drawing.Size(285, 283);
            this.dataGridView.TabIndex = 0;
            this.dataGridView.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_CellMouseDown);
            this.dataGridView.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_CellMouseUp);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLblStatus});
            this.toolStrip1.Location = new System.Drawing.Point(1, 339);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(194, 25);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLblStatus
            // 
            this.toolStripLblStatus.Name = "toolStripLblStatus";
            this.toolStripLblStatus.Size = new System.Drawing.Size(182, 22);
            this.toolStripLblStatus.Text = "(10000 out of 10000 Selected)";
            // 
            // PropertyTableForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(285, 361);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.toolStripStatus);
            this.Controls.Add(this.PanelData);
            this.Controls.Add(this.toolStripLayers);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PropertyTableForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "属性表";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PropertyPanel_FormClosing);
            this.Shown += new System.EventHandler(this.PropertyPanel_Shown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PropertyTableForm_KeyPress);
            this.toolStripLayers.ResumeLayout(false);
            this.toolStripLayers.PerformLayout();
            this.toolStripStatus.ResumeLayout(false);
            this.toolStripStatus.PerformLayout();
            this.PanelData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStripLayers;
        private System.Windows.Forms.ToolStripLabel lblFeatureName;
        private System.Windows.Forms.ToolStripButton toolStripButtonAddField;
        private System.Windows.Forms.ToolStrip toolStripStatus;
        private System.Windows.Forms.ToolStripButton toolStripStartRecord;
        private System.Windows.Forms.ToolStripButton toolStripPreviewRecord;
        private System.Windows.Forms.ToolStripTextBox toolStripTxtBoxPage;
        private System.Windows.Forms.ToolStripButton toolStripNextRecord;
        private System.Windows.Forms.ToolStripButton toolStripEndRecord;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButtonShowAllRecords;
        private System.Windows.Forms.ToolStripButton toolStripButtonShowSelectedRecords;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolStripSelectByAttribute;
        private System.Windows.Forms.ToolStripButton toolStripSwitchSelection;
        private System.Windows.Forms.ToolStripButton toolStripClearSelection;
        private System.Windows.Forms.Panel PanelData;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLblStatus;
        private System.Windows.Forms.DataGridView dataGridView;
    }
}