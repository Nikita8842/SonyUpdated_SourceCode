using AMBOModels.UserManagement;
using APIAccessLayer.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAccessLayer.INTERFACE
{
    public interface IAPIEmployeeData
    {
        Envelope<bool> CreateEmployee(EmployeeFormData objFormData);

        Envelope<bool> UpdateEmployee(EmployeeFormData objFormData);

        EmployeeFormData GetEmployeeById(Int64? ID);

        Envelope<bool> DeleteEmployee(EmployeeFormData objFormData);

        Envelope<IEnumerable<OfferedEmployeeGridData>> GetOfferedEmployeeMasterGrid(OfferedEmployeeGridSearchFilters objGridVariables);

        Envelope<MenusList> GetModuleRightsByRole(ModuleRightsByRoleInput ModuleRightsByRoleInput);

    }
}
