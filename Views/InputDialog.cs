using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Huffman.Common.Views
{
    public partial class InputDialog : Form
    {
        public InputDialog()
        {
            InitializeComponent();
        }

        public InputDialog(string title)
            : this()
        {
            this.Text = title;
        }

        public InputDialog(string title, bool password)
            : this(title)
        {
            _textBoxInput.UseSystemPasswordChar = true;
        }

        public string Input { get { return _textBoxInput.Text; } set { _textBoxInput.Text = value; } }
    }
}