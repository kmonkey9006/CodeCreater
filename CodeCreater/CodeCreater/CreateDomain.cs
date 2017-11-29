using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;

namespace CodeCreater
{
    /// <summary>
    /// 常见业务层代码
    /// by 王延领
    /// date:2017-11-20
    /// </summary>
    public class CreateDomain
    {
        DataTable dt = new DataTable();
        string dtName = string.Empty;
        string strNamespace = "RTSafe.HiddenTroubleTreatm.BusinessModules.HiddenTroubleTreatmModules";
        string modeName = string.Empty;
        StringBuilder checkString = new StringBuilder();
        StringBuilder query = new StringBuilder();
        string dataContext = "HiddenTroubleTreatmDataContext";
        public CreateDomain()
        {
        }
        /// <summary>
        /// by 王延领
        /// date:2017-11-20
        /// </summary>
        /// <param name="_dt"></param>
        /// <param name="_strNamespace">命名空间</param>
        /// <param name="_dataContext">数据库操作上线文</param>
        public CreateDomain(DataTable _dt, string _strNamespace, string _dataContext)
        {
            strNamespace = _strNamespace;
            dataContext = _dataContext;
            dt = _dt;
            dtName = dt.TableName;
            modeName = dtName + "Model";
            #region 生成的查询条件
            foreach (DataRow dr in dt.Rows)
            {

                string dataType = dr["DataType"].ToString();
                if (dr["IsQuery"].ToString().ToLower() == "true" && dataType != "datetime")
                {
                    string columuName = dr["name"].ToString();
                    string _columuName = columuName.Substring(0, 1).ToLower() + columuName.Substring(1);
                    checkString.AppendFormat("{0} {1},", CreateHelper.GetCsType(dataType), _columuName);
                    query.AppendFormat("            if ({0}.HasValue)", _columuName);
                    query.AppendLine();
                    query.AppendFormat("                sql += string.Format(\" and {0} = '{1}'\", {2}.Value);", columuName, "{0}", _columuName);
                    query.AppendLine();

                }
                else if (dr["IsQuery"].ToString().ToLower() == "true" && dataType == "datetime")
                {
                    string columuName = dr["name"].ToString();
                    checkString.Append(" DateTime? sd, DateTime? ed,");
                    query.AppendLine("            if (!Convert.ToDateTime(sd).IsEmpty())");
                    query.AppendFormat("                sql += string.Format(\" And {0}>'{1}'\", Convert.ToDateTime(sd).ToString(\"yyyy-MM-dd 0:00\"));", columuName, "{0}");
                    query.AppendLine();
                    query.AppendLine("            if (!Convert.ToDateTime(ed).IsEmpty())");
                    query.AppendFormat("                sql += string.Format(\" And {0}<'{1}'\", Convert.ToDateTime(ed).ToString(\"yyyy-MM-dd 23:59:59\"));", columuName, "{0}");
                    query.AppendLine();

                }
            #endregion
            }
        }
        public bool setDomainService()
        {
            #region 业务服务处理层模版
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("using System;");
            sb.AppendLine("using System.Linq;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using SafetySupervision.SuperviseDataAccess;");
            sb.AppendLine("using SafetySupervision.SuperviseDataAccess.Entities;");
            sb.AppendLine();
            sb.AppendFormat("using {0}.Models;", strNamespace);
            sb.AppendLine();
            sb.AppendFormat("namespace {0}.Domain", strNamespace);
            sb.AppendLine();
            sb.AppendLine("{");
            sb.AppendLine();
            sb.AppendLine(GetDomainInterface().ToString());
            sb.AppendFormat("    public class {0}Service : I{0}Service", dtName);
            sb.AppendLine();
            sb.AppendLine("    {");
            sb.AppendFormat("        {0} dbContext = new {0}();", dataContext);
            sb.AppendLine();
            sb.AppendFormat("        public {0}Service()", dtName);
            sb.AppendLine();
            sb.AppendLine("        {");
            sb.AppendLine(" ");
            sb.AppendLine("        }");
            sb.AppendLine();

            sb.AppendFormat("        public {0} Get(Guid id)", modeName);
            sb.AppendLine("        {");
            sb.AppendLine();
            sb.AppendFormat("            var entity = dbContext.{0}.First(p => p.{1} == id);", dtName, dt.Rows[0]["name"].ToString());
            sb.AppendLine();
            sb.AppendFormat("            return entity.ToViewModel<{0}>();", modeName);
            sb.AppendLine();
            sb.AppendLine("        }");
            sb.AppendFormat("        public int Add({0} model)", modeName);
            sb.AppendLine("        {");
            sb.AppendFormat("            {0} ent = model.ToORMEntity<{0}>();", dtName);
            sb.AppendLine();
            sb.AppendFormat("            int re = dbContext.{0}.Insert(ent);", dtName);
            sb.AppendLine();
            sb.AppendLine("            return re;");
            sb.AppendLine("        }");
            sb.AppendFormat("        public int Edit({0} model)", modeName);
            sb.AppendLine();
            sb.AppendLine("        {");
            sb.AppendFormat("            var ent = model.ToORMEntity<{0}>();", dtName);
            sb.AppendLine();
            sb.AppendFormat("            int re = dbContext.{0}.Update(ent);", dtName);
            sb.AppendLine();
            sb.AppendLine("            return re;");
            sb.AppendLine("        }");
            sb.AppendLine("        public int Delete(Guid id)");
            sb.AppendLine("        {");
            sb.AppendFormat("            return dbContext.{0}.Delete(id);", dtName);
            sb.AppendLine();
            sb.AppendLine("        }");
            sb.AppendLine();
            sb.AppendFormat("        public List<{0}> GetAllPage(int index, int size,{1} out int total)", modeName, checkString.ToString());
            sb.AppendLine();
            sb.AppendLine("        {");
            sb.AppendFormat("            string sql = {0}\"select * from {1}  where 1=1 \";", "@", dtName);
            sb.AppendLine();
            sb.AppendLine(query.ToString());
            sb.AppendLine("            try");
            sb.AppendLine("            {");
            sb.AppendLine("                var query = dbContext.CustomSql(sql).SetSelectRange(size * (index - 1), size);");
            sb.AppendLine("                total = query.GetTotalForPaging();");
            sb.AppendFormat("                return query.OrderBy(\" CreateTime Desc\").ToList<{0}>();", modeName);
            sb.AppendLine();
            sb.AppendLine("            }");
            sb.AppendLine("            catch (Exception ex)");
            sb.AppendLine("            {");
            sb.AppendLine("                total = 0;");
            sb.AppendLine("                return null;");
            sb.AppendLine("            }");
            sb.AppendLine("        }");
            sb.AppendLine("    }");
            sb.AppendLine("}");
            #endregion
            File.WriteAllText(CreateHelper.getPath(dtName) + "//Domain" + "//" + dtName + "Service.cs", sb.ToString());
            return false;
        }
        public StringBuilder GetDomainInterface()
        {
            StringBuilder sb = new StringBuilder();
            //sb.AppendLine("using System;");
            //sb.AppendLine("using System.Collections.Generic;");
            //sb.AppendFormat("using {0}.Models;", strNamespace);
            //sb.AppendLine();
            //sb.AppendFormat("namespace {0}.Domain", strNamespace);
            //sb.AppendLine();
            //sb.AppendLine("{");
            #region 接口层模版
            sb.AppendFormat("    public interface I{0}Service", dtName);
            sb.AppendLine();
            sb.AppendLine("    {");
            sb.AppendFormat("        {0} Get(Guid id);", modeName);
            sb.AppendLine();
            sb.AppendFormat("        int Add({0} model);", modeName);
            sb.AppendLine();
            sb.AppendFormat("        int Edit({0} model);", modeName);
            sb.AppendLine();
            sb.AppendFormat("        int Delete(Guid id);");
            sb.AppendLine();
            //sb.AppendFormat("        List<{0}> GetListPage(int index, int size, string filter, int state, out int total);", modeName);
            //sb.AppendLine();
            sb.AppendFormat("        List<{0}> GetAllPage(int index, int size, {1} out int total);", modeName, checkString.ToString());
            sb.AppendLine();
            sb.AppendLine("    }");
            #endregion
            //sb.AppendLine("}");
            //File.WriteAllText(CreateHelper.getPath(dtName) + "//Domain" + "//I" + dtName + "Service.cs", sb.ToString());
            //return false;
            return sb;

        }


    }
}
