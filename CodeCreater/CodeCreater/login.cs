using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeCreater
{
    public partial class login : Form
    {
        public string SqlType = "";
        public string SqlConnectString = "";
        public string SqlDataBaseName = "";
        public DataSet TESTss = new DataSet();
        public login()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ls_databasetype = "";
            string ls_myoledbconn = "";
            string ls_datasource = "", ls_username = "", ls_password = "", ls_servername = "";
            ls_databasetype = cmb_dbtype.Text.Trim();
            ls_username = tb_username.Text.Trim();
            ls_password = tb_pass.Text.Trim();
            ls_servername = tb_server.Text.Trim();
            ls_datasource = tb_database.Text.Trim();

            if (ls_databasetype.Length <= 0)
            {
                MessageBox.Show("请选择数据库类型!", "提示");
                cmb_dbtype.Focus();
                return;
            }
            if (ls_username.Length <= 0)
            {
                MessageBox.Show("请输入用户名!", "提示");
                tb_pass.Focus();
                return;
            }
            //CreateHelper.db_DisConnect();
            switch (ls_databasetype)
            {
                case "SQL SERVER":
                    if (ls_servername.Length <= 0)
                    {
                        MessageBox.Show("请输入服务器名!", "提示");
                        tb_server.Focus();
                        return;
                    }
                    if (ls_datasource.Length <= 0)
                    {
                        MessageBox.Show("请输入数据库名!", "提示");
                        tb_server.Focus();
                        return;
                    }
                    ls_myoledbconn = "Provider=sqloledb;Data Source=" + ls_servername + ";Initial Catalog=" + ls_datasource + ";User Id=" + ls_username + ";Password=" + ls_password + "";
                    break;
                case "ORACLE":
                    if (ls_datasource.Length <= 0)
                    {
                        MessageBox.Show("请输入服务名称!", "提示");
                        tb_server.Focus();
                        return;
                    }
                    ls_myoledbconn = "Provider=msdaora;Data Source=" + ls_datasource + ";User Id=" + ls_username + ";Password=" + ls_password + "";
                    break;
                case "DB2":
                    if (ls_servername.Length <= 0)
                    {
                        MessageBox.Show("请输入服务器IP地址!", "提示");
                        tb_server.Focus();
                        return;
                    }
                    //检测是否是正确的IP地址
                    if (Regex.IsMatch(ls_servername, @"^[1-2]{0,1}\d{0,2}\.[1-2]{0,1}\d{0,2}\.[1-2]{0,1}\d{0,2}\.[1-2]{0,1}\d{0,2}$") == false)
                    {
                        MessageBox.Show("请输入正确的IP地址,如192.168.90.38", "提示");
                        tb_server.Text = "";
                        tb_server.Focus();
                        return;
                    }
                    if (ls_datasource.Length <= 0)
                    {
                        MessageBox.Show("请输入数据库名!", "提示");
                        tb_server.Focus();
                        return;
                    }
                    ls_myoledbconn = "Provider=IBMDADB2.1;Data Source=" + ls_datasource + ";Mode=ReadWrite;Network Address=" + ls_servername + ";Default Schema=" + ls_username + ";Persist Security Info=True;User ID=" + ls_username + ";password=" + ls_password + "";
                    break;
                default:
                    break;
            }
            if (ls_myoledbconn.Length <= 0)
            {
                return;
            }

            CreateHelper.db_SetConnstring(ls_databasetype, ls_myoledbconn, ls_username);
            if (CreateHelper.db_Connect() == false)
            {
                MessageBox.Show("数据库连接失败!", "提示");
                return;
            }
            SqlType = ls_databasetype;
            SqlConnectString = ls_myoledbconn;
            SqlDataBaseName = ls_datasource;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void login_Load(object sender, EventArgs e)
        {
           // tb_server.Text = SystemInformation.ComputerName.ToString();
        }
    }
}
