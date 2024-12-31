using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using AmboServices.Interface;
using AmboUtilities;
using AmboLibrary.UserManagement;
using AmboLibrary.GlobalAccessible;
using AmboUtilities.Helper;


namespace SonyAmbo.Controllers
{
    /// <summary>
    /// Controller to Manage User for the Product
    /// Manbeer Singh Makhloga [06/03/2018]
    /// </summary>
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [ExceptionHandling]
    [Compression]
    public class UserManagementController : ApiController
    {
        private readonly IUserManagementService _IUserManagementService;

        /// <summary>
        /// Dependency injected controller
        /// </summary>
        /// <param name="UserManagementService"></param>
        /// <returns></returns>
        public UserManagementController(IUserManagementService UserManagementService)
        {
            _IUserManagementService = UserManagementService;
        }

        /// <summary>
        /// Method to Register Employee
        /// </summary>
        /// <param name="List"></param>
        /// <returns></returns>
        public IHttpActionResult CreateEmployee(Envelope<EmployeeFormData> List)
        {
            var getList = _IUserManagementService.CreateEmployee(List.Data);
            return Ok(getList);
        }

        /// <summary>
        /// Method to Register SFA
        /// </summary>
        /// <param name="List"></param>
        /// <returns></returns>
        public IHttpActionResult CreateSFA(Envelope<SFAFormData> List)
        {
            var getList = _IUserManagementService.CreateSFA(List.Data);
            return Ok(getList);
        }

        /// <summary>
        /// Method to Update Employee
        /// </summary>
        /// <param name="List"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult UpdateEmployee(Envelope<EmployeeFormData> List)
        {
            var getList= _IUserManagementService.UpdateEmployee(List.Data);
            return Ok(getList);
        }

        /// <summary>
        /// Method to Update SFA
        /// </summary>
        /// <param name="List"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult UpdateSFA(Envelope<SFAFormData> List)
        {
            var getList = _IUserManagementService.UpdateSFA(List.Data);
            return Ok(getList);
        }

        /// <summary>
        /// Method to Delete Employee
        /// </summary>
        /// <param name="List"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DeleteEmployee(Envelope<EmployeeFormData> List)
        {
            var getList = _IUserManagementService.DeleteEmployee(List.Data);
            return Ok(getList);
        }

        /// <summary>
        /// Method to Delete SFA
        /// </summary>
        /// <param name="List"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DeleteSFA(Envelope<SFAFormData> List)
        {
            var getList = _IUserManagementService.DeleteSFA(List.Data);
            return Ok(getList);
        }

        /// <summary>
        /// Method to auto generate SFA Code
        /// </summary>
        /// <param name="none"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetSFACode()
        {
            var getList = _IUserManagementService.GetSFACode();
            return Ok(getList);
        }

        /// <summary>
        /// Method to Get UserList
        /// UserId,EncryptId & RoleName and need to be pass
        /// PageIndex / PageSize if not passed will 
        /// share default Page 01 and Size 10
        /// </summary>
        /// <param name="List"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetUserList(Envelope<GlobalPagination> List)
        {
            var getList = _IUserManagementService.GetUserList(List.Data);
            return Ok(getList);
        }

        /// <summary>
        /// Method to Get Employee/SFA on basis of it's ID
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetEmployeeById(Envelope<Int64> param)
        {
            var output = _IUserManagementService.GetEmployeeById(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// Method to Get Offered Employee on basis of it's ID
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetOfferedEmployeeById(Envelope<Int64> param)
        {
            var output = _IUserManagementService.GetOfferedEmployeeById(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// Method to Get SFA on basis of it's LoginId
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetSFADetails(Envelope<string> param)
        {
            var output = _IUserManagementService.GetSFADetails(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// Method to Register offered Employee
        /// </summary>
        /// <param name="List"></param>
        /// <returns></returns>
        public IHttpActionResult ManageOfferedEmployee(Envelope<SFAFormData> List)
        {
            var getList = _IUserManagementService.ManageOfferedEmployee(List.Data);
            return Ok(getList);
        }

        #region SFA Master for HR API
        /// <summary>
        /// Get all SFA Master for HR saved in the system.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetAllDetailsSFAMasterforHR()
        {
            var output = _IUserManagementService.GetAllDetailsSFAMasterforHR();
            return Ok(output);
        }

        /// <summary>
        /// Get all data related to a specific SFA Master for HR on basis of it's ID.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetSFAMasterforHRById(Envelope<SFAMasterForHR> param)
        {
            var output = _IUserManagementService.GetSFAMasterforHRById(param.Data.LoginId);
            return Ok(output);
        }

        /// <summary>
        /// Create a new Employee for HR in the system.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult CreateSFAMasterforHR(Envelope<SFAMasterForHR> param)
        {
            var output = _IUserManagementService.CreateSFAMasterforHR(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// Delete a Employee for HR if it has no dependency in the system.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DeleteSFAMasterforHR(Envelope<SFAMasterforHRDelete> param)
        {
            var output = _IUserManagementService.DeleteSFAMasterforHR(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// Update an existing Employee for HR in the system.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult UpdateSFAMasterforHR(Envelope<SFAMasterForHR> param)
        {
            var output = _IUserManagementService.UpdateSFAMasterforHR(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// Get SFA Master for HR (download purpose)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult SFAMasterforHRDataDownload(Envelope<SFAMasterforHRDownload> param)
        {
            var output = _IUserManagementService.SFAMasterforHRDataDownload(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// insert SFA details from excel file
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult ManageSFAMasterforHRData(Envelope<List<SFAMasterForHR>> param)
        {
            var output = _IUserManagementService.ManageSFAMasterforHRData(param.Data);
            return Ok(output);
        }

        [HttpPost]
        public IHttpActionResult ManageSFADetails(Envelope<List<EncryptionInput>> param)
        {
            var output = _IUserManagementService.ManageSFADetails(param.Data);
            return Ok(output);
        }
        #endregion SFA Master for HR API

        /// <summary>
        /// Method to Get modules right
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetModuleRightsByRole(Envelope<ModuleRightsByRoleInput> param)
        {
            var output = _IUserManagementService.GetModuleRightsByRole(param.Data);
            return Ok(output);
        }

    }
}
