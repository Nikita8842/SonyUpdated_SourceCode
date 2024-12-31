using AMBOModels.Modules;
using APIAccessLayer.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAccessLayer.INTERFACE
{
    public interface IAPIDealerMasterCodeUpdate
    {
        Envelope<bool> UpdateDealerMasterCode(DealerMasterCodeUpdate objFormData);

        Envelope<bool> ValidateDealerMasterCode(DealerMasterCodeUpdate objFormData);

        List<Dealerdetails> GetDealerByMasterCode(DealerMasterCodeUpdate InputParam);

        List<string> GetDealerMasterCodeList();
    }
}
