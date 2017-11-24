namespace CodeCreater
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.length = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsNull = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.DisplayName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DefaultAttribute = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SelectList = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SelectData = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DefaultVal = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Regular = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsQuery = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.HiddenInput = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_dataContext = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_Function = new System.Windows.Forms.TextBox();
            this.txt_prijoct = new System.Windows.Forms.TextBox();
            this.txt_Namespace = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.treeView1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 782);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(3, 17);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(194, 762);
            this.treeView1.TabIndex = 0;
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(200, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1244, 782);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "数据展示区域";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.dataGridView1);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(3, 17);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(1238, 704);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.name,
            this.DataType,
            this.length,
            this.IsNull,
            this.DisplayName,
            this.DefaultAttribute,
            this.SelectList,
            this.SelectData,
            this.DefaultVal,
            this.Regular,
            this.IsQuery,
            this.HiddenInput});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 17);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(1232, 684);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // name
            // 
            this.name.DataPropertyName = "name";
            this.name.HeaderText = "名称";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            // 
            // DataType
            // 
            this.DataType.DataPropertyName = "DataType";
            this.DataType.HeaderText = "类型";
            this.DataType.Name = "DataType";
            this.DataType.ReadOnly = true;
            // 
            // length
            // 
            this.length.DataPropertyName = "Length";
            this.length.HeaderText = "长度";
            this.length.Name = "length";
            this.length.ReadOnly = true;
            // 
            // IsNull
            // 
            this.IsNull.DataPropertyName = "IsNull";
            this.IsNull.HeaderText = "是否非空";
            this.IsNull.Name = "IsNull";
            this.IsNull.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.IsNull.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // DisplayName
            // 
            this.DisplayName.DataPropertyName = "DisplayName";
            this.DisplayName.HeaderText = "显示名称";
            this.DisplayName.Name = "DisplayName";
            // 
            // DefaultAttribute
            // 
            this.DefaultAttribute.DataPropertyName = "DefaultAttribute";
            this.DefaultAttribute.HeaderText = "默认特性";
            this.DefaultAttribute.Name = "DefaultAttribute";
            this.DefaultAttribute.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // SelectList
            // 
            this.SelectList.DataPropertyName = "SelectList";
            this.SelectList.HeaderText = "自定义关联地址";
            this.SelectList.Name = "SelectList";
            // 
            // SelectData
            // 
            this.SelectData.DataPropertyName = "SelectData";
            this.SelectData.HeaderText = "关联参数";
            this.SelectData.Name = "SelectData";
            // 
            // DefaultVal
            // 
            this.DefaultVal.DataPropertyName = "DefaultVal";
            this.DefaultVal.HeaderText = "默认值";
            this.DefaultVal.Items.AddRange(new object[] {
            "当前时间",
            "登录人"});
            this.DefaultVal.Name = "DefaultVal";
            this.DefaultVal.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.DefaultVal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Regular
            // 
            this.Regular.DataPropertyName = "Regular";
            this.Regular.HeaderText = "正则表达式";
            this.Regular.Name = "Regular";
            // 
            // IsQuery
            // 
            this.IsQuery.DataPropertyName = "IsQuery";
            this.IsQuery.HeaderText = "是否为查询条件";
            this.IsQuery.Name = "IsQuery";
            this.IsQuery.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.IsQuery.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // HiddenInput
            // 
            this.HiddenInput.DataPropertyName = "HiddenInput";
            this.HiddenInput.HeaderText = "是否显示";
            this.HiddenInput.Name = "HiddenInput";
            this.HiddenInput.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.HiddenInput.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button3);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.txt_dataContext);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.txt_Function);
            this.groupBox3.Controls.Add(this.txt_prijoct);
            this.groupBox3.Controls.Add(this.txt_Namespace);
            this.groupBox3.Controls.Add(this.button2);
            this.groupBox3.Controls.Add(this.button1);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox3.Location = new System.Drawing.Point(3, 721);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1238, 58);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(337, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "上下文";
            // 
            // txt_dataContext
            // 
            this.txt_dataContext.Location = new System.Drawing.Point(384, 19);
            this.txt_dataContext.Name = "txt_dataContext";
            this.txt_dataContext.Size = new System.Drawing.Size(160, 21);
            this.txt_dataContext.TabIndex = 8;
            this.txt_dataContext.Text = "HiddenTroubleTreatmDataContext";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "命名空间";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(809, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "功能名称";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(550, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "模块名称";
            // 
            // txt_Function
            // 
            this.txt_Function.Location = new System.Drawing.Point(868, 18);
            this.txt_Function.Name = "txt_Function";
            this.txt_Function.Size = new System.Drawing.Size(156, 21);
            this.txt_Function.TabIndex = 4;
            this.txt_Function.Text = "台帐";
            // 
            // txt_prijoct
            // 
            this.txt_prijoct.Location = new System.Drawing.Point(609, 16);
            this.txt_prijoct.Name = "txt_prijoct";
            this.txt_prijoct.Size = new System.Drawing.Size(184, 21);
            this.txt_prijoct.TabIndex = 3;
            this.txt_prijoct.Text = "隐患排查与治理";
            // 
            // txt_Namespace
            // 
            this.txt_Namespace.Location = new System.Drawing.Point(65, 19);
            this.txt_Namespace.Name = "txt_Namespace";
            this.txt_Namespace.Size = new System.Drawing.Size(254, 21);
            this.txt_Namespace.TabIndex = 2;
            this.txt_Namespace.Text = "RTSafe.HiddenTroubleTreatm.BusinessModules.HiddenTroubleTreatmModules";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1094, 17);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(66, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "模版";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1163, 16);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "生成";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(1026, 18);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(68, 23);
            this.button3.TabIndex = 10;
            this.button3.Text = "批量";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1444, 782);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_Function;
        private System.Windows.Forms.TextBox txt_prijoct;
        private System.Windows.Forms.TextBox txt_Namespace;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_dataContext;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataType;
        private System.Windows.Forms.DataGridViewTextBoxColumn length;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsNull;
        private System.Windows.Forms.DataGridViewTextBoxColumn DisplayName;
        private System.Windows.Forms.DataGridViewTextBoxColumn DefaultAttribute;
        private System.Windows.Forms.DataGridViewTextBoxColumn SelectList;
        private System.Windows.Forms.DataGridViewTextBoxColumn SelectData;
        private System.Windows.Forms.DataGridViewComboBoxColumn DefaultVal;
        private System.Windows.Forms.DataGridViewTextBoxColumn Regular;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsQuery;
        private System.Windows.Forms.DataGridViewCheckBoxColumn HiddenInput;
        private System.Windows.Forms.Button button3;
    }
}

