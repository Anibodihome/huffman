using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Huffman.Common.Views
{
    public partial class NumberDialog : Form
    {
        public NumberDialog()
        {
            InitializeComponent();
        }

        public NumberDialog(decimal value, string title)
            : this()
        {
            _numericUpDown.Value = value;
            this.Text = title;
        }

        public NumberDialog(decimal value, string title, decimal minimum, decimal maxmum, int decimalPlace)
            : this(value, title)
        {
            _numericUpDown.DecimalPlaces = decimalPlace;
            _numericUpDown.Minimum = minimum;
            _numericUpDown.Maximum = maxmum;
        }

        public string Title { get { return this.Text; } set { this.Text = value; } }
        public decimal Value { get { return _numericUpDown.Value; } set { _numericUpDown.Value = value; } }
        public decimal Max { get { return _numericUpDown.Maximum; } set { _numericUpDown.Maximum = value; } }
        public decimal Min { get { return _numericUpDown.Minimum; } set { _numericUpDown.Minimum = value; } }
        public int DecimalPlaces { get { return _numericUpDown.DecimalPlaces; } set { _numericUpDown.DecimalPlaces = value; } }
    }
}