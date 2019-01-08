using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeCreater
{
    public class CreateViews
    {
        string strNamespace = " RTSafe.HiddenTroubleTreatm.BusinessModules.HiddenTroubleTreatmXMLModules";
        string projectName = "项目名称";
        string functionNme = "功能名称";
        string dtName = string.Empty;
        string _dtName = string.Empty;
        string modelName = string.Empty;
        DataTable dt = new DataTable();

        StringBuilder query = new StringBuilder();
        StringBuilder defaultValue = new StringBuilder();
        StringBuilder checkString = new StringBuilder();
        StringBuilder listGrid = new StringBuilder();
        StringBuilder researchData = new StringBuilder();
        StringBuilder searchData = new StringBuilder();
        StringBuilder searchDataStr = new StringBuilder();
        public CreateViews()
        { }

        public CreateViews(DataTable _dt, string _strNamespace, string _projectName, string _functionNme)
        {
            strNamespace = _strNamespace;
            projectName = _projectName;
            functionNme = _functionNme;
            dt = _dt;
            dtName = dt.TableName;
            _dtName = dtName.Substring(0, 1).ToLower() + dtName.Substring(1);
            modelName = dtName + "Model";
            foreach (DataRow dr in dt.Rows)
            {
                string defaultVal = dr["DefaultVal"].ToString();
                string dataType = dr["DataType"].ToString();
                string displayName = dr["DisplayName"].ToString();
                string selectUrl = dr["SelectList"].ToString();
                string selectData = dr["SelectData"].ToString();
                string columuName = dr["name"].ToString();
                string _columuName = columuName.Substring(0, 1).ToLower() + columuName.Substring(1);
                #region grid信息
                if (dataType == "datetime")
                    listGrid.AppendFormat("                 cols.Bound(col => col.{0}).Title(\"{1}\").Format(\"{{0:yyyy-MM-dd}}\").Sortable(false).TextAlignCenter();", columuName, displayName);
                else if (defaultVal.Contains("Upload"))
                {
                    listGrid.AppendFormat("                 cols.Bound(col => col.{0}).Title(\"{1}\").Sortable(false).TextAlignCenter().ClientTemplate({2}<text>", columuName, displayName, "@");
                    listGrid.AppendLine();
                    listGrid.AppendFormat("                    #if( {0} != null && {0} != \"[]\" &&picture!=\"\"){#", columuName);
                    listGrid.AppendLine();
                    listGrid.AppendFormat("                    <span style=\"color:blue;cursor:pointer;\" onclick=\"DownloadFile('${{0}}')\">下载</span>", columuName);
                    listGrid.AppendLine();
                    listGrid.AppendLine("                    #}else{#");
                    listGrid.AppendLine("                    <span style=\"color:gray;\">下载</span>");
                    listGrid.AppendLine("                    #}#");
                    listGrid.AppendLine("                </text>);");

                }
                else if (dataType == "datetime")
                {
                    listGrid.AppendFormat("                 cols.Bound(col => col.{0}).Title(\"{1}\").Format(\"{0:c}\").Sortable(false).TextAlignCenter();", columuName, displayName);
                    listGrid.AppendLine();
                }
                else
                {
                    listGrid.AppendFormat("                 cols.Bound(col => col.{0}).Title(\"{1}\").Sortable(false).TextAlignCenter();", columuName, displayName);
                    listGrid.AppendLine();
                }
                #endregion
                #region 查询条件
                if (dr["IsQuery"].ToString().ToLower() == "true" && dataType == "datetime")
                {
                    checkString.AppendFormat("    {0}： @Html.Kendo().DatePicker().Name(\"sd\")", displayName);
                    checkString.AppendFormat("    至:  @Html.Kendo().DatePicker().Name(\"ed\")");
                    researchData.AppendLine("              $(\"#sd\").val(\"\");");
                    researchData.AppendLine("              $(\"#ed\").val(\"\");");
                    searchData.AppendFormat("              var {0} = $(\"#{0}\").val();", _columuName);
                    searchData.AppendLine();
                    searchDataStr.AppendFormat("{0}: {0},", _columuName);

                }
                else if (dr["IsQuery"].ToString().ToLower() == "true" && !string.IsNullOrEmpty(selectUrl) && string.IsNullOrEmpty(selectData))
                {
                    checkString.AppendFormat("    {0}：{1}(Html.Kendo().DropDownList().Name(\"{2}\").OptionLabel(\"--请选择--\").HtmlAttributes(new {{ {1}style = \"width:150px;\" }}).DataTextField(\"Name\").DataValueField(\"{3}\")", displayName, "@", _columuName, columuName);
                    checkString.AppendLine();
                    checkString.AppendFormat("                    .DataSource(source =>{{source.Read(read =>{{read.Url(\"{0}\").Type(HttpVerbs.Post).Data(\"\");}}).ServerFiltering(true); }})", selectUrl);
                    checkString.AppendLine();
                    checkString.AppendLine("    )");
                    researchData.AppendFormat("            $(\"#{0}\").data(\"kendoDropDownList\").value(null);", _columuName);
                    researchData.AppendLine();
                    searchData.AppendFormat("var {0} = $(\"#{0}\").val();", _columuName);
                    searchData.AppendLine();
                    searchDataStr.AppendFormat("{0}: {0},", _columuName);
                }
                else if (dr["IsQuery"].ToString().ToLower() == "true" && !string.IsNullOrEmpty(selectUrl))
                {
                    string[] datas = selectData.Split(':');
                    checkString.AppendFormat("    {0}：{1}(Html.Kendo().DropDownList().Name(\"{2}\").OptionLabel(\"--请选择--\").HtmlAttributes(new {{ {1}style = \"width:150px;\" }}).DataTextField(\"Name\").DataValueField(\"{3}\")", displayName, "@", _columuName, columuName);
                    checkString.AppendLine();
                    checkString.AppendFormat("                    .DataSource(source =>{{source.Read(read =>{{read.Url(\"{0}\").Type(HttpVerbs.Post).Data(\"function(){{return {{ {1}: $('#{2}').val() }};}}\");}}).ServerFiltering(true); }})", selectUrl, datas[0], datas[1]);
                    checkString.AppendLine();
                    checkString.AppendLine("    )");
                    researchData.AppendFormat("            $(\"#{0}\").data(\"kendoDropDownList\").value(null);", _columuName);
                    searchData.AppendFormat("var {0} = $(\"#{0}\").val();", _columuName);
                    searchData.AppendLine();
                    searchDataStr.AppendFormat("{0}: {0},", _columuName);
                }
                else if (dr["IsQuery"].ToString().ToLower() == "true")
                {
                    checkString.AppendFormat("    {0}：@Html.Kendo().TextBox().Name(\"{1}\")", displayName, _columuName);
                    checkString.AppendLine();
                    researchData.AppendFormat("            $(\"#{0}\").val(\"\");", _columuName);
                    searchData.AppendFormat("var {0} = $(\"#{0}\").val();", _columuName);
                    searchData.AppendLine();
                    searchDataStr.AppendFormat("{0}: {0},", _columuName);
                }
                #endregion
                #region 初始化数据
                //if (!string.IsNullOrEmpty(defaultVal))
                //{
                //    string name = dr["name"].ToString();
                //    if (defaultVal == "当前时间")
                //        defaultValue.AppendFormat("            model.{0} = DateTime.Now;", name);
                //    else if (defaultVal == "登入人")
                //    {
                //        defaultValue.AppendFormat("            model.{0} = Platform.Core.PlatformContext.CurrentUser.UserName;", name);
                //    }
                //    defaultValue.AppendLine();
                //}
                #endregion
            }
        }
        public void setViewAdd()
        {
            StringBuilder sb = new StringBuilder();
            #region 模版
            sb.AppendFormat("{0}{{", "@");
            sb.AppendLine();
            sb.AppendFormat("    PlatformContext.ApplicationScreen.Title = \"{0}管理\";", functionNme);
            sb.AppendLine();
            //sb.AppendFormat("    Layout = \"~/Views/Shared/KLayouts/_GridViewLayout.cshtml\";");
            //sb.AppendLine();
            sb.AppendLine("}");
            #endregion
            #region 样式
            sb.AppendLine("<style>");
            sb.AppendLine("    .inline-label1 {");
            sb.AppendLine("        width: 45%;");
            sb.AppendLine("    }");
            sb.AppendLine("");
            sb.AppendLine("    .block-label1 {");
            sb.AppendLine("        width: 94%;");
            sb.AppendLine("    }");
            sb.AppendLine("");
            sb.AppendLine("    .k-edit-form-container .block-label1 .label1 {");
            sb.AppendLine("        width: 14%;");
            sb.AppendLine("    }");
            sb.AppendLine("");
            sb.AppendLine("    .ko-UploadPic-file {");
            sb.AppendLine("        float: left;");
            sb.AppendLine("        margin-right: 10px;");
            sb.AppendLine("    }");
            sb.AppendLine("");
            sb.AppendLine("    .ko-upload-filelist {");
            sb.AppendLine("        margin-top: 10px;");
            sb.AppendLine("    }");
            sb.AppendLine("");
            sb.AppendLine("    .ko-upload-container .k-upload-status {");
            sb.AppendLine("        display: none;");
            sb.AppendLine("    }");
            sb.AppendLine("");
            sb.AppendLine("    .ko-upload-container .k-header {");
            sb.AppendLine("        background-color: transparent;");
            sb.AppendLine("        border: none;");
            sb.AppendLine("    }");
            sb.AppendLine("");
            sb.AppendLine("    .ko-upload-container .k-dropzone {");
            sb.AppendLine("        padding: 0 0 0 0;");
            sb.AppendLine("    }");
            sb.AppendLine("</style>");
            #endregion
            sb.AppendLine("<script type=\"text/javascript\">");
            sb.AppendLine("    $(function () {");
            sb.AppendFormat("        if ($(\"#{0}\").val().length == 0) {{", dt.Rows[0][0].ToString());
            sb.AppendLine();
            sb.AppendFormat("            $(\"#{0}\").val(\"00000000-0000-0000-0000-000000000000\");", dt.Rows[0][0].ToString());
            sb.AppendLine();
            sb.AppendLine("        }");
            sb.AppendLine("    })");
            sb.AppendLine("</script>");
            sb.AppendLine("@Html.AjaxEditorForm(width: 800, isDialog: false)");

            File.WriteAllText(CreateHelper.getPath(dtName) + "//Views" + "//" + dtName + "Add.cshtml", sb.ToString());
            File.WriteAllText(CreateHelper.getPath(dtName) + "//Views" + "//" + dtName + "Query.cshtml", sb.ToString());
        }

        public void setViewList()
        {
            string namesp = strNamespace.Substring(strNamespace.LastIndexOf('.') + 1).TrimEnd('s');
            StringBuilder sb = new StringBuilder();
            #region 模版权限
            sb.AppendLine("@{");
            sb.AppendFormat("    PlatformContext.ApplicationScreen.Title = \"{0}管理\";", functionNme);
            sb.AppendLine();
            sb.AppendLine("    Layout = \"~/Views/Shared/KLayouts/_GridViewLayout.cshtml\";");
            sb.AppendFormat("    var admin = User.IsInRole(\"{0}.管理员\");", namesp);
            sb.AppendLine();
            sb.AppendFormat("    var add = User.IsInRole(\"{0}.{1}：添加\");", namesp, functionNme);
            sb.AppendLine();
            sb.AppendFormat("    var edit = User.IsInRole(\"{0}.{1}：编辑\");", namesp, functionNme);
            sb.AppendLine();
            sb.AppendFormat("    var del = User.IsInRole(\"{0}.{1}：删除\");", namesp, functionNme);
            sb.AppendLine();
            sb.AppendLine("}");
            #endregion

            #region 样式
            sb.AppendLine("<style>");
            sb.AppendLine("    .s-search {");
            sb.AppendLine("        padding-top: 5px;");
            sb.AppendLine("        padding-bottom: 5px;");
            sb.AppendLine("        background-color: #ffffff;");
            sb.AppendLine("        border: 0px;");
            sb.AppendLine("    }");
            sb.AppendLine("");
            sb.AppendLine("    table {");
            sb.AppendLine("        width: 100%;");
            sb.AppendLine("        border: 1px solid #cccccc;");
            sb.AppendLine("    }");
            sb.AppendLine("");
            sb.AppendLine("    .tabtable {");
            sb.AppendLine("        width: 100%;");
            sb.AppendLine("    }");
            sb.AppendLine("");
            sb.AppendLine("    th {");
            sb.AppendLine("        background-color: #f9f9f9;");
            sb.AppendLine("        text-align: center;");
            sb.AppendLine("        vertical-align: middle;");
            sb.AppendLine("    }");
            sb.AppendLine("");
            sb.AppendLine("    .k-grid td, .k-grid th {");
            sb.AppendLine("        border-width: 0px 1px 1px 0px;");
            sb.AppendLine("    }");
            sb.AppendLine("");
            sb.AppendLine("    table th {");
            sb.AppendLine("        margin: 0px;");
            sb.AppendLine("        padding: 0px;");
            sb.AppendLine("        line-height: 30px;");
            sb.AppendLine("        text-align: center;");
            sb.AppendLine("        border-bottom: 1px solid #cccccc;");
            sb.AppendLine("        border-right: 1px solid #cccccc;");
            sb.AppendLine("    }");
            sb.AppendLine("");
            sb.AppendLine("    table td {");
            sb.AppendLine("        margin: 0px;");
            sb.AppendLine("        padding: 0px;");
            sb.AppendLine("        line-height: 30px;");
            sb.AppendLine("        text-align: center;");
            sb.AppendLine("    }");
            sb.AppendLine("");
            sb.AppendLine("    .textright {");
            sb.AppendLine("        text-align: right;");
            sb.AppendLine("        display: inline-block;");
            sb.AppendLine("        float: right;");
            sb.AppendLine("    }");
            sb.AppendLine("    .textcolor {");
            sb.AppendLine("        color: red;");
            sb.AppendLine("        font-size: 12px;");
            sb.AppendLine("    }");
            sb.AppendLine("    .tsuccess {");
            sb.AppendLine("        color: #27b605;");
            sb.AppendLine("        font-weight: bold;");
            sb.AppendLine("    }");
            sb.AppendLine("    .twning {");
            sb.AppendLine("        color: #ff6a00;");
            sb.AppendLine("        font-weight: bold;");
            sb.AppendLine("    }");
            sb.AppendLine("</style>");
            #endregion
            #region 工具栏
            sb.AppendFormat("{0}section ToolsBar{{", "@");
            sb.AppendLine();
            sb.AppendFormat("    {0}if (admin||add){{", "@");
            sb.AppendLine();
            sb.AppendFormat("        {0}Html.CommandButton(\"添加\", \"k-add\", \"{1}Add(null)\", \"btnAdd\", true)", "@", dtName);
            sb.AppendLine();
            sb.AppendLine("    }");
            sb.AppendLine("}");
            #endregion
            #region 查询
            sb.AppendFormat("{0}section Search{{", "@");
            sb.AppendLine();
            sb.AppendLine(checkString.ToString());
            sb.AppendLine("}");
            #endregion
            #region grid
            sb.AppendLine("<div style=\"margin:auto;\">");
            sb.AppendFormat("    {0}(", "@");
            sb.AppendLine();
            sb.AppendFormat(" Html.Kendo().Grid<{0}.Models.{1}Model>()", strNamespace, dtName);
            sb.AppendFormat("      .Name(\"{0}Data\")", dtName);
            sb.AppendLine("  .Columns(cols =>");
            sb.AppendLine("             {");
            string pkname = dt.Rows[0]["name"].ToString();
            sb.AppendLine(listGrid.ToString());
            sb.AppendFormat("                 cols.Bound(col => col.{1}).Title(\"操作\").Sortable(false).TextAlignCenter().ClientTemplate({0}<text>", "@", pkname);
            sb.AppendLine();
            sb.AppendLine("            @if (admin || edit)");
            sb.AppendLine("            {");

            sb.AppendFormat("                @Html.CommandButton(\"编辑\", \"s-i-edit\", \"{0}Add('${{{1}}}')\")", dtName, pkname);
            sb.AppendLine();
            sb.AppendLine("            }");
            sb.AppendLine("            @if (admin || del)");
            sb.AppendLine("            {");
            sb.AppendFormat("                @Html.CommandButton(\"删除\", \"s-i-del\", \"{0}Delete('${{{1}}}')\")", dtName, pkname);
            sb.AppendLine();
            sb.AppendLine("            }");
            sb.AppendFormat("            @Html.CommandButton(\"查看\", \"s-i-check\", \"{0}Query('${{{1}}}')\")", dtName, pkname);
            sb.AppendLine();
            sb.AppendLine("                </text>);");
            sb.AppendLine("             }).Pageable()");
            sb.AppendLine("                                             .DataSource(dataSource => dataSource");
            sb.AppendLine("                                             .Ajax()");

            sb.AppendLine("                                             .PageSize(20)");
            sb.AppendLine("                                             .ServerOperation(true)");
            sb.AppendLine("                                             .Events(events =>");
            sb.AppendLine("                                             {");
            sb.AppendLine("                                             //events.Error(\"errorHandler\");");
            sb.AppendLine("                                             })");
            sb.AppendLine("                                             .Read(read =>");
            sb.AppendLine("                                             {");
            sb.AppendFormat("                                             read.Action(\"ListPage\", \"{0}\");", dtName);
            sb.AppendLine();
            sb.AppendLine("                                             read.Data(\"searchdata\");");
            sb.AppendLine("                                              })");
            sb.AppendLine("                                             )");
            sb.AppendLine("             )");
            sb.AppendLine("</div>");
            #endregion
            #region 脚本
            sb.AppendLine("@section Scripts{");
            sb.AppendLine("    <script src=\"@Url.Content(\"~/KScripts/jquery.kendo.extend.js\")\"></script>");
            sb.AppendLine("    <script>");
            sb.AppendLine("            function researchdata() {");
            sb.AppendLine(researchData.ToString());
            sb.AppendLine("        }");
            sb.AppendLine("        function searchdata() {");
            sb.AppendLine(searchData.ToString());

            sb.AppendFormat("            return {{{0} }};", searchDataStr.ToString().TrimEnd(','));
            sb.AppendLine();
            sb.AppendLine("        }");
            sb.AppendLine("        function Search() {");

            sb.AppendFormat("            $(\"#{0}Data\").data(\"kendoGrid\").dataSource.read({{ page: 1 }});", dtName);
            sb.AppendLine();
            sb.AppendLine("            if ($(\".k-pager-numbers a\").eq(0)) {");
            sb.AppendLine("                var page = $(\".k-pager-numbers a\").eq(0).attr(\"data-page\");");
            sb.AppendLine("                if (page != null && page == \"1\") {");
            sb.AppendLine("                    $(\".k-pager-numbers a\").eq(0).click();");
            sb.AppendLine("                }");
            sb.AppendLine("            }");
            sb.AppendLine("        }");
            sb.AppendFormat("        function {0}Query(id) {{", dtName);
            sb.AppendLine();
            sb.AppendLine("            kendo.showDialog({");
            sb.AppendFormat("                id: \"{0}Query\",", dtName);
            sb.AppendLine();
            sb.AppendFormat("                title: \"{0}信息\",", functionNme);
            sb.AppendLine();
            sb.AppendLine("                content: {");
            sb.AppendFormat("                    url: \"/{0}/{0}Query\",", dtName);
            sb.AppendLine();
            sb.AppendLine("                    data: { id: id },");
            sb.AppendLine("                    complete: function (data) {");
            sb.AppendLine("                        if (!data.success) {");
            sb.AppendLine("                            alert(\"没有查到相应数据，请检查相应数据是否已经删除！\")");
            sb.AppendLine("                        }");
            sb.AppendLine("                    }");
            sb.AppendLine("                }");
            sb.AppendLine("            })");
            sb.AppendLine("        }");
            sb.AppendFormat("        function {0}Add(id) {{", dtName);
            sb.AppendLine();

            sb.AppendLine("            kendo.showDialog({");
            sb.AppendFormat("                id: \"{0}ADD\",", dtName);
            sb.AppendLine();
            sb.AppendFormat("                title: id ? \"{0}编辑\" : \"{0}新增\",", functionNme);
            sb.AppendLine();
            sb.AppendLine("                content: {");
            sb.AppendFormat("                    url: \"/{0}/{0}Add\",", dtName);
            sb.AppendLine();
            sb.AppendLine("                    data: { id: id },");
            sb.AppendLine("                    complete: function (data) {");
            sb.AppendLine("                        if (data.success) {");
            sb.AppendLine("                            alert(\"操作成功！\");");
            sb.AppendFormat("                            $(\"#{0}Data\").data(\"kendoGrid\").dataSource.read();", dtName);
            sb.AppendLine();
            sb.AppendLine("                        }");
            sb.AppendLine("                        else {");
            sb.AppendLine("                            alert(\"操作失败！\")");
            sb.AppendLine("                        }");
            sb.AppendLine("                    }");
            sb.AppendLine("                },");
            sb.AppendLine("            });");
            sb.AppendLine("        }");
            sb.AppendFormat("        function {0}Delete(id) {{", dtName);
            sb.AppendLine();
            sb.AppendLine("            if (confirm(\"确认要删除该条信息吗？\")) {");
            sb.AppendLine("                $.ajax({");
            sb.AppendLine("                    type: \"POST\",");
            sb.AppendFormat("                    url: \"/{0}/{0}Delete\",", dtName);
            sb.AppendLine();
            sb.AppendLine("                    data: { id: id },");
            sb.AppendLine("                    success: function (re) {");
            sb.AppendLine("                        if (re.success) {");
            sb.AppendLine("                            alert(\"删除成功!\");");
            sb.AppendFormat("                            $(\"#{0}Data\").data(\"kendoGrid\").dataSource.read();", dtName);
            sb.AppendLine();
            sb.AppendLine("                        } else {");
            sb.AppendLine("                            alert(\"删除失败!\");");
            sb.AppendLine("                        }");
            sb.AppendLine("                    }");
            sb.AppendLine("                });");
            sb.AppendLine("            }");
            sb.AppendLine("        }");
            sb.AppendLine("        function DownloadFile(filenames) {");
            sb.AppendLine("            kendo.showDialog({");
            sb.AppendLine("                title: \"下载文档列表\",");
            sb.AppendLine("                content: {");
            sb.AppendLine("                    url: \"/Demo/DownLoadFiles\",");
            sb.AppendLine("                    data: { names: filenames }");
            sb.AppendLine("                },");
            sb.AppendLine("            })");
            sb.AppendLine("        }");
            sb.AppendLine("    </script>");
            sb.AppendLine("}");
            #endregion
            File.WriteAllText(CreateHelper.getPath(dtName) + "//Views" + "//" + dtName + "List.cshtml", sb.ToString());

        }
    }
}