using System;
using System.Data;
using AmboLibrary.UserValidation;
using AmboProvider.Interface;
using AmboUtilities.Helper;
using DBHelper;
using System.Linq;

namespace AmboProvider.Implimentation
{
    /// <summary>
    /// To perform User validation related operations like validate Login, change password etc.
    /// Dhruv Sharma, ValueFirst, Gurugram
    /// </summary>
    public class UserValidationProvider: IUserValidationProvider
    {
        private readonly DbHelper _dataHelper;

        private readonly IErrorLogProvider _IErrorLogProvider;

        public UserValidationProvider(IErrorLogProvider IErrorLogProvider)
        {
            _dataHelper = DbHelper.GetInstance("defaultConnection");
            _IErrorLogProvider = IErrorLogProvider;
        }

        /// <summary>
        /// To validate user login for both Mobile and Web.
        /// </summary>
        /// <param name="UserDetails">User Cridentials</param>
        /// <param name="Message">Error Message</param>
        /// <returns>User Id, Name, Role Id, Role Name</returns>
        public UserDetailsModel ValidateLogin(UserValidationModel UserDetails, out string Message)
        {
            UserDetailsModel UserData = new UserDetailsModel();
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@LoginMode", UserDetails.LoginMode, DbType.Boolean));
                objDBParam.Add(new DbParameter("@UserName", UserDetails.UserName, DbType.String));

                UserDetails.UserPassword = AMBOEcryption.GetHashValue(UserDetails.UserPassword.Trim());

                objDBParam.Add(new DbParameter("@UserPassword", UserDetails.UserPassword, DbType.String));
                objDBParam.Add(new DbParameter("@EncryptKey", UserDetails.EncryptKey, DbType.String));
                objDBParam.Add(new DbParameter("@SessionTime", UserDetails.SessionTime, DbType.DateTime));
                objDBParam.Add(new DbParameter("@IMEI", UserDetails.IMEI, DbType.String));

                var outputFromSP = _dataHelper.ExecuteDataSet("[dbo].[ValidateUserLogin]", objDBParam, CommandType.StoredProcedure);

                if (outputFromSP.Tables.Count > 0)
                {
                    Message = Convert.ToString(outputFromSP.Tables[0].Rows[0]["Message"]);
                    if (Convert.ToBoolean(outputFromSP.Tables[0].Rows[0]["Status"]))
                    {
                        UserData.Name = Convert.ToString(outputFromSP.Tables[0].Rows[0]["Name"]);
                        UserData.UserId = Convert.ToInt64(outputFromSP.Tables[0].Rows[0]["Id"]);
                        UserData.RoleName = Convert.ToString(outputFromSP.Tables[0].Rows[0]["RoleName"]);
                        UserData.RoleId = Convert.ToInt64(outputFromSP.Tables[0].Rows[0]["RoleId"]);
                        UserData.LastLoginDate = Convert.ToDateTime(outputFromSP.Tables[0].Rows[0]["LastLoginDate"]);

                        UserData.listRoleRights = (from rolerights in outputFromSP.Tables[1].AsEnumerable()
                                                        select new RoleRights
                                                        {
                                                            RoleId = rolerights.Field<Int64>("RoleId"),
                                                            ModuleId = rolerights.Field<Int64>("ModuleId"),
                                                            SubModuleId = rolerights.Field<Int64>("SubModuleId"),
                                                            ViewPermission = rolerights.Field<bool>("ViewPermission"),
                                                            CreatePermission = rolerights.Field<bool>("CreatePermission"),
                                                            EditPermission = rolerights.Field<bool>("EditPermission"),
                                                            DeletePermission = rolerights.Field<bool>("DeletePermission")
                                                        }).ToList();
                    }
                    UserData.Status = Convert.ToInt32(outputFromSP.Tables[0].Rows[0]["Status"]);
                    UserData.Message = Convert.ToString(outputFromSP.Tables[0].Rows[0]["Message"]);

                }
                
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("ValidateLogin", ex.StackTrace, ex.Message);
                UserData.Status = 0;
                UserData.Message = Convert.ToString("Something went wrong. Please try again.");
            }
            return UserData;
        }
    }
}
