using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Huffman.Common.Views
{
    public partial class AboutForm : Form
    {
        public AboutForm(string product)
        {
            InitializeComponent();

            _labelTitle.Text = product;

            buttonOK.Click += new EventHandler(buttonOK_Click);
            _linkLabelEmail.Click += new EventHandler(_linkLabelEmail_Click);
        }

        void _linkLabelEmail_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("mailto:huffman.hwang@gmail.com?subject=To Huffman:");
        }

        void buttonOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}