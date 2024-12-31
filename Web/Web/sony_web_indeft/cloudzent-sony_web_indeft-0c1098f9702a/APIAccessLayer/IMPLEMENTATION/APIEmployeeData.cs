using APIAccessLayer.INTERFACE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMBOModels.UserManagement;
using APIAccessLayer.Helper;
using Newtonsoft.Json;

namespace APIAccessLayer.IMPLEMENTATION
{
    public class APIEmployeeData : IAPIEmployeeData
    {
        public Envelope<bool> CreateEmployee(EmployeeFormData objFormData)
        {
            Envelope<EmployeeFormData> postData = new Envelope<EmployeeFormData>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<EmployeeFormData>> client;
            postData.Data = objFormData;
            try
            {
                client = new APIClient<Envelope<EmployeeFormData>>();
                string apiUrl = "api/UserManagement/CreateEmployee";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }

        public EmployeeFormData GetEmployeeById(Int64? ID)
        {
            Envelope<Int64?> postData = new Envelope<Int64?>();
            Envelope<EmployeeFormData> output = new Envelope<EmployeeFormData>();
            APIClient<Envelope<Int64?>> client;
            postData.Data = ID;
            try
            {
                client = new APIClient<Envelope<Int64?>>();
                string apiUrl = "api/UserManagement/GetEmployeeById";
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<EmployeeFormData>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output.Data;
        }

        public Envelope<bool> UpdateEmployee(EmployeeFormData objFormData)
        {
            Envelope<EmployeeFormData> postData = new Envelope<EmployeeFormData>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<EmployeeFormData>> client;
            postData.Data = objFormData;
            try
            {
                client = new APIClient<Envelope<EmployeeFormData>>();
                string apiUrl = "api/UserManagement/UpdateEmployee";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }

        public Envelope<bool> DeleteEmployee(EmployeeFormData objFormData)
        {
            Envelope<EmployeeFormData> postData = new Envelope<EmployeeFormData>();
            Envelope<bool> output = new Envelope<bool>();
            APIClient<Envelope<EmployeeFormData>> client;
            postData.Data = objFormData;
            try
            {
                client = new APIClient<Envelope<EmployeeFormData>>();
                string apiUrl = "api/UserManagement/DeleteEmployee";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<bool>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }

        public Envelope<IEnumerable<OfferedEmployeeGridData>> GetOfferedEmployeeMasterGrid(OfferedEmployeeGridSearchFilters objGridData)
        {
            Envelope<OfferedEmployeeGridSearchFilters> postData = new Envelope<OfferedEmployeeGridSearchFilters>();
            Envelope<IEnumerable<OfferedEmployeeGridData>> output;
            APIClient<Envelope<OfferedEmployeeGridSearchFilters>> client;
            postData.Data = objGridData;
            try
            {
                client = new APIClient<Envelope<OfferedEmployeeGridSearchFilters>>();
                string apiUrl = "api/Grid/GetOfferedEmployeeMasterGrid";//checked
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<IEnumerable<OfferedEmployeeGridData>>>(apiOutput.Result);
                return output;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Envelope<MenusList> GetModuleRightsByRole(ModuleRightsByRoleInput ModuleRightsByRoleInput)
        {
            Envelope<ModuleRightsByRoleInput> postData = new Envelope<ModuleRightsByRoleInput>();
            Envelope<MenusList> output = new Envelope<MenusList>();
            APIClient<Envelope<ModuleRightsByRoleInput>> client;
            postData.Data = ModuleRightsByRoleInput;
            try
            {
                client = new APIClient<Envelope<ModuleRightsByRoleInput>>();
                string apiUrl = "api/UserManagement/GetModuleRightsByRole";
                var apiOutput = client.Post(apiUrl, postData);
                output = JsonConvert.DeserializeObject<Envelope<MenusList>>(apiOutput.Result);
            }
            catch (Exception ex)
            {
            }
            return output;
        }
    }
}
