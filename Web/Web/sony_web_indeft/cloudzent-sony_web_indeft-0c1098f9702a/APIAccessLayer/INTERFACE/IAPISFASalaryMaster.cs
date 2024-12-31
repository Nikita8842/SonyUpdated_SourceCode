using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMBOModels.MasterMaintenance;
using APIAccessLayer.Helper;
using System.Data;

namespace APIAccessLayer.INTERFACE
{
    public interface IAPISFASalaryMaster
    {
        Envelope<bool> UpdateSFASalaryMaster(SFASalaryMasterGrid objFormData);

        Envelope<bool> DeleteSFASalaryMaster(SFASalaryMasterDelete objFormData);

        Envelope<bool> CreateSFASalaryMaster(SFASalaryMasterGrid objFormData);

        SFASalaryMasterGrid GetSFASalaryMasterbyId(SFASalaryMasterGrid InputParam);

        List<SFASalaryMasterGrid> SFASalaryMasterDataDownload(SFASalaryMasterDownload InputParam);

        Envelope<DataTable> ManageSFASalaryMasterData(List<SFASalaryMasterGrid> InputParam);
    }
}
