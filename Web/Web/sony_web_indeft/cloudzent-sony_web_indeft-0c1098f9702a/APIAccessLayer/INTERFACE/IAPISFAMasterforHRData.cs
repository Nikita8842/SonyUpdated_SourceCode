using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMBOModels.UserManagement;
using APIAccessLayer.Helper;
using System.Data;

namespace APIAccessLayer.INTERFACE
{
    public interface IAPISFAMasterforHRData
    {
        Envelope<bool> UpdateSFAMasterforHR(SFAMasterforHR objFormData);

        Envelope<bool> DeleteSFAMasterforHR(SFAMasterforHRDelete objFormData);

        Envelope<bool> CreateSFAMasterforHR(SFAMasterforHR objFormData);

        SFAMasterforHR GetSFAMasterforHRbyId(SFAMasterforHR InputParam);

        IEnumerable<SFAMasterforHR> GetAllDetailsSFAMasterforHR();

        List<SFAMasterforHR> SFAMasterforHRDataDownload(SFAMasterforHRDownload InputParam);

        Envelope<DataTable> ManageSFAMasterforHRData(List<SFAMasterforHR> InputParam);

        Envelope<DataTable> ManageSFADetails(List<EncryptionInput> InputParam);
    }
}
