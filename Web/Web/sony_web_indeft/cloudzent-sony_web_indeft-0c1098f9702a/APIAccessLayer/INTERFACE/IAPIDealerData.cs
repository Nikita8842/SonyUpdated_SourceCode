using AMBOModels.MasterMaintenance;
using AMBOModels.GlobalAccessible;
using APIAccessLayer.Helper;

namespace APIAccessLayer.INTERFACE
{
    public interface IAPIDealerData
    {
        DealerMaster GetDealerById(DealerMaster InputParam);
        Envelope<PayerDetails> GetDealerCode(PayerDetails InputParam);

        Envelope<bool> InsertDealer(DealerMaster InputParam);
        Envelope<bool> UpdateDealer(DealerMaster InputParam);
        Envelope<bool> DeleteDealer(DealerMaster InputParam);
        Envelope<bool> CheckPSIOutlet(DealerMaster InputParam);
    }
}
