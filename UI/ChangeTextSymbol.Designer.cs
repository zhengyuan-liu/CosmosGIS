namespace CosmosGIS.UI
{
    partial class ChangeTextSymbol
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
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_OK = new System.Windows.Forms.Button();
            this.TextStyleMessage = new System.Windows.Forms.GroupBox();
            this.TextShow = new System.Windows.Forms.Label();
            this.btn_I = new System.Windows.Forms.Button();
            this.btn_B = new System.Windows.Forms.Button();
            this.btn_SelectColor = new System.Windows.Forms.Button();
            this.CB_FontSize = new System.Windows.Forms.ComboBox();
            this.CB_FontStyle = new System.Windows.Forms.ComboBox();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.groupBox1.SuspendLayout();
            this.TextStyleMessage.SuspendLayout();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(18, 27);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(649, 128);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.richTextBox1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(684, 166);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "内容";
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Location = new System.Drawing.Point(581, 301);
            this.btn_Cancel.Margin = new System.Windows.Forms.Padding(4);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(98, 34);
            this.btn_Cancel.TabIndex = 9;
            this.btn_Cancel.Text = "取消";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_OK
            // 
            this.btn_OK.Location = new System.Drawing.Point(581, 251);
            this.btn_OK.Margin = new System.Windows.Forms.Padding(4);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(98, 34);
            this.btn_OK.TabIndex = 7;
            this.btn_OK.Text = "确定";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // TextStyleMessage
            // 
            this.TextStyleMessage.Controls.Add(this.TextShow);
            this.TextStyleMessage.Controls.Add(this.btn_I);
            this.TextStyleMessage.Controls.Add(this.btn_B);
            this.TextStyleMessage.Controls.Add(this.btn_SelectColor);
            this.TextStyleMessage.Controls.Add(this.CB_FontSize);
            this.TextStyleMessage.Controls.Add(this.CB_FontStyle);
            this.TextStyleMessage.Location = new System.Drawing.Point(12, 196);
            this.TextStyleMessage.Margin = new System.Windows.Forms.Padding(4);
            this.TextStyleMessage.Name = "TextStyleMessage";
            this.TextStyleMessage.Padding = new System.Windows.Forms.Padding(4);
            this.TextStyleMessage.Size = new System.Drawing.Size(516, 139);
            this.TextStyleMessage.TabIndex = 6;
            this.TextStyleMessage.TabStop = false;
            this.TextStyleMessage.Text = "文本符号";
            // 
            // TextShow
            // 
            this.TextShow.BackColor = System.Drawing.Color.LightGray;
            this.TextShow.Location = new System.Drawing.Point(27, 48);
            this.TextShow.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.TextShow.Name = "TextShow";
            this.TextShow.Size = new System.Drawing.Size(207, 75);
            this.TextShow.TabIndex = 6;
            this.TextShow.Text = "AAABBBCCC";
            this.TextShow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_I
            // 
            this.btn_I.BackColor = System.Drawing.Color.White;
            this.btn_I.Location = new System.Drawing.Point(362, 94);
            this.btn_I.Margin = new System.Windows.Forms.Padding(4);
            this.btn_I.Name = "btn_I";
            this.btn_I.Size = new System.Drawing.Size(36, 30);
            this.btn_I.TabIndex = 5;
            this.btn_I.Text = "I";
            this.btn_I.UseVisualStyleBackColor = false;
            this.btn_I.Click += new System.EventHandler(this.btn_I_Click);
            // 
            // btn_B
            // 
            this.btn_B.BackColor = System.Drawing.Color.White;
            this.btn_B.Location = new System.Drawing.Point(316, 94);
            this.btn_B.Margin = new System.Windows.Forms.Padding(4);
            this.btn_B.Name = "btn_B";
            this.btn_B.Size = new System.Drawing.Size(36, 30);
            this.btn_B.TabIndex = 4;
            this.btn_B.Text = "B";
            this.btn_B.UseVisualStyleBackColor = false;
            this.btn_B.Click += new System.EventHandler(this.btn_B_Click);
            // 
            // btn_SelectColor
            // 
            this.btn_SelectColor.BackColor = System.Drawing.Color.Black;
            this.btn_SelectColor.Location = new System.Drawing.Point(272, 94);
            this.btn_SelectColor.Margin = new System.Windows.Forms.Padding(4);
            this.btn_SelectColor.Name = "btn_SelectColor";
            this.btn_SelectColor.Size = new System.Drawing.Size(36, 30);
            this.btn_SelectColor.TabIndex = 3;
            this.btn_SelectColor.UseVisualStyleBackColor = false;
            this.btn_SelectColor.Click += new System.EventHandler(this.btn_SelectColor_Click);
            // 
            // CB_FontSize
            // 
            this.CB_FontSize.FormattingEnabled = true;
            this.CB_FontSize.Location = new System.Drawing.Point(272, 63);
            this.CB_FontSize.Margin = new System.Windows.Forms.Padding(4);
            this.CB_FontSize.Name = "CB_FontSize";
            this.CB_FontSize.Size = new System.Drawing.Size(150, 26);
            this.CB_FontSize.TabIndex = 2;
            this.CB_FontSize.SelectedIndexChanged += new System.EventHandler(this.CB_FontSize_SelectedIndexChanged);
            // 
            // CB_FontStyle
            // 
            this.CB_FontStyle.FormattingEnabled = true;
            this.CB_FontStyle.Location = new System.Drawing.Point(272, 29);
            this.CB_FontStyle.Margin = new System.Windows.Forms.Padding(4);
            this.CB_FontStyle.Name = "CB_FontStyle";
            this.CB_FontStyle.Size = new System.Drawing.Size(192, 26);
            this.CB_FontStyle.TabIndex = 1;
            this.CB_FontStyle.SelectedIndexChanged += new System.EventHandler(this.CB_FontStyle_SelectedIndexChanged);
            // 
            // ChangeTextSymbol
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(721, 348);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_OK);
            this.Controls.Add(this.TextStyleMessage);
            this.Controls.Add(this.groupBox1);
            this.Name = "ChangeTextSymbol";
            this.Text = "ChangeTextSymbol";
            this.groupBox1.ResumeLayout(false);
            this.TextStyleMessage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.GroupBox TextStyleMessage;
        private System.Windows.Forms.Label TextShow;
        private System.Windows.Forms.Button btn_I;
        private System.Windows.Forms.Button btn_B;
        private System.Windows.Forms.Button btn_SelectColor;
        private System.Windows.Forms.ComboBox CB_FontSize;
        private System.Windows.Forms.ComboBox CB_FontStyle;
        private System.Windows.Forms.ColorDialog colorDialog1;
    }
}