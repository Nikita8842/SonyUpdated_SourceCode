using AMBOModels.MasterMaintenance;
using APIAccessLayer.Helper;

namespace APIAccessLayer.INTERFACE
{
    public interface IAPILocationData
    {
        Envelope<bool> CreateLocation(CreateLocationForm objFormData);

        Envelope<bool> UpdateLocation(UpdateLocationForm objFormData);

        Envelope<bool> DeleteLocation(DeleteLocationForm objFormData);
    }
}
