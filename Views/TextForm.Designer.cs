namespace Huffman.Common.Views
{
    partial class TextForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TextForm));
            this._buttonAccept = new System.Windows.Forms.Button();
            this._buttonCancel = new System.Windows.Forms.Button();
            this._richTextBox = new System.Windows.Forms.RichTextBox();
            this._checkBoxWordWrap = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // _buttonAccept
            // 
            this._buttonAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._buttonAccept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._buttonAccept.Location = new System.Drawing.Point(448, 338);
            this._buttonAccept.Name = "_buttonAccept";
            this._buttonAccept.Size = new System.Drawing.Size(75, 23);
            this._buttonAccept.TabIndex = 1;
            this._buttonAccept.Text = "确定";
            this._buttonAccept.UseVisualStyleBackColor = true;
            // 
            // _buttonCancel
            // 
            this._buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._buttonCancel.Location = new System.Drawing.Point(529, 338);
            this._buttonCancel.Name = "_buttonCancel";
            this._buttonCancel.Size = new System.Drawing.Size(75, 23);
            this._buttonCancel.TabIndex = 2;
            this._buttonCancel.Text = "取消";
            this._buttonCancel.UseVisualStyleBackColor = true;
            // 
            // _richTextBox
            // 
            this._richTextBox.AcceptsTab = true;
            this._richTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._richTextBox.BulletIndent = 4;
            this._richTextBox.DataBindings.Add(new System.Windows.Forms.Binding("WordWrap", global::Huffman.Common.Properties.Settings.Default, "WordWrap", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this._richTextBox.Location = new System.Drawing.Point(12, 12);
            this._richTextBox.Name = "_richTextBox";
            this._richTextBox.Size = new System.Drawing.Size(592, 320);
            this._richTextBox.TabIndex = 0;
            this._richTextBox.Text = "";
            this._richTextBox.WordWrap = global::Huffman.Common.Properties.Settings.Default.WordWrap;
            // 
            // _checkBoxWordWrap
            // 
            this._checkBoxWordWrap.AutoSize = true;
            this._checkBoxWordWrap.Checked = global::Huffman.Common.Properties.Settings.Default.WordWrap;
            this._checkBoxWordWrap.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Huffman.Common.Properties.Settings.Default, "WordWrap", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this._checkBoxWordWrap.Location = new System.Drawing.Point(12, 345);
            this._checkBoxWordWrap.Name = "_checkBoxWordWrap";
            this._checkBoxWordWrap.Size = new System.Drawing.Size(72, 16);
            this._checkBoxWordWrap.TabIndex = 3;
            this._checkBoxWordWrap.Text = "自动换行";
            this._checkBoxWordWrap.UseVisualStyleBackColor = true;
            this._checkBoxWordWrap.CheckedChanged += new System.EventHandler(this._checkBoxWordWrap_CheckedChanged);
            // 
            // TextForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(616, 373);
            this.Controls.Add(this._checkBoxWordWrap);
            this.Controls.Add(this._richTextBox);
            this.Controls.Add(this._buttonCancel);
            this.Controls.Add(this._buttonAccept);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TextForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "文本编辑";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _buttonAccept;
        private System.Windows.Forms.Button _buttonCancel;
        private System.Windows.Forms.RichTextBox _richTextBox;
        private System.Windows.Forms.CheckBox _checkBoxWordWrap;
    }
}