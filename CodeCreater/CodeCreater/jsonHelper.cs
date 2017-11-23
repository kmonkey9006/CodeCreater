using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            sbuBuilder.Append("{\""+dtaJson.TableName+"\":[");
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
