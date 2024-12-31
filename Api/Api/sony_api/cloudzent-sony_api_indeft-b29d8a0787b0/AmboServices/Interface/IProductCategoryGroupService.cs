using AmboLibrary.GlobalAccessible;
using AmboLibrary.MasterMaintenance;
using AmboUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboServices.Interface
{
    public interface IProductCategoryGroupService
    {
        Envelope<IEnumerable<ProductGroupCategoryMaster>> GetProductCategoryGroup(ProCatGroupFilter InputParam);
        Envelope<ProductGroupCategoryMaster> GetProductCategoryGroupById(Common InputParam);
        Envelope<bool> CreateProductCategoryGroup(ProductGroupCategoryMaster InputParam);
        Envelope<bool> UpdateProductCategoryGroup(ProductGroupCategoryMaster InputParam);
        Envelope<bool> DeleteProductCategoryGroup(Common InputParam);
    }
}
