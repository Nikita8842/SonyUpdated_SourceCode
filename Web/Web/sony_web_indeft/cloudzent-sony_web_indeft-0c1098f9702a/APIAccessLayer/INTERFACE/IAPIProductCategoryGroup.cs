using AMBOModels.GlobalAccessible;
using AMBOModels.MasterMaintenance;
using APIAccessLayer.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAccessLayer.INTERFACE
{
    public interface IAPIProductCategoryGroup
    {
        Envelope<bool> CreateProductCategoryGroup(ProductCategoryGroupMaster objFormData);
        Envelope<bool> UpdateProductCategoryGroup(ProductCategoryGroupMaster objFormData);
        Envelope<bool> DeleteProductCategoryGroup(Common InputParam);
        
        ProductCategoryGroupMaster GetProductCategoryGroupById(Common InputParam);
    }
}
