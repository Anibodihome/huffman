using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Huffman.Common.Authentication
{
    public partial class FormLogIn : Form
    {
        public string UserName
        {
            get { return _textBoxUsername.Text.ToString(); }
            set { _textBoxUsername.Text = (value ?? string.Empty).ToString(); }
        }

        public string Password
        {
            get { return _textBoxPassword.Text.ToString(); }
            set { _textBoxPassword.Text = (value ?? string.Empty).ToString(); }
        }

        public FormLogIn()
        {
            InitializeComponent();

            _buttonAccept.Click += new EventHandler(_buttonAccept_Click);
            _buttonCancel.Click += new EventHandler(_buttonCancel_Click);
        }

        void _buttonAccept_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        void _buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}