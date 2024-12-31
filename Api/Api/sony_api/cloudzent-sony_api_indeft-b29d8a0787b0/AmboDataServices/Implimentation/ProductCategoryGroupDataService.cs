using AmboDataServices.Interface;
using AmboLibrary.GlobalAccessible;
using AmboLibrary.MasterMaintenance;
using AmboProvider.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboDataServices.Implimentation
{
    public class ProductCategoryGroupDataService: IProductCategoryGroupDataService
    {
        private readonly IProductCategoryGroupProvider _IProductCategoryGroupProvider;

        public ProductCategoryGroupDataService(IProductCategoryGroupProvider IProductCategoryGroupProvider)
        {
            _IProductCategoryGroupProvider = IProductCategoryGroupProvider;
        }

        public IEnumerable<ProductGroupCategoryMaster> GetProductCategoryGroup(ProCatGroupFilter InputParam)
        {
            return _IProductCategoryGroupProvider.GetProductCategoryGroup(InputParam);
        }
        public ProductGroupCategoryMaster GetProductCategoryGroupById(Common InputParam)
        {
            return _IProductCategoryGroupProvider.GetProductCategoryGroupById(InputParam);
        }

        public bool CreateProductCategoryGroup(ProductGroupCategoryMaster InputParam, out string Message)
        {
            return Convert.ToBoolean(_IProductCategoryGroupProvider.CreateProductCategoryGroup(InputParam, out Message));
        }

        public bool UpdateProductCategoryGroup(ProductGroupCategoryMaster InputParam, out string Message)
        {
            return Convert.ToBoolean(_IProductCategoryGroupProvider.UpdateProductCategoryGroup(InputParam, out Message));
        }

        public bool DeleteProductCategoryGroup(Common InputParam, out string Message)
        {
            return Convert.ToBoolean(_IProductCategoryGroupProvider.DeleteProductCategoryGroup(InputParam, out Message));
        }
    }
}
