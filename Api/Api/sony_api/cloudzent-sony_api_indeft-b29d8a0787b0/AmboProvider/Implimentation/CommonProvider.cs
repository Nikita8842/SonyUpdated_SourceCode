using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using AmboLibrary.UserManagement;
using AmboLibrary.Modules;
using AmboLibrary.MasterMaintenance;
using AmboLibrary.GlobalAccessible;
using AmboProvider.Interface;
using DBHelper;
using System.Data.SqlClient;
using AmboLibrary.Mappings;
using AmboLibrary.IncentiveManagement;

namespace AmboProvider.Implimentation
{
    /// <summary>
    /// Fetching data by Id only 
    /// Kritika chadha
    /// </summary>
    public class CommonProvider : ICommonProvider
    {
        private readonly DbHelper _dataHelper;
        private readonly IErrorLogProvider _IErrorLogProvider;

        public CommonProvider(IErrorLogProvider IErrorLogProvider)
        {
            _dataHelper = DbHelper.GetInstance("defaultConnection");
            _IErrorLogProvider = IErrorLogProvider;
        }

        #region Get Location data by Id's API for dropdown

        public  IEnumerable<StateMaster> GetStatesByRegion(Common InputParam)
        {
            DbParameterCollection objDBParam = new DbParameterCollection();
            IEnumerable<StateMaster> ListofStates = null;
            try
            {
                objDBParam.Add(new DbParameter("@RegionId", InputParam.Id, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetStatesByRegion]", objDBParam, CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    ListofStates = (from states in outputFromSP.AsEnumerable()
                                   select new StateMaster
                                   {
                                       ID = states.Field<Int64>("StateId"),
                                       StateName = states.Field<string>("StateName")
                                   });
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetStatesByRegion", ex.StackTrace, ex.Message);
            }
            return ListofStates;


        }


        public IEnumerable<CityMaster> GetCityByState(Common InputParam)
        {
            DbParameterCollection objDBParam = new DbParameterCollection();
            IEnumerable<CityMaster> ListofCity = null;
            try
            {
                objDBParam.Add(new DbParameter("@StateId", InputParam.Id, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetCityByState]", objDBParam, CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    ListofCity = (from cities in outputFromSP.AsEnumerable()
                                    select new CityMaster
                                    {
                                        ID = cities.Field<Int64>("CityId"),
                                        CityName = cities.Field<string>("CityName")
                                    });
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetCityByState", ex.StackTrace, ex.Message);
            }
            return ListofCity;


        }

        public IEnumerable<CityData> GetAllCities()
        {
            IEnumerable<CityData> listCities = null;
            try
            {
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetAllCities]", CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    listCities = (from city in outputFromSP.AsEnumerable()
                                  select new CityData
                                  {
                                      CityId = Convert.ToInt64(city.Field<Int64>("ID")),
                                      CityName = Convert.ToString(city.Field<string>("CITYNAME"))
                                  });
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetAllCities", ex.StackTrace, ex.Message);
            }
            return listCities;
        }

        public IEnumerable<LocationData> GetAllLocations()
        {
            IEnumerable<LocationData> listLocs = null;
            try
            {
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetAllLocations]", CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    listLocs = (from loc in outputFromSP.AsEnumerable()
                                  select new LocationData
                                  {
                                      ID = Convert.ToInt64(loc.Field<Int64>("ID")),
                                      LocationName = Convert.ToString(loc.Field<string>("LOCATIONNAME"))
                                  });
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetAllLocations", ex.StackTrace, ex.Message);
            }
            return listLocs;
        }

        public IEnumerable<LocationMaster> GetLocationByCity(Common InputParam)
        {
            DbParameterCollection objDBParam = new DbParameterCollection();
            IEnumerable<LocationMaster> ListofLocation = null;
            try
            {
                objDBParam.Add(new DbParameter("@CityId", InputParam.Id, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetLocationByCity]", objDBParam, CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    ListofLocation = (from location in outputFromSP.AsEnumerable()
                                      select new LocationMaster
                                      {
                                          ID = location.Field<Int64>("LocationId"),
                                          LocationName = location.Field<string>("LocationName")
                                      });
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetLocationByCity", ex.StackTrace, ex.Message);
            }
            return ListofLocation;


        }

        public IEnumerable<DealerMaster> GetDealersByLocation(Common InputParam)
        {
            DbParameterCollection objDBParam = new DbParameterCollection();
            IEnumerable<DealerMaster> ListOfDealers = null;
            try
            {
                objDBParam.Add(new DbParameter("@LocationId", InputParam.Id, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetDealersByLocation]", objDBParam, CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    ListOfDealers = (from dealer in outputFromSP.AsEnumerable()
                                      select new DealerMaster
                                      {
                                          ID = dealer.Field<Int64>("DealerId"),
                                          DealerName = dealer.Field<string>("DealerName")
                                      });
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetDealersByLocation", ex.StackTrace, ex.Message);
            }
            return ListOfDealers;
        }

        public IEnumerable<DealerMaster> GetDealersByLocationNonSFA(Common InputParam)
        {
            DbParameterCollection objDBParam = new DbParameterCollection();
            IEnumerable<DealerMaster> ListOfDealers = null;
            try
            {
                objDBParam.Add(new DbParameter("@LocationId", InputParam.Id, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetDealersByLocationNonSFA]", objDBParam, CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    ListOfDealers = (from dealer in outputFromSP.AsEnumerable()
                                     select new DealerMaster
                                     {
                                         ID = dealer.Field<Int64>("DealerId"),
                                         DealerName = dealer.Field<string>("DealerName")
                                     });
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetDealersByLocationNonSFA", ex.StackTrace, ex.Message);
            }
            return ListOfDealers;
        }


        public IEnumerable<DealerMaster> GetDealersByBranch(Common InputParam)
        {
            DbParameterCollection objDBParam = new DbParameterCollection();
            IEnumerable<DealerMaster> ListOfDealers = null;
            try
            {
                objDBParam.Add(new DbParameter("@BranchId", InputParam.Id, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetDealersByBranch]", objDBParam, CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    ListOfDealers = (from dealer in outputFromSP.AsEnumerable()
                                     select new DealerMaster
                                     {
                                         ID = dealer.Field<Int64>("DealerId"),
                                         DealerName = dealer.Field<string>("DealerName")
                                     });
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetDealersByBranch", ex.StackTrace, ex.Message);
            }
            return ListOfDealers;
        }

        public IEnumerable<SearchSFAOutput> GetAllActiveSFA()
        {
            IEnumerable<SearchSFAOutput> ListofSFA = null;
            try
            {
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetAllActiveSFA]", CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    ListofSFA = (from sfa in outputFromSP.AsEnumerable()
                                 select new SearchSFAOutput
                                 {
                                     SFAUserId = sfa.Field<Int64>("SFAID"),
                                     SFAName = sfa.Field<string>("SFANAME")
                                 }).ToList();
                }
                else
                    ListofSFA = new List<SearchSFAOutput>();
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetAllActiveSFA", ex.StackTrace, ex.Message);
            }
            return ListofSFA;
        }

        public IEnumerable<DealerList> GetAllActiveDealers()
        {
            IEnumerable<DealerList> ListofDealers = null;
            try
            {
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetAllActiveDealers]", CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    ListofDealers = (from dealer in outputFromSP.AsEnumerable()
                                 select new DealerList
                                 {
                                     DealerId = dealer.Field<Int64>("DealerId"),
                                     DealerName = dealer.Field<string>("DealerName")
                                 }).ToList();
                }
                else
                    ListofDealers = new List<DealerList>();
            }
            catch (Exception ex)
            {
                 _IErrorLogProvider.CreateErrorLog("GetAllActiveDealers", ex.StackTrace, ex.Message);
            }
            return ListofDealers;
        }

        public IEnumerable<DealerList> GetAllActiveDealersNonSFA()
        {
            IEnumerable<DealerList> ListofDealers = null;
            try
            {
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetAllActiveDealersNonSFA]", CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    ListofDealers = (from dealer in outputFromSP.AsEnumerable()
                                     select new DealerList
                                     {
                                         DealerId = dealer.Field<Int64>("DealerId"),
                                         DealerName = dealer.Field<string>("DealerName")
                                     }).ToList();
                }
                else
                    ListofDealers = new List<DealerList>();
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetAllActiveDealers", ex.StackTrace, ex.Message);
            }
            return ListofDealers;
        }
        
        public IEnumerable<BrandList> GetActiveBrands()
        {
            var getList = new List<BrandList>();
            try
            {
                var datatable = _dataHelper.ExecuteDataTable("[dbo].[GetActiveBrands]", CommandType.StoredProcedure);

                if (datatable.Rows.Count > 0)
                {
                    getList = (from item in datatable.AsEnumerable()
                               select new BrandList
                               {
                                   BrandId = Convert.ToInt64(item.Field<Int64>("BrandId")),
                                   BrandName = Convert.ToString(item.Field<string>("BrandName"))
                               }).ToList();
                    return getList;
                }
            }
            catch(Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetActiveBrands", ex.StackTrace, ex.Message);
            }
            return getList;
        }

        public IEnumerable<GetFeedbackForm> GetActiveFeedbackForms()
        {
            var getList = new List<GetFeedbackForm>();
            try
            {
                var datatable = _dataHelper.ExecuteDataTable("[dbo].[GetActiveFeedbackForms]", CommandType.StoredProcedure);

                if (datatable.Rows.Count > 0)
                {
                    getList = (from item in datatable.AsEnumerable()
                               select new GetFeedbackForm
                               {
                                   FormId = Convert.ToInt64(item.Field<Int64>("FormId")),
                                   FormName = Convert.ToString(item.Field<string>("FormName"))
                               }).ToList();
                    return getList;
                }
            }
            catch(Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetActiveFeedbackForms", ex.StackTrace, ex.Message);
            }
            return getList;
        }

        public IEnumerable<DealerMasterCode> GetNonSFADealerMasterCodesByBranch(Common InputParam)
        {
            DbParameterCollection objDBParam = new DbParameterCollection();
            IEnumerable<DealerMasterCode> ListOfDealers = null;
            try
            {
                objDBParam.Add(new DbParameter("@BranchId", InputParam.Id, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetNonSFADealerMasterCodesByBranch]", objDBParam, CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    ListOfDealers = (from dealer in outputFromSP.AsEnumerable()
                                     select new DealerMasterCode
                                     {
                                         MasterCode = dealer.Field<string>("MasterCode")
                                     });
                }
                else
                    ListOfDealers = new List<DealerMasterCode>();
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetNonSFADealerMasterCodesByBranch", ex.StackTrace, ex.Message);
            }
            return ListOfDealers;
        }

        public IEnumerable<NonSFADealer> GetNonSFADealersByMasterCodes(NonSFAMasterCodeList objInput, out string Message)
        {
            Message = string.Empty;
            SqlParameter[] objDBParam = new SqlParameter[1];
            DataTable dt = new DataTable();
            List<NonSFADealer> objOutput;
            DataRow row;
            try
            {
                dt.Columns.Add("MasterCode");
                if (objInput.MasterCodes != null)
                {
                    foreach (DealerMasterCode mastercode in objInput.MasterCodes)
                    {
                        row = dt.NewRow();
                        row[0] = mastercode.MasterCode;
                        dt.Rows.Add(row);
                    }
                }
                objDBParam[0] = new SqlParameter("@NonSFAMasterCodeList", dt);
                var outputFromSP = _dataHelper.ExecuteProcedure("[dbo].[GetNonSFADealersByMasterCodes]", objDBParam);
                if (!(outputFromSP == null || outputFromSP.Rows.Count == 0))
                {
                    objOutput = (from dealer in outputFromSP.AsEnumerable()
                                 select new NonSFADealer
                                 {
                                     DealerId = Convert.ToInt64(dealer.Field<Int64>("DealerId")),
                                     DealerName = Convert.ToString(dealer.Field<string>("DealerName"))
                                 }).ToList();
                    Message = "All Non-SFA dealers fetched successfully using selected master codes.";
                }
                else
                {
                    objOutput = new List<NonSFADealer>();
                    Message = "No Non-SFA dealers found for selected master codes.";
                }
            }
            catch(Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetNonSFADealersByMasterCodes", ex.StackTrace, ex.Message);
                Message = ex.Message;
                objOutput = new List<NonSFADealer>();
            }
            finally
            {
                dt.Dispose();
                row = null;
            }
            return objOutput;
        }

        public IEnumerable<SFAFormData> GetSFAByDealer(Common InputParam)
        {
            DbParameterCollection objDBParam = new DbParameterCollection();
            IEnumerable<SFAFormData> ListofSFA = null;
            try
            {
                objDBParam.Add(new DbParameter("@DealerId", InputParam.Id, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetSFAByDealer]", objDBParam, CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    ListofSFA = (from sfa in outputFromSP.AsEnumerable()
                                 select new SFAFormData
                                 {
                                     EmployeeId = sfa.Field<Int64>("ID"),
                                     SFAFullName = sfa.Field<string>("SFANAME")
                                 });
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetSFAByDealer", ex.StackTrace, ex.Message);
            }
            return ListofSFA;
        }

        public IEnumerable<SFADropdown> GetSFADropdown()
        {
           
            IEnumerable<SFADropdown> ListofSFA = null;
            try
            {
                
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetSFADropdown]", CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    ListofSFA = (from sfa in outputFromSP.AsEnumerable()
                                 select new SFADropdown
                                 {
                                     Id = sfa.Field<Int64>("ID"),
                                     SFAName = sfa.Field<string>("SFANAME")
                                 });
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetSFADropdown", ex.StackTrace, ex.Message);
            }
            return ListofSFA;
        }

        public IEnumerable<SFAFormData> GetAllActiveRDI()
        {
            IEnumerable<SFAFormData> ListofRDI = null;
            try
            {
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetAllActiveRDI]", CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    ListofRDI = (from rdi in outputFromSP.AsEnumerable()
                                 select new SFAFormData
                                 {
                                     EmployeeId = rdi.Field<Int64>("ID"),
                                     RDIFullName = rdi.Field<string>("RDINAME")
                                 });
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetAllActiveRDI", ex.StackTrace, ex.Message);
            }
            return ListofRDI;
        }

        public IEnumerable<SFALevelData> GetActiveSFALevels()
        {
            IEnumerable<SFALevelData> ListofSFALevels = null;
            try
            {
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetActiveSFALevels]", CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    ListofSFALevels = (from level in outputFromSP.AsEnumerable()
                                 select new SFALevelData
                                 {
                                     SFALevelId = level.Field<Int64>("SFALEVELID"),
                                     SFALevelName = level.Field<string>("SFALEVELNAME")
                                 });
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetActiveSFALevels", ex.StackTrace, ex.Message);
            }
            return ListofSFALevels;
        }

        public IEnumerable<SFAFormData> GetSFAByBranch(Common InputParam)
        {
            DbParameterCollection objDBParam = new DbParameterCollection();
            IEnumerable<SFAFormData> ListofSFA = null;
            try
            {
                objDBParam.Add(new DbParameter("@BranchId", InputParam.Id, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetSFAByBranch]", objDBParam, CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    ListofSFA = (from sfa in outputFromSP.AsEnumerable()
                                 select new SFAFormData
                                 {
                                     EmployeeId = sfa.Field<Int64>("ID"),
                                     SFAFullName = sfa.Field<string>("SFANAME")
                                 });
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetSFAByBranch", ex.StackTrace, ex.Message);
            }
            return ListofSFA;
        }

        public IEnumerable<SearchSFAOutput> GetSFAFromDivisionAndBranch(SearchSFA InputParam)
        {
            SqlParameter[] objDBParam = new SqlParameter[3];
            List<SearchSFAOutput> ListofSFA = new List<SearchSFAOutput>();
            DataTable dtBranchIds = null;
            DataTable dtDivisionIds = null;
            DataRow dr = null;
            try
            {
                #region Division
                dtDivisionIds = new DataTable();
                dtDivisionIds.Columns.Add("DivisionId");
                if (InputParam.DivisionIds != null && InputParam.DivisionIds.Count() > 0)
                {
                    foreach (Int64 id in InputParam.DivisionIds)
                    {
                        dr = dtDivisionIds.NewRow();
                        dr["DivisionId"] = id;
                        dtDivisionIds.Rows.Add(dr);
                    }
                }
                #endregion Division

                #region Branch
                dtBranchIds = new DataTable();
                dtBranchIds.Columns.Add("BranchId");
                if (InputParam.BranchIds != null && InputParam.BranchIds.Count() > 0)
                {
                    foreach (Int64 id in InputParam.BranchIds)
                    {
                        dr = dtBranchIds.NewRow();
                        dr["BranchId"] = id;
                        dtBranchIds.Rows.Add(dr);
                    }
                }
                #endregion Branch

                objDBParam[0] = new SqlParameter("@Divisions", dtDivisionIds);
                objDBParam[1] = new SqlParameter("@Branches", dtBranchIds);
                objDBParam[2] = new SqlParameter("@UserId", InputParam.UserId);
                var outputFromSP = _dataHelper.ExecuteProcedure("[dbo].[GetSFAFromDivisionAndBranch]", objDBParam);
                if (outputFromSP.Rows.Count > 0)
                {
                    ListofSFA = (from sfa in outputFromSP.AsEnumerable()
                                 select new SearchSFAOutput
                                 {
                                     SFAUserId = sfa.Field<Int64>("SFAUSERID"),
                                     SFAName = sfa.Field<string>("SFANAME")
                                 }).ToList();
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetSFAFromDivisionAndBranch", ex.StackTrace, ex.Message);
            }
            finally
            {
                dtDivisionIds.Dispose();
                dtBranchIds.Dispose();
                dr = null;
            }
            return ListofSFA;
        }







        public IEnumerable<SearchSFAOutput> GetSFAFromDivisionAndBranchAndRole(SearchSFA InputParam)
        {
            SqlParameter[] objDBParam = new SqlParameter[4];
            List<SearchSFAOutput> ListofSFA = new List<SearchSFAOutput>();
            DataTable dtBranchIds = null;
            DataTable dtDivisionIds = null;
            DataTable dtRoleIds = null;
            DataRow dr = null;
            try
            {
                #region Division
                dtDivisionIds = new DataTable();
                dtDivisionIds.Columns.Add("DivisionId");
                if (InputParam.DivisionIds != null && InputParam.DivisionIds.Count() > 0)
                {
                    foreach (Int64 id in InputParam.DivisionIds)
                    {
                        dr = dtDivisionIds.NewRow();
                        dr["DivisionId"] = id;
                        dtDivisionIds.Rows.Add(dr);
                    }
                }
                #endregion Division

                #region Branch
                dtBranchIds = new DataTable();
                dtBranchIds.Columns.Add("BranchId");
                if (InputParam.BranchIds != null && InputParam.BranchIds.Count() > 0)
                {
                    foreach (Int64 id in InputParam.BranchIds)
                    {
                        dr = dtBranchIds.NewRow();
                        dr["BranchId"] = id;
                        dtBranchIds.Rows.Add(dr);
                    }
                }
                #endregion Branch


                #region Role
                dtRoleIds = new DataTable();
                dtRoleIds.Columns.Add("FilterId");
                if (InputParam.RoleIds != null && InputParam.RoleIds.Count() > 0)
                {
                    foreach (Int64 id in InputParam.RoleIds)
                    {
                        dr = dtRoleIds.NewRow();
                        dr["FilterId"] = id;
                        dtRoleIds.Rows.Add(dr);
                    }
                }
                #endregion 

                objDBParam[0] = new SqlParameter("@Divisions", dtDivisionIds);
                objDBParam[1] = new SqlParameter("@Branches", dtBranchIds);
                objDBParam[2] = new SqlParameter("@UserId", InputParam.UserId);
                 objDBParam[3] = new SqlParameter("@RoleIds", dtRoleIds);
                var outputFromSP = _dataHelper.ExecuteProcedure("[dbo].[GetSFAFromDivisionAndBranchAndRole]", objDBParam);
                if (outputFromSP.Rows.Count > 0)
                {
                    ListofSFA = (from sfa in outputFromSP.AsEnumerable()
                                 select new SearchSFAOutput
                                 {
                                     SFAUserId = sfa.Field<Int64>("SFAUSERID"),
                                     SFAName = sfa.Field<string>("SFANAME")
                                 }).ToList();
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetSFAFromDivisionAndBranchAndRole", ex.StackTrace, ex.Message);
            }
            finally
            {
                dtDivisionIds.Dispose();
                dtBranchIds.Dispose();
                dr = null;
            }
            return ListofSFA;
        }

        public IEnumerable<SearchSIDOutput> GetSIDFromBranchForBroadcast(SearchSID InputParam)
        {
            SqlParameter[] objDBParam = new SqlParameter[2];
            List<SearchSIDOutput> ListofSID = new List<SearchSIDOutput>();
            DataTable dtBranchIds = null;
            DataRow dr = null;
            try
            {
                #region Branch
                dtBranchIds = new DataTable();
                dtBranchIds.Columns.Add("BranchId");
                if (InputParam.BranchIds != null && InputParam.BranchIds.Count() > 0)
                {
                    foreach (Int64 id in InputParam.BranchIds)
                    {
                        dr = dtBranchIds.NewRow();
                        dr["BranchId"] = id;
                        dtBranchIds.Rows.Add(dr);
                    }
                }
                #endregion Branch

                objDBParam[0] = new SqlParameter("@Branches", dtBranchIds);
                objDBParam[1] = new SqlParameter("@UserId", InputParam.UserId);
                var outputFromSP = _dataHelper.ExecuteProcedure("[dbo].[GetSIDFromBranchForBroadcast]", objDBParam);
                if (outputFromSP.Rows.Count > 0)
                {
                    ListofSID = (from sid in outputFromSP.AsEnumerable()
                                 select new SearchSIDOutput
                                 {
                                     SIDUserId = sid.Field<Int64>("SIDUSERID"),
                                     SIDName = sid.Field<string>("SIDNAME")
                                 }).ToList();
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetSIDFromBranchForBroadcast", ex.StackTrace, ex.Message);
            }
            finally
            {
                dtBranchIds.Dispose();
                dr = null;
            }
            return ListofSID;
        }





        public IEnumerable<SearchSIDOutput> GetSIDFromBranchAndRoleForBroadcast(SearchSID InputParam)
        {
            SqlParameter[] objDBParam = new SqlParameter[3];
            List<SearchSIDOutput> ListofSID = new List<SearchSIDOutput>();
            DataTable dtBranchIds = null;
            DataTable dtRoleIds = null;
            DataRow dr = null;
            try
            {
                #region Branch
                dtBranchIds = new DataTable();
                dtBranchIds.Columns.Add("BranchId");
                if (InputParam.BranchIds != null && InputParam.BranchIds.Count() > 0)
                {
                    foreach (Int64 id in InputParam.BranchIds)
                    {
                        dr = dtBranchIds.NewRow();
                        dr["BranchId"] = id;
                        dtBranchIds.Rows.Add(dr);
                    }
                }
                #endregion Branch


                #region Role
                dtRoleIds = new DataTable();
                dtRoleIds.Columns.Add("FilterId");
                if (InputParam.RoleIds != null && InputParam.RoleIds.Count() > 0)
                {
                    foreach (Int64 id in InputParam.RoleIds)
                    {
                        dr = dtRoleIds.NewRow();
                        dr["FilterId"] = id;
                        dtRoleIds.Rows.Add(dr);
                    }
                }
                #endregion 


                objDBParam[0] = new SqlParameter("@Branches", dtBranchIds);
                objDBParam[1] = new SqlParameter("@UserId", InputParam.UserId);
                objDBParam[2] = new SqlParameter("@RoleIds", dtRoleIds);

                var outputFromSP = _dataHelper.ExecuteProcedure("[dbo].[GetSIDFromBranchAndRoleForBroadcast]", objDBParam);
                if (outputFromSP.Rows.Count > 0)
                {
                    ListofSID = (from sid in outputFromSP.AsEnumerable()
                                 select new SearchSIDOutput
                                 {
                                     SIDUserId = sid.Field<Int64>("SIDUSERID"),
                                     SIDName = sid.Field<string>("SIDNAME")
                                 }).ToList();
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetSIDFromBranchForBroadcast", ex.StackTrace, ex.Message);
            }
            finally
            {
                dtBranchIds.Dispose();
                dr = null;
            }
            return ListofSID;
        }

        public IEnumerable<EmployeeFormData> GetAllActiveUsersByRole(Common InputParam)
        {
            DbParameterCollection objDBParam = new DbParameterCollection();
            IEnumerable<EmployeeFormData> ListofUsers = null;
            try
            {
                objDBParam.Add(new DbParameter("@RoleId", InputParam.Id, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetAllActiveUsersByRole]", objDBParam, CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    ListofUsers = (from sfa in outputFromSP.AsEnumerable()
                                 select new EmployeeFormData
                                 {
                                     EmployeeId = sfa.Field<Int64>("ID"),
                                     UserFullName = sfa.Field<string>("USERFULLNAME")
                                 });
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetAllActiveUsersByRole", ex.StackTrace, ex.Message);
            }
            return ListofUsers;
        }

        public IEnumerable<SalesPIC> GetSalesPICByBranch(Common InputParam)
        {
            DbParameterCollection objDBParam = new DbParameterCollection();
            IEnumerable<SalesPIC> ListofUsers = null;
            try
            {
                objDBParam.Add(new DbParameter("@BranchId", InputParam.Id, DbType.Int64));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetSalesPICByBranch]", objDBParam, CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    ListofUsers = (from salespic in outputFromSP.AsEnumerable()
                                   select new SalesPIC
                                   {
                                       Id = salespic.Field<Int64>("ID"),
                                       Name = salespic.Field<string>("NAME")
                                   });
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetSalesPICByBranch", ex.StackTrace, ex.Message);
            }
            return ListofUsers;
        }

        public IEnumerable<SFAFormData> GetUnmappedSFAByBranch(Common InputParam)
        {
            DbParameterCollection objDBParam = new DbParameterCollection();
            IEnumerable<SFAFormData> ListofSFA = null;
            try
            {
                objDBParam.Add(new DbParameter("@BranchId", InputParam.Id, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetUnmappedSFAByBranch]", objDBParam, CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    ListofSFA = (from sfa in outputFromSP.AsEnumerable()
                                     select new SFAFormData
                                     {
                                         EmployeeId = sfa.Field<Int64>("ID"),
                                         SFAFullName = sfa.Field<string>("SFANAME")
                                     });
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetUnmappedSFAByBranch", ex.StackTrace, ex.Message);
            }
            return ListofSFA;
        }

        public IEnumerable<RegionMaster> GetRegion()
        {
            IEnumerable<RegionMaster> ListofRegion = null;
            try
            {
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetRegion]", CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    ListofRegion = (from region in outputFromSP.AsEnumerable()
                                      select new RegionMaster
                                      {
                                          ID = region.Field<Int64>("RegionId"),
                                          RegionName = region.Field<string>("RegionName")
                                      });
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetRegion", ex.StackTrace, ex.Message);
            }
            return ListofRegion;

        }

        public IEnumerable<ChannelMaster> GetChannels()
        {
            IEnumerable<ChannelMaster> ListofChannels = null;
            try
            {
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetChannel]", CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    ListofChannels = (from channel in outputFromSP.AsEnumerable()
                                    select new ChannelMaster
                                    {
                                        Id = channel.Field<Int64>("ID"),
                                        ChannelName = channel.Field<string>("CHANNELNAME"),
                                        ChannelCode = channel.Field<string>("CHANNELCODE")
                                    });
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetChannels", ex.StackTrace, ex.Message);
            }
            return ListofChannels;

        }

        public IEnumerable<StateMaster> GetStates()
        {
            IEnumerable<StateMaster> ListofStates = null;
            try
            {
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetState]", CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    ListofStates = (from state in outputFromSP.AsEnumerable()
                                    select new StateMaster
                                    {
                                        ID = Convert.ToInt64(state.Field<Int64>("ID")),
                                        StateName = Convert.ToString(state.Field<string>("STATENAME")),
                                        RegionName = Convert.ToString(state.Field<string>("REGIONAME"))
                                    });
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetStates", ex.StackTrace, ex.Message);
            }
            return ListofStates;

        }

        public IEnumerable<RoleMaster> GetRole()
        {
            IEnumerable<RoleMaster> ListofRole = null;
            try
            {
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetRole]", CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    ListofRole = (from role in outputFromSP.AsEnumerable()
                                    select new RoleMaster
                                    {
                                        RoleId = role.Field<int>("RoleId"),
                                        Name = role.Field<string>("Name")
                                    });
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetRole", ex.StackTrace, ex.Message);
            }
            return ListofRole;


        }

        public IEnumerable<ClassificationTypeMaster> GetDealerClassificationTypes()
        {
            IEnumerable<ClassificationTypeMaster> ListofClassTypes = null;
            try
            {
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetDealerClassificationTypes]", CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    ListofClassTypes = (from classtype in outputFromSP.AsEnumerable()
                                    select new ClassificationTypeMaster
                                    {
                                        ID = classtype.Field<Int64>("ID"),
                                        ClassificationName = classtype.Field<string>("CLASSNAME")
                                    });
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetDealerClassificationTypes", ex.StackTrace, ex.Message);
            }
            return ListofClassTypes;
        }

        public IEnumerable<AgencyMaster> GetAgency(AgencyDropdownInput InputData)
        {
            IEnumerable<AgencyMaster> ListofAgency = null;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@BranchId", InputData.BranchId, DbType.Int64));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetAgency]", objDBParam, CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    ListofAgency = (from agency in outputFromSP.AsEnumerable()
                                  select new AgencyMaster
                                  {
                                      AgencyId = agency.Field<Int64>("AgencyId"),
                                      AgencyName = agency.Field<string>("AgencyName")
                                  });
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetAgency", ex.StackTrace, ex.Message);
            }
            return ListofAgency;

        }

        public IEnumerable<IncentiveCategoryMaster> GetIncentiveCategory()
        {
            IEnumerable<IncentiveCategoryMaster> ListofAgency = null;
            try
            {
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetIncentiveCategory]", CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    ListofAgency = (from incentiveCategory in outputFromSP.AsEnumerable()
                                    select new IncentiveCategoryMaster
                                    {
                                        IncentiveCategoryId = incentiveCategory.Field<Int64>("IncentiveCategoryId"),
                                        IncentiveCategoryName = incentiveCategory.Field<string>("IncentiveCategory")
                                    });
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetIncentiveCategory", ex.StackTrace, ex.Message);
            }
            return ListofAgency;

        }

        public IEnumerable<ProductTargetCategoryMaster> GetProductTargetCategories()
        {
            IEnumerable<ProductTargetCategoryMaster> ListofCategories = null;
            try
            {
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetProductTargetCategory]", CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    ListofCategories = (from targetCategory in outputFromSP.AsEnumerable()
                                    select new ProductTargetCategoryMaster
                                    {
                                        Id = Convert.ToInt64(targetCategory.Field<Int64>("ID")),
                                        TargetCategory = targetCategory.Field<string>("TARGETCATEGORY")
                                    });
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetProductTargetCategories", ex.StackTrace, ex.Message);
            }
            return ListofCategories;

        }

        public IEnumerable<ProductTargetCategoryData> GetTargetCategoriesByProductCategories(ProductTargetCategorySearch objSearchData)
        {
            SqlParameter[] objDBParam = new SqlParameter[1];
            IEnumerable<ProductTargetCategoryData> ListofCategories = null;
            DataTable dtProdCatIds = new DataTable();
            DataRow row;
            try
            {
                if (objSearchData.ProductCategoryIds == null || objSearchData.ProductCategoryIds.Count == 0)
                    return ListofCategories;

                dtProdCatIds.Columns.Add("UserId");
                dtProdCatIds.Columns.Add("ProdCatId");

                foreach (Int64 categoryId in objSearchData.ProductCategoryIds)
                {
                    row = dtProdCatIds.NewRow();
                    row["UserId"] = objSearchData.UserId;
                    row["ProdCatId"] = categoryId;
                    dtProdCatIds.Rows.Add(row);
                }
                
                objDBParam[0] = new SqlParameter("@ProductCategoryIds", dtProdCatIds);

                var outputFromSP = _dataHelper.ExecuteProcedure("[dbo].[GetTargetCategoriesByProductCategories]", objDBParam);
                if (outputFromSP.Rows.Count > 0)
                {
                    ListofCategories = (from targetCategory in outputFromSP.AsEnumerable()
                                        select new ProductTargetCategoryData
                                        {
                                            TargetCategoryId = Convert.ToInt64(targetCategory.Field<Int64>("TARGETCATEGORYID")),
                                            TargetCategoryName = targetCategory.Field<string>("TARGETCATEGORYNAME")
                                        }).ToList();
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetTargetCategoriesByProductCategories", ex.StackTrace, ex.Message);
            }
            finally
            {
                row = null;
                dtProdCatIds.Dispose();
            }
            return ListofCategories;

        }

        public IEnumerable<TargetTypeMaster> GetTargetTypes()
        {
            IEnumerable<TargetTypeMaster> ListOfTypes = null;
            try
            {
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetTargetTypes]", CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    ListOfTypes = (from type in outputFromSP.AsEnumerable()
                                        select new TargetTypeMaster
                                        {
                                            Id = Convert.ToInt64(type.Field<Int64>("ID")),
                                            TargetType = Convert.ToString(type.Field<string>("TARGETTYPE"))
                                        });
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetTargetTypes", ex.StackTrace, ex.Message);
            }
            return ListOfTypes;
        }

        public IEnumerable<DivisionMaster> GetDivisions()
        {
            IEnumerable<DivisionMaster> ListofDivisions = null;
            try
            {
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetDivision]", CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    ListofDivisions = (from division in outputFromSP.AsEnumerable()
                                    select new DivisionMaster
                                    {
                                        Id = Convert.ToInt64(division.Field<Int64>("ID")),
                                        DivisionName = Convert.ToString(division.Field<string>("DIVISIONNAME"))
                                    });
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetDivisions", ex.StackTrace, ex.Message);
            }
            return ListofDivisions;
        }

        public IEnumerable<ProductCategoryMaster> GetProductCategories()
        {
            IEnumerable<ProductCategoryMaster> ListofCategories = null;
            try
            {
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetProductCategory]", CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    ListofCategories = (from category in outputFromSP.AsEnumerable()
                                       select new ProductCategoryMaster
                                       {
                                           ID = Convert.ToInt64(category.Field<Int64>("ID")),
                                           CategoryName = Convert.ToString(category.Field<string>("CATEGORYNAME"))
                                       });
                }
                else
                {
                    ListofCategories = new List<ProductCategoryMaster>();
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetProductCategories", ex.StackTrace, ex.Message);
            }
            return ListofCategories;
        }

        public IEnumerable<ProductCategoryMaster> GetUnmappedProdCatsForSFA(Common InputParam)
        {
            IEnumerable<ProductCategoryMaster> ListofCategories = null;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@EmployeeId", InputParam.Id, DbType.Int64));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetUnmappedProdCatsForSFA]", objDBParam, CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    ListofCategories = (from category in outputFromSP.AsEnumerable()
                                        select new ProductCategoryMaster
                                        {
                                            ID = Convert.ToInt64(category.Field<Int64>("ID")),
                                            CategoryName = Convert.ToString(category.Field<string>("CATEGORYNAME"))
                                        });
                }
                else
                {
                    ListofCategories = new List<ProductCategoryMaster>();
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetUnmappedProdCatsForSFA", ex.StackTrace, ex.Message);
            }
            return ListofCategories;
        }

        public IEnumerable<ProductCategoryGroupDD> GetProductCategoryForGroupMapping(Common InputParam)
        {
            IEnumerable<ProductCategoryGroupDD> ListofCategories = null;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@GroupId", InputParam.Id, DbType.String));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetProductCategoryForGroupMapping]", objDBParam, CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    ListofCategories = (from category in outputFromSP.AsEnumerable()
                                        select new ProductCategoryGroupDD
                                        {
                                            ID = Convert.ToInt64(category.Field<Int64>("ID")),
                                            CategoryName = Convert.ToString(category.Field<string>("CATEGORYNAME")),
                                            ProductId = Convert.ToInt64(category.Field<Int64>("ProductId")),
                                        });
                }
                else
                {
                    ListofCategories = new List<ProductCategoryGroupDD>();
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetProductCategoryForGroupMapping", ex.StackTrace, ex.Message);
            }
            return ListofCategories;
        }


        public IEnumerable<SFASubLevelMaster> GetSFALevels()
        {
            IEnumerable<SFASubLevelMaster> ListofLevels = null;
            try
            {
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetSFASubLevel]", CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    ListofLevels = (from level in outputFromSP.AsEnumerable()
                                        select new SFASubLevelMaster
                                        {
                                            Id = Convert.ToInt64(level.Field<Int64>("ID")),
                                            SFASubLevelName = Convert.ToString(level.Field<string>("SFASUBLEVELNAME")),
                                            SFALevelName = Convert.ToString(level.Field<string>("SFALEVELNAME"))
                                        });
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetSFALevels", ex.StackTrace, ex.Message);
            }
            return ListofLevels;
        }

        #endregion Get data API for dropdown

        /// <summary>
        /// To get list of City Type.
        /// </summary>
        /// <returns>List of City Type.</returns>
        public CityTypeList GetCityType()
        {
            CityTypeList Data = new CityTypeList();
            try
            {
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetCityType]", CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    Data.TypeList = (from region in outputFromSP.AsEnumerable()
                                   select new CityTypeMaster
                                   {
                                       CityTypeId = Convert.ToInt64(region.Field<Int64>("ID")),
                                       CityType = Convert.ToString(region.Field<string>("CityType")),
                                   });
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetCityType", ex.StackTrace, ex.Message);
            }
            return Data;
        }


        public IEnumerable<DivisionMaster> GetDivisionForProductCategory()
        {
            IEnumerable<DivisionMaster> ListofDivision = null;
            try
            {
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetDivisionforProductCategory]", CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    ListofDivision = (from category in outputFromSP.AsEnumerable()
                                        select new DivisionMaster
                                        {
                                            IdValue = Convert.ToString(category.Field<string>("IdValue")),
                                            DivisionName = Convert.ToString(category.Field<string>("DIVISIONNAME"))
                                        });
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetDivisionForProductCategory", ex.StackTrace, ex.Message);
            }
            return ListofDivision;
        }

        public IEnumerable<ProductCategoryMaster> GetProductCategoryByDivision(Common InputParam)
        {
            IEnumerable<ProductCategoryMaster> ListofProductCategory = null;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@DivisionId", InputParam.Value, DbType.String));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetProductCategoryByDivision]",objDBParam, CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    ListofProductCategory = (from category in outputFromSP.AsEnumerable()
                                      select new ProductCategoryMaster
                                      {
                                          
                                          CategoryName = Convert.ToString(category.Field<string>("ProductCategory"))
                                      });
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetProductCategoryByDivision", ex.StackTrace, ex.Message);
            }
            return ListofProductCategory;
        }


        public IEnumerable<Size> GetSize(AttributeGet InputParam)
        {
            DbParameterCollection objDBParam = new DbParameterCollection();
            IEnumerable<Size> ListofSize = null;
            try
            {
                objDBParam.Add(new DbParameter("@ProductCategoryId", InputParam.ProductCategoryId, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetSize]", objDBParam, CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    ListofSize = (from size in outputFromSP.AsEnumerable()
                                      select new Size
                                      {
                                          SizeId = size.Field<Int64>("SizeId"),
                                          SizeName = size.Field<string>("SizeName")
                                      });
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetSize", ex.StackTrace, ex.Message);
            }
            return ListofSize;

        }

        public IEnumerable<Segment> GetSegment(AttributeGet InputParam)
        {
            DbParameterCollection objDBParam = new DbParameterCollection();
            IEnumerable<Segment> ListofSegment = null;
            try
            {
                objDBParam.Add(new DbParameter("@ProductCategoryId", InputParam.ProductCategoryId, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetSegment]", objDBParam, CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    ListofSegment = (from Segment in outputFromSP.AsEnumerable()
                                  select new Segment
                                  {
                                      SegmentId = Segment.Field<Int64>("SegmentId"),
                                      SegmentName = Segment.Field<string>("SegmentName")
                                  });
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetSegment", ex.StackTrace, ex.Message);
            }
            return ListofSegment;

        }

        public IEnumerable<Internet> GetInternet(AttributeGet InputParam)
        {
            DbParameterCollection objDBParam = new DbParameterCollection();
            IEnumerable<Internet> ListofInternet = null;
            try
            {
                objDBParam.Add(new DbParameter("@ProductCategoryId", InputParam.ProductCategoryId, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetInternet]", objDBParam, CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    ListofInternet = (from Internet in outputFromSP.AsEnumerable()
                                  select new Internet
                                  {
                                      InternetId = Internet.Field<Int64>("InternetId"),
                                      InternetName = Internet.Field<string>("InternetName")
                                  });
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetInternet", ex.StackTrace, ex.Message);
            }
            return ListofInternet;

        }

        public IEnumerable<TVType> GetTVType(AttributeGet InputParam)
        {
            DbParameterCollection objDBParam = new DbParameterCollection();
            IEnumerable<TVType> ListofTVType = null;
            try
            {
                objDBParam.Add(new DbParameter("@ProductCategoryId", InputParam.ProductCategoryId, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetTVType]", objDBParam, CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    ListofTVType = (from TVType in outputFromSP.AsEnumerable()
                                  select new TVType
                                  {
                                      TVTypeId = TVType.Field<Int64>("TVTypeId"),
                                      TVTypeName = TVType.Field<string>("TVTypeName")
                                  });
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetTVType", ex.StackTrace, ex.Message);
            }
            return ListofTVType;

        }

        public IEnumerable<D3> GetD3(AttributeGet InputParam)
        {
            DbParameterCollection objDBParam = new DbParameterCollection();
            IEnumerable<D3> ListofD3 = null;
            try
            {
                objDBParam.Add(new DbParameter("@ProductCategoryId", InputParam.ProductCategoryId, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[Get3D]", objDBParam, CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    ListofD3 = (from D3 in outputFromSP.AsEnumerable()
                                  select new D3
                                  {
                                      D3Id = D3.Field<Int64>("D3Id"),
                                      D3Name = D3.Field<string>("D3Name")
                                  });
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetD3", ex.StackTrace, ex.Message);
            }
            return ListofD3;

        }

        public IEnumerable<Resolution> GetResolution(AttributeGet InputParam)
        {
            DbParameterCollection objDBParam = new DbParameterCollection();
            IEnumerable<Resolution> ListofResolution = null;
            try
            {
                objDBParam.Add(new DbParameter("@ProductCategoryId", InputParam.ProductCategoryId, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetResolution]", objDBParam, CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    ListofResolution = (from Resolution in outputFromSP.AsEnumerable()
                                  select new Resolution
                                  {
                                      ResolutionId = Resolution.Field<Int64>("ResolutionId"),
                                      ResolutionName = Resolution.Field<string>("ResolutionName")
                                  });
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetResolution", ex.StackTrace, ex.Message);
            }
            return ListofResolution;

        }


        public int ValidateMaterialCode(Common InputParam, out string Message)
        {
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@MaterialCode", InputParam.Value, DbType.String));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[ValidateMaterialCode]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return 1;
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("ValidateMaterialCode", ex.StackTrace, ex.Message);
                Message = "Could not validate code. Exception occured in API.";
            }
            return 0;
        }
		
		public IEnumerable<ProductSubCategoryMaster> GetProductSubCategoryByCategoryId(Common InputParam)
        {
            DbParameterCollection objDBParam = new DbParameterCollection();
            IEnumerable<ProductSubCategoryMaster> ListofSubCat = null;
            try
            {
                objDBParam.Add(new DbParameter("@ProductCategoryId", InputParam.Id, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetProductSubCategoryByCategoryId]", objDBParam, CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    ListofSubCat = (from subcat in outputFromSP.AsEnumerable()
                                    select new ProductSubCategoryMaster
                                    {
                                        ID = subcat.Field<Int64>("ID"),
                                        SubCategoryName = subcat.Field<string>("SUBCATEGORYNAME")
                                    });
                }
                else
                    ListofSubCat = new List<ProductSubCategoryMaster>();
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetProductSubCategoryByCategoryId", ex.StackTrace, ex.Message);
            }
            return ListofSubCat;
        }

        /// <summary>
        /// To get list of Competitors.
        /// </summary>
        /// <returns>List of Competitors.</returns>
        public IEnumerable<Competitors> GetCompetitors()
        {
            IEnumerable<Competitors> CompetitorsList = null;
            try
            {
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetCompetitorGridData]", CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    CompetitorsList = (from competitors in outputFromSP.AsEnumerable()
                                        select new Competitors
                                        {
                                            ID = competitors.Field<Int64>("ID"),
                                            Name = competitors.Field<string>("CompetitorName")
                                        });
                }
                else
                {
                    CompetitorsList = new List<Competitors>();
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetCompetitors", ex.StackTrace, ex.Message);
            }
            return CompetitorsList;

        }

        /// <summary>
        /// To get Competitor's Product Categories.
        /// </summary>
        /// <param name="CompetitorId">Competitor ID</param>
        /// <returns>Competitor's Product Categories.</returns>
        public IEnumerable<CompetitorProducts> GetCompetitorProducts(CompetitorProductsInput CompetitorId)
        {
            DbParameterCollection objDBParam = new DbParameterCollection();
            IEnumerable<CompetitorProducts> CompetitorProductsList = null;
            try
            {
                objDBParam.Add(new DbParameter("@CompetitorId", CompetitorId.CompetitorID, DbType.Int64));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetCompetitorProCatDropdown]",objDBParam, CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    CompetitorProductsList = (from products in outputFromSP.AsEnumerable()
                                                   select new CompetitorProducts
                                                   {
                                                       ID = products.Field<Int64>("ID"),
                                                       Name = products.Field<string>("ProductName")
                                                   });
                }
                else
                {
                    CompetitorProductsList = new List<CompetitorProducts>(); 
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetCompetitorProducts", ex.StackTrace, ex.Message);
            }
            return CompetitorProductsList;

        }

        /// <summary>
        /// To get list of Materials(Models).
        /// </summary>
        /// <returns>Materials(Models)</returns>
        public IEnumerable<Materials> GetMaterialDataforDD()
        {
            IEnumerable<Materials> MaterialsList = null;
            try
            {
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetMaterialMasterDropdown]", CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    MaterialsList = (from materials in outputFromSP.AsEnumerable()
                                                                 select new Materials
                                                                 {
                                                                     ID = materials.Field<Int64>("ID"),
                                                                     Name = materials.Field<string>("Name")
                                                                 });
                }
                else
                {
                    MaterialsList = new List<Materials>();
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetMaterialDataforDD", ex.StackTrace, ex.Message);
            }
            return MaterialsList;

        }

        /// <summary>
        /// To get list of Materials(Models) based on product sub category.
        /// </summary>
        /// <returns>Materials(Models)</returns>
        public IEnumerable<Materials> GetMaterialDataforDDByProSub(MaterialDDInput Input)
        {
            DbParameterCollection objDBParam = new DbParameterCollection();
            IEnumerable<Materials> MaterialsList = null;
            try
            {
                objDBParam.Add(new DbParameter("@ProSubCatId", Input.ProSubCatId, DbType.Int64));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetMaterialMasterDropdown]", objDBParam, CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    MaterialsList = (from materials in outputFromSP.AsEnumerable()
                                     select new Materials
                                     {
                                         ID = materials.Field<Int64>("ID"),
                                         Name = materials.Field<string>("Name")
                                     });
                }
                else
                {
                    MaterialsList = new List<Materials>();
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetMaterialDataforDDByProSub", ex.StackTrace, ex.Message);
            }
            return MaterialsList;

        }

        /// <summary>
        /// To fetch Sony Product Category and Sub Category data based on Competitor Category Id.
        /// </summary>
        /// <param name="Id">Competitor Category Id.</param>
        /// <returns>Sony Product Category and Sub Category data</returns>
        public CompetitorData GetSonyProducts(CompetitorDataInput Id)
        {
            DbParameterCollection objDBParam = new DbParameterCollection();
            CompetitorData CompetitorData = new CompetitorData();
            try
            {
                objDBParam.Add(new DbParameter("@CProductCatId", Id.CProductCatId, DbType.Int64));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetSonyProdCatbyCompProdCatId]", objDBParam, CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    CompetitorData.SProductCatId = Convert.ToInt64(outputFromSP.Rows[0]["ID"]);
                    CompetitorData.SProductCatName = Convert.ToString(outputFromSP.Rows[0]["CategoryName"]);
                    if (outputFromSP.Rows[0]["SubCatID"].ToString() != "")
                    {
                        CompetitorData.SProductSubCatList = (from Resolution in outputFromSP.AsEnumerable()
                                                             select new CompetitorProducts
                                                             {
                                                                 ID = Convert.ToInt64(Resolution.Field<Int64>("SubCatID")),
                                                                 Name = Convert.ToString(Resolution.Field<string>("SubCategoryName"))
                                                             });
                    }
                    else
                        CompetitorData.SProductSubCatList = new List<CompetitorProducts>();
                }
                else
                {
                    CompetitorData = new CompetitorData() {SProductSubCatList = new List<CompetitorProducts>() };
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetSonyProducts", ex.StackTrace, ex.Message);
                CompetitorData = new CompetitorData() { SProductSubCatList = new List<CompetitorProducts>() };
            }
            return CompetitorData;

        }


        /// <summary>
        /// To get list of SFAType(Models).
        /// </summary>
        /// <returns>Materials(Models)</returns>
        public IEnumerable<SFAType> GetSFAType()
        {
            IEnumerable<SFAType> ListOfSFAType = null;
            try
            {
                
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetSFAType]", CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    ListOfSFAType = (from sfaType in outputFromSP.AsEnumerable()
                                    select new SFAType
                                    {
                                        SFATypeId = sfaType.Field<Int64>("SFATypeId"),
                                        SFATypeName = sfaType.Field<string>("SFATypeName")
                                    });
                }
                else
                    ListOfSFAType = new List<SFAType>();
            }
            catch(Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetSFAType", ex.StackTrace, ex.Message);
            }
            return ListOfSFAType;

        }


        public int ValidateSAPCode(Common InputParam, out string Message)
        {
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@SAPCode", InputParam.Value, DbType.String));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[ValidateSAPCode]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                return Convert.ToInt32(outputFromSP.Rows[0]["Status"]);
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("ValidateSAPCode", ex.StackTrace, ex.Message);
                Message = "Could not delete location. Exception occured in API.";
            }
            return 0;
        }

        /// <summary>
        /// To get list of DisplayOrder(Models).
        /// </summary>
        /// <returns>DisplayOrder(Models)</returns>
        public IEnumerable<DisplayOrder> GetDisplayOrder()
        {
            IEnumerable<DisplayOrder> ListOfDisplayOrder = null;
            try
            {

                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetDisplayOrder]", CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    ListOfDisplayOrder = (from displayOrder in outputFromSP.AsEnumerable()
                                     select new DisplayOrder
                                     {
                                         DisplayOrderId = displayOrder.Field<Int64>("DisplayOrderId"),
                                         DisplayOrderName =Convert.ToString(displayOrder.Field<int>("DisplayOrderName"))
                                     });
                }
                else
                    ListOfDisplayOrder = new List<DisplayOrder>();
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetDisplayOrder", ex.StackTrace, ex.Message);
            }
            return ListOfDisplayOrder;

        }

        public IEnumerable<AssetAssignmentToRDIGet> GetReferenceId()
        {
            IEnumerable<AssetAssignmentToRDIGet> ListOfReferences = null;
            try
            {

                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetReferenceID]", CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    ListOfReferences = (from sfaType in outputFromSP.AsEnumerable()
                                     select new AssetAssignmentToRDIGet
                                     {
                                         //Id = sfaType.Field<Int64>("Id"),
                                         Reference = sfaType.Field<string>("Reference")
                                     });
                }
                else
                    ListOfReferences = new List<AssetAssignmentToRDIGet>();
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetReferenceId", ex.StackTrace, ex.Message);
            }
            return ListOfReferences;
        }

        public List<string> GetMaterialMasterCodeList()
        {
            List<string> ListCode = null;
            try
            {
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetMaterialMasterCodeList]", CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    ListCode = (from data in outputFromSP.AsEnumerable()
                                select data.Field<string>("MaterialCode")).Distinct().ToList();
                }
                else
                    ListCode = new List<string>();
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetMaterialMasterCodeList", ex.StackTrace, ex.Message);
            }
            return ListCode;
        }

        public List<string> GetDealerCodeList()
        {
            List<string> ListCode = null;
            try
            {
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetDealerCodeList]", CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    ListCode = (from data in outputFromSP.AsEnumerable()
                                select data.Field<string>("MasterCode")).Distinct().ToList();
                }
                else
                    ListCode = new List<string>();
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetDealerCodeList", ex.StackTrace, ex.Message);
            }
            return ListCode;
        }

        public IEnumerable<LevelOfEducation> GetLevelOfEducation()
        {
            IEnumerable<LevelOfEducation> ListOfLoe = null;
            try
            {
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetLevelofEducation]", CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    ListOfLoe = (from loe in outputFromSP.AsEnumerable()
                                        select new LevelOfEducation
                                        {
                                            Id = loe.Field<Int64>("Id"),
                                            Name = loe.Field<string>("Name")
                                        });
                }
                else
                    ListOfLoe = new List<LevelOfEducation>();
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetLevelOfEducation", ex.StackTrace, ex.Message);
            }
            return ListOfLoe;
        }

        public IEnumerable<Source> GetSource()
        {
            IEnumerable<Source> ListOfSource = null;
            try
            {
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetSource]", CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    ListOfSource = (from source in outputFromSP.AsEnumerable()
                                        select new Source
                                        {
                                            Id = source.Field<Int64>("Id"),
                                            Name = source.Field<string>("Name")
                                        });
                }
                else
                    ListOfSource = new List<Source>();
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetSource", ex.StackTrace, ex.Message);
            }
            return ListOfSource;
        }

        public IEnumerable<Gender> GetGender()
        {
            IEnumerable<Gender> ListOfGender = null;
            try
            {
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetGender]", CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    ListOfGender = (from gender in outputFromSP.AsEnumerable()
                                        select new Gender
                                        {
                                            Id = gender.Field<Int64>("Id"),
                                            Name = gender.Field<string>("Name")
                                        });
                }
                else
                    ListOfGender = new List<Gender>();
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetGender", ex.StackTrace, ex.Message);
            }
            return ListOfGender;
        }

        public IEnumerable<OutletType> GetOutletType()
        {
            IEnumerable<OutletType> ListOfOutlet = null;
            try
            {
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetOutletType]", CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    ListOfOutlet = (from outlettype in outputFromSP.AsEnumerable()
                                    select new OutletType
                                    {
                                        Id = outlettype.Field<Int64>("Id"),
                                        Name = outlettype.Field<string>("OutletType")
                                    });
                }
                else
                    ListOfOutlet = new List<OutletType>();
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetOutletType", ex.StackTrace, ex.Message);
            }
            return ListOfOutlet;
        }

        public IEnumerable<IncentiveCategoryMaster> GetAllIncentiveCategory()
        {
            IEnumerable<IncentiveCategoryMaster> ListOfIncenCat = null;
            try
            {
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetAllIncentiveCategory]", CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    ListOfIncenCat = (from incentivecat in outputFromSP.AsEnumerable()
                                    select new IncentiveCategoryMaster
                                    {
                                        IncentiveCategoryId = incentivecat.Field<Int64>("ID"),
                                        IncentiveCategoryName = incentivecat.Field<string>("INCENTIVECATEGORYNAME")
                                    });
                }
                else
                    ListOfIncenCat = new List<IncentiveCategoryMaster>();
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetAllIncentiveCategory", ex.StackTrace, ex.Message);
            }
            return ListOfIncenCat;
        }

        public IEnumerable<ModuleMaster> GetAllModules()
        {
            IEnumerable<ModuleMaster> ListOfModules = null;
            try
            {
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetAllModules]", CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    ListOfModules = (from modulemaster in outputFromSP.AsEnumerable()
                                      select new ModuleMaster
                                      {
                                          ModuleId = modulemaster.Field<Int64>("ModuleId"),
                                          ModuleName = modulemaster.Field<string>("ModuleName")
                                      });
                }
                else
                    ListOfModules = new List<ModuleMaster>();
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetAllModules", ex.StackTrace, ex.Message);
            }
            return ListOfModules;
        }

        public IEnumerable<SubModuleMaster> GetSubModulesByModuleId(SubModuleMasterGet InputParam)
        {
            IEnumerable<SubModuleMaster> ListofSubModules = null;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@ModuleId", InputParam.ModuleId, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetSubModulesByModuleId]", objDBParam, CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    ListofSubModules = (from submodulemaster in outputFromSP.AsEnumerable()
                                      select new SubModuleMaster
                                      {
                                          SubModuleId = submodulemaster.Field<Int64>("SubModuleId"),
                                          SubModuleName = submodulemaster.Field<string>("SubModuleName")
                                      });
                }
                else
                    ListofSubModules = new List<SubModuleMaster>();
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetSubModulesByModuleId", ex.StackTrace, ex.Message);
            }
            return ListofSubModules;
        }

        public IEnumerable<FestivalIncentiveScheme> GetAllFestivalScheme(FestivalIncentiveSchemeParam InputParam)
        {
            IEnumerable<FestivalIncentiveScheme> ListOfFestivalIncentiveScheme = null;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@StartDate", InputParam.StartDate, DbType.String));
                objDBParam.Add(new DbParameter("@EndDate", InputParam.EndDate, DbType.String));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetAllFestivalScheme]",objDBParam ,CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    ListOfFestivalIncentiveScheme = (from schememaster in outputFromSP.AsEnumerable()
                                     select new FestivalIncentiveScheme
                                     {
                                         Id = schememaster.Field<Int64>("Id"),
                                         SchemeName = schememaster.Field<string>("SchemeName")
                                     });
                }
                else
                    ListOfFestivalIncentiveScheme = new List<FestivalIncentiveScheme>();
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetAllFestivalScheme", ex.StackTrace, ex.Message);
            }
            return ListOfFestivalIncentiveScheme;
        }

        /// <summary>
        /// To get all Attendance Type Data.
        /// Dhruv Sharma, ValueFirst, Gurgaon
        /// </summary>
        /// <returns>Attendance Type List</returns>
        public IEnumerable<AttendanceType> GetAllAttendanceType()
        {
            IEnumerable<AttendanceType> AttendanceType = null;
            try
            {
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetAttendanceTypeList]", CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    AttendanceType = (from schememaster in outputFromSP.AsEnumerable()
                                                     select new AttendanceType
                                                     {
                                                         AttTypeId = schememaster.Field<Int64>("Id"),
                                                         AttType = schememaster.Field<string>("AttendanceType")
                                                     });
                }
                else
                    AttendanceType = new List<AttendanceType>();
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetAllAttendanceType", ex.StackTrace, ex.Message);
            }
            return AttendanceType;
        }

        /// <summary>
        /// To get all deviation reasons.
        /// Nikhil Thakral, ValueFirst, Gurgaon
        /// </summary>
        /// <returns>Deviation Reasons List</returns>
        public IEnumerable<DeviationReasons> GetAllDeviationReasons()
        {
            IEnumerable<DeviationReasons> DeviationReasons = null;
            try
            {
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetAllDeviationReasons]", CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    DeviationReasons = (from reasons in outputFromSP.AsEnumerable()
                                      select new DeviationReasons
                                      {
                                          Id = reasons.Field<Int64>("Id"),
                                          ReasonName = reasons.Field<string>("ReasonName"),
                                          Attribute1 = reasons.Field<string>("Attribute1"),
                                          Attribute2 = reasons.Field<string>("Attribute2")
                                      });
                }
                else
                    DeviationReasons = new List<DeviationReasons>();
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetAllDeviationReasons", ex.StackTrace, ex.Message);
            }
            return DeviationReasons;
        }
		
		public IEnumerable<ProductGroupCategoryMaster> GetProductCategoryGroupDropDown()
        {
            IEnumerable<ProductGroupCategoryMaster> groupnames = null;
            try
            {
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetProductCategoryGroupDropDown]", CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    groupnames = (from schememaster in outputFromSP.AsEnumerable()
                                      select new ProductGroupCategoryMaster
                                      {
                                          GroupId = schememaster.Field<Int64>("GroupId"),
                                          GroupName = schememaster.Field<string>("GroupName")
                                      });
                }
                else
                    groupnames = new List<ProductGroupCategoryMaster>();
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetProductCategoryGroupDropDown", ex.StackTrace, ex.Message);
            }
            return groupnames;
        }

        public GetBranch GetBranchByUserId(GetBranch InputParam)
        {
            GetBranch Branchdetails = new GetBranch();
            DbParameterCollection objDBParam = new DbParameterCollection(); 
            try
            {
                objDBParam.Add(new DbParameter("@UserId", InputParam.UserId, DbType.Int64));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetBranchByUserId]", objDBParam, CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    Branchdetails = (from GetBranch in outputFromSP.AsEnumerable()
                                     select new GetBranch
                                     {
                                         BranchId = GetBranch.Field<Int64>("BRANCHID"),
                                         Branch = GetBranch.Field<string>("BRANCH")
                                     }).FirstOrDefault();
                }

            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetBranchByUserId", ex.StackTrace, ex.Message);
            }
            return Branchdetails;
        }

        public IEnumerable<TrainingDropdown> GetTrainings(GetTrainingDropdown InputParam)
        {
            IEnumerable<TrainingDropdown> ListofTrainings = null;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@BranchId", InputParam.BranchId, DbType.Int64));
                objDBParam.Add(new DbParameter("@ChannelId", InputParam.ChannelId, DbType.Int64));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetTrainings]", objDBParam, CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    ListofTrainings = (from training in outputFromSP.AsEnumerable()
                                        select new TrainingDropdown
                                        {
                                            Id = training.Field<Int64>("Id"),
                                            CourseName = training.Field<string>("CourseName")
                                        });
                }
                else
                    ListofTrainings = new List<TrainingDropdown>();
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetTrainings", ex.StackTrace, ex.Message);
            }
            return ListofTrainings;
        }

        public IEnumerable<SubCatgeory> GetSubCategoryforMultipleProducts(SubCatgeoryByProduct InputParam)
        {
            IEnumerable<SubCatgeory> output = null;
            DataTable dtproducts = new DataTable();
            
            DataRow row;
            try
            {
                dtproducts.Columns.Add("FilterId");
                if (InputParam.ProductIds != null)
                {
                    foreach (Int64 productid in InputParam.ProductIds)
                    {
                        row = dtproducts.NewRow();
                        row["FilterId"] = productid;
                        dtproducts.Rows.Add(row);
                    }
                }

                SqlParameter[] objDBParam = new SqlParameter[1];
                objDBParam[0] = new SqlParameter("@ProCatId", dtproducts);
               

                var outputFromSP = _dataHelper.ExecuteProcedure("[dbo].[GetSubCategoryforMultipleProducts]", objDBParam);

                if (outputFromSP.Rows.Count > 0)
                {
                    output = (from location in outputFromSP.AsEnumerable()
                                   select new SubCatgeory
                                   {
                                       Id = Convert.ToInt64(location.Field<Int64>("ID")),
                                       Name = Convert.ToString(location.Field<string>("Name")),
                                   });
                }
                else
                    output = new List<SubCatgeory>();
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetMaterialByProduct", ex.StackTrace, ex.Message);
            }
            return output;
        }

        public Branchfohr GetBranchMappedForHR(GetBranch InputParam)
        {
            Branchfohr ListofBranches = new Branchfohr();
            ListofBranches.BranchIds = new List<Int64>();
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@UserId", InputParam.UserId, DbType.String));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetBranchMappedForHR]", objDBParam, CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    for (int i = 0; i < outputFromSP.Rows.Count; i++)
                    {                         
                        ListofBranches.BranchIds.Add(Convert.ToInt32(outputFromSP.Rows[i]["BranchId"]));
                    }
                }
                
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetBranchMappedForHR", ex.StackTrace, ex.Message);
            }
            return ListofBranches;
        }

        public IEnumerable<ShiftTimingName> GetShiftTiming()
        {
            IEnumerable<ShiftTimingName> ListOfShift = null;
            try
            {
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetShiftTiming]", CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {
                    ListOfShift = (from shift in outputFromSP.AsEnumerable()
                                    select new ShiftTimingName
                                    {
                                        ShiftNameId = shift.Field<Int64>("Id"),
                                        ShiftName = shift.Field<string>("ShiftName")
                                    });
                }
                else
                    ListOfShift = new List<ShiftTimingName>();
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetShiftTiming", ex.StackTrace, ex.Message);
            }
            return ListOfShift;
        }
    }
}
