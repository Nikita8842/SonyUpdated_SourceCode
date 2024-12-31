using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DBHelper;
using AmboUtilities.Helper;
using AmboProvider.Interface;
using AmboLibrary.UserManagement;
using AmboLibrary.GlobalAccessible;
using AmboLibrary.Mappings;
using System.Data.SqlClient;
using System.Globalization;
using AmboLibrary.MasterMaintenance;

namespace AmboProvider.Implimentation
{
    /// <summary>
    /// Provider for UserManagement
    /// Manbeer Singh Makhloga
    /// 08/03/2017
    /// </summary>
    public class UserManagementProvider : IUserManagementProvider
    {
        private readonly DbHelper _dataHelper;
        private readonly IErrorLogProvider _IErrorLogProvider;


        public UserManagementProvider(IErrorLogProvider IErrorLogProvider)
        {
            _dataHelper = DbHelper.GetInstance("defaultConnection");
            _IErrorLogProvider = IErrorLogProvider;
        }

        #region Employee Management
        public int CreateEmployee(EmployeeFormData List, out string Message)
        {
            Message = string.Empty;

            //if (!GlobalAccessible.ValidateSpecialCharacter(List.FirstName.Trim() + ';' + List.LastName.Trim()))
            //{
            //    Message = "Special Character  not allowed";
            //    return 0;
            //}

            DbParameterCollection sqlparam = new DbParameterCollection();
            sqlparam.Add(new DbParameter("@RoleId", List.RoleId, DbType.Int64));
            sqlparam.Add(new DbParameter("@LoginId", List.LoginId.Trim(), DbType.String));
            sqlparam.Add(new DbParameter("@Password", AMBOEcryption.GetHashValue(List.Password.Trim()), DbType.String));
            sqlparam.Add(new DbParameter("@FirstName", List.FirstName.Trim(), DbType.String));
            sqlparam.Add(new DbParameter("@LastName", List.LastName.Trim(), DbType.String));
            sqlparam.Add(new DbParameter("@MailId", (List.EmailId == null)?"":AMBOEcryption.EncryptData(List.EmailId.Trim(),true), DbType.String));
            sqlparam.Add(new DbParameter("@UserId", List.UserId, DbType.Int64));
            sqlparam.Add(new DbParameter("@EmployeeCode", List.EmployeeCode.Trim(), DbType.String));
            sqlparam.Add(new DbParameter("@BranchId", List.BranchId, DbType.Int32));
            sqlparam.Add(new DbParameter("@MobileNumber", List.MobileNumber.Trim(), DbType.String));
            sqlparam.Add(new DbParameter("@AlternateNumber", (List.AltMobileNumber == null) ? "" : List.AltMobileNumber.Trim(), DbType.String));
            sqlparam.Add(new DbParameter("@IMEI1", (List.IMEI1 == null) ? "" : List.IMEI1.Trim(), DbType.String));
            sqlparam.Add(new DbParameter("@IMEI2", (List.IMEI2 == null) ? "" : List.IMEI2.Trim(), DbType.String));
            sqlparam.Add(new DbParameter("@Status", List.Status));

            var datatable = _dataHelper.ExecuteDataTable("[dbo].[CreateEmployee]", sqlparam, CommandType.StoredProcedure);

            Message = Convert.ToString(datatable.Rows[0]["Description"]);
            if (Convert.ToBoolean(datatable.Rows[0]["Status"]))
                return 1;

            return 0;
        }

        /// <summary>
        /// Method to get all registered User
        /// </summary>
        /// <param name="List"></param>
        /// <returns></returns>
        public GetUserList GetUserList(GlobalPagination List)
        {
            var getList = new GetUserList { List = null, TotalSize = 0 };

            DbParameterCollection sqlparam = new DbParameterCollection();
            sqlparam.Add(new DbParameter("@UserId", List.UserId, DbType.Int64));
            sqlparam.Add(new DbParameter("@PageIndex", List.PageIndex, DbType.Int64));
            sqlparam.Add(new DbParameter("@PageSize", List.PageSize, DbType.Int64));

            var datatable = _dataHelper.ExecuteDataSet("[dbo].[GetUserList]", sqlparam, CommandType.StoredProcedure);
            if (datatable.Tables.Count > 0)
            {
                getList.List = (from item in datatable.Tables[0].AsEnumerable()
                                select new UserBasicProperties
                                {
                                    //Need to Capture the data
                                });

                getList.TotalSize = Convert.ToInt64(datatable.Tables[1].Rows[0]["totalCount"]);
            }
            return getList; 
        }

        public int UpdateEmployee(EmployeeFormData List, out string Message)
        {
            try
            {
                Message = string.Empty;

                //if (!GlobalAccessible.ValidateSpecialCharacter(List.FirstName.Trim() + ';' + List.LastName.Trim()))
                //{
                //    Message = "Special Character  not allowed";
                //    return 0;
                //}

                DbParameterCollection sqlparam = new DbParameterCollection();
                sqlparam.Add(new DbParameter("@EmployeeId", List.EmployeeId, DbType.Int64));
                sqlparam.Add(new DbParameter("@RoleId", List.RoleId, DbType.Int64));
                sqlparam.Add(new DbParameter("@LoginId", List.LoginId.Trim(), DbType.String));
                sqlparam.Add(new DbParameter("@Password", (List.isPasswordChange) ? AMBOEcryption.GetHashValue(List.Password.Trim()) : null, DbType.String));
                sqlparam.Add(new DbParameter("@FirstName", List.FirstName.Trim(), DbType.String));
                sqlparam.Add(new DbParameter("@LastName", List.LastName.Trim(), DbType.String));
                sqlparam.Add(new DbParameter("@MailId", List.EmailId == null ? "" : AMBOEcryption.EncryptData(List.EmailId.Trim(), true), DbType.String));
                sqlparam.Add(new DbParameter("@UserId", List.UserId, DbType.Int64));
                sqlparam.Add(new DbParameter("@EmployeeCode", List.EmployeeCode.Trim(), DbType.String));
                sqlparam.Add(new DbParameter("@BranchId", List.BranchId, DbType.Int32));
                sqlparam.Add(new DbParameter("@MobileNumber", List.MobileNumber.Trim(), DbType.String));
                sqlparam.Add(new DbParameter("@AlternateNumber", (List.AltMobileNumber == null) ? "" : List.AltMobileNumber.Trim(), DbType.String));
                sqlparam.Add(new DbParameter("@IMEI1", (List.IMEI1 == null) ? "" : List.IMEI1.Trim(), DbType.String));
                sqlparam.Add(new DbParameter("@IMEI2", (List.IMEI2 == null) ? "" : List.IMEI2.Trim(), DbType.String));
                sqlparam.Add(new DbParameter("@Status", List.Status));

                var datatable = _dataHelper.ExecuteDataTable("[dbo].[UpdateEmployee]", sqlparam, CommandType.StoredProcedure);

                Message = Convert.ToString(datatable.Rows[0]["Description"]);
                if (Convert.ToBoolean(datatable.Rows[0]["Status"]))
                    return 1;

                return 0;
            }
            catch(Exception ex)
            {
                Message = "Something went wrong, please try again.";
                return 0;
            }

            
        }

        public int DeleteEmployee(EmployeeFormData List, out string Message)
        {
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@EmployeeId", List.EmployeeId, DbType.Int32));
                objDBParam.Add(new DbParameter("@UserId", List.UserId, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[DeleteEmployee]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return 1;
            }
            catch (Exception ex)
            {
                //Log exception here
                Message = "Could not delete employee. Exception occured in API.";
            }
            return 0;
        }

        public UserBasicProperties GetEmployeeById(Int64 ID)
        {
            DbParameterCollection objDBParam = new DbParameterCollection();
            UserBasicProperties outputEmployee = null;
            try
            {
                objDBParam.Add(new DbParameter("@EmployeeId", ID, DbType.Int64));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetEmployee]", objDBParam, CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    outputEmployee = (from employee in outputFromSP.AsEnumerable()
                                   select new UserBasicProperties
                                   {
                                       EmployeeId = Convert.ToInt64(employee.Field<Int64>("ID")),
                                       EmployeeCode = Convert.ToString(employee.Field<string>("EMPLOYEECODE")),
                                       LoginId = Convert.ToString(employee.Field<string>("LOGINID")),
                                       Password = Convert.ToString(employee.Field<string>("PASSWORD")),
                                       ReTypePassword = Convert.ToString(employee.Field<string>("PASSWORD")),
                                       BranchId = Convert.ToInt64(employee.Field<Int64>("BRANCHID")),
                                       RegionId = Convert.ToInt64(employee.Field<Int64>("REGIONID")),
                                       StateId = Convert.ToInt64(employee.Field<Int64>("STATEID")),
                                       CityId = Convert.ToInt64(employee.Field<Int64>("CITYID")),
                                       DivisionId = Convert.ToInt64(employee.Field<Int64>("DIVISIONID")),
                                       FirstName = Convert.ToString(employee.Field<string>("FIRSTNAME")),
                                       LastName = Convert.ToString(employee.Field<string>("LASTNAME")),
                                       Address = Convert.ToString(employee.Field<string>("ADDRESS")) == "" ?
                                       "" : AMBOEcryption.DecryptData(employee.Field<string>("ADDRESS"), true),
                                       Gender = Convert.ToString(employee.Field<string>("Gender")) == "" ?
                                       "" : AMBOEcryption.DecryptData(employee.Field<string>("Gender"), true),
                                       FatherName = Convert.ToString(employee.Field<string>("FatherName")) == "" ?
                                       "" : AMBOEcryption.DecryptData(employee.Field<string>("FatherName"), true),
                                       MobileNumber = Convert.ToString(employee.Field<string>("MOBNO")),
                                       AltMobileNumber = Convert.ToString(employee.Field<string>("ALTMOBNO")),
                                       EmailId = Convert.ToString(employee.Field<string>("EMAILID")) == "" ?
                                       "" : AMBOEcryption.DecryptData(employee.Field<string>("EMAILID"), true),
                                       PANNo = Convert.ToString(employee.Field<string>("PAN")) == "" ?
                                       "" : AMBOEcryption.DecryptData(employee.Field<string>("PAN"), true),
                                       DOB = Convert.ToString(employee.Field<string>("DOB")) == "" ?
                                       "" : AMBOEcryption.DecryptData(employee.Field<string>("DOB"), true),
                                       DOJ = Convert.ToString(employee.Field<string>("DOJ")),
                                       DOL = Convert.ToString(employee.Field<string>("DOL")),
                                       RoleId = Convert.ToInt64(employee.Field<Int64>("ROLEID")),
                                       SFALevel = Convert.ToInt64(employee.Field<Int64>("SFALEVEL")),
                                       AgencyId = Convert.ToInt64(employee.Field<Int64>("AGENCYID")),
                                       IMEI1 = Convert.ToString(employee.Field<string>("IMEI1")),
                                       IMEI2 = Convert.ToString(employee.Field<string>("IMEI2")),
                                       SFATypeId = Convert.ToInt64(employee.Field<Int64>("SFATYPEID")),
                                       Status = Convert.ToBoolean(employee.Field<bool>("STATUS")),
                                       ShiftNameId = Convert.ToInt64(employee.Field<Int64>("ShiftNameId")),
                                   }).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                //Log exception here
            }
            return outputEmployee;
        }

        public UserBasicProperties GetOfferedEmployeeById(Int64 ID)
        {
            DbParameterCollection objDBParam = new DbParameterCollection();
            UserBasicProperties outputEmployee = null;
            try
            {
                objDBParam.Add(new DbParameter("@Id", ID, DbType.Int64));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetOfferedEmployeeById]", objDBParam, CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    outputEmployee = (from employee in outputFromSP.AsEnumerable()
                                      select new UserBasicProperties
                                      {
                                          EmployeeId = Convert.ToInt64(employee.Field<Int64>("ID")),
                                          EmployeeCode = Convert.ToString(employee.Field<string>("LOGINID")),
                                          LoginId = Convert.ToString(employee.Field<string>("LOGINID")),
                                          Password = Convert.ToString(employee.Field<string>("PASSWORD")),
                                          ReTypePassword = Convert.ToString(employee.Field<string>("PASSWORD")),
                                          BranchId = Convert.ToInt64(employee.Field<Int64>("BRANCHID")),
                                          RegionId = Convert.ToInt64(employee.Field<Int64>("REGIONID")),
                                          StateId = Convert.ToInt64(employee.Field<Int64>("STATEID")),
                                          CityId = Convert.ToInt64(employee.Field<Int64>("CITYID")),
                                          DivisionId = Convert.ToInt64(employee.Field<Int64>("DIVISIONID")),
                                          FirstName = Convert.ToString(employee.Field<string>("FIRSTNAME")),
                                          LastName = Convert.ToString(employee.Field<string>("LASTNAME")),
                                          Address = Convert.ToString(employee.Field<string>("ADDRESS")) == "" ?
                                          "" : AMBOEcryption.DecryptData(employee.Field<string>("ADDRESS"), true),
                                          MobileNumber = Convert.ToString(employee.Field<string>("MOBNO")),
                                          AltMobileNumber = Convert.ToString(employee.Field<string>("ALTMOBNO")),
                                          EmailId = Convert.ToString(employee.Field<string>("EMAILID")) == "" ?
                                          "" : AMBOEcryption.DecryptData(employee.Field<string>("EMAILID"), true),
                                          PANNo = Convert.ToString(employee.Field<string>("PAN")) == "" ?
                                          "" : AMBOEcryption.DecryptData(employee.Field<string>("PAN"), true),
                                          DOB = Convert.ToString(employee.Field<string>("DOB")),
                                          DOJ = Convert.ToString(employee.Field<string>("DOJ")),
                                          DOL = Convert.ToString(employee.Field<string>("DOL")),
                                          RoleId = Convert.ToInt64(employee.Field<Int64>("ROLEID")),
                                          SFALevel = Convert.ToInt64(employee.Field<Int64>("SFALEVEL")),
                                          AgencyId = Convert.ToInt64(employee.Field<Int64>("AGENCYID")),
                                          Status = Convert.ToBoolean(employee.Field<Int32>("STATUS"))
                                      }).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                //Log exception here
            }
            return outputEmployee;
        }

        public int ManageOfferedEmployee(SFAFormData List, out string Message)
        {
            int outputval = 0;
            int Uid = 0;
            try
            {
                Message = string.Empty;
                string Message1 = string.Empty;
                string Message2 = string.Empty;

                if (!GlobalAccessible.CheckBirthAndJoiningDates(List.DOB, List.DOJ))
                {
                    Message = "Date of Joining cannot be prior to date of birth.";
                    return 0;
                }

                if (!GlobalAccessible.CheckJoiningAndLeavingDates(List.DOJ, List.DOL))
                {
                    Message = "Date of leaving cannot be prior to date of joining.";
                    return 0;
                }

                if (!String.IsNullOrWhiteSpace(List.EmailId) && !GlobalAccessible.IsValidEmail(List.EmailId.Trim()))
                {
                    Message = "Invalid email id format provided.";
                    return 0;
                }

                DbParameterCollection sqlparam = new DbParameterCollection();
                sqlparam.Add(new DbParameter("@RoleId", List.RoleId, DbType.Int64));
                sqlparam.Add(new DbParameter("@LoginId", List.LoginId.Trim(), DbType.String));
                sqlparam.Add(new DbParameter("@Password", AMBOEcryption.GetHashValue(List.Password.Trim()), DbType.String));
                sqlparam.Add(new DbParameter("@FirstName", List.FirstName.Trim(), DbType.String));
                sqlparam.Add(new DbParameter("@LastName", List.LastName.Trim(), DbType.String));
                sqlparam.Add(new DbParameter("@Gender", (List.Gender == null) ? "" : AMBOEcryption.EncryptData(List.Gender.Trim(), true), DbType.String));
                sqlparam.Add(new DbParameter("@FatherName", (List.FatherName == null) ? "" : AMBOEcryption.EncryptData(List.FatherName.Trim(), true), DbType.String));
                sqlparam.Add(new DbParameter("@MailId", (List.EmailId == null) ? "" : AMBOEcryption.EncryptData(List.EmailId.Trim(), true), DbType.String));
                sqlparam.Add(new DbParameter("@UserId", List.UserId, DbType.Int64));
                sqlparam.Add(new DbParameter("@EmployeeCode", List.EmployeeCode.Trim(), DbType.String));
                sqlparam.Add(new DbParameter("@BranchId", List.BranchId, DbType.Int64));
                sqlparam.Add(new DbParameter("@StateId", List.StateId, DbType.Int64));
                sqlparam.Add(new DbParameter("@CityId", List.CityId, DbType.Int64));
                sqlparam.Add(new DbParameter("@Address", AMBOEcryption.EncryptData(List.Address.Trim(), true), DbType.String));
                sqlparam.Add(new DbParameter("@MobileNumber", List.MobileNumber.Trim(), DbType.String));
                sqlparam.Add(new DbParameter("@AlternateNumber", (List.AltMobileNumber == null) ? "" : List.AltMobileNumber.Trim(), DbType.String));
                sqlparam.Add(new DbParameter("@PanNo", (List.PANNo == null) ? "" : AMBOEcryption.EncryptData(List.PANNo.Trim(), true), DbType.String));
                sqlparam.Add(new DbParameter("@DOB", (List.DOB == null) ? "" : AMBOEcryption.EncryptData(List.DOB.Trim(), true), DbType.String));
                sqlparam.Add(new DbParameter("@DOJ", (List.DOJ == null) ? "" : List.DOJ.Trim(), DbType.String));
                sqlparam.Add(new DbParameter("@DOL", (List.DOL == null) ? "" : List.DOL.Trim(), DbType.String));
                sqlparam.Add(new DbParameter("@SFALevel", List.SFALevel, DbType.Int64));
                sqlparam.Add(new DbParameter("@RegionId", List.RegionId, DbType.Int64));
                sqlparam.Add(new DbParameter("@AgencyId", List.AgencyId, DbType.Int64));
                sqlparam.Add(new DbParameter("@DivisionId", List.DivisionId, DbType.Int64));
                sqlparam.Add(new DbParameter("@IMEI1", (List.IMEI1 == null) ? "" : List.IMEI1.Trim(), DbType.String));
                sqlparam.Add(new DbParameter("@IMEI2", (List.IMEI2 == null) ? "" : List.IMEI2.Trim(), DbType.String));
                sqlparam.Add(new DbParameter("@SFATypeId", List.SFATypeId, DbType.Int64));
                sqlparam.Add(new DbParameter("@Status", List.Status));

                var datatable = _dataHelper.ExecuteDataTable("[dbo].[ManageOfferedEmployee]", sqlparam, CommandType.StoredProcedure);

                Message = Convert.ToString(datatable.Rows[0]["Description"]);
                if (Convert.ToBoolean(datatable.Rows[0]["Status"]))
                {
                    Uid = Convert.ToInt32(datatable.Rows[0]["UID"]);

                    //**** To insert Salary record of an offered employee thorugh MPRSalarySTG table records******
                    SFASalaryMasterGrid salaryoutput = new SFASalaryMasterGrid();
                    DbParameterCollection sqlparam1 = new DbParameterCollection();
                    sqlparam1.Add(new DbParameter("@LoginId", List.LoginId.Trim(), DbType.Int32));
                    var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetSalaryDetails]", sqlparam1, CommandType.StoredProcedure);

                    if (outputFromSP.Rows.Count > 0)
                    {
                        salaryoutput = (from sfasalarydata in outputFromSP.AsEnumerable()
                                        select new SFASalaryMasterGrid
                                        {
                                            Basic = Convert.ToString(sfasalarydata.Field<string>("Basic")),
                                            HRA = Convert.ToString(sfasalarydata.Field<string>("HRA")),
                                            Other = Convert.ToString(sfasalarydata.Field<string>("Other")),
                                            Airtime = Convert.ToString(sfasalarydata.Field<string>("Airtime")),

                                        }).FirstOrDefault();
                        salaryoutput.LoginId = Uid;
                        salaryoutput.UserId = List.UserId;
                        salaryoutput.Med = salaryoutput.Conv = salaryoutput.Insurance = salaryoutput.OtherAllowance1 = salaryoutput.OtherAllowance2 = "0";

                        MasterMaintenanceProvider masterprovider = new MasterMaintenanceProvider(_IErrorLogProvider);
                        var finaloutput = masterprovider.CreateSFASalaryMaster(salaryoutput, out Message1);

                        if (finaloutput == 1)
                            Message = Message + ' ' + Message1;

                        else
                            Message = Message + ' ' + Message1;
                    }

                    else
                        Message = Message + ", " + "No salary details found, please update salary through SFA Salary Master.";


                    //**** To insert Employee for HR record thorugh MPRSalarySTG table records******
                    SFAMasterForHR employeeforhr = new SFAMasterForHR();
                    var employeeforhroutput = _dataHelper.ExecuteDataTable("[dbo].[GetSFAMasterforHRDetails]", sqlparam1, CommandType.StoredProcedure);
                    if (employeeforhroutput.Rows.Count > 0)
                    {
                        employeeforhr = (from sfamasterforhr in employeeforhroutput.AsEnumerable()
                                         select new SFAMasterForHR
                                         {
                                             Source = Convert.ToString(sfamasterforhr.Field<string>("Source")),
                                             SourceName = Convert.ToString(sfamasterforhr.Field<string>("SourceName")),
                                             SourceCode = Convert.ToString(sfamasterforhr.Field<string>("SourceCode")),
                                             LevelofEducation = Convert.ToString(sfamasterforhr.Field<string>("LevelofEducation")),
                                             Education = Convert.ToString(sfamasterforhr.Field<string>("Education")),
                                             Experience = Convert.ToString(sfamasterforhr.Field<string>("Experience")),

                                         }).FirstOrDefault();
                        employeeforhr.LoginId = Convert.ToInt64(Uid);
                        employeeforhr.UserId = List.UserId;

                        var finaloutput1 = CreateSFAMasterforHR(employeeforhr, out Message2);

                        if (finaloutput1 == 1)
                            Message = Message + ' ' + Message2;

                        else
                            Message = Message + ' ' + Message2;
                    }
                    else
                        Message = Message + ", " + "No details found, please update data through SFA Master for HR.";

                    outputval = 1;
                }
            }

            catch (Exception ex)
            {
                Message = ex.Message;
            }
            return outputval;
        }
        #endregion Employee Management

        #region SFA Management
        public int CreateSFA(SFAFormData List, out string Message)
        {
            Message = string.Empty;

            //if (!GlobalAccessible.ValidateSpecialCharacter(List.FirstName.Trim() + ';' + List.LastName.Trim()))
            //{
            //    Message = "Special Character not allowed in First Name or Last Name";
            //    return 0;
            //}
            if (!GlobalAccessible.CheckBirthAndJoiningDates(List.DOB, List.DOJ))
            {
                Message = "Date of Joining cannot be prior to date of birth.";
                return 0;
            }

            if (!GlobalAccessible.CheckJoiningAndLeavingDates(List.DOJ, List.DOL))
            {
                Message = "Date of leaving cannot be prior to date of joining.";
                return 0;
            }

            if(!String.IsNullOrWhiteSpace(List.EmailId) && !GlobalAccessible.IsValidEmail(List.EmailId.Trim()))
            {
                Message = "Invalid email id format provided.";
                return 0;
            }

            DbParameterCollection sqlparam = new DbParameterCollection();
            sqlparam.Add(new DbParameter("@RoleId", List.RoleId, DbType.Int64));
            sqlparam.Add(new DbParameter("@LoginId", List.LoginId.Trim(), DbType.String));
            sqlparam.Add(new DbParameter("@Password", AMBOEcryption.GetHashValue(List.Password.Trim()), DbType.String));
            sqlparam.Add(new DbParameter("@FirstName", List.FirstName.Trim(), DbType.String));
            sqlparam.Add(new DbParameter("@LastName", List.LastName.Trim(), DbType.String));
            sqlparam.Add(new DbParameter("@Gender", (List.Gender == null) ? "" : AMBOEcryption.EncryptData(List.Gender.Trim(), true), DbType.String));
            sqlparam.Add(new DbParameter("@FatherName", (List.FatherName == null) ? "" : AMBOEcryption.EncryptData(List.FatherName.Trim(), true), DbType.String));
            sqlparam.Add(new DbParameter("@MailId", (List.EmailId == null) ? "" : AMBOEcryption.EncryptData(List.EmailId.Trim(), true), DbType.String));
            sqlparam.Add(new DbParameter("@UserId", List.UserId, DbType.Int64));
            sqlparam.Add(new DbParameter("@EmployeeCode", List.EmployeeCode.Trim(), DbType.String));
            sqlparam.Add(new DbParameter("@BranchId", List.BranchId, DbType.Int64));
            sqlparam.Add(new DbParameter("@StateId", List.StateId, DbType.Int64));
            sqlparam.Add(new DbParameter("@CityId", List.CityId, DbType.Int64));
            sqlparam.Add(new DbParameter("@Address", AMBOEcryption.EncryptData(List.Address.Trim(),true), DbType.String));
            sqlparam.Add(new DbParameter("@MobileNumber", List.MobileNumber.Trim(), DbType.String));
            sqlparam.Add(new DbParameter("@AlternateNumber", (List.AltMobileNumber == null) ? "" : List.AltMobileNumber.Trim(), DbType.String));
            sqlparam.Add(new DbParameter("@PanNo", (List.PANNo == null) ? "" : AMBOEcryption.EncryptData(List.PANNo.Trim(), true), DbType.String));
            sqlparam.Add(new DbParameter("@DOB", (List.DOB == null) ? "" : AMBOEcryption.EncryptData(List.DOB.Trim(), true), DbType.String));
            sqlparam.Add(new DbParameter("@DOJ", (List.DOJ == null) ? "" : List.DOJ.Trim(), DbType.String));
            sqlparam.Add(new DbParameter("@DOL", (List.DOL == null) ? "" : List.DOL.Trim(), DbType.String));
            sqlparam.Add(new DbParameter("@SFALevel", List.SFALevel, DbType.Int64));
            sqlparam.Add(new DbParameter("@RegionId", List.RegionId, DbType.Int64));
            sqlparam.Add(new DbParameter("@AgencyId", List.AgencyId, DbType.Int64));
            sqlparam.Add(new DbParameter("@DivisionId", List.DivisionId, DbType.Int64));
            sqlparam.Add(new DbParameter("@IMEI1", (List.IMEI1 == null) ? "" : List.IMEI1.Trim(), DbType.String));
            sqlparam.Add(new DbParameter("@IMEI2", (List.IMEI2 == null) ? "" : List.IMEI2.Trim(), DbType.String));
            sqlparam.Add(new DbParameter("@SFATypeId", List.SFATypeId, DbType.Int64));
            sqlparam.Add(new DbParameter("@Status", List.Status));
            sqlparam.Add(new DbParameter("@ShiftNameId", List.ShiftNameId, DbType.Int32));

            var datatable = _dataHelper.ExecuteDataTable("[dbo].[CreateSFA]", sqlparam, CommandType.StoredProcedure);

            Message = Convert.ToString(datatable.Rows[0]["Description"]);
            if (Convert.ToBoolean(datatable.Rows[0]["Status"]))
                return 1;

            return 0;
        }

        public int UpdateSFA(SFAFormData List, out string Message)
        {
            Message = string.Empty;

            //if (!GlobalAccessible.ValidateSpecialCharacter(List.FirstName.Trim() + ';' + List.LastName.Trim()))
            //{
            //    Message = "Special Character  not allowed";
            //    return 0;
            //}
            if (!GlobalAccessible.CheckBirthAndJoiningDates(List.DOB, List.DOJ))
            {
                Message = "Date of Joining cannot be prior to date of birth.";
                return 0;
            }

            if (!GlobalAccessible.CheckJoiningAndLeavingDates(List.DOJ, List.DOL))
            {
                Message = "Date of leaving cannot be prior to date of joining.";
                return 0;
            }

            if (!String.IsNullOrWhiteSpace(List.EmailId) && !GlobalAccessible.IsValidEmail(List.EmailId.Trim()))
            {
                Message = "Invalid email id format provided.";
                return 0;
            }

            DbParameterCollection sqlparam = new DbParameterCollection();
            sqlparam.Add(new DbParameter("@EmployeeId", List.EmployeeId, DbType.Int64));
            sqlparam.Add(new DbParameter("@RoleId", List.RoleId, DbType.Int64));
            sqlparam.Add(new DbParameter("@LoginId", List.LoginId.Trim(), DbType.String));
            sqlparam.Add(new DbParameter("@Password", (List.isPasswordChange) ? AMBOEcryption.GetHashValue(List.Password.Trim()) : null, DbType.String));
            sqlparam.Add(new DbParameter("@FirstName", List.FirstName.Trim(), DbType.String));
            sqlparam.Add(new DbParameter("@LastName", List.LastName.Trim(), DbType.String));
            sqlparam.Add(new DbParameter("@Gender", (List.Gender == null) ? "" : AMBOEcryption.EncryptData(List.Gender.Trim(), true), DbType.String));
            sqlparam.Add(new DbParameter("@FatherName", (List.FatherName == null) ? "" : AMBOEcryption.EncryptData(List.FatherName.Trim(), true), DbType.String));
            sqlparam.Add(new DbParameter("@MailId", (List.EmailId == null) ? "" : AMBOEcryption.EncryptData(List.EmailId.Trim(), true), DbType.String));
            sqlparam.Add(new DbParameter("@UserId", List.UserId, DbType.Int64));
            sqlparam.Add(new DbParameter("@EmployeeCode", List.EmployeeCode.Trim(), DbType.String));
            sqlparam.Add(new DbParameter("@BranchId", List.BranchId, DbType.Int64));
            sqlparam.Add(new DbParameter("@StateId", List.StateId, DbType.Int64));
            sqlparam.Add(new DbParameter("@CityId", List.CityId, DbType.Int64));
            sqlparam.Add(new DbParameter("@Address", AMBOEcryption.EncryptData(List.Address.Trim(), true), DbType.String));
            sqlparam.Add(new DbParameter("@MobileNumber", List.MobileNumber.Trim(), DbType.String));
            sqlparam.Add(new DbParameter("@AlternateNumber", (List.AltMobileNumber == null) ? "" : List.AltMobileNumber.Trim(), DbType.String));
            sqlparam.Add(new DbParameter("@PanNo", (List.PANNo == null) ? "" : AMBOEcryption.EncryptData(List.PANNo.Trim(), true), DbType.String));
            sqlparam.Add(new DbParameter("@DOB", (List.DOB == null) ? "" : AMBOEcryption.EncryptData(List.DOB.Trim(), true), DbType.String));
            sqlparam.Add(new DbParameter("@DOJ", (List.DOJ == null) ? "" : List.DOJ.Trim(), DbType.String));
            sqlparam.Add(new DbParameter("@DOL", (List.DOL == null) ? "" : List.DOL.Trim(), DbType.String));
            sqlparam.Add(new DbParameter("@SFALevel", List.SFALevel, DbType.Int64));
            sqlparam.Add(new DbParameter("@RegionId", List.RegionId, DbType.Int64));
            sqlparam.Add(new DbParameter("@AgencyId", List.AgencyId, DbType.Int64));
            sqlparam.Add(new DbParameter("@DivisionId", List.DivisionId, DbType.Int64));
            sqlparam.Add(new DbParameter("@IMEI1", (List.IMEI1 == null) ? "" : List.IMEI1.Trim(), DbType.String));
            sqlparam.Add(new DbParameter("@IMEI2", (List.IMEI2 == null) ? "" : List.IMEI2.Trim(), DbType.String));
            sqlparam.Add(new DbParameter("@SFATypeId", List.SFATypeId, DbType.Int64));
            sqlparam.Add(new DbParameter("@Status", List.Status));
            sqlparam.Add(new DbParameter("@ShiftNameId", List.ShiftNameId, DbType.Int32));

            var datatable = _dataHelper.ExecuteDataTable("[dbo].[UpdateSFA]", sqlparam, CommandType.StoredProcedure);

            Message = Convert.ToString(datatable.Rows[0]["Description"]);
            if (Convert.ToBoolean(datatable.Rows[0]["Status"]))
                return 1;

            return 0;
        }

        public int DeleteSFA(SFAFormData List, out string Message)
        {
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@EmployeeId", List.EmployeeId, DbType.Int32));
                objDBParam.Add(new DbParameter("@UserId", List.UserId, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[DeleteSFA]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return 1;
            }
            catch (Exception ex)
            {
                //Log exception here
                Message = "Could not delete SFA. Exception occured in API.";
            }
            return 0;
        }


        public SFACodeAutoGenerate GetSFACode()
        {
            SFACodeAutoGenerate outputSFA = null;
            try
            {
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetSFACode]", CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    outputSFA = (from employee in outputFromSP.AsEnumerable()
                                 select new SFACodeAutoGenerate
                                 {
                                     SFACode = Convert.ToString(employee.Field<Int64>("Id")),
                                     
                                 }).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                //Log exception here
            }
            return outputSFA;
        }

        public DealerSFAMapping GetSFADetails(string LoginId)
        {
            DbParameterCollection objDBParam = new DbParameterCollection();
            DealerSFAMapping outputSFA = null;
            try
            {
                objDBParam.Add(new DbParameter("@LoginId", LoginId, DbType.String));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetSFADetails]", objDBParam, CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    outputSFA = (from employee in outputFromSP.AsEnumerable()
                                      select new DealerSFAMapping
                                      {
                                          EmployeeId = Convert.ToInt64(employee.Field<Int64>("ID")),
                                          BranchId = Convert.ToInt64(employee.Field<Int64>("BranchId")),
                                          StateId = Convert.ToInt64(employee.Field<Int64>("StateId")),
                                          CityId = Convert.ToInt64(employee.Field<Int64>("CityId")),
                                      }).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                //Log exception here
            }
            return outputSFA;
        }
        #endregion SFA Management


        #region SFA Master for HR API

        public int CreateSFAMasterforHR(SFAMasterForHR employeeForHR, out string Message)
        {
            int IsDone = 0;
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@LoginId", employeeForHR.LoginId, DbType.Int64));
                objDBParam.Add(new DbParameter("@Source", AMBOEcryption.EncryptData((employeeForHR.Source != null ? employeeForHR.Source.Trim() : null),true), DbType.String));
                objDBParam.Add(new DbParameter("@SourceName", AMBOEcryption.EncryptData((employeeForHR.SourceName != null ? employeeForHR.SourceName.Trim() : null), true), DbType.String));
                objDBParam.Add(new DbParameter("@SourceCode", AMBOEcryption.EncryptData((employeeForHR.SourceCode != null ? employeeForHR.SourceCode.Trim() : null), true), DbType.String));
                //objDBParam.Add(new DbParameter("@Asset", AMBOEcryption.EncryptData((employeeForHR.AssetIssued != null ? employeeForHR.AssetIssued.Trim() : null), true), DbType.String));
                //objDBParam.Add(new DbParameter("@Gender", AMBOEcryption.EncryptData((employeeForHR.Gender != null ? employeeForHR.Gender.Trim() : null), true), DbType.String));
                //objDBParam.Add(new DbParameter("@FatherName", AMBOEcryption.EncryptData((employeeForHR.FatherName != null ? employeeForHR.FatherName.Trim() : null), true), DbType.String));
                objDBParam.Add(new DbParameter("@LevelofEducation", AMBOEcryption.EncryptData((employeeForHR.LevelofEducation != null ? employeeForHR.LevelofEducation.Trim() : null), true), DbType.String));
                objDBParam.Add(new DbParameter("@Education", AMBOEcryption.EncryptData((employeeForHR.Education != null ? employeeForHR.Education.Trim() : null), true), DbType.String));
                objDBParam.Add(new DbParameter("@Experience", AMBOEcryption.EncryptData((employeeForHR.Experience != null ? employeeForHR.Experience.Trim() : null), true), DbType.String));
                //objDBParam.Add(new DbParameter("@BankName", AMBOEcryption.EncryptData((employeeForHR.BankName != null ? employeeForHR.BankName.Trim() : null), true), DbType.String));
                //objDBParam.Add(new DbParameter("@BankAccountNo", AMBOEcryption.EncryptData((employeeForHR.BankAccountNo != null ? employeeForHR.BankAccountNo.Trim() : null), true), DbType.String));
                //objDBParam.Add(new DbParameter("@ESIAccountNo", AMBOEcryption.EncryptData((employeeForHR.ESIAccountNo != null ? employeeForHR.ESIAccountNo.Trim() : null), true), DbType.String));
                //objDBParam.Add(new DbParameter("@PFAccountNo", AMBOEcryption.EncryptData((employeeForHR.PFAccountNo != null ? employeeForHR.PFAccountNo.Trim() : null), true), DbType.String));
                //objDBParam.Add(new DbParameter("@MedicalInsuranceNo", AMBOEcryption.EncryptData((employeeForHR.MedicalInsuranceNo != null ? employeeForHR.MedicalInsuranceNo.Trim() : null), true), DbType.String));
                //objDBParam.Add(new DbParameter("@MICoverageFrom", AMBOEcryption.EncryptData((employeeForHR.MICoverageFrom != null ? employeeForHR.MICoverageFrom.Trim() : null), true), DbType.String));
                //objDBParam.Add(new DbParameter("@MICoverageTo", AMBOEcryption.EncryptData((employeeForHR.MICoverageTo != null ? employeeForHR.MICoverageTo.Trim() : null), true), DbType.String));
                //objDBParam.Add(new DbParameter("@PersonalInsuranceNo", AMBOEcryption.EncryptData((employeeForHR.PersonalInsuranceNo != null ? employeeForHR.PersonalInsuranceNo.Trim() : null), true), DbType.String));
                //objDBParam.Add(new DbParameter("@PICoverageFrom", AMBOEcryption.EncryptData((employeeForHR.PICoverageFrom != null ? employeeForHR.PICoverageFrom.Trim() : null), true), DbType.String));
                //objDBParam.Add(new DbParameter("@PICoverageTo", AMBOEcryption.EncryptData((employeeForHR.PICoverageTo != null ? employeeForHR.PICoverageTo.Trim() : null), true), DbType.String));
                objDBParam.Add(new DbParameter("@UserId", employeeForHR.UserId, DbType.Int64));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[CreateEmployeesForHR]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    IsDone = 1;
            }
            catch (Exception ex)
            {
                Message = "Error occured while creating Employee for HR";
            }

            return IsDone; 
        }

        public IEnumerable<SFAMasterForHR> GetAllDetailsSFAMasterforHR()
        {
            IEnumerable<SFAMasterForHR> sfaMasterforHRList = null;

            try
            {
                var outputDBParam = _dataHelper.ExecuteDataTable("[dbo].[GetAllSFAMasterforHR]", CommandType.StoredProcedure);
                if (outputDBParam.Rows.Count > 0)
                {
                    sfaMasterforHRList = (from sfamasterforhr in outputDBParam.AsEnumerable()
                                          select new SFAMasterForHR
                                          {
                                              Id = Convert.ToInt64(sfamasterforhr.Field<Int64>("ID")),
                                              SFAName= Convert.ToString(sfamasterforhr.Field<string>("SFANAME")),
                                              SFACode = Convert.ToString(sfamasterforhr.Field<string>("SFACode"))
                                              //Branch = Convert.ToString(sfamasterforhr.Field<string>("BRANCH")),
                                              //State = Convert.ToString(sfamasterforhr.Field<string>("STATE")),
                                              //City = Convert.ToString(sfamasterforhr.Field<string>("CITY")),
                                              //Gender = Convert.ToString(sfamasterforhr.Field<string>("GENDER")) == "" ?
                                              //         "" : AMBOEcryption.DecryptData(sfamasterforhr.Field<string>("GENDER"), true),
                                              //SourceName = Convert.ToString(sfamasterforhr.Field<string>("SOURCENAME")) == "" ?
                                              //         "" : AMBOEcryption.DecryptData(sfamasterforhr.Field<string>("SOURCENAME"), true),
                                              //Education = Convert.ToString(sfamasterforhr.Field<string>("EDUCATION")) == "" ?
                                              //         "" : AMBOEcryption.DecryptData(sfamasterforhr.Field<string>("EDUCATION"), true),
                                              //Experience = Convert.ToString(sfamasterforhr.Field<string>("EXPERIENCE")) == "" ?
                                              //         "" : AMBOEcryption.DecryptData(sfamasterforhr.Field<string>("EXPERIENCE"), true),
                                          });
                }
            }
            catch(Exception ex)
            {

            }

            return sfaMasterforHRList;
        }

        public SFAMasterForHR GetSFAMasterforHRById(Int64 sfaMasterforHRId)
        {
            SFAMasterForHR sfaMasterforHR = null;
            DbParameterCollection paramcollection = new DbParameterCollection();
            try
            {
                paramcollection.Add(new DbParameter("@Id", sfaMasterforHRId, DbType.Int64));
                var outputDBParam = _dataHelper.ExecuteDataTable("[dbo].[GetSFAMasterforHRById]", paramcollection, CommandType.StoredProcedure);
                if (outputDBParam.Rows.Count > 0)
                {
                    sfaMasterforHR = (from sfamasterforhr in outputDBParam.AsEnumerable()
                                          select new SFAMasterForHR
                                          {
                                              Id = Convert.ToInt64(sfamasterforhr.Field<Int64>("ID")),
                                              LoginId = Convert.ToInt64(sfamasterforhr.Field<Int64>("LOGINID")),                                              
                                              SFAName = Convert.ToString(sfamasterforhr.Field<string>("SFANAME")),
                                              SFACode = Convert.ToString(sfamasterforhr.Field<string>("SFACode")),
                                              Region = Convert.ToString(sfamasterforhr.Field<string>("Region")),
                                              Branch = Convert.ToString(sfamasterforhr.Field<string>("BRANCH")),
                                              DOL = Convert.ToString(sfamasterforhr.Field<string>("DOL")),
                                              DOJ = Convert.ToString(sfamasterforhr.Field<string>("DOJ")),                                              
                                              DOB = Convert.ToString(sfamasterforhr.Field<string>("DOB")) == "" ?
                                                "" : AMBOEcryption.DecryptData(sfamasterforhr.Field<string>("DOB"), true),
                                              State = Convert.ToString(sfamasterforhr.Field<string>("STATE")),
                                              City = Convert.ToString(sfamasterforhr.Field<string>("CITY")),
                                              Division = Convert.ToString(sfamasterforhr.Field<string>("DIVISION")),
                                              //Gender = Convert.ToString(sfamasterforhr.Field<string>("GENDER")) == "" ?
                                              //  "" : AMBOEcryption.DecryptData(sfamasterforhr.Field<string>("GENDER"), true),
                                              SourceName = Convert.ToString(sfamasterforhr.Field<string>("SOURCENAME")) == "" ?
                                                "" : AMBOEcryption.DecryptData(sfamasterforhr.Field<string>("SOURCENAME"), true),
                                              Education = Convert.ToString(sfamasterforhr.Field<string>("EDUCATION")) == "" ?
                                                "" : AMBOEcryption.DecryptData(sfamasterforhr.Field<string>("EDUCATION"), true),
                                              Experience = Convert.ToString(sfamasterforhr.Field<string>("EXPERIENCE")) == "" ?
                                                "" : AMBOEcryption.DecryptData(sfamasterforhr.Field<string>("EXPERIENCE"), true),
                                              SFALevel = Convert.ToString(sfamasterforhr.Field<string>("SFALEVEL")),
                                              Address = Convert.ToString(sfamasterforhr.Field<string>("ADDRESS")) == "" ?
                                                "" : AMBOEcryption.DecryptData(sfamasterforhr.Field<string>("ADDRESS"), true),
                                              EmailId = Convert.ToString(sfamasterforhr.Field<string>("EMAILID")) == "" ?
                                                "" : AMBOEcryption.DecryptData(sfamasterforhr.Field<string>("EMAILID"), true),
                                              MobileNumber = Convert.ToString(sfamasterforhr.Field<string>("MOBILENUMBER")),
                                              Source = Convert.ToString(sfamasterforhr.Field<string>("SOURCE")) == "" ?
                                                "" : AMBOEcryption.DecryptData(sfamasterforhr.Field<string>("SOURCE"), true),
                                              SourceCode = Convert.ToString(sfamasterforhr.Field<string>("SOURCECODE")) == "" ?
                                                "" : AMBOEcryption.DecryptData(sfamasterforhr.Field<string>("SOURCECODE"), true),
                                              AssetIssued = Convert.ToString(sfamasterforhr.Field<string>("ASSETISSUED")) == "" ?
                                                "" : AMBOEcryption.DecryptData(sfamasterforhr.Field<string>("ASSETISSUED"), true),
                                              //FatherName = Convert.ToString(sfamasterforhr.Field<string>("FATHERNAME")) == "" ?
                                              //  "" : AMBOEcryption.DecryptData(sfamasterforhr.Field<string>("FATHERNAME"), true),
                                              LevelofEducation = Convert.ToString(sfamasterforhr.Field<string>("LEVELOFEDUCATION")) == "" ?
                                                "" : AMBOEcryption.DecryptData(sfamasterforhr.Field<string>("LEVELOFEDUCATION"), true),
                                              BankName = Convert.ToString(sfamasterforhr.Field<string>("BANKNAME")) == "" ?
                                                "" : AMBOEcryption.DecryptData(sfamasterforhr.Field<string>("BANKNAME"), true),
                                              BankAccountNo = Convert.ToString(sfamasterforhr.Field<string>("BANKACCOUNTNO")) == "" ?
                                                "" : AMBOEcryption.DecryptData(sfamasterforhr.Field<string>("BANKACCOUNTNO"), true),
                                              ESIAccountNo = Convert.ToString(sfamasterforhr.Field<string>("ESIACCOUNTNO")) == "" ?
                                                "" : AMBOEcryption.DecryptData(sfamasterforhr.Field<string>("ESIACCOUNTNO"), true),
                                              PFAccountNo = Convert.ToString(sfamasterforhr.Field<string>("PFACCOUNTNO")) == "" ?
                                                "" : AMBOEcryption.DecryptData(sfamasterforhr.Field<string>("PFACCOUNTNO"), true),
                                              MedicalInsuranceNo = Convert.ToString(sfamasterforhr.Field<string>("MEDICALINSURANCENO")) == "" ?
                                                "" : AMBOEcryption.DecryptData(sfamasterforhr.Field<string>("MEDICALINSURANCENO"), true),
                                              MICoverageFrom = Convert.ToString(sfamasterforhr.Field<string>("MICOVERAGEFROM")) == "" ?
                                                "" : AMBOEcryption.DecryptData(sfamasterforhr.Field<string>("MICOVERAGEFROM"), true),
                                              MICoverageTo = Convert.ToString(sfamasterforhr.Field<string>("MICOVERAGETO")) == "" ?
                                                "" : AMBOEcryption.DecryptData(sfamasterforhr.Field<string>("MICOVERAGETO"), true),
                                              PersonalInsuranceNo = Convert.ToString(sfamasterforhr.Field<string>("PERSONALINSURANCENO")) == "" ?
                                                "" : AMBOEcryption.DecryptData(sfamasterforhr.Field<string>("PERSONALINSURANCENO"), true),
                                              PICoverageFrom = Convert.ToString(sfamasterforhr.Field<string>("PICOVERAGEFROM")) == "" ?
                                                "" : AMBOEcryption.DecryptData(sfamasterforhr.Field<string>("PICOVERAGEFROM"), true),
                                              PICoverageTo = Convert.ToString(sfamasterforhr.Field<string>("PICOVERAGETO")) == "" ?
                                                "" : AMBOEcryption.DecryptData(sfamasterforhr.Field<string>("PICOVERAGETO"), true),
                                              
                                          }).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {

            }

            return sfaMasterforHR;
        }

        public int UpdateSFAMasterforHR(SFAMasterForHR employeeForHR, out string Message)
        {
            bool IsDone = false;
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@Id", employeeForHR.Id, DbType.Int64));
                objDBParam.Add(new DbParameter("@LoginId", employeeForHR.LoginId, DbType.Int64));
                objDBParam.Add(new DbParameter("@Source", AMBOEcryption.EncryptData((employeeForHR.Source != null ? employeeForHR.Source.Trim() : ""), true), DbType.String));
                objDBParam.Add(new DbParameter("@SourceName", AMBOEcryption.EncryptData((employeeForHR.SourceName != null ? employeeForHR.SourceName.Trim() : ""), true), DbType.String));
                objDBParam.Add(new DbParameter("@SourceCode", AMBOEcryption.EncryptData((employeeForHR.SourceCode != null ? employeeForHR.SourceCode.Trim() : ""), true), DbType.String));
                objDBParam.Add(new DbParameter("@Asset", AMBOEcryption.EncryptData((employeeForHR.AssetIssued != null ? employeeForHR.AssetIssued.Trim() : ""), true), DbType.String));
                //objDBParam.Add(new DbParameter("@Gender", AMBOEcryption.EncryptData((employeeForHR.Gender != null ? employeeForHR.Gender.Trim() : ""), true), DbType.String));
                //objDBParam.Add(new DbParameter("@FatherName", AMBOEcryption.EncryptData((employeeForHR.FatherName != null ? employeeForHR.FatherName.Trim() : ""), true), DbType.String));
                objDBParam.Add(new DbParameter("@LevelofEducation", AMBOEcryption.EncryptData((employeeForHR.LevelofEducation != null ? employeeForHR.LevelofEducation.Trim() : ""), true), DbType.String));
                objDBParam.Add(new DbParameter("@Education", AMBOEcryption.EncryptData((employeeForHR.Education != null ? employeeForHR.Education.Trim() : ""), true), DbType.String));
                objDBParam.Add(new DbParameter("@Experience", AMBOEcryption.EncryptData((employeeForHR.Experience != null ? employeeForHR.Experience.Trim() : ""), true), DbType.String));
                objDBParam.Add(new DbParameter("@BankName", AMBOEcryption.EncryptData((employeeForHR.BankName != null ? employeeForHR.BankName.Trim() : ""), true), DbType.String));
                objDBParam.Add(new DbParameter("@BankAccountNo", AMBOEcryption.EncryptData((employeeForHR.BankAccountNo != null ? employeeForHR.BankAccountNo.Trim() : ""), true), DbType.String));
                objDBParam.Add(new DbParameter("@ESIAccountNo", AMBOEcryption.EncryptData((employeeForHR.ESIAccountNo != null ? employeeForHR.ESIAccountNo.Trim() : ""), true), DbType.String));
                objDBParam.Add(new DbParameter("@PFAccountNo", AMBOEcryption.EncryptData((employeeForHR.PFAccountNo != null ? employeeForHR.PFAccountNo.Trim() : ""), true), DbType.String));
                objDBParam.Add(new DbParameter("@MedicalInsuranceNo", AMBOEcryption.EncryptData((employeeForHR.MedicalInsuranceNo != null ? employeeForHR.MedicalInsuranceNo.Trim() : ""), true), DbType.String));
                objDBParam.Add(new DbParameter("@MICoverageFrom", AMBOEcryption.EncryptData((employeeForHR.MICoverageFrom != null ? employeeForHR.MICoverageFrom.Trim() : ""), true), DbType.String));
                objDBParam.Add(new DbParameter("@MICoverageTo", AMBOEcryption.EncryptData((employeeForHR.MICoverageTo != null ? employeeForHR.MICoverageTo.Trim() : ""), true), DbType.String));
                objDBParam.Add(new DbParameter("@PersonalInsuranceNo", AMBOEcryption.EncryptData((employeeForHR.PersonalInsuranceNo != null ? employeeForHR.PersonalInsuranceNo.Trim() : ""), true), DbType.String));
                objDBParam.Add(new DbParameter("@PICoverageFrom", AMBOEcryption.EncryptData((employeeForHR.PICoverageFrom != null ? employeeForHR.PICoverageFrom.Trim() : ""), true), DbType.String));
                objDBParam.Add(new DbParameter("@PICoverageTo", AMBOEcryption.EncryptData((employeeForHR.PICoverageTo != null ? employeeForHR.PICoverageTo.Trim() : ""), true), DbType.String));
                //objDBParam.Add(new DbParameter("@IsActive", employeeForHR.IsActive));
                objDBParam.Add(new DbParameter("@UserId", employeeForHR.UserId, DbType.Int64));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[UpdateEmployeesForHR]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    IsDone = true;
            }
            catch (Exception ex)
            {
                Message = "Error occured while updating Employee for HR";
            }

            return IsDone == true ? 1 : 0;
        }

        public int DeleteSFAMasterforHR(SFAMasterforHRDelete employeeForHR, out string Message)
        {
            bool IsDone = false;
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@Id", employeeForHR.Id));
                objDBParam.Add(new DbParameter("@UserId", employeeForHR.UserId, DbType.Int64));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[DeleteEmployeesForHR]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    IsDone = true;
            }
            catch (Exception ex)
            {
                Message = "Error occured while deleting Employee for HR";
            }

            return IsDone == true ? 1 : 0;
        }

        public IEnumerable<SFAMasterForHR> SFAMasterforHRDataDownload(SFAMasterforHRDownload InputParam)
        {
            IEnumerable<SFAMasterForHR> sfaMasterforHRList = null;
            DataRow row;
            DataTable dtbranch = new DataTable();

            try
            {
                dtbranch.Columns.Add("FilterId");
                if (InputParam.BranchIds != null)
                {
                    foreach (Int64 branchid in InputParam.BranchIds)
                    {
                        row = dtbranch.NewRow();
                        row["FilterId"] = branchid;
                        dtbranch.Rows.Add(row);
                    }
                }

                SqlParameter[] objDBParam = new SqlParameter[3];
                objDBParam[0] = new SqlParameter("@FromDate", InputParam.FromDate);
                objDBParam[1] = new SqlParameter("@ToDate", InputParam.ToDate);
                objDBParam[2] = new SqlParameter("@BranchIds", dtbranch);

                var outputDBParam = _dataHelper.ExecuteProcedure("[dbo].[SFAMasterforHRDataDownload]", objDBParam);
                if (outputDBParam.Rows.Count > 0)
                {
                    sfaMasterforHRList = (from sfamasterforhr in outputDBParam.AsEnumerable()
                                          select new SFAMasterForHR
                                          {
                                              Id = Convert.ToInt64(sfamasterforhr.Field<Int64>("ID")),
                                              SFAName = Convert.ToString(sfamasterforhr.Field<string>("SFAName")),
                                              SFACode = Convert.ToString(sfamasterforhr.Field<string>("SFACode")),
                                              Region = Convert.ToString(sfamasterforhr.Field<string>("Region")),
                                              Branch = Convert.ToString(sfamasterforhr.Field<string>("Branch")),
                                              DOL = Convert.ToString(sfamasterforhr.Field<string>("DOL")),
                                              DOJ = Convert.ToString(sfamasterforhr.Field<string>("DOJ")),
                                              DOB = Convert.ToString(sfamasterforhr.Field<string>("DOB")) == "" ?
                                                "" : AMBOEcryption.DecryptData(sfamasterforhr.Field<string>("DOB"), true),
                                              City = Convert.ToString(sfamasterforhr.Field<string>("City")),
                                              Division = Convert.ToString(sfamasterforhr.Field<string>("Division")),
                                              //Gender = Convert.ToString(sfamasterforhr.Field<string>("Gender")) == "" ?
                                              //  "" : AMBOEcryption.DecryptData(sfamasterforhr.Field<string>("Gender"), true),
                                              SourceName = Convert.ToString(sfamasterforhr.Field<string>("SourceName")) == "" ?
                                                "" : AMBOEcryption.DecryptData(sfamasterforhr.Field<string>("SourceName"), true),
                                              Education = Convert.ToString(sfamasterforhr.Field<string>("Education")) == "" ?
                                                "" : AMBOEcryption.DecryptData(sfamasterforhr.Field<string>("Education"), true),
                                              Experience = Convert.ToString(sfamasterforhr.Field<string>("Experience")) == "" ?
                                                "" : AMBOEcryption.DecryptData(sfamasterforhr.Field<string>("Experience"), true),
                                              SFALevel = Convert.ToString(sfamasterforhr.Field<string>("SFASubLevelName")),
                                              Address = Convert.ToString(sfamasterforhr.Field<string>("Address")) == "" ?
                                                "" : AMBOEcryption.DecryptData(sfamasterforhr.Field<string>("Address"), true),
                                              EmailId = Convert.ToString(sfamasterforhr.Field<string>("MailId")) == "" ?
                                                "" : AMBOEcryption.DecryptData(sfamasterforhr.Field<string>("MailId"), true),
                                              MobileNumber = Convert.ToString(sfamasterforhr.Field<string>("MobileNumber")),
                                              Source = Convert.ToString(sfamasterforhr.Field<string>("Source")) == "" ?
                                                "" : AMBOEcryption.DecryptData(sfamasterforhr.Field<string>("Source"), true),
                                              SourceCode = Convert.ToString(sfamasterforhr.Field<string>("SourceCode")) == "" ?
                                                "" : AMBOEcryption.DecryptData(sfamasterforhr.Field<string>("SourceCode"), true),
                                              AssetIssued = Convert.ToString(sfamasterforhr.Field<string>("MobileIssued")) == "" ?
                                                "" : AMBOEcryption.DecryptData(sfamasterforhr.Field<string>("MobileIssued"), true),
                                              //FatherName = Convert.ToString(sfamasterforhr.Field<string>("FatherName")) == "" ?
                                              //  "" : AMBOEcryption.DecryptData(sfamasterforhr.Field<string>("FatherName"), true),
                                              LevelofEducation = Convert.ToString(sfamasterforhr.Field<string>("LevelofEducation")) == "" ?
                                                "" : AMBOEcryption.DecryptData(sfamasterforhr.Field<string>("LevelofEducation"), true),
                                              BankName = Convert.ToString(sfamasterforhr.Field<string>("BankName")) == "" ?
                                                "" : AMBOEcryption.DecryptData(sfamasterforhr.Field<string>("BankName"), true),
                                              BankAccountNo = Convert.ToString(sfamasterforhr.Field<string>("BankAccountNo")) == "" ?
                                                "" : AMBOEcryption.DecryptData(sfamasterforhr.Field<string>("BankAccountNo"), true),
                                              ESIAccountNo = Convert.ToString(sfamasterforhr.Field<string>("ESIAccountNo")) == "" ?
                                                "" : AMBOEcryption.DecryptData(sfamasterforhr.Field<string>("ESIAccountNo"), true),
                                              PFAccountNo = Convert.ToString(sfamasterforhr.Field<string>("PFAccountNo")) == "" ?
                                                "" : AMBOEcryption.DecryptData(sfamasterforhr.Field<string>("PFAccountNo"), true),
                                              MedicalInsuranceNo = Convert.ToString(sfamasterforhr.Field<string>("MedicalInsuranceNo")) == "" ?
                                                "" : AMBOEcryption.DecryptData(sfamasterforhr.Field<string>("MedicalInsuranceNo"), true),
                                              MICoverageFrom = Convert.ToString(sfamasterforhr.Field<string>("MICoverageFrom")) == "" ?
                                                "" : AMBOEcryption.DecryptData(sfamasterforhr.Field<string>("MICoverageFrom"), true),
                                              MICoverageTo = Convert.ToString(sfamasterforhr.Field<string>("MICoverageTo")) == "" ?
                                                "" : AMBOEcryption.DecryptData(sfamasterforhr.Field<string>("MICoverageTo"), true),
                                              PersonalInsuranceNo = Convert.ToString(sfamasterforhr.Field<string>("PersonalInsuranceNo")) == "" ?
                                                "" : AMBOEcryption.DecryptData(sfamasterforhr.Field<string>("PersonalInsuranceNo"), true),
                                              PICoverageFrom = Convert.ToString(sfamasterforhr.Field<string>("PICoverageFrom")) == "" ?
                                                "" : AMBOEcryption.DecryptData(sfamasterforhr.Field<string>("PICoverageFrom"), true),
                                              PICoverageTo = Convert.ToString(sfamasterforhr.Field<string>("PICoverageTo")) == "" ?
                                                "" : AMBOEcryption.DecryptData(sfamasterforhr.Field<string>("PICoverageTo"), true),
                                          });
                }
            }
            catch (Exception ex)
            {

            }

            return sfaMasterforHRList;
        }

        public DataTable ManageSFAMasterforHRData(List<SFAMasterForHR> InputParam)
        {
            DataRow row;
            DataTable dtSFADetails = new DataTable();
            DataTable dtResult = new DataTable();

            try
            {
                dtSFADetails.Columns.Add("UserId");
                dtSFADetails.Columns.Add("SFACode");
                dtSFADetails.Columns.Add("SFAName");
                dtSFADetails.Columns.Add("Source");
                dtSFADetails.Columns.Add("SourceName");
                dtSFADetails.Columns.Add("SourceCode");
                dtSFADetails.Columns.Add("Branch");
                dtSFADetails.Columns.Add("City");
                dtSFADetails.Columns.Add("Division");
                //dtSFADetails.Columns.Add("Gender");
                //dtSFADetails.Columns.Add("FatherName");
                dtSFADetails.Columns.Add("LevelofEducation");
                dtSFADetails.Columns.Add("Education");
                dtSFADetails.Columns.Add("Experience");
                dtSFADetails.Columns.Add("BankName");
                dtSFADetails.Columns.Add("BankAccountNo");
                dtSFADetails.Columns.Add("ESICAccountNo");
                dtSFADetails.Columns.Add("PFAAccountNo");
                dtSFADetails.Columns.Add("MedicalInsurance");
                dtSFADetails.Columns.Add("MICoverageFrom");
                dtSFADetails.Columns.Add("MICoverageTo");
                dtSFADetails.Columns.Add("PersonalInsuranceNo");
                dtSFADetails.Columns.Add("PICoverageFrom");
                dtSFADetails.Columns.Add("PICoverageTo");
                if (InputParam.Count > 0)
                {
                    foreach (SFAMasterForHR details in InputParam)
                    {
                        row = dtSFADetails.NewRow();
                        row["UserId"] = details.UserId;
                        row["SFACode"] = details.SFACode;
                        row["SFAName"] = details.SFAName;
                        row["Source"] = AMBOEcryption.EncryptData((details.Source != null ? details.Source.Trim() : null), true);
                        row["SourceName"] = AMBOEcryption.EncryptData((details.SourceName != null ? details.SourceName.Trim() : null), true);
                        row["SourceCode"] = AMBOEcryption.EncryptData((details.SourceCode != null ? details.SourceCode.Trim() : null), true);
                        row["Branch"] = details.Branch;
                        row["City"] = details.City;
                        row["Division"] = details.Division;
                        //row["Gender"] = AMBOEcryption.EncryptData((details.Gender != null ? details.Gender.Trim() : null), true);
                        //row["FatherName"] = AMBOEcryption.EncryptData((details.FatherName != null ? details.FatherName.Trim() : null), true);
                        row["LevelofEducation"] = AMBOEcryption.EncryptData((details.LevelofEducation != null ? details.LevelofEducation.Trim() : null), true);
                        row["Education"] = AMBOEcryption.EncryptData((details.Education != null ? details.Education.Trim() : null), true); 
                        row["Experience"] = AMBOEcryption.EncryptData((details.Experience != null ? details.Experience.Trim() : null), true); 
                        row["BankName"] = AMBOEcryption.EncryptData((details.BankName != null ? details.BankName.Trim() : null), true);
                        row["BankAccountNo"] = AMBOEcryption.EncryptData((details.BankAccountNo != null ? details.BankAccountNo.Trim() : null), true);
                        row["ESICAccountNo"] = AMBOEcryption.EncryptData((details.ESIAccountNo != null ? details.ESIAccountNo.Trim() : null), true);
                        row["PFAAccountNo"] = AMBOEcryption.EncryptData((details.PFAccountNo != null ? details.PFAccountNo.Trim() : null), true);
                        row["MedicalInsurance"] = AMBOEcryption.EncryptData((details.MedicalInsuranceNo != null ? details.MedicalInsuranceNo.Trim() : null), true);
                        row["MICoverageFrom"] = AMBOEcryption.EncryptData((details.MICoverageFrom != null ? details.MICoverageFrom.Trim() : null), true);
                        row["MICoverageTo"] = AMBOEcryption.EncryptData((details.MICoverageTo != null ? details.MICoverageTo.Trim() : null), true);
                        row["PersonalInsuranceNo"] = AMBOEcryption.EncryptData((details.PersonalInsuranceNo != null ? details.PersonalInsuranceNo.Trim() : null), true);
                        row["PICoverageFrom"] = AMBOEcryption.EncryptData((details.PICoverageFrom != null ? details.PICoverageFrom.Trim() : null), true);
                        row["PICoverageTo"] = AMBOEcryption.EncryptData((details.PICoverageTo != null ? details.PICoverageTo.Trim() : null), true);
                        dtSFADetails.Rows.Add(row);
                    }
                }

                SqlParameter[] objDBParam = new SqlParameter[1];
                objDBParam[0] = new SqlParameter("@SFADetails", dtSFADetails);

                dtResult = _dataHelper.ExecuteProcedure("[dbo].[ManageSFAMasterforHRData]", objDBParam);
                if (dtResult.Rows.Count > 0)
                    return dtResult;
                else
                    return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataTable ManageSFADetails(List<EncryptionInput> InputParam)
        {
            DataRow row;
            DataTable dtSFADetails = new DataTable();
            DataTable dtResult = new DataTable();

            try
            {
                dtSFADetails.Columns.Add("LoginId");
                dtSFADetails.Columns.Add("Gender");
                dtSFADetails.Columns.Add("FatherName");
                dtSFADetails.Columns.Add("EncryptGender");
                dtSFADetails.Columns.Add("EncryptFatherName");
                
                if (InputParam.Count > 0)
                {
                    foreach (EncryptionInput details in InputParam)
                    {
                        row = dtSFADetails.NewRow();
                        row["LoginId"] = details.LoginId;
                        row["Gender"] = details.Gender;
                        row["FatherName"] = details.FatherName;
                        row["EncryptGender"] = AMBOEcryption.EncryptData((details.Gender != null ? details.Gender.Trim() : null), true);
                        row["EncryptFatherName"] = AMBOEcryption.EncryptData((details.FatherName != null ? details.FatherName.Trim() : null), true);
                        
                        dtSFADetails.Rows.Add(row);
                    }
                }

                SqlParameter[] objDBParam = new SqlParameter[1];
                objDBParam[0] = new SqlParameter("@TempSFADetails", dtSFADetails);

                dtResult = _dataHelper.ExecuteProcedure("[dbo].[GetGenderFatherName]", objDBParam);
                if (dtResult.Rows.Count > 0)
                    return dtResult;
                else
                    return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion SFA Master for HR API

        public MenusList GetModuleRightsByRole(ModuleRightsByRoleInput ModuleRightsByRoleInput, out string Message)
        {
            DbParameterCollection objDbParam = new DbParameterCollection();
            MenusList data = new MenusList();
            try
            {
                objDbParam.Add(new DbParameter("@RoleId", ModuleRightsByRoleInput.RoleId, DbType.Int64));
                var output = _dataHelper.ExecuteDataTable("dbo.GetModuleRightsByRole", objDbParam, CommandType.StoredProcedure);
                if (output == null || output.Rows.Count == 0)
                {
                    Message = "No data returned from database.";
                    return null;
                }
                List<ModuleSubModule> totalList = new List<ModuleSubModule>();
                // totalList = CommonMethod.ConvertTo<ModuleSubModule>(datatable);
                totalList = (from item in output.AsEnumerable()
                             select new ModuleSubModule
                             {
                                 ModuleId = item.Field<Int64>("ModuleId"),
                                 ModuleName = item.Field<string>("ModuleName"),
                                 MenuIcon = item.Field<string>("ModuleIcon"),
                                 ModuleSequence = item.Field<int>("ModuleSequence"),
                                 ModuleUrl = item.Field<string>("ModuleUrl"),
                                 SubModuleId = item.Field<Int64>("SubModuleId"),
                                 SubModuleName = item.Field<string>("SubModuleName"),
                                 SubModuleSequence = item.Field<int>("SubModuleSequence"),
                                 SubModuleUrl = item.Field<string>("SubModuleUrl"),
                                 SubMenuIcon = item.Field<string>("SubModuleIcon"),
                                 IsActive = item.Field<bool>("IsActive")
                             }).ToList();

                //preparing Module list
                List<ModuleList> MdlList = new List<ModuleList>();
                ModuleList mdl;
                var ml = totalList.Select(m => new { m.ModuleId, m.ModuleName, m.ModuleUrl, m.ModuleSequence, m.MenuIcon }).Distinct().ToList().OrderBy(m => m.ModuleSequence);
                foreach (var modules in ml)
                {
                    mdl = new ModuleList();
                    mdl.ModuleId = modules.ModuleId;
                    mdl.ModuleName = modules.ModuleName;
                    mdl.ModuleUrl = modules.ModuleUrl;
                    mdl.ModuleSequence = modules.ModuleSequence;
                    mdl.MenuIcon = modules.MenuIcon;
                    //mdl.IsActive = modules.IsActive;
                    if (mdl.ModuleUrl != "#" && mdl.ModuleUrl != null)
                    {
                        string[] words = mdl.ModuleUrl.Split('/');
                        mdl.ActionName = words[2].ToString();
                        mdl.ControllerName = words[1].ToString();
                    }
                    else
                    {
                        mdl.ActionName = "#"; mdl.ControllerName = "#";
                    }
                    MdlList.Add(mdl);
                }
                data.ModuleList = MdlList;
                //preparing the submodule list
                List<ModuleSubModule> MdlSbMdlList = new List<ModuleSubModule>();
                ModuleSubModule MdlSbMdlList1;
                var sm = totalList.Where(m => m.SubModuleId != 0).ToList().OrderBy(m => m.SubModuleSequence);
                foreach (var sml in sm)
                {
                    MdlSbMdlList1 = new ModuleSubModule();
                    MdlSbMdlList1.ModuleId = sml.ModuleId;
                    MdlSbMdlList1.ModuleName = sml.ModuleName;
                    MdlSbMdlList1.ModuleUrl = sml.ModuleUrl;
                    MdlSbMdlList1.ModuleSequence = sml.ModuleSequence;
                    MdlSbMdlList1.SubModuleId = sml.SubModuleId;
                    MdlSbMdlList1.SubModuleName = sml.SubModuleName;
                    MdlSbMdlList1.SubModuleUrl = sml.SubModuleUrl;
                    MdlSbMdlList1.SubModuleSequence = sml.SubModuleSequence;
                    MdlSbMdlList1.SubMenuIcon = sml.SubMenuIcon;
                    MdlSbMdlList1.IsActive = sml.IsActive;
                    if (MdlSbMdlList1.SubModuleUrl != "#" && MdlSbMdlList1.SubModuleUrl != null)
                    {
                        string[] words = MdlSbMdlList1.SubModuleUrl.Split('/');
                        MdlSbMdlList1.ActionName = words[2].ToString();
                        MdlSbMdlList1.ControllerName = words[1].ToString();
                    }
                    else
                    {
                        MdlSbMdlList1.ActionName = "#"; MdlSbMdlList1.ControllerName = "#";
                    }
                    MdlSbMdlList.Add(MdlSbMdlList1);
                }
                data.SubModuleList = MdlSbMdlList;
                Message = "Module rights fetched successfully.";
            }
            catch (Exception ex)
            {
                Message = "Something went wrong, please try again.";
            }
            return data;
        }

    }
}
