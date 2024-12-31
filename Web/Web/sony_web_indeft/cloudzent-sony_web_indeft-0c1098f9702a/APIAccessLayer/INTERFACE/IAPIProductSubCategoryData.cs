using AMBOModels.MasterMaintenance;
using APIAccessLayer.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAccessLayer.INTERFACE
{
    public interface IAPIProductSubCategoryData
    {
        Envelope<bool> CreateProductSubCategory(CreateProductSubCategoryForm objFormData);
        Envelope<bool> UpdateProductSubCategory(UpdateProductSubCategoryForm objFormData);
        Envelope<bool> DeleteProductSubCategory(DeleteProductSubCategoryForm objFormData);
    }
}
