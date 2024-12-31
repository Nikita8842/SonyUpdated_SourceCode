using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmboLibrary.MPRIntegration;
using AmboUtilities;

namespace AmboServices.Interface
{
    public interface IMPRIntegrationService
    {
        Envelope<IEnumerable<SalesThroughQuantityOutput>> GetSalesThroughQuantity(SalesThroughQuantityInput InputParam);

        Envelope<IEnumerable<GetSFADetailsBySFACodeOutput>> GetSFADetailsBySFACode(GetSFADetailsBySFACodeInput InputParam);

        Envelope<IEnumerable<GetBranchDivisionWise_SFACountOutput>> GetBranchDivisionWise_SFACount(GetBranchDivisionWise_SFACountInput InputParam);
    }
}
