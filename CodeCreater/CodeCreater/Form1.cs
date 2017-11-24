using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeCreater
{
    public partial class Form1 : Form
    {

        public string sql_type = "";
        public string sql_connectstring = "";
        public string sql_database = "";
        public string tabType = "table";
        public string tabName = "";


        public Form1(string as_type, string as_connectstring, string as_database)
        {
            sql_type = as_type;
            sql_connectstring = as_connectstring;
            sql_database = as_database;
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            TreeNode node = new TreeNode();
            try
            {
                SqlConnection conn = new SqlConnection(sql_connectstring.Replace("Provider=sqloledb;", ""));
                conn.Open();
                List<string> ulist = CreateHelper.GetTable(conn, "U").ToList();
                List<string> vlist = CreateHelper.GetTable(conn, "V").ToList();
                node.Text = "表";
                treeView1.Nodes.Add(node);
                ulist.ForEach(l =>
                {
                    TreeNode unode = new TreeNode();
                    unode.Text = l;

                    node.Nodes.Add(unode);
                });
                TreeNode node1 = new TreeNode();
                node1.Text = "试图";
                treeView1.Nodes.Add(node1);
                vlist.ForEach(l =>
                {
                    TreeNode vnode = new TreeNode();
                    vnode.Text = l;
                    node1.Nodes.Add(vnode);
                });
            }
            catch (Exception ex)
            { }



        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Parent != null && e.Node.Parent.Text == "表")
            {
                tabName = e.Node.Text;
                DataTable dt = CreateHelper.GetSyscolumns(sql_connectstring.Replace("Provider=sqloledb;", ""), e.Node.Text);
                this.dataGridView1.DataSource = dt;//数据源  
                this.dataGridView1.AutoGenerateColumns = false;//不自动  
            }
            else if (e.Node.Parent != null && e.Node.Parent.Text == "试图")
            {
                tabName = e.Node.Text;
                DataTable dt = CreateHelper.GetVSyscolumns(sql_connectstring.Replace("Provider=sqloledb;", ""), e.Node.Text);
                this.dataGridView1.DataSource = dt;//数据源  
                this.dataGridView1.AutoGenerateColumns = false;//不自动  
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)this.dataGridView1.DataSource;

            if (dt != null && dt.Rows.Count > 0)
            {
                dt.TableName = tabName;
                jsonHelper.WriteFile(CreateHelper.getPath(tabName), tabName + ".js", jsonHelper.ToJson(dt, true));
            }
            CreateFunc(dt);
            MessageBox.Show("生成成功！！！！");
        }
        bool CreateFunc(DataTable dt)
        {
            string functionName = txt_Function.Text;
            string Namespace = txt_Namespace.Text;
            string prijectName = txt_prijoct.Text;
            string dataContext = txt_dataContext.Text;
            new CreateModels(dt, Namespace).setModel();
            // new CreateDomain(dt, Namespace, dataContext).setDomainInterface();
            new CreateDomain(dt, Namespace, dataContext).setDomainService();
            new CreateControllers(dt, Namespace, prijectName, functionName).setController();
            new CreateControllers(functionName, dt.TableName).Setmapping();
            new CreateViews(dt, Namespace, prijectName, functionName).setViewAdd();
            new CreateViews(dt, Namespace, prijectName, functionName).setViewList();
            return true;
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string flag = this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().ToLower();
                if (flag == "true")
                {
                    this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = false;
                }
                else if (flag == "false")
                {
                    this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = true;
                }
            }
        }

    }
}
