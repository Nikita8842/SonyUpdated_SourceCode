using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBHelper;
using AmboProvider.Interface;
using AmboLibrary.MasterMaintenance;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using AmboUtilities.Helper;
using System.IO;
using AmboLibrary.WebReports;

namespace AmboProvider.Implimentation
{
    /// <summary>
    /// Class contains Master Maintenance task only
    /// Manbeer Singh Makhloga 25th January 2018
    /// </summary>
    public class MasterMaintenanceProvider : IMasterMaintenanceProvider
    {
        private readonly DbHelper _dataHelper;
        private readonly IErrorLogProvider _IErrorLogProvider;

        public MasterMaintenanceProvider(IErrorLogProvider IErrorLogProvider)
        {
            _dataHelper = DbHelper.GetInstance("defaultConnection");
            _IErrorLogProvider = IErrorLogProvider;
        }

        #region State Master API
        public int CreateState(CreateStateForm stateData, out string Message)
        {
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            //IDbTransaction transaction = _dataHelper.BeginTransaction();
            try
            {
                objDBParam.Add(new DbParameter("@StateName", stateData.StateName, DbType.String));
                objDBParam.Add(new DbParameter("@RegionID", stateData.RegionID, DbType.Int32));
                objDBParam.Add(new DbParameter("@IsActive", stateData.IsActive));
                objDBParam.Add(new DbParameter("@CreatedBy", stateData.UserId, DbType.Int32));
                
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[CreateState]", objDBParam, CommandType.StoredProcedure);

                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return 1;
                //_dataHelper.CommitTransaction(transaction);
            }
            catch (Exception ex)
            {
                //_dataHelper.RollbackTransaction(transaction);
                _IErrorLogProvider.CreateErrorLog("CreateState", ex.StackTrace, ex.Message);
                Message = "Could not create new state. Exception occured in API.";
            }
            return 0;
        }

        public int DeleteState(DeleteStateForm stateData, out string Message)
        {
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@StateId", stateData.ID, DbType.Int32));
                objDBParam.Add(new DbParameter("@UserId", stateData.UserId, DbType.Int32));

                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[DeleteState]", objDBParam, CommandType.StoredProcedure);

                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return 1;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("DeleteState", ex.StackTrace, ex.Message);
                Message = "Could not delete state. Exception occured in API.";
            }
            return 0;
        }

        public IEnumerable<StateMaster> GetAllStates()
        {
            IEnumerable<StateMaster> listStates = null;
            try
            {
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetState]", CommandType.StoredProcedure);
                if(outputFromSP.Rows.Count > 0)
                {
                    listStates = (from state in outputFromSP.AsEnumerable()
                                  select new StateMaster
                                  {
                                      ID = Convert.ToInt64(state.Field<Int64>("ID")),
                                      StateName = Convert.ToString(state.Field<string>("STATENAME")),
                                      RegionID = Convert.ToInt64(state.Field<Int64>("REGIONID")),
                                      isActive = Convert.ToBoolean(state.Field<bool>("ISACTIVE"))
                                  });
                }
            }
            catch(Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetAllStates", ex.StackTrace, ex.Message);
            }
            return listStates;
        }

        public StateMaster GetStateById(Int64 stateId)
        {
            DbParameterCollection objDBParam = new DbParameterCollection();
            StateMaster outputState = null;
            try
            {
                objDBParam.Add(new DbParameter("@StateId", stateId, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetState]", objDBParam, CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    outputState = (from state in outputFromSP.AsEnumerable()
                             select new StateMaster
                             {
                                 ID = Convert.ToInt64(state.Field<Int64>("ID")),
                                 StateName = Convert.ToString(state.Field<string>("STATENAME")),
                                 RegionID = Convert.ToInt64(state.Field<Int64>("REGIONID")),
                                 isActive = Convert.ToBoolean(state.Field<bool>("ISACTIVE"))
                             }).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetStateById", ex.StackTrace, ex.Message);
            }
            return outputState;
        }

        public int UpdateState(UpdateStateForm stateData, out string Message)
        {
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@StateID", stateData.ID, DbType.Int32));
                objDBParam.Add(new DbParameter("@StateName", stateData.StateName, DbType.String));
                objDBParam.Add(new DbParameter("@RegionID", stateData.RegionID, DbType.Int32));
                objDBParam.Add(new DbParameter("@IsActive", stateData.IsActive));
                objDBParam.Add(new DbParameter("@UserID", stateData.UserId, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[UpdateState]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return 1;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("UpdateState", ex.StackTrace, ex.Message);
                Message = "Could not update state data. Exception occured in API.";
            }
            return 0;
        }
        #endregion State Master API

        #region Branch Master API
        /// <summary>
        /// To Add New Brach in Master
        /// </summary>
        /// <param name="List"></param>
        /// <returns></returns>
        public int AddNewBrachMaster(CreateBranchForm list, out string Message)
        {
            Message = string.Empty;
            DbParameterCollection sqlparam = new DbParameterCollection();
            try
            {
                sqlparam.Add(new DbParameter("@BranchCode", list.BranchCode.Trim(), DbType.String));
                sqlparam.Add(new DbParameter("@BranchName", list.BranchName.Trim(), DbType.String));
                sqlparam.Add(new DbParameter("@IsActive", list.IsActive));
                sqlparam.Add(new DbParameter("@UserId", list.UserId, DbType.Int32));

                var datatable = _dataHelper.ExecuteDataSet("[dbo].[InsertNewBranch]", sqlparam, CommandType.StoredProcedure);
                Message = Convert.ToString(datatable.Tables[0].Rows[0]["Message"]);
                if (Convert.ToBoolean(datatable.Tables[0].Rows[0]["MessageCode"]))
                    return 1;
            }
            catch(Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("AddNewBrachMaster", ex.StackTrace, ex.Message);
                
            }
            return 0;
        }

        /// <summary>
        /// To Get All Branch details
        /// </summary>
        /// <returns></returns>
        public GetBranchDetail GetAllBranchDetails()
        {
            var getList = new GetBranchDetail { List = null };
            try
            {

                var datatable = _dataHelper.ExecuteDataSet("[dbo].[GetAllBranchDetails]", CommandType.StoredProcedure);

                if (datatable.Tables[0].Rows.Count > 0)
                {
                    getList.List = (from item in datatable.Tables[0].AsEnumerable()
                                    select new MasterMaintenance
                                    {
                                        BranchId = Convert.ToInt64(item.Field<Int64>("Id")),
                                        BranchCode = Convert.ToString(item.Field<string>("BranchCode")),
                                        BranchName = Convert.ToString(item.Field<string>("BranchName")),
                                        IsActive = Convert.ToBoolean(item.Field<bool>("IsActive"))
                                    }).ToList();
                    return getList;
                }
                    
            }
            catch(Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetAllBranchDetails", ex.StackTrace, ex.Message);
            }
            return getList;
        }

        public int UpdateBrachMaster(UpdateBranchForm List, out string Message)
        {
            Message = string.Empty;
            DbParameterCollection sqlparam = new DbParameterCollection();
            try
            {
                sqlparam.Add(new DbParameter("@BranchCode", List.BranchCode.Trim(), DbType.String));
                sqlparam.Add(new DbParameter("@BranchName", List.BranchName.Trim(), DbType.String));
                sqlparam.Add(new DbParameter("@UserId", List.UserId, DbType.Int32));
                sqlparam.Add(new DbParameter("@IsActive", List.IsActive));
                sqlparam.Add(new DbParameter("@Id", List.ID, DbType.Int32));

                var datatable = _dataHelper.ExecuteDataSet("[dbo].[UpdateBrachDetail]", sqlparam, CommandType.StoredProcedure);
                Message = Convert.ToString(datatable.Tables[0].Rows[0]["Message"]);
                if (Convert.ToBoolean(datatable.Tables[0].Rows[0]["MessageCode"]))
                    return 1;
            }
            catch(Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("UpdateBrachMaster", ex.StackTrace, ex.Message);
            }
            return 0;
        }

        /// <summary>
        /// Method to delete respective Branch
        /// </summary>
        /// <param name="List"></param>
        /// <returns></returns>
        public int DeleteBrach(DeleteBranchForm List, out string Message)
        {
            DbParameterCollection sqlparam = new DbParameterCollection();
            try
            {
                sqlparam.Add(new DbParameter("@Id", List.ID, DbType.Int32));
                sqlparam.Add(new DbParameter("@UserId", List.UserId, DbType.Int32));

                var datatable = _dataHelper.ExecuteDataSet("[dbo].[DeleteBranch]", sqlparam, CommandType.StoredProcedure);

                Message = Convert.ToString(datatable.Tables[0].Rows[0]["Message"]);
                if (Convert.ToBoolean(datatable.Tables[0].Rows[0]["MessageCode"]))
                    return 1;
            }
            catch(Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("DeleteBranch", ex.StackTrace, ex.Message);
                Message = ex.Message;
            }
            return 0;
        }
        #endregion Branch Master API

        #region Region Master API
        public int CreateRegion(RegionMaster regionData, out string Message)
        {
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@RegionName", regionData.RegionName, DbType.String));
                objDBParam.Add(new DbParameter("@IsActive", regionData.IsActive));
                objDBParam.Add(new DbParameter("@CreatedBy", regionData.UserId, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[CreateRegion]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return 1;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("CreateRegion", ex.StackTrace, ex.Message);
                Message = ex.Message;
            }
            return 0;
        }

        public int UpdateRegion(RegionMaster regionData, out string Message)
        {
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@RegionID", regionData.ID, DbType.Int32));
                objDBParam.Add(new DbParameter("@RegionName", regionData.RegionName, DbType.String));
                objDBParam.Add(new DbParameter("@IsActive", regionData.IsActive));
                objDBParam.Add(new DbParameter("@UserID", regionData.UserId, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[UpdateRegion]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return 1;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("UpdateRegion", ex.StackTrace, ex.Message);
                Message = "Could not update state data. Exception occured in API.";
            }
            return 0;
        }

        public int DeleteRegion(RegionMaster regionData, out string Message)
        {
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@RegionId", regionData.ID, DbType.Int32));
                objDBParam.Add(new DbParameter("@UserId", regionData.UserId, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[DeleteRegion]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return 1;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("DeleteRegion", ex.StackTrace, ex.Message);
                Message = "Could not delete region. Exception occured in API.";
            }
            return 0;
        }

        public IEnumerable<RegionMaster> GetAllRegions()
        {
            IEnumerable<RegionMaster> listRegions = null;
            try
            {
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetRegion]", CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    listRegions = (from region in outputFromSP.AsEnumerable()
                                  select new RegionMaster
                                  {
                                      //commented as SP is giving different result, thus this code was not working
                                      //ID = Convert.ToInt64(region.Field<Int64>("ID")),
                                      //RegionName = Convert.ToString(region.Field<string>("REGIONNAME")),
                                      //IsActive = Convert.ToBoolean(region.Field<bool>("ISACTIVE")),
                                      //CreatedBy = Convert.ToInt64(region.Field<Int64>("CREATEDBY")),
                                      //CreationDate = Convert.ToDateTime(region.Field<DateTime>("CREATIONDATE")),
                                      //LastModifiedBy = Convert.ToInt64(region.Field<Int64?>("LASTMODIFIEDBY")),
                                      //LastModificationDate = Convert.ToDateTime(region.Field<DateTime?>("LASTMODDATE"))

                                      ID = region.Field<Int64>("RegionId"),
                                      RegionName = region.Field<string>("RegionName")
                                  });
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetAllRegions", ex.StackTrace, ex.Message);
            }
            return listRegions;
        }

        public RegionMaster GetRegionById(Int64 regionId)
        {
            DbParameterCollection objDBParam = new DbParameterCollection();
            RegionMaster outputRegion = null;
            try
            {
                objDBParam.Add(new DbParameter("@RegionId", regionId, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetRegion]", objDBParam, CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    outputRegion = (from region in outputFromSP.AsEnumerable()
                                   select new RegionMaster
                                   {
                                       //commented as SP is giving different result, thus this code was not working
                                       //ID = Convert.ToInt64(region.Field<Int64>("ID")),
                                       //RegionName = Convert.ToString(region.Field<string>("REGIONNAME")),
                                       //IsActive = Convert.ToBoolean(region.Field<bool>("ISACTIVE")),
                                       //CreatedBy = Convert.ToInt64(region.Field<Int64>("CREATEDBY")),
                                       //CreationDate = Convert.ToDateTime(region.Field<DateTime>("CREATIONDATE")),
                                       //LastModifiedBy = Convert.ToInt64(region.Field<Int64?>("LASTMODIFIEDBY")),
                                       //LastModificationDate = Convert.ToDateTime(region.Field<DateTime?>("LASTMODDATE"))

                                       ID = region.Field<Int64>("RegionId"),
                                       RegionName = region.Field<string>("RegionName")
                                   }).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetRegionById", ex.StackTrace, ex.Message);
            }
            return outputRegion;
        }
        #endregion Region Master API

        #region City Master API
        public int CreateCity(CreateCityForm cityData, out string Message)
        {
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@CityName", cityData.CityName, DbType.String));
                objDBParam.Add(new DbParameter("@CityTypeID", cityData.CityTypeId, DbType.Int32));
                objDBParam.Add(new DbParameter("@StateID", cityData.StateId, DbType.String));
                objDBParam.Add(new DbParameter("@IsActive", cityData.IsActive));
                objDBParam.Add(new DbParameter("@CreatedBy", cityData.UserId, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[CreateCity]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return 1;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("CreateCity", ex.StackTrace, ex.Message);
                Message = ex.Message;
            }
            return 0;
        }

        public int UpdateCity(UpdateCityForm cityData, out string Message)
        {
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@CityID", cityData.ID, DbType.Int32));
                objDBParam.Add(new DbParameter("@CityName", cityData.CityName, DbType.String));
                objDBParam.Add(new DbParameter("@CityTypeID", cityData.CityTypeId, DbType.Int32));
                objDBParam.Add(new DbParameter("@StateID", cityData.StateId, DbType.Int32));
                objDBParam.Add(new DbParameter("@IsActive", cityData.IsActive));
                objDBParam.Add(new DbParameter("@UserID", cityData.UserId, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[UpdateCity]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return 1;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("UpdateCity", ex.StackTrace, ex.Message);
                Message = "Could not update city data. Exception occured in API.";
            }
            return 0;
        }

        public int DeleteCity(DeleteCityForm cityData, out string Message)
        {
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@CityId", cityData.ID, DbType.Int32));
                objDBParam.Add(new DbParameter("@UserId", cityData.UserId, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[DeleteCity]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return 1;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("DeleteCity", ex.StackTrace, ex.Message);
                Message = "Could not delete city. Exception occured in API.";
            }
            return 0;
        }

        public IEnumerable<CityMaster> GetAllCities()
        {
            IEnumerable<CityMaster> listCities = null;
            try
            {
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetCity]", CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    listCities = (from city in outputFromSP.AsEnumerable()
                                   select new CityMaster
                                   {
                                       ID = Convert.ToInt64(city.Field<Int64>("ID")),
                                       CityName = Convert.ToString(city.Field<string>("CITYNAME")),
                                       StateId = Convert.ToInt64(city.Field<Int64>("STATEID")),
                                       CityTypeId = Convert.ToInt64(city.Field<Int64>("CITYTYPEID")),
                                       IsActive = Convert.ToBoolean(city.Field<bool>("ISACTIVE")),
                                       CityType = Convert.ToString(city.Field<string>("CityType")),
                                       Region = Convert.ToString(city.Field<string>("RegionName")),
                                       State = Convert.ToString(city.Field<string>("StateName"))
                                   });
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetAllCities", ex.StackTrace, ex.Message);
            }
            return listCities;
        }

        public CityMaster GetCityById(Int64 cityId)
        {
            DbParameterCollection objDBParam = new DbParameterCollection();
            CityMaster outputCity = null;
            try
            {
                objDBParam.Add(new DbParameter("@CityId", cityId, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetCity]", objDBParam, CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    outputCity = (from city in outputFromSP.AsEnumerable()
                                    select new CityMaster
                                    {
                                        ID = Convert.ToInt64(city.Field<Int64>("ID")),
                                        CityName = Convert.ToString(city.Field<string>("CITYNAME")),
                                        StateId = Convert.ToInt64(city.Field<Int64>("STATEID")),
                                        CityTypeId = Convert.ToInt64(city.Field<Int64>("CITYTYPEID")),
                                        IsActive = Convert.ToBoolean(city.Field<bool>("ISACTIVE"))
                                    }).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetCityById", ex.StackTrace, ex.Message);
            }
            return outputCity;
        }
        #endregion City Master API

        #region Location Master API
        public int CreateLocation(CreateLocationForm locationData, out string Message)
        {
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@LocationName", locationData.LocationName, DbType.String));
                objDBParam.Add(new DbParameter("@CityID", locationData.CityId, DbType.Int32));
                objDBParam.Add(new DbParameter("@IsActive", locationData.IsActive));
                objDBParam.Add(new DbParameter("@CreatedBy", locationData.UserId, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[CreateLocation]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return 1;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("CreateLocation", ex.StackTrace, ex.Message);
                Message = ex.Message;
            }
            return 0;
        }

        public int UpdateLocation(UpdateLocationForm locationData, out string Message)
        {
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@LocationID", locationData.ID, DbType.Int32));
                objDBParam.Add(new DbParameter("@LocationName", locationData.LocationName, DbType.String));
                objDBParam.Add(new DbParameter("@CityID", locationData.CityId, DbType.Int32));
                objDBParam.Add(new DbParameter("@IsActive", locationData.IsActive));
                objDBParam.Add(new DbParameter("@UserID", locationData.UserId, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[UpdateLocation]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return 1;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("UpdateLocation", ex.StackTrace, ex.Message);
                Message = "Could not update location data. Exception occured in API.";
            }
            return 0;
        }

        public int DeleteLocation(DeleteLocationForm locationData, out string Message)
        {
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@LocationId", locationData.ID, DbType.Int32));
                objDBParam.Add(new DbParameter("@UserId", locationData.UserId, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[DeleteLocation]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return 1;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("DeleteLocation", ex.StackTrace, ex.Message);
                Message = "Could not delete location. Exception occured in API.";
            }
            return 0;
        }

        public IEnumerable<LocationMaster> GetAllLocations()
        {
            IEnumerable<LocationMaster> listLocations = null;
            try
            {
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetLocation]", CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    listLocations = (from location in outputFromSP.AsEnumerable()
                                  select new LocationMaster
                                  {
                                      ID = Convert.ToInt64(location.Field<Int64>("ID")),
                                      LocationName = Convert.ToString(location.Field<string>("LOCATIONNAME")),
                                      CityId = Convert.ToInt64(location.Field<Int64>("CITYID")),
                                      IsActive = Convert.ToBoolean(location.Field<bool>("ISACTIVE"))
                                  });
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetAllLocations", ex.StackTrace, ex.Message);
            }
            return listLocations;
        }

        public LocationMaster GetLocationById(Int64 locationId)
        {
            DbParameterCollection objDBParam = new DbParameterCollection();
            LocationMaster outputLocation = null;
            try
            {
                objDBParam.Add(new DbParameter("@LocationId", locationId, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetLocation]", objDBParam, CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    outputLocation = (from location in outputFromSP.AsEnumerable()
                                  select new LocationMaster
                                  {
                                      ID = Convert.ToInt64(location.Field<Int64>("ID")),
                                      LocationName = Convert.ToString(location.Field<string>("LOCATIONNAME")),
                                      CityId = Convert.ToInt64(location.Field<Int64>("CITYID")),
                                      IsActive = Convert.ToBoolean(location.Field<bool>("ISACTIVE"))
                                  }).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetLocationById", ex.StackTrace, ex.Message);
            }
            return outputLocation;
        }
        #endregion Location Master API

        #region Product Category Master API
        public int CreateProductCategory(CreateProductCategoryForm productCategoryData, out string Message)
        {
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@CategoryName", productCategoryData.CategoryName, DbType.String));
                objDBParam.Add(new DbParameter("@DivisionID", productCategoryData.Division, DbType.String));
                objDBParam.Add(new DbParameter("@Description", productCategoryData.Description, DbType.String));
                objDBParam.Add(new DbParameter("@Status", productCategoryData.Status));
                objDBParam.Add(new DbParameter("@CreatedBy", productCategoryData.UserId, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[CreateProductCategory]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return 1;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("CreateProductCategory", ex.StackTrace, ex.Message);
                Message = ex.Message;
            }
            return 0;
        }

        public int UpdateProductCategory(UpdateProductCategoryForm productCategoryData, out string Message)
        {
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@CategoryID", productCategoryData.ID, DbType.Int32));
                objDBParam.Add(new DbParameter("@CategoryName", productCategoryData.CategoryName, DbType.String));
                objDBParam.Add(new DbParameter("@DivisionID", productCategoryData.Division, DbType.String));
                objDBParam.Add(new DbParameter("@Description", productCategoryData.Description, DbType.String));
                objDBParam.Add(new DbParameter("@Status", productCategoryData.Status));
                objDBParam.Add(new DbParameter("@UserID", productCategoryData.UserId, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[UpdateProductCategory]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return 1;
            } 
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("UpdateProductCategory", ex.StackTrace, ex.Message);
                Message = ex.Message;
            }
            return 0;
        }

        public int DeleteProductCategory(DeleteProductCategoryForm productCategoryData, out string Message)
        {
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@CategoryId", productCategoryData.ID, DbType.Int32));
                objDBParam.Add(new DbParameter("@UserId", productCategoryData.UserId, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[DeleteProductCategory]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return 1;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("DeleteProductCategory", ex.StackTrace, ex.Message);
                Message = "Could not delete product category. Exception occured in API.";
            }
            return 0;
        }

        public IEnumerable<ProductCategoryMaster> GetAllProductCategories()
        {
            IEnumerable<ProductCategoryMaster> listProductCategories = null;
            try
            {
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetProductCategory]", CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    listProductCategories = (from prodCategory in outputFromSP.AsEnumerable()
                                     select new ProductCategoryMaster
                                     {
                                         ID = Convert.ToInt64(prodCategory.Field<Int64>("ID")),
                                         Division = prodCategory.Field<string>("DIVISION"),
                                         CategoryName = Convert.ToString(prodCategory.Field<string>("CATEGORYNAME")),
                                         Description = Convert.ToString(prodCategory.Field<string>("DESCRIPTION")),
                                         Status = Convert.ToBoolean(prodCategory.Field<bool>("STATUS")),
                                         IsActive = Convert.ToBoolean(prodCategory.Field<bool>("ISACTIVE")),
                                         CreatedBy = Convert.ToInt64(prodCategory.Field<Int64>("CREATEDBY")),
                                         CreationDate = Convert.ToDateTime(prodCategory.Field<DateTime>("CREATIONDATE")),
                                         LastModifiedBy = Convert.ToInt64(prodCategory.Field<Int64?>("LASTMODIFIEDBY")),
                                         LastModificationDate = Convert.ToDateTime(prodCategory.Field<DateTime?>("LASTMODDATE"))
                                     });
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetAllProductCategories", ex.StackTrace, ex.Message);
            }
            return listProductCategories;
        }

        public ProductCategoryMaster GetProductCategoryById(Int64 productCategoryId)
        {
            DbParameterCollection objDBParam = new DbParameterCollection();
            ProductCategoryMaster outputProdCategory = null;
            try
            {
                objDBParam.Add(new DbParameter("@ProductCategoryId", productCategoryId, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetProductCategory]", objDBParam, CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    outputProdCategory = (from prodCategory in outputFromSP.AsEnumerable()
                                      select new ProductCategoryMaster
                                      {
                                          ID = Convert.ToInt64(prodCategory.Field<Int64>("ID")),
                                          Division = prodCategory.Field<string>("DIVISION"),
                                          CategoryName = Convert.ToString(prodCategory.Field<string>("CATEGORYNAME")),
                                          Description = Convert.ToString(prodCategory.Field<string>("DESCRIPTION")),
                                          Status = Convert.ToBoolean(prodCategory.Field<bool>("STATUS")),
                                          IsActive = Convert.ToBoolean(prodCategory.Field<bool>("ISACTIVE")),
                                          CreatedBy = Convert.ToInt64(prodCategory.Field<Int64>("CREATEDBY")),
                                          CreationDate = Convert.ToDateTime(prodCategory.Field<DateTime>("CREATIONDATE")),
                                          LastModifiedBy = Convert.ToInt64(prodCategory.Field<Int64?>("LASTMODIFIEDBY")),
                                          LastModificationDate = Convert.ToDateTime(prodCategory.Field<DateTime?>("LASTMODDATE"))
                                      }).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetProductCategoryById", ex.StackTrace, ex.Message);
            }
            return outputProdCategory;
        }
        #endregion Product Category Master API

        #region Product Sub Category Master API
        public int CreateProductSubCategory(ProductSubCategoryMaster productSubCategoryData, out string Message)
        {
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@SubCategoryName", productSubCategoryData.SubCategoryName, DbType.String));
                objDBParam.Add(new DbParameter("@ProductCategoryId", productSubCategoryData.ProductCategoryId, DbType.Int32));
                objDBParam.Add(new DbParameter("@Description", productSubCategoryData.Description, DbType.String));
                objDBParam.Add(new DbParameter("@Status", productSubCategoryData.Status));
                //objDBParam.Add(new DbParameter("@IsActive", productSubCategoryData.IsActive));
                objDBParam.Add(new DbParameter("@CreatedBy", productSubCategoryData.UserId, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[CreateProductSubCategory]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return 1;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("CreateProductSubCategory", ex.StackTrace, ex.Message);
                Message = ex.Message;
            }
            return 0;
        }

        public int UpdateProductSubCategory(ProductSubCategoryMaster productSubCategoryData, out string Message)
        {
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@SubCategoryID", productSubCategoryData.ID, DbType.Int32));
                objDBParam.Add(new DbParameter("@SubCategoryName", productSubCategoryData.SubCategoryName, DbType.String));
                objDBParam.Add(new DbParameter("@ProductCategoryId", productSubCategoryData.ProductCategoryId, DbType.Int32));
                objDBParam.Add(new DbParameter("@Description", productSubCategoryData.Description, DbType.String));
                objDBParam.Add(new DbParameter("@Status", productSubCategoryData.Status));
                //objDBParam.Add(new DbParameter("@IsActive", productSubCategoryData.IsActive));
                objDBParam.Add(new DbParameter("@UserID", productSubCategoryData.UserId, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[UpdateProductSubCategory]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return 1;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("UpdateProductSubCategory", ex.StackTrace, ex.Message);
                Message = ex.Message;
            }
            return 0;
        }

        public int DeleteProductSubCategory(ProductSubCategoryMaster productSubCategoryData, out string Message)
        {
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@SubCategoryId", productSubCategoryData.ID, DbType.Int32));
                objDBParam.Add(new DbParameter("@UserId", productSubCategoryData.UserId, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[DeleteProductSubCategory]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return 1;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("DeleteProductSubCategory", ex.StackTrace, ex.Message);
                Message = "Could not delete product sub category. Exception occured in API.";
            }
            return 0;
        }

        public IEnumerable<ProductSubCategoryMaster> GetAllProductSubCategories()
        {
            IEnumerable<ProductSubCategoryMaster> listProductSubCategories = null;
            try
            {
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetProductSubCategory]", CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    listProductSubCategories = (from prodSubCategory in outputFromSP.AsEnumerable()
                                             select new ProductSubCategoryMaster
                                             {
                                                 ID = Convert.ToInt64(prodSubCategory.Field<Int64>("ID")),
                                                 ProductCategoryId = Convert.ToInt64(prodSubCategory.Field<Int64>("PRODUCTCATEGORYID")),
                                                 SubCategoryName = Convert.ToString(prodSubCategory.Field<string>("SUBCATEGORYNAME")),
                                                 Description = Convert.ToString(prodSubCategory.Field<string>("DESCRIPTION")),
                                                 Status = Convert.ToBoolean(prodSubCategory.Field<bool>("STATUS")),
                                                 IsActive = Convert.ToBoolean(prodSubCategory.Field<bool>("ISACTIVE")),
                                                 CreatedBy = Convert.ToInt64(prodSubCategory.Field<Int64>("CREATEDBY")),
                                                 CreationDate = Convert.ToDateTime(prodSubCategory.Field<DateTime>("CREATIONDATE")),
                                                 LastModifiedBy = Convert.ToInt64(prodSubCategory.Field<Int64?>("LASTMODIFIEDBY")),
                                                 LastModificationDate = Convert.ToDateTime(prodSubCategory.Field<DateTime?>("LASTMODDATE"))
                                             });
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetAllProductSubCategories", ex.StackTrace, ex.Message);
            }
            return listProductSubCategories;
        }

        public ProductSubCategoryMaster GetProductSubCategoryById(Int64 productSubCategoryId)
        {
            DbParameterCollection objDBParam = new DbParameterCollection();
            ProductSubCategoryMaster outputProdSubCategory = null;
            try
            {
                objDBParam.Add(new DbParameter("@ProductSubCategoryId", productSubCategoryId, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetProductSubCategory]", objDBParam, CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    outputProdSubCategory = (from prodSubCategory in outputFromSP.AsEnumerable()
                                          select new ProductSubCategoryMaster
                                          {
                                              ID = Convert.ToInt64(prodSubCategory.Field<Int64>("ID")),
                                              ProductCategoryId = Convert.ToInt64(prodSubCategory.Field<Int64>("PRODUCTCATEGORYID")),
                                              SubCategoryName = Convert.ToString(prodSubCategory.Field<string>("SUBCATEGORYNAME")),
                                              Description = Convert.ToString(prodSubCategory.Field<string>("DESCRIPTION")),
                                              Status = Convert.ToBoolean(prodSubCategory.Field<bool>("STATUS")),
                                              IsActive = Convert.ToBoolean(prodSubCategory.Field<bool>("ISACTIVE")),
                                              CreatedBy = Convert.ToInt64(prodSubCategory.Field<Int64>("CREATEDBY")),
                                              CreationDate = Convert.ToDateTime(prodSubCategory.Field<DateTime>("CREATIONDATE")),
                                              LastModifiedBy = Convert.ToInt64(prodSubCategory.Field<Int64?>("LASTMODIFIEDBY")),
                                              LastModificationDate = Convert.ToDateTime(prodSubCategory.Field<DateTime?>("LASTMODDATE"))
                                          }).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetProductSubCategoryById", ex.StackTrace, ex.Message);
            }
            return outputProdSubCategory;
        }
        #endregion Product Sub Category Master API

        #region Material Master API
        public int CreateMaterial(MaterialMaster materialData, out string Message)
        {
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@MaterialCode", materialData.MaterialCode, DbType.String));
                objDBParam.Add(new DbParameter("@ProductCategoryId", materialData.ProductCategoryId, DbType.Int64));
                objDBParam.Add(new DbParameter("@ProductSubCategoryId", materialData.ProductSubCategoryId, DbType.Int64));
                objDBParam.Add(new DbParameter("@Material", materialData.Name, DbType.String));
                objDBParam.Add(new DbParameter("@MaterialDescription", materialData.Description, DbType.String));
                objDBParam.Add(new DbParameter("@MOP", materialData.MOP, DbType.String));
                objDBParam.Add(new DbParameter("@Status", materialData.Status));
                objDBParam.Add(new DbParameter("@SizeId", materialData.SizeId));
                objDBParam.Add(new DbParameter("@SegmentId", materialData.SegmentId));
                objDBParam.Add(new DbParameter("@ResolutionId", materialData.ResolutionId));
                objDBParam.Add(new DbParameter("@TVTypeId", materialData.TvTypeId));
                objDBParam.Add(new DbParameter("@InternetId", materialData.InternetId));
                objDBParam.Add(new DbParameter("@3DId", materialData.Id3D));
                objDBParam.Add(new DbParameter("@CreatedBy", materialData.UserId, DbType.Int32));
                objDBParam.Add(new DbParameter("@IsCombo", materialData.IsCombo));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[CreateMaterial]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return 1;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("CreateMaterial", ex.StackTrace, ex.Message);
                Message = ex.Message;
            }
            return 0;
        }

        public int UpdateMaterial(MaterialMaster materialData, out string Message)
        {
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@MaterialId", materialData.Id, DbType.Int32));
                objDBParam.Add(new DbParameter("@MaterialCode", materialData.MaterialCode, DbType.String));
                objDBParam.Add(new DbParameter("@ProductCategoryId", materialData.ProductCategoryId, DbType.Int32));
                objDBParam.Add(new DbParameter("@ProductSubCategoryId", materialData.ProductSubCategoryId, DbType.Int32));
                objDBParam.Add(new DbParameter("@Material", materialData.Name, DbType.String));
                objDBParam.Add(new DbParameter("@MaterialDescription", materialData.Description, DbType.String));
                objDBParam.Add(new DbParameter("@MOP", materialData.MOP, DbType.String));
                objDBParam.Add(new DbParameter("@Status", materialData.Status));
                objDBParam.Add(new DbParameter("@SizeId", materialData.SizeId));
                objDBParam.Add(new DbParameter("@SegmentId", materialData.SegmentId));
                objDBParam.Add(new DbParameter("@ResolutionId", materialData.ResolutionId));
                objDBParam.Add(new DbParameter("@TVTypeId", materialData.TvTypeId));
                objDBParam.Add(new DbParameter("@InternetId", materialData.InternetId));
                objDBParam.Add(new DbParameter("@3DId", materialData.Id3D));
                objDBParam.Add(new DbParameter("@ModifiedBy", materialData.UserId, DbType.Int32));
                objDBParam.Add(new DbParameter("@IsCombo", materialData.IsCombo));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[UpdateMaterial]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return 1;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("UpdateMaterial", ex.StackTrace, ex.Message);
                Message = ex.Message;
            }
            return 0;
        }

        public int DeleteMaterial(MaterialMaster materialData, out string Message)
        {
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@MaterialId", materialData.Id, DbType.Int32));
                objDBParam.Add(new DbParameter("@UserId", materialData.UserId, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[DeleteMaterial]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return 1;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("DeleteMaterial", ex.StackTrace, ex.Message);
                Message = "Could not delete material. Exception occured in API.";
            }
            return 0;
        }

        public IEnumerable<MaterialMaster> GetAllMaterials()
        {
            IEnumerable<MaterialMaster> listMaterials = null;
            try
            {
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetMaterial]", CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    listMaterials = (from material in outputFromSP.AsEnumerable()
                                                select new MaterialMaster
                                                {
                                                    Id = Convert.ToInt64(material.Field<Int64>("ID")),
                                                    MaterialCode = Convert.ToString(material.Field<string>("MATERIALCODE")),
                                                    MOP = Convert.ToString(material.Field<string>("MOP")),
                                                    Name = Convert.ToString(material.Field<string>("NAME")),
                                                    ProductCategoryId = Convert.ToInt64(material.Field<Int64>("PRODUCTCATEGORYID")),
                                                    ProductSubCategoryId = Convert.ToInt64(material.Field<Int64>("PRODUCTSUBCATEGORYID")),
                                                    Description = Convert.ToString(material.Field<string>("DESCRIPTION")),
                                                    Discontinued = Convert.ToBoolean(material.Field<bool>("DISCONTINUED")),
                                                    Status = Convert.ToBoolean(material.Field<bool>("STATUS")),
                                                    IsActive = Convert.ToBoolean(material.Field<bool>("ISACTIVE")),
                                                    CreatedBy = Convert.ToInt64(material.Field<Int64>("CREATEDBY")),
                                                    CreationDate = Convert.ToDateTime(material.Field<DateTime>("CREATIONDATE")),
                                                    LastModifiedBy = Convert.ToInt64(material.Field<Int64?>("LASTMODIFIEDBY")),
                                                    LastModificationDate = Convert.ToDateTime(material.Field<DateTime?>("LASTMODDATE"))
                                                });
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetAllMaterials", ex.StackTrace, ex.Message);
            }
            return listMaterials;
        }

        public MaterialMaster GetMaterialByMaterialCode(MaterialMaster InputParam)
        {
            DbParameterCollection objDBParam = new DbParameterCollection();
            MaterialMaster outputMaterial = null;
            try
            {
                objDBParam.Add(new DbParameter("@MaterialCode", InputParam.MaterialCode, DbType.String));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetDataForMaterialCode]", objDBParam, CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    outputMaterial = (from material in outputFromSP.AsEnumerable()
                                      select new MaterialMaster
                                      {

                                          Name = Convert.ToString(material.Field<string>("Name")),
                                          ProductCategoryId = Convert.ToInt64(material.Field<Int64>("ProductCategoryId")),
                                          ProductSubCategoryId = Convert.ToInt64(material.Field<Int64>("ProductSubCategoryId")),
                                          Description = Convert.ToString(material.Field<string>("Description")),
                                          MOP = Convert.ToString(material.Field<string>("MOP")),
                                      }).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetMaterialByMaterialCode", ex.StackTrace, ex.Message);
            }
            return outputMaterial;
        }

        public MaterialMaster GetMaterialById(MaterialMaster InputParam)
        {
            DbParameterCollection objDBParam = new DbParameterCollection();
            MaterialMaster outputMaterial = null;
            try
            {
                objDBParam.Add(new DbParameter("@MaterialId", InputParam.Id, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetMaterialDetailById]", objDBParam, CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    outputMaterial = (from material in outputFromSP.AsEnumerable()
                                             select new MaterialMaster
                                             {
                                                 Id = Convert.ToInt64(material.Field<Int64>("ID")),
                                                 MaterialCode = Convert.ToString(material.Field<string>("MaterialCode")),
                                                 MOP = Convert.ToString(material.Field<string>("MOP")),
                                                 Name = Convert.ToString(material.Field<string>("Name")),
                                                 ProductCategoryId = Convert.ToInt64(material.Field<Int64>("ProductCategoryId")),
                                                 ProductSubCategoryId = Convert.ToInt64(material.Field<Int64>("ProductSubCategoryId")),
                                                 Description = Convert.ToString(material.Field<string>("Description")),
                                                 Status = Convert.ToBoolean(material.Field<bool>("Status")),
                                                 SizeId = material.Field<Int64>("SizeId"),
                                                 SegmentId = material.Field<Int64>("SegmentId"),
                                                 InternetId = material.Field<Int64>("InternetId"),
                                                 ResolutionId = material.Field<Int64>("ResolutionId"),
                                                 Id3D = material.Field<Int64>("Id3D"),
                                                 TvTypeId = material.Field<Int64>("TVTypeId"),
                                                 IsCombo=material.Field<bool>("IsCombo"),
                                             }).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetMaterialById", ex.StackTrace, ex.Message);
            }
            return outputMaterial;
        }        

        public MaterialCodeGet GetMaterialIdByMaterialCode(MaterialCodeGet InputParam, out string Message)
        {
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            MaterialCodeGet outputMaterial = null;
            try
            {
                objDBParam.Add(new DbParameter("@MaterialCode", InputParam.MaterialCode, DbType.String));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetMaterialIdByMaterialCode]", objDBParam, CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    outputMaterial = (from material in outputFromSP.AsEnumerable()
                                      select new MaterialCodeGet
                                      {
                                          MaterialId = Convert.ToInt64(material.Field<Int64>("MaterialId"))                                          
                                      }).FirstOrDefault();
                    Message = "Material Id fetched successfully.";
                }
                else
                    Message = "Error occured while retrieving MaterialId.";

            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetMaterialIdByMaterialCode", ex.StackTrace, ex.Message);
                Message = "Exception occured in API. Please contact administrator.";
            }
            return outputMaterial;
        }

        #endregion Material Master API

        #region Channel Master API
        public int CreateChannel(ChannelMaster channelData, out string Message)
        {
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@ChannelCode", channelData.ChannelCode, DbType.String));
                objDBParam.Add(new DbParameter("@ChannelName", channelData.ChannelName, DbType.String));
                //objDBParam.Add(new DbParameter("@IsActive", channelData.IsActive));
                objDBParam.Add(new DbParameter("@CreatedBy", channelData.UserId, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[CreateChannel]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return 1;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("CreateChannel", ex.StackTrace, ex.Message);
                Message = ex.Message;
            }
            return 0;
        }

        public int UpdateChannel(ChannelMaster channelData, out string Message)
        {
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@ChannelId", channelData.Id, DbType.Int32));
                objDBParam.Add(new DbParameter("@ChannelCode", channelData.ChannelCode != null ? channelData.ChannelCode.Trim() : null, DbType.String));
                objDBParam.Add(new DbParameter("@ChannelName", channelData.ChannelName, DbType.String));
                //objDBParam.Add(new DbParameter("@IsActive", channelData.IsActive));
                objDBParam.Add(new DbParameter("@UserID", channelData.UserId, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[UpdateChannel]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return 1;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("UpdateChannel", ex.StackTrace, ex.Message);
                Message = ex.Message;
            }
            return 0;
        }

        public int DeleteChannel(ChannelMaster channelData, out string Message)
        {
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@ChannelId", channelData.Id, DbType.Int32));
                objDBParam.Add(new DbParameter("@UserId", channelData.UserId, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[DeleteChannel]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return 1;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("DeleteChannel", ex.StackTrace, ex.Message);
                Message = "Could not delete material. Exception occured in API.";
            }
            return 0;
        }

        public IEnumerable<ChannelMaster> GetAllChannels()
        {
            IEnumerable<ChannelMaster> listChannels = null;
            try
            {
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetChannel]", CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    listChannels = (from channel in outputFromSP.AsEnumerable()
                                     select new ChannelMaster
                                     {
                                         Id = Convert.ToInt64(channel.Field<Int64>("ID")),
                                         ChannelCode = Convert.ToString(channel.Field<string>("CHANNELCODE")),
                                         ChannelName = Convert.ToString(channel.Field<string>("CHANNELNAME")),
                                         IsActive = Convert.ToBoolean(channel.Field<bool>("ISACTIVE")),
                                         CreatedBy = Convert.ToInt64(channel.Field<Int64>("CREATEDBY")),
                                         CreationDate = Convert.ToDateTime(channel.Field<DateTime>("CREATIONDATE")),
                                         LastModifiedBy = Convert.ToInt64(channel.Field<Int64?>("LASTMODIFIEDBY")),
                                         LastModificationDate = Convert.ToDateTime(channel.Field<DateTime?>("LASTMODDATE"))
                                     });
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetAllChannels", ex.StackTrace, ex.Message);
            }
            return listChannels;
        }

        public ChannelMaster GetChannelById(Int64 channelId)
        {
            DbParameterCollection objDBParam = new DbParameterCollection();
            ChannelMaster outputChannel = null;
            try
            {
                objDBParam.Add(new DbParameter("@ChannelId", channelId, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetChannel]", objDBParam, CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    outputChannel = (from channel in outputFromSP.AsEnumerable()
                                      select new ChannelMaster
                                      {
                                          Id = Convert.ToInt64(channel.Field<Int64>("ID")),
                                          ChannelCode = Convert.ToString(channel.Field<string>("CHANNELCODE")),
                                          ChannelName = Convert.ToString(channel.Field<string>("CHANNELNAME")),
                                          IsActive = Convert.ToBoolean(channel.Field<bool>("ISACTIVE")),
                                          CreatedBy = Convert.ToInt64(channel.Field<Int64>("CREATEDBY")),
                                          CreationDate = Convert.ToDateTime(channel.Field<DateTime>("CREATIONDATE")),
                                          LastModifiedBy = Convert.ToInt64(channel.Field<Int64?>("LASTMODIFIEDBY")),
                                          LastModificationDate = Convert.ToDateTime(channel.Field<DateTime?>("LASTMODDATE"))
                                      }).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetChannelById", ex.StackTrace, ex.Message);
            }
            return outputChannel;
        }
        #endregion Channel Master API

        #region SFA Level Master API
        public int CreateSFALevel(SFALevelMaster SFALevelData, out string Message)
        {
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@SFALevelName", SFALevelData.SFALevelName, DbType.String));
                //objDBParam.Add(new DbParameter("@IsActive", SFALevelData.IsActive));
                objDBParam.Add(new DbParameter("@CreatedBy", SFALevelData.UserId, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[CreateSFALevel]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return 1;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("CreateSFALevel", ex.StackTrace, ex.Message);
                Message = ex.Message;
            }
            return 0;
        }

        public int UpdateSFALevel(SFALevelMaster SFALevelData, out string Message)
        {
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@SFALevelId", SFALevelData.Id, DbType.Int32));
                objDBParam.Add(new DbParameter("@SFALevelName", SFALevelData.SFALevelName, DbType.String));
                //objDBParam.Add(new DbParameter("@IsActive", SFALevelData.IsActive));
                objDBParam.Add(new DbParameter("@UserID", SFALevelData.UserId, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[UpdateSFALevel]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return 1;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("UpdateSFALevel", ex.StackTrace, ex.Message);
                Message = ex.Message;
            }
            return 0;
        }

        public int DeleteSFALevel(SFALevelInput SFALevelData, out string Message)
        {
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@SFALevelId", SFALevelData.SFALevelId, DbType.Int64));
                objDBParam.Add(new DbParameter("@UserId", SFALevelData.UserId, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[DeleteSFALevel]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return 1;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("DeleteSFALevel", ex.StackTrace, ex.Message);
                Message = "Could not delete SFA Level. Exception occured in API.";
            }
            return 0;
        }

        public IEnumerable<SFALevelMaster> GetAllSFALevels()
        {
            IEnumerable<SFALevelMaster> listSFALevels = null;
            try
            {
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetSFALevel]", CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    listSFALevels = (from SFALevel in outputFromSP.AsEnumerable()
                                    select new SFALevelMaster
                                    {
                                        Id = Convert.ToInt64(SFALevel.Field<Int64>("ID")), 
                                        SFALevelName = Convert.ToString(SFALevel.Field<string>("SFALEVELNAME")),
                                        IsActive = Convert.ToBoolean(SFALevel.Field<bool>("ISACTIVE")),
                                        CreatedBy = Convert.ToInt64(SFALevel.Field<Int64>("CREATEDBY")),
                                        CreationDate = Convert.ToDateTime(SFALevel.Field<DateTime>("CREATIONDATE")),
                                        LastModifiedBy = Convert.ToInt64(SFALevel.Field<Int64?>("LASTMODIFIEDBY")),
                                        LastModificationDate = Convert.ToDateTime(SFALevel.Field<DateTime?>("LASTMODDATE"))
                                    });
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetAllSFALevels", ex.StackTrace, ex.Message);
            }
            return listSFALevels;
        }

        public SFALevelMaster GetSFALevelById(Int64 SFALevelId)
        {
            DbParameterCollection objDBParam = new DbParameterCollection();
            SFALevelMaster outputSFALevel = null;
            try
            {
                objDBParam.Add(new DbParameter("@SFALevelId", SFALevelId, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetSFALevel]", objDBParam, CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    outputSFALevel = (from SFALevel in outputFromSP.AsEnumerable()
                                     select new SFALevelMaster
                                     {
                                         Id = Convert.ToInt64(SFALevel.Field<Int64>("ID")),
                                         SFALevelName = Convert.ToString(SFALevel.Field<string>("SFALEVELNAME")),
                                         IsActive = Convert.ToBoolean(SFALevel.Field<bool>("ISACTIVE")),
                                         CreatedBy = Convert.ToInt64(SFALevel.Field<Int64>("CREATEDBY")),
                                         CreationDate = Convert.ToDateTime(SFALevel.Field<DateTime>("CREATIONDATE")),
                                         LastModifiedBy = Convert.ToInt64(SFALevel.Field<Int64?>("LASTMODIFIEDBY")),
                                         LastModificationDate = Convert.ToDateTime(SFALevel.Field<DateTime?>("LASTMODDATE"))
                                     }).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetSFALevelById", ex.StackTrace, ex.Message);
            }
            return outputSFALevel;
        }
        #endregion SFA Level Master API

        #region SFA Sub Level Master API
        public int CreateSFASubLevel(SFASubLevelMaster SFASubLevelData, out string Message)
        {
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@SFASubLevelName", SFASubLevelData.SFASubLevelName, DbType.String));
                objDBParam.Add(new DbParameter("@Description", SFASubLevelData.Description, DbType.String));
                objDBParam.Add(new DbParameter("@SFALevelId", SFASubLevelData.SFALevelId, DbType.Int32));
                //objDBParam.Add(new DbParameter("@IsActive", SFASubLevelData.IsActive));
                objDBParam.Add(new DbParameter("@CreatedBy", SFASubLevelData.UserId, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[CreateSFASubLevel]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return 1;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("CreateSFASubLevel", ex.StackTrace, ex.Message);
                Message = ex.Message;
            }
            return 0;
        }

        public int UpdateSFASubLevel(SFASubLevelMaster SFASubLevelData, out string Message)
        {
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@SFASubLevelId", SFASubLevelData.Id, DbType.Int32));
                objDBParam.Add(new DbParameter("@SFASubLevelName", SFASubLevelData.SFASubLevelName, DbType.String));
                objDBParam.Add(new DbParameter("@Description", SFASubLevelData.Description, DbType.String));
                objDBParam.Add(new DbParameter("@SFALevelId", SFASubLevelData.SFALevelId, DbType.Int32));
                //objDBParam.Add(new DbParameter("@IsActive", SFASubLevelData.IsActive));
                objDBParam.Add(new DbParameter("@UserID", SFASubLevelData.UserId, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[UpdateSFASubLevel]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return 1;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("UpdateSFASubLevel", ex.StackTrace, ex.Message);
                Message = ex.Message;
            }
            return 0;
        }

        public int DeleteSFASubLevel(SFASubLevelInput SFASubLevelData, out string Message)
        {
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@SFASubLevelId", SFASubLevelData.SFASubLevelId, DbType.Int64));
                objDBParam.Add(new DbParameter("@UserId", SFASubLevelData.UserId, DbType.Int64));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[DeleteSFASubLevel]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return 1;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("DeleteSFASubLevel", ex.StackTrace, ex.Message);
                Message = "Could not delete SFA Sub Level. Exception occured in API.";
            }
            return 0;
        }

        public IEnumerable<SFASubLevelMaster> GetAllSFASubLevels()
        {
            IEnumerable<SFASubLevelMaster> listSFASubLevels = null;
            try
            {
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetSFASubLevel]", CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    listSFASubLevels = (from SFASubLevel in outputFromSP.AsEnumerable()
                                     select new SFASubLevelMaster
                                     {
                                         Id = Convert.ToInt64(SFASubLevel.Field<Int64>("ID")),
                                         Description = Convert.ToString(SFASubLevel.Field<string>("DESCRIPTION")),
                                         SFASubLevelName = Convert.ToString(SFASubLevel.Field<string>("SFASUBLEVELNAME")),
                                         SFALevelId = Convert.ToInt64(SFASubLevel.Field<Int64>("SFALEVELID")),
                                         IsActive = Convert.ToBoolean(SFASubLevel.Field<bool>("ISACTIVE")),
                                         CreatedBy = Convert.ToInt64(SFASubLevel.Field<Int64>("CREATEDBY")),
                                         CreationDate = Convert.ToDateTime(SFASubLevel.Field<DateTime>("CREATIONDATE")),
                                         LastModifiedBy = Convert.ToInt64(SFASubLevel.Field<Int64?>("LASTMODIFIEDBY")),
                                         LastModificationDate = Convert.ToDateTime(SFASubLevel.Field<DateTime?>("LASTMODDATE"))
                                     });
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetAllSFASubLevels", ex.StackTrace, ex.Message);
            }
            return listSFASubLevels;
        }

        public SFASubLevelMaster GetSFASubLevelById(Int64 SFASubLevelId)
        {
            DbParameterCollection objDBParam = new DbParameterCollection();
            SFASubLevelMaster outputSFASubLevel = null;
            try
            {
                objDBParam.Add(new DbParameter("@SFASubLevelId", SFASubLevelId, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetSFASubLevel]", objDBParam, CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    outputSFASubLevel = (from SFASubLevel in outputFromSP.AsEnumerable()
                                      select new SFASubLevelMaster
                                      {
                                          Id = Convert.ToInt64(SFASubLevel.Field<Int64>("ID")),
                                          Description = Convert.ToString(SFASubLevel.Field<string>("DESCRIPTION")),
                                          SFASubLevelName = Convert.ToString(SFASubLevel.Field<string>("SFASUBLEVELNAME")),
                                          SFALevelId = Convert.ToInt64(SFASubLevel.Field<Int64>("SFALEVELID")),
                                          IsActive = Convert.ToBoolean(SFASubLevel.Field<bool>("ISACTIVE")),
                                          CreatedBy = Convert.ToInt64(SFASubLevel.Field<Int64>("CREATEDBY")),
                                          CreationDate = Convert.ToDateTime(SFASubLevel.Field<DateTime>("CREATIONDATE")),
                                          LastModifiedBy = Convert.ToInt64(SFASubLevel.Field<Int64?>("LASTMODIFIEDBY")),
                                          LastModificationDate = Convert.ToDateTime(SFASubLevel.Field<DateTime?>("LASTMODDATE"))
                                      }).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetSFASubLevelById", ex.StackTrace, ex.Message);
            }
            return outputSFASubLevel;
        }
        #endregion SFA Sub Level Master API

        #region Competitor Master API
        public int CreateCompetitor(CompetitorMaster competitorData, out string Message)
        {
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@CompetitorCode", competitorData.CompetitorCode, DbType.String));
                objDBParam.Add(new DbParameter("@CompetitorName", competitorData.CompetitorName, DbType.String));
                objDBParam.Add(new DbParameter("@Status", competitorData.Status));
                objDBParam.Add(new DbParameter("@CreatedBy", competitorData.UserId, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[CreateCompetitor]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return 1;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("CreateCompetitor", ex.StackTrace, ex.Message);
                Message = ex.Message;
            }
            return 0;
        }

        public int UpdateCompetitor(CompetitorMaster competitorData, out string Message)
        {
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@CompetitorId", competitorData.ID, DbType.Int32));
                objDBParam.Add(new DbParameter("@CompetitorCode", competitorData.CompetitorCode, DbType.String));
                objDBParam.Add(new DbParameter("@CompetitorName", competitorData.CompetitorName, DbType.String));
                objDBParam.Add(new DbParameter("@Status", competitorData.Status));
                objDBParam.Add(new DbParameter("@UserID", competitorData.UserId, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[UpdateCompetitor]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return 1;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("UpdateCompetitor", ex.StackTrace, ex.Message);
                Message = ex.Message;
            }
            return 0;
        }

        public int DeleteCompetitor(CompetitorMaster competitorData, out string Message)
        {
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@CompetitorId", competitorData.ID, DbType.Int32));
                objDBParam.Add(new DbParameter("@UserId", competitorData.UserId, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[DeleteCompetitor]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return 1;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("DeleteCompetitor", ex.StackTrace, ex.Message);
                Message = "Could not delete Competitor. Exception occured in API.";
            }
            return 0;
        }

        public IEnumerable<CompetitorMaster> GetAllCompetitors()
        {
            IEnumerable<CompetitorMaster> listCompetitors = null;
            try
            {
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetCompetitor]", CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    listCompetitors = (from Competitor in outputFromSP.AsEnumerable()
                                        select new CompetitorMaster
                                        {
                                            ID = Convert.ToInt64(Competitor.Field<Int64>("ID")),
                                            CompetitorCode = Convert.ToString(Competitor.Field<string>("COMPETITORCODE")),
                                            CompetitorName = Convert.ToString(Competitor.Field<string>("COMPETITORNAME")),
                                            Discontinued = Convert.ToBoolean(Competitor.Field<bool>("DISCONTINUED")),
                                            Status = Convert.ToBoolean(Competitor.Field<bool>("STATUS")),
                                            IsActive = Convert.ToBoolean(Competitor.Field<bool>("ISACTIVE")),
                                            CreatedBy = Convert.ToInt64(Competitor.Field<Int64>("CREATEDBY")),
                                            CreationDate = Convert.ToDateTime(Competitor.Field<DateTime>("CREATIONDATE")),
                                            LastModifiedBy = Convert.ToInt64(Competitor.Field<Int64?>("LASTMODIFIEDBY")),
                                            LastModificationDate = Convert.ToDateTime(Competitor.Field<DateTime?>("LASTMODDATE"))
                                        });
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetAllCompetitors", ex.StackTrace, ex.Message);
            }
            return listCompetitors;
        }

        public CompetitorMaster GetCompetitorById(Int64 competitorId)
        {
            DbParameterCollection objDBParam = new DbParameterCollection();
            CompetitorMaster outputCompetitor = null;
            try
            {
                objDBParam.Add(new DbParameter("@CompetitorId", competitorId, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetCompetitor]", objDBParam, CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    outputCompetitor = (from Competitor in outputFromSP.AsEnumerable()
                                         select new CompetitorMaster
                                         {
                                             ID = Convert.ToInt64(Competitor.Field<Int64>("ID")),
                                             CompetitorCode = Convert.ToString(Competitor.Field<string>("COMPETITORCODE")),
                                             CompetitorName = Convert.ToString(Competitor.Field<string>("COMPETITORNAME")),
                                             Discontinued = Convert.ToBoolean(Competitor.Field<bool>("DISCONTINUED")),
                                             Status = Convert.ToBoolean(Competitor.Field<bool>("STATUS")),
                                             IsActive = Convert.ToBoolean(Competitor.Field<bool>("ISACTIVE")),
                                             CreatedBy = Convert.ToInt64(Competitor.Field<Int64>("CREATEDBY")),
                                             CreationDate = Convert.ToDateTime(Competitor.Field<DateTime>("CREATIONDATE")),
                                             LastModifiedBy = Convert.ToInt64(Competitor.Field<Int64?>("LASTMODIFIEDBY")),
                                             LastModificationDate = Convert.ToDateTime(Competitor.Field<DateTime?>("LASTMODDATE"))
                                         }).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetCompetitorById", ex.StackTrace, ex.Message);
            }
            return outputCompetitor;
        }
        #endregion Competitor Master API

        #region Competitor Product Master API
        public int CreateCompetitorProduct(CompetitorProductMaster competitorProductData, out string Message)
        {
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@CompetitorID", competitorProductData.CompetitorID, DbType.Int32));
                objDBParam.Add(new DbParameter("@ProductName", competitorProductData.ProductName, DbType.String));
                //objDBParam.Add(new DbParameter("@Discontinued", competitorProductData.Discontinued));
                objDBParam.Add(new DbParameter("@Status", competitorProductData.Status));
                objDBParam.Add(new DbParameter("@SonyProdCategory", competitorProductData.SonyProductCategory, DbType.Int32));
                objDBParam.Add(new DbParameter("@TopModel", competitorProductData.TopModel));
                //objDBParam.Add(new DbParameter("@IsActive", competitorProductData.IsActive));
                objDBParam.Add(new DbParameter("@CreatedBy", competitorProductData.UserId, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[CreateCompetitorProduct]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return 1;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("CreateCompetitorProduct", ex.StackTrace, ex.Message);
                Message = ex.Message;
            }
            return 0;
        }

        public int UpdateCompetitorProduct(CompetitorProductMaster competitorProductData, out string Message)
        {
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@CompetitorProductID", competitorProductData.ID, DbType.Int32));
                objDBParam.Add(new DbParameter("@CompetitorID", competitorProductData.CompetitorID, DbType.Int32));
                objDBParam.Add(new DbParameter("@ProductName", competitorProductData.ProductName, DbType.String));
                //objDBParam.Add(new DbParameter("@Discontinued", competitorProductData.Discontinued));
                objDBParam.Add(new DbParameter("@Status", competitorProductData.Status));
                objDBParam.Add(new DbParameter("@SonyProdCategory", competitorProductData.SonyProductCategory, DbType.Int32));
                objDBParam.Add(new DbParameter("@TopModel", competitorProductData.TopModel));
                //objDBParam.Add(new DbParameter("@IsActive", competitorProductData.IsActive));
                objDBParam.Add(new DbParameter("@UserID", competitorProductData.UserId, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[UpdateCompetitorProduct]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return 1;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("UpdateCompetitorProduct", ex.StackTrace, ex.Message);
                Message = ex.Message;
            }
            return 0;
        }

        public int DeleteCompetitorProduct(CompetitorProductMaster competitorProductData, out string Message)
        {
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@CompetitorProductId", competitorProductData.ID, DbType.Int32));
                objDBParam.Add(new DbParameter("@UserId", competitorProductData.UserId, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[DeleteCompetitorProduct]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return 1;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("DeleteCompetitorProduct", ex.StackTrace, ex.Message);
                Message = "Could not delete Competitor Product. Exception occured in API.";
            }
            return 0;
        }

        public IEnumerable<CompetitorProductMaster> GetAllCompetitorProducts()
        {
            IEnumerable<CompetitorProductMaster> listCompetitorProducts = null;
            try
            {
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetCompetitorProduct]", CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    listCompetitorProducts = (from CompetitorProduct in outputFromSP.AsEnumerable()
                                       select new CompetitorProductMaster
                                       {
                                           ID = Convert.ToInt64(CompetitorProduct.Field<Int64>("ID")),
                                           CompetitorID = Convert.ToInt64(CompetitorProduct.Field<Int64>("COMPETITORID")),
                                           ProductName = Convert.ToString(CompetitorProduct.Field<string>("PRODUCTNAME")),
                                           Discontinued = Convert.ToBoolean(CompetitorProduct.Field<bool>("DISCONTINUED")),
                                           Status = Convert.ToBoolean(CompetitorProduct.Field<bool>("STATUS")),
                                           SonyProductCategory = Convert.ToInt64(CompetitorProduct.Field<Int64>("SONYPRODCATEGORY")),
                                           TopModel = Convert.ToBoolean(CompetitorProduct.Field<bool>("TOPMODEL")),
                                           IsActive = Convert.ToBoolean(CompetitorProduct.Field<bool>("ISACTIVE")),
                                           CreatedBy = Convert.ToInt64(CompetitorProduct.Field<Int64>("CREATEDBY")),
                                           CreationDate = Convert.ToDateTime(CompetitorProduct.Field<DateTime>("CREATIONDATE")),
                                           LastModifiedBy = Convert.ToInt64(CompetitorProduct.Field<Int64?>("LASTMODIFIEDBY")),
                                           LastModificationDate = Convert.ToDateTime(CompetitorProduct.Field<DateTime?>("LASTMODDATE"))
                                       });
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetAllCompetitorProducts", ex.StackTrace, ex.Message);
            }
            return listCompetitorProducts;
        }

        public CompetitorProductMaster GetCompetitorProductById(Int64 competitorProductId)
        {
            DbParameterCollection objDBParam = new DbParameterCollection();
            CompetitorProductMaster outputCompetitorProduct = null;
            try
            {
                objDBParam.Add(new DbParameter("@CompetitorProductId", competitorProductId, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetCompetitorProduct]", objDBParam, CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    outputCompetitorProduct = (from CompetitorProduct in outputFromSP.AsEnumerable()
                                        select new CompetitorProductMaster
                                        {
                                            ID = Convert.ToInt64(CompetitorProduct.Field<Int64>("ID")),
                                            CompetitorID = Convert.ToInt64(CompetitorProduct.Field<Int64>("COMPETITORID")),
                                            ProductName = Convert.ToString(CompetitorProduct.Field<string>("PRODUCTNAME")),
                                            Discontinued = Convert.ToBoolean(CompetitorProduct.Field<bool>("DISCONTINUED")),
                                            Status = Convert.ToBoolean(CompetitorProduct.Field<bool>("STATUS")),
                                            SonyProductCategory = Convert.ToInt64(CompetitorProduct.Field<Int64>("SONYPRODCATEGORY")),
                                            TopModel = Convert.ToBoolean(CompetitorProduct.Field<bool>("TOPMODEL")),
                                            IsActive = Convert.ToBoolean(CompetitorProduct.Field<bool>("ISACTIVE")),
                                            CreatedBy = Convert.ToInt64(CompetitorProduct.Field<Int64>("CREATEDBY")),
                                            CreationDate = Convert.ToDateTime(CompetitorProduct.Field<DateTime>("CREATIONDATE")),
                                            LastModifiedBy = Convert.ToInt64(CompetitorProduct.Field<Int64?>("LASTMODIFIEDBY")),
                                            LastModificationDate = Convert.ToDateTime(CompetitorProduct.Field<DateTime?>("LASTMODDATE"))
                                        }).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetCompetitorProductById", ex.StackTrace, ex.Message);
            }
            return outputCompetitorProduct;
        }
        #endregion Competitor Product Master API

        #region Comptetitor Model Master API
        public int CreateCompetitorModel(CompetitorModelMaster competitorModelData, out string Message)
        {
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@CompetitorProdCatID", competitorModelData.CompetitorProductCatID, DbType.Int32));
                objDBParam.Add(new DbParameter("@SonyProdSubCatID", competitorModelData.SonyProductSubCategory, DbType.Int32));
                objDBParam.Add(new DbParameter("@ModelName", competitorModelData.ModelName, DbType.String));
                //objDBParam.Add(new DbParameter("@Discontinued", competitorModelData.Discontinued));
                objDBParam.Add(new DbParameter("@Status", competitorModelData.Status));
                objDBParam.Add(new DbParameter("@SonyMaterialId", competitorModelData.SonyMaterialId, DbType.Int32));
                //objDBParam.Add(new DbParameter("@IsActive", competitorModelData.IsActive));
                objDBParam.Add(new DbParameter("@SizeId", competitorModelData.SizeValueId, DbType.Int32));
                objDBParam.Add(new DbParameter("@SegmentId", competitorModelData.SegmentValueId, DbType.Int32));
                objDBParam.Add(new DbParameter("@InternetId", competitorModelData.InternetValueId, DbType.Int32));
                objDBParam.Add(new DbParameter("@TVTypeId", competitorModelData.TvTypeValueId, DbType.Int32));
                objDBParam.Add(new DbParameter("@3DId", competitorModelData.Id3DValue, DbType.Int32));
                objDBParam.Add(new DbParameter("@ResolutionId", competitorModelData.ResolutioValueId, DbType.Int32));
                objDBParam.Add(new DbParameter("@CreatedBy", competitorModelData.UserId, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[CreateCompetitorModel]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return 1;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("CreateCompetitorModel", ex.StackTrace, ex.Message);
                Message = ex.Message;
            }
            return 0;
        }

        public int UpdateCompetitorModel(CompetitorModelMaster competitorModelData, out string Message)
        {
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@CompetitorModelID", competitorModelData.ID, DbType.Int32));
                objDBParam.Add(new DbParameter("@CompetitorProdCatID", competitorModelData.CompetitorProductCatID, DbType.Int32));
                objDBParam.Add(new DbParameter("@SonyProdSubCatID", competitorModelData.SonyProductSubCategory, DbType.Int32));
                objDBParam.Add(new DbParameter("@ModelName", competitorModelData.ModelName, DbType.String));
                //objDBParam.Add(new DbParameter("@Discontinued", competitorModelData.Discontinued));
                objDBParam.Add(new DbParameter("@Status", competitorModelData.Status));
                objDBParam.Add(new DbParameter("@SonyMaterialId", competitorModelData.SonyMaterialId, DbType.Int32));
                objDBParam.Add(new DbParameter("@IsActive", competitorModelData.Status));
                objDBParam.Add(new DbParameter("@UserID", competitorModelData.UserId, DbType.Int32));
                objDBParam.Add(new DbParameter("@SizeId", competitorModelData.SizeValueId, DbType.Int32));
                objDBParam.Add(new DbParameter("@SegmentId", competitorModelData.SegmentValueId, DbType.Int32));
                objDBParam.Add(new DbParameter("@InternetId", competitorModelData.InternetValueId, DbType.Int32));
                objDBParam.Add(new DbParameter("@TVTypeId", competitorModelData.TvTypeValueId, DbType.Int32));
                objDBParam.Add(new DbParameter("@3DId", competitorModelData.Id3DValue, DbType.Int32));
                objDBParam.Add(new DbParameter("@ResolutionId", competitorModelData.ResolutioValueId, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[UpdateCompetitorModel]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return 1;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("UpdateCompetitorModel", ex.StackTrace, ex.Message);
                Message = ex.Message;
            }
            return 0;
        }

        public int DeleteCompetitorModel(CompetitorModelMaster competitorModelData, out string Message)
        {
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@CompetitorModelId", competitorModelData.ID, DbType.Int32));
                objDBParam.Add(new DbParameter("@UserId", competitorModelData.UserId, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[DeleteCompetitorModel]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return 1;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("DeleteCompetitorModel", ex.StackTrace, ex.Message);
                Message = "Could not delete Competitor Model. Exception occured in API.";
            }
            return 0;
        }

        public IEnumerable<CompetitorModelMaster> GetAllCompetitorModels()
        {
            IEnumerable<CompetitorModelMaster> listCompetitorModels = null;
            try
            {
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetCompetitorModel]", CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    listCompetitorModels = (from CompetitorModel in outputFromSP.AsEnumerable()
                                              select new CompetitorModelMaster
                                              {
                                                  ID = Convert.ToInt64(CompetitorModel.Field<Int64>("ID")),
                                                  CompetitorProductCatID = Convert.ToInt64(CompetitorModel.Field<Int64>("COMPETITORPRODCATID")),
                                                  SonyProductSubCategory = Convert.ToInt64(CompetitorModel.Field<Int64>("SONYPRODSUBCATID")),
                                                  ModelName = Convert.ToString(CompetitorModel.Field<string>("MODELNAME")),
                                                  Discontinued = Convert.ToBoolean(CompetitorModel.Field<bool>("DISCONTINUED")),
                                                  Status = Convert.ToBoolean(CompetitorModel.Field<bool>("STATUS")),
                                                  SonyMaterialId = Convert.ToInt64(CompetitorModel.Field<Int64>("SONYMATERIALID")),
                                                  IsActive = Convert.ToBoolean(CompetitorModel.Field<bool>("ISACTIVE")),
                                                  
                                              });
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetAllCompetitorModels", ex.StackTrace, ex.Message);
            }
            return listCompetitorModels;
        }

        public CompetitorModelList GetCompetitorModelById(CompetitorDataInput competitorModelId)
        {
            DbParameterCollection objDBParam = new DbParameterCollection();
            CompetitorModelList outputCompetitorModel = new CompetitorModelList();
            try
            {
                objDBParam.Add(new DbParameter("@ModelId", competitorModelId.CProductCatId, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetCompetitorModelById]", objDBParam, CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    outputCompetitorModel = (from CompetitorModel in outputFromSP.AsEnumerable()
                                               select new CompetitorModelList
                                               {
                                                   ID = Convert.ToInt64(CompetitorModel.Field<Int64>("ID")),
                                                   CompetitorProductCatID = Convert.ToInt64(CompetitorModel.Field<Int64>("CompetitorProductCatID")),
                                                   SonyProductSubCategory = Convert.ToInt64(CompetitorModel.Field<Int64>("SonyProductSubCategory")),
                                                   ModelName = Convert.ToString(CompetitorModel.Field<string>("ModelName")),
                                                   Status = Convert.ToBoolean(CompetitorModel.Field<bool>("Status")),
                                                   SonyMaterialId = Convert.ToInt64(CompetitorModel.Field<Int64>("SonyMaterialId")),
                                                   CompetitorId = Convert.ToInt64(CompetitorModel.Field<Int64>("CompetitorID")),
                                                   SonyProductCategoryId = Convert.ToInt64(CompetitorModel.Field<Int64>("ProductCategoryId")),
                                                   SizeValueId = Convert.ToInt64(CompetitorModel.Field<Int64>("SizeId")),
                                                   SegmentValueId = Convert.ToInt64(CompetitorModel.Field<Int64>("SegmentId")),
                                                   ResolutioValueId = Convert.ToInt64(CompetitorModel.Field<Int64>("ResolutionId")),
                                                   InternetValueId = Convert.ToInt64(CompetitorModel.Field<Int64>("InternetId")),
                                                   Id3DValue = Convert.ToInt64(CompetitorModel.Field<Int64>("Id3D")),
                                                   TvTypeValueId = Convert.ToInt64(CompetitorModel.Field<Int64>("TVTypeId")),
                                               }).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetCompetitorModelById", ex.StackTrace, ex.Message);
                outputCompetitorModel = null;
            }
            return outputCompetitorModel;
        }
        #endregion Competitor Model Master API

        #region Dealer Master API
        public int CreateDealer(DealerMaster dealerData, out string Message)
        {
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@DealerCode", dealerData.DealerCode, DbType.String));
                objDBParam.Add(new DbParameter("@SAPCode", dealerData.SAPCode, DbType.String));
                objDBParam.Add(new DbParameter("@MasterCode1", dealerData.MasterCode1, DbType.String));
                objDBParam.Add(new DbParameter("@MasterCode2", dealerData.MasterCode2, DbType.String));
                objDBParam.Add(new DbParameter("@PayerCode", dealerData.PayerCode, DbType.String));
                objDBParam.Add(new DbParameter("@PayerName", dealerData.PayerName, DbType.String));
                objDBParam.Add(new DbParameter("@StoreCode", dealerData.StoreCode, DbType.String));
                objDBParam.Add(new DbParameter("@CustomerId", dealerData.CustomerId, DbType.String));
                objDBParam.Add(new DbParameter("@DealerName", dealerData.DealerName, DbType.String));
                objDBParam.Add(new DbParameter("@ChannelId", dealerData.ChannelId, DbType.Int32));
                objDBParam.Add(new DbParameter("@BranchId", dealerData.BranchId, DbType.Int32));
                objDBParam.Add(new DbParameter("@LocationId", dealerData.LocationId, DbType.Int32));
                objDBParam.Add(new DbParameter("@Address", dealerData.Address, DbType.String));
                objDBParam.Add(new DbParameter("@Latitude", dealerData.Latitude, DbType.String));
                objDBParam.Add(new DbParameter("@Longitude", dealerData.Longitude, DbType.String));
                objDBParam.Add(new DbParameter("@MobileNumber", dealerData.MobileNumber, DbType.String));
                objDBParam.Add(new DbParameter("@ContactPerson", dealerData.ContactPerson, DbType.String));
                objDBParam.Add(new DbParameter("@EmailID", dealerData.EmailID, DbType.String));
                objDBParam.Add(new DbParameter("@TIN", dealerData.TIN, DbType.String));
                objDBParam.Add(new DbParameter("@PAN", dealerData.PAN, DbType.String));
                objDBParam.Add(new DbParameter("@PSIOutlet1", dealerData.PSIOutlet1));
                objDBParam.Add(new DbParameter("@PSIOutlet2", dealerData.PSIOutlet2));
                objDBParam.Add(new DbParameter("@OpeningDate", dealerData.OpeningDate, DbType.String));
                objDBParam.Add(new DbParameter("@ClosingDate", dealerData.ClosingDate, DbType.String));
                objDBParam.Add(new DbParameter("@OutletType", dealerData.OutletType, DbType.String));
                objDBParam.Add(new DbParameter("@Status", dealerData.Status));
                objDBParam.Add(new DbParameter("@Discontinued", dealerData.Discontinued));
                objDBParam.Add(new DbParameter("@CreatedBy", dealerData.UserId, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[CreateDealer]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return 1;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("CreateDealer", ex.StackTrace, ex.Message);
                Message = ex.Message;
            }
            return 0;
        }


        public int UpdateDealer(DealerMaster dealerData, out string Message)
        {
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@DealerId", dealerData.ID, DbType.Int32));
                objDBParam.Add(new DbParameter("@DealerCode", dealerData.DealerCode, DbType.String));
                objDBParam.Add(new DbParameter("@SAPCode", dealerData.SAPCode, DbType.String));
                objDBParam.Add(new DbParameter("@MasterCode1", dealerData.MasterCode1, DbType.String));
                objDBParam.Add(new DbParameter("@MasterCode2", dealerData.MasterCode2, DbType.String));
                objDBParam.Add(new DbParameter("@PayerCode", dealerData.PayerCode, DbType.String));
                objDBParam.Add(new DbParameter("@PayerName", dealerData.PayerName, DbType.String));
                objDBParam.Add(new DbParameter("@StoreCode", dealerData.StoreCode, DbType.String));
                objDBParam.Add(new DbParameter("@CustomerId", dealerData.CustomerId, DbType.String));
                objDBParam.Add(new DbParameter("@DealerName", dealerData.DealerName, DbType.String));
                objDBParam.Add(new DbParameter("@ChannelId", dealerData.ChannelId, DbType.Int32));
                objDBParam.Add(new DbParameter("@BranchId", dealerData.BranchId, DbType.Int32));
                objDBParam.Add(new DbParameter("@LocationId", dealerData.LocationId, DbType.Int32));
                objDBParam.Add(new DbParameter("@Address", dealerData.Address, DbType.String));
                objDBParam.Add(new DbParameter("@Latitude", dealerData.Latitude, DbType.String));
                objDBParam.Add(new DbParameter("@Longitude", dealerData.Longitude, DbType.String));
                objDBParam.Add(new DbParameter("@MobileNumber", dealerData.MobileNumber, DbType.String));
                objDBParam.Add(new DbParameter("@ContactPerson", dealerData.ContactPerson, DbType.String));
                objDBParam.Add(new DbParameter("@EmailID", dealerData.EmailID, DbType.String));
                objDBParam.Add(new DbParameter("@TIN", dealerData.TIN, DbType.String));
                objDBParam.Add(new DbParameter("@PAN", dealerData.PAN, DbType.String));
                objDBParam.Add(new DbParameter("@PSIOutlet1", dealerData.PSIOutlet1));
                objDBParam.Add(new DbParameter("@PSIOutlet2", dealerData.PSIOutlet2));
                objDBParam.Add(new DbParameter("@OpeningDate", dealerData.OpeningDate, DbType.String));
                objDBParam.Add(new DbParameter("@ClosingDate", dealerData.ClosingDate, DbType.String));
                objDBParam.Add(new DbParameter("@OutletType", dealerData.OutletType, DbType.String));
                objDBParam.Add(new DbParameter("@Status", dealerData.Status));
                objDBParam.Add(new DbParameter("@Discontinued", dealerData.Discontinued));
                objDBParam.Add(new DbParameter("@UserID", dealerData.UserId, DbType.Int32));                
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[UpdateDealer]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return 1;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("UpdateDealer", ex.StackTrace, ex.Message);
                Message = ex.Message;
            }
            return 0;
        }


        public int DeleteDealer(DealerMaster dealerData, out string Message)
        {
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@DealerId", dealerData.ID, DbType.Int32));
                objDBParam.Add(new DbParameter("@UserId", dealerData.UserId, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[DeleteDealer]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return 1;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("DeleteDealer", ex.StackTrace, ex.Message);
                Message = "Could not delete dealer. Exception occured in API.";
            }
            return 0;
        }

        public int CheckPSIOutlet(DealerMaster dealerData, out string Message)
        {
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@DealerId", dealerData.ID, DbType.Int32));                
                objDBParam.Add(new DbParameter("@SAPCode", dealerData.SAPCode, DbType.String));                
                objDBParam.Add(new DbParameter("@PSIOutlet1", dealerData.PSIOutlet1));
                objDBParam.Add(new DbParameter("@PSIOutlet2", dealerData.PSIOutlet2));                
                
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[CheckPSIOutlet]", objDBParam, CommandType.StoredProcedure);

                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return 1;

            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("CheckPSIOutlet", ex.StackTrace, ex.Message);
                Message = "Exception occured while checking PSIOutlet.";
            }
            return 0;
        }

        public IEnumerable<DealerMaster> GetAllDealers()
        {
            IEnumerable<DealerMaster> listDealers = null;
            try
            {
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetDealer]", CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    listDealers = (from dealer in outputFromSP.AsEnumerable()
                                    select new DealerMaster
                                    {
                                        ID = Convert.ToInt64(dealer.Field<Int64>("ID")),
                                        DealerCode = Convert.ToString(dealer.Field<string>("DEALERCODE")),
                                        SAPCode = Convert.ToString(dealer.Field<string>("SAPCODE")),
                                        MasterCode1 = Convert.ToString(dealer.Field<string>("MASTERCODE1")),
                                        MasterCode2 = Convert.ToString(dealer.Field<string>("MASTERCODE2")),
                                        PayerCode = Convert.ToString(dealer.Field<string>("PAYERCODE")),
                                        PayerName = Convert.ToString(dealer.Field<string>("PAYERNAME")),
                                        StoreCode = Convert.ToString(dealer.Field<string>("STORECODE")),
                                        CustomerId = Convert.ToString(dealer.Field<string>("CUSTOMERID")),
                                        DealerName = Convert.ToString(dealer.Field<string>("DEALERNAME")),
                                        ChannelId = Convert.ToInt64(dealer.Field<Int64>("CHANNELID")),
                                        BranchId = Convert.ToInt64(dealer.Field<Int64>("BRANCHID")),
                                        LocationId = Convert.ToInt64(dealer.Field<Int64>("LOCATIONID")),
                                        Address = Convert.ToString(dealer.Field<string>("ADDRESS")),
                                        Latitude = Convert.ToString(dealer.Field<string>("LATITUDE")),
                                        Longitude = Convert.ToString(dealer.Field<string>("LONGITUDE")),
                                        MobileNumber = Convert.ToString(dealer.Field<string>("MOBILENUMBER")),
                                        ContactPerson = Convert.ToString(dealer.Field<string>("CONTACTPERSON")),
                                        EmailID = Convert.ToString(dealer.Field<string>("EMAILID")),
                                        TIN = Convert.ToString(dealer.Field<string>("TIN")),
                                        PAN = Convert.ToString(dealer.Field<string>("PAN")),
                                        PSIOutlet1 = Convert.ToBoolean(dealer.Field<bool>("PSIOUTLET1")),
                                        PSIOutlet2 = Convert.ToBoolean(dealer.Field<bool>("PSIOUTLET2")),
                                        OpeningDate = dealer.Field<string>("OPENINGDATE"),
                                        OutletType = Convert.ToString(dealer.Field<string>("OUTLETTYPE")),
                                        Status = Convert.ToBoolean(dealer.Field<bool>("STATUS")),
                                        Discontinued = Convert.ToBoolean(dealer.Field<bool>("DISCONTINUED")),
                                        CreatedBy = Convert.ToInt64(dealer.Field<Int64>("CREATEDBY")),
                                        CreationDate = Convert.ToDateTime(dealer.Field<DateTime>("CREATIONDATE")),
                                        LastModifiedBy = Convert.ToInt64(dealer.Field<Int64?>("LASTMODIFIEDBY")),
                                        LastModificationDate = Convert.ToDateTime(dealer.Field<DateTime?>("LASTMODDATE"))
                                    });
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetAllDealers", ex.StackTrace, ex.Message);
            }
            return listDealers;
        }

        public DealerMaster GetDealerById(Int64 dealerId, string dealerCode)
        {
            DbParameterCollection objDBParam = new DbParameterCollection();
            DealerMaster outputDealer = null;
            try
            {
                objDBParam.Add(new DbParameter("@DealerCode", dealerCode, DbType.String));
                objDBParam.Add(new DbParameter("@DealerId", dealerId, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetDealer]", objDBParam, CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    outputDealer = (from dealer in outputFromSP.AsEnumerable()
                                     select new DealerMaster
                                     {
                                         ID = Convert.ToInt64(dealer.Field<Int64>("ID")),
                                         DealerCode = Convert.ToString(dealer.Field<string>("DEALERCODE")),
                                         SAPCode = Convert.ToString(dealer.Field<string>("SAPCODE")),
                                         MasterCode1 = Convert.ToString(dealer.Field<string>("MASTERCODE1")),
                                         MasterCode2 = Convert.ToString(dealer.Field<string>("MASTERCODE2")),
                                         PayerCode = Convert.ToString(dealer.Field<string>("PAYERCODE")),
                                         PayerName = Convert.ToString(dealer.Field<string>("PAYERNAME")),
                                         StoreCode = Convert.ToString(dealer.Field<string>("STORECODE")),
                                         CustomerId = Convert.ToString(dealer.Field<string>("CUSTOMERID")),
                                         DealerName = Convert.ToString(dealer.Field<string>("DEALERNAME")),
                                         ChannelId = Convert.ToInt64(dealer.Field<Int64>("CHANNELID")),
                                         BranchId = Convert.ToInt64(dealer.Field<Int64>("BRANCHID")),
                                         RegionId = Convert.ToInt64(dealer.Field<Int64>("REGIONID")),
                                         StateId = Convert.ToInt64(dealer.Field<Int64>("STATEID")),
                                         CityId = Convert.ToInt64(dealer.Field<Int64>("CITYID")),
                                         LocationId = Convert.ToInt64(dealer.Field<Int64>("LOCATIONID")),
                                         Address = Convert.ToString(dealer.Field<string>("ADDRESS")),
                                         Latitude = Convert.ToString(dealer.Field<string>("LATITUDE")),
                                         Longitude = Convert.ToString(dealer.Field<string>("LONGITUDE")),
                                         MobileNumber = Convert.ToString(dealer.Field<string>("MOBILENUMBER")),
                                         ContactPerson = Convert.ToString(dealer.Field<string>("CONTACTPERSON")),
                                         EmailID = Convert.ToString(dealer.Field<string>("EMAILID")),
                                         TIN = Convert.ToString(dealer.Field<string>("TIN")),
                                         PAN = Convert.ToString(dealer.Field<string>("PAN")),
                                         PSIOutlet1 = Convert.ToBoolean(dealer.Field<bool>("PSIOUTLET1")),
                                         PSIOutlet2 = Convert.ToBoolean(dealer.Field<bool>("PSIOUTLET2")),
                                         OpeningDate = dealer.Field<string>("OPENINGDATE"),
                                         OutletType = Convert.ToString(dealer.Field<string>("OUTLETTYPE")),
                                         Status = Convert.ToBoolean(dealer.Field<bool>("STATUS")),
                                         Discontinued = Convert.ToBoolean(dealer.Field<bool>("DISCONTINUED")),
                                         CreatedBy = Convert.ToInt64(dealer.Field<Int64>("CREATEDBY")),
                                         CreationDate = Convert.ToDateTime(dealer.Field<DateTime>("CREATIONDATE")),
                                         LastModifiedBy = Convert.ToInt64(dealer.Field<Int64?>("LASTMODIFIEDBY")),
                                         LastModificationDate = Convert.ToDateTime(dealer.Field<DateTime?>("LASTMODDATE")),
                                     }).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetDealerById", ex.StackTrace, ex.Message);
            }
            return outputDealer;
        }

        public PayerDetails GetDealerCode(string SAPCode)
        {
            DbParameterCollection objDBParam = new DbParameterCollection();
            PayerDetails DealerCode = new PayerDetails();
            PayerDetails DealerCode1 = null;
            PayerDetails DealerCode2 = null;
            try
            {
                objDBParam.Add(new DbParameter("@SAPCode", SAPCode, DbType.String));
                var outputFromSP = _dataHelper.ExecuteDataSet("[dbo].[GetDealerCode]", objDBParam, CommandType.StoredProcedure);
                if (outputFromSP.Tables.Count > 0)
                {
                    if (outputFromSP.Tables[0].Rows.Count > 0)
                    {
                        DealerCode1 = (from dealer in outputFromSP.Tables[0].AsEnumerable()
                                      select new PayerDetails
                                      {
                                          DealerCode = Convert.ToString(dealer.Field<string>("DEALERCODE")),

                                      }).FirstOrDefault();
                        if (outputFromSP.Tables[1].Rows.Count > 0)
                        {
                            DealerCode2 = (from dealer in outputFromSP.Tables[1].AsEnumerable()
                                          select new PayerDetails
                                          {
                                              PayerCode = Convert.ToString(dealer.Field<string>("PAYERCODE")),
                                              PayerName = Convert.ToString(dealer.Field<string>("PAYERNAME")),

                                          }).FirstOrDefault();

                            DealerCode.DealerCode = DealerCode1.DealerCode;
                            DealerCode.PayerCode = DealerCode2.PayerCode;
                            DealerCode.PayerName = DealerCode2.PayerName;
                        }
                        

                    }
                    
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetDealerCode", ex.StackTrace, ex.Message);
            }
            return DealerCode;
        }

        #endregion Dealer Master API

        #region Division Master API
        public int CreateDivision(DivisionMaster divisionData, out string Message)
        {
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@DivisionName", divisionData.DivisionName, DbType.String));
                //objDBParam.Add(new DbParameter("@IsActive", divisionData.IsActive));
                objDBParam.Add(new DbParameter("@CreatedBy", divisionData.UserId, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[CreateDivision]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return 1;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("CreateDivision", ex.StackTrace, ex.Message);
                Message = ex.Message;
            }
            return 0;
        }

        public int UpdateDivision(DivisionMaster divisionData, out string Message)
        {
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@DivisionId", divisionData.Id, DbType.Int32));
                objDBParam.Add(new DbParameter("@DivisionName", divisionData.DivisionName, DbType.String));
                //objDBParam.Add(new DbParameter("@IsActive", divisionData.IsActive));
                objDBParam.Add(new DbParameter("@UserID", divisionData.UserId, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[UpdateDivision]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return 1;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("UpdateDivision", ex.StackTrace, ex.Message);
                Message = ex.Message;
            }
            return 0;
        }

        public int DeleteDivision(DivisionMaster divisionData, out string Message)
        {
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@DivisionId", divisionData.Id, DbType.Int32));
                objDBParam.Add(new DbParameter("@UserId", divisionData.UserId, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[DeleteDivision]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return 1;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("DeleteDivision", ex.StackTrace, ex.Message);
                Message = "Could not delete division. Exception occured in API.";
            }
            return 0;
        }

        public IEnumerable<DivisionMaster> GetAllDivisions()
        {
            IEnumerable<DivisionMaster> listDivisions = null;
            try
            {
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetDivision]", CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    listDivisions = (from division in outputFromSP.AsEnumerable()
                                   select new DivisionMaster
                                   {
                                       Id = Convert.ToInt64(division.Field<Int64>("ID")),
                                       DivisionName = Convert.ToString(division.Field<string>("DIVISIONNAME")),
                                       //IsActive = Convert.ToBoolean(division.Field<bool>("ISACTIVE")),
                                       //CreatedBy = Convert.ToInt64(division.Field<Int64>("CREATEDBY")),
                                       //CreationDate = Convert.ToDateTime(division.Field<DateTime>("CREATIONDATE")),
                                       //LastModifiedBy = Convert.ToInt64(division.Field<Int64?>("LASTMODIFIEDBY")),
                                       //LastModificationTime = Convert.ToDateTime(division.Field<DateTime?>("LASTMODDATE"))
                                   });
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetAllDivisions", ex.StackTrace, ex.Message);
            }
            return listDivisions;
        }

        public DivisionMaster GetDivisionById(Int64 divisionId)
        {
            DbParameterCollection objDBParam = new DbParameterCollection();
            DivisionMaster outputDivision = null;
            try
            {
                objDBParam.Add(new DbParameter("@DivisionId", divisionId, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetDivision]", objDBParam, CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    outputDivision = (from division in outputFromSP.AsEnumerable()
                                     select new DivisionMaster
                                     {
                                         Id = Convert.ToInt64(division.Field<Int64>("ID")),
                                         DivisionName = Convert.ToString(division.Field<string>("DIVISIONNAME")),
                                         IsActive = Convert.ToBoolean(division.Field<bool>("ISACTIVE")),
                                         CreatedBy = Convert.ToInt64(division.Field<Int64>("CREATEDBY")),
                                         CreationDate = Convert.ToDateTime(division.Field<DateTime>("CREATIONDATE")),
                                         LastModifiedBy = Convert.ToInt64(division.Field<Int64?>("LASTMODIFIEDBY")),
                                         LastModificationTime = Convert.ToDateTime(division.Field<DateTime?>("LASTMODDATE"))
                                     }).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetDivisionById", ex.StackTrace, ex.Message);
            }
            return outputDivision;
        }
        #endregion Division Master API

        #region Asset Master API
        public int CreateAsset(AssetMaster assetData, out string Message)
        {
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@ProductCode", assetData.ProductCode, DbType.String));
                objDBParam.Add(new DbParameter("@ProductName", assetData.ProductName, DbType.String));
                objDBParam.Add(new DbParameter("@Category", assetData.Category, DbType.String));
                objDBParam.Add(new DbParameter("@Type", assetData.Type, DbType.String));
                objDBParam.Add(new DbParameter("@Status", assetData.Status));
                objDBParam.Add(new DbParameter("@CreatedBy", assetData.UserId, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[CreateAsset]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return 1;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("CreateAsset", ex.StackTrace, ex.Message);
                Message = ex.Message;
            }
            return 0;
        }

        public int UpdateAsset(AssetMaster assetData, out string Message)
        {
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@AssetId", assetData.Id, DbType.Int32));
                objDBParam.Add(new DbParameter("@ProductCode", assetData.ProductCode, DbType.String));
                objDBParam.Add(new DbParameter("@ProductName", assetData.ProductName, DbType.String));
                objDBParam.Add(new DbParameter("@Category", assetData.Category, DbType.String));
                objDBParam.Add(new DbParameter("@Type", assetData.Type, DbType.String));
                objDBParam.Add(new DbParameter("@Status", assetData.Status));
                objDBParam.Add(new DbParameter("@UserID", assetData.UserId, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[UpdateAsset]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return 1;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("UpdateAsset", ex.StackTrace, ex.Message);
                Message = ex.Message;
            }
            return 0;
        }

        public int DeleteAsset(AssetMaster assetData, out string Message)
        {
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@AssetId", assetData.Id, DbType.Int32));
                objDBParam.Add(new DbParameter("@UserId", assetData.UserId, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[DeleteAsset]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return 1;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("DeleteAsset", ex.StackTrace, ex.Message);
                Message = "Could not delete asset. Exception occured in API.";
            }
            return 0;
        }

        public IEnumerable<AssetMaster> GetAllAssets()
        {
            IEnumerable<AssetMaster> listAssets = null;
            try
            {
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetAsset]", CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    listAssets = (from asset in outputFromSP.AsEnumerable()
                                     select new AssetMaster
                                     {
                                         Id = Convert.ToInt64(asset.Field<Int64>("ID")),
                                         ProductCode = Convert.ToString(asset.Field<string>("PRODUCTCODE")),
                                         ProductName = Convert.ToString(asset.Field<string>("PRODUCTNAME")),
                                         Category = Convert.ToString(asset.Field<string>("CATEGORY")),
                                         Type = Convert.ToString(asset.Field<string>("TYPE")),
                                         Status = Convert.ToBoolean(asset.Field<bool>("STATUS")),
                                         CreatedBy = Convert.ToInt64(asset.Field<Int64>("CREATEDBY")),
                                         CreatedDate = Convert.ToDateTime(asset.Field<DateTime>("CREATIONDATE")),
                                         ModifiedBy = Convert.ToInt64(asset.Field<Int64?>("LASTMODIFIEDBY")),
                                         LastModificationDate = Convert.ToDateTime(asset.Field<DateTime?>("LASTMODDATE"))
                                     });
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetAllAssets", ex.StackTrace, ex.Message);
            }
            return listAssets;
        }

        public AssetMaster GetAssetById(Int64 assetId)
        {
            DbParameterCollection objDBParam = new DbParameterCollection();
            AssetMaster outputAsset = null;
            try
            {
                objDBParam.Add(new DbParameter("@AssetId", assetId, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetAsset]", objDBParam, CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    outputAsset = (from asset in outputFromSP.AsEnumerable()
                                      select new AssetMaster
                                      {
                                          Id = Convert.ToInt64(asset.Field<Int64>("ID")),
                                          ProductCode = Convert.ToString(asset.Field<string>("PRODUCTCODE")),
                                          ProductName = Convert.ToString(asset.Field<string>("PRODUCTNAME")),
                                          Category = Convert.ToString(asset.Field<string>("CATEGORY")),
                                          Type = Convert.ToString(asset.Field<string>("TYPE")),
                                          Status = Convert.ToBoolean(asset.Field<bool>("STATUS")),
                                          CreatedBy = Convert.ToInt64(asset.Field<Int64>("CREATEDBY")),
                                          CreatedDate = Convert.ToDateTime(asset.Field<DateTime>("CREATIONDATE")),
                                          ModifiedBy = Convert.ToInt64(asset.Field<Int64?>("LASTMODIFIEDBY")),
                                          LastModificationDate = Convert.ToDateTime(asset.Field<DateTime?>("LASTMODDATE"))
                                      }).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetAssetById", ex.StackTrace, ex.Message);
            }
            return outputAsset;
        }
        #endregion Asset Master API

        #region Product Target Category Master API
        public int CreateProductTargetCategory(ProductTargetCategoryMaster productTargetCategoryData, out string Message)
        {
            bool IsDone = false;
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@ProductCategoryId", productTargetCategoryData.ProductCategoryId, DbType.Int64));
                objDBParam.Add(new DbParameter("@TargetCategory", productTargetCategoryData.TargetCategory, DbType.String));
                objDBParam.Add(new DbParameter("@Status", productTargetCategoryData.Status));
                objDBParam.Add(new DbParameter("@CreatedBy", productTargetCategoryData.UserId, DbType.Int64));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[CreateProductTargetCategory]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    IsDone = true;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("CreateProductTargetCategory", ex.StackTrace, ex.Message);
                Message = "Error occured while creating Product Target Category";
            }

            return IsDone == true ? 1 : 0;
        }

        public int UpdateProductTargetCategory(ProductTargetCategoryMaster productTargetCategoryData, out string Message)
        {
            bool IsDone = false;
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@Id", productTargetCategoryData.Id, DbType.Int64));
                objDBParam.Add(new DbParameter("@ProductCategoryId", productTargetCategoryData.ProductCategoryId, DbType.Int64));
                objDBParam.Add(new DbParameter("@TargetCategory", productTargetCategoryData.TargetCategory, DbType.String));
                objDBParam.Add(new DbParameter("@Status", productTargetCategoryData.Status));
                objDBParam.Add(new DbParameter("@UserId", productTargetCategoryData.UserId, DbType.Int64));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[UpdateProductTargetCategory]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    IsDone = true;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("UpdateProductTargetCategory", ex.StackTrace, ex.Message);
                Message = "Error occured while updating Product Target Category";
            }

            return IsDone == true ? 1 : 0;
        }

        public int DeleteProductTargetCategory(ProductTargetCategoryMaster productTargetCategoryData, out string Message)
        {
            bool IsDone = false;
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@Id", productTargetCategoryData.Id, DbType.Int64));
                objDBParam.Add(new DbParameter("@UserId", productTargetCategoryData.UserId, DbType.Int64));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[DeleteProductTargetCategory]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    IsDone = true;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("DeleteProductTargetCategory", ex.StackTrace, ex.Message);
                Message = "Error occured while deleting Product Target Category";
            }

            return IsDone == true ? 1 : 0;
        }

        public IEnumerable<ProductTargetCategoryMaster> GetAllProductTargetCategories()
        {
            IEnumerable<ProductTargetCategoryMaster> listProductTargetCategory = null;
            try
            {
                var outputDBParam = _dataHelper.ExecuteDataTable("[dbo].[GetProductTargetCategory]", CommandType.StoredProcedure);
                if (outputDBParam.Rows.Count > 0)
                {
                    listProductTargetCategory = (from producttargetcategory in outputDBParam.AsEnumerable()
                                                 select new ProductTargetCategoryMaster
                                                 {
                                                     Id = Convert.ToInt64(producttargetcategory.Field<Int64>("ID")),
                                                     ProductCategoryId = Convert.ToInt64(producttargetcategory.Field<Int64>("PRODUCTCATEGORYID")),
                                                     TargetCategory = Convert.ToString(producttargetcategory.Field<string>("TARGETCATEGORY")),
                                                     IsActive = Convert.ToBoolean(producttargetcategory.Field<bool>("ISACTIVE")),
                                                     CreatedBy = Convert.ToInt64(producttargetcategory.Field<Int64>("CREATEDBY")),
                                                     CreationDate = Convert.ToDateTime(producttargetcategory.Field<DateTime>("CREATIONDATE")),
                                                     LastModifiedBy = Convert.ToInt64(producttargetcategory.Field<Int64?>("LASTMODIFIEDBY")),
                                                     LastModificationDate = Convert.ToDateTime(producttargetcategory.Field<DateTime?>("LASTMODIFICATIONDATE"))
                                                 });
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetAllProductTargetCategories", ex.StackTrace, ex.Message);
            }
            return listProductTargetCategory;
        }

        public ProductTargetCategoryMaster GetProductTargetCategoryById(Int64 productTargetCategoryId)
        {
            DbParameterCollection objDBParam = new DbParameterCollection();
            ProductTargetCategoryMaster ProductTargetCategory = null;
            try
            {
                objDBParam.Add(new DbParameter("@Id", productTargetCategoryId, DbType.Int64));
                var outputSP = _dataHelper.ExecuteDataTable("[dbo].[GetProductTargetCategory]", objDBParam, CommandType.StoredProcedure);
                if (outputSP.Rows.Count > 0)
                {
                    ProductTargetCategory = (from producttargetcategory in outputSP.AsEnumerable()
                                             select new ProductTargetCategoryMaster
                                             {
                                                 Id = Convert.ToInt64(producttargetcategory.Field<Int64>("ID")),
                                                 ProductCategoryId = Convert.ToInt64(producttargetcategory.Field<Int64>("PRODUCTCATEGORYID")),
                                                 TargetCategory = Convert.ToString(producttargetcategory.Field<string>("TARGETCATEGORY")),
                                                 IsActive = Convert.ToBoolean(producttargetcategory.Field<bool>("ISACTIVE")),
                                                 CreatedBy = Convert.ToInt64(producttargetcategory.Field<Int64>("CREATEDBY")),
                                                 CreationDate = Convert.ToDateTime(producttargetcategory.Field<DateTime>("CREATIONDATE")),
                                                 LastModifiedBy = Convert.ToInt64(producttargetcategory.Field<Int64?>("LASTMODIFIEDBY")),
                                                 LastModificationDate = Convert.ToDateTime(producttargetcategory.Field<DateTime?>("LASTMODIFICATIONDATE"))
                                             }).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetProductTargetCategoryById", ex.StackTrace, ex.Message);
            }
            return ProductTargetCategory;
        }
        #endregion Product Target Category Master API

        #region Product Definition Under Target Category Master API (to be removed) (written in Mappings)
        public int CreateProductDefinitionUnderTargetCategory(ProductDefinitionUnderTargetCategory productDefinitionUnderTargetCategoryData, out string Message)
        {
            bool IsDone = false;
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@ProductCategoryId", productDefinitionUnderTargetCategoryData.ProductCategoryId, DbType.Int64));
                objDBParam.Add(new DbParameter("@ProductSubCatId", productDefinitionUnderTargetCategoryData.ProductSubCatId, DbType.Int64));
                objDBParam.Add(new DbParameter("@MaterialId", productDefinitionUnderTargetCategoryData.MaterialId, DbType.Int64));
                objDBParam.Add(new DbParameter("@TargetCategoryId", productDefinitionUnderTargetCategoryData.TargetCategoryId, DbType.Int64));
                objDBParam.Add(new DbParameter("@IsActive", productDefinitionUnderTargetCategoryData.IsActive));
                objDBParam.Add(new DbParameter("@CreatedBy", productDefinitionUnderTargetCategoryData.UserId, DbType.Int64));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[CreateProductDefinitionUnderTargetCategory]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    IsDone = true;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("CreateProductDefinitionUnderTargetCategory", ex.StackTrace, ex.Message);
                Message = "Error occured while creating Product Definition Under Target Category";
            }

            return IsDone == true ? 1 : 0;
        }

        public int UpdateProductDefinitionUnderTargetCategory(ProductDefinitionUnderTargetCategory productDefinitionUnderTargetCategoryData, out string Message)
        {
            bool IsDone = false;
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@Id", productDefinitionUnderTargetCategoryData.Id, DbType.Int64));
                objDBParam.Add(new DbParameter("@ProductCategoryId", productDefinitionUnderTargetCategoryData.ProductCategoryId, DbType.Int64));
                objDBParam.Add(new DbParameter("@ProductSubCatId", productDefinitionUnderTargetCategoryData.ProductSubCatId, DbType.Int64));
                objDBParam.Add(new DbParameter("@MaterialId", productDefinitionUnderTargetCategoryData.MaterialId, DbType.Int64));
                objDBParam.Add(new DbParameter("@TargetCategoryId", productDefinitionUnderTargetCategoryData.TargetCategoryId, DbType.Int64));
                objDBParam.Add(new DbParameter("@IsActive", productDefinitionUnderTargetCategoryData.IsActive));
                objDBParam.Add(new DbParameter("@UserId", productDefinitionUnderTargetCategoryData.UserId, DbType.Int64));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[UpdateProductDefinitionUnderTargetCategory]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    IsDone = true;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("UpdateProductDefinitionUnderTargetCategory", ex.StackTrace, ex.Message);
                Message = "Error occured while updating Product Definition Under Target Category";
            }

            return IsDone == true ? 1 : 0;
        }

        public int DeleteProductDefinitionUnderTargetCategory(ProductDefinitionUnderTargetCategory productDefinitionUnderTargetCategoryData, out string Message)
        {
            bool IsDone = false;
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@Id", productDefinitionUnderTargetCategoryData.Id, DbType.Int64));
                objDBParam.Add(new DbParameter("@UserId", productDefinitionUnderTargetCategoryData.UserId, DbType.Int64));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[DeleteProductDefinitionUnderTargetCategory]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    IsDone = true;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("DeleteProductDefinitionUnderTargetCategory", ex.StackTrace, ex.Message);
                Message = "Error occured while deleting Product Definition Under Target Category";
            }

            return IsDone == true ? 1 : 0;
        }

        public IEnumerable<ProductDefinitionUnderTargetCategory> GetAllProductDefinitionUnderTargetCategories()
        {
            IEnumerable<ProductDefinitionUnderTargetCategory> listProductDefinitionUnderTargetCategory = null;
            try
            {
                var outputDBParam = _dataHelper.ExecuteDataTable("[dbo].[GetProductDefinitionUnderTargetCategory]", CommandType.StoredProcedure);
                if (outputDBParam.Rows.Count > 0)
                {
                    listProductDefinitionUnderTargetCategory = (from productdefinitionundertargetcategory in outputDBParam.AsEnumerable()
                                                                select new ProductDefinitionUnderTargetCategory
                                                                {
                                                                    Id = Convert.ToInt64(productdefinitionundertargetcategory.Field<Int64>("ID")),
                                                                    ProductCategoryId = Convert.ToInt64(productdefinitionundertargetcategory.Field<Int64>("PRODUCTCATEGORYID")),
                                                                    ProductSubCatId = Convert.ToInt64(productdefinitionundertargetcategory.Field<Int64>("PRODUCTSUBCATID")),
                                                                    MaterialId = Convert.ToInt64(productdefinitionundertargetcategory.Field<Int64>("MATERIALID")),
                                                                    TargetCategoryId = Convert.ToInt64(productdefinitionundertargetcategory.Field<Int64>("TARGETCATEGORYID")),
                                                                    IsActive = Convert.ToBoolean(productdefinitionundertargetcategory.Field<bool>("ISACTIVE")),
                                                                    CreatedBy = Convert.ToInt64(productdefinitionundertargetcategory.Field<Int64>("CREATEDBY")),
                                                                    CreationDate = Convert.ToDateTime(productdefinitionundertargetcategory.Field<DateTime>("CREATIONDATE")),
                                                                    LastModifiedBy = Convert.ToInt64(productdefinitionundertargetcategory.Field<Int64?>("LASTMODIFIEDBY")),
                                                                    LastModificationDate = Convert.ToDateTime(productdefinitionundertargetcategory.Field<DateTime?>("LASTMODIFICATIONDATE"))
                                                                });
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetAllProductDefinitionUnderTargetCategories", ex.StackTrace, ex.Message);
            }
            return listProductDefinitionUnderTargetCategory;
        }

        public ProductDefinitionUnderTargetCategory GetProductDefinitionUnderTargetCategoryById(Int64 productDefinitionUnderTargetCategoryId)
        {
            DbParameterCollection objDBParam = new DbParameterCollection();
            ProductDefinitionUnderTargetCategory ProductDefinitionUnderTargetCategory = null;
            try
            {
                objDBParam.Add(new DbParameter("@Id", productDefinitionUnderTargetCategoryId, DbType.Int64));
                var outputSP = _dataHelper.ExecuteDataTable("[dbo].[GetProductDefinitionUnderTargetCategory]", objDBParam, CommandType.StoredProcedure);
                if (outputSP.Rows.Count > 0)
                {
                    ProductDefinitionUnderTargetCategory = (from productdefinitionundertargetcategory in outputSP.AsEnumerable()
                                                            select new ProductDefinitionUnderTargetCategory
                                                            {
                                                                Id = Convert.ToInt64(productdefinitionundertargetcategory.Field<Int64>("ID")),
                                                                ProductCategoryId = Convert.ToInt64(productdefinitionundertargetcategory.Field<Int64>("PRODUCTCATEGORYID")),
                                                                ProductSubCatId = Convert.ToInt64(productdefinitionundertargetcategory.Field<Int64>("PRODUCTSUBCATID")),
                                                                MaterialId = Convert.ToInt64(productdefinitionundertargetcategory.Field<Int64>("MATERIALID")),
                                                                TargetCategoryId = Convert.ToInt64(productdefinitionundertargetcategory.Field<Int64>("TARGETCATEGORYID")),
                                                                IsActive = Convert.ToBoolean(productdefinitionundertargetcategory.Field<bool>("ISACTIVE")),
                                                                CreatedBy = Convert.ToInt64(productdefinitionundertargetcategory.Field<Int64>("CREATEDBY")),
                                                                CreationDate = Convert.ToDateTime(productdefinitionundertargetcategory.Field<DateTime>("CREATIONDATE")),
                                                                LastModifiedBy = Convert.ToInt64(productdefinitionundertargetcategory.Field<Int64?>("LASTMODIFIEDBY")),
                                                                LastModificationDate = Convert.ToDateTime(productdefinitionundertargetcategory.Field<DateTime?>("LASTMODIFICATIONDATE"))
                                                            }).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetProductDefinitionUnderTargetCategoryById", ex.StackTrace, ex.Message);
            }
            return ProductDefinitionUnderTargetCategory;
        }
        #endregion Product Definition Under Target Category Master API

        #region SFA Salary Master API
        public int CreateSFASalaryMaster(SFASalaryMasterGrid sfaSalaryData, out string Message)
        {
            bool IsDone = false;
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@LoginId", sfaSalaryData.LoginId, DbType.Int64));
                objDBParam.Add(new DbParameter("@Basic", AMBOEcryption.EncryptData((sfaSalaryData.Basic != null ? sfaSalaryData.Basic.Trim() : null), true), DbType.String));
                objDBParam.Add(new DbParameter("@HRA", AMBOEcryption.EncryptData((sfaSalaryData.HRA != null ? sfaSalaryData.HRA.Trim() : null), true), DbType.String));
                objDBParam.Add(new DbParameter("@Med", AMBOEcryption.EncryptData((sfaSalaryData.Med != null ? sfaSalaryData.Med.Trim() : null), true), DbType.String));
                objDBParam.Add(new DbParameter("@Conv", AMBOEcryption.EncryptData((sfaSalaryData.Conv != null ? sfaSalaryData.Conv.Trim() : null), true), DbType.String));
                objDBParam.Add(new DbParameter("@Other", AMBOEcryption.EncryptData((sfaSalaryData.Other != null ? sfaSalaryData.Other.Trim() : null), true), DbType.String));
                objDBParam.Add(new DbParameter("@Airtime", AMBOEcryption.EncryptData((sfaSalaryData.Airtime != null ? sfaSalaryData.Airtime.Trim() : null), true), DbType.String));
                objDBParam.Add(new DbParameter("@Insurance", AMBOEcryption.EncryptData((sfaSalaryData.Insurance != null ? sfaSalaryData.Insurance.Trim() : null), true), DbType.String));
                objDBParam.Add(new DbParameter("@OtherAllowance1", sfaSalaryData.OtherAllowance1 == null ? "" : AMBOEcryption.EncryptData(sfaSalaryData.OtherAllowance1.Trim(), true), DbType.String));
                objDBParam.Add(new DbParameter("@OtherAllowance2", sfaSalaryData.OtherAllowance2 == null ? "" : AMBOEcryption.EncryptData(sfaSalaryData.OtherAllowance2.Trim(), true), DbType.String));
                objDBParam.Add(new DbParameter("@UserId", sfaSalaryData.UserId, DbType.Int64));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[CreateSFASalaryMaster]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    IsDone = true;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("CreateSFASalaryMaster", ex.StackTrace, ex.Message);
                Message = "Error occured while updating SFA Salary Master";
            }

            return IsDone == true ? 1 : 0;
        }

        public int UpdateSFASalaryMaster(SFASalaryMasterGrid sfaSalaryData, out string Message)
        {
            bool IsDone = false;
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {    
                objDBParam.Add(new DbParameter("@Id", sfaSalaryData.Id, DbType.Int64));
                objDBParam.Add(new DbParameter("@LoginId", sfaSalaryData.LoginId, DbType.Int64));
                objDBParam.Add(new DbParameter("@Basic", AMBOEcryption.EncryptData((sfaSalaryData.Basic != null ? sfaSalaryData.Basic.Trim() : null), true), DbType.String));
                objDBParam.Add(new DbParameter("@HRA", AMBOEcryption.EncryptData((sfaSalaryData.HRA != null ? sfaSalaryData.HRA.Trim() : null), true), DbType.String));
                objDBParam.Add(new DbParameter("@Med", AMBOEcryption.EncryptData((sfaSalaryData.Med != null ? sfaSalaryData.Med.Trim() : null), true), DbType.String));
                objDBParam.Add(new DbParameter("@Conv", AMBOEcryption.EncryptData((sfaSalaryData.Conv != null ? sfaSalaryData.Conv.Trim() : null), true), DbType.String));
                objDBParam.Add(new DbParameter("@Other", AMBOEcryption.EncryptData((sfaSalaryData.Other != null ? sfaSalaryData.Other.Trim() : null), true), DbType.String));
                objDBParam.Add(new DbParameter("@Airtime", AMBOEcryption.EncryptData((sfaSalaryData.Airtime != null ? sfaSalaryData.Airtime.Trim() : null), true), DbType.String));
                objDBParam.Add(new DbParameter("@Insurance", AMBOEcryption.EncryptData((sfaSalaryData.Insurance != null ? sfaSalaryData.Insurance.Trim() : null), true), DbType.String));
                objDBParam.Add(new DbParameter("@OtherAllowance1", AMBOEcryption.EncryptData((sfaSalaryData.OtherAllowance1 != null ? sfaSalaryData.OtherAllowance1.Trim() : null), true), DbType.String));
                objDBParam.Add(new DbParameter("@OtherAllowance2", AMBOEcryption.EncryptData((sfaSalaryData.OtherAllowance2 != null ? sfaSalaryData.OtherAllowance2.Trim() : null), true), DbType.String));
                objDBParam.Add(new DbParameter("@UserId", sfaSalaryData.UserId, DbType.Int64));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[UpdateSFASalaryMaster]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    IsDone = true;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("UpdateSFASalaryMaster", ex.StackTrace, ex.Message);
                Message = "Error occured while updating SFA Salary Master";
            }

            return IsDone == true ? 1 : 0;
        }

        public int DeleteSFASalaryMaster(SFASalaryMasterDelete sfaSalaryData, out string Message)
        {
            bool IsDone = false;
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@Id", sfaSalaryData.Id, DbType.Int64));
                objDBParam.Add(new DbParameter("@UserId", sfaSalaryData.UserId, DbType.Int64));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[DeleteSFASalaryMaster]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    IsDone = true;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("DeleteSFASalaryMaster", ex.StackTrace, ex.Message);
                Message = "Error occured while deleting SFA Salary Master";
            }

            return IsDone == true ? 1 : 0;
        }

        public SFASalaryMasterGrid GetSFASalaryMasterById(Int64 sfaSalaryMasterId)
        {
            SFASalaryMasterGrid sfaMasterforHR = null;
            DbParameterCollection paramcollection = new DbParameterCollection();
            try
            {
                paramcollection.Add(new DbParameter("@Id", sfaSalaryMasterId, DbType.Int64));
                var outputDBParam = _dataHelper.ExecuteDataTable("[dbo].[GetSFASalaryMasterById]", paramcollection, CommandType.StoredProcedure);
                if (outputDBParam.Rows.Count > 0)
                {
                    sfaMasterforHR = (from sfasalarymaster in outputDBParam.AsEnumerable()
                                      select new SFASalaryMasterGrid
                                      {
                                          Id = Convert.ToInt64(sfasalarymaster.Field<Int64?>("ID")),
                                          LoginId = Convert.ToInt64(sfasalarymaster.Field<Int64>("LOGINID")),
                                          SFAName = Convert.ToString(sfasalarymaster.Field<string>("SFANAME")),
                                          SFACode = Convert.ToString(sfasalarymaster.Field<string>("SFACode")),                                        
                                          Branch = Convert.ToString(sfasalarymaster.Field<string>("BRANCH")),                                          
                                          State = Convert.ToString(sfasalarymaster.Field<string>("STATE")),
                                          City = Convert.ToString(sfasalarymaster.Field<string>("CITY")),
                                          Division = Convert.ToString(sfasalarymaster.Field<string>("DIVISION")),
                                          SFALevel = Convert.ToString(sfasalarymaster.Field<string>("SFALEVEL")),
                                          Basic = Convert.ToString(sfasalarymaster.Field<string>("BASIC")) == "" ?
                                                "" : AMBOEcryption.DecryptData(sfasalarymaster.Field<string>("BASIC"), true),
                                          HRA = Convert.ToString(sfasalarymaster.Field<string>("HRA")) == "" ?
                                                "" : AMBOEcryption.DecryptData(sfasalarymaster.Field<string>("HRA"), true),
                                          Med = Convert.ToString(sfasalarymaster.Field<string>("MED")) == "" ?
                                                "" : AMBOEcryption.DecryptData(sfasalarymaster.Field<string>("MED"), true),
                                          Other = Convert.ToString(sfasalarymaster.Field<string>("OTHER")) == "" ?
                                                "" : AMBOEcryption.DecryptData(sfasalarymaster.Field<string>("OTHER"), true),
                                          Conv = Convert.ToString(sfasalarymaster.Field<string>("CONV")) == "" ?
                                                "" : AMBOEcryption.DecryptData(sfasalarymaster.Field<string>("CONV"), true),
                                          Airtime = Convert.ToString(sfasalarymaster.Field<string>("AIRTIME")) == "" ?
                                                "" : AMBOEcryption.DecryptData(sfasalarymaster.Field<string>("AIRTIME"), true),
                                          Insurance = Convert.ToString(sfasalarymaster.Field<string>("INSURANCE")) == "" ?
                                                "" : AMBOEcryption.DecryptData(sfasalarymaster.Field<string>("INSURANCE"), true),
                                          OtherAllowance1 = Convert.ToString(sfasalarymaster.Field<string>("OTHERALLOWANCE1")) == "" ?
                                                "" : AMBOEcryption.DecryptData(sfasalarymaster.Field<string>("OTHERALLOWANCE1"), true),
                                          OtherAllowance2 = Convert.ToString(sfasalarymaster.Field<string>("OTHERALLOWANCE2")) == "" ?
                                                "" : AMBOEcryption.DecryptData(sfasalarymaster.Field<string>("OTHERALLOWANCE2"), true)

                                      }).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetSFASalaryMasterById", ex.StackTrace, ex.Message);
            }

            return sfaMasterforHR;
        }

        public IEnumerable<SFASalaryMasterGrid> SFASalaryMasterDataDownload(SFASalaryMasterDownload InputParam)
        {
            IEnumerable<SFASalaryMasterGrid> sfaSalaryMasterList = null;
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

                var outputDBParam = _dataHelper.ExecuteProcedure("[dbo].[SFASalaryMasterDataDownload]", objDBParam);
                if (outputDBParam.Rows.Count > 0)
                {
                    sfaSalaryMasterList = (from sfamasterforhr in outputDBParam.AsEnumerable()
                                          select new SFASalaryMasterGrid
                                          {
                                              Id = Convert.ToInt64(sfamasterforhr.Field<Int64>("ID")),
                                              SFAName = Convert.ToString(sfamasterforhr.Field<string>("SFAName")),
                                              SFACode = Convert.ToString(sfamasterforhr.Field<string>("SFACode")),
                                              Region = Convert.ToString(sfamasterforhr.Field<string>("Region")),
                                              Branch = Convert.ToString(sfamasterforhr.Field<string>("Branch")),                                              
                                              City = Convert.ToString(sfamasterforhr.Field<string>("City")),
                                              Division = Convert.ToString(sfamasterforhr.Field<string>("Division")),                                              
                                              SFALevel = Convert.ToString(sfamasterforhr.Field<string>("SFASubLevelName")),
                                              Basic = Convert.ToString(sfamasterforhr.Field<string>("Basic")) == "" ?
                                                "" : AMBOEcryption.DecryptData(sfamasterforhr.Field<string>("Basic"), true),
                                              HRA = Convert.ToString(sfamasterforhr.Field<string>("HRA")) == "" ?
                                                "" : AMBOEcryption.DecryptData(sfamasterforhr.Field<string>("HRA"), true),
                                              Med = Convert.ToString(sfamasterforhr.Field<string>("Med")) == "" ?
                                                "" : AMBOEcryption.DecryptData(sfamasterforhr.Field<string>("Med"), true),
                                              Conv = Convert.ToString(sfamasterforhr.Field<string>("Conv")) == "" ?
                                                "" : AMBOEcryption.DecryptData(sfamasterforhr.Field<string>("Conv"), true),
                                              Other = Convert.ToString(sfamasterforhr.Field<string>("Other")) == "" ?
                                                "" : AMBOEcryption.DecryptData(sfamasterforhr.Field<string>("Other"), true),
                                              Airtime = Convert.ToString(sfamasterforhr.Field<string>("Airtime")) == "" ?
                                                "" : AMBOEcryption.DecryptData(sfamasterforhr.Field<string>("Airtime"), true),
                                              Insurance = Convert.ToString(sfamasterforhr.Field<string>("Insurance")) == "" ?
                                                "" : AMBOEcryption.DecryptData(sfamasterforhr.Field<string>("Insurance"), true),                                              
                                          });
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("SFASalaryMasterDataDownload", ex.StackTrace, ex.Message);
            }

            return sfaSalaryMasterList;
        }

        public DataTable ManageSFASalaryMasterData(List<SFASalaryMasterGrid> InputParam)
        {
            DataRow row;
            DataTable dtSFADetails = new DataTable();
            DataTable dtResult = new DataTable();

            try
            {
                dtSFADetails.Columns.Add("UserId");
                dtSFADetails.Columns.Add("SFACode");
                dtSFADetails.Columns.Add("SFAName");                
                dtSFADetails.Columns.Add("Branch");
                dtSFADetails.Columns.Add("City");
                dtSFADetails.Columns.Add("Division");
                dtSFADetails.Columns.Add("Basic");
                dtSFADetails.Columns.Add("HRA");
                dtSFADetails.Columns.Add("Med");
                dtSFADetails.Columns.Add("Conv");
                dtSFADetails.Columns.Add("Other");
                dtSFADetails.Columns.Add("Airtime");
                dtSFADetails.Columns.Add("Insurance");                
                if (InputParam.Count > 0)
                {
                    foreach (SFASalaryMasterGrid details in InputParam)
                    {
                        row = dtSFADetails.NewRow();
                        row["UserId"] = details.UserId;
                        row["SFACode"] = details.SFACode;
                        row["SFAName"] = details.SFAName;
                        row["Basic"] = AMBOEcryption.EncryptData((details.Basic != null ? details.Basic.Trim() : null), true);
                        row["HRA"] = AMBOEcryption.EncryptData((details.HRA != null ? details.HRA.Trim() : null), true);
                        row["Med"] = AMBOEcryption.EncryptData((details.Med != null ? details.Med.Trim() : null), true);
                        row["Branch"] = details.Branch;
                        row["City"] = details.City;
                        row["Division"] = details.Division;
                        row["Conv"] = AMBOEcryption.EncryptData((details.Conv != null ? details.Conv.Trim() : null), true);
                        row["Other"] = AMBOEcryption.EncryptData((details.Other != null ? details.Other.Trim() : null), true);
                        row["Airtime"] = AMBOEcryption.EncryptData((details.Airtime != null ? details.Airtime.Trim() : null), true);
                        row["Insurance"] = AMBOEcryption.EncryptData((details.Insurance != null ? details.Insurance.Trim() : null), true);                        
                        dtSFADetails.Rows.Add(row);
                    }
                }

                SqlParameter[] objDBParam = new SqlParameter[1];
                objDBParam[0] = new SqlParameter("@SFADetails", dtSFADetails);

                dtResult = _dataHelper.ExecuteProcedure("[dbo].[ManageSFASalaryMasterData]", objDBParam);
                if (dtResult.Rows.Count > 0)
                    return dtResult;
                else
                    return null;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("ManageSFASalaryMasterData", ex.StackTrace, ex.Message);
                return null;
            }
        }
        #endregion SFA Salary Master API

        #region Target Date Setting Master API
        public int UpdateTargetDateSettingMaster(TargetDateSettingMaster targetDateSetting, out string Message)
        {
            int IsDone = 0;
            Message = string.Empty;
            DataTable dtbranches = new DataTable();
            DataRow row;
            try
            {
                dtbranches.Columns.Add("FilterId");
                if (targetDateSetting.BranchIds != null)
                {
                    foreach (Int64 branchid in targetDateSetting.BranchIds)
                    {
                        row = dtbranches.NewRow();
                        row["FilterId"] = branchid;
                        dtbranches.Rows.Add(row);
                    }
                }

                SqlParameter[] objDBParam = new SqlParameter[3];
                objDBParam[0] = new SqlParameter("@BranchIds", dtbranches);
                objDBParam[1] = new SqlParameter("@TargetDate", targetDateSetting.TargetDate);
                objDBParam[2] = new SqlParameter("@UserId", targetDateSetting.UserId);
                
                var outputFromSP = _dataHelper.ExecuteProcedure("[dbo].[UpdateTargetDateSetting]", objDBParam);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    IsDone = 1;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("UpdateTargetDateSettingMaster", ex.StackTrace, ex.Message);
                Message = "Error occured while updating Target Date Setting Master";
            }

            return IsDone;
        }

        public int CreateTargetDateSettingMaster(TargetDateSettingMaster targetDateSetting, out string Message)
        {
            bool IsDone = false;
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@BranchId", targetDateSetting.BranchId, DbType.Int64));
                objDBParam.Add(new DbParameter("@TargetDate", targetDateSetting.TargetDate, DbType.DateTime));
                objDBParam.Add(new DbParameter("@CreatedBy", targetDateSetting.UserId, DbType.Int64));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetTargetDateSettingGrid]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    IsDone = true;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("CreateTargetDateSettingMaster", ex.StackTrace, ex.Message);
                Message = "Error occured while creating Target Date Setting Master";
            }

            return IsDone == true ? 1 : 0;
        }

        public TargetDateSettingMaster GetTargetDateSettingMasterById(Int64 targetDateSettingId)
        {
            TargetDateSettingMaster targetDate = new TargetDateSettingMaster();
            DbParameterCollection paramcollection = new DbParameterCollection();
            try
            {
                paramcollection.Add(new DbParameter("@Id", targetDateSettingId, DbType.Int64));
                var outputDBParam = _dataHelper.ExecuteDataTable("[dbo].[GetTargetDateSettingById]", paramcollection, CommandType.StoredProcedure);
                if (outputDBParam.Rows.Count > 0)
                {
                    targetDate = (from targetdatemaster in outputDBParam.AsEnumerable()
                                      select new TargetDateSettingMaster
                                      {
                                          Id = Convert.ToInt64(targetdatemaster.Field<Int64>("ID")),
                                          BranchId=Convert.ToInt64(targetdatemaster.Field<Int64>("BRANCHID")),
                                          TargetDate=targetdatemaster.Field<string>("TARGETDATE")
                                      }).FirstOrDefault();
                }
                return targetDate;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetTargetDateSettingMasterById", ex.StackTrace, ex.Message);
                return targetDate;
            }
        }
        #endregion Target Date Setting Master API

        #region Broadcast Message
        public CreateBroadcastMessageFormOutput CreateBroadcastMessage(CreateBroadcastMessageForm objBroadcastData, out string Message)
        {
            Message = string.Empty;
            SqlParameter[] objDBParam = new SqlParameter[10];
            DataTable UserIds = new DataTable();
            DataTable dtRoleIds = new DataTable();
            DataRow dr;
            string path = string.Empty;
            string base64String = string.Empty;
            string filepath = string.Empty;
            string filebase64String = string.Empty;
            CreateBroadcastMessageFormOutput objOutput = new CreateBroadcastMessageFormOutput();
            try
            {
                UserIds.Columns.Add("UserId");
                foreach(Int64 id in objBroadcastData.SFAIds)
                {
                    dr = UserIds.NewRow();
                    dr["UserId"] = id;
                    UserIds.Rows.Add(dr);
                }
                foreach (Int64 id in objBroadcastData.SIDIds)
                {
                    dr = UserIds.NewRow();
                    dr["UserId"] = id;
                    UserIds.Rows.Add(dr);
                }



                dtRoleIds.Columns.Add("FilterId");
                if (objBroadcastData.RoleIds != null && objBroadcastData.RoleIds.Count() > 0)
                {
                    foreach (Int64 id in objBroadcastData.RoleIds)
                    {
                        dr = dtRoleIds.NewRow();
                        dr["FilterId"] = id;
                        dtRoleIds.Rows.Add(dr);
                    }
                }

                //if (objBroadcastData.FileName != null)
                //{
                //    filepath = objBroadcastData.a.Length > 0 ? "Invoice_Images/" + objBroadcastData.UserId + "_" + InputData.ComboInvoiceNumber + "_" + ".jpg" : "";
                //    filebase64String = objBroadcastData.ComboInvoiceImage;
                //    if (!String.IsNullOrEmpty(objBroadcastData.ComboInvoiceImage))
                //    {
                //        //move file path to controller
                //        string filePath = System.Web.HttpContext.Current.Server.MapPath("~/" + filepath);
                //        string DirectoryPath = System.Web.HttpContext.Current.Server.MapPath("~/" + "Invoice_Images");
                //        if (!Directory.Exists(DirectoryPath))
                //        {
                //            Directory.CreateDirectory(DirectoryPath);
                //        }
                //        if (File.Exists(filePath))
                //        {
                //            File.SetAttributes(filePath, FileAttributes.Normal);
                //            File.Delete(filePath);
                //        }
                //        File.WriteAllBytes(filePath, Convert.FromBase64String(filebase64String));
                //    }
                //}

                objDBParam[0] = new SqlParameter("@ID", objBroadcastData.Id);
                objDBParam[1] = new SqlParameter("@Subject", objBroadcastData.Subject);
                objDBParam[2] = new SqlParameter("@Message", objBroadcastData.Message);
                objDBParam[3] = new SqlParameter("@FileName", objBroadcastData.FileName == null ? "" : objBroadcastData.FileName);
                objDBParam[4] = new SqlParameter("@StartDate", DateTime.ParseExact(objBroadcastData.StartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture));
                objDBParam[5] = new SqlParameter("@EndDate", DateTime.ParseExact(objBroadcastData.EndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture));
                objDBParam[6] = new SqlParameter("@UserIds", UserIds);
                objDBParam[7] = new SqlParameter("@Status", objBroadcastData.Status);
                objDBParam[8] = new SqlParameter("@UserId", objBroadcastData.UserId);
                objDBParam[9] = new SqlParameter("@RoleIds", dtRoleIds);


                var outputFromSP = _dataHelper.ExecuteProcedure("[dbo].[CreateBroadcastMessage]", objDBParam);
                if(outputFromSP != null && outputFromSP.Rows.Count > 0)
                {
                    Message = "Broadcast message saved successfully.";
                    //objOutput.DeviceIds = outputFromSP.AsEnumerable().Select(x => x[3].ToString()).ToList();
                    objOutput.MessageId = Convert.ToInt64(outputFromSP.Rows[0][1]);

                    objOutput.FCMIds = (from row in outputFromSP.AsEnumerable() select new FCMIDListWithRole {
                            FCMId = row.Field<string>("FCMId"),
                            RoleName = row.Field<string>("RoleName")
                    }).ToList();
                    return objOutput;
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("CreateBroadcastMessage", ex.StackTrace, ex.Message);
                Message = "Could not create new broadcast message. Exception occured in API.";
            }
            return objOutput;
        }



        public int InActiveBroadcastMessage(CreateBroadcastMessageForm objBroadcastData, out string Message)
        {
            DbParameterCollection sqlparam = new DbParameterCollection();
            try
            {
                sqlparam.Add(new DbParameter("@Id", objBroadcastData.Id, DbType.Int32));
                sqlparam.Add(new DbParameter("@UserId", objBroadcastData.UserId, DbType.Int32));

                var datatable = _dataHelper.ExecuteDataSet("[dbo].[InActiveBroadcastMessage]", sqlparam, CommandType.StoredProcedure);

                Message = Convert.ToString(datatable.Tables[0].Rows[0]["Message"]);
                if (Convert.ToBoolean(datatable.Tables[0].Rows[0]["MessageCode"]))
                    return 1;
            }
            catch(Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("InActiveBroadcastMessage", ex.StackTrace, ex.Message);
                Message = "Error occured, please try again.";
            }
            return 0;
        }

        #endregion Broadcast Message

        #region Role Rights Mapping API
        public int CreateRoleRightsMapping(RoleRightsMappingMaster rolerightsmappingData, out string Message)
        {
            SqlParameter[] objDBParam = new SqlParameter[7];            
            DataTable dtSubModuleIds = null;
            DataRow dr = null;
            int IsDone = 0;
            Message = string.Empty;
            
            try
            {
                #region SubModule
                dtSubModuleIds = new DataTable();
                dtSubModuleIds.Columns.Add("SubModuleId");
                if (rolerightsmappingData.SubModuleIds != null && rolerightsmappingData.SubModuleIds.Count() > 0)
                {
                    foreach (Int64 id in rolerightsmappingData.SubModuleIds)
                    {
                        dr = dtSubModuleIds.NewRow();
                        dr["SubModuleId"] = id;
                        dtSubModuleIds.Rows.Add(dr);
                    }
                }
                #endregion SubModule

                objDBParam[0] = new SqlParameter("@RoleId", rolerightsmappingData.RoleId);
                objDBParam[1] = new SqlParameter("@SubModuleIds", dtSubModuleIds);
                objDBParam[2] = new SqlParameter("@View", rolerightsmappingData.ViewPermission);
                objDBParam[3] = new SqlParameter("@Create", rolerightsmappingData.CreatePermission);
                objDBParam[4] = new SqlParameter("@Edit", rolerightsmappingData.EditPermission);
                objDBParam[5] = new SqlParameter("@Delete", rolerightsmappingData.DeletePermission);
                objDBParam[6] = new SqlParameter("@CreatedBy", rolerightsmappingData.UserId);

                var outputFromSP = _dataHelper.ExecuteProcedure("[dbo].[CreateRoleRightsMapping]", objDBParam);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                IsDone = Convert.ToInt32(outputFromSP.Rows[0]["Status"]);

            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("CreateRoleRightsMapping", ex.StackTrace, ex.Message);
                Message = "Error occured while creating Role Rights Mapping";
            }

            return IsDone;
        }

        public int UpdateRoleRightsMapping(RoleRightsMappingMaster rolerightsmappingData, out string Message)
        {
            int IsDone = 0;
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@Id", rolerightsmappingData.RoleRightsId, DbType.Int64));
                objDBParam.Add(new DbParameter("@View", rolerightsmappingData.ViewPermission));
                objDBParam.Add(new DbParameter("@Create", rolerightsmappingData.CreatePermission));
                objDBParam.Add(new DbParameter("@Edit", rolerightsmappingData.EditPermission));
                objDBParam.Add(new DbParameter("@Delete", rolerightsmappingData.DeletePermission));
                objDBParam.Add(new DbParameter("@UserId", rolerightsmappingData.UserId, DbType.Int64));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[UpdateRoleRightsMapping]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                IsDone = Convert.ToInt32(outputFromSP.Rows[0]["Status"]);
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("UpdateRoleRightsMapping", ex.StackTrace, ex.Message);
                Message = "Error occured while updating Role Rights Mapping";
            }
            return IsDone;
        }
        #endregion Role Rights Mapping API

        #region Feedback & Trainings
        public bool CreateFeedbackForm(CreateFeedbackForm objFormData, out string Message)
        {
            Message = string.Empty;
            SqlParameter[] objDBParam = new SqlParameter[4];
            DataTable dtTitleData = new DataTable();
            DataTable dtSubTitleData = new DataTable();
            DataRow row;
            try
            {
                #region Form Design
                dtTitleData.Columns.Add("Id");
                dtTitleData.Columns.Add("TitleName");
                dtSubTitleData.Columns.Add("TitleId");
                dtSubTitleData.Columns.Add("SubTitleName");
                if (objFormData.Titles != null)
                {
                    int i = 0;
                    foreach (FeedbackTitles title in objFormData.Titles)
                    {
                        row = dtTitleData.NewRow();
                        row[0] = i;
                        row[1] = title.TitleName;
                        dtTitleData.Rows.Add(row);

                        if (title.SubTitles != null)
                        {
                            foreach (string subtitle in title.SubTitles)
                            {
                                row = dtSubTitleData.NewRow();
                                row[0] = i;
                                row[1] = subtitle;
                                dtSubTitleData.Rows.Add(row);
                            }
                        }
                        i++;
                    }
                }
                #endregion Form Design

                objDBParam[0] = new SqlParameter("@FormName", objFormData.FormName);
                objDBParam[1] = new SqlParameter("@Titles", dtTitleData);
                objDBParam[2] = new SqlParameter("@SubTitles", dtSubTitleData);
                objDBParam[3] = new SqlParameter("@UserId", objFormData.UserId);

                var outputFromSP = _dataHelper.ExecuteProcedure("[dbo].[CreateFeedbackForm]", objDBParam);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return true;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("CreateFeedbackForm", ex.StackTrace, ex.Message);
                Message = ex.Message;
            }
            finally
            {
                dtTitleData.Dispose();
                dtSubTitleData.Dispose();
            }
            return false;
        }

        public bool DeleteFeedbackForm(DeleteFeedbackForm objFormData, out string Message)
        {
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@FormId", objFormData.FormId, DbType.Int64));
                objDBParam.Add(new DbParameter("@UserId", objFormData.UserId, DbType.Int64));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[DeleteFeedbackForm]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return true;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("DeleteFeedbackForm", ex.StackTrace, ex.Message);
                Message = "Could not delete feedback form. Exception occured in API.";
            }
            return false;
        }

        public CreateFeedbackForm ViewFeedbackFormDesign(ViewFeedbackForm objFormData, out string Message)
        {
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@FormId", objFormData.FormId, DbType.Int64));
                objDBParam.Add(new DbParameter("@UserId", objFormData.UserId, DbType.Int64));
                var outputFromSP = _dataHelper.ExecuteDataSet("[dbo].[ViewFeedbackFormDesign]", objDBParam, CommandType.StoredProcedure);
                if (outputFromSP == null)
                {
                    Message = "Could not fetch feedback form. Exception occured in database.";
                    return null;
                }
                else if(outputFromSP.Tables == null)
                {
                    Message = "Form design not found in database. Please contact administrator.";
                    return null;
                }
                else if (outputFromSP.Tables.Count == 0)
                {
                    Message = "Form design not found in database. Please contact administrator.";
                    return null;
                }
                else if (outputFromSP.Tables.Count == 3)
                {
                    CreateFeedbackForm objFeedbackForm = new CreateFeedbackForm();
                    objFeedbackForm.FormName = Convert.ToString(outputFromSP.Tables[0].Rows[0]["FormName"]);
                    objFeedbackForm.Titles = (from title in outputFromSP.Tables[1].AsEnumerable()
                                              select new FeedbackTitles {
                                                  TitleId = Convert.ToInt64(title.Field<Int64>("TitleId")),
                                                  TitleName = Convert.ToString(title.Field<string>("TitleName")),
                                              }).ToList();
                    foreach(FeedbackTitles objTitle in objFeedbackForm.Titles)
                    {
                        objTitle.SubTitles = (from subtitle in outputFromSP.Tables[2].AsEnumerable() where 
                                             Convert.ToInt64(subtitle.Field<Int64>("TitleId")) == objTitle.TitleId
                                             select subtitle.Field<string>("SubTitleName")).ToList();
                    }
                    Message = "Feedback form design fetched successfully.";
                    return objFeedbackForm;
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("ViewFeedbackFormDesign", ex.StackTrace, ex.Message);
                Message = "Could not fetch feedback form. Exception occured in API.";
            }
            return null;
        }

        public bool CreateTrainingForm(CreateTrainingForm objFormData, out string Message)
        {
            Message = string.Empty;
            DataTable dtbranches = new DataTable();
            DataTable dtProductCat = new DataTable();
            DataTable dtChanelId = new DataTable();

            DataRow row;
            try
            {

                dtbranches.Columns.Add("FilterId");
                if (objFormData.BranchIds != null)
                {
                    foreach (Int64 branchid in objFormData.BranchIds)
                    {
                        row = dtbranches.NewRow();
                        row["FilterId"] = branchid;
                        dtbranches.Rows.Add(row);
                    }
                }

                dtProductCat.Columns.Add("FilterId");
                if (objFormData.ProdCatId != null)
                {
                    foreach (Int64 ProdCat in objFormData.ProdCatId)
                    {
                        row = dtProductCat.NewRow();
                        row["FilterId"] = ProdCat;
                        dtProductCat.Rows.Add(row);
                    }
                }

                dtChanelId.Columns.Add("FilterId");
                if (objFormData.ChannelId != null)
                {
                    foreach (Int64 Channel in objFormData.ChannelId)
                    {
                        row = dtChanelId.NewRow();
                        row["FilterId"] = Channel;
                        dtChanelId.Rows.Add(row);
                    }
                }

                SqlParameter[] objDBParam = new SqlParameter[9];
                objDBParam[0] = new SqlParameter("@CourseName", objFormData.CourseName);
                objDBParam[1] = new SqlParameter("@TrainerName", objFormData.TrainerName);
                objDBParam[2] = new SqlParameter("@FromDate", DateTime.ParseExact(objFormData.FromDate, "dd/MM/yyyy", CultureInfo.InvariantCulture));
                objDBParam[3] = new SqlParameter("@ToDate", DateTime.ParseExact(objFormData.ToDate, "dd/MM/yyyy", CultureInfo.InvariantCulture));
                objDBParam[4] = new SqlParameter("@ProdCatId", dtProductCat);
                objDBParam[5] = new SqlParameter("@ChannelId", dtChanelId);
                objDBParam[6] = new SqlParameter("@FormId", objFormData.FormId);
                objDBParam[7] = new SqlParameter("@CreatedBy", objFormData.UserId);
                objDBParam[8] = new SqlParameter("@BranchIds", dtbranches);


                var outputFromSP = _dataHelper.ExecuteProcedure("[dbo].[CreateTrainingForm]", objDBParam);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return true;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("CreateTrainingForm", ex.StackTrace, ex.Message);
                Message = ex.Message;
            }
            return false;
        }


        public int InActiveTrainingMessage(CreateTrainingForm objFormData, out string Message)
        {
            DbParameterCollection sqlparam = new DbParameterCollection();
            try
            {
                sqlparam.Add(new DbParameter("@Id", objFormData.Id, DbType.Int32));
                sqlparam.Add(new DbParameter("@UserId", objFormData.UserId, DbType.Int32));

                var datatable = _dataHelper.ExecuteDataSet("[dbo].[InActiveTrainingMessage]", sqlparam, CommandType.StoredProcedure);

                Message = Convert.ToString(datatable.Tables[0].Rows[0]["Message"]);
                if (Convert.ToBoolean(datatable.Tables[0].Rows[0]["MessageCode"]))
                    return 1;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("InActiveTrainingMessage", ex.StackTrace, ex.Message);
                Message = "Error occured, please try again.";
            }
            return 0;
        }


        #endregion Feedback & Trainings


        #region Shift Master API
        public int CreateShiftTiming(ShiftMaster shift, out string Message)
        {
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@ShiftName", shift.ShiftName, DbType.String));
                objDBParam.Add(new DbParameter("@CreatedBy", shift.UserId, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[CreateShiftTiming]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return 1;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("CreateShiftTiming", ex.StackTrace, ex.Message);
                Message = ex.Message;
            }
            return 0;
        }

        public int UpdateShiftTiming(ShiftMaster shift, out string Message)
        {
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@ShiftNameId", shift.Id, DbType.Int32));
                objDBParam.Add(new DbParameter("@ShiftName", shift.ShiftName, DbType.String));
                objDBParam.Add(new DbParameter("@UserID", shift.UserId, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[UpdateShiftTiming]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return 1;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("UpdateShiftTiming", ex.StackTrace, ex.Message);
                Message = ex.Message;
            }
            return 0;
        }

        public int DeleteShift(ShiftMaster shift, out string Message)
        {
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@ShiftNameId", shift.Id, DbType.Int32));
                objDBParam.Add(new DbParameter("@UserID", shift.UserId, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[DeleteShift]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return 1;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("DeleteShift", ex.StackTrace, ex.Message);
                Message = "Could not delete material. Exception occured in API.";
            }
            return 0;
        }

        public IEnumerable<ShiftMaster> GetShiftTiming()
        {
            IEnumerable<ShiftMaster> listShift = null;
            try
            {
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetShiftTiming]", CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    listShift = (from shift in outputFromSP.AsEnumerable()
                                    select new ShiftMaster
                                    {
                                        Id = Convert.ToInt64(shift.Field<Int64>("ID")),
                                        ShiftName = Convert.ToString(shift.Field<string>("ShiftName")),
                                        IsActive = Convert.ToBoolean(shift.Field<bool>("ISACTIVE")),
                                        CreatedBy = Convert.ToInt64(shift.Field<Int64>("CREATEDBY")),
                                        CreationDate = Convert.ToDateTime(shift.Field<DateTime>("CREATIONDATE")),
                                        LastModifiedBy = Convert.ToInt64(shift.Field<Int64?>("LASTMODIFIEDBY")),
                                        LastModificationDate = Convert.ToDateTime(shift.Field<DateTime?>("LASTMODDATE"))
                                    });
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetShiftTiming", ex.StackTrace, ex.Message);
            }
            return listShift;
        }

        public ShiftMaster GetShiftTimingById(Int64 shiftNameId)
        {
            DbParameterCollection objDBParam = new DbParameterCollection();
            ShiftMaster outputShift = null;
            try
            {
                objDBParam.Add(new DbParameter("@ShiftNameId", shiftNameId, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetShiftTiming]", objDBParam, CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    outputShift = (from shift in outputFromSP.AsEnumerable()
                                     select new ShiftMaster
                                     {
                                         Id = Convert.ToInt64(shift.Field<Int64>("ID")),
                                         ShiftName = Convert.ToString(shift.Field<string>("ShiftName")),
                                         IsActive = Convert.ToBoolean(shift.Field<bool>("ISACTIVE")),
                                         CreatedBy = Convert.ToInt64(shift.Field<Int64>("CREATEDBY")),
                                         CreationDate = Convert.ToDateTime(shift.Field<DateTime>("CREATIONDATE")),
                                         LastModifiedBy = Convert.ToInt64(shift.Field<Int64?>("LASTMODIFIEDBY")),
                                         LastModificationDate = Convert.ToDateTime(shift.Field<DateTime?>("LASTMODDATE"))
                                     }).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetShiftTimingById", ex.StackTrace, ex.Message);
            }
            return outputShift;
        }
        #endregion Shift Master API

        public int UpdateMonthlyReport(UpdateMonthlyReport objFormData, out string Message)
        {
            DbParameterCollection sqlparam = new DbParameterCollection();
            try
            {
                
                sqlparam.Add(new DbParameter("@SFAId", objFormData.SFAId, DbType.Int64));
                sqlparam.Add(new DbParameter("@SFAAttendanceDate", objFormData.AttendanceDate, DbType.String));
                sqlparam.Add(new DbParameter("@OLDAttendanceType", objFormData.OLD_ATT_TYPE, DbType.String));
                sqlparam.Add(new DbParameter("@Remarks", objFormData.Remarks, DbType.String));
                sqlparam.Add(new DbParameter("@UserId", objFormData.UserId, DbType.Int32));
                sqlparam.Add(new DbParameter("@RoleName", objFormData.RoleName, DbType.String));
                //sqlparam.Add(new DbParameter("@IsApproved", objFormData.IsApproved));
                sqlparam.Add(new DbParameter("@NewAttendanceType", objFormData.NewAttendanceTypeId, DbType.Int64));
                sqlparam.Add(new DbParameter("@DealerCode", objFormData.DealerCode, DbType.Int64)); //nikita 30/12/2024

                var datatable = _dataHelper.ExecuteDataSet("[dbo].[UpdateMonthlyReport]", sqlparam, CommandType.StoredProcedure);

                Message = Convert.ToString(datatable.Tables[0].Rows[0]["Message"]);
                if (Convert.ToBoolean(datatable.Tables[0].Rows[0]["MessageCode"]))
                    return 1;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("InActiveTrainingMessage", ex.StackTrace, ex.Message);
                Message = "Error occured, please try again.";
            }
            return 0;
        }

        public void Dispose()
        {
            Dispose();
            GC.SuppressFinalize(this);
        }

        
    }
}
