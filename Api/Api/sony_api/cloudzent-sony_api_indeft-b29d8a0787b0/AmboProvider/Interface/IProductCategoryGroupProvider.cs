using AmboLibrary.GlobalAccessible;
using AmboLibrary.MasterMaintenance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboProvider.Interface
{
    public interface IProductCategoryGroupProvider
    {
        IEnumerable<ProductGroupCategoryMaster> GetProductCategoryGroup(ProCatGroupFilter InputParam);
        int CreateProductCategoryGroup(ProductGroupCategoryMaster InputParam, out string Message);
        int UpdateProductCategoryGroup(ProductGroupCategoryMaster InputParam, out string Message);
        int DeleteProductCategoryGroup(Common InputParam, out string Message);
        ProductGroupCategoryMaster GetProductCategoryGroupById(Common InputParam);
    }
}
