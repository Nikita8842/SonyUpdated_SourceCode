using AmboDataServices.Interface;
using AmboLibrary.IncentiveManagement;
using AmboServices.Interface;
using AmboUtilities;
using AmboUtilities.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboServices.Implimentation
{
    public class IncentiveCalculationDateSettingService : IIncentiveCalculationDateSettingService
    {

        private readonly IIncentiveCalculationDateSettingDataService _IIncentiveCalculationDateSettingDataService;

        public IncentiveCalculationDateSettingService(IIncentiveCalculationDateSettingDataService IIncentiveCalculationDateSettingDataService)
        {
            _IIncentiveCalculationDateSettingDataService = IIncentiveCalculationDateSettingDataService;
        }

        public Envelope<IncentiveCalculationDateSettingList> GetIncentiveCalculationDateSettingReport(IncentiveCalculationDateSettingParam Input)
        {
            var output = _IIncentiveCalculationDateSettingDataService.GetIncentiveCalculationDateSettingReport(Input);
            return (output == null || output.IncentiveCalculationDateSettingData.Count() == 0) ?
                new Envelope<IncentiveCalculationDateSettingList> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IncentiveCalculationDateSettingList> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Incentive Calculation date data fetched successfully." };
        }
        public Envelope<bool> UpdateIncentiveCalculationDateSetting(IncentiveCalculationDateSetting incentiveCalculationDateSetting)
        {
            var Message = string.Empty;
            var output = _IIncentiveCalculationDateSettingDataService.UpdateIncentiveCalculationDateSetting(incentiveCalculationDateSetting, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Accepted, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotModified, Message = Message };
        }
    }
}
