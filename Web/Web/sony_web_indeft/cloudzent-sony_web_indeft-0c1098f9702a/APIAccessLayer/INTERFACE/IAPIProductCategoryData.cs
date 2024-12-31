using AMBOModels.MasterMaintenance;
using APIAccessLayer.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAccessLayer.INTERFACE
{
    public interface IAPIProductCategoryData
    {
        Envelope<bool> CreateProductCategory(CreateProductCategoryForm objFormData);
        Envelope<bool> UpdateProductCategory(UpdateProductCategoryForm objFormData);
        Envelope<bool> DeleteProductCategory(DeleteProductCategoryForm objFormData);

    }
}
