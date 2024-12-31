using AMBOModels.MasterMaintenance;
using APIAccessLayer.Helper;

namespace APIAccessLayer.INTERFACE
{
    public interface IAPIMaterialData
    {
        Envelope<bool> CreateMaterial(MaterialMaster objFormData);

        Envelope<bool> UpdateMaterial(MaterialMaster objFormData);

        Envelope<bool> DeleteMaterial(MaterialMaster objFormData);

        MaterialMaster GetMaterialById(MaterialMaster objFormData);

        MaterialMaster GetMaterialByMaterialCode(MaterialMaster objFormData);
    }
}
