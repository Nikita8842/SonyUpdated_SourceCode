using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmboDataServices.Interface;
using AmboLibrary.MPRIntegration;
using AmboProvider.Interface;

namespace AmboDataServices.Implimentation
{
    public class MPRIntegrationDataService : IMPRIntergrationDataService
    {
        private readonly IMPRIntegration _IMPRIntegration;

        public MPRIntegrationDataService(IMPRIntegration IMPRIntegration)
        {
            _IMPRIntegration = IMPRIntegration;
        }        

        public IEnumerable<SalesThroughQuantityOutput> GetSalesThroughQuantity(SalesThroughQuantityInput InputParam, out string Message)
        {
            return _IMPRIntegration.GetSalesThroughQuantity(InputParam, out Message);
        }

        public IEnumerable<GetSFADetailsBySFACodeOutput> GetSFADetailsBySFACode(GetSFADetailsBySFACodeInput InputParam, out string Message)
        {
            return _IMPRIntegration.GetSFADetailsBySFACode(InputParam, out Message);
        }

        public IEnumerable<GetBranchDivisionWise_SFACountOutput> GetBranchDivisionWise_SFACount(GetBranchDivisionWise_SFACountInput InputParam, out string Message)
        {
            return _IMPRIntegration.GetBranchDivisionWise_SFACount(InputParam, out Message);
        }
    }
}
