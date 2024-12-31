using AmboLibrary.IncentiveManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboProvider.Interface
{
    public interface IIncentiveCalculationDateSettingProvider
    {
        IncentiveCalculationDateSettingList GetIncentiveCalculationDateSettingReport(IncentiveCalculationDateSettingParam Input);
        int UpdateIncentiveCalculationDateSetting(IncentiveCalculationDateSetting IncentiveCalculationDateSetting, out string Message);
    }
}
