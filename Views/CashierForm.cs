using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Huffman.Common.Views
{
    public partial class CashierForm : Form
    {
        decimal _the应收金额 = 0;
        decimal _the预收金额 = 0;
        decimal _the找零金额 = 0;

        public CashierForm(decimal total)
        {
            InitializeComponent();

            _the应收金额 = total;

            _textBox应收金额.Text = _the应收金额.ToString("C");
            _textBox应收大写.Text = Tools.NumToCNum(_the应收金额);

            _button收款.Click += new EventHandler(_button收款_Click);
            _button取消.Click += new EventHandler(_button取消_Click);

            _numericUpDownCash.ValueChanged += new EventHandler(_numericUpDownCash_ValueChanged);
        }

        public decimal The应收金额
        {
            get { return _the应收金额; }
        }

        public decimal The预收金额
        {
            get { return _the预收金额; }
        }

        public decimal The找零金额
        {
            get { return _the找零金额; }
        }


        void _numericUpDownCash_ValueChanged(object sender, EventArgs e)
        {
            _the预收金额 = _numericUpDownCash.Value;
            if (_the预收金额 < 0) _the预收金额 = 0;
            _textBox预收大写.Text = Tools.NumToCNum(_the预收金额);

            _the找零金额 = _the预收金额 - _the应收金额;
            if (_the找零金额 < 0) _the找零金额 = 0;
            _textBox找零金额.Text = _the找零金额.ToString("C");
            _textBox找零大写.Text = Tools.NumToCNum(_the找零金额);
        }

        void _button取消_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        void _button收款_Click(object sender, EventArgs e)
        {
            _button收款.Focus();

            if (_the预收金额 < _the应收金额)
            {
                MessageBox.Show("预售款额不足");
                return;
            }

            DialogResult resp = MessageBox.Show("应收款额：" + _the应收金额.ToString("C") + "元整\r\n"
                + "预收款额：" + _the预收金额.ToString("C") + "元整\r\n"
                + "找零：" + _the找零金额.ToString("C") + "元整\r\n", "收款确认",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
            if (resp != DialogResult.OK) return;

            DialogResult = DialogResult.OK;
            Close();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            _numericUpDownCash.Select(0, (_numericUpDownCash as UpDownBase).Text.Length);
            _numericUpDownCash.Focus();

            this._numericUpDownCash_ValueChanged(this, null);
        }
    }
}