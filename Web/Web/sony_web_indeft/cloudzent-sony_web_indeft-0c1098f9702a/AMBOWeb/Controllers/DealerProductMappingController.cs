using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using APIAccessLayer.INTERFACE;
using AMBOModels.Mappings;
using AMBOModels.MasterMaintenance;

namespace AMBOWeb.Controllers
{
    public class DealerProductMappingController : SonyBaseController
    {
        private readonly IAPIMappingData _APIMappingData;
        private readonly IAPIDealerData _APIDealerData;

        public DealerProductMappingController(IAPIMappingData IAPIMappingData, IAPIDealerData IAPIDealerData)
        {
            _APIMappingData = IAPIMappingData;
            _APIDealerData = IAPIDealerData;
        }

        // GET: DealerProductMapping
        public ActionResult Index(string DealerCode)
        {
            DealerMaster input = new DealerMaster();
            input.DealerCode = DealerCode;
            var output = _APIDealerData.GetDealerById(input);
            DealerProductMapping result = new DealerProductMapping();
            if (output != null)
            {
                
                result.DealerId = output.ID;
                result.DealerCode = output.DealerCode;
                result.DealerName = output.DealerName;
                result.BranchId = output.BranchId;
                result.MobileNumber = output.MobileNumber;
                result.EmailID = output.EmailID;
            }
            return View(result);
        }

        [HttpPost]
        public ActionResult Create(DealerProductMapping InputParam)
        {            
            InputParam.UserId = objUserSession.UserId;      
            var output = _APIMappingData.ManageDealerClassificationMapping(InputParam);

            return Json(output, JsonRequestBehavior.AllowGet);
        }
    }
}