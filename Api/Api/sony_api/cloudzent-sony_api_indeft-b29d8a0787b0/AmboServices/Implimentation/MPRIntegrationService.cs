using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmboLibrary.MPRIntegration;
using AmboUtilities;
using AmboServices.Interface;
using AmboDataServices.Interface;
using AmboUtilities.Helper;

namespace AmboServices.Implimentation
{
    public class MPRIntegrationService : IMPRIntegrationService
    {
        private readonly IMPRIntergrationDataService _IMPRIntergrationDataService;
        public MPRIntegrationService(IMPRIntergrationDataService IMPRIntergrationDataService)
        {
            _IMPRIntergrationDataService = IMPRIntergrationDataService;
        }        

        public Envelope<IEnumerable<SalesThroughQuantityOutput>> GetSalesThroughQuantity(SalesThroughQuantityInput InputParam)
        {
            try
            {
                string Message = "";
                var output = _IMPRIntergrationDataService.GetSalesThroughQuantity(InputParam, out Message);
                return (output == null) ?
                    new Envelope<IEnumerable<SalesThroughQuantityOutput>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = Message } :
                    new Envelope<IEnumerable<SalesThroughQuantityOutput>> { Data = output, MessageCode = (int)Acceptable.Found, Message = Message };
            }
            catch(Exception ex)
            {
                return new Envelope<IEnumerable<SalesThroughQuantityOutput>> { Data = null, MessageCode = (int)NotAcceptable.BadRequest, Message = "" };
            }
        }

        public Envelope<IEnumerable<GetSFADetailsBySFACodeOutput>> GetSFADetailsBySFACode(GetSFADetailsBySFACodeInput InputParam)
        {
            try
            {
                string Message = "";
                var output = _IMPRIntergrationDataService.GetSFADetailsBySFACode(InputParam, out Message);
                return (output == null) ?
                    new Envelope<IEnumerable<GetSFADetailsBySFACodeOutput>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = Message } :
                    new Envelope<IEnumerable<GetSFADetailsBySFACodeOutput>> { Data = output, MessageCode = (int)Acceptable.Found, Message = Message };
            }
            catch (Exception ex)
            {
                return new Envelope<IEnumerable<GetSFADetailsBySFACodeOutput>> { Data = null, MessageCode = (int)NotAcceptable.BadRequest, Message = "" };
            }
        }

        public Envelope<IEnumerable<GetBranchDivisionWise_SFACountOutput>> GetBranchDivisionWise_SFACount(GetBranchDivisionWise_SFACountInput InputParam)
        {
            try
            {
                string Message = "";
                var output = _IMPRIntergrationDataService.GetBranchDivisionWise_SFACount(InputParam, out Message);
                return (output == null) ?
                    new Envelope<IEnumerable<GetBranchDivisionWise_SFACountOutput>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = Message } :
                    new Envelope<IEnumerable<GetBranchDivisionWise_SFACountOutput>> { Data = output, MessageCode = (int)Acceptable.Found, Message = Message };
            }
            catch (Exception ex)
            {
                return new Envelope<IEnumerable<GetBranchDivisionWise_SFACountOutput>> { Data = null, MessageCode = (int)NotAcceptable.BadRequest, Message = "" };
            }
        }
    }
}
