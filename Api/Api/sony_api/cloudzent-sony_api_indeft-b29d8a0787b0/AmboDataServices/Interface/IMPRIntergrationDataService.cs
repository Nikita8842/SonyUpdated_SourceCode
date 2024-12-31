using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmboLibrary.MPRIntegration;

namespace AmboDataServices.Interface
{
    public interface IMPRIntergrationDataService
    {
        IEnumerable<SalesThroughQuantityOutput> GetSalesThroughQuantity(SalesThroughQuantityInput InputParam, out string Message);

        IEnumerable<GetSFADetailsBySFACodeOutput> GetSFADetailsBySFACode(GetSFADetailsBySFACodeInput InputParam, out string Message);

        IEnumerable<GetBranchDivisionWise_SFACountOutput> GetBranchDivisionWise_SFACount(GetBranchDivisionWise_SFACountInput InputParam, out string Message);
    }
}
