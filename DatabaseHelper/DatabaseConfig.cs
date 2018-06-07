using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections;

namespace Huffman.Common.DatabaseHelper
{
    public class DatabaseConfig
    {
        public DatabaseConfig() { }
        public DatabaseConfig(string connection)
        {
            if (connection == null) throw new Exception("连接串不存在");
            if (connection.Length == 0) throw new Exception("连接串为空");

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

            if (!parameters.TryGetValue("DATA SOURCE", out _server)) throw new Exception("读取连接串中服务器地址错误");

            parameters.TryGetValue("INITIAL CATALOG", out _database);

            string integrated = null;
            _integratedSecurity = false;
            parameters.TryGetValue("INTEGRATED SECURITY", out integrated);
            if (integrated != null) _integratedSecurity = (integrated.ToUpper() == "TRUE");

            if (!_integratedSecurity)
            {
                parameters.TryGetValue("USER ID", out  _userName);
                parameters.TryGetValue("PASSWORD", out  _password);
            }
        }

        public string getConnectionString(bool withDatabase)
        {
            string result = "";

            if (_server == null) throw new Exception("数据库服务器地址不存在");
            if (_server.Length == 0) throw new Exception("数据库服务器地址为空");
            result += "Data Source=" + _server + ";";

            if (withDatabase)
            {
                if (_database != null)
                    if (_database.Length > 0)
                        result += "Initial Catalog=" + _database + ";";
            }

            if (_integratedSecurity)
                result += "Integrated Security=True;";
            else
            {
                result += "Integrated Security=False;";
                if (_userName == null) throw new Exception("用户名不存在");
                if (_userName.Length == 0) throw new Exception("用户名为空");
                result += "User ID=" + _userName + ";";
                result += "Password=" + _password + ";";
            }

            return result;
        }

        public override string ToString()
        {
            return this.getConnectionString(true);
        }

        bool _integratedSecurity;
        public bool IntegratedSecurity
        {
            get { return _integratedSecurity; }
            set { _integratedSecurity = value; }
        }

        string _server;
        public string Server
        {
            get { return _server; }
            set { _server = value; }
        }

        string _userName;
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        string _database;
        public string Database
        {
            get { return _database; }
            set { _database = value; }
        }
    }
}
