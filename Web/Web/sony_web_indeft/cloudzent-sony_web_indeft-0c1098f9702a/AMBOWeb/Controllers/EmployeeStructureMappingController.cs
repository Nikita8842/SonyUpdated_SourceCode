using AMBOModels.Mappings;
using AMBOModels.UserManagement;
using AMBOWeb.Filters;
using APIAccessLayer.INTERFACE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static AMBOWeb.Classes.Common;

namespace AMBOWeb.Controllers
{
    public class EmployeeStructureMappingController : SonyBaseController
    {
        private readonly IAPIMappingData _APIMappingData;

        public EmployeeStructureMappingController(IAPIMappingData IAPIMappingData)
        {
            _APIMappingData = IAPIMappingData;
        }

        public ActionResult GetEmployeeStructureMapGridFilters()
        {
            return PartialView("PartialViews/EmployeeStructureMapGridFilters");
        }

        [HttpGet]
        [SessionAuthorize(ModuleId = (Int64)Modules.Employee, SubModuleId = (Int64)SubModules.EmployeeStructureMapping, Rights = (int)Right.View)]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [SessionAuthorize(ModuleId = (Int64)Modules.Employee, SubModuleId = (Int64)SubModules.EmployeeStructureMapping, Rights = (int)Right.Create)]
        public JsonResult GetSFAForStructureMapping(SFAForStructureMappingInput objSearchData)
        {
            var output = _APIMappingData.GetSFAForStructureMapping(objSearchData);
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [SessionAuthorize(ModuleId = (Int64)Modules.Employee, SubModuleId = (Int64)SubModules.EmployeeStructureMapping, Rights = (int)Right.Create)]
        public ActionResult Create(EmployeeStructureMapping mappingData)
        {
            var output = _APIMappingData.CreateEmployeeStructureMapping(mappingData);
            return Json(output);
        }

        [HttpPost]
        [SessionAuthorize(ModuleId = (Int64)Modules.Employee, SubModuleId = (Int64)SubModules.EmployeeStructureMapping, Rights = (int)Right.Delete)]
        public ActionResult Delete(EmployeeStructureMapping mappingData)
        {
            var output = _APIMappingData.DeleteEmployeeStructureMapping(mappingData);
            return Json(output);
        }
    }
}