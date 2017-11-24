using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace CodeCreater
{
    public class jsonHelper
    {
        /// <summary>
        /// 读取
        /// </summary>
        /// <param name="FileName">完整路径</param>
        /// <returns></returns>
        public static string ReadFile(string FileName)
        {
            using (StreamReader srFile = new StreamReader(FileName, System.Text.Encoding.UTF8))
            {
                string strResult = srFile.ReadToEnd();
                return strResult;
            }
        }

        /// <summary>
        /// 将JSON解析成DataSet只限标准的JSON数据
        /// 例如：Json＝{t1:[{name:'数据name',type:'数据type'}]} 
        /// 或 Json＝{t1:[{name:'数据name',type:'数据type'}],t2:[{id:'数据id',gx:'数据gx',val:'数据val'}]}
        /// </summary>
        /// <param name="Json">Json字符串</param>
        /// <returns>DataSet</returns>
        public static DataSet JsonToDataSet(string Json)
        {
            try
            {
                DataSet ds = new DataSet();
                JavaScriptSerializer JSS = new JavaScriptSerializer();


                object obj = JSS.DeserializeObject(Json);
                Dictionary<string, object> datajson = (Dictionary<string, object>)obj;


                foreach (var item in datajson)
                {
                    DataTable dt = new DataTable(item.Key);
                    object[] rows = (object[])item.Value;
                    foreach (var row in rows)
                    {
                        Dictionary<string, object> val = (Dictionary<string, object>)row;
                        DataRow dr = dt.NewRow();
                        foreach (KeyValuePair<string, object> sss in val)
                        {
                            if (!dt.Columns.Contains(sss.Key))
                            {
                                dt.Columns.Add(sss.Key.ToString());
                                dr[sss.Key] = sss.Value;
                            }
                            else
                                dr[sss.Key] = sss.Value;
                        }
                        dt.Rows.Add(dr);
                    }
                    ds.Tables.Add(dt);
                }
                return ds;
            }
            catch
            {
                return null;
            }
        }




        public static DataTable JsonToDataTable(string strJson)
        {
            //取出表名  
            //Regex rg = new Regex(@"(?<={)[^:]+(?=:/)", RegexOptions.IgnoreCase);
            //string strName = rg.Match(strJson).Value;
            DataTable tb = null;
            //去除表名  
            strJson = strJson.Substring(strJson.IndexOf("[") + 1);
            strJson = strJson.Substring(0, strJson.IndexOf("]"));

            //获取数据  
            Regex rg = new Regex(@"(?<={)[^}]+(?=})");
            MatchCollection mc = rg.Matches(strJson);
            for (int i = 0; i < mc.Count; i++)
            {
                string strRow = mc[i].Value;
                string[] strRows = strRow.Split('*');
                //创建表   
                if (tb == null)
                {
                    tb = new DataTable();
                   // tb.TableName = strName;
                    foreach (string str in strRows)
                    {
                        var dc = new DataColumn();
                        string[] strCell = str.Split('#');
                        if (strCell[0].Substring(0, 1) == "\"")
                        {
                            int a = strCell[0].Length;
                            dc.ColumnName = strCell[0].Substring(1, a - 2);
                        }
                        else
                        {
                            dc.ColumnName = strCell[0];
                        }
                        tb.Columns.Add(dc);
                    }
                    tb.AcceptChanges();
                }
                //增加内容   
                DataRow dr = tb.NewRow();
                for (int r = 0; r < strRows.Length; r++)
                {
                    dr[r] = strRows[r].Trim().Replace("，", ",").Replace("：", ":").Replace("\"", "");
                }
                tb.Rows.Add(dr);
                tb.AcceptChanges();
            }
            return tb;
        } 


        /// <summary>
        /// 写入
        /// </summary>
        /// <param name="FilePath">完整路劲</param>
        /// <param name="FileName">文件名包括后缀</param>
        /// <param name="content">内容</param>
        public static void WriteFile(string FilePath, string FileName, string content)
        {
            string strFilePath = FilePath + "//" + FileName;
            if (!Directory.Exists(FilePath))
            {
                Directory.CreateDirectory(FilePath);
            }

            StreamWriter write = new StreamWriter(strFilePath, false, System.Text.Encoding.UTF8);
            write.Write(content);
            write.Close();
        }


        /// <summary>
        /// 删除文件
        /// </summary>
        public static void deleteFile(string path)
        {
            try
            {
                FileInfo file = new FileInfo(path);
                file.Delete();
            }
            catch (Exception ex)
            {
                //写入日志
                string msg = "删除文件【BLL.commd.deleteEntInfo】:\n 地址：" + path + "\n 时间:" + DateTime.Now.ToString("yyyyMMdd-hhmmss");

            }
        }

        //去除html标签
        public static string ParseTags(string HTMLStr)
        {
            HTMLStr = HTMLStr.Replace("&nbsp;", "");
            HTMLStr = System.Text.RegularExpressions.Regex.Replace(HTMLStr, "<[^>]*>", "");

            HTMLStr = HTMLStr.Replace("\r\n", "");

            // HTMLStr = System.Text.RegularExpressions.Regex.Replace(HTMLStr, "\r\n", "");
            return HTMLStr;
        }




        #region dataTable转换成Json格式
        /// <summary>      
        /// dataTable转换成Json格式      
        /// </summary>      
        /// <param name="dt"></param>      
        /// <returns></returns>      
        public static string ToJson(ArrayList aliJson)
        {
            StringBuilder sbuBuilder = new StringBuilder();
            sbuBuilder.Append("{\"Value\":[");
            for (int i = 0; i < aliJson.Count; i++)
            {
                sbuBuilder.Append("\"");
                sbuBuilder.Append(aliJson[i].ToString().Replace("\"", "\\\""));
                sbuBuilder.Append("\",");
            }
            if (aliJson.Count > 0)
            {
                sbuBuilder.Remove(sbuBuilder.Length - 1, 1);
            }
            sbuBuilder.Append("]}");
            string strJson = sbuBuilder.ToString();
            strJson = strJson.Replace("\n", "<br />");
            strJson = strJson.Replace("\r", "<br />");
            return strJson;//sbuBuilder.ToString();
        }
        public static string ToJson(DataTable dtaJson)
        {
            StringBuilder sbuBuilder = new StringBuilder();
            sbuBuilder.Append("{\"Rows\":[");
            for (int i = 0; i < dtaJson.Rows.Count; i++)
            {
                sbuBuilder.Append("[");
                for (int j = 0; j < dtaJson.Columns.Count; j++)
                {
                    sbuBuilder.Append("\"");
                    sbuBuilder.Append(dtaJson.Rows[i][j].ToString().Replace("\"", "\\\"").Replace("\r\n", "<br>"));
                    sbuBuilder.Append("\",");
                }
                sbuBuilder.Remove(sbuBuilder.Length - 1, 1);
                sbuBuilder.Append("],");
            }
            if (dtaJson.Rows.Count > 0)
            {
                sbuBuilder.Remove(sbuBuilder.Length - 1, 1);
            }
            sbuBuilder.Append("]}");

            string strJson = sbuBuilder.ToString();
            strJson = strJson.Replace("\n", "<br />");
            strJson = strJson.Replace("\r", "<br />");

            return strJson;//sbuBuilder.ToString();
        }

        #endregion dataTable转换成Json格式

        #region DataSet转换成Json格式
        /// <summary>      
        /// DataSet转换成Json格式      
        /// </summary>      
        /// <param name="ds">DataSet</param>      
        /// <returns></returns>      
        public static string ToJson(DataSet dseJson)
        {
            StringBuilder sbuBuilder = new StringBuilder();
            sbuBuilder.Append("{\"Tables\":[");
            foreach (DataTable dtJson in dseJson.Tables)
            {
                sbuBuilder.Append(ToJson(dtJson) + ",");
            }
            sbuBuilder.Remove(sbuBuilder.Length - 1, 1);
            sbuBuilder.Append("]}");

            string strJson = sbuBuilder.ToString();
            strJson = strJson.Replace("\n", "<br />");
            strJson = strJson.Replace("\r", "<br />");
            return strJson;//sbuBuilder.ToString();
        }
        #endregion



        /// <summary>
        /// 带表头的
        /// </summary>
        /// <param name="dtaJson"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static string ToJson(DataTable dtaJson, bool b)
        {
            StringBuilder sbuBuilder = new StringBuilder();
            sbuBuilder.Append("{\"" + dtaJson.TableName + "\":[");
            for (int i = 0; i < dtaJson.Rows.Count; i++)
            {
                sbuBuilder.Append("{");
                for (int j = 0; j < dtaJson.Columns.Count; j++)
                {

                    sbuBuilder.Append("\"" + dtaJson.Columns[j].ColumnName + "\":");
                    sbuBuilder.Append("\"");
                    sbuBuilder.Append(dtaJson.Rows[i][j].ToString().Replace("\"", "\\\"").Replace("\r\n", "<br>"));
                    sbuBuilder.Append("\",");
                }
                sbuBuilder.Remove(sbuBuilder.Length - 1, 1);
                sbuBuilder.Append("},");
            }
            if (dtaJson.Rows.Count > 0)
            {
                sbuBuilder.Remove(sbuBuilder.Length - 1, 1);
            }
            sbuBuilder.Append("]}");

            string strJson = sbuBuilder.ToString();
            strJson = strJson.Replace("\n", "<br />");
            strJson = strJson.Replace("\r", "<br />");

            return strJson;//sbuBuilder.ToString();
        }

        /// <summary>
        /// 带表头的
        /// </summary>
        /// <param name="dseJson"></param>
        /// <returns></returns>
        public static string ToJson(DataSet dseJson, bool b)
        {
            StringBuilder sbuBuilder = new StringBuilder();
            sbuBuilder.Append("{\"Tables\":[");
            foreach (DataTable dtJson in dseJson.Tables)
            {
                sbuBuilder.Append(ToJson(dtJson, true) + ",");
            }
            sbuBuilder.Remove(sbuBuilder.Length - 1, 1);
            sbuBuilder.Append("]}");


            string strJson = sbuBuilder.ToString();
            strJson = strJson.Replace("\n", "<br />");
            strJson = strJson.Replace("\r", "<br />");
            return strJson;//sbuBuilder.ToString();
        }
    }
}
