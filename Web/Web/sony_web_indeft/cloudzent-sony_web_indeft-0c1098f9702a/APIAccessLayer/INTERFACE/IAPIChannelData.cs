using AMBOModels.MasterMaintenance;
using APIAccessLayer.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAccessLayer.INTERFACE
{
    public interface IAPIChannelData
    {
        Envelope<bool> CreateChannel(ChannelMaster objFormData);

        Envelope<bool> UpdateChannel(ChannelMaster objFormData);

        Envelope<bool> DeleteChannel(ChannelMaster objFormData);
    }
}
