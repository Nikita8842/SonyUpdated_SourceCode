using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmboLibrary.UserManagement;
using AmboLibrary.GlobalAccessible;
using AmboLibrary.Mappings;
using System.Data;

namespace AmboProvider.Interface
{
    public interface IUserManagementProvider
    {
        int CreateEmployee(EmployeeFormData List, out string Message);
        int UpdateEmployee(EmployeeFormData List, out string Message);
        int DeleteEmployee(EmployeeFormData List, out string Message);

        int CreateSFA(SFAFormData List, out string Message);
        int UpdateSFA(SFAFormData List, out string Message);
        int DeleteSFA(SFAFormData List, out string Message);
        SFACodeAutoGenerate GetSFACode();


        DealerSFAMapping GetSFADetails(string LoginId);

        GetUserList GetUserList(GlobalPagination List);
        UserBasicProperties GetEmployeeById(Int64 ID);

        UserBasicProperties GetOfferedEmployeeById(Int64 ID);
        int ManageOfferedEmployee(SFAFormData List, out string Message);

        #region SFA Master for HR API
        IEnumerable<SFAMasterForHR> GetAllDetailsSFAMasterforHR();

        SFAMasterForHR GetSFAMasterforHRById(Int64 sfaMasterforHRId);

        int CreateSFAMasterforHR(SFAMasterForHR employeeForHR, out string Message);

        int UpdateSFAMasterforHR(SFAMasterForHR employeeForHR, out string Message);

        int DeleteSFAMasterforHR(SFAMasterforHRDelete employeeForHR, out string Message);

        IEnumerable<SFAMasterForHR> SFAMasterforHRDataDownload(SFAMasterforHRDownload InputParam);

        DataTable ManageSFAMasterforHRData(List<SFAMasterForHR> InputParam);

        DataTable ManageSFADetails(List<EncryptionInput> InputParam);
        #endregion SFA Master for HR API API

        MenusList GetModuleRightsByRole(ModuleRightsByRoleInput ModuleRightsByRoleInput, out string Message);
    }
}
