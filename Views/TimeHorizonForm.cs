using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Huffman.Common.Views
{
    public partial class TimeHorizonForm : Form
    {
        public TimeHorizonForm()
        {
            InitializeComponent();
        }

        public TimeHorizonForm(DateTime begin, DateTime end)
        {
            InitializeComponent();

            this.BeginTime = begin;
            this.EndTime = end;
        }

        public DateTime BeginTime
        {
            get
            {
                return new DateTime(_dateTimePickerBeginDate.Value.Year, _dateTimePickerBeginDate.Value.Month, _dateTimePickerBeginDate.Value.Day,
                    _dateTimePickerBeginTime.Value.Hour, _dateTimePickerBeginTime.Value.Minute, _dateTimePickerBeginTime.Value.Second);
            }
            set
            {
                _dateTimePickerBeginDate.Value = value;
                _dateTimePickerBeginTime.Value = value;
            }
        }

        public DateTime EndTime
        {
            get
            {
                return new DateTime(_dateTimePickerEndDate.Value.Year, _dateTimePickerEndDate.Value.Month, _dateTimePickerEndDate.Value.Day,
                    _dateTimePickerEndTime.Value.Hour, _dateTimePickerEndTime.Value.Minute, _dateTimePickerEndTime.Value.Second);
            }
            set
            {
                _dateTimePickerEndDate.Value = value;
                _dateTimePickerEndTime.Value = value;
            }
        }

        public TimeSpan TimeHorizon
        {
            get
            {
                return this.EndTime - BeginTime;
            }
        }

        private void _buttonAccept_Click(object sender, EventArgs e)
        {
            if (this.EndTime < this.BeginTime)
            {
                MessageBox.Show("终止时间不能早于起始时间");
                return;
            }

            this.DialogResult = DialogResult.OK;
            Close();
        }
    }
}