using System;

namespace CosmosGIS.UI
{
    partial class BriefMapControl
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
            this.pictBoxBreviary = new System.Windows.Forms.PictureBox();
            this.lblName = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictBoxBreviary)).BeginInit();
            this.SuspendLayout();
            // 
            // pictBoxBreviary
            // 
            this.pictBoxBreviary.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictBoxBreviary.Image = global::CosmosGIS.Properties.Resources.BlankDocThumb;
            this.pictBoxBreviary.Location = new System.Drawing.Point(21, 19);
            this.pictBoxBreviary.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.pictBoxBreviary.Name = "pictBoxBreviary";
            this.pictBoxBreviary.Size = new System.Drawing.Size(252, 169);
            this.pictBoxBreviary.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictBoxBreviary.TabIndex = 0;
            this.pictBoxBreviary.TabStop = false;
            this.pictBoxBreviary.Click += new System.EventHandler(this.ChangeBackColor_Click);
            this.pictBoxBreviary.DoubleClick += new System.EventHandler(this.GetSelectMap_DoubleClick);
            // 
            // lblName
            // 
            this.lblName.BackColor = System.Drawing.Color.White;
            this.lblName.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblName.Location = new System.Drawing.Point(0, 194);
            this.lblName.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(298, 44);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "Blank Map";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblName.Click += new System.EventHandler(this.ChangeBackColor_Click);
            this.lblName.DoubleClick += new System.EventHandler(this.GetSelectMap_DoubleClick);
            // 
            // BriefMapControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Controls.Add(this.pictBoxBreviary);
            this.Controls.Add(this.lblName);
            this.Margin = new System.Windows.Forms.Padding(12, 12, 12, 12);
            this.Name = "BriefMapControl";
            this.Size = new System.Drawing.Size(298, 238);
            this.Click += new System.EventHandler(this.ChangeBackColor_Click);
            this.DoubleClick += new System.EventHandler(this.GetSelectMap_DoubleClick);
            ((System.ComponentModel.ISupportInitialize)(this.pictBoxBreviary)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictBoxBreviary;
        private System.Windows.Forms.Label lblName;
    }
}
