using AMBOModels.GlobalAccessible;
using AMBOModels.MasterMaintenance;
using AMBOWeb.Filters;
using APIAccessLayer.Helper;
using APIAccessLayer.INTERFACE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static AMBOWeb.Classes.Common;

namespace AMBOWeb.Controllers
{
    public class ProductCategoryGroupController : SonyBaseController
    {
        private readonly IAPIProductCategoryGroup _IAPIProductCategoryGroup;
        
        public static IAPICommon _Common;

        public ProductCategoryGroupController(IAPIProductCategoryGroup IAPIProductCategoryGroup, IAPICommon IAPICommon)
        {
            _IAPIProductCategoryGroup = IAPIProductCategoryGroup;
            _Common = IAPICommon;
        }
        
        [HttpGet]
        [SessionAuthorize(ModuleId = (Int64)Modules.Product, SubModuleId = (Int64)SubModules.ProductCatGroup, Rights = (int)Right.View)]
        public ActionResult Index()
        {           
            return View();
        }

        //[HttpGet]
        //public ActionResult GetProductCategoryGroup()
        //{
        //    var output = _IAPIProductCategoryGroup.GetProductCategoryGroup();
        //    return Json(output, JsonRequestBehavior.AllowGet);
        //}

        [HttpGet]
        [SessionAuthorize(ModuleId = (Int64)Modules.Product, SubModuleId = (Int64)SubModules.ProductCatGroup, Rights = (int)Right.Create)]
        public ActionResult AddUpdateProductCategoryGroup(string GroupId,string Operation)
        {
            ProductCategoryGroupMaster Data = new ProductCategoryGroupMaster();
             if (Operation == "Update")
            {
                Common InputData = new Common();
                InputData.Id = Convert.ToInt32(GroupId);
                Data = _IAPIProductCategoryGroup.GetProductCategoryGroupById(InputData);
            }
            //FillList(Data);
            return View(Data);

        }

        //private void FillList(ProductCategoryGroupMaster data)
        //{

        //    var ListDisplayOrder = _Common.GetDisplayOrder();
        //    if (ListDisplayOrder == null)
        //        ListDisplayOrder = new List<DisplayOrder>();
        //    ListDisplayOrder.Add(new DisplayOrder { DisplayOrderId = 0, DisplayOrderName = "Select Display Order" });
        //    ViewBag.DisplayOrderList = new SelectList(ListDisplayOrder.OrderBy(b => b.DisplayOrderId), "DisplayOrderId", "DisplayOrderName");
        //}

        [HttpPost]
        [SessionAuthorize(ModuleId = (Int64)Modules.Product, SubModuleId = (Int64)SubModules.ProductCatGroup, Rights = (int)Right.Create)]
        public ActionResult AddUpdateProductCategoryGroup(ProductCategoryGroupMaster objFormData)

        {
            Envelope<bool> output = null;
            objFormData.UserId = objUserSession.UserId;
            objFormData.EncryptKey = objUserSession.EncryptKey;
            objFormData.RoleName = objUserSession.RoleName;
            if (ModelState.IsValid)
            {
                if (objFormData.GroupId == 0)
                {
                    output = _IAPIProductCategoryGroup.CreateProductCategoryGroup(objFormData);
                    if (output.Data)
                    {
                        AddSuccessNotification(output.Message);
                        return Redirect("Index");
                    }
                    else
                    {
                        AddErrorNotification(output.Message);
                        return View(objFormData);
                    }
                }
                else
                {
                    output = _IAPIProductCategoryGroup.UpdateProductCategoryGroup(objFormData);
                    if (output.Data)
                    {
                        AddSuccessNotification(output.Message);
                        return Redirect("Index");
                    }
                    else
                    {
                        AddErrorNotification(output.Message);
                        return View(objFormData);
                    }
                }
                
                //ViewBag.Status = isInserted.ToString();
                //ViewBag.Message = result;
            }
            else
                ViewBag.Message = "Invalid data entered.";
            //FillList(objFormData);
            return View(objFormData);
        }

        [HttpPost]
        [SessionAuthorize(ModuleId = (Int64)Modules.Product, SubModuleId = (Int64)SubModules.ProductCatGroup, Rights = (int)Right.Delete)]
        public ActionResult DeleteProductCategoryGroup(ProductCategoryGroupMaster Data)
        {
            Common InputParam = new Common();
            InputParam.Id = Data.GroupId;
            InputParam.UserId = objUserSession.UserId;
            InputParam.EncryptKey = objUserSession.EncryptKey;
            InputParam.RoleName = objUserSession.RoleName;
            var output = _IAPIProductCategoryGroup.DeleteProductCategoryGroup(InputParam);
            return Json(output);
        }
    }
}