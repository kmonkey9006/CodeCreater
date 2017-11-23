using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;

namespace CodeCreater
{
    public class CreateModels
    {
        DataTable dt = new DataTable();
        string nameSpace = string.Empty;
        public CreateModels(DataTable _dt, string _nameSpace)
        {
            dt = _dt;
            nameSpace = _nameSpace;
        }

        public bool setModel()
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                string tbname = dt.TableName;
                sb.AppendLine("using System;");
                sb.AppendLine("using System.Web.Mvc;");
                sb.AppendLine("using System.ComponentModel;");
                sb.AppendLine("using RTSafe.Platform.Core;");
                sb.AppendLine("using System.ComponentModel.DataAnnotations;");
                sb.AppendLine("using RTSafe.Platform.KMvcControl.Attributes;");
                sb.AppendLine();
                sb.AppendFormat("namespace {0}.Models", nameSpace);
                sb.AppendLine();
                sb.AppendLine("{");
                sb.AppendFormat("    public class {0}Model : MapperModel", tbname);
                sb.AppendLine();
                sb.AppendLine("    {");
                if (dt != null || dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        string dataName = dr["name"].ToString();
                        sb.AppendLine();
                        sb.AppendFormat("        [DisplayName(\"{0}\")]", dr["DisplayName"].ToString());
                        string l = dr["Length"].ToString();
                        if (dr["IsNull"].ToString().ToLower() == "true")
                        {
                            sb.AppendLine("        [Required(ErrorMessage = \"{0}不能为空\")] ");
                            sb.AppendFormat("        [StringLength({0},", l);
                            sb.Append("MinimumLength = 1, ErrorMessage =\"{0}长度在{2}-{1}之间\")]");
                        }
                        sb.AppendLine();
                        sb.AppendFormat("        [StringLength({0}, ErrorMessage = \"长度不可超出{0}\")]", l);
                        sb.AppendLine();
                        if (dr["HiddenInput"].ToString() == "false" || dt.Rows.IndexOf(dr) == 0)
                            sb.AppendLine("        [HiddenInput(DisplayValue=false)]");
                        if (!string.IsNullOrEmpty(dr["SelectList"].ToString()) && string.IsNullOrEmpty(dr["SelectData"].ToString()))
                        {
                            sb.AppendFormat("        [SelectList(\"{0}\",HttpVerbs.Post,DataValueField = \"{1}\",DataTextField = \"Name\",DataType = \"DropDownList\")]", dataName, dr["SelectList"].ToString());
                            sb.AppendLine();
                        }
                        if (!string.IsNullOrEmpty(dr["SelectData"].ToString()) && !string.IsNullOrEmpty(dr["SelectList"].ToString()))
                        {
                            string[] SelectData = dr["SelectData"].ToString().Split(':');
                            sb.AppendFormat("        [SelectList(\"{0}\",HttpVerbs.Post,DataValueField = \"{1}\",DataTextField = \"Name\",Data ={2}\"function(){{return {{ {3}: $(\"\"#{4}\"\").val() }}; }}\",DataType = \"DropDownList\")]", dr["SelectList"].ToString(),dataName,"@", SelectData[0], SelectData[1]);
                            sb.AppendLine();
                        }
                        if (dr["DataType"].ToString() == "datetime")
                            sb.AppendLine("[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = \"{{0:yyyy-MM-dd}}\")]");
                        string da = dr["DefaultAttribute"].ToString();
                        if (!string.IsNullOrEmpty(da))
                        {
                            string[] daArray = da.Split('|');
                            foreach (string das in daArray)
                            {
                                sb.AppendFormat("        [{0}]", das);
                                sb.AppendLine();
                            }
                        }
                        string regular = dr["Regular"].ToString();
                        if (!string.IsNullOrEmpty(regular))
                        {
                            sb.AppendFormat("        [RegularExpression({0}\"{1}\")]", "@", regular);
                            sb.AppendLine();
                        }
                        sb.AppendFormat("        public {0} {1} {{ get; set; }}", CreateHelper.GetCsType(dr["DataType"].ToString()), dataName);
                    }
                }
                sb.AppendLine("        }");
                sb.AppendLine("}");
                File.WriteAllText(CreateHelper.getPath(tbname) + "//Models" + "//" + tbname + "Model.cs", sb.ToString());
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }


    }
}
