using System;
using System.Collections.Generic;
using System.Linq;
using AmboServices.Interface;
using AmboDataServices.Interface;
using AmboLibrary.UserManagement;
using AmboLibrary.GlobalAccessible;
using AmboUtilities.Helper;
using AmboUtilities;
using AmboLibrary.Mappings;
using System.Data;

namespace AmboServices.Implimentation
{
    /// <summary>
    /// UserManagement Service
    /// </summary>
    public class UserManagementService : IUserManagementService
    {
        private readonly IUserManagementDataService _IUserManagementDataService;

        public UserManagementService(IUserManagementDataService UserManagementDataService)
        {
            _IUserManagementDataService = UserManagementDataService;
        }

        public Envelope<bool> CreateEmployee(EmployeeFormData List)
        {
            var Message = string.Empty;
            var IsInsert = _IUserManagementDataService.CreateEmployee(List, out Message);

            return (IsInsert != false) ?
                new Envelope<bool> { Data = IsInsert, MessageCode = (int)Acceptable.Created, Message = Message } :
                new Envelope<bool> { Data = IsInsert, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }

        public Envelope<GetUserList> GetUserList(GlobalPagination List)
        {
            var getlist = _IUserManagementDataService.GetUserList(List);

            return (getlist.List != null) ?
                new Envelope<GetUserList> { Data = getlist, MessageCode = (int)Acceptable.Found, Message = "List of Registered User" } :
                new Envelope<GetUserList> { Data = getlist, MessageCode = (int)NotAcceptable.NotFound, Message = "None of the User Registered Yet" };
        }

        public Envelope<bool> UpdateEmployee(EmployeeFormData List)
        {
            var Message = string.Empty;
            var IsUpdate = _IUserManagementDataService.UpdateEmployee(List, out Message);

            return (IsUpdate != false) ?
                new Envelope<bool> { Data = IsUpdate, MessageCode = (int)Acceptable.Created, Message = Message } :
                new Envelope<bool> { Data = IsUpdate, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }

        public Envelope<bool> DeleteEmployee(EmployeeFormData List)
        {
            var Message = string.Empty;
            var IsDelete = _IUserManagementDataService.DeleteEmployee(List, out Message);

            return (IsDelete != false) ?
                new Envelope<bool> { Data = IsDelete, MessageCode = (int)Acceptable.Created, Message = Message } :
                new Envelope<bool> { Data = IsDelete, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }

        public Envelope<UserBasicProperties> GetEmployeeById(Int64 ID)
        {
            var output = _IUserManagementDataService.GetEmployeeById(ID);
            return (output == null ) ?
                new Envelope<UserBasicProperties> { Data = new UserBasicProperties(), MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<UserBasicProperties> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Employee's data fetched successfully." };
        }

        public Envelope<bool> CreateSFA(SFAFormData List)
        {
            var Message = string.Empty;
            var IsInsert = _IUserManagementDataService.CreateSFA(List, out Message);

            return (IsInsert != false) ?
                new Envelope<bool> { Data = IsInsert, MessageCode = (int)Acceptable.Created, Message = Message } :
                new Envelope<bool> { Data = IsInsert, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }

        public Envelope<bool> UpdateSFA(SFAFormData List)
        {
            var Message = string.Empty;
            var IsUpdate = _IUserManagementDataService.UpdateSFA(List, out Message);

            return (IsUpdate != false) ?
                new Envelope<bool> { Data = IsUpdate, MessageCode = (int)Acceptable.Created, Message = Message } :
                new Envelope<bool> { Data = IsUpdate, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }

        public Envelope<bool> DeleteSFA(SFAFormData List)
        {
            var Message = string.Empty;
            var IsDelete = _IUserManagementDataService.DeleteSFA(List, out Message);

            return (IsDelete != false) ?
                new Envelope<bool> { Data = IsDelete, MessageCode = (int)Acceptable.Created, Message = Message } :
                new Envelope<bool> { Data = IsDelete, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }

        public Envelope<SFACodeAutoGenerate> GetSFACode()
        {
            var output = _IUserManagementDataService.GetSFACode();
            return (output == null) ?
                new Envelope<SFACodeAutoGenerate> { Data = new SFACodeAutoGenerate(), MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<SFACodeAutoGenerate> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Sfa Code fetched successfully." };
        }

        public Envelope<UserBasicProperties> GetOfferedEmployeeById(Int64 ID)
        {
            var output = _IUserManagementDataService.GetOfferedEmployeeById(ID);
            return (output == null) ?
                new Envelope<UserBasicProperties> { Data = new UserBasicProperties(), MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<UserBasicProperties> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Employee's data fetched successfully." };
        }

        public Envelope<bool> ManageOfferedEmployee(SFAFormData List)
        {
            var Message = string.Empty;
            var IsInsert = _IUserManagementDataService.ManageOfferedEmployee(List, out Message);

            return (IsInsert != false) ?
                new Envelope<bool> { Data = IsInsert, MessageCode = (int)Acceptable.Created, Message = Message } :
                new Envelope<bool> { Data = IsInsert, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }

        #region SFA Master for HR API
        public Envelope<IEnumerable<SFAMasterForHR>> GetAllDetailsSFAMasterforHR()
        {            
            var list = _IUserManagementDataService.GetAllDetailsSFAMasterforHR();

            return (list == null || list.Count() > 0) ?
                new Envelope<IEnumerable<SFAMasterForHR>> { Data = list, MessageCode = (int)NotAcceptable.NotFound, Message = "No data found." } :
                new Envelope<IEnumerable<SFAMasterForHR>> { Data = list, MessageCode = (int)Acceptable.Found, Message = "SFA Master for HR data fetched successfully." };
        }

        public Envelope<SFAMasterForHR> GetSFAMasterforHRById(Int64 sfaMasterforHRId)
        {
            var output = _IUserManagementDataService.GetSFAMasterforHRById(sfaMasterforHRId);

            return (output == null) ?
                new Envelope<SFAMasterForHR> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No data found." } :
                new Envelope<SFAMasterForHR> { Data = output, MessageCode = (int)Acceptable.Found, Message = "SFA Master for HR data fetched successfully." };
        }

        public Envelope<bool> CreateSFAMasterforHR(SFAMasterForHR employeeForHR)
        {
            var Message = string.Empty;
            var IsInsert = _IUserManagementDataService.CreateSFAMasterforHR(employeeForHR, out Message);

            return (IsInsert != false) ?
                new Envelope<bool> { Data = IsInsert, MessageCode = (int)Acceptable.Created, Message = Message } :
                new Envelope<bool> { Data = IsInsert, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }

        public Envelope<bool> UpdateSFAMasterforHR(SFAMasterForHR employeeForHR)
        {
            var Message = string.Empty;
            var IsUpdate = _IUserManagementDataService.UpdateSFAMasterforHR(employeeForHR, out Message);

            return (IsUpdate != false) ?
                new Envelope<bool> { Data = IsUpdate, MessageCode = (int)Acceptable.Found, Message = Message } :
                new Envelope<bool> { Data = IsUpdate, MessageCode = (int)NotAcceptable.NotModified, Message = Message };
        }

        public Envelope<bool> DeleteSFAMasterforHR(SFAMasterforHRDelete employeeForHR)
        {
            var Message = string.Empty;
            var IsUpdate = _IUserManagementDataService.DeleteSFAMasterforHR(employeeForHR, out Message);

            return (IsUpdate != false) ?
                new Envelope<bool> { Data = IsUpdate, MessageCode = (int)Acceptable.Found, Message = Message } :
                new Envelope<bool> { Data = IsUpdate, MessageCode = (int)NotAcceptable.NotFound, Message = Message };
        }

        public Envelope<DealerSFAMapping> GetSFADetails(string LoginId)
        {
            var output = _IUserManagementDataService.GetSFADetails(LoginId);

            return (output == null) ?
                new Envelope<DealerSFAMapping> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No data found." } :
                new Envelope<DealerSFAMapping> { Data = output, MessageCode = (int)Acceptable.Found, Message = "SFA data fetched successfully." };
        }

        public Envelope<IEnumerable<SFAMasterForHR>> SFAMasterforHRDataDownload(SFAMasterforHRDownload InputParam)
        {
            var list = _IUserManagementDataService.SFAMasterforHRDataDownload(InputParam);

            return (list == null ) ?
                new Envelope<IEnumerable<SFAMasterForHR>> { Data = list, MessageCode = (int)NotAcceptable.NotFound, Message = "No data found." } :
                new Envelope<IEnumerable<SFAMasterForHR>> { Data = list, MessageCode = (int)Acceptable.Found, Message = "SFA Master for HR data fetched successfully." };
        }

        public Envelope<DataTable> ManageSFAMasterforHRData(List<SFAMasterForHR> InputParam)
        {
            var output = _IUserManagementDataService.ManageSFAMasterforHRData(InputParam);

            return (output != null && output.Rows.Count > 0) ?
                new Envelope<DataTable> { Data = output, MessageCode = (int)Acceptable.Created, Message = "Full/Partial data uploaded successfully." } :
                new Envelope<DataTable> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = "Error occured while uploading data." };
        }

        public Envelope<DataTable> ManageSFADetails(List<EncryptionInput> InputParam)
        {
            var output = _IUserManagementDataService.ManageSFADetails(InputParam);

            return (output != null && output.Rows.Count > 0) ?
                new Envelope<DataTable> { Data = output, MessageCode = (int)Acceptable.Created, Message = "Full/Partial data uploaded successfully." } :
                new Envelope<DataTable> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = "Error occured while uploading data." };
        }
        #endregion SFA Master for HR API

        public Envelope<MenusList> GetModuleRightsByRole(ModuleRightsByRoleInput ModuleRightsByRoleInput)
        {
            string Message = "";
            var output = _IUserManagementDataService.GetModuleRightsByRole(ModuleRightsByRoleInput, out Message);

            return (output != null) ?
                new Envelope<MenusList> { Data = output, MessageCode = (int)Acceptable.Found, Message = Message} :
                new Envelope<MenusList> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = Message };
        }
    }
}
