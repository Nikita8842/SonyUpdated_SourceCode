using AmboLibrary.IncentiveManagement;
using AmboUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboServices.Interface
{
    public interface IIncentiveCalculationDateSettingService
    {
        Envelope<IncentiveCalculationDateSettingList> GetIncentiveCalculationDateSettingReport(IncentiveCalculationDateSettingParam Input);
        Envelope<bool> UpdateIncentiveCalculationDateSetting(IncentiveCalculationDateSetting incentiveCalculationDateSetting);
    }
}
