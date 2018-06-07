namespace Huffman.Common.Views
{
    partial class TimeHorizonForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TimeHorizonForm));
            this._buttonAccept = new System.Windows.Forms.Button();
            this._buttonCancel = new System.Windows.Forms.Button();
            this._dateTimePickerBeginDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this._dateTimePickerBeginTime = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this._dateTimePickerEndDate = new System.Windows.Forms.DateTimePicker();
            this._dateTimePickerEndTime = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // _buttonAccept
            // 
            this._buttonAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._buttonAccept.Location = new System.Drawing.Point(139, 75);
            this._buttonAccept.Name = "_buttonAccept";
            this._buttonAccept.Size = new System.Drawing.Size(75, 23);
            this._buttonAccept.TabIndex = 0;
            this._buttonAccept.Text = "确定";
            this._buttonAccept.UseVisualStyleBackColor = true;
            this._buttonAccept.Click += new System.EventHandler(this._buttonAccept_Click);
            // 
            // _buttonCancel
            // 
            this._buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._buttonCancel.Location = new System.Drawing.Point(220, 75);
            this._buttonCancel.Name = "_buttonCancel";
            this._buttonCancel.Size = new System.Drawing.Size(75, 23);
            this._buttonCancel.TabIndex = 1;
            this._buttonCancel.Text = "取消";
            this._buttonCancel.UseVisualStyleBackColor = true;
            // 
            // _dateTimePickerBeginDate
            // 
            this._dateTimePickerBeginDate.Location = new System.Drawing.Point(83, 12);
            this._dateTimePickerBeginDate.Name = "_dateTimePickerBeginDate";
            this._dateTimePickerBeginDate.Size = new System.Drawing.Size(109, 21);
            this._dateTimePickerBeginDate.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "起始时间：";
            // 
            // _dateTimePickerBeginTime
            // 
            this._dateTimePickerBeginTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this._dateTimePickerBeginTime.Location = new System.Drawing.Point(215, 12);
            this._dateTimePickerBeginTime.Name = "_dateTimePickerBeginTime";
            this._dateTimePickerBeginTime.ShowUpDown = true;
            this._dateTimePickerBeginTime.Size = new System.Drawing.Size(75, 21);
            this._dateTimePickerBeginTime.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(198, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(11, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "-";
            // 
            // _dateTimePickerEndDate
            // 
            this._dateTimePickerEndDate.Location = new System.Drawing.Point(83, 39);
            this._dateTimePickerEndDate.Name = "_dateTimePickerEndDate";
            this._dateTimePickerEndDate.Size = new System.Drawing.Size(109, 21);
            this._dateTimePickerEndDate.TabIndex = 2;
            // 
            // _dateTimePickerEndTime
            // 
            this._dateTimePickerEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this._dateTimePickerEndTime.Location = new System.Drawing.Point(215, 39);
            this._dateTimePickerEndTime.Name = "_dateTimePickerEndTime";
            this._dateTimePickerEndTime.ShowUpDown = true;
            this._dateTimePickerEndTime.Size = new System.Drawing.Size(75, 21);
            this._dateTimePickerEndTime.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "终止时间：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(198, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(11, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "-";
            // 
            // TimeHorizonForm
            // 
            this.AcceptButton = this._buttonAccept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._buttonCancel;
            this.ClientSize = new System.Drawing.Size(307, 110);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._dateTimePickerEndTime);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._dateTimePickerEndDate);
            this.Controls.Add(this._dateTimePickerBeginTime);
            this.Controls.Add(this._dateTimePickerBeginDate);
            this.Controls.Add(this._buttonCancel);
            this.Controls.Add(this._buttonAccept);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TimeHorizonForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "选择时间范围";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _buttonAccept;
        private System.Windows.Forms.Button _buttonCancel;
        private System.Windows.Forms.DateTimePicker _dateTimePickerBeginDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker _dateTimePickerBeginTime;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker _dateTimePickerEndDate;
        private System.Windows.Forms.DateTimePicker _dateTimePickerEndTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
    }
}