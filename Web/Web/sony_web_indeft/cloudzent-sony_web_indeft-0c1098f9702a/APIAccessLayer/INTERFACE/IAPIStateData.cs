using AMBOModels.MasterMaintenance;
using APIAccessLayer.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAccessLayer.INTERFACE
{
    public interface IAPIStateData
    {
        Envelope<bool> CreateState(CreateStateForm objFormData);

        Envelope<bool> UpdateState(UpdateStateForm objFormData);

        Envelope<bool> DeleteState(DeleteStateForm objFormData);
    }
}
