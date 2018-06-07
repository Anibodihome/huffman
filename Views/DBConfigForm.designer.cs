namespace Huffman.Common.Views
{
    partial class DBConfigForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DBConfigForm));
            this._buttonOK = new System.Windows.Forms.Button();
            this._buttonCancel = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this._textBoxDataBaseName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this._textBoxSqlServer = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this._checkBoxAccount = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this._textBoxDatabasePassword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this._textBoxDatabaseUser = new System.Windows.Forms.TextBox();
            this._labelMessage = new System.Windows.Forms.Label();
            this._textBoxError = new System.Windows.Forms.TextBox();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // _buttonOK
            // 
            this._buttonOK.Location = new System.Drawing.Point(53, 359);
            this._buttonOK.Name = "_buttonOK";
            this._buttonOK.Size = new System.Drawing.Size(75, 23);
            this._buttonOK.TabIndex = 4;
            this._buttonOK.Text = "确定";
            this._buttonOK.UseVisualStyleBackColor = true;
            this._buttonOK.Click += new System.EventHandler(this._buttonOK_Click);
            // 
            // _buttonCancel
            // 
            this._buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._buttonCancel.Location = new System.Drawing.Point(134, 359);
            this._buttonCancel.Name = "_buttonCancel";
            this._buttonCancel.Size = new System.Drawing.Size(75, 23);
            this._buttonCancel.TabIndex = 5;
            this._buttonCancel.Text = "取消";
            this._buttonCancel.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.AutoSize = true;
            this.groupBox2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this._textBoxDataBaseName);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this._textBoxSqlServer);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(268, 88);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "服务器设置";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "数据库：";
            // 
            // _textBoxDataBaseName
            // 
            this._textBoxDataBaseName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._textBoxDataBaseName.Location = new System.Drawing.Point(65, 47);
            this._textBoxDataBaseName.Name = "_textBoxDataBaseName";
            this._textBoxDataBaseName.Size = new System.Drawing.Size(197, 21);
            this._textBoxDataBaseName.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "地址：";
            // 
            // _textBoxSqlServer
            // 
            this._textBoxSqlServer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._textBoxSqlServer.Location = new System.Drawing.Point(53, 20);
            this._textBoxSqlServer.Name = "_textBoxSqlServer";
            this._textBoxSqlServer.Size = new System.Drawing.Size(209, 21);
            this._textBoxSqlServer.TabIndex = 1;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this._checkBoxAccount);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this._textBoxDatabasePassword);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this._textBoxDatabaseUser);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(0, 88);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(268, 81);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            // 
            // _checkBoxAccount
            // 
            this._checkBoxAccount.AutoSize = true;
            this._checkBoxAccount.Location = new System.Drawing.Point(6, 0);
            this._checkBoxAccount.Name = "_checkBoxAccount";
            this._checkBoxAccount.Size = new System.Drawing.Size(132, 16);
            this._checkBoxAccount.TabIndex = 0;
            this._checkBoxAccount.Text = "使用指定数据库帐户";
            this._checkBoxAccount.UseVisualStyleBackColor = true;
            this._checkBoxAccount.CheckedChanged += new System.EventHandler(this._checkBoxAccount_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "用户：";
            // 
            // _textBoxDatabasePassword
            // 
            this._textBoxDatabasePassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._textBoxDatabasePassword.Location = new System.Drawing.Point(53, 47);
            this._textBoxDatabasePassword.Name = "_textBoxDatabasePassword";
            this._textBoxDatabasePassword.PasswordChar = '*';
            this._textBoxDatabasePassword.Size = new System.Drawing.Size(209, 21);
            this._textBoxDatabasePassword.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "口令：";
            // 
            // _textBoxDatabaseUser
            // 
            this._textBoxDatabaseUser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._textBoxDatabaseUser.Location = new System.Drawing.Point(53, 20);
            this._textBoxDatabaseUser.Name = "_textBoxDatabaseUser";
            this._textBoxDatabaseUser.Size = new System.Drawing.Size(209, 21);
            this._textBoxDatabaseUser.TabIndex = 2;
            // 
            // _labelMessage
            // 
            this._labelMessage.AutoSize = true;
            this._labelMessage.Location = new System.Drawing.Point(12, 172);
            this._labelMessage.Name = "_labelMessage";
            this._labelMessage.Size = new System.Drawing.Size(245, 48);
            this._labelMessage.TabIndex = 2;
            this._labelMessage.Text = "\r\n注意：如果您未选择[使用指定数据库账户]，\r\n\r\n那么您必须拥有足够对服务器的访问权限。";
            // 
            // _textBoxError
            // 
            this._textBoxError.Location = new System.Drawing.Point(12, 251);
            this._textBoxError.Multiline = true;
            this._textBoxError.Name = "_textBoxError";
            this._textBoxError.ReadOnly = true;
            this._textBoxError.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._textBoxError.Size = new System.Drawing.Size(245, 92);
            this._textBoxError.TabIndex = 3;
            // 
            // DBConfigForm
            // 
            this.AcceptButton = this._buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._buttonCancel;
            this.ClientSize = new System.Drawing.Size(268, 394);
            this.Controls.Add(this._textBoxError);
            this.Controls.Add(this._labelMessage);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this._buttonCancel);
            this.Controls.Add(this._buttonOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DBConfigForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据库服务器设置";
            this.TopMost = true;
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _buttonOK;
        private System.Windows.Forms.Button _buttonCancel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox _textBoxSqlServer;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox _checkBoxAccount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox _textBoxDatabasePassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox _textBoxDatabaseUser;
        private System.Windows.Forms.Label _labelMessage;
        private System.Windows.Forms.TextBox _textBoxError;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox _textBoxDataBaseName;
    }
}