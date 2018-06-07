using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;

namespace Huffman.Common.Views
{
    public partial class FormSqlBackup : Form
    {
        private static Server srvSql;

        public FormSqlBackup()
        {
            InitializeComponent();
        }

        private void _buttonRefresh_Click(object sender, EventArgs e)
        {
            _buttonRefresh.Enabled = false;
            this.Cursor = Cursors.WaitCursor;

            try
            {
                cmbServer.Items.Clear();

                // Create a DataTable where we enumerate the available servers
                DataTable dtServers = SmoApplication.EnumAvailableSqlServers(true);

                // If there are any servers at all
                if (dtServers.Rows.Count > 0)
                {
                    // Loop through each server in the DataTable
                    foreach (DataRow drServer in dtServers.Rows)
                    {
                        // Add the name to the combobox
                        cmbServer.Items.Add(drServer["Name"]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "发现数据库服务器失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                _buttonRefresh.Enabled = true;
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            btnConnect.Enabled = false;
            this.Cursor = Cursors.WaitCursor;

            try
            {
                // Create a new connection to the selected server name
                ServerConnection srvConn = new ServerConnection(cmbServer.Text);

                if (!string.IsNullOrEmpty(txtUsername.Text.Trim()))
                {
                    // Log in using SQL authentication instead of Windows authentication
                    srvConn.LoginSecure = false;

                    // Give the login username
                    srvConn.Login = txtUsername.Text;

                    // Give the login password
                    srvConn.Password = txtPassword.Text;
                }

                // Create a new SQL Server object using the connection we created
                srvSql = new Server(srvConn);

                cmbDatabase.Items.Clear();
                // Loop through the databases list
                foreach (Database dbServer in srvSql.Databases)
                {
                    // Add database to combobox
                    cmbDatabase.Items.Add(dbServer.Name);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "连接失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                btnConnect.Enabled = true;
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            btnCreate.Enabled = false;
            this.Cursor = Cursors.WaitCursor;

            try
            {
                // If there was a SQL connection created
                if (srvSql != null)
                {
                    // If the user has chosen a path where to save the backup file
                    if (saveBackupDialog.ShowDialog() == DialogResult.OK)
                    {
                        // Create a new backup operation
                        Backup bkpDatabase = new Backup();
                        // Set the backup type to a database backup
                        bkpDatabase.Action = BackupActionType.Database;
                        // Set the database that we want to perform a backup on
                        bkpDatabase.Database = cmbDatabase.SelectedItem.ToString();

                        // Set the backup device to a file
                        BackupDeviceItem bkpDevice = new BackupDeviceItem(saveBackupDialog.FileName, DeviceType.File);
                        // Add the backup device to the backup
                        bkpDatabase.Devices.Add(bkpDevice);
                        // Perform the backup
                        bkpDatabase.SqlBackup(srvSql);
                    }
                }
                else
                {
                    // There was no connection established; probably the Connect button was not clicked
                    throw new Exception("无法创建到SQL服务器的连接");
                }

                MessageBox.Show("备份完成");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, " 备份失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                btnCreate.Enabled = true;
            }
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            this.btnRestore.Enabled = false;
            this.Cursor = Cursors.WaitCursor;

            try
            {
                // If there was a SQL connection created
                if (srvSql != null)
                {
                    // If the user has chosen the file from which he wants the database to be restored
                    if (openBackupDialog.ShowDialog() == DialogResult.OK)
                    {
                        // Create a new database restore operation
                        Restore rstDatabase = new Restore();
                        // Set the restore type to a database restore
                        rstDatabase.Action = RestoreActionType.Database;
                        // Set the database that we want to perform the restore on
                        rstDatabase.Database = cmbDatabase.SelectedItem.ToString();

                        // Set the backup device from which we want to restore, to a file
                        BackupDeviceItem bkpDevice = new BackupDeviceItem(openBackupDialog.FileName, DeviceType.File);
                        // Add the backup device to the restore type
                        rstDatabase.Devices.Add(bkpDevice);
                        // If the database already exists, replace it
                        rstDatabase.ReplaceDatabase = true;
                        // Perform the restore
                        rstDatabase.SqlRestore(srvSql);
                    }
                }
                else
                {
                    // There was no connection established; probably the Connect button was not clicked
                    throw new Exception("无法创建到SQL服务器的连接。");
                }
                MessageBox.Show("恢复完成");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "恢复失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                this.btnRestore.Enabled = true;
            }
        }
    }
}