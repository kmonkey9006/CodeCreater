using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace CodeCreater
{
    public class CreateHelper
    {
        public static string myOleDBConnstring = "";
        public static string mySqlType = "";
        public static string myLoginName = "";
        private static OleDbTransaction myTransation = null;
        private static OleDbConnection myOledbConn = new OleDbConnection();
        public static string db_GetConnstring()
        {
            return myOleDBConnstring;
        }
        public static IList<string> GetTable(SqlConnection conn, string xtype)
        {
            SqlCommand comd = new SqlCommand
             (@"SELECT name FROM sysobjects where xtype='" + xtype + "' order by name ", conn);
            List<string> tableName = new List<string>();
            SqlDataReader dtReader = comd.ExecuteReader();
            while (dtReader.Read())
            {
                string name = dtReader[0].ToString();
                tableName.Add(name);
            }
            dtReader.Close();
            return tableName;

        }
        public static void db_SetConnstring(string sqltype, string dbconnstring, string as_loginname)
        {
            myOleDBConnstring = dbconnstring;
            mySqlType = sqltype;
            myLoginName = as_loginname;
        }

        public static void of_rollback()
        {
            myTransation.Rollback();
        }
        public static Boolean db_Connect()
        {
            if (db_isconnect() == true)
            {
                return true;
            }
            myOledbConn.ConnectionString = myOleDBConnstring;
            try
            {
                myOledbConn.Open();

            }
            catch
            {
                return false;
            }
            //mytransation = myoledbconn.BeginTransaction(IsolationLevel.ReadCommitted);
            return true;
        }
        public static Boolean db_DisConnect()
        {
            if (db_isconnect())
            {
                myOledbConn.Close();
            }
            return true;
        }
        public static Boolean db_isconnect()
        {
            if (myOledbConn.State.ToString() == "Closed")
            {
                return false;
            }
            return true;
        }




        /// <summary>  
        /// 获取某一个表的所有字段  
        /// </summary>  
        /// <param name="object_id">表名</param>  
        /// <returns></returns>  
        public static DataTable GetVSyscolumns(string conString, string object_id)
        {
            DataTable dt = new DataTable();
            StringBuilder strSql = new StringBuilder();
            if (!string.IsNullOrEmpty(object_id) && object_id != "未选择")
            {
                strSql.Append(@"SELECT  
                             DISTINCT   a.id,  a.name, a.colorder ,
                                    [DataType]=b.name,  
                                    [Length]=COLUMNPROPERTY(a.id,a.name,'PRECISION'),  
                                    [IsNull]=case when a.isnullable=1 then 'true' else 'false' end,  
                                    [DisplayName]=isnull(g.[value],''),
                                    [SelectList]='',
                                    [SelectData]='',
                                    [DefaultVal]='' ,
                                    [Regular]='' ,
                                    [IsQuery]='false',
                                    [HiddenInput]='false',
                                    [DefaultAttribute]=''
                                    FROM syscolumns a  
                                    left join systypes b on a.xusertype=b.xusertype  
                                    inner join sysobjects d on a.id=d.id  and d.xtype='V' and  d.name<>'dtproperties'  
                                    left join syscomments e on a.cdefault=e.id  
                                    left join sys.extended_properties g on a.id=g.major_id and a.colid=g.minor_id   
                                    left join sys.extended_properties f on d.id=f.major_id and f.minor_id=0");
                strSql.Append("where d.name='" + object_id + "' order by a.id,a.colorder");





                SqlConnection cn = new SqlConnection(conString);
                if (cn.State != ConnectionState.Open)
                    cn.Open();
                SqlCommand cmd = new SqlCommand(strSql.ToString(), cn);
                SqlDataAdapter sdap = new SqlDataAdapter();
                sdap.SelectCommand = cmd;
                sdap.Fill(dt);
                cn.Close();
            }
            return dt;
        }

        /// <summary>  
        /// 获取某一个表的所有字段  
        /// </summary>  
        /// <param name="object_id">表名</param>  
        /// <returns></returns>  
        public static DataTable GetSyscolumns(string conString, string object_id)
        {
            DataTable dt = new DataTable();
            StringBuilder strSql = new StringBuilder();
            if (!string.IsNullOrEmpty(object_id) && object_id != "未选择")
            {
                strSql.Append(@"SELECT  
                                   a.name,  
                                    [DataType]=b.name,  
                                    [Length]=COLUMNPROPERTY(a.id,a.name,'PRECISION'),  
                                    [IsNull]=case when a.isnullable=1 then 'true' else 'false' end,  
                                    [DisplayName]=isnull(g.[value],''),
                                    [SelectList]='',
                                    [SelectData]='',
                                    [DefaultVal]='' ,
                                    [Regular]='' ,
                                    [IsQuery]='false',
                                    [HiddenInput]='true',
                                    [DefaultAttribute]=''
                                    FROM syscolumns a  
                                    left join systypes b on a.xusertype=b.xusertype  
                                    inner join sysobjects d on a.id=d.id  and d.xtype='U' and  d.name<>'dtproperties'  
                                    left join syscomments e on a.cdefault=e.id  
                                    left join sys.extended_properties g on a.id=g.major_id and a.colid=g.minor_id   
                                    left join sys.extended_properties f on d.id=f.major_id and f.minor_id=0");
                strSql.Append("where d.name='" + object_id + "' order by a.id,a.colorder");





                SqlConnection cn = new SqlConnection(conString);
                if (cn.State != ConnectionState.Open)
                    cn.Open();
                SqlCommand cmd = new SqlCommand(strSql.ToString(), cn);
                SqlDataAdapter sdap = new SqlDataAdapter();
                sdap.SelectCommand = cmd;
                sdap.Fill(dt);
                cn.Close();
            }
            return dt;
        }
        public static string getPath(string Name)
        {
            string location = System.Windows.Forms.Application.StartupPath;
            location = location.Substring(0, location.LastIndexOf('\\') + 1);
            string path = location + Name;
            if (!Directory.Exists(path + "//Models"))
                Directory.CreateDirectory(path + "//Models");
            if (!Directory.Exists(path + "//Domain"))
                Directory.CreateDirectory(path + "//Domain");
            if (!Directory.Exists(path + "//Controllers"))
                Directory.CreateDirectory(path + "//Controllers");
            if (!Directory.Exists(path + "//Views"))
                Directory.CreateDirectory(path + "//Views");
            return path;
        }
        private static DataSet XMLToDataSet(string xmlData)
        {
            StringReader stream = null;
            XmlTextReader reader = null;
            try
            {
                DataSet xmlDS = new DataSet();
                stream = new StringReader(xmlData);
                reader = new XmlTextReader(stream);
                xmlDS.ReadXml(reader);
                return xmlDS;
            }
            catch (Exception ex)
            {
                string strTest = ex.Message;
                return null;
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }
        /// <summary>
        /// 数据库类型 c#类型 转换
        /// </summary>
        private static IDictionary<string, string> dataTypeDic = new Dictionary<string, string>{
        {"nvarchar","string"}
        ,{"varchar","string"}
        ,{"nchar","string"}
        ,{"char","string"}
        ,{"xml","string"}
        ,{"text","string"}
        ,{"int","int"}
        ,{"bigint","int"}
        ,{"bit","bool"}
        ,{"float","float"}
        ,{"uniqueidentifier","Guid"}
        ,{"datetime","Nullable<System.DateTime>"}
        ,{"decimal","Decimal"}
        };
        /// <summary>
        /// 数据库类型 转换为 c# 类型
        /// </summary>
        /// <param name="dataType"></param>
        /// <returns></returns>
        public static string GetCsType(string dataType)
        {
            dataType = dataType.ToLower();
            if (dataTypeDic[dataType] == null)
            {
                return "string";
            }
            else
            {
                return dataTypeDic[dataType];
            }
        }
    }

}
