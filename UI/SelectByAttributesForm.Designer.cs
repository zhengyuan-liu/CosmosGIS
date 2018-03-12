namespace CosmosGIS.UI
{
    partial class SelectByAttributesForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxLayers = new System.Windows.Forms.ComboBox();
            this.listBoxFields = new System.Windows.Forms.ListBox();
            this.btnGetUniqueValues = new System.Windows.Forms.Button();
            this.listBoxUniqueValue = new System.Windows.Forms.ListBox();
            this.lblSQL = new System.Windows.Forms.Label();
            this.textBoxSQL = new System.Windows.Forms.TextBox();
            this.btnMore = new System.Windows.Forms.Button();
            this.btnLess = new System.Windows.Forms.Button();
            this.btnEqual = new System.Windows.Forms.Button();
            this.btnAnd = new System.Windows.Forms.Button();
            this.btnNot = new System.Windows.Forms.Button();
            this.btnOr = new System.Windows.Forms.Button();
            this.btnLessOrEqual = new System.Windows.Forms.Button();
            this.btnBracket = new System.Windows.Forms.Button();
            this.btnMoreOrEqual = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "layers";
            // 
            // comboBoxLayers
            // 
            this.comboBoxLayers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLayers.FormattingEnabled = true;
            this.comboBoxLayers.Location = new System.Drawing.Point(59, 8);
            this.comboBoxLayers.Name = "comboBoxLayers";
            this.comboBoxLayers.Size = new System.Drawing.Size(213, 20);
            this.comboBoxLayers.TabIndex = 1;
            this.comboBoxLayers.SelectedIndexChanged += new System.EventHandler(this.comboBoxLayers_SelectedIndexChanged);
            // 
            // listBoxFields
            // 
            this.listBoxFields.FormattingEnabled = true;
            this.listBoxFields.ItemHeight = 12;
            this.listBoxFields.Items.AddRange(new object[] {
            "ID",
            "NAME",
            "Latitude",
            "Longitude"});
            this.listBoxFields.Location = new System.Drawing.Point(12, 39);
            this.listBoxFields.Name = "listBoxFields";
            this.listBoxFields.Size = new System.Drawing.Size(259, 64);
            this.listBoxFields.TabIndex = 2;
            this.listBoxFields.SelectedIndexChanged += new System.EventHandler(this.listBoxFields_SelectedIndexChanged);
            this.listBoxFields.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBoxFields_MouseDoubleClick);
            // 
            // btnGetUniqueValues
            // 
            this.btnGetUniqueValues.Enabled = false;
            this.btnGetUniqueValues.Location = new System.Drawing.Point(189, 191);
            this.btnGetUniqueValues.Name = "btnGetUniqueValues";
            this.btnGetUniqueValues.Size = new System.Drawing.Size(82, 24);
            this.btnGetUniqueValues.TabIndex = 3;
            this.btnGetUniqueValues.Text = "获取唯一值";
            this.btnGetUniqueValues.UseVisualStyleBackColor = true;
            this.btnGetUniqueValues.Click += new System.EventHandler(this.btnGetUniqueValues_Click);
            // 
            // listBoxUniqueValue
            // 
            this.listBoxUniqueValue.FormattingEnabled = true;
            this.listBoxUniqueValue.ItemHeight = 12;
            this.listBoxUniqueValue.Location = new System.Drawing.Point(128, 109);
            this.listBoxUniqueValue.Name = "listBoxUniqueValue";
            this.listBoxUniqueValue.Size = new System.Drawing.Size(143, 76);
            this.listBoxUniqueValue.TabIndex = 4;
            this.listBoxUniqueValue.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBoxUniqueValue_MouseDoubleClick);
            // 
            // lblSQL
            // 
            this.lblSQL.AutoSize = true;
            this.lblSQL.Location = new System.Drawing.Point(13, 223);
            this.lblSQL.Name = "lblSQL";
            this.lblSQL.Size = new System.Drawing.Size(143, 12);
            this.lblSQL.TabIndex = 5;
            this.lblSQL.Text = "Select * From XXX Where";
            // 
            // textBoxSQL
            // 
            this.textBoxSQL.Location = new System.Drawing.Point(11, 238);
            this.textBoxSQL.Multiline = true;
            this.textBoxSQL.Name = "textBoxSQL";
            this.textBoxSQL.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxSQL.Size = new System.Drawing.Size(260, 45);
            this.textBoxSQL.TabIndex = 6;
            // 
            // btnMore
            // 
            this.btnMore.Location = new System.Drawing.Point(11, 109);
            this.btnMore.Name = "btnMore";
            this.btnMore.Size = new System.Drawing.Size(33, 21);
            this.btnMore.TabIndex = 7;
            this.btnMore.Text = ">";
            this.btnMore.UseVisualStyleBackColor = true;
            this.btnMore.Click += new System.EventHandler(this.btnOperator_Click);
            // 
            // btnLess
            // 
            this.btnLess.Location = new System.Drawing.Point(50, 109);
            this.btnLess.Name = "btnLess";
            this.btnLess.Size = new System.Drawing.Size(33, 21);
            this.btnLess.TabIndex = 8;
            this.btnLess.Text = "<";
            this.btnLess.UseVisualStyleBackColor = true;
            this.btnLess.Click += new System.EventHandler(this.btnOperator_Click);
            // 
            // btnEqual
            // 
            this.btnEqual.Location = new System.Drawing.Point(89, 109);
            this.btnEqual.Name = "btnEqual";
            this.btnEqual.Size = new System.Drawing.Size(33, 21);
            this.btnEqual.TabIndex = 9;
            this.btnEqual.Text = "=";
            this.btnEqual.UseVisualStyleBackColor = true;
            this.btnEqual.Click += new System.EventHandler(this.btnOperator_Click);
            // 
            // btnAnd
            // 
            this.btnAnd.Location = new System.Drawing.Point(11, 164);
            this.btnAnd.Name = "btnAnd";
            this.btnAnd.Size = new System.Drawing.Size(33, 21);
            this.btnAnd.TabIndex = 10;
            this.btnAnd.Text = "And";
            this.btnAnd.UseVisualStyleBackColor = true;
            this.btnAnd.Click += new System.EventHandler(this.btnOperator_Click);
            // 
            // btnNot
            // 
            this.btnNot.Location = new System.Drawing.Point(89, 164);
            this.btnNot.Name = "btnNot";
            this.btnNot.Size = new System.Drawing.Size(33, 21);
            this.btnNot.TabIndex = 11;
            this.btnNot.Text = "Not";
            this.btnNot.UseVisualStyleBackColor = true;
            this.btnNot.Click += new System.EventHandler(this.btnOperator_Click);
            // 
            // btnOr
            // 
            this.btnOr.Location = new System.Drawing.Point(50, 164);
            this.btnOr.Name = "btnOr";
            this.btnOr.Size = new System.Drawing.Size(33, 21);
            this.btnOr.TabIndex = 12;
            this.btnOr.Text = "Or";
            this.btnOr.UseVisualStyleBackColor = true;
            this.btnOr.Click += new System.EventHandler(this.btnOperator_Click);
            // 
            // btnLessOrEqual
            // 
            this.btnLessOrEqual.Location = new System.Drawing.Point(50, 136);
            this.btnLessOrEqual.Name = "btnLessOrEqual";
            this.btnLessOrEqual.Size = new System.Drawing.Size(33, 21);
            this.btnLessOrEqual.TabIndex = 15;
            this.btnLessOrEqual.Text = "<=";
            this.btnLessOrEqual.UseVisualStyleBackColor = true;
            this.btnLessOrEqual.Click += new System.EventHandler(this.btnOperator_Click);
            // 
            // btnBracket
            // 
            this.btnBracket.Location = new System.Drawing.Point(89, 136);
            this.btnBracket.Name = "btnBracket";
            this.btnBracket.Size = new System.Drawing.Size(33, 21);
            this.btnBracket.TabIndex = 14;
            this.btnBracket.Text = "()";
            this.btnBracket.UseVisualStyleBackColor = true;
            this.btnBracket.Click += new System.EventHandler(this.btnOperator_Click);
            // 
            // btnMoreOrEqual
            // 
            this.btnMoreOrEqual.Location = new System.Drawing.Point(11, 136);
            this.btnMoreOrEqual.Name = "btnMoreOrEqual";
            this.btnMoreOrEqual.Size = new System.Drawing.Size(33, 21);
            this.btnMoreOrEqual.TabIndex = 13;
            this.btnMoreOrEqual.Text = ">=";
            this.btnMoreOrEqual.UseVisualStyleBackColor = true;
            this.btnMoreOrEqual.Click += new System.EventHandler(this.btnOperator_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(68, 289);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(64, 24);
            this.btnOK.TabIndex = 16;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(138, 289);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(64, 24);
            this.btnApply.TabIndex = 17;
            this.btnApply.Text = "应用";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(208, 289);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(64, 24);
            this.btnCancel.TabIndex = 18;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // SelectByAttributesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 325);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnLessOrEqual);
            this.Controls.Add(this.btnBracket);
            this.Controls.Add(this.btnMoreOrEqual);
            this.Controls.Add(this.btnOr);
            this.Controls.Add(this.btnNot);
            this.Controls.Add(this.btnAnd);
            this.Controls.Add(this.btnEqual);
            this.Controls.Add(this.btnLess);
            this.Controls.Add(this.btnMore);
            this.Controls.Add(this.textBoxSQL);
            this.Controls.Add(this.lblSQL);
            this.Controls.Add(this.listBoxUniqueValue);
            this.Controls.Add(this.btnGetUniqueValues);
            this.Controls.Add(this.listBoxFields);
            this.Controls.Add(this.comboBoxLayers);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectByAttributesForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Text = "Select By Attributes";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxLayers;
        private System.Windows.Forms.ListBox listBoxFields;
        private System.Windows.Forms.Button btnGetUniqueValues;
        private System.Windows.Forms.ListBox listBoxUniqueValue;
        private System.Windows.Forms.Label lblSQL;
        private System.Windows.Forms.TextBox textBoxSQL;
        private System.Windows.Forms.Button btnMore;
        private System.Windows.Forms.Button btnLess;
        private System.Windows.Forms.Button btnEqual;
        private System.Windows.Forms.Button btnAnd;
        private System.Windows.Forms.Button btnNot;
        private System.Windows.Forms.Button btnOr;
        private System.Windows.Forms.Button btnLessOrEqual;
        private System.Windows.Forms.Button btnBracket;
        private System.Windows.Forms.Button btnMoreOrEqual;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnCancel;
        
    }
}