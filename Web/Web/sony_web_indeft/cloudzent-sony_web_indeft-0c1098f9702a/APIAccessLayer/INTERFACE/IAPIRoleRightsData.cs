using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMBOModels.MasterMaintenance;
using APIAccessLayer.Helper;

namespace APIAccessLayer.INTERFACE
{
    public interface IAPIRoleRightsData
    {
        Envelope<bool> CreateRoleRightsMapping(RoleRightsMappingMaster rolerightsmappingData);

        Envelope<bool> UpdateRoleRightsMapping(RoleRightsMappingMaster rolerightsmappingData);
    }
}
