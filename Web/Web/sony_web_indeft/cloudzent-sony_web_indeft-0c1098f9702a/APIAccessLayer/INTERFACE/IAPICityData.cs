using AMBOModels.MasterMaintenance;
using APIAccessLayer.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAccessLayer.INTERFACE
{
    public interface IAPICityData
    {
        Envelope<bool> CreateCity(CreateCityForm objFormData);

        Envelope<bool> UpdateCity(UpdateCityForm objFormData);

        Envelope<bool> DeleteCity(DeleteCityForm objFormData);

        List<CityMaster> GetAllCities();
    }
}
