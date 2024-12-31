using AMBOModels.MasterMaintenance;
using APIAccessLayer.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAccessLayer.INTERFACE
{
    public interface IAPIBroadcastMessage
    {
        Envelope<CreateBroadcastMessageFormOutput> CreateBroadcastMessage(CreateBroadcastMessageForm objBroadcastData);

        Envelope<bool> InActiveBroadcastMessage(CreateBroadcastMessageForm objFormData);
    }
}
