using AmboDataServices.Interface;
using AmboLibrary.GlobalAccessible;
using AmboLibrary.MasterMaintenance;
using AmboServices.Interface;
using AmboUtilities;
using AmboUtilities.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboServices.Implimentation
{
    public class ProductCategoryGroupService: IProductCategoryGroupService
    {
        public readonly IProductCategoryGroupDataService _IProductCategoryGroupDataService;
        public ProductCategoryGroupService(IProductCategoryGroupDataService IProductCategoryGroupDataService)
        {
            _IProductCategoryGroupDataService = IProductCategoryGroupDataService;
        }

        public Envelope<IEnumerable<ProductGroupCategoryMaster>> GetProductCategoryGroup(ProCatGroupFilter InputParam)
        {
            var output = _IProductCategoryGroupDataService.GetProductCategoryGroup(InputParam);
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<ProductGroupCategoryMaster>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<ProductGroupCategoryMaster>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Product Category Group data fetched successfully." };
        }

        public Envelope<ProductGroupCategoryMaster> GetProductCategoryGroupById(Common InputParam)
        {
            var output = _IProductCategoryGroupDataService.GetProductCategoryGroupById(InputParam);
            return (output == null) ?
                new Envelope<ProductGroupCategoryMaster> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<ProductGroupCategoryMaster> { Data = output, MessageCode = (int)Acceptable.Found, Message = "All Product Category Group data fetched successfully." };
        }

        public Envelope<bool> CreateProductCategoryGroup(ProductGroupCategoryMaster InputParam)
        {
            var Message = string.Empty;
            var output = _IProductCategoryGroupDataService.CreateProductCategoryGroup(InputParam, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Created, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }

        public Envelope<bool> UpdateProductCategoryGroup(ProductGroupCategoryMaster stateData)
        {
            var Message = string.Empty;
            var output = _IProductCategoryGroupDataService.UpdateProductCategoryGroup(stateData, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Created, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }

        public Envelope<bool> DeleteProductCategoryGroup(Common InputParam)
        {
            var Message = string.Empty;
            var output = _IProductCategoryGroupDataService.DeleteProductCategoryGroup(InputParam, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Created, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }
    }
}
