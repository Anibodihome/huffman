namespace Huffman.Common.Authentication
{
    partial class FormLogIn
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLogIn));
            this._buttonAccept = new System.Windows.Forms.Button();
            this._buttonCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this._textBoxUsername = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this._textBoxPassword = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // _buttonAccept
            // 
            resources.ApplyResources(this._buttonAccept, "_buttonAccept");
            this._buttonAccept.Name = "_buttonAccept";
            this._buttonAccept.UseVisualStyleBackColor = true;
            // 
            // _buttonCancel
            // 
            resources.ApplyResources(this._buttonCancel, "_buttonCancel");
            this._buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._buttonCancel.Name = "_buttonCancel";
            this._buttonCancel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // _textBoxUsername
            // 
            resources.ApplyResources(this._textBoxUsername, "_textBoxUsername");
            this._textBoxUsername.Name = "_textBoxUsername";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // _textBoxPassword
            // 
            resources.ApplyResources(this._textBoxPassword, "_textBoxPassword");
            this._textBoxPassword.Name = "_textBoxPassword";
            this._textBoxPassword.UseSystemPasswordChar = true;
            // 
            // FormLogIn
            // 
            this.AcceptButton = this._buttonAccept;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._buttonCancel;
            this.Controls.Add(this._textBoxPassword);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._textBoxUsername);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._buttonCancel);
            this.Controls.Add(this._buttonAccept);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormLogIn";
            this.ShowInTaskbar = false;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _buttonAccept;
        private System.Windows.Forms.Button _buttonCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox _textBoxUsername;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox _textBoxPassword;
    }
}