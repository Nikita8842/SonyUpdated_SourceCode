using AmboDataServices.Interface;
using AmboLibrary.IncentiveManagement;
using AmboProvider.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboDataServices.Implimentation
{
    public class IncentiveCalculationDateSettingDataService : IIncentiveCalculationDateSettingDataService
    {
        private readonly IIncentiveCalculationDateSettingProvider _IIncentiveCalculationDateSettingProvider;

        public IncentiveCalculationDateSettingDataService(IIncentiveCalculationDateSettingProvider IIncentiveCalculationDateSettingProvider)
        {
            _IIncentiveCalculationDateSettingProvider = IIncentiveCalculationDateSettingProvider;
        }
        public IncentiveCalculationDateSettingList GetIncentiveCalculationDateSettingReport(IncentiveCalculationDateSettingParam Input)
        {
            return _IIncentiveCalculationDateSettingProvider.GetIncentiveCalculationDateSettingReport(Input);
        }
        public bool UpdateIncentiveCalculationDateSetting(IncentiveCalculationDateSetting incentiveCalculationDateSetting, out string Message)
        {
            return Convert.ToBoolean(_IIncentiveCalculationDateSettingProvider.UpdateIncentiveCalculationDateSetting(incentiveCalculationDateSetting, out Message));
        }

    }
}
