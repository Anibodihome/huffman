using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Xml;
using System.Data;

namespace Huffman.Common.DatabaseHelper
{
    /// <summary>
    /// 数据层数据操作类
    /// </summary>
    public class Database : IDisposable
    {
        // 数据库连接对象
        public SqlConnection Connection;

        public Database()
        {
            this.Open();
        }

        /// <summary>
        /// 运行存储过程
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="prams">存储过程参数</param>
        /// <returns>由存储过程返回值</returns>
        public int RunProc(string procName, SqlParameter[] prams)
        {
            SqlCommand cmd = CreateCommand(procName, prams);
            cmd.ExecuteNonQuery();
            this.Close();
            return (int)cmd.Parameters["ReturnValue"].Value;
        }

        /// <summary>
        /// 运行存储过程，仅单值查询
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="prams">存储过程参数列表</param>
        /// <param name="OneValue">输出单值查询数据</param>
        public void RunProc(string procName, SqlParameter[] prams, out Object OneValue)
        {
            SqlCommand cmd = CreateCommand(procName, prams);
            OneValue = cmd.ExecuteScalar();
            this.Close();
        }

        /// <summary>
        /// 运行存储过程，并获取数据阅读器
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="prams">存储过程参数</param>
        /// <param name="dataReader">返回存储过程结果集</param>
        public void RunProc(string procName, SqlParameter[] prams, out SqlDataReader dataReader)
        {
            SqlCommand cmd = CreateCommand(procName, prams);
            dataReader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
        }

        /// <summary>
        /// 运行存储过程，并获取XML流数据阅读器
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="prams">存储过程参数列表</param>
        /// <param name="xmlReader">返回存储过程结果集（XML数据）</param>
        public void RunProc(string procName, SqlParameter[] prams, out XmlTextReader xmlReader)
        {
            SqlCommand cmd = CreateCommand(procName, prams);
            xmlReader = (XmlTextReader)cmd.ExecuteXmlReader();
        }

        /// <summary>
        /// 运行存储过程，并返回单个数据表
        /// </summary>
        /// <param name="ProcedureName">存储过程名称</param>
        /// <param name="SqlPrams">存储过程参数</param>
        /// <param name="SqlDT">数据表对象</param>
        public void RunProc(string ProcedureName, SqlParameter[] SqlPrams, DataTable SqlDT)
        {
            SqlDataAdapter SqlDA = CreateDataAdapter(ProcedureName, SqlPrams);
            SqlDA.Fill(SqlDT);
        }

        /// <summary>
        /// 运行存储过程，并返回数据集（主要用于创建包含多个数据表的数据集）
        /// </summary>
        /// <param name="ProcedureName">存储过程名称</param>
        /// <param name="SqlPrams">存储过程参数</param>
        /// <param name="SqlDS">数据集对象</param>
        public void RunProc(string ProcedureName, SqlParameter[] SqlPrams, DataSet SqlDS)
        {
            SqlDataAdapter SqlDA = CreateDataAdapter(ProcedureName, SqlPrams);
            SqlDA.Fill(SqlDS);
        }

        /// <summary>
        /// 运行存储过程，并返回指定记录范围的数据表
        /// </summary>
        /// <param name="ProcedureName">存储过程名称</param>
        /// <param name="SqlPrams">存储过程参数</param>
        /// <param name="SqlDT">数据表对象</param>
        /// <param name="StartRecord">开始记录</param>
        /// <param name="MaxRecord">最大记录数</param>
        public void RunProc(string ProcedureName, SqlParameter[] SqlPrams, DataTable SqlDT, int StartRecord, int MaxRecord)
        {
            DataSet SqlDS = new DataSet();
            SqlDS.Tables.Add(SqlDT);
            SqlDataAdapter SqlDA = CreateDataAdapter(ProcedureName, SqlPrams);
            SqlDA.Fill(SqlDS, StartRecord, MaxRecord, SqlDT.TableName);
        }

        /// <summary>
        /// 创建数据适配器对象
        /// </summary>
        /// <param name="ProcedureName">存储过程名</param>
        /// <param name="SqlPrams">存储过程参数</param>
        /// <param name="Type">参数类型</param>
        /// <returns>返回数据适配器对象</returns>
        private SqlDataAdapter CreateDataAdapter(string ProcedureName, SqlParameter[] SqlPrams)
        {
            //打开数据库连接
            Open();

            SqlDataAdapter SqlDA = new SqlDataAdapter(ProcedureName, Connection);

            SqlDA.SelectCommand.CommandType = CommandType.StoredProcedure;
            // 添加储存过程参数
            if (SqlPrams != null)
            {
                foreach (SqlParameter Sqlparameter in SqlPrams)
                {
                    SqlDA.SelectCommand.Parameters.Add(Sqlparameter);
                }
            }

            // 返回参数
            SqlDA.SelectCommand.Parameters.Add(
                new SqlParameter("ReturnValue", SqlDbType.Int, 4,
                ParameterDirection.ReturnValue, false, 0, 0,
                string.Empty, DataRowVersion.Default, null));

            return SqlDA;
        }

        /// <summary>
        /// 创建调用存储过程的命令对象
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="prams">存储过程参数</param>
        /// <returns>返回命令对象</returns>
        public SqlCommand CreateCommand(string procName, SqlParameter[] prams)
        {
            // 确定数据库连接被打开
            Open();

            SqlCommand cmd = new SqlCommand(procName, Connection);
            cmd.CommandType = CommandType.StoredProcedure;

            // 添加命令对象参数
            if (prams != null)
            {
                foreach (SqlParameter parameter in prams)
                    cmd.Parameters.Add(parameter);
            }

            // 添加命令对象返回参数
            cmd.Parameters.Add(
                new SqlParameter("ReturnValue", SqlDbType.Int, 4,
                ParameterDirection.ReturnValue, false, 0, 0,
                string.Empty, DataRowVersion.Default, null));

            return cmd;
        }

        /// <summary>
        /// 打开数据库连接
        /// </summary>
        private void Open()
        {
            // 打开数据库连接
            if (Connection == null)
            {
                string theConnStr = Properties.Settings.Default.ConnString;
                if (string.IsNullOrEmpty(theConnStr)) throw new Exception("尚未配置数据库连接");
                // SqlConn = new SqlConnection("user id='ren';password='12903';initial catalog='WYGL_asp';data source='(CM26-DEV)';");
                Connection = new SqlConnection(theConnStr);
            }
            if (Connection != null && Connection.State == ConnectionState.Closed)
            {
                Connection.Open();
            }
        }

        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        public void Close()
        {
            if (Connection != null)
                Connection.Close();
        }

        /// <summary>
        /// 释放数据库连接资源
        /// </summary>
        public void Dispose()
        {
            // 确定数据库连接是关闭的
            if (Connection != null)
            {
                Connection.Dispose();
                Connection = null;
            }
        }

        /// <summary>
        /// 产生输入参数
        /// </summary>
        /// <param name="ParamName">参数名称</param>
        /// <param name="DbType">参数的类型</param>
        /// <param name="Size">参数的字节宽度</param>
        /// <param name="Value">参数的实值</param>
        /// <returns>返回参数对象</returns>
        public SqlParameter MakeInParam(string ParamName, SqlDbType DbType, int Size, object Value)
        {
            return MakeParam(ParamName, DbType, Size, ParameterDirection.Input, Value);
        }

        /// <summary>
        /// 产生输出参数
        /// </summary>
        /// <param name="ParamName">参数名称</param>
        /// <param name="DbType">参数的类型</param>
        /// <param name="Size">参数的字节宽度</param>
        /// <returns>返回参数对象</returns>
        public SqlParameter MakeOutParam(string ParamName, SqlDbType DbType, int Size)
        {
            return MakeParam(ParamName, DbType, Size, ParameterDirection.Output, null);
        }

        /// <summary>
        /// 创建参数
        /// </summary>
        /// <param name="ParamName">参数的名称</param>
        /// <param name="DbType">参数的类型</param>
        /// <param name="Size">参数的字节宽度</param>
        /// <param name="Direction">参数的输入输出类型</param>
        /// <param name="Value">参数的实值</param>
        /// <returns>返回参数对象</returns>
        public SqlParameter MakeParam(string ParamName, SqlDbType DbType, Int32 Size, ParameterDirection Direction, object Value)
        {
            SqlParameter param;

            if (Size > 0)
                param = new SqlParameter(ParamName, DbType, Size);
            else
                param = new SqlParameter(ParamName, DbType);

            param.Direction = Direction;
            if (!(Direction == ParameterDirection.Output && Value == null))
                param.Value = Value;
            return param;
        }

        public static string ConnString
        {
            get
            {
                string result = string.IsNullOrEmpty(Properties.Settings.Default.ConnString) ?
                    string.Empty : Properties.Settings.Default.ConnString.ToString();
                return result;
            }

            set
            {
                string connString = string.IsNullOrEmpty(value) ? string.Empty : value.ToString();
                Properties.Settings.Default.ConnString = connString;
                Properties.Settings.Default.Save();
            }
        }
    }
}