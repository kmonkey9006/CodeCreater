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
    public class CreateControllers
    {
        public string strNamespace = " RTSafe.HiddenTroubleTreatm.BusinessModules.HiddenTroubleTreatmXMLModules";
        public string projectName = "项目名称";
        public string functionNme = "功能名称";
        string dtName = string.Empty;
        string _dtName = string.Empty;
        string modelName = string.Empty;
        DataTable dt = new DataTable();
        StringBuilder query = new StringBuilder();
        StringBuilder defaultValue = new StringBuilder();
        StringBuilder checkString = new StringBuilder();
        public CreateControllers(string _functionNme, string _dtName)
        {
            functionNme = _functionNme;
            dtName = _dtName;
        }
        /// <summary>
        /// 
        /// by 王延领
        /// date:2017-11-20
        /// </summary>
        /// <param name="_dt"></param>
        /// <param name="_strNamespace">命名空间</param>
        /// <param name="_projectName">项目模块名称</param>
        /// <param name="_functionNme">功能名称</param>
        public CreateControllers(DataTable _dt, string _strNamespace, string _projectName, string _functionNme)
        {
            strNamespace = _strNamespace;
            projectName = _projectName;
            functionNme = _functionNme;
            dt = _dt;
            //表名
            dtName = dt.TableName;
            //表名（首字母小写）
            _dtName = dtName.Substring(0, 1).ToLower() + dtName.Substring(1);
            modelName = dtName + "Model";

            #region 遍历datatable  可读性有点低哈，为了实现设计代码的时候就没做太多设计
            foreach (DataRow dr in dt.Rows)
            {
                string defaultVal = dr["DefaultVal"].ToString();
                string dataType = dr["DataType"].ToString();
                #region 查询条件
                if (dr["IsQuery"].ToString().ToLower() == "true" && dataType != "datetime")
                {
                    string columuName = dr["name"].ToString();
                    string _columuName = columuName.Substring(0, 1).ToLower() + columuName.Substring(1);
                    query.AppendFormat("{0},", _columuName);
                    checkString.AppendFormat("{0} {1},", CreateHelper.GetCsType(dataType), _columuName);

                }
                else if (dr["IsQuery"].ToString().ToLower() == "true" && dataType == "datetime")
                {
                    query.Append("sd,ed,");
                    checkString.Append(" DateTime? sd, DateTime? ed,");
                }
                #endregion
                #region 初始化
                if (!string.IsNullOrEmpty(defaultVal))
                {
                    string name = dr["name"].ToString();
                    if (defaultVal == "当前时间")
                        defaultValue.AppendFormat("            model.{0} = DateTime.Now;", name);
                    else if (defaultVal == "登入人")
                    {
                        defaultValue.AppendFormat("            model.{0} = Platform.Core.PlatformContext.CurrentUser.UserName;", name);
                    }
                    defaultValue.AppendLine();
                }
                #endregion
            }
            #endregion
        }
        public bool setController()
        {

            StringBuilder sb = new StringBuilder();
            #region controller生成模版（没有提供的T4模版，暂时这样处理吧）
            if (dt != null && dt.Rows.Count > 0)
            {
                sb.AppendLine("using System;");
                sb.AppendLine("using Kendo.Mvc.UI;");
                sb.AppendLine("using System.Web.Mvc;");
                sb.AppendLine("using System.Collections.Generic;");
                sb.AppendLine("using RTSafe.Platform.Module.Mvc;");
                sb.AppendLine("using RTSafe.Platform.Module.Attributes;");
                sb.AppendLine();
                sb.AppendFormat("using {0}.Models;", strNamespace);
                sb.AppendLine();
                sb.AppendFormat("using {0}.Domain;", strNamespace);
                sb.AppendLine();
                sb.AppendLine();
                sb.AppendFormat("namespace {0}.Controllers", strNamespace);
                sb.AppendLine();
                sb.AppendLine("{");
                sb.AppendLine();
                sb.AppendFormat("    [ControllerDescription(\"{0}\", \"{1}\")]", projectName, functionNme);
                sb.AppendLine();
                sb.AppendLine("    [Authorize]");
                sb.AppendLine();
                sb.AppendFormat("    public class {0}Controller : ModuleController", dtName);
                sb.AppendLine();
                sb.AppendLine("    {");
                sb.AppendFormat("        I{0}Service {1};", dtName, _dtName);
                sb.AppendLine();
                sb.AppendFormat("        public {0}Controller(I{0}Service _{1}Service)", dtName, _dtName);
                sb.AppendLine();
                sb.AppendLine("        {");
                sb.AppendFormat("            this.{0} = _{0}Service;", _dtName);
                sb.AppendLine();
                sb.AppendLine("        }");
                sb.AppendFormat("        [ActionDescription(\"{0}管理\",\" /{1}/{1}List)\")]", functionNme, dtName);
                sb.AppendLine();
                sb.AppendFormat("        [RoleName(\"管理员\",\"{0}：查询\")]", functionNme);
                sb.AppendLine();
                sb.AppendFormat("        public ActionResult {0}List()", dtName);
                sb.AppendLine();
                sb.AppendLine("        {");
                sb.AppendLine("            return View();");
                sb.AppendLine("        }");
                sb.AppendLine();
                sb.AppendFormat("        public ActionResult {0}Add(Guid? id)", dtName);
                sb.AppendLine();
                sb.AppendLine("        {");
                sb.AppendFormat("            {0} model = new {0}();", modelName);
                sb.AppendLine();
                sb.AppendLine(defaultValue.ToString());
                sb.AppendLine("            if (id.HasValue)");
                sb.AppendLine("                model = buildUnit.Get(id.Value);");
                sb.AppendLine("            return PartialView(model);");
                sb.AppendLine("        }");
                sb.AppendLine("        [HttpPost]");
                sb.AppendLine();
                sb.AppendFormat("        [RoleName(\"管理员\", \"{0}：添加\")]", functionNme);
                sb.AppendLine();
                sb.AppendFormat("        public ActionResult {0}Add({0}Model model)", dtName);
                sb.AppendLine();
                sb.AppendLine("        {");
                sb.AppendLine("            int re = 0;");
                sb.AppendLine("            if (ModelState.IsValid)");
                sb.AppendLine("            {");
                sb.AppendLine("                if (model != null)");
                sb.AppendLine("                {");
                string pkid = dt.Rows[0]["name"].ToString();
                sb.AppendLine();
                sb.AppendFormat("                    if (model.{0} != Guid.Empty)", pkid);
                sb.AppendLine();
                sb.AppendFormat("                        re = {0}.Edit(model);", _dtName);
                sb.AppendLine();
                sb.AppendLine("                    else");
                sb.AppendLine("                    {");
                sb.AppendFormat("                        model.{0} = Guid.NewGuid();", pkid);
                sb.AppendLine();
                sb.AppendFormat("                        re = {0}.Add(model);", _dtName);
                sb.AppendLine();
                sb.AppendLine("                    }");
                sb.AppendLine("                    if (re > 0)");
                sb.AppendLine("                        return AjaxResult(success: re > 0, data: model, ajaxDataTypes: AjaxDataTypes.Json);");
                sb.AppendLine("                }");
                sb.AppendLine("                return AjaxResult(success: false, data: null, ajaxDataTypes: RTSafe.Platform.Module.Mvc.AjaxDataTypes.Json);");
                sb.AppendLine("            }");
                sb.AppendLine("            else");
                sb.AppendLine("            {");
                sb.AppendLine("                ModelState.AddModelError(\"erro\", \"验证不通过，请检查输入\");");
                sb.AppendLine("            }");
                sb.AppendLine("            return PartialView(model);");
                sb.AppendLine("        }");
                sb.AppendLine();
                // sb.AppendFormat("        [ActionDescription(\"{0}\", \"/{1}/{1}Query\")]");
                //sb.AppendLine();
                sb.AppendFormat("        [RoleName(\"管理员\", \"{0}：查询\")]", functionNme);
                sb.AppendLine();
                sb.AppendFormat("        public ActionResult {0}Query(Guid? id)", dtName);
                sb.AppendLine();
                sb.AppendLine("        {");
                sb.AppendFormat("            {0} model = new {0}();", modelName);
                sb.AppendLine();
                sb.AppendLine("            if (id.HasValue)");
                sb.AppendLine();
                sb.AppendFormat("                model = {0}.Get(id.Value);", _dtName);
                sb.AppendLine();
                sb.AppendLine("            return View(model);");
                sb.AppendLine("        }");
                sb.AppendLine("        [HttpPost]");
                sb.AppendFormat("        [RoleName(\"管理员\", \"{0}：删除\")]", functionNme);
                sb.AppendLine();
                sb.AppendFormat("        public ActionResult {0}Delete(Guid id)", dtName);
                sb.AppendLine();
                sb.AppendLine("        {");
                sb.AppendLine("            if (id == Guid.Empty)");
                sb.AppendLine("                return Json(new { success = false });");
                sb.AppendFormat("            int re = {0}.Delete(id);", _dtName);
                sb.AppendLine();
                sb.AppendLine("            return Json(new { success = re > 0 });");
                sb.AppendLine("        }");
                sb.AppendFormat("        public ActionResult ListPage([DataSourceRequest]  DataSourceRequest dsRequest,{0})", checkString.ToString().TrimEnd(','));
                sb.AppendLine();
                sb.AppendLine("        {");
                sb.AppendLine("            var size = dsRequest.PageSize;");
                sb.AppendLine("            var index = dsRequest.Page;");
                sb.AppendLine("            var total = 0;");
                sb.AppendFormat("            var modelWrapper = {1}.GetAllPage(index, size, {0} out total);", query.ToString(), _dtName);
                sb.AppendLine();
                sb.AppendLine("            var rs = new DataSourceResult();");
                sb.AppendLine("            rs.Data = modelWrapper;");
                sb.AppendLine("            rs.Total = total;");
                sb.AppendLine("            return Json(rs);");
                sb.AppendLine("        }");
                sb.AppendLine("    }");
                sb.AppendLine("}");
            }
            #endregion
            File.WriteAllText(CreateHelper.getPath(dtName) + "//Controllers" + "//" + dtName + "Controller.cs", sb.ToString());

            return false;
        }
        public bool Setmapping()
        {
            #region 映射 ，依赖注入，权限自行复制粘贴不是使用类
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("        protected override void RegisterBuilder(ContainerBuilderWrapper builder)");
            sb.AppendLine("        {");
            sb.AppendLine("            base.RegisterBuilder(builder);");
            sb.AppendFormat("            builder.RegisterType<{0}Service>().As<I{0}Service>();", dtName);
            sb.AppendLine();
            sb.AppendLine("        }");
            sb.AppendLine("        public override void Initialize()");
            sb.AppendLine("        {");
            sb.AppendLine("            base.Initialize();");
            sb.AppendFormat("            AutoMapper.Mapper.CreateMap<{0}Model, {0}>();", dtName);
            sb.AppendLine();
            sb.AppendFormat("            AutoMapper.Mapper.CreateMap<{0}, {0}Model>();", dtName);
            sb.AppendLine();
            sb.AppendLine("        }");
            sb.AppendFormat(" [RoleName( \"{0}：管理\",\"{0}：添加\", \"{0}：编辑\",\"{0}：查询\", \"{0}：删除\")]", functionNme);
            #endregion
            File.WriteAllText(CreateHelper.getPath(dtName) + "//" + dtName + "Context.cs", sb.ToString());
            return false;
        }
    }
}
