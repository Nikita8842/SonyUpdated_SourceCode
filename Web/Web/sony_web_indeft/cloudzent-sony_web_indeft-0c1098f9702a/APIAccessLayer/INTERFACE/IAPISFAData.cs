using AMBOModels.Mappings;
using AMBOModels.UserManagement;
using APIAccessLayer.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAccessLayer.INTERFACE
{
    public interface IAPISFAData
    {
        Envelope<bool> CreateSFA(SFAFormData objFormData);

        Envelope<bool> UpdateSFA(SFAFormData objFormData);

        SFAFormData GetSFAById(Int64? ID);

        Envelope<bool> DeleteSFA(SFAFormData objFormData);

        SFAFormData GetOfferedEmployeeById(Int64? ID);

        Envelope<bool> ManageOfferedEmployee(SFAFormData List);

        Envelope<SFACodeAutoGenerate> GetSFACode();

    }
}
