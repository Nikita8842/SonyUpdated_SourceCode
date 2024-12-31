using System.Collections.Generic;
using System;
using AmboLibrary.Mappings;
using AmboLibrary.UserManagement;
using AmboLibrary.GlobalAccessible;
using System.Data;

namespace AmboDataServices.Interface
{
    public interface IUserManagementDataService
    {
        bool CreateEmployee(EmployeeFormData List, out string Message);
        bool UpdateEmployee(EmployeeFormData List, out string Message);
        bool DeleteEmployee(EmployeeFormData List, out string Message);

        bool CreateSFA(SFAFormData List, out string Message);
        bool UpdateSFA(SFAFormData List, out string Message);
        bool DeleteSFA(SFAFormData List, out string Message);
        SFACodeAutoGenerate GetSFACode();
        GetUserList GetUserList(GlobalPagination List);
        UserBasicProperties GetEmployeeById(Int64 ID);

        DealerSFAMapping GetSFADetails(string LoginId);
        UserBasicProperties GetOfferedEmployeeById(Int64 ID);

        bool ManageOfferedEmployee(SFAFormData List, out string Message);

        #region SFA Master for HR Data Service Methods
        IEnumerable<SFAMasterForHR> GetAllDetailsSFAMasterforHR();

        SFAMasterForHR GetSFAMasterforHRById(Int64 sfaMasterforHRId);

        bool CreateSFAMasterforHR(SFAMasterForHR employeeForHR, out string Message);

        bool DeleteSFAMasterforHR(SFAMasterforHRDelete employeeForHR, out string Message);

        bool UpdateSFAMasterforHR(SFAMasterForHR employeeForHR, out string Message);

        IEnumerable<SFAMasterForHR> SFAMasterforHRDataDownload(SFAMasterforHRDownload InputParam);

        DataTable ManageSFAMasterforHRData(List<SFAMasterForHR> InputParam);

        DataTable ManageSFADetails(List<EncryptionInput> InputParam);
        #endregion SFA Master for HR Data Service Methods

        MenusList GetModuleRightsByRole(ModuleRightsByRoleInput ModuleRightsByRoleInput, out string Message);
    }
}
