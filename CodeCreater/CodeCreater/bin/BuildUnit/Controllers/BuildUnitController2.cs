using System;
using Kendo.Mvc.UI;
using System.Web.Mvc;
using System.Collections.Generic;
using RTSafe.Platform.Module.Mvc;
using RTSafe.Platform.Module.Attributes;

using RTSafe.HiddenTroubleTreatm.BusinessModules.HiddenTroubleTreatmModules.Models;
using RTSafe.HiddenTroubleTreatm.BusinessModules.HiddenTroubleTreatmModules.Domain;

namespace RTSafe.HiddenTroubleTreatm.BusinessModules.HiddenTroubleTreatmModules.Controllers
{

    [ControllerDescription(隐患排查与治理, 台帐)]
    [Authorize]

    public class BuildUnitController : ModuleController
    {
        IBuildUnitService buildUnitService;
        public BuildUnitController(IBuildUnitService _buildUnitService)
        {
            this.buildUnitService = _buildUnitService;
        }
        [ActionDescription("台帐管理"," /BuildUnit/BuildUnitList)")]
        [RoleName("管理员","台帐：查询")]
        public ActionResult BuildUnitList()
        {
            return View();
        }

        public ActionResult BuildUnitAdd(Guid? id)
        {
            BuildUnitModel model = new BuildUnitModel();

            if (id.HasValue)
                model = buildUnit.Get(id.Value);
            return PartialView(model);
        }
        [HttpPost]

        [RoleName("管理员", "台帐：添加")]
        public ActionResult BuildUnitAdd(BuildUnitModel model)
        {
            int re = 0;
            if (ModelState.IsValid)
            {
                if (model != null)
                {

                    if (model.BID != Guid.Empty)
                        re = buildUnit.Edit(model);
                    else
                    {
                        model.BID = Guid.NewGuid();
                        re = buildUnit.Add(model);
                    }
                    if (re > 0)
                        return AjaxResult(success: re > 0, data: model, ajaxDataTypes: AjaxDataTypes.Json);
                }
                return AjaxResult(success: false, data: null, ajaxDataTypes: RTSafe.Platform.Module.Mvc.AjaxDataTypes.Json);
            }
            else
            {
                ModelState.AddModelError("erro", "验证不通过，请检查输入");
            }
            return PartialView(model);
        }

        [RoleName("管理员", "台帐：查询")]
        public ActionResult BuildUnitQuery(Guid? id)        {
            BuildUnitModel model = new BuildUnitModel();
            if (id.HasValue)

                model = buildUnit.Get(id.Value);
            return View(model);
        }
        public ActionResult ListPage([DataSourceRequest]  DataSourceRequest dsRequest,Guid? lineID,Guid? segmentID,Guid? siteID,string? createUser,Nullable<System.DateTime>? createTime,)
        {
            var size = dsRequest.PageSize;
            var index = dsRequest.Page;
            var total = 0;
            var modelWrapper = buildUnit.GetAllPage(index, size, lineID,segmentID,siteID,createUser,createTime, out total);
            var rs = new DataSourceResult();
            rs.Data = modelWrapper;
            rs.Total = total;
            return Json(rs);
        }
    }
}
