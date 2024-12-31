using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static AMBOWeb.Classes.Common;
using AMBOModels.Global;
using AMBOModels.GlobalAccessible;
using AMBOModels.MasterMaintenance;
using AMBOWeb.Filters;
using APIAccessLayer.Helper;
using APIAccessLayer.INTERFACE;

namespace AMBOWeb.Controllers
{
    /// <summary>
    /// To perform Competitor related operations including model and product bindings.
    /// Dhruv Sharma, VFirst, Gurugram
    /// </summary>
    public class CompetitorController : SonyBaseController
    {
        public static IAPICompetitorMasterData _Competitor;
        public static IAPIGridData _Grid;
        public static IAPICommon _Common;

        public CompetitorController(IAPICompetitorMasterData IAPICompetitorMasterData, IAPIGridData IAPIGridData, IAPICommon IAPICommon)
        {
            _Competitor = IAPICompetitorMasterData;
            _Grid = IAPIGridData;
            _Common = IAPICommon;
        }

        #region Competitor Master
        public ActionResult GetCompetitors()
        {
            var result = _Competitor.GetCompetitors();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [SessionAuthorize(ModuleId = (Int64)Modules.Product, SubModuleId = (Int64)SubModules.Competitor, Rights = (int)Right.View)]
        public ActionResult CompetitorMaster()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CompetitorMaster(CompetitorMaster Data, string Operation ="")
        {
            Data.UserId = objUserSession.UserId;
            Data.EncryptKey = objUserSession.EncryptKey;
            Data.RoleName = objUserSession.RoleName;
            Data.CreatedBy = objUserSession.UserId;
            if (Operation != "Delete")
            {
                if (Data.ID != 0)
                {
                    var output = _Competitor.UpdateCompetitorMaster(Data);
                    if (output.Data)
                        AddSuccessNotification("Competitor updated successfully.");
                    return Json(output, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var output = _Competitor.InsertCompetitorMaster(Data);
                    if (output.Data)
                        AddSuccessNotification("Competitor created successfully.");
                    return Json(output, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                var output = _Competitor.DeleteCompetitorMaster(Data);
                if(output.Data)
                    AddSuccessNotification("Competitor deleted successfully.");
                return Json(output, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region Competitor Product Master
        [HttpGet]
        public ActionResult CompetitorProductMaster()
        {
            //CompetitorFilter obj = new CompetitorFilter();
            //obj.CompetitorCodeIds = null;
            //obj.CompetitorNames = null;
            //obj.Status = null;
            //ViewBag.CompetitorList = _Grid.GetCompetitorMasterGrid(obj);
            //ViewBag.ProductCategoryList = _Common.GetProductCategories();
            return View();
        }

        [HttpPost]
        public ActionResult CompetitorProductMaster(CompetitorProductData Data, string Operation = "")
        {
            CompetitorFilter obj = new CompetitorFilter();
            obj = null;
            Data.UserId = objUserSession.UserId;
            Data.EncryptKey = objUserSession.EncryptKey;
            Data.RoleName = objUserSession.RoleName;
            Data.CreatedBy = objUserSession.UserId;

            if (Operation != "Delete")
            {
                if (Data.ID != 0)
                {
                    var output = _Competitor.UpdateCompetitorProduct(Data);
                    if (output.Data)
                        AddSuccessNotification(output.Message);
                    return Json(output, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var output = _Competitor.InsertCompetitorProduct(Data);
                    if (output.Data)
                        AddSuccessNotification(output.Message);
                    return Json(output, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                var output = _Competitor.DeleteCompetitorProduct(Data);
                if (output.Data)
                    AddSuccessNotification(output.Message);
                return Json(output, JsonRequestBehavior.AllowGet);
            }
            ViewBag.CompetitorList = _Grid.GetCompetitorMasterGrid(obj);
            ViewBag.ProductCategoryList = _Common.GetProductCategories();
            return View();
        }
        #endregion

        #region Competitor Model Master
        public ActionResult GetAllCompetitorModels()
        {
            var result = _Competitor.GetAllCompetitorModels();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult CompetitorModelMaster()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreateCompetitorModel(Int64 ModelId = 0, string Operation = "Create")
        {
            CompetitorModelList Data = new CompetitorModelList();            

            if (Operation == "Update")
            {
                CompetitorDataInput Input = new CompetitorDataInput();
                Input.CProductCatId = ModelId;
                Data = _Competitor.GetCompetitorModelById(Input);                
            }

            return View(Data);
        }        

        [HttpPost]
        public ActionResult CreateCompetitorModel(CompetitorModelList InputParam)
        {
            CompetitorModelList Output = new CompetitorModelList();
            InputParam.UserId = objUserSession.UserId;
            InputParam.EncryptKey = objUserSession.EncryptKey;
            InputParam.RoleName = objUserSession.RoleName;

            Envelope<bool> output = new Envelope<bool>();

            if (InputParam.ID != 0)
            {                
                output = _Competitor.UpdateCompetitorModel(InputParam);
                if (output.Data)
                    AddSuccessNotification(output.Message);
            }
            else
            {                
                output = _Competitor.InsertCompetitorModel(InputParam);
                if (output.Data)
                    AddSuccessNotification(output.Message);
            }            

            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteCompetitorModel(Int64 ModelId)
        {
            CompetitorModelMaster Data = new CompetitorModelMaster();
            Data.ID = ModelId;

            Data.UserId = objUserSession.UserId;
            Data.EncryptKey = objUserSession.EncryptKey;
            Data.RoleName = objUserSession.RoleName;

            var output = _Competitor.DeleteCompetitorModel(Data);
            if (output.Data)
                AddSuccessNotification(output.Message);

            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetCompetitorProductData(string Id)
        {
            CompetitorProductsInput Input = new CompetitorProductsInput();
            Input.CompetitorID = Convert.ToInt64(Id);
            List<CompetitorProducts> Data = new List<CompetitorProducts>();
            Data = _Common.GetCompetitorProducts(Input);
            return Json(Data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetSonyProductData(string CompetitorCatId)
        {
            CompetitorDataInput Input = new CompetitorDataInput();
            Input.CProductCatId = Convert.ToInt64(CompetitorCatId);
            CompetitorData Data = new CompetitorData();
            Data = _Common.GetSonyProducts(Input);
            return Json(Data, JsonRequestBehavior.AllowGet);
        }        

        #endregion
    }
}