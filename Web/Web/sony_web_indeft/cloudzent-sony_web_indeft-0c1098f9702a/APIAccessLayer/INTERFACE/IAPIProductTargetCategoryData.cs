using AMBOModels.MasterMaintenance;
using APIAccessLayer.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAccessLayer.INTERFACE
{
    public interface IAPIProductTargetCategoryData
    {
        Envelope<bool> DeleteProductTargetCategoryMapping(ProductTargetCategoryMaster objFormData);

        Envelope<bool> UpdateProductTargetCategoryMapping(ProductTargetCategoryMaster objFormData);

        Envelope<bool> CreateProductTargetCategoryMapping(ProductTargetCategoryMaster objFormData);
    }
}
