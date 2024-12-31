using AmboLibrary.IncentiveManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboDataServices.Interface
{
    public interface IIncentiveCalculationDateSettingDataService
    {
        IncentiveCalculationDateSettingList GetIncentiveCalculationDateSettingReport(IncentiveCalculationDateSettingParam Input);
        bool UpdateIncentiveCalculationDateSetting(IncentiveCalculationDateSetting incentiveCalculationDateSetting, out string Message);
    }
}
