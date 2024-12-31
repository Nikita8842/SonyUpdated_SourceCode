using System;
using AmboLibrary.UserManagement;
using AmboLibrary.GlobalAccessible;
using AmboProvider.Interface;
using AmboDataServices.Interface;
using System.Collections.Generic;
using AmboLibrary.Mappings;
using System.Data;
//using AmboLibrary.GlobalAccessible;

namespace AmboDataServices.Implimentation
{
    public class UserManagementDataService : IUserManagementDataService
    {
        private readonly IUserManagementProvider _IUserManagementProvider;
        public UserManagementDataService(IUserManagementProvider IUserManagementProvider)
        {
            _IUserManagementProvider = IUserManagementProvider;
        }       

        public bool CreateEmployee(EmployeeFormData List, out string Message)
        {
            return Convert.ToBoolean(_IUserManagementProvider.CreateEmployee(List, out Message));
        }        

        public GetUserList GetUserList(GlobalPagination List)
        {
            return (_IUserManagementProvider.GetUserList(List));
        }
        
        public bool UpdateEmployee(EmployeeFormData List, out string Message)
        {
            return Convert.ToBoolean(_IUserManagementProvider.UpdateEmployee(List, out Message));
        }

        public UserBasicProperties GetEmployeeById(Int64 ID)
        {
            return _IUserManagementProvider.GetEmployeeById(ID);
        }        
        
        public bool DeleteEmployee(EmployeeFormData List, out string Message)
        {
            return Convert.ToBoolean(_IUserManagementProvider.DeleteEmployee(List, out Message));
        }

        public bool CreateSFA(SFAFormData List, out string Message)
        {
            return Convert.ToBoolean(_IUserManagementProvider.CreateSFA(List, out Message));
        }

        public bool UpdateSFA(SFAFormData List, out string Message)
        {
            return Convert.ToBoolean(_IUserManagementProvider.UpdateSFA(List, out Message));
        }

        public bool DeleteSFA(SFAFormData List, out string Message)
        {
            return Convert.ToBoolean(_IUserManagementProvider.DeleteSFA(List, out Message));
        }

        public SFACodeAutoGenerate GetSFACode()
        {
            return _IUserManagementProvider.GetSFACode();
        }

        public UserBasicProperties GetOfferedEmployeeById(Int64 ID)
        {
            return _IUserManagementProvider.GetOfferedEmployeeById(ID);
        }

        public bool ManageOfferedEmployee(SFAFormData List, out string Message)
        {
            return Convert.ToBoolean(_IUserManagementProvider.ManageOfferedEmployee(List, out Message));
        }

        #region SFA Master for HR API
        public IEnumerable<SFAMasterForHR> GetAllDetailsSFAMasterforHR()
        {
            return _IUserManagementProvider.GetAllDetailsSFAMasterforHR();
        }

        public SFAMasterForHR GetSFAMasterforHRById(Int64 sfaMasterforHRId)
        {
            return _IUserManagementProvider.GetSFAMasterforHRById(sfaMasterforHRId);
        }

        public bool CreateSFAMasterforHR(SFAMasterForHR employeeForHR, out string Message)
        {
            return Convert.ToBoolean(_IUserManagementProvider.CreateSFAMasterforHR(employeeForHR, out Message));
        }

        public bool UpdateSFAMasterforHR(SFAMasterForHR employeeForHR, out string Message)
        {
            return Convert.ToBoolean(_IUserManagementProvider.UpdateSFAMasterforHR(employeeForHR, out Message));
        }

        public bool DeleteSFAMasterforHR(SFAMasterforHRDelete employeeForHR, out string Message)
        {
            return Convert.ToBoolean(_IUserManagementProvider.DeleteSFAMasterforHR(employeeForHR, out Message));
        }

        public DealerSFAMapping GetSFADetails(string LoginId)
        {
            return _IUserManagementProvider.GetSFADetails(LoginId);
        }

        public IEnumerable<SFAMasterForHR> SFAMasterforHRDataDownload(SFAMasterforHRDownload InputParam)
        {
            return _IUserManagementProvider.SFAMasterforHRDataDownload(InputParam);
        }

        public DataTable ManageSFAMasterforHRData(List<SFAMasterForHR> InputParam)
        {
            return _IUserManagementProvider.ManageSFAMasterforHRData(InputParam);
        }

        public DataTable ManageSFADetails(List<EncryptionInput> InputParam)
        {
            return _IUserManagementProvider.ManageSFADetails(InputParam);
        }
        #endregion SFA Master for HR API

        public MenusList GetModuleRightsByRole(ModuleRightsByRoleInput ModuleRightsByRoleInput, out string Message)
        {
            return _IUserManagementProvider.GetModuleRightsByRole(ModuleRightsByRoleInput,out Message);
        }
    }
}
