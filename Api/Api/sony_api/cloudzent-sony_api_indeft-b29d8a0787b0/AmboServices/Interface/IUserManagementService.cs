using AmboLibrary.UserManagement;
using AmboLibrary.GlobalAccessible;
using AmboUtilities;
using System.Collections.Generic;
using System;
using AmboLibrary.Mappings;
using System.Data;

namespace AmboServices.Interface
{
    public interface IUserManagementService
    {
        Envelope<bool> CreateEmployee(EmployeeFormData List);
        Envelope<bool> UpdateEmployee(EmployeeFormData List);
        Envelope<bool> DeleteEmployee(EmployeeFormData List);
        Envelope<bool> CreateSFA(SFAFormData List);
        Envelope<bool> UpdateSFA(SFAFormData List);
        Envelope<bool> DeleteSFA(SFAFormData List);
        Envelope<SFACodeAutoGenerate> GetSFACode();
        Envelope<GetUserList> GetUserList(GlobalPagination List);
        Envelope<UserBasicProperties> GetEmployeeById(Int64 ID);

        Envelope<DealerSFAMapping> GetSFADetails(string LoginId);
        Envelope<UserBasicProperties> GetOfferedEmployeeById(Int64 ID);
        Envelope<bool> ManageOfferedEmployee(SFAFormData List);

        #region SFA Master for HR Service Methods
        Envelope<IEnumerable<SFAMasterForHR>> GetAllDetailsSFAMasterforHR();

        Envelope<SFAMasterForHR> GetSFAMasterforHRById(Int64 sfaMasterforHRId);

        Envelope<bool> CreateSFAMasterforHR(SFAMasterForHR employeeForHR);

        Envelope<bool> DeleteSFAMasterforHR(SFAMasterforHRDelete employeeForHR);

        Envelope<bool> UpdateSFAMasterforHR(SFAMasterForHR employeeForHR);

        Envelope<IEnumerable<SFAMasterForHR>> SFAMasterforHRDataDownload(SFAMasterforHRDownload InputParam);

        Envelope<DataTable> ManageSFAMasterforHRData(List<SFAMasterForHR> InputParam);

        Envelope<DataTable> ManageSFADetails(List<EncryptionInput> InputParam);
        #endregion SFA Master for HR Service Methods

        Envelope<MenusList> GetModuleRightsByRole(ModuleRightsByRoleInput ModuleRightsByRoleInput);
    }
}
