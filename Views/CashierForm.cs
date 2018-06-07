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
        decimal _theӦ�ս�� = 0;
        decimal _theԤ�ս�� = 0;
        decimal _the������ = 0;

        public CashierForm(decimal total)
        {
            InitializeComponent();

            _theӦ�ս�� = total;

            _textBoxӦ�ս��.Text = _theӦ�ս��.ToString("C");
            _textBoxӦ�մ�д.Text = Tools.NumToCNum(_theӦ�ս��);

            _button�տ�.Click += new EventHandler(_button�տ�_Click);
            _buttonȡ��.Click += new EventHandler(_buttonȡ��_Click);

            _numericUpDownCash.ValueChanged += new EventHandler(_numericUpDownCash_ValueChanged);
        }

        public decimal TheӦ�ս��
        {
            get { return _theӦ�ս��; }
        }

        public decimal TheԤ�ս��
        {
            get { return _theԤ�ս��; }
        }

        public decimal The������
        {
            get { return _the������; }
        }


        void _numericUpDownCash_ValueChanged(object sender, EventArgs e)
        {
            _theԤ�ս�� = _numericUpDownCash.Value;
            if (_theԤ�ս�� < 0) _theԤ�ս�� = 0;
            _textBoxԤ�մ�д.Text = Tools.NumToCNum(_theԤ�ս��);

            _the������ = _theԤ�ս�� - _theӦ�ս��;
            if (_the������ < 0) _the������ = 0;
            _textBox������.Text = _the������.ToString("C");
            _textBox�����д.Text = Tools.NumToCNum(_the������);
        }

        void _buttonȡ��_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        void _button�տ�_Click(object sender, EventArgs e)
        {
            _button�տ�.Focus();

            if (_theԤ�ս�� < _theӦ�ս��)
            {
                MessageBox.Show("Ԥ�ۿ���");
                return;
            }

            DialogResult resp = MessageBox.Show("Ӧ�տ�" + _theӦ�ս��.ToString("C") + "Ԫ��\r\n"
                + "Ԥ�տ�" + _theԤ�ս��.ToString("C") + "Ԫ��\r\n"
                + "���㣺" + _the������.ToString("C") + "Ԫ��\r\n", "�տ�ȷ��",
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