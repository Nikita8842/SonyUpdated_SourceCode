using AMBOModels.MasterMaintenance;
using APIAccessLayer.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAccessLayer.INTERFACE
{
    public interface IAPIIncentiveCalculationDateSetting
    {
        Envelope<IncentiveCalculationDateSettingList> GetIncentiveCalculationDateSetting(IncentiveCalculationDateSettingParam objFormData);
        Envelope<bool> UpdateIncentiveCalculationDateSetting(IncentiveCalculationDateSetting objFormData);
    }
}
