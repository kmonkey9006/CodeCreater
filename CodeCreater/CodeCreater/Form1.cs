using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
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
                treeView1.CheckBoxes = true;
            }
            catch (Exception ex)
            { }



        }



        ///// <summary>  
        ///// 获得选中节点  
        ///// </summary>  
        ///// <param name="sender"></param>  
        ///// <param name="e"></param>  
        //private void button1_Click(object sender, EventArgs e)
        //{
        //    List<TreeNode> listNodes = new List<TreeNode>();
        //    foreach (TreeNode node in treeView1.Nodes)
        //    {
        //        FindCheckNode(node, listNodes);
        //    }
        //    this.Close();
        //}

        private void FindCheckNode(TreeNode node, List<TreeNode> listNodes)
        {
            if (node.Checked)
            {
                listNodes.Add(node);
                //Form1.str += node.Text+",";  
                //  Form1.str += node.Tag + ",";

            }
            foreach (TreeNode childnode in node.Nodes)
            {
                FindCheckNode(childnode, listNodes);
            }
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
            if (CreateFunc(dt))
                MessageBox.Show("生成成功！！！！");
            else
                MessageBox.Show("生成失败！！！！");
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

        private void button2_Click(object sender, EventArgs e)
        {
            //初始化一个OpenFileDialog类
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "(*.js)|*.js";
            //判断用户是否正确的选择了文件
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                string path = fileDialog.FileName;
                string jsonStr = jsonHelper.ReadFile(path);
                DataTable dt = jsonHelper.JsonToDataSet(jsonStr).Tables[0];
                this.dataGridView1.DataSource = dt;//数据源  
                this.dataGridView1.AutoGenerateColumns = false;//不自动  

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.Description = "请选择模版文件夹进行批量生产";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string folderFullName = dialog.SelectedPath;
                if (!string.IsNullOrEmpty(folderFullName))
                {
                    //遍历文件夹
                    string[] fileNames = Directory.GetFiles(folderFullName, "*.js");
                    foreach (string file in fileNames)
                    {
                        DataTable dt = jsonHelper.JsonToDataSet(file).Tables[0];
                        bool flag = CreateFunc(dt);
                        if (flag)
                            MessageBox.Show("生成成功！！！！");
                        else
                            MessageBox.Show("生成失败！！！！");

                    }


                }

            }
        }

        private void 单表生成ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)this.dataGridView1.DataSource;

            if (dt != null && dt.Rows.Count > 0)
            {
                dt.TableName = tabName;
                jsonHelper.WriteFile(CreateHelper.getPath(tabName), tabName + ".js", jsonHelper.ToJson(dt, true));
            }
            if (CreateFunc(dt))
                MessageBox.Show("生成成功！！！！");
            else
                MessageBox.Show("生成失败！！！！");
        }

        private void 模版生成ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //初始化一个OpenFileDialog类
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "(*.js)|*.js";
            //判断用户是否正确的选择了文件
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                string path = fileDialog.FileName;
                string jsonStr = jsonHelper.ReadFile(path);
                DataTable dt = jsonHelper.JsonToDataSet(jsonStr).Tables[0];
                this.dataGridView1.DataSource = dt;//数据源  
                this.dataGridView1.AutoGenerateColumns = false;//不自动  

            }
        }

        private void 批量生成ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<TreeNode> listNodes = new List<TreeNode>();
            foreach (TreeNode node in treeView1.Nodes)
            {
                if (node.Text == "表")
                {
                    foreach (TreeNode childnode in node.Nodes)
                    {
                        if (childnode.Checked)
                        {

                            tabName = childnode.Text;
                            DataTable dt = CreateHelper.GetSyscolumns(sql_connectstring.Replace("Provider=sqloledb;", ""), childnode.Text);
                            dt.TableName = tabName;
                            CreateFunc(dt);
                            MessageBox.Show("生成成功！！！！");

                        }
                    }
                }
                else 
                {
                    foreach (TreeNode childnode in node.Nodes)
                    {
                        if (childnode.Checked)
                        {

                            tabName = childnode.Text;
                            DataTable dt = CreateHelper.GetVSyscolumns(sql_connectstring.Replace("Provider=sqloledb;", ""), childnode.Text);
                            dt.TableName = tabName;
                            CreateFunc(dt);
                            MessageBox.Show("生成成功！！！！");

                        }
                    }
                }

                //  FindCheckNode(node, listNodes);
            }
            this.Close();
        }



    }
}
