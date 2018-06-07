using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Huffman.Common.Views
{
    public partial class DBConfigForm : Form
    {
        string _connectionString = null;

        public string ConnectionString
        {
            get { return _connectionString; }
        }

        public DBConfigForm(string connection, string message)
        {
            InitializeComponent();

            this.AcceptButton = _buttonOK;
            this.CancelButton = _buttonCancel;

            if (string.IsNullOrEmpty(connection)) return;

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            StringReader sr = new StringReader(connection.Replace(";", "\r\n"));
            while (sr.Peek() != -1)
            {
                string line = sr.ReadLine();

                if (line == null) break;
                if (line.Trim().Length < 3) continue;

                string[] item = line.Split(new char[1] { '=' }, 2, StringSplitOptions.RemoveEmptyEntries);
                parameters.Add(item[0].Trim().ToUpper(), item[1].Trim());
            }
            sr.Close();

            string serverAddress = null;
            parameters.TryGetValue("DATA SOURCE", out serverAddress);
            _textBoxSqlServer.Text = serverAddress;

            string serverDatabase = null;
            parameters.TryGetValue("INITIAL CATALOG", out serverDatabase);
            _textBoxDataBaseName.Text = serverDatabase;

            string integrateState = null;
            bool integrated = false;
            parameters.TryGetValue("INTEGRATED SECURITY", out integrateState);
            if (integrateState != null) integrated = (integrateState.ToUpper() == "TRUE");
            _checkBoxAccount.Checked = !integrated;

            string userName = null;
            parameters.TryGetValue("USER ID", out  userName);
            _textBoxDatabaseUser.Text = userName;

            string password = null;
            parameters.TryGetValue("PASSWORD", out  password);
            _textBoxDatabasePassword.Text = password;

            _textBoxError.Text = message;
        }

        private void _buttonOK_Click(object sender, EventArgs e)
        {
            _connectionString = "";

            try
            {
                if (string.IsNullOrEmpty(_textBoxSqlServer.Text)) throw new Exception("数据库服务器地址不存在");
                _connectionString += "Data Source=" + _textBoxSqlServer.Text + ";";

                _connectionString += "Initial Catalog=" + _textBoxDataBaseName.Text + ";";

                if (_checkBoxAccount.Checked)
                {
                    _connectionString += "Integrated Security=False;";
                    if (string.IsNullOrEmpty(_textBoxDatabaseUser.Text)) throw new Exception("必须输入用户名");
                    _connectionString += "User ID=" + _textBoxDatabaseUser.Text + ";";
                    _connectionString += "Password=" + _textBoxDatabasePassword.Text + ";";
                }
                else _connectionString += "Integrated Security=True;";

                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "数据库服务器设置错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void _checkBoxAccount_CheckedChanged(object sender, EventArgs e)
        {
            _textBoxDatabaseUser.Enabled = _textBoxDatabasePassword.Enabled = _checkBoxAccount.Checked;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            _checkBoxAccount_CheckedChanged(this, null);
        }
    }
}