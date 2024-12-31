using AMBOModels.MasterMaintenance;
using APIAccessLayer.Helper;

namespace APIAccessLayer.INTERFACE
{
    public interface IAPIBranchData
    {
        Envelope<bool> CreateBranch(CreateBranchForm objFormData);

        Envelope<bool> UpdateBranch(UpdateBranchForm objFormData);

        Envelope<bool> DeleteBranch(DeleteBranchForm objFormData);
    }
}
