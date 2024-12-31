using AmboLibrary.GlobalAccessible;
using AmboLibrary.MasterMaintenance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboDataServices.Interface
{
    public interface IProductCategoryGroupDataService
    {
        IEnumerable<ProductGroupCategoryMaster> GetProductCategoryGroup(ProCatGroupFilter InputParam);
        ProductGroupCategoryMaster GetProductCategoryGroupById(Common InputParam);
        bool CreateProductCategoryGroup(ProductGroupCategoryMaster InputParam, out string Message);
        bool UpdateProductCategoryGroup(ProductGroupCategoryMaster InputParam, out string Message);
        bool DeleteProductCategoryGroup(Common InputParam, out string Message);
    }
}
