using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Huffman.Common.Views
{
    public partial class TextForm : Form
    {
        public TextForm()
        {
            InitializeComponent();
        }

        public TextForm(string text)
        {
            InitializeComponent();
            _richTextBox.Text = text;
        }

        public string Result { get { return _richTextBox.Text; } set { _richTextBox.Text = value; } }

        private void _checkBoxWordWrap_CheckedChanged(object sender, EventArgs e)
        {
            bool wrap = _checkBoxWordWrap.Checked;
            _richTextBox.WordWrap = wrap;

            Properties.Settings.Default.WordWrap = wrap;
            Properties.Settings.Default.Save();
        }
    }
}