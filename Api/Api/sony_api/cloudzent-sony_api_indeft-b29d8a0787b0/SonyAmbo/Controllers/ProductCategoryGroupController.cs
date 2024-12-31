using AmboLibrary.GlobalAccessible;
using AmboLibrary.MasterMaintenance;
using AmboServices.Interface;
using AmboUtilities;
using AmboUtilities.Helper;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SonyAmbo.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [ExceptionHandling]
    [Compression]
    public class ProductCategoryGroupController : ApiController
    {
      
        private readonly IProductCategoryGroupService _IProductCategoryGroupService;

        public ProductCategoryGroupController(IProductCategoryGroupService IProductCategoryGroupService)
        {
            _IProductCategoryGroupService = IProductCategoryGroupService;
        }

        [HttpPost]
        public IHttpActionResult GetProductCategoryGroup(Envelope<ProCatGroupFilter> InputParam)
        {
            var output = _IProductCategoryGroupService.GetProductCategoryGroup(InputParam.Data);
            return Ok(output);
        }

        [HttpPost]
        public IHttpActionResult GetProductCategoryGroupById(Envelope<Common> InputParam)
        {
            var output = _IProductCategoryGroupService.GetProductCategoryGroupById(InputParam.Data);
            return Ok(output);
        }
        [HttpPost]
        public IHttpActionResult CreateProductCategoryGroup(Envelope<ProductGroupCategoryMaster> InputParam)
        {
            var output = _IProductCategoryGroupService.CreateProductCategoryGroup(InputParam.Data);
            return Ok(output);
        }

        [HttpPost]
        public IHttpActionResult UpdateProductCategoryGroup(Envelope<ProductGroupCategoryMaster> InputParam)
        {
            var output = _IProductCategoryGroupService.UpdateProductCategoryGroup(InputParam.Data);
            return Ok(output);
        }

        [HttpPost]
        public IHttpActionResult DeleteProductCategoryGroup(Envelope<Common> InputParam)
        {
            var output = _IProductCategoryGroupService.DeleteProductCategoryGroup(InputParam.Data);
            return Ok(output);
        }
    }
}