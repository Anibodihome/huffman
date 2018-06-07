namespace Huffman.Common.Views
{
    partial class CashierForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CashierForm));
            this._button收款 = new System.Windows.Forms.Button();
            this._button取消 = new System.Windows.Forms.Button();
            this._textBox应收金额 = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this._textBox应收大写 = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this._numericUpDownCash = new System.Windows.Forms.NumericUpDown();
            this._textBox预收大写 = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this._textBox找零大写 = new System.Windows.Forms.TextBox();
            this._textBox找零金额 = new System.Windows.Forms.TextBox();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._numericUpDownCash)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // _button收款
            // 
            this._button收款.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._button收款.Location = new System.Drawing.Point(263, 210);
            this._button收款.Name = "_button收款";
            this._button收款.Size = new System.Drawing.Size(75, 23);
            this._button收款.TabIndex = 3;
            this._button收款.Text = "收款";
            this._button收款.UseVisualStyleBackColor = true;
            // 
            // _button取消
            // 
            this._button取消.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._button取消.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._button取消.Location = new System.Drawing.Point(344, 210);
            this._button取消.Name = "_button取消";
            this._button取消.Size = new System.Drawing.Size(75, 23);
            this._button取消.TabIndex = 4;
            this._button取消.Text = "取消";
            this._button取消.UseVisualStyleBackColor = true;
            // 
            // _textBox应收金额
            // 
            this._textBox应收金额.Location = new System.Drawing.Point(12, 20);
            this._textBox应收金额.Name = "_textBox应收金额";
            this._textBox应收金额.ReadOnly = true;
            this._textBox应收金额.Size = new System.Drawing.Size(132, 21);
            this._textBox应收金额.TabIndex = 0;
            this._textBox应收金额.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.AutoSize = true;
            this.groupBox3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox3.Controls.Add(this._textBox应收大写);
            this.groupBox3.Controls.Add(this._textBox应收金额);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(431, 61);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "应付金额";
            // 
            // _textBox应收大写
            // 
            this._textBox应收大写.Location = new System.Drawing.Point(150, 20);
            this._textBox应收大写.Name = "_textBox应收大写";
            this._textBox应收大写.ReadOnly = true;
            this._textBox应收大写.Size = new System.Drawing.Size(269, 21);
            this._textBox应收大写.TabIndex = 1;
            this._textBox应收大写.TabStop = false;
            // 
            // groupBox4
            // 
            this.groupBox4.AutoSize = true;
            this.groupBox4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox4.Controls.Add(this._numericUpDownCash);
            this.groupBox4.Controls.Add(this._textBox预收大写);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox4.Location = new System.Drawing.Point(0, 61);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(431, 61);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "现金";
            // 
            // _numericUpDownCash
            // 
            this._numericUpDownCash.DecimalPlaces = 2;
            this._numericUpDownCash.Location = new System.Drawing.Point(12, 20);
            this._numericUpDownCash.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this._numericUpDownCash.Name = "_numericUpDownCash";
            this._numericUpDownCash.Size = new System.Drawing.Size(132, 21);
            this._numericUpDownCash.TabIndex = 0;
            this._numericUpDownCash.ThousandsSeparator = true;
            // 
            // _textBox预收大写
            // 
            this._textBox预收大写.Location = new System.Drawing.Point(150, 20);
            this._textBox预收大写.Name = "_textBox预收大写";
            this._textBox预收大写.ReadOnly = true;
            this._textBox预收大写.Size = new System.Drawing.Size(269, 21);
            this._textBox预收大写.TabIndex = 1;
            this._textBox预收大写.TabStop = false;
            // 
            // groupBox5
            // 
            this.groupBox5.AutoSize = true;
            this.groupBox5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox5.Controls.Add(this._textBox找零大写);
            this.groupBox5.Controls.Add(this._textBox找零金额);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox5.Location = new System.Drawing.Point(0, 122);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(431, 61);
            this.groupBox5.TabIndex = 2;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "找零";
            // 
            // _textBox找零大写
            // 
            this._textBox找零大写.Location = new System.Drawing.Point(150, 20);
            this._textBox找零大写.Name = "_textBox找零大写";
            this._textBox找零大写.ReadOnly = true;
            this._textBox找零大写.Size = new System.Drawing.Size(269, 21);
            this._textBox找零大写.TabIndex = 1;
            this._textBox找零大写.TabStop = false;
            // 
            // _textBox找零金额
            // 
            this._textBox找零金额.Location = new System.Drawing.Point(12, 20);
            this._textBox找零金额.Name = "_textBox找零金额";
            this._textBox找零金额.ReadOnly = true;
            this._textBox找零金额.Size = new System.Drawing.Size(132, 21);
            this._textBox找零金额.TabIndex = 0;
            this._textBox找零金额.TabStop = false;
            // 
            // CashierForm
            // 
            this.AcceptButton = this._button收款;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._button取消;
            this.ClientSize = new System.Drawing.Size(431, 245);
            this.Controls.Add(this._button收款);
            this.Controls.Add(this._button取消);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CashierForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "付款";
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._numericUpDownCash)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _button收款;
        private System.Windows.Forms.Button _button取消;
        private System.Windows.Forms.TextBox _textBox应收金额;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox _textBox应收大写;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox _textBox预收大写;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox _textBox找零大写;
        private System.Windows.Forms.TextBox _textBox找零金额;
        private System.Windows.Forms.NumericUpDown _numericUpDownCash;
    }
}