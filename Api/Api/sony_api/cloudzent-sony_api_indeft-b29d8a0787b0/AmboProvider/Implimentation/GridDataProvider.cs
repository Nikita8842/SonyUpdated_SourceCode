using AmboProvider.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmboLibrary.MasterMaintenance;
using DBHelper;
using System.Data;
using AmboLibrary.UserManagement;
using AmboLibrary.Mappings;
using AmboLibrary.Modules;
using AmboLibrary.IncentiveManagement;
using System.Data.SqlClient;
using AmboUtilities.Helper;
using System.Globalization;

namespace AmboProvider.Implimentation
{
    public class GridDataProvider : IGridDataProvider
    {
        private readonly DbHelper _dataHelper;
        private readonly IErrorLogProvider _IErrorLogProvider;

        public GridDataProvider(IErrorLogProvider IErrorLogProvider)
        {
            _dataHelper = DbHelper.GetInstance("defaultConnection");
            _IErrorLogProvider = IErrorLogProvider;
        }

        private DbParameterCollection GetDBParamFromGrid(ref GridVariables objGridVariables)
        {
            DbParameterCollection objDBParam = new DbParameterCollection();
            objDBParam.Add(new DbParameter("@PageStart", objGridVariables.PageStart, DbType.Int32));
            objDBParam.Add(new DbParameter("@PageSize", objGridVariables.PageSize, DbType.Int32));
            objDBParam.Add(new DbParameter("@SortColumn", objGridVariables.SortColumn, DbType.Int32));
            objDBParam.Add(new DbParameter("@SortDirection", objGridVariables.SortDirection, DbType.String));
            objDBParam.Add(new DbParameter("@SearchValue", objGridVariables.SearchValue, DbType.String));
            return objDBParam;
        }

        public GridOutput<IEnumerable<RegionGridData>> GetRegionMasterGrid(GridVariables objGridVariables)
        {
            GridOutput<IEnumerable<RegionGridData>> output = new GridOutput<IEnumerable<RegionGridData>>();
            output.draw = objGridVariables.Draw;
            DbParameterCollection objDBParam = GetDBParamFromGrid(ref objGridVariables);
            try
            {
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetRegionMasterGrid]", objDBParam, CommandType.StoredProcedure);

                if (outputFromSP.Rows.Count > 0)
                {
                    output.data = (from region in outputFromSP.AsEnumerable()
                                   select new RegionGridData
                                   {
                                       ID = Convert.ToInt64(region.Field<Int64>("ID")),
                                       RegionName = Convert.ToString(region.Field<string>("REGIONNAME")),
                                       Status = Convert.ToString(region.Field<string>("STATUS"))
                                   });
                }
                else
                    output.data = new List<RegionGridData>();
                output.recordsFiltered = Convert.ToInt32(outputFromSP.Rows[0]["FilteredCount"]);
                output.recordsTotal = Convert.ToInt32(outputFromSP.Rows[0]["FilteredCount"]);
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetRegionMasterGrid", ex.StackTrace, ex.Message);
            }
            return output;
        }
    
        public GridOutput<IEnumerable<StateGridData>> GetStateMasterGrid(GridVariables objGridVariables)
        {
            GridOutput<IEnumerable<StateGridData>> output = new GridOutput<IEnumerable<StateGridData>>();
            output.draw = objGridVariables.Draw;
            DbParameterCollection objDBParam = GetDBParamFromGrid(ref objGridVariables);
            try
            {
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetStateMasterGrid]", objDBParam, CommandType.StoredProcedure);

                if (outputFromSP.Rows.Count > 0)
                {
                    output.data = (from state in outputFromSP.AsEnumerable()
                                   select new StateGridData
                                   {
                                       ID = Convert.ToInt64(state.Field<Int64>("ID")),
                                       StateName = Convert.ToString(state.Field<string>("STATENAME")),
                                       RegionID = Convert.ToInt64(state.Field<Int64>("REGIONID")),
                                       RegionName = Convert.ToString(state.Field<string>("REGIONNAME")),
                                       Status = Convert.ToString(state.Field<string>("STATUS"))
                                   });
                }
                else
                    output.data = new List<StateGridData>();
                output.recordsFiltered = Convert.ToInt32(outputFromSP.Rows[0]["FilteredCount"]);
                output.recordsTotal = Convert.ToInt32(outputFromSP.Rows[0]["FilteredCount"]);
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetStateMasterGrid", ex.StackTrace, ex.Message);
            }
            return output;
        }

        public GridOutput<IEnumerable<CityGridData>> GetCityMasterGrid(GridVariables objGridVariables)
        {
            GridOutput<IEnumerable<CityGridData>> output = new GridOutput<IEnumerable<CityGridData>>();
            output.draw = objGridVariables.Draw;
            DbParameterCollection objDBParam = GetDBParamFromGrid(ref objGridVariables);
            try
            {
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetCityMasterGrid]", objDBParam, CommandType.StoredProcedure);

                if (outputFromSP.Rows.Count > 0)
                {
                    output.data = (from city in outputFromSP.AsEnumerable()
                                   select new CityGridData
                                   {
                                       ID = Convert.ToInt64(city.Field<Int64>("ID")),
                                       CityName = Convert.ToString(city.Field<string>("CITYNAME")),
                                       StateName = Convert.ToString(city.Field<string>("STATENAME")),
                                       RegionName = Convert.ToString(city.Field<string>("REGIONNAME")),
                                       CityTypeName = Convert.ToString(city.Field<string>("CITYTYPENAME")),
                                       Status = Convert.ToString(city.Field<string>("STATUS")),
                                       CityTypeId = Convert.ToInt64(city.Field<Int64>("CITYTYPEID")),
                                       StateId = Convert.ToInt64(city.Field<Int64>("STATEID")),
                                       RegionId = Convert.ToInt64(city.Field<Int64>("REGIONID"))
                                   });
                }
                else
                    output.data = new List<CityGridData>();
                output.recordsFiltered = Convert.ToInt32(outputFromSP.Rows[0]["FilteredCount"]);
                output.recordsTotal = Convert.ToInt32(outputFromSP.Rows[0]["FilteredCount"]);
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetCityMasterGrid", ex.StackTrace, ex.Message);
            }
            return output;
        }

        public GridOutput<IEnumerable<LocationGridData>> GetLocationMasterGrid(LocationFilter objGridVariables)
        {
            GridOutput<IEnumerable<LocationGridData>> output = new GridOutput<IEnumerable<LocationGridData>>();
            output.draw = objGridVariables.draw;
            DataTable dtregions = new DataTable();
            DataTable dtstates = new DataTable();
            DataTable dtcities = new DataTable();
            DataTable dtlocations = new DataTable();
            DataRow row;
            //DbParameterCollection objDBParam = GetDBParamFromGrid(ref objGridVariables);
            try
            {
                dtregions.Columns.Add("FilterId");
                if (objGridVariables.RegionIds != null)
                {
                    foreach (Int64 regionid in objGridVariables.RegionIds)
                    {
                        row = dtregions.NewRow();
                        row["FilterId"] = regionid;
                        dtregions.Rows.Add(row);
                    }
                }

                dtstates.Columns.Add("FilterId");
                if (objGridVariables.StateIds != null)
                {
                    foreach (Int64 stateid in objGridVariables.StateIds)
                    {
                        row = dtstates.NewRow();
                        row["FilterId"] = stateid;
                        dtstates.Rows.Add(row);
                    }
                }

                dtcities.Columns.Add("FilterId");
                if (objGridVariables.CityIds != null)
                {
                    foreach (Int64 cityid in objGridVariables.CityIds)
                    {
                        row = dtcities.NewRow();
                        row["FilterId"] = cityid;
                        dtcities.Rows.Add(row);
                    }
                }

                dtlocations.Columns.Add("FilterId");
                if (objGridVariables.LocationIds != null)
                {
                    foreach (Int64 locid in objGridVariables.LocationIds)
                    {
                        row = dtlocations.NewRow();
                        row["FilterId"] = locid;
                        dtlocations.Rows.Add(row);
                    }
                }

                SqlParameter[] objDBParam = new SqlParameter[10];
                objDBParam[0] = new SqlParameter("@RegionIds", dtregions);
                objDBParam[1] = new SqlParameter("@StateIds", dtstates);
                objDBParam[2] = new SqlParameter("@CityIds", dtcities);
                objDBParam[3] = new SqlParameter("@LocationIds", dtlocations);
                objDBParam[4] = new SqlParameter("@Status", objGridVariables.Status);
                objDBParam[5] = new SqlParameter("@PageStart", objGridVariables.start);
                objDBParam[6] = new SqlParameter("@PageSize", objGridVariables.length);
                if (objGridVariables.order != null)
                    objDBParam[7] = new SqlParameter("@SortColumn", objGridVariables.order.FirstOrDefault().column);
                else
                    objDBParam[7] = new SqlParameter("@SortColumn", "");
                if (objGridVariables.order != null)
                    objDBParam[8] = new SqlParameter("@SortDirection", objGridVariables.order.FirstOrDefault().dir);
                else
                    objDBParam[8] = new SqlParameter("@SortDirection", "");

                if (objGridVariables.search != null)
                    objDBParam[9] = new SqlParameter("@SearchValue", objGridVariables.search.value);
                else
                    objDBParam[9] = new SqlParameter("@SearchValue", "");

                //var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetLocationMasterGrid]", objDBParam, CommandType.StoredProcedure);

                var outputFromSP = _dataHelper.ExecuteProcedure("[dbo].[GetLocationMasterGrid]", objDBParam);

                if (outputFromSP.Rows.Count > 0)
                {
                    output.data = (from location in outputFromSP.AsEnumerable()
                                   select new LocationGridData
                                   {
                                       ID = Convert.ToInt64(location.Field<Int64>("ID")),
                                       LocationName = Convert.ToString(location.Field<string>("LOCATIONNAME")),
                                       CityName = Convert.ToString(location.Field<string>("CITYNAME")),
                                       StateName = Convert.ToString(location.Field<string>("STATENAME")),
                                       RegionName = Convert.ToString(location.Field<string>("REGIONNAME")),
                                       Status = Convert.ToString(location.Field<string>("ISACTIVE")),
                                       CityId = Convert.ToInt64(location.Field<Int64>("CITYID")),
                                       StateId = Convert.ToInt64(location.Field<Int64>("STATEID")),
                                       RegionId = Convert.ToInt64(location.Field<Int64>("REGIONID"))
                                   });
                }
                else
                    output.data = new List<LocationGridData>();
                output.recordsFiltered = Convert.ToInt32(outputFromSP.Rows[0]["FilteredCount"]);
                output.recordsTotal = Convert.ToInt32(outputFromSP.Rows[0]["FilteredCount"]);
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetLocationMasterGrid", ex.StackTrace, ex.Message);
            }
            return output;
        }

        public GridOutput<IEnumerable<BranchGridData>> GetBranchMasterGrid(GridVariables objGridVariables)
        {
            GridOutput<IEnumerable<BranchGridData>> output = new GridOutput<IEnumerable<BranchGridData>>();
            output.draw = objGridVariables.Draw;
            DbParameterCollection objDBParam = GetDBParamFromGrid(ref objGridVariables);
            try
            {
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetBranchMasterGrid]", objDBParam, CommandType.StoredProcedure);

                if (outputFromSP.Rows.Count > 0)
                {
                    output.data = (from branch in outputFromSP.AsEnumerable()
                                   select new BranchGridData
                                   {
                                       ID = Convert.ToInt32(branch.Field<Int64>("ID")),
                                       BranchCode = Convert.ToString(branch.Field<string>("BRANCHCODE")),
                                       BranchName = Convert.ToString(branch.Field<string>("BRANCHNAME")),
                                       Status = Convert.ToString(branch.Field<string>("ISACTIVE"))
                                   });
                }
                else
                    output.data = new List<BranchGridData>();
                output.recordsFiltered = Convert.ToInt32(outputFromSP.Rows[0]["FilteredCount"]);
                output.recordsTotal = Convert.ToInt32(outputFromSP.Rows[0]["FilteredCount"]);
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetBranchMasterGrid", ex.StackTrace, ex.Message);
            }
            return output;
        }

        public GridOutput<IEnumerable<ProductCategoryGridData>> GetProductCategoryMasterGrid(GridVariables objGridVariables)
        {
            GridOutput<IEnumerable<ProductCategoryGridData>> output = new GridOutput<IEnumerable<ProductCategoryGridData>>();
            output.draw = objGridVariables.Draw;
            DbParameterCollection objDBParam = GetDBParamFromGrid(ref objGridVariables);
            try
            {
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetProductCategoryMasterGrid]", objDBParam, CommandType.StoredProcedure);

                if (outputFromSP.Rows.Count > 0)
                {
                    output.data = (from category in outputFromSP.AsEnumerable()
                                   select new ProductCategoryGridData
                                   {
                                       ID = Convert.ToInt64(category.Field<Int64>("ID")),
                                       DivisionName = Convert.ToString(category.Field<string>("DIVISION")),
                                       DivisionId = Convert.ToString(category.Field<string>("DivisionId")),
                                       Description = Convert.ToString(category.Field<string>("DESCRIPTION")),
                                       CategoryName = Convert.ToString(category.Field<string>("CATEGORYNAME")),
                                       ActiveStatus = Convert.ToString(category.Field<string>("STATUS"))
                                   });
                }
                else
                    output.data = new List<ProductCategoryGridData>();
                output.recordsFiltered = Convert.ToInt32(outputFromSP.Rows[0]["FilteredCount"]);
                output.recordsTotal = Convert.ToInt32(outputFromSP.Rows[0]["FilteredCount"]);
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetProductCategoryMasterGrid", ex.StackTrace, ex.Message);
            }
            return output;
        }

        public GridOutput<IEnumerable<ProductSubCategoryGridData>> GetProductSubCategoryMasterGrid(GridVariables objGridVariables)
        {
            GridOutput<IEnumerable<ProductSubCategoryGridData>> output = new GridOutput<IEnumerable<ProductSubCategoryGridData>>();
            output.draw = objGridVariables.Draw;
            DbParameterCollection objDBParam = GetDBParamFromGrid(ref objGridVariables);
            try
            {
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetProductSubCategoryMasterGrid]", objDBParam, CommandType.StoredProcedure);

                if (outputFromSP.Rows.Count > 0)
                {
                    output.data = (from subcategory in outputFromSP.AsEnumerable()
                                   select new ProductSubCategoryGridData
                                   {
                                       ID = Convert.ToInt64(subcategory.Field<Int64>("ID")),
                                       Division = Convert.ToString(subcategory.Field<string>("DIVISION")),
                                       ProductCategoryId = Convert.ToInt64(subcategory.Field<Int64>("CATEGORYID")),
                                       SubCategoryName = Convert.ToString(subcategory.Field<string>("SUBCATEGORYNAME")),
                                       Description = Convert.ToString(subcategory.Field<string>("DESCRIPTION")),
                                       CategoryName = Convert.ToString(subcategory.Field<string>("CATEGORYNAME")),
                                       ActiveStatus = Convert.ToString(subcategory.Field<string>("STATUS"))
                                   });
                }
                else
                    output.data = new List<ProductSubCategoryGridData>();
                output.recordsFiltered = Convert.ToInt32(outputFromSP.Rows[0]["FilteredCount"]);
                output.recordsTotal = Convert.ToInt32(outputFromSP.Rows[0]["FilteredCount"]);
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetProductSubCategoryMasterGrid", ex.StackTrace, ex.Message);
            }
            return output;
        }

        public GridOutput<IEnumerable<MaterialGridData>> GetMaterialMasterGrid(MaterialFilter objGridVariables)
        {
            GridOutput<IEnumerable<MaterialGridData>> output = new GridOutput<IEnumerable<MaterialGridData>>();
            output.draw = objGridVariables.draw;
            //DbParameterCollection objDBParam = GetDBParamFromGrid(ref objGridVariables);
            DataTable dtprocat = new DataTable();
            DataTable dtsubpro = new DataTable();
            DataTable dtmaterials = new DataTable();
            DataRow row;
            try
            {
                dtmaterials.Columns.Add("FilterId");
                if (objGridVariables.MaterialIds != null)
                {
                    foreach (Int64 materialid in objGridVariables.MaterialIds)
                    {
                        row = dtmaterials.NewRow();
                        row["FilterId"] = materialid;
                        dtmaterials.Rows.Add(row);
                    }
                }

                dtprocat.Columns.Add("FilterId");
                if (objGridVariables.ProductCatIds != null)
                {
                    foreach (Int64 procatid in objGridVariables.ProductCatIds)
                    {
                        row = dtprocat.NewRow();
                        row["FilterId"] = procatid;
                        dtprocat.Rows.Add(row);
                    }
                }

                dtsubpro.Columns.Add("FilterId");
                if (objGridVariables.SubProCatIds != null)
                {
                    foreach (Int64 subproid in objGridVariables.SubProCatIds)
                    {
                        row = dtsubpro.NewRow();
                        row["FilterId"] = subproid;
                        dtsubpro.Rows.Add(row);
                    }
                }

                SqlParameter[] objDBParam = new SqlParameter[9];
                objDBParam[0] = new SqlParameter("@ProductIds", dtprocat);
                objDBParam[1] = new SqlParameter("@SubProductIds", dtsubpro);
                objDBParam[2] = new SqlParameter("@MaterialIds", dtmaterials);
                objDBParam[3] = new SqlParameter("@Status", objGridVariables.Status);
                objDBParam[4] = new SqlParameter("@PageStart", objGridVariables.start);
                objDBParam[5] = new SqlParameter("@PageSize", objGridVariables.length);
                if (objGridVariables.order != null)
                    objDBParam[6] = new SqlParameter("@SortColumn", objGridVariables.order.FirstOrDefault().column);
                else
                    objDBParam[6] = new SqlParameter("@SortColumn", "");
                if (objGridVariables.order != null)
                    objDBParam[7] = new SqlParameter("@SortDirection", objGridVariables.order.FirstOrDefault().dir);
                else
                    objDBParam[7] = new SqlParameter("@SortDirection", "");

                if (objGridVariables.search != null)
                    objDBParam[8] = new SqlParameter("@SearchValue", objGridVariables.search.value);
                else
                    objDBParam[8] = new SqlParameter("@SearchValue", "");

                var outputFromSP = _dataHelper.ExecuteProcedure("[dbo].[GetMaterialMasterGrid]", objDBParam);

                if (outputFromSP.Rows.Count > 0)
                {
                    output.data = (from material in outputFromSP.AsEnumerable()
                                   select new MaterialGridData
                                   {
                                       Id = Convert.ToInt64(material.Field<Int64>("ID")),
                                       MaterialCode = Convert.ToString(material.Field<string>("MATERIALCODE")),
                                       Name = Convert.ToString(material.Field<string>("MATERIALNAME")),
                                       ProductSubCategory = Convert.ToString(material.Field<string>("SUBCATEGORYNAME")),
                                       MOP = Convert.ToString(material.Field<string>("MOP")),
                                       Description = Convert.ToString(material.Field<string>("DESCRIPTION")),
                                       //Division = Convert.ToString(material.Field<string>("DIVISION")),
                                       ProductCategory = Convert.ToString(material.Field<string>("CATEGORYNAME")),
                                       ActiveStatus = Convert.ToString(material.Field<string>("STATUS"))
                                   });
                }
                else
                    output.data = new List<MaterialGridData>();
                output.recordsFiltered = Convert.ToInt32(outputFromSP.Rows[0]["FilteredCount"]);
                output.recordsTotal = Convert.ToInt32(outputFromSP.Rows[0]["FilteredCount"]);
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetMaterialMasterGrid", ex.StackTrace, ex.Message);
            }
            return output;
        }

        public GridOutput<IEnumerable<DealerGridData>> GetDealerMasterGrid(DealerFilter objGridVariables)
        {
            GridOutput<IEnumerable<DealerGridData>> output = new GridOutput<IEnumerable<DealerGridData>>();
            output.draw = objGridVariables.draw;
            //DbParameterCollection objDBParam = GetDBParamFromGrid(ref objGridVariables);
            //DataTable dtdealer = new DataTable();
            DataTable dtcode = new DataTable();
            DataTable dtbranch = new DataTable();
            DataTable dtloc = new DataTable();
            DataRow row;
            try
            {
                //dtdealer.Columns.Add("FilterId");
                //if (objGridVariables.DealerIds != null)
                //{
                //    foreach (Int64 dealerid in objGridVariables.DealerIds)
                //    {
                //        row = dtdealer.NewRow();
                //        row["FilterId"] = dealerid;
                //        dtdealer.Rows.Add(row);
                //    }
                //}

                dtcode.Columns.Add("FilterName");
                if (objGridVariables.MasterCodeIds != null)
                {
                    foreach (string code in objGridVariables.MasterCodeIds)
                    {
                        row = dtcode.NewRow();
                        row["FilterName"] = code;
                        dtcode.Rows.Add(row);
                    }
                }

                dtbranch.Columns.Add("FilterId");
                if (objGridVariables.BranchIds != null)
                {
                    foreach (Int64 branchid in objGridVariables.BranchIds)
                    {
                        row = dtbranch.NewRow();
                        row["FilterId"] = branchid;
                        dtbranch.Rows.Add(row);
                    }
                }

                dtloc.Columns.Add("FilterId");
                if (objGridVariables.LocationIds != null)
                {
                    foreach (Int64 locid in objGridVariables.LocationIds)
                    {
                        row = dtloc.NewRow();
                        row["FilterId"] = locid;
                        dtloc.Rows.Add(row);
                    }
                }

                SqlParameter[] objDBParam = new SqlParameter[11];
                objDBParam[0] = new SqlParameter("@DealerName", objGridVariables.DealerName);
                objDBParam[1] = new SqlParameter("@MasterCodeIds", dtcode);
                objDBParam[2] = new SqlParameter("@BranchIds", dtbranch);
                objDBParam[3] = new SqlParameter("@LocationIds", dtloc);
                objDBParam[4] = new SqlParameter("@Status", objGridVariables.Status);
                objDBParam[5] = new SqlParameter("@PageStart", objGridVariables.start);
                objDBParam[6] = new SqlParameter("@PageSize", objGridVariables.length);
                if (objGridVariables.order != null)
                    objDBParam[7] = new SqlParameter("@SortColumn", objGridVariables.order.FirstOrDefault().column);
                else
                    objDBParam[7] = new SqlParameter("@SortColumn", 0);
                if (objGridVariables.order != null)
                    objDBParam[8] = new SqlParameter("@SortDirection", objGridVariables.order.FirstOrDefault().dir);
                else
                    objDBParam[8] = new SqlParameter("@SortDirection", "asc");

                if (objGridVariables.search != null)
                    objDBParam[9] = new SqlParameter("@SearchValue", objGridVariables.search.value);
                else
                    objDBParam[9] = new SqlParameter("@SearchValue", "");

                objDBParam[10] = new SqlParameter("@DealerCode", objGridVariables.DealerCode);
                


                var outputFromSP = _dataHelper.ExecuteProcedure("[dbo].[GetDealerMasterGrid]", objDBParam);

                if (outputFromSP.Rows.Count > 0)
                {
                    output.data = (from dealer in outputFromSP.AsEnumerable()
                                   select new DealerGridData
                                   {
                                       ID = Convert.ToInt64(dealer.Field<Int64>("ID")),
                                       DealerCode = Convert.ToString(dealer.Field<string>("DealerCode")),
                                       SAPCode = Convert.ToString(dealer.Field<string>("SAPCODE")),
                                       PayerName = Convert.ToString(dealer.Field<string>("PAYERNAME")),
                                       DealerName = Convert.ToString(dealer.Field<string>("DEALERNAME")),
                                       OutletType = Convert.ToString(dealer.Field<string>("OUTLETTYPE")),
                                       BranchName = Convert.ToString(dealer.Field<string>("BRANCHNAME")),
                                       LocationName = Convert.ToString(dealer.Field<string>("LOCATIONNAME")),
                                       ActiveStatus = Convert.ToString(dealer.Field<string>("ACTIVESTATUS")),
                                       OpeningDate = Convert.ToString(dealer.Field<string>("OpeningDate")),
                                       ClosingDate = Convert.ToString(dealer.Field<string>("ClosingDate")),
                                       ChannelName = Convert.ToString(dealer.Field<string>("ChannelName")),
                                       Address = Convert.ToString(dealer.Field<string>("Address")),
                                       MobileNumber = Convert.ToString(dealer.Field<string>("MobileNumber")),
                                       ContactPerson = Convert.ToString(dealer.Field<string>("ContactPerson")),
                                       EmailID = Convert.ToString(dealer.Field<string>("EmailId")),
                                       PSIOutlet1Data = Convert.ToString(dealer.Field<string>("PSIOutlet1")),
                                       PSIOutlet2Data = Convert.ToString(dealer.Field<string>("PSIOutlet2")),
                                       MasterCode1 = Convert.ToString(dealer.Field<string>("MasterCode1")),
                                       MasterCode2 = Convert.ToString(dealer.Field<string>("MasterCode2")),
                                       PayerCode = Convert.ToString(dealer.Field<string>("PayerCode")),
                                       StoreCode = Convert.ToString(dealer.Field<string>("StoreCode")),
                                       CustomerId = Convert.ToString(dealer.Field<string>("CustomerId")),
                                       Latitude = Convert.ToString(dealer.Field<string>("Latitude")),
                                       Longitude = Convert.ToString(dealer.Field<string>("Longitude")),
                                       TIN = Convert.ToString(dealer.Field<string>("TIN")),
                                       PAN = Convert.ToString(dealer.Field<string>("PAN")),
                                       CityName = Convert.ToString(dealer.Field<string>("City")),
                                       StateName = Convert.ToString(dealer.Field<string>("State")),

                                   });
                }
                else
                    output.data = new List<DealerGridData>();
                output.recordsFiltered = Convert.ToInt32(outputFromSP.Rows[0]["FilteredCount"]);
                output.recordsTotal = Convert.ToInt32(outputFromSP.Rows[0]["FilteredCount"]);
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetDealerMasterGrid", ex.StackTrace, ex.Message);
            }
            return output;
        }

        public GridOutput<IEnumerable<DealerSFAMappingGridData>> GetDealerSFAMappingGrid(DealerSFAFilter objGridVariables)
        {
            GridOutput<IEnumerable<DealerSFAMappingGridData>> output = new GridOutput<IEnumerable<DealerSFAMappingGridData>>();
            output.draw = objGridVariables.draw;
            //DbParameterCollection objDBParam = GetDBParamFromGrid(ref objGridVariables);

            DataTable dtdealer = new DataTable();
            //DataTable dtsfa = new DataTable();
            DataTable dtcity = new DataTable();
            DataTable dtstate = new DataTable();
            DataTable dtbranch = new DataTable();
            DataTable dtloc = new DataTable();
            DataRow row;
            try
            {
                dtdealer.Columns.Add("FilterId");
                if (objGridVariables.DealerIds != null)
                {
                    foreach (Int64 dealerid in objGridVariables.DealerIds)
                    {
                        row = dtdealer.NewRow();
                        row["FilterId"] = dealerid;
                        dtdealer.Rows.Add(row);
                    }
                }

                //dtsfa.Columns.Add("FilterId");
                //if (objGridVariables.SFAIds != null)
                //{
                //    foreach (Int64 sfaid in objGridVariables.SFAIds)
                //    {
                //        row = dtsfa.NewRow();
                //        row["FilterName"] = sfaid;
                //        dtsfa.Rows.Add(row);
                //    }
                //}

                dtcity.Columns.Add("FilterId");
                if (objGridVariables.CityIds != null)
                {
                    foreach (Int64 cityid in objGridVariables.CityIds)
                    {
                        row = dtcity.NewRow();
                        row["FilterId"] = cityid;
                        dtcity.Rows.Add(row);
                    }
                }

                dtstate.Columns.Add("FilterId");
                if (objGridVariables.StateIds != null)
                {
                    foreach (Int64 stateid in objGridVariables.StateIds)
                    {
                        row = dtstate.NewRow();
                        row["FilterId"] = stateid;
                        dtstate.Rows.Add(row);
                    }
                }

                dtbranch.Columns.Add("FilterId");
                if (objGridVariables.BranchIds != null)
                {
                    foreach (Int64 branchid in objGridVariables.BranchIds)
                    {
                        row = dtbranch.NewRow();
                        row["FilterId"] = branchid;
                        dtbranch.Rows.Add(row);
                    }
                }

                dtloc.Columns.Add("FilterId");
                if (objGridVariables.LocationIds != null)
                {
                    foreach (Int64 locid in objGridVariables.LocationIds)
                    {
                        row = dtloc.NewRow();
                        row["FilterId"] = locid;
                        dtloc.Rows.Add(row);
                    }
                }

                SqlParameter[] objDBParam = new SqlParameter[10];
                objDBParam[0] = new SqlParameter("@DealerIds", dtdealer);
                objDBParam[1] = new SqlParameter("@SFAIds", objGridVariables.SFAIds != null ? objGridVariables.SFAIds.Trim() : null);
                objDBParam[2] = new SqlParameter("@CityIds", dtcity);
                objDBParam[3] = new SqlParameter("@StateIds", dtstate);
                objDBParam[4] = new SqlParameter("@BranchIds", dtbranch);
                objDBParam[5] = new SqlParameter("@LocationIds", dtloc);
                objDBParam[6] = new SqlParameter("@PageStart", objGridVariables.start);
                objDBParam[7] = new SqlParameter("@PageSize", objGridVariables.length);
                if (objGridVariables.order != null)
                    objDBParam[8] = new SqlParameter("@SortColumn", objGridVariables.order.FirstOrDefault().column);
                else
                    objDBParam[8] = new SqlParameter("@SortColumn", 0);
                if (objGridVariables.order != null)
                    objDBParam[9] = new SqlParameter("@SortDirection", objGridVariables.order.FirstOrDefault().dir);
                else
                    objDBParam[9] = new SqlParameter("@SortDirection", "asc");

                //if (objGridVariables.search != null)
                //    objDBParam[9] = new SqlParameter("@SearchValue", objGridVariables.search.value);
                //else
                //    objDBParam[9] = new SqlParameter("@SearchValue", "");
                //objDBParam[10] = new SqlParameter("@SearchValue", objGridVariables.search.value);

                var outputFromSP = _dataHelper.ExecuteProcedure("[dbo].[GetDealerSFAMappingGrid]", objDBParam);

                if (outputFromSP.Rows.Count > 0)
                {
                    output.data = (from dealer in outputFromSP.AsEnumerable()
                                   select new DealerSFAMappingGridData
                                   {
                                       ID = Convert.ToInt64(dealer.Field<Int64>("ID")),
                                       BranchId = Convert.ToInt64(dealer.Field<Int64>("BRANCHID")),
                                       BranchName = Convert.ToString(dealer.Field<string>("BRANCHNAME")),
                                       StateId = Convert.ToInt64(dealer.Field<Int64>("STATEID")),
                                       StateName = Convert.ToString(dealer.Field<string>("STATENAME")),
                                       CityId = Convert.ToInt64(dealer.Field<Int64>("CITYID")),
                                       CityName = Convert.ToString(dealer.Field<string>("CITYNAME")),
                                       LocationId = Convert.ToInt64(dealer.Field<Int64>("LOCATIONID")),
                                       LocationName = Convert.ToString(dealer.Field<string>("LOCATIONNAME")),
                                       DealerId = Convert.ToInt64(dealer.Field<Int64>("DEALERID")),
                                       DealerName = Convert.ToString(dealer.Field<string>("DEALERNAME")),
                                       EmployeeId = Convert.ToInt64(dealer.Field<Int64>("EMPLOYEEID")),
                                       SFAName = Convert.ToString(dealer.Field<string>("SFANAME")),
                                   });
                }
                else
                    output.data = new List<DealerSFAMappingGridData>();
                output.recordsFiltered = Convert.ToInt32(outputFromSP.Rows[0]["FilteredCount"]);
                output.recordsTotal = Convert.ToInt32(outputFromSP.Rows[0]["FilteredCount"]);
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetDealerSFAMappingGrid", ex.StackTrace, ex.Message);
            }
            return output;
        }

        public GridOutput<IEnumerable<DealerClassificationMappingGridData>> GetDealerClassificationMappingGrid(GridVariables objGridVariables)
        {
            GridOutput<IEnumerable<DealerClassificationMappingGridData>> output = new GridOutput<IEnumerable<DealerClassificationMappingGridData>>();
            output.draw = objGridVariables.Draw;
            DbParameterCollection objDBParam = GetDBParamFromGrid(ref objGridVariables);
            try
            {
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetDealerClassificationMappingGrid]", objDBParam, CommandType.StoredProcedure);

                if (outputFromSP.Rows.Count > 0)
                {
                    output.data = (from mapping in outputFromSP.AsEnumerable()
                                   select new DealerClassificationMappingGridData
                                   {
                                       ID = Convert.ToInt64(mapping.Field<Int64>("ID")),
                                       ProductCategoryId = Convert.ToInt64(mapping.Field<Int64>("PRODCATID")),
                                       ProductCategoryName = Convert.ToString(mapping.Field<string>("PRODCATNAME")),
                                       BranchId = Convert.ToInt64(mapping.Field<Int64>("BRANCHID")),
                                       BranchName = Convert.ToString(mapping.Field<string>("BRANCHNAME")),
                                       DealerId = Convert.ToInt64(mapping.Field<Int64>("DEALERID")),
                                       DealerName = Convert.ToString(mapping.Field<string>("DEALERNAME")),
                                       ClassificationId = Convert.ToInt64(mapping.Field<Int64>("CLASSID")),
                                       ClassificationName = Convert.ToString(mapping.Field<string>("CLASSNAME"))
                                   });
                }
                else
                    output.data = new List<DealerClassificationMappingGridData>();
                output.recordsFiltered = Convert.ToInt32(outputFromSP.Rows[0]["FilteredCount"]);
                output.recordsTotal = Convert.ToInt32(outputFromSP.Rows[0]["FilteredCount"]);
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetDealerClassificationMappingGrid", ex.StackTrace, ex.Message);
            }
            return output;
        }

        public GridOutput<List<BroadcastMessageGridData>> GetBroadcastMessageMasterGrid(GridVariables objGridVariables)
        {
            GridOutput<List<BroadcastMessageGridData>> output = new GridOutput<List<BroadcastMessageGridData>>();
            output.draw = objGridVariables.Draw;
            DbParameterCollection objDBParam = GetDBParamFromGrid(ref objGridVariables);
            try
            {
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetBroadcastMessageMasterGrid]", objDBParam, CommandType.StoredProcedure);

                if (outputFromSP.Rows.Count > 0)
                {
                    output.data = (from messages in outputFromSP.AsEnumerable()
                                   select new BroadcastMessageGridData
                                   {
                                       Id = Convert.ToInt64(messages.Field<Int64>("ID")),
                                       Message = Convert.ToString(messages.Field<string>("MESSAGE")),
                                       StartDate = Convert.ToString(messages.Field<string>("STARTDATE")),
                                       EndDate = Convert.ToString(messages.Field<string>("ENDDATE")),
                                       Status = Convert.ToString(messages.Field<string>("STATUS")),
                                       Subject = Convert.ToString(messages.Field<string>("Subject")),
                                       FileName = Convert.ToString(messages.Field<string>("FileName")),
                                       FilePath = System.Web.Hosting.HostingEnvironment.MapPath("~/Uploads/Broadcast/") + Convert.ToString(messages.Field<string>("FileName"))
                                   }).ToList();
                }
                else
                    output.data = new List<BroadcastMessageGridData>();
                output.recordsFiltered = Convert.ToInt32(outputFromSP.Rows[0]["FilteredCount"]);
                output.recordsTotal = Convert.ToInt32(outputFromSP.Rows[0]["FilteredCount"]);
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetBroadcastMessageMasterGrid", ex.StackTrace, ex.Message);
            }
            return output;
        }



        public BroadcastMessageEDITData BroadcastMessageMasterGridBYID(BMessageInputModel objBroadcastMessageEDITData)
        {
            BroadcastMessageEDITData output = new BroadcastMessageEDITData();
            //  output.draw = objBroadcastMessageGridData.Draw;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@ID", objBroadcastMessageEDITData.Id, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[BroadcastMessageMasterGridSFABYID]", objDBParam, CommandType.StoredProcedure);

                if (outputFromSP.Rows.Count > 0)
                {
                    //output.data = (from messages in outputFromSP.AsEnumerable()
                    //               select new BroadcastMessageEDITData
                    //               {

                    //                   SFAIds=
                    //               });

                    var SData = (from message in outputFromSP.AsEnumerable()
                                  select new BMessageModel
                                  {
                                      SFAId = Convert.ToInt64(message.Field<Int64>("SFAIds")),
                                      BranchIds = Convert.ToInt64(message.Field<Int64>("BranchIds")),
                                      DivisionIds = Convert.ToInt64(message.Field<Int64>("DivisionIds")),
                                      SFANAME= Convert.ToString(message.Field<String>("SFANAME")),
                                      BRANCHNAME = Convert.ToString(message.Field<String>("BRANCHNAME")),
                                      DivisionName = Convert.ToString(message.Field<String>("DivisionName"))

                                  }).ToList();
                    output.SFAIds = (from SFA in SData.AsEnumerable()
                                          select SFA.SFAId).Distinct().ToList();
                    output.BranchIds = (from Branch in SData.AsEnumerable()
                                          select Branch.BranchIds).Distinct().ToList();
                    output.DivisionIds = (from DivisionId in SData.AsEnumerable()
                                          select DivisionId.DivisionIds).Distinct().ToList();


                    output.SFANAME = (from SFA in SData.AsEnumerable()
                                     select SFA.SFANAME).Distinct().ToList();
                    output.BRANCHNAME = (from Branch in SData.AsEnumerable()
                                        select Branch.BRANCHNAME).Distinct().ToList();
                    output.DivisionName = (from DivisionId in SData.AsEnumerable()
                                          select DivisionId.DivisionName).Distinct().ToList();
                }
                else
                    output = new BroadcastMessageEDITData();
                
                var outputFromSP1 = _dataHelper.ExecuteDataTable("[dbo].[BroadcastMessageMasterGridSIDBYID]", objDBParam, CommandType.StoredProcedure);

                if (outputFromSP1.Rows.Count > 0)
                {
                    //output.data = (from messages in outputFromSP.AsEnumerable()
                    //               select new BroadcastMessageEDITData
                    //               {

                    //                   SFAIds=
                    //               });

                    var SData = (from message in outputFromSP1.AsEnumerable()
                                 select new BMessageModel
                                 {
                                     SIDID = Convert.ToInt64(message.Field<Int64>("SIDID")),
                                     BranchIds = Convert.ToInt64(message.Field<Int64>("BranchIds")),
                                     DivisionIds = Convert.ToInt64(message.Field<Int64>("DivisionIds")),
                                     SIDNAME = Convert.ToString(message.Field<String>("SIDNAME")),
                                 }).ToList();
                    output.SIDID = (from SIDID in SData.AsEnumerable()
                                          select SIDID.SIDID).Distinct().ToList();
                    output.SIDNAME = (from SIDNAME in SData.AsEnumerable()
                                    select SIDNAME.SIDNAME).Distinct().ToList();
                }
                else
                {
                    if(output.SIDID.Count()==0 && output.SFAIds.Count==0)
                    {
                        output = new BroadcastMessageEDITData();
                    }

                }


                var outputFromSP2 = _dataHelper.ExecuteDataTable("[dbo].[BroadcastMessageMasterGridRolesBYID]", objDBParam, CommandType.StoredProcedure);
                if (outputFromSP2.Rows.Count > 0)
                {
                    var SData = (from message in outputFromSP2.AsEnumerable()
                                 select new BMessageModel
                                 {
                                     RoleIds = Convert.ToInt64(message.Field<Int64>("RoleIds")),
                                     RoleNames = Convert.ToString(message.Field<string>("RoleNames"))
                                 }).ToList();

                    output.RoleIds = (from roleid in SData.AsEnumerable()
                                    select roleid.RoleIds).Distinct().ToList();
                    output.RoleNames = (from rolename in SData.AsEnumerable()
                                      select rolename.RoleNames).Distinct().ToList();
                }

                }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("BroadcastMessageMasterGridBYID", ex.StackTrace, ex.Message);
                output = new BroadcastMessageEDITData();
            }
            return output;
        }
        /// <summary>
        /// To fetch all SFA Level Master Data.
        /// </summary>
        /// <returns>SFA Level Master Data.</returns>
        public IEnumerable<SFALevel> GetSFALevelMasterData(SFALevelFilter InputParam)
        {
            DataTable dtsfalevel = new DataTable();
            IEnumerable<SFALevel> output = null;
            DataRow row;
            try
            {
                dtsfalevel.Columns.Add("SFALevel");
                if (InputParam.SFALevelIds != null)
                {
                    foreach (Int64 sfalevel in InputParam.SFALevelIds)
                    {
                        row = dtsfalevel.NewRow();
                        row["SFALevel"] = sfalevel;
                        dtsfalevel.Rows.Add(row);
                    }
                }

                SqlParameter[] objDBParam = new SqlParameter[1];
                objDBParam[0] = new SqlParameter("@SFALevelIds", dtsfalevel);
                var outputFromSP = _dataHelper.ExecuteProcedure("[dbo].[GetSFALevelMasterData]", objDBParam);

                if (outputFromSP.Rows.Count > 0)
                {
                    output = (from data in outputFromSP.AsEnumerable()
                              select new SFALevel
                              {
                                  Id = Convert.ToInt64(data.Field<Int64>("Id")),
                                  SFALevelName = Convert.ToString(data.Field<string>("SFALevelName")),
                                  LevelCreationDate = Convert.ToString(data.Field<string>("CreationDate"))
                              }).ToList();
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetSFALevelMasterData", ex.StackTrace, ex.Message);
            }
            return output;
        }

        /// <summary>
        /// To fetch all Shift.
        /// </summary>
        /// <returns>SFA Level Master Data.</returns>
        public IEnumerable<ShiftMaster> GetShiftMasterData(ShiftFilter InputParam)
        {
            DataTable dtShift = new DataTable();
            IEnumerable<ShiftMaster> output = null;
            DataRow row;
            try
            {
                dtShift.Columns.Add("FilterId");
                if (InputParam.ShiftIds != null)
                {
                    foreach (Int64 shiftid in InputParam.ShiftIds)
                    {
                        row = dtShift.NewRow();
                        row["FilterId"] = shiftid;
                        dtShift.Rows.Add(row);
                    }
                }

                SqlParameter[] objDBParam = new SqlParameter[1];
                objDBParam[0] = new SqlParameter("@dtShift", dtShift);
                var outputFromSP = _dataHelper.ExecuteProcedure("[dbo].[GetShiftMasterData]", objDBParam);

                if (outputFromSP.Rows.Count > 0)
                {
                    output = (from shift in outputFromSP.AsEnumerable()
                              select new ShiftMaster
                              {
                                  Id = Convert.ToInt64(shift.Field<Int64>("ID")),
                                  ShiftName = Convert.ToString(shift.Field<string>("ShiftName")),
                                  IsActive = Convert.ToBoolean(shift.Field<bool>("ISACTIVE")),
                                  CreatedBy = Convert.ToInt64(shift.Field<Int64?>("CREATEDBY")),
                                  CreationDate = Convert.ToDateTime(shift.Field<DateTime>("CREATIONDATE")),
                                  LastModifiedBy = Convert.ToInt64(shift.Field<Int64?>("LASTMODIFIEDBY")),
                                  LastModificationDate = Convert.ToDateTime(shift.Field<DateTime?>("LASTMODDATE"))
                              }).ToList();
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetShiftMasterData", ex.StackTrace, ex.Message);
            }
            return output;
        }

        /// <summary>
        /// To fetch SFA Level master data by Id.
        /// </summary>
        /// <param name="Input">SFA Level Id</param>
        /// <returns>SFA Level master data by Id.</returns>
        public SFALevel GetSFALevelMasterDataById(SFALevelInput Input)
        {
            SFALevel Data = new SFALevel();
            try
            {
                DbParameterCollection objDBParam = new DbParameterCollection();
                objDBParam.Add(new DbParameter("@SFALevelId", Input.SFALevelId, DbType.Int64));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetSFALevelMasterDataById]", objDBParam, CommandType.StoredProcedure);

                if (outputFromSP.Rows.Count > 0)
                {
                    Data = (from data in outputFromSP.AsEnumerable()
                            select new SFALevel
                            {
                                Id = Convert.ToInt64(data.Field<Int64>("Id")),
                                SFALevelName = Convert.ToString(data.Field<string>("SFALevelName")),
                                LevelCreationDate = Convert.ToString(data.Field<string>("CreationDate"))
                            }).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetSFALevelMasterDataById", ex.StackTrace, ex.Message);
            }
            return Data;
        }

        public GridOutput<IEnumerable<AssetGridData>> GetAssetMasterGrid(GridVariables objGridVariables)
        {
            GridOutput<IEnumerable<AssetGridData>> output = new GridOutput<IEnumerable<AssetGridData>>();
            output.draw = objGridVariables.Draw;
            DbParameterCollection objDBParam = GetDBParamFromGrid(ref objGridVariables);
            try
            {
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetAssetMasterGrid]", objDBParam, CommandType.StoredProcedure);

                if (outputFromSP.Rows.Count > 0)
                {
                    output.data = (from asset in outputFromSP.AsEnumerable()
                                   select new AssetGridData
                                   {
                                       Id = Convert.ToInt64(asset.Field<Int64>("ID")),
                                       Category = Convert.ToString(asset.Field<string>("CATEGORY")),
                                       ProductCode = Convert.ToString(asset.Field<string>("PRODUCTCODE")),
                                       ProductName = Convert.ToString(asset.Field<string>("PRODUCTNAME")),
                                       Type = Convert.ToString(asset.Field<string>("TYPE")),
                                       ActiveStatus = Convert.ToString(asset.Field<string>("STATUS")),
                                   });
                }
                else
                    output.data = new List<AssetGridData>();

                output.recordsFiltered = Convert.ToInt32(outputFromSP.Rows[0]["FilteredCount"]);
                output.recordsTotal = Convert.ToInt32(outputFromSP.Rows[0]["FilteredCount"]);
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetAssetMasterGrid", ex.StackTrace, ex.Message);
            }
            return output;
        }

        public GridOutput<IEnumerable<ProductTargetCategoryGridData>> GetProductTargetCategoryMasterGrid(ProductTargetCategoryGridFilters objGridVariables)
        {
            GridOutput<IEnumerable<ProductTargetCategoryGridData>> output = new GridOutput<IEnumerable<ProductTargetCategoryGridData>>();
            output.draw = objGridVariables.draw;
            SqlParameter[] objDBParam = new SqlParameter[6];
            DataTable dtProdCat = new DataTable();
            DataRow dr;
            try
            {
                dtProdCat.Columns.Add("FilterId");
                if (objGridVariables.ProductCategoryIds != null)
                    foreach (Int64 ProductCategoryId in objGridVariables.ProductCategoryIds)
                    {
                        dr = dtProdCat.NewRow();
                        dr["FilterId"] = ProductCategoryId;
                        dtProdCat.Rows.Add(dr);
                    }

                objDBParam[0] = new SqlParameter("@PageStart", objGridVariables.start);
                objDBParam[1] = new SqlParameter("@PageSize", objGridVariables.length);
                if (objGridVariables.order != null)
                    objDBParam[2] = new SqlParameter("@SortColumn", objGridVariables.order.FirstOrDefault().column);
                else
                    objDBParam[2] = new SqlParameter("@SortColumn", null);
                if (objGridVariables.order != null)
                    objDBParam[3] = new SqlParameter("@SortDirection", objGridVariables.order.FirstOrDefault().dir);
                else
                    objDBParam[3] = new SqlParameter("@SortDirection", "asc");

                objDBParam[4] = new SqlParameter("@ProdCatIds", dtProdCat);
                objDBParam[5] = new SqlParameter("@TargetCategory", objGridVariables.TargetCategory);

                var outputFromSP = _dataHelper.ExecuteProcedure("[dbo].[GetProductTargetCategoryMasterGrid]", objDBParam);

                if (outputFromSP.Rows.Count > 0)
                {
                    output.data = (from targetcategory in outputFromSP.AsEnumerable()
                                   select new ProductTargetCategoryGridData
                                   {
                                       Id = Convert.ToInt64(targetcategory.Field<Int64>("ID")),
                                       TargetCategory = Convert.ToString(targetcategory.Field<string>("TARGETCATEGORY")),
                                       ProductCategoryId = Convert.ToInt64(targetcategory.Field<Int64>("PRODUCTCATEGORYID")),
                                       ProductCategory = Convert.ToString(targetcategory.Field<string>("PRODUCTCATEGORY")),
                                       ActiveStatus = Convert.ToString(targetcategory.Field<string>("ACTIVESTATUS"))
                                   });
                }
                else
                    output.data = new List<ProductTargetCategoryGridData>();
                output.recordsFiltered = Convert.ToInt32(outputFromSP.Rows[0]["FilteredCount"]);
                output.recordsTotal = Convert.ToInt32(outputFromSP.Rows[0]["FilteredCount"]);
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetProductTargetCategoryMasterGrid", ex.StackTrace, ex.Message);
            }
            return output;
        }

        /// <summary>
        /// To fetch all SFA Sub Level Master Data.
        /// </summary>
        /// <returns>SFA Sub Level Master Data.</returns>
        public IEnumerable<SFASubLevel> GetSFASubLevelMasterData(SFASubLevelFilter InputParam)
        {
            DataTable dtsfalevel = new DataTable();
            DataTable dtsfasublevel = new DataTable();
            IEnumerable<SFASubLevel> output = null;
            DataRow row;
           
            try
            {
                dtsfalevel.Columns.Add("SFALevel");
                if (InputParam.SFALevelIds != null)
                {
                    foreach (Int64 sfalevel in InputParam.SFALevelIds)
                    {
                        row = dtsfalevel.NewRow();
                        row["SFALevel"] = sfalevel;
                        dtsfalevel.Rows.Add(row);
                    }
                }

                dtsfasublevel.Columns.Add("SFASubLevel");
                if (InputParam.SFASubLevelIds != null)
                {
                    foreach (Int64 sfasublevel in InputParam.SFASubLevelIds)
                    {
                        row = dtsfasublevel.NewRow();
                        row["SFASubLevel"] = sfasublevel;
                        dtsfasublevel.Rows.Add(row);
                    }
                }

                SqlParameter[] objDBParam = new SqlParameter[2];
                objDBParam[0] = new SqlParameter("@SFALevelIds", dtsfalevel);
                objDBParam[1] = new SqlParameter("@SFASubLevelIds", dtsfasublevel);

                var outputFromSP = _dataHelper.ExecuteProcedure("[dbo].[GetSFASubLevelData]", objDBParam);

                if (outputFromSP.Rows.Count > 0)
                {
                    output = (from data in outputFromSP.AsEnumerable()
                              select new SFASubLevel
                              {
                                  Id = Convert.ToInt64(data.Field<Int64>("Id")),
                                  SFALevelId = Convert.ToInt64(data.Field<Int64>("SFALevelId")),
                                  SFALevelName = Convert.ToString(data.Field<string>("SFALevelName")),
                                  SFASubLevelName = Convert.ToString(data.Field<string>("SFASubLevelName")),
                                  Description = Convert.ToString(data.Field<string>("Description")),
                                  LevelCreationDate = Convert.ToString(data.Field<string>("CreationDate"))
                              }).ToList();
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetSFASubLevelMasterData", ex.StackTrace, ex.Message);
            }
            return output;
        }

        /// <summary>
        /// To fetch SFA Sub Level master data by Id.
        /// </summary>
        /// <param name="Input">SFA Sub Level Id</param>
        /// <returns>SFA Sub Level master data by Id.</returns>
        public SFASubLevel GetSFASubLevelMasterDataById(SFASubLevelInput Input)
        {
            SFASubLevel Data = new SFASubLevel();
            try
            {
                DbParameterCollection objDBParam = new DbParameterCollection();
                objDBParam.Add(new DbParameter("@SFALevelId", Input.SFASubLevelId, DbType.Int64));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetSFASubLevelMasterDataById]", objDBParam, CommandType.StoredProcedure);

                if (outputFromSP.Rows.Count > 0)
                {
                    Data = (from data in outputFromSP.AsEnumerable()
                            select new SFASubLevel
                            {
                                Id = Convert.ToInt64(data.Field<Int64>("Id")),
                                SFALevelName = Convert.ToString(data.Field<string>("SFALevelName")),
                                SFASubLevelName = Convert.ToString(data.Field<string>("SFASubLevelName")),
                                Description = Convert.ToString(data.Field<string>("Description")),
                                LevelCreationDate = Convert.ToString(data.Field<string>("CreationDate"))
                            }).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetSFASubLevelMasterDataById", ex.StackTrace, ex.Message);
            }
            return Data;
        }

        public GridOutput<IEnumerable<EmployeeGridData>> GetEmployeeMasterGrid(EmployeeGridSearchFilters objGridVariables)
        {
            GridOutput<IEnumerable<EmployeeGridData>> output = new GridOutput<IEnumerable<EmployeeGridData>>();
            output.draw = objGridVariables.draw;
            SqlParameter[] objDBParam = new SqlParameter[10];
            DataTable dtBranch = new DataTable();
            DataTable dtState = new DataTable();
            DataTable dtCity = new DataTable();
            DataTable dtAgency = new DataTable();
            DataRow dr;
            try
            {
                dtBranch.Columns.Add("FilterId");
                dtState.Columns.Add("FilterId");
                dtCity.Columns.Add("FilterId");
                dtAgency.Columns.Add("FilterId");

                if (objGridVariables.BranchIds != null)
                    foreach (Int64 BranchId in objGridVariables.BranchIds)
                    {
                        dr = dtBranch.NewRow();
                        dr["FilterId"] = BranchId;
                        dtBranch.Rows.Add(dr);
                    }

                if (objGridVariables.StateIds != null)
                    foreach (Int64 StateId in objGridVariables.StateIds)
                    {
                        dr = dtState.NewRow();
                        dr["FilterId"] = StateId;
                        dtState.Rows.Add(dr);
                    }

                if (objGridVariables.CityIds != null)
                    foreach (Int64 CityId in objGridVariables.CityIds)
                    {
                        dr = dtCity.NewRow();
                        dr["FilterId"] = CityId;
                        dtCity.Rows.Add(dr);
                    }

                objDBParam[0] = new SqlParameter("@PageStart", objGridVariables.start);
                objDBParam[1] = new SqlParameter("@PageSize", objGridVariables.length);
                if (objGridVariables.order != null)
                    objDBParam[2] = new SqlParameter("@SortColumn", objGridVariables.order.FirstOrDefault().column);
                else
                    objDBParam[2] = new SqlParameter("@SortColumn", 0);
                if (objGridVariables.order != null)
                    objDBParam[3] = new SqlParameter("@SortDirection", objGridVariables.order.FirstOrDefault().dir);
                else
                    objDBParam[3] = new SqlParameter("@SortDirection", "asc");
                objDBParam[4] = new SqlParameter("@BranchIds", dtBranch);
                objDBParam[5] = new SqlParameter("@StateIds", dtState);
                objDBParam[6] = new SqlParameter("@CityIds", dtCity);
                objDBParam[7] = new SqlParameter("@Status", objGridVariables.Status);
                objDBParam[8] = new SqlParameter("@LoginId", objGridVariables.LoginId);
                objDBParam[9] = new SqlParameter("@EmployeeName", objGridVariables.EmployeeName);

                var outputFromSP = _dataHelper.ExecuteProcedure("[dbo].[GetEmployeeMasterGrid]", objDBParam);

                if (outputFromSP.Rows.Count > 0)
                {
                    output.data = (from employee in outputFromSP.AsEnumerable()
                                   select new EmployeeGridData
                                   {
                                       EmployeeId = Convert.ToInt64(employee.Field<Int64>("ID")),
                                       FirstName = Convert.ToString(employee.Field<string>("FIRSTNAME")),
                                       LastName = Convert.ToString(employee.Field<string>("LASTNAME")),
                                       LoginId = Convert.ToString(employee.Field<string>("LOGINID")),
                                       BranchName = Convert.ToString(employee.Field<string>("BRANCHNAME")),
                                       Role = Convert.ToString(employee.Field<string>("ROLE")),
                                       StatusName = Convert.ToString(employee.Field<string>("STATUS")),
                                       StateName = Convert.ToString(employee.Field<string>("StateName")),
                                       CityName = Convert.ToString(employee.Field<string>("CityName")),
                                       AgencyName = Convert.ToString(employee.Field<string>("AgencyName")),
                                       EmployeeCode = Convert.ToString(employee.Field<string>("Code")),
                                       RegionName = Convert.ToString(employee.Field<string>("RegionName")),
                                       Division = Convert.ToString(employee.Field<string>("DivisionName")),
                                       Address = Convert.ToString(employee.Field<string>("Address")) == "" ?
                                               "" : AMBOEcryption.DecryptData(employee.Field<string>("Address"), true),
                                       MobileNumber = Convert.ToString(employee.Field<string>("MobileNumber")),
                                       AltMobileNumber = Convert.ToString(employee.Field<string>("AlternateNumber")),
                                       PANNo = Convert.ToString(employee.Field<string>("PAN")) == "" ?
                                               "" : AMBOEcryption.DecryptData(employee.Field<string>("PAN"), true),
                                       DOB = Convert.ToString(employee.Field<string>("DOB")) == "" ?
                                               "" : AMBOEcryption.DecryptData(employee.Field<string>("DOB"), true),
                                       DOJ = Convert.ToString(employee.Field<string>("DOJ")),
                                       DOL = Convert.ToString(employee.Field<string>("DOL")),
                                       SFALevelName = Convert.ToString(employee.Field<string>("SFASubLevelName")),
                                       SFAType = Convert.ToString(employee.Field<string>("Type")),
                                   });
                }
                else
                    output.data = new List<EmployeeGridData>();
                output.recordsFiltered = Convert.ToInt32(outputFromSP.Rows[0]["FilteredCount"]);
                output.recordsTotal = Convert.ToInt32(outputFromSP.Rows[0]["FilteredCount"]);
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetEmployeeMasterGrid", ex.StackTrace, ex.Message);
            }
            return output;
        }

        public GridOutput<IEnumerable<EmployeeStructureMappingGridData>> GetEmployeeStructureMappingGrid(EmployeeStructureMapGridFilters objGridVariables)
        {
            GridOutput<IEnumerable<EmployeeStructureMappingGridData>> output = new GridOutput<IEnumerable<EmployeeStructureMappingGridData>>();
            output.draw = objGridVariables.draw;
            SqlParameter[] objDBParam = new SqlParameter[6];
            DataTable dtBranch = new DataTable();
            DataRow dr;
            try
            {
                dtBranch.Columns.Add("FilterId");
                if (objGridVariables.BranchIds != null)
                    foreach (Int64 BranchId in objGridVariables.BranchIds)
                    {
                        dr = dtBranch.NewRow();
                        dr["FilterId"] = BranchId;
                        dtBranch.Rows.Add(dr);
                    }

                objDBParam[0] = new SqlParameter("@PageStart", objGridVariables.start);
                objDBParam[1] = new SqlParameter("@PageSize", objGridVariables.length);
                objDBParam[2] = new SqlParameter("@SortColumn", objGridVariables.order == null ? 0 : objGridVariables.order[0].column);
                objDBParam[3] = new SqlParameter("@SortDirection", objGridVariables.order == null ? "asc" : objGridVariables.order[0].dir);
                objDBParam[4] = new SqlParameter("@BranchIds", dtBranch);
                objDBParam[5] = new SqlParameter("@RDIName", objGridVariables.RDIName);

                var outputFromSP = _dataHelper.ExecuteProcedure("[dbo].[GetEmployeeStructureMappingGrid]", objDBParam);

                if (outputFromSP.Rows.Count > 0)
                {
                    output.data = (from employee in outputFromSP.AsEnumerable()
                                   select new EmployeeStructureMappingGridData
                                   {
                                       ID = Convert.ToInt64(employee.Field<Int64>("ID")),
                                       RDIId = Convert.ToInt64(employee.Field<Int64>("RDIID")),
                                       SFAId = Convert.ToInt64(employee.Field<Int64>("SFAID")),
                                       RDIBranchName = Convert.ToString(employee.Field<string>("RDIBRANCHNAME")),
                                       RDIName = Convert.ToString(employee.Field<string>("RDINAME")),
                                       SFAName = Convert.ToString(employee.Field<string>("SFANAME"))
                                   });
                }
                else
                    output.data = new List<EmployeeStructureMappingGridData>();
                output.recordsFiltered = Convert.ToInt32(outputFromSP.Rows[0]["FilteredCount"]);
                output.recordsTotal = Convert.ToInt32(outputFromSP.Rows[0]["FilteredCount"]);
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetEmployeeStructureMappingGrid", ex.StackTrace, ex.Message);
            }
            return output;
        }

        public GridOutput<IEnumerable<SalesPICOutletMappingGridData>> GetSalesPICOutletMappingGrid(GridVariables objGridVariables)
        {
            GridOutput<IEnumerable<SalesPICOutletMappingGridData>> output = new GridOutput<IEnumerable<SalesPICOutletMappingGridData>>();
            output.draw = objGridVariables.Draw;
            DbParameterCollection objDBParam = GetDBParamFromGrid(ref objGridVariables);
            try
            {
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetSalesPICOutletMappingGrid]", objDBParam, CommandType.StoredProcedure);

                if (outputFromSP.Rows.Count > 0)
                {
                    output.data = (from employee in outputFromSP.AsEnumerable()
                                   select new SalesPICOutletMappingGridData
                                   {
                                       SalesPICId = Convert.ToInt64(employee.Field<Int64>("SALESPICID")),
                                       SalesPICName = Convert.ToString(employee.Field<string>("SALESPICNAME")),
                                       BranchName = Convert.ToString(employee.Field<string>("BRANCHNAME")),
                                       BranchId = Convert.ToInt64(employee.Field<Int64>("BRANCHID"))                                       
                                   });
                }
                else
                    output.data = new List<SalesPICOutletMappingGridData>();
                output.recordsFiltered = Convert.ToInt32(outputFromSP.Rows[0]["FilteredCount"]);
                output.recordsTotal = Convert.ToInt32(outputFromSP.Rows[0]["FilteredCount"]);
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetSalesPICOutletMappingGrid", ex.StackTrace, ex.Message);
            }
            return output;
        }

        public GridOutput<IEnumerable<UserBranchChannelPCMappingGridData>> GetUserBranchChannelPCMappingGrid(UserBranchChannelPCMappingFilter objGridVariables)
        {
            GridOutput<IEnumerable<UserBranchChannelPCMappingGridData>> output = new GridOutput<IEnumerable<UserBranchChannelPCMappingGridData>>();
            output.draw = objGridVariables.draw;
            SqlParameter[] objDBParam = new SqlParameter[7];
            DataTable dtBranch = new DataTable();
            DataRow dr;
            try
            {
                dtBranch.Columns.Add("FilterId");
                if (objGridVariables.BranchIds != null)
                    foreach (Int64 BranchId in objGridVariables.BranchIds)
                    {
                        dr = dtBranch.NewRow();
                        dr["FilterId"] = BranchId;
                        dtBranch.Rows.Add(dr);
                    }

                objDBParam[0] = new SqlParameter("@PageStart", objGridVariables.start);
                objDBParam[1] = new SqlParameter("@PageSize", objGridVariables.length);
                objDBParam[2] = new SqlParameter("@SortColumn", objGridVariables.order == null ? 0 : objGridVariables.order[0].column);
                objDBParam[3] = new SqlParameter("@SortDirection", objGridVariables.order == null ? "asc" : objGridVariables.order[0].dir);
                objDBParam[4] = new SqlParameter("@BranchIds", dtBranch);
                objDBParam[5] = new SqlParameter("@LoginId", objGridVariables.LoginId);
                objDBParam[6] = new SqlParameter("@EmployeeName", objGridVariables.EmployeeName);

                var outputFromSP = _dataHelper.ExecuteProcedure("[dbo].[GetUserBranchChannelPCMappingGrid]", objDBParam);

                if (outputFromSP.Rows.Count > 0)
                {
                    output.data = (from employee in outputFromSP.AsEnumerable()
                                   select new UserBranchChannelPCMappingGridData
                                   {
                                       //ID = Convert.ToInt64(employee.Field<Int64>("ID")),
                                       UserIdForMapping = Convert.ToInt64(employee.Field<Int64>("MAPPEDUSERID")),
                                       UserFullName = Convert.ToString(employee.Field<string>("USERFULLNAME")),
                                       RoleId = Convert.ToInt64(employee.Field<Int64>("ROLEID")),
                                       UserRoleName = Convert.ToString(employee.Field<string>("USERROLENAME"))
                                   });
                }
                else
                    output.data = new List<UserBranchChannelPCMappingGridData>();
                output.recordsFiltered = Convert.ToInt32(outputFromSP.Rows[0]["FilteredCount"]);
                output.recordsTotal = Convert.ToInt32(outputFromSP.Rows[0]["FilteredCount"]);
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetUserBranchChannelPCMappingGrid", ex.StackTrace, ex.Message);
            }
            return output;          
            
        }

        /// <summary>
        /// to get sfa master for hr grid details from User, Employee and Employees for HR table
        /// </summary>
        /// <param name="objGridVariables"></param>
        /// <returns></returns>
        public GridOutput<IEnumerable<SFAMasterforHRGridData>> GetSFAMasterforHRGrid(SFAMasterforHRFilter objGridVariables)
        {
            GridOutput<IEnumerable<SFAMasterforHRGridData>> output = new GridOutput<IEnumerable<SFAMasterforHRGridData>>();
            output.draw = objGridVariables.draw;
            //DbParameterCollection objDBParam = GetDBParamFromGrid(ref objGridVariables);

            //DataTable dtlogin = new DataTable();
            //DataTable dtemployee = new DataTable();
            DataTable dtcity = new DataTable();
            DataTable dtstate = new DataTable();
            DataTable dtbranch = new DataTable();
            DataRow row;
            try
            {

                //dtlogin.Columns.Add("FilterId");
                //if (objGridVariables.LoginIds != null)
                //{
                //    foreach (Int64 loginid in objGridVariables.LoginIds)
                //    {
                //        row = dtlogin.NewRow();
                //        row["FilterId"] = loginid;
                //        dtlogin.Rows.Add(row);
                //    }
                //}

                //dtemployee.Columns.Add("FilterId");
                //if (objGridVariables.EmployeeIds != null)
                //{
                //    foreach (Int64 sfaid in objGridVariables.EmployeeIds)
                //    {
                //        row = dtemployee.NewRow();
                //        row["FilterName"] = sfaid;
                //        dtemployee.Rows.Add(row);
                //    }
                //}

                dtcity.Columns.Add("FilterId");
                if (objGridVariables.CityIds != null)
                {
                    foreach (Int64 cityid in objGridVariables.CityIds)
                    {
                        row = dtcity.NewRow();
                        row["FilterId"] = cityid;
                        dtcity.Rows.Add(row);
                    }
                }

                dtstate.Columns.Add("FilterId");
                if (objGridVariables.StateIds != null)
                {
                    foreach (Int64 stateid in objGridVariables.StateIds)
                    {
                        row = dtstate.NewRow();
                        row["FilterId"] = stateid;
                        dtstate.Rows.Add(row);
                    }
                }

                dtbranch.Columns.Add("FilterId");
                if (objGridVariables.BranchIds != null)
                {
                    foreach (Int64 branchid in objGridVariables.BranchIds)
                    {
                        row = dtbranch.NewRow();
                        row["FilterId"] = branchid;
                        dtbranch.Rows.Add(row);
                    }
                }

                

                SqlParameter[] objDBParam = new SqlParameter[12];
                objDBParam[0] = new SqlParameter("@LoginIds", objGridVariables.LoginIds != null ? objGridVariables.LoginIds.Trim() : null);
                objDBParam[1] = new SqlParameter("@EmployeeIds", objGridVariables.EmployeeIds != null ? objGridVariables.EmployeeIds.Trim() : null);
                objDBParam[2] = new SqlParameter("@CityIds", dtcity);
                objDBParam[3] = new SqlParameter("@StateIds", dtstate);
                objDBParam[4] = new SqlParameter("@BranchIds", dtbranch);
                objDBParam[5] = new SqlParameter("@PageStart", objGridVariables.start);
                objDBParam[6] = new SqlParameter("@PageSize", objGridVariables.length);
                if (objGridVariables.order != null)
                    objDBParam[7] = new SqlParameter("@SortColumn", objGridVariables.order.FirstOrDefault().column);
                else
                    objDBParam[7] = new SqlParameter("@SortColumn", 0);
                if (objGridVariables.order != null)
                    objDBParam[8] = new SqlParameter("@SortDirection", objGridVariables.order.FirstOrDefault().dir);
                else
                    objDBParam[8] = new SqlParameter("@SortDirection", "asc");
                if (objGridVariables.search != null)
                    objDBParam[9] = new SqlParameter("@SearchValue", objGridVariables.search.value);
                else
                    objDBParam[9] = new SqlParameter("@SearchValue", "");
                objDBParam[10] = new SqlParameter("@Status", objGridVariables.Status);
                objDBParam[11] = new SqlParameter("@SFATypeId", objGridVariables.SFATypeId);

                _IErrorLogProvider.CreateErrorLog("GetSFAMasterforHRGrid", "before calling procedure", "data is ready to insert");

                var outputFromSP = _dataHelper.ExecuteProcedure("[dbo].[GetSFAMasterforHRGrid]", objDBParam);

                if (outputFromSP.Rows.Count > 0)
                {
                    _IErrorLogProvider.CreateErrorLog("GetSFAMasterforHRGrid", "after procedure is called", "data inserted");
                    output.data = (from sfamasterforhr in outputFromSP.AsEnumerable()
                                   select new SFAMasterforHRGridData
                                   {
                                       Id = Convert.ToInt64(sfamasterforhr.Field<Int64>("ID")),
                                       SFAName = Convert.ToString(sfamasterforhr.Field<string>("SFANAME")),                                      
                                       SFACode = Convert.ToString(sfamasterforhr.Field<string>("SFACode")),
                                       Branch = Convert.ToString(sfamasterforhr.Field<string>("BRANCH")),
                                       Region = Convert.ToString(sfamasterforhr.Field<string>("Region")),
                                       State = Convert.ToString(sfamasterforhr.Field<string>("STATE")),
                                       City = Convert.ToString(sfamasterforhr.Field<string>("CITY")),
                                       Gender = Convert.ToString(sfamasterforhr.Field<string>("GENDER")) == "" ?
                                                "" : AMBOEcryption.DecryptData(sfamasterforhr.Field<string>("GENDER"), true),
                                       SourceName = Convert.ToString(sfamasterforhr.Field<string>("SOURCENAME")) == "" ?
                                                    "" : AMBOEcryption.DecryptData(sfamasterforhr.Field<string>("SOURCENAME"), true),
                                       AgencyRef = Convert.ToString(sfamasterforhr.Field<string>("AgencyRef")) == "" ?
                                                    "" : AMBOEcryption.DecryptData(sfamasterforhr.Field<string>("AgencyRef"), true),
                                       SFAType = Convert.ToString(sfamasterforhr.Field<string>("SFAType")),
                                       DOL = Convert.ToString(sfamasterforhr.Field<string>("DOL")),
                                       DOJ = Convert.ToString(sfamasterforhr.Field<string>("DOJ")),
                                       DOB = Convert.ToString(sfamasterforhr.Field<string>("DOB")) == "" ?
                                                   "" : AMBOEcryption.DecryptData(sfamasterforhr.Field<string>("DOB"), true),
                                       FatherName = Convert.ToString(sfamasterforhr.Field<string>("FatherName")) == "" ?
                                                   "" : AMBOEcryption.DecryptData(sfamasterforhr.Field<string>("FatherName"), true),
                                       LevelofEducation = Convert.ToString(sfamasterforhr.Field<string>("LevelofEducation")) == "" ?
                                                   "" : AMBOEcryption.DecryptData(sfamasterforhr.Field<string>("LevelofEducation"), true),
                                       Education = Convert.ToString(sfamasterforhr.Field<string>("EDUCATION")) == "" ?
                                                   "" : AMBOEcryption.DecryptData(sfamasterforhr.Field<string>("EDUCATION"), true),
                                       Experience = Convert.ToString(sfamasterforhr.Field<string>("EXPERIENCE")) == "" ?
                                                    "" : AMBOEcryption.DecryptData(sfamasterforhr.Field<string>("EXPERIENCE"), true),
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
                                       Address = Convert.ToString(sfamasterforhr.Field<string>("Address")) == "" ?
                                                   "" : AMBOEcryption.DecryptData(sfamasterforhr.Field<string>("Address"), true),
                                       EmailId = Convert.ToString(sfamasterforhr.Field<string>("Email")) == "" ?
                                                   "" : AMBOEcryption.DecryptData(sfamasterforhr.Field<string>("Email"), true),
                                       SourceCode = Convert.ToString(sfamasterforhr.Field<string>("SourceCode")) == "" ?
                                                    "" : AMBOEcryption.DecryptData(sfamasterforhr.Field<string>("SourceCode"), true),
                                       MobileNumber = Convert.ToString(sfamasterforhr.Field<string>("MobileNumber")),
                                       CityType = Convert.ToString(sfamasterforhr.Field<string>("CityType")),
                                       DealerName = Convert.ToString(sfamasterforhr.Field<string>("DealerName")),
                                       DealerCode = Convert.ToString(sfamasterforhr.Field<string>("DealerCode")),
                                       Channel = Convert.ToString(sfamasterforhr.Field<string>("ChannelName")),
                                       Division = Convert.ToString(sfamasterforhr.Field<string>("DivisionName")),
                                       Agency = Convert.ToString(sfamasterforhr.Field<string>("AgencyName")),
                                       SFASubLevel = Convert.ToString(sfamasterforhr.Field<string>("SFASubLevel")),
                                       SFALevel = Convert.ToString(sfamasterforhr.Field<string>("SFALevel")),
                                       Location = Convert.ToString(sfamasterforhr.Field<string>("LocationName")),
                                   });

                    _IErrorLogProvider.CreateErrorLog("GetSFAMasterforHRGrid", "after procedure is called", "data converted successfully");
                }
                else
                    output.data = new List<SFAMasterforHRGridData>();
                output.recordsFiltered = Convert.ToInt32(outputFromSP.Rows[0]["FilteredCount"]);
                output.recordsTotal = Convert.ToInt32(outputFromSP.Rows[0]["FilteredCount"]);
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetSFAMasterforHRGrid", ex.StackTrace, ex.Message);
            }
            return output;
        }

        /// <summary>
        /// To get Competitor Master data.
        /// </summary>
        /// <returns>Competitor Master data.</returns>
        public IEnumerable<CompetitorMasterData> GetCompetitorMasterGrid(CompetitorFilter InputParam)
        {
            DataTable dtcompetitorcodes = new DataTable();
            DataTable dtcompetitornames = new DataTable();
            IEnumerable<CompetitorMasterData> output = null;
            DataRow row;
            try
            {
                dtcompetitorcodes.Columns.Add("CompetitorCode");
                if (InputParam.CompetitorCodeIds!=null)
                {
                    foreach (Int64 competitorcode in InputParam.CompetitorCodeIds)
                    {
                        row = dtcompetitorcodes.NewRow();
                        row["CompetitorCode"] = competitorcode;
                        dtcompetitorcodes.Rows.Add(row);
                    }
                }

                dtcompetitornames.Columns.Add("CompetitorName");
                if (InputParam.CompetitorNames!=null)
                {
                    foreach (Int64 competitorname in InputParam.CompetitorNames)
                    {
                        row = dtcompetitornames.NewRow();
                        row["CompetitorName"] = competitorname;
                        dtcompetitornames.Rows.Add(row);
                    }
                }

                SqlParameter[] objDBParam = new SqlParameter[3];
                objDBParam[0] = new SqlParameter("@CompetitorCodes", dtcompetitorcodes);
                objDBParam[1] = new SqlParameter("@CompetitorNames", dtcompetitornames);
                objDBParam[2] = new SqlParameter("@Status", InputParam.Status);

                var outputFromSP = _dataHelper.ExecuteProcedure("[dbo].[GetCompetitorGridData]", objDBParam);

                if (outputFromSP.Rows.Count > 0)
                {
                    output = (from data in outputFromSP.AsEnumerable()
                              select new CompetitorMasterData
                              {
                                  ID = Convert.ToInt64(data.Field<Int64>("ID")),
                                  CompetitorCode = Convert.ToString(data.Field<string>("CompetitorCode")),
                                  CompetitorName = Convert.ToString(data.Field<string>("CompetitorName")),
                                  CompetitorStatus = Convert.ToString(data.Field<string>("Status")),
                              }).ToList();
                }
                else
                    output = new List<CompetitorMasterData>();
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetCompetitorMasterGrid", ex.StackTrace, ex.Message);
            }
            return output;
        }

        /// <summary>
        /// To get Competitor Product Category Master Data.
        /// </summary>
        /// <returns>Competitor Product Category Master Data.</returns>
        public IEnumerable<CompetitorProductData> GetCompetitorProductMasterGrid(CompetitorProductFilter InputParam)
        {
            DataTable dtcompetitorids = new DataTable();
            DataTable dtproductids = new DataTable();
            IEnumerable<CompetitorProductData> output = null;
            DataRow row;

            try
            {
                dtcompetitorids.Columns.Add("FilterId");
                if (InputParam.CompetitorIds != null)
                {
                    foreach (Int64 competitorname in InputParam.CompetitorIds)
                    {
                        row = dtcompetitorids.NewRow();
                        row["FilterId"] = competitorname;
                        dtcompetitorids.Rows.Add(row);
                    }
                }

                dtproductids.Columns.Add("FilterId");
                if (InputParam.ProductCatIds != null)
                {
                    foreach (Int64 productid in InputParam.ProductCatIds)
                    {
                        row = dtproductids.NewRow();
                        row["FilterId"] = productid;
                        dtproductids.Rows.Add(row);
                    }
                }

                SqlParameter[] objDBParam = new SqlParameter[3];
                objDBParam[0] = new SqlParameter("@CompetitorIds", dtcompetitorids);
                objDBParam[1] = new SqlParameter("@ProductIds", dtproductids);
                objDBParam[2] = new SqlParameter("@Status", InputParam.Status);

                var outputFromSP = _dataHelper.ExecuteProcedure("[dbo].[GetCompetitorProductMasterGrid]", objDBParam);

                if (outputFromSP.Rows.Count > 0)
                {
                    output = (from data in outputFromSP.AsEnumerable()
                              select new CompetitorProductData
                              {
                                  ID = Convert.ToInt64(data.Field<Int64>("ID")),
                                  ProductName = Convert.ToString(data.Field<string>("ProductName")),
                                  CompetitorID = Convert.ToInt64(data.Field<Int64>("CompetitorID")),
                                  ProductStatus = Convert.ToString(data.Field<string>("ProductStatus")),
                                  SonyProductCategory = Convert.ToInt64(data.Field<Int64>("SonyProductCategoryId")),
                                  TopModelString = Convert.ToString(data.Field<string>("TopModel")),
                                  CompetitorName = Convert.ToString(data.Field<string>("CompetitorName")),
                                  ProductCategoryName = Convert.ToString(data.Field<string>("CategoryName"))
                                  
                              }).ToList();
                }
                else
                    output = new List<CompetitorProductData>();
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetCompetitorProductMasterGrid", ex.StackTrace, ex.Message);
            }
            return output;
        }

        /// <summary>
        /// To get Competitor's model master grid data.
        /// </summary>
        /// <returns>Competitor's model master grid data.</returns>
        public IEnumerable<CompetitorModelList> GetCompetitorModelMasterGrid(CompetitorModelFilter InputParam)
        {
            IEnumerable<CompetitorModelList> output = null;
            DataTable dtcompetitorids = new DataTable();
            DataTable dtproductids = new DataTable();
            DataTable dtmodelids = new DataTable();
            DataTable dtsubproids = new DataTable();
            DataRow row;
            try
            {
                dtcompetitorids.Columns.Add("FilterId");
                if (InputParam.CompetitorIds != null)
                {
                    foreach (Int64 competitorname in InputParam.CompetitorIds)
                    {
                        row = dtcompetitorids.NewRow();
                        row["FilterId"] = competitorname;
                        dtcompetitorids.Rows.Add(row);
                    }
                }

                dtproductids.Columns.Add("FilterId");
                if (InputParam.ProductCatIds != null)
                {
                    foreach (Int64 productid in InputParam.ProductCatIds)
                    {
                        row = dtproductids.NewRow();
                        row["FilterId"] = productid;
                        dtproductids.Rows.Add(row);
                    }
                }

                dtmodelids.Columns.Add("FilterId");
                if (InputParam.ModelIds != null)
                {
                    foreach (Int64 modelid in InputParam.ModelIds)
                    {
                        row = dtmodelids.NewRow();
                        row["FilterId"] = modelid;
                        dtmodelids.Rows.Add(row);
                    }
                }

                dtsubproids.Columns.Add("FilterId");
                if (InputParam.SonyProSubCatIds != null)
                {
                    foreach (Int64 subproid in InputParam.SonyProSubCatIds)
                    {
                        row = dtsubproids.NewRow();
                        row["FilterId"] = subproid;
                        dtsubproids.Rows.Add(row);
                    }
                }

                SqlParameter[] objDBParam = new SqlParameter[6];
                objDBParam[0] = new SqlParameter("@CompetitorIds", dtcompetitorids);
                objDBParam[1] = new SqlParameter("@ProductIds", dtproductids);
                objDBParam[2] = new SqlParameter("@ModelIds", dtmodelids);
                objDBParam[3] = new SqlParameter("@SonyProCatId", InputParam.SonyProCatId);
                objDBParam[4] = new SqlParameter("@SonyProSubCatIds", dtsubproids);
                objDBParam[5] = new SqlParameter("@Status", InputParam.Status);

                var outputFromSP = _dataHelper.ExecuteProcedure("[dbo].[GetCompetitorModelGridData]", objDBParam);

                if (outputFromSP.Rows.Count > 0)
                {
                    output = (from data in outputFromSP.AsEnumerable()
                              select new CompetitorModelList
                              {
                                  ID = Convert.ToInt64(data.Field<Int64>("ID")),
                                  CompetitorCode = Convert.ToString(data.Field<string>("CompetitorCode")),
                                  CompetitorProductCategory = Convert.ToString(data.Field<string>("CompetitorProductName")),
                                  SonyProductCategory = Convert.ToString(data.Field<string>("CategoryName")),
                                  SonyProductSubCategoryName = Convert.ToString(data.Field<string>("SubCategoryName")),
                                  SonyModel = Convert.ToString(data.Field<string>("Name")),
                                  ModelName = Convert.ToString(data.Field<string>("ModelName")),
                                  CompetitorModelStatus = Convert.ToString(data.Field<string>("Status"))
                              }).ToList();
                }
                else
                    output = new List<CompetitorModelList>();
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetCompetitorModelMasterGrid", ex.StackTrace, ex.Message);
            }
            return output;
        }

        /// <summary>
        /// to get sfa salary master grid data from User, Employee and Salary Master table
        /// </summary>
        /// <param name="objGridVariables"></param>
        /// <returns></returns>
        public GridOutput<IEnumerable<SFASalaryMasterGrid>> GetSFASalaryMasterGrid(SFASalaryMasterFilter objGridVariables)
        {
            GridOutput<IEnumerable<SFASalaryMasterGrid>> output = new GridOutput<IEnumerable<SFASalaryMasterGrid>>();
            output.draw = objGridVariables.draw;
            DataTable dtcity = new DataTable();
            DataTable dtstate = new DataTable();
            DataTable dtbranch = new DataTable();
            DataRow row;
            try
            {

                //dtlogin.Columns.Add("FilterId");
                //if (objGridVariables.LoginIds != null)
                //{
                //    foreach (Int64 loginid in objGridVariables.LoginIds)
                //    {
                //        row = dtlogin.NewRow();
                //        row["FilterId"] = loginid;
                //        dtlogin.Rows.Add(row);
                //    }
                //}

                //dtemployee.Columns.Add("FilterId");
                //if (objGridVariables.EmployeeIds != null)
                //{
                //    foreach (Int64 sfaid in objGridVariables.EmployeeIds)
                //    {
                //        row = dtemployee.NewRow();
                //        row["FilterName"] = sfaid;
                //        dtemployee.Rows.Add(row);
                //    }
                //}

                dtcity.Columns.Add("FilterId");
                if (objGridVariables.CityIds != null)
                {
                    foreach (Int64 cityid in objGridVariables.CityIds)
                    {
                        row = dtcity.NewRow();
                        row["FilterId"] = cityid;
                        dtcity.Rows.Add(row);
                    }
                }

                dtstate.Columns.Add("FilterId");
                if (objGridVariables.StateIds != null)
                {
                    foreach (Int64 stateid in objGridVariables.StateIds)
                    {
                        row = dtstate.NewRow();
                        row["FilterId"] = stateid;
                        dtstate.Rows.Add(row);
                    }
                }

                dtbranch.Columns.Add("FilterId");
                if (objGridVariables.BranchIds != null)
                {
                    foreach (Int64 branchid in objGridVariables.BranchIds)
                    {
                        row = dtbranch.NewRow();
                        row["FilterId"] = branchid;
                        dtbranch.Rows.Add(row);
                    }
                }


                SqlParameter[] objDBParam = new SqlParameter[11];
                objDBParam[0] = new SqlParameter("@LoginIds", objGridVariables.LoginIds != null ? objGridVariables.LoginIds.Trim() : null);
                objDBParam[1] = new SqlParameter("@EmployeeIds", objGridVariables.EmployeeIds != null ? objGridVariables.EmployeeIds.Trim() : null);
                objDBParam[2] = new SqlParameter("@CityIds", dtcity);
                objDBParam[3] = new SqlParameter("@StateIds", dtstate);
                objDBParam[4] = new SqlParameter("@BranchIds", dtbranch);
                objDBParam[5] = new SqlParameter("@PageStart", objGridVariables.start);
                objDBParam[6] = new SqlParameter("@PageSize", objGridVariables.length);
                if (objGridVariables.order != null)
                    objDBParam[7] = new SqlParameter("@SortColumn", objGridVariables.order.FirstOrDefault().column);
                else
                    objDBParam[7] = new SqlParameter("@SortColumn", 0);
                if (objGridVariables.order != null)
                    objDBParam[8] = new SqlParameter("@SortDirection", objGridVariables.order.FirstOrDefault().dir);
                else
                    objDBParam[8] = new SqlParameter("@SortDirection", "asc");
                if (objGridVariables.search != null)
                    objDBParam[9] = new SqlParameter("@SearchValue", objGridVariables.search.value);
                else
                    objDBParam[9] = new SqlParameter("@SearchValue", "");
                objDBParam[10] = new SqlParameter("@Status", objGridVariables.Status);

                var outputFromSP = _dataHelper.ExecuteProcedure("[dbo].[GetSFASalaryMasterGrid]", objDBParam);

                if (outputFromSP.Rows.Count > 0)
                {
                    output.data = (from sfaSalaryMaster in outputFromSP.AsEnumerable()
                                   select new SFASalaryMasterGrid
                                   {
                                       Id = Convert.ToInt64(sfaSalaryMaster.Field<Int64?>("ID")),
                                       LoginId = Convert.ToInt64(sfaSalaryMaster.Field<Int64>("LOGINID")),
                                       SFAName = Convert.ToString(sfaSalaryMaster.Field<string>("SFANAME")),
                                       SFACode = Convert.ToString(sfaSalaryMaster.Field<string>("SFACode")),
                                       Branch = Convert.ToString(sfaSalaryMaster.Field<string>("BRANCH")),
                                       State = Convert.ToString(sfaSalaryMaster.Field<string>("STATE")),
                                       City = Convert.ToString(sfaSalaryMaster.Field<string>("CITY")),
                                       Division = Convert.ToString(sfaSalaryMaster.Field<string>("DIVISION")),
                                       SFALevel = Convert.ToString(sfaSalaryMaster.Field<string>("SFALEVEL")),
                                       Basic = Convert.ToString(sfaSalaryMaster.Field<string>("BASIC")) == "" ?
                                               "" : AMBOEcryption.DecryptData(sfaSalaryMaster.Field<string>("BASIC"), true),
                                       HRA = Convert.ToString(sfaSalaryMaster.Field<string>("HRA")) == "" ?
                                               "" : AMBOEcryption.DecryptData(sfaSalaryMaster.Field<string>("HRA"), true),
                                       Med = Convert.ToString(sfaSalaryMaster.Field<string>("Med")) == "" ?
                                               "" : AMBOEcryption.DecryptData(sfaSalaryMaster.Field<string>("Med"), true),
                                       Conv = Convert.ToString(sfaSalaryMaster.Field<string>("Conv")) == "" ?
                                               "" : AMBOEcryption.DecryptData(sfaSalaryMaster.Field<string>("Conv"), true),
                                       Other = Convert.ToString(sfaSalaryMaster.Field<string>("Other")) == "" ?
                                               "" : AMBOEcryption.DecryptData(sfaSalaryMaster.Field<string>("Other"), true),
                                       Airtime = Convert.ToString(sfaSalaryMaster.Field<string>("Airtime")) == "" ?
                                               "" : AMBOEcryption.DecryptData(sfaSalaryMaster.Field<string>("Airtime"), true),
                                       Insurance = Convert.ToString(sfaSalaryMaster.Field<string>("Insurance")) == "" ?
                                               "" : AMBOEcryption.DecryptData(sfaSalaryMaster.Field<string>("Insurance"), true)
                                   });
                }
                else
                    output.data = new List<SFASalaryMasterGrid>();
                output.recordsFiltered = Convert.ToInt32(outputFromSP.Rows[0]["FilteredCount"]);
                output.recordsTotal = Convert.ToInt32(outputFromSP.Rows[0]["FilteredCount"]);
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetSFASalaryMasterGrid", ex.StackTrace, ex.Message);
            }
            return output;
        }
		
		public IEnumerable<ChannelGridData> GetChannelMasterGrid(ChannelFilter objGridVariables)
        {
            IEnumerable<ChannelGridData> output = null;
            DataTable dtcodeids = new DataTable();
            DataTable dtnameids = new DataTable();
            DataRow row;
            //output.draw = objGridVariables.Draw;
            //DbParameterCollection objDBParam = GetDBParamFromGrid(ref objGridVariables);
            try
            {
                dtcodeids.Columns.Add("FilterId");
                if (objGridVariables.ChannelCodeIds != null)
                {
                    foreach (Int64 codeid in objGridVariables.ChannelCodeIds)
                    {
                        row = dtcodeids.NewRow();
                        row["FilterId"] = codeid;
                        dtcodeids.Rows.Add(row);
                    }
                }

                dtnameids.Columns.Add("FilterId");
                if (objGridVariables.ChannelNameIds != null)
                {
                    foreach (Int64 nameid in objGridVariables.ChannelNameIds)
                    {
                        row = dtnameids.NewRow();
                        row["FilterId"] = nameid;
                        dtnameids.Rows.Add(row);
                    }
                }

                SqlParameter[] objDBParam = new SqlParameter[2];
                objDBParam[0] = new SqlParameter("@ChannelCodeIds", dtcodeids);
                objDBParam[1] = new SqlParameter("@ChannelNameIds", dtnameids);

                var outputFromSP = _dataHelper.ExecuteProcedure("[dbo].[GetChannelMasterGrid]", objDBParam);

                if (outputFromSP.Rows.Count > 0)
                {
                    output = (from state in outputFromSP.AsEnumerable()
                                   select new ChannelGridData
                                   {
                                       Id = Convert.ToInt64(state.Field<Int64>("ID")),
                                       ChannelCode = Convert.ToString(state.Field<string>("CHANNELCODE")),
                                       ChannelName = Convert.ToString(state.Field<string>("CHANNELNAME"))
                                   });
                }
                else
                    output = new List<ChannelGridData>();
                
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetChannelMasterGrid", ex.StackTrace, ex.Message);
            }
            return output;
        }

        public GridOutput<IEnumerable<TargetDateSettingMaster>> GetTargetDateSettingGrid(GridVariables objGridVariables)
        {
            GridOutput<IEnumerable<TargetDateSettingMaster>> output = new GridOutput<IEnumerable<TargetDateSettingMaster>>();
            output.draw = objGridVariables.Draw;
            DbParameterCollection objDBParam = GetDBParamFromGrid(ref objGridVariables);
            try
            {
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetTargetDateSettingGrid]", objDBParam, CommandType.StoredProcedure);

                if (outputFromSP.Rows.Count > 0)
                {
                    output.data = (from targetDateSetting in outputFromSP.AsEnumerable()
                                   select new TargetDateSettingMaster
                                   {
                                       Id = Convert.ToInt64(targetDateSetting.Field<Int64>("ID")),                                       
                                       BranchName = Convert.ToString(targetDateSetting.Field<string>("BRANCH")),
                                       TargetDate = targetDateSetting.Field<string>("TARGETDATE"),
                                      
                                   });
                }
                else
                    output.data = new List<TargetDateSettingMaster>();
                output.recordsFiltered = Convert.ToInt32(outputFromSP.Rows[0]["FilteredCount"]);
                output.recordsTotal = Convert.ToInt32(outputFromSP.Rows[0]["FilteredCount"]);
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetTargetDateSettingGrid", ex.StackTrace, ex.Message);
            }
            return output;
        }

        public GridOutput<IEnumerable<IncentiveTargetCategoryMappingGridData>> GetIncentiveTargetCategoryMappingGrid(GridVariables objGridVariables)
        {
            GridOutput<IEnumerable<IncentiveTargetCategoryMappingGridData>> output = new GridOutput<IEnumerable<IncentiveTargetCategoryMappingGridData>>();
            output.draw = objGridVariables.Draw;
            DbParameterCollection objDBParam = GetDBParamFromGrid(ref objGridVariables);
            try
            {
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetIncentiveTargetCategoryMappingGrid]", objDBParam, CommandType.StoredProcedure);

                if (outputFromSP.Rows.Count > 0)
                {
                    output.data = (from category in outputFromSP.AsEnumerable()
                                   select new IncentiveTargetCategoryMappingGridData
                                   {
                                       IncentiveCategoryId = Convert.ToInt64(category.Field<Int64>("ID")),
                                       IncentiveCategoryName = Convert.ToString(category.Field<string>("CATEGORYNAME")),
                                       StatusName = Convert.ToString(category.Field<string>("STATUSNAME"))
                                   });
                }
                else
                    output.data = new List<IncentiveTargetCategoryMappingGridData>();
                output.recordsFiltered = Convert.ToInt32(outputFromSP.Rows[0]["FilteredCount"]);
                output.recordsTotal = Convert.ToInt32(outputFromSP.Rows[0]["FilteredCount"]);
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetIncentiveTargetCategoryMappingGrid", ex.StackTrace, ex.Message);
            }
            return output;
        }

        public GridOutput<IEnumerable<BaseIncentiveGridData>> GetBaseIncentiveGrid(GridVariables objGridVariables)
        {
            GridOutput<IEnumerable<BaseIncentiveGridData>> output = new GridOutput<IEnumerable<BaseIncentiveGridData>>();
            output.draw = objGridVariables.Draw;
            DbParameterCollection objDBParam = GetDBParamFromGrid(ref objGridVariables);
            try
            {
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetBaseIncentiveDefinitionGrid]", objDBParam, CommandType.StoredProcedure);

                if (outputFromSP.Rows.Count > 0)
                {
                    output.data = (from category in outputFromSP.AsEnumerable()
                                   select new BaseIncentiveGridData
                                   {
                                       TargetCategoryId = Convert.ToInt64(category.Field<Int64>("TARGETCATID")),
                                       TargetCategory = Convert.ToString(category.Field<string>("TARGETCATNAME")),
                                       Status = Convert.ToString(category.Field<string>("STATUS"))
                                   });
                }
                else
                    output.data = new List<BaseIncentiveGridData>();
                output.recordsFiltered = Convert.ToInt32(outputFromSP.Rows[0]["FilteredCount"]);
                output.recordsTotal = Convert.ToInt32(outputFromSP.Rows[0]["FilteredCount"]);
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetBaseIncentiveGrid", ex.StackTrace, ex.Message);
            }
            return output;
        }

        public GridOutput<IEnumerable<PerPieceIncentiveGridData>> GetPerPieceIncentiveGrid(GridVariables objGridVariables)
        {
            GridOutput<IEnumerable<PerPieceIncentiveGridData>> output = new GridOutput<IEnumerable<PerPieceIncentiveGridData>>();
            output.draw = objGridVariables.Draw;
            DbParameterCollection objDBParam = GetDBParamFromGrid(ref objGridVariables);
            try
            {
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetPerPieceIncentiveGrid]", objDBParam, CommandType.StoredProcedure);

                if (outputFromSP.Rows.Count > 0)
                {
                    output.data = (from scheme in outputFromSP.AsEnumerable()
                                   select new PerPieceIncentiveGridData
                                   {
                                       SchemeId = Convert.ToInt64(scheme.Field<Int64>("SCHEMEID")),
                                       SchemeName = Convert.ToString(scheme.Field<string>("SCHEMENAME")),
                                       Applicability = Convert.ToString(scheme.Field<string>("APPLICABILITY")),
                                       DateFrom = Convert.ToDateTime(scheme.Field<DateTime>("DATEFROM")),
                                       DateTo = Convert.ToDateTime(scheme.Field<DateTime>("DATETO"))
                                   });
                }
                else
                    output.data = new List<PerPieceIncentiveGridData>();
                output.recordsFiltered = Convert.ToInt32(outputFromSP.Rows[0]["FilteredCount"]);
                output.recordsTotal = Convert.ToInt32(outputFromSP.Rows[0]["FilteredCount"]);
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetPerPieceIncentiveGrid", ex.StackTrace, ex.Message);
            }
            return output;
        }

        public GridOutput<IEnumerable<FestivalIncentiveGridData>> GetFestivalIncentiveGrid(GridVariables objGridVariables)
        {
            GridOutput<IEnumerable<FestivalIncentiveGridData>> output = new GridOutput<IEnumerable<FestivalIncentiveGridData>>();
            output.draw = objGridVariables.Draw;
            DbParameterCollection objDBParam = GetDBParamFromGrid(ref objGridVariables);
            try
            {
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetFestivalIncentiveGrid]", objDBParam, CommandType.StoredProcedure);

                if (outputFromSP.Rows.Count > 0)
                {
                    output.data = (from scheme in outputFromSP.AsEnumerable()
                                   select new FestivalIncentiveGridData
                                   {
                                       SchemeId = Convert.ToInt64(scheme.Field<Int64>("SCHEMEID")),
                                       SchemeName = Convert.ToString(scheme.Field<string>("SCHEMENAME")),
                                       CalculateBaseIncentive = Convert.ToString(scheme.Field<string>("CALCULATEBASEINCENTIVE")),
                                       StartDate = Convert.ToDateTime(scheme.Field<DateTime>("STARTDATE")),
                                       EndDate = Convert.ToDateTime(scheme.Field<DateTime>("ENDDATE"))
                                   });
                }
                else
                    output.data = new List<FestivalIncentiveGridData>();
                output.recordsFiltered = Convert.ToInt32(outputFromSP.Rows[0]["FilteredCount"]);
                output.recordsTotal = Convert.ToInt32(outputFromSP.Rows[0]["FilteredCount"]);
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetFestivalIncentiveGrid", ex.StackTrace, ex.Message);
            }
            return output;
        }

        public GridOutput<IEnumerable<SFAGridData>> GetSFAMasterGrid(SFAGridSearchFilters objGridVariables)
        {
            GridOutput<IEnumerable<SFAGridData>> output = new GridOutput<IEnumerable<SFAGridData>>();
            output.draw = objGridVariables.draw;
            SqlParameter[] objDBParam = new SqlParameter[12];
            DataTable dtBranch = new DataTable();
            DataTable dtState = new DataTable();
            DataTable dtCity = new DataTable();
            DataTable dtAgency = new DataTable();
            DataTable dtSFAType = new DataTable();
            DataRow dr;
            try
            {
                dtBranch.Columns.Add("FilterId");
                dtState.Columns.Add("FilterId");
                dtCity.Columns.Add("FilterId");
                dtAgency.Columns.Add("FilterId");
                dtSFAType.Columns.Add("FilterId");

                if (objGridVariables.BranchIds != null)
                    foreach(Int64 BranchId in objGridVariables.BranchIds)
                    {
                        dr = dtBranch.NewRow();
                        dr["FilterId"] = BranchId;
                        dtBranch.Rows.Add(dr);
                    }

                if (objGridVariables.StateIds != null)
                    foreach (Int64 StateId in objGridVariables.StateIds)
                    {
                        dr = dtState.NewRow();
                        dr["FilterId"] = StateId;
                        dtState.Rows.Add(dr);
                    }

                if (objGridVariables.CityIds != null)
                    foreach (Int64 CityId in objGridVariables.CityIds)
                    {
                        dr = dtCity.NewRow();
                        dr["FilterId"] = CityId;
                        dtCity.Rows.Add(dr);
                    }

                if (objGridVariables.AgencyIds != null)
                    foreach (Int64 AgencyId in objGridVariables.AgencyIds)
                    {
                        dr = dtAgency.NewRow();
                        dr["FilterId"] = AgencyId;
                        dtAgency.Rows.Add(dr);
                    }

                if (objGridVariables.SFAType != null)
                    foreach (Int64 sfatypeid in objGridVariables.SFAType)
                    {
                        dr = dtSFAType.NewRow();
                        dr["FilterId"] = sfatypeid;
                        dtSFAType.Rows.Add(dr);
                    }

                objDBParam[0] = new SqlParameter("@PageStart", objGridVariables.start);
                objDBParam[1] = new SqlParameter("@PageSize", objGridVariables.length);
                if (objGridVariables.order != null)
                    objDBParam[2] = new SqlParameter("@SortColumn", objGridVariables.order.FirstOrDefault().column);
                else
                    objDBParam[2] = new SqlParameter("@SortColumn", 0);
                if (objGridVariables.order != null)
                    objDBParam[3] = new SqlParameter("@SortDirection", objGridVariables.order.FirstOrDefault().dir);
                else
                    objDBParam[3] = new SqlParameter("@SortDirection", "asc");
                
                objDBParam[4] = new SqlParameter("@BranchIds", dtBranch);
                objDBParam[5] = new SqlParameter("@StateIds", dtState);
                objDBParam[6] = new SqlParameter("@CityIds", dtCity);
                objDBParam[7] = new SqlParameter("@AgencyIds", dtAgency);
                objDBParam[8] = new SqlParameter("@Status", objGridVariables.Status);
                objDBParam[9] = new SqlParameter("@LoginId", objGridVariables.LoginId);
                objDBParam[10] = new SqlParameter("@EmployeeName", objGridVariables.EmployeeName);
                objDBParam[11] = new SqlParameter("@SFAType", dtSFAType);

                var outputFromSP = _dataHelper.ExecuteProcedureForDataset("[dbo].[GetSFAMasterGrid]", objDBParam);

                if (outputFromSP.Tables[0].Rows.Count > 0)
                {
                    output.data = (from sfa in outputFromSP.Tables[0].AsEnumerable()
                                   select new SFAGridData
                                   {
                                       EmployeeId = Convert.ToInt64(sfa.Field<Int64>("ID")),
                                       FirstName = Convert.ToString(sfa.Field<string>("FIRSTNAME")),
                                       LastName = Convert.ToString(sfa.Field<string>("LASTNAME")),
                                       LoginId = Convert.ToString(sfa.Field<string>("LOGINID")),
                                       BranchName = Convert.ToString(sfa.Field<string>("BRANCHNAME")),
                                       StateName = Convert.ToString(sfa.Field<string>("STATENAME")),
                                       CityName = Convert.ToString(sfa.Field<string>("CITYNAME")),
                                       Role = Convert.ToString(sfa.Field<string>("ROLE")),
                                       AgencyName = Convert.ToString(sfa.Field<string>("AGENCYNAME")),
                                       StatusName = Convert.ToString(sfa.Field<string>("STATUS")),
                                       EmployeeCode = Convert.ToString(sfa.Field<string>("EmployeeCode")),
                                       RegionName = Convert.ToString(sfa.Field<string>("RegionName")),
                                       Division = Convert.ToString(sfa.Field<string>("DivisionName")),
                                       Address = Convert.ToString(sfa.Field<string>("Address")) == "" ?
                                               "" : AMBOEcryption.DecryptData(sfa.Field<string>("Address"), true),
                                       MobileNumber = Convert.ToString(sfa.Field<string>("MobileNumber")),
                                       AltMobileNumber = Convert.ToString(sfa.Field<string>("AlternateNumber")),
                                       EmailId = Convert.ToString(sfa.Field<string>("Email")) == "" ?
                                               "" : AMBOEcryption.DecryptData(sfa.Field<string>("Email"), true),
                                       PANNo = Convert.ToString(sfa.Field<string>("PAN")) == "" ?
                                               "" : AMBOEcryption.DecryptData(sfa.Field<string>("PAN"), true),
                                       DOB = Convert.ToString(sfa.Field<string>("DOB")) == "" ?
                                               "" : AMBOEcryption.DecryptData(sfa.Field<string>("DOB"), true),
                                       DOJ = Convert.ToString(sfa.Field<string>("DOJ")),
                                       DOL = Convert.ToString(sfa.Field<string>("DOL")),
                                       IMEI1 = Convert.ToString(sfa.Field<string>("IMEI1")),
                                       IMEI2 = Convert.ToString(sfa.Field<string>("IMEI2")),
                                       SFALevelName = Convert.ToString(sfa.Field<string>("SFALevel")),
                                       SFAType = Convert.ToString(sfa.Field<string>("SFAType")),
                                       PrimaryCode = Convert.ToString(sfa.Field<string>("PrimaryCode")),
                                       MasterCode = Convert.ToString(sfa.Field<string>("MasterCode")),
                                       DealerCity = Convert.ToString(sfa.Field<string>("DealerCity")),
                                       DealerLocation = Convert.ToString(sfa.Field<string>("DealerLocation")),
                                       Channel = Convert.ToString(sfa.Field<string>("Channel")),
                                       DealerName = Convert.ToString(sfa.Field<string>("DealerName")),
                                       IncentiveCategoryName = Convert.ToString(sfa.Field<string>("CategoryName")),
                                       ShiftName = Convert.ToString(sfa.Field<string>("ShiftName")),
                                   });
                }
                else
                    output.data = new List<SFAGridData>();
                output.recordsFiltered = Convert.ToInt32(outputFromSP.Tables[0].Rows[0]["FilteredCount"]);
                output.recordsTotal = Convert.ToInt32(outputFromSP.Tables[0].Rows[0]["FilteredCount"]);
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetSFAMasterGrid", ex.StackTrace, ex.Message);
            }
            return output;
        }

        public IEnumerable<AssetAssignmentToRDIGrid> GetAssetAssignmentToRDIByReference(string Reference)
        {
            IEnumerable<AssetAssignmentToRDIGrid> output = null;

            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@Reference", Reference, DbType.String));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetAssetAssignmentToRDIGridByReference]", objDBParam, CommandType.StoredProcedure);

                if (outputFromSP.Rows.Count > 0)
                {
                    output = (from assetassignmenttordi in outputFromSP.AsEnumerable()
                                   select new AssetAssignmentToRDIGrid
                                   {
                                       Id = Convert.ToInt64(assetassignmenttordi.Field<Int64>("ID")),
                                       Reference = Convert.ToString(assetassignmenttordi.Field<string>("REFERENCE")),
                                       IssuedDate = assetassignmenttordi.Field<string>("ISSUEDDATE"),
                                       MaterialCode = Convert.ToString(assetassignmenttordi.Field<string>("MATERIALCODE")),
                                       ProductName = Convert.ToString(assetassignmenttordi.Field<string>("PRODUCTNAME")),
                                       RDIName = Convert.ToString(assetassignmenttordi.Field<string>("RDINAME")),
                                       RDICode = Convert.ToString(assetassignmenttordi.Field<string>("RDICODE")),
                                       Place = Convert.ToString(assetassignmenttordi.Field<string>("PLACE")),
                                       Reason = Convert.ToString(assetassignmenttordi.Field<string>("REASON")),
                                       IssuedQty = Convert.ToInt32(assetassignmenttordi.Field<int>("ISSUEDQTY")),
                                   });
                }
                else
                    output = new List<AssetAssignmentToRDIGrid>();
               
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetAssetAssignmentToRDIByReference", ex.StackTrace, ex.Message);
            }
            return output;
        }
		
		public GridOutput<IEnumerable<ProductCategorySFAMappingGridData>> GetProductCategorySFAMappingGrid(ProdCatSFAMapGridSearchFilters objGridVariables)
        {
            GridOutput<IEnumerable<ProductCategorySFAMappingGridData>> output = new GridOutput<IEnumerable<ProductCategorySFAMappingGridData>>();
            output.draw = objGridVariables.draw;
            SqlParameter[] objDBParam = new SqlParameter[10];
            DataTable dtBranch = new DataTable();
            DataTable dtLocation = new DataTable();
            DataTable dtCity = new DataTable();
            DataTable dtDealer = new DataTable();
            //DataTable dtSFA = new DataTable();
            DataTable dtProdCat = new DataTable();
            DataRow dr;
            try
            {

                dtBranch.Columns.Add("FilterId");
                dtLocation.Columns.Add("FilterId");
                dtCity.Columns.Add("FilterId");
                dtDealer.Columns.Add("FilterId");
                //dtSFA.Columns.Add("FilterId");
                dtProdCat.Columns.Add("FilterId");

                if (objGridVariables.BranchIds != null)
                    foreach (Int64 BranchId in objGridVariables.BranchIds)
                    {
                        dr = dtBranch.NewRow();
                        dr["FilterId"] = BranchId;
                        dtBranch.Rows.Add(dr);
                    }

                if (objGridVariables.CityIds != null)
                    foreach (Int64 CityId in objGridVariables.CityIds)
                    {
                        dr = dtCity.NewRow();
                        dr["FilterId"] = CityId;
                        dtCity.Rows.Add(dr);
                    }

                if (objGridVariables.LocationIds != null)
                    foreach (Int64 LocationId in objGridVariables.LocationIds)
                    {
                        dr = dtLocation.NewRow();
                        dr["FilterId"] = LocationId;
                        dtLocation.Rows.Add(dr);
                    }

                if (objGridVariables.DealerIds != null)
                    foreach (Int64 DealerId in objGridVariables.DealerIds)
                    {
                        dr = dtDealer.NewRow();
                        dr["FilterId"] = DealerId;
                        dtDealer.Rows.Add(dr);
                    }

                //if (objGridVariables.SFAIds != null)
                //    foreach (Int64 SFAId in objGridVariables.SFAIds)
                //    {
                //        dr = dtSFA.NewRow();
                //        dr["FilterId"] = SFAId;
                //        dtSFA.Rows.Add(dr);
                //    }

                if (objGridVariables.ProductCategoryIds != null)
                    foreach (Int64 ProductCategoryId in objGridVariables.ProductCategoryIds)
                    {
                        dr = dtProdCat.NewRow();
                        dr["FilterId"] = ProductCategoryId;
                        dtProdCat.Rows.Add(dr);
                    }

                objDBParam[0] = new SqlParameter("@PageStart", objGridVariables.start);
                objDBParam[1] = new SqlParameter("@PageSize", objGridVariables.length);
                objDBParam[2] = new SqlParameter("@SortColumn", objGridVariables.order == null ? 0 : objGridVariables.order[0].column);
                objDBParam[3] = new SqlParameter("@SortDirection", objGridVariables.order == null ? "asc" : objGridVariables.order[0].dir);
                objDBParam[4] = new SqlParameter("@BranchIds", dtBranch);
                objDBParam[5] = new SqlParameter("@CityIds", dtCity);
                objDBParam[6] = new SqlParameter("@LocationIds", dtLocation);
                objDBParam[7] = new SqlParameter("@SFAIds",objGridVariables.SFAIds);
                objDBParam[8] = new SqlParameter("@DealerIds", dtDealer);
                objDBParam[9] = new SqlParameter("@ProdCatIds", dtProdCat);

                var outputFromSP = _dataHelper.ExecuteProcedure("[dbo].[GetProductCategorySFAMappingGrid]", objDBParam);

                if (outputFromSP.Rows.Count > 0)
                {
                    output.data = (from mapping in outputFromSP.AsEnumerable()
                                   select new ProductCategorySFAMappingGridData
                                   {
                                       ID = Convert.ToInt64(mapping.Field<Int64>("ID")),
                                       ProductCategoryId = Convert.ToInt64(mapping.Field<Int64>("PRODCATID")),
                                       ProductCategoryName = Convert.ToString(mapping.Field<string>("PRODCATNAME")),
                                       BranchId = Convert.ToInt64(mapping.Field<Int64>("BRANCHID")),
                                       BranchName = Convert.ToString(mapping.Field<string>("BRANCHNAME")),
                                       DealerId = Convert.ToInt64(mapping.Field<Int64>("DEALERID")),
                                       DealerName = Convert.ToString(mapping.Field<string>("DEALERNAME")),
                                       EmployeeId = Convert.ToInt64(mapping.Field<Int64>("SFAID")),
                                       SFAName = Convert.ToString(mapping.Field<string>("SFANAME")),
                                       SFACode = Convert.ToString(mapping.Field<string>("SFACode")),
                                       DealerChannel = Convert.ToString(mapping.Field<string>("ChannelName")),
                                       City = Convert.ToString(mapping.Field<string>("CityName")),
                                       Location = Convert.ToString(mapping.Field<string>("LocationName"))
                                   });
                }
                else
                    output.data = new List<ProductCategorySFAMappingGridData>();
                output.recordsFiltered = Convert.ToInt32(outputFromSP.Rows[0]["FilteredCount"]);
                output.recordsTotal = Convert.ToInt32(outputFromSP.Rows[0]["FilteredCount"]);
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetProductCategorySFAMappingGrid", ex.StackTrace, ex.Message);
            }
            return output;
        }
		
		public List<AssignTargetToSFAGrid> GetTargetToSFAByMonth(AssignTargetToSFAGet targettoSFA)
        {
            DbParameterCollection objDBParam = new DbParameterCollection();
            List<AssignTargetToSFAGrid> targetlist = null;
            try
            {
                objDBParam.Add(new DbParameter("@TargetMonth", targettoSFA.TargetDate, DbType.String));
                objDBParam.Add(new DbParameter("@UserId", targettoSFA.UserId, DbType.Int64));
                objDBParam.Add(new DbParameter("@BranchId", targettoSFA.BranchId, DbType.Int64));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetTargetToSFAByMonth]", objDBParam, CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {

                    targetlist = (from data in outputFromSP.AsEnumerable()
                                  select new AssignTargetToSFAGrid
                                  {
                                      //Id = Convert.ToInt64(data.Field<Int64>("Id")),
                                      Month = Convert.ToString(data.Field<string>("Month")),
                                      Branch = Convert.ToString(data.Field<string>("Branch")),
                                      City = Convert.ToString(data.Field<string>("City")),
                                      Location = Convert.ToString(data.Field<string>("Location")),
                                      SFACode = Convert.ToString(data.Field<string>("SFACode")),
                                      SFAName = Convert.ToString(data.Field<string>("SFAName")),
                                      DealerCode = Convert.ToString(data.Field<string>("DealerCode")),
                                      MasterCode = Convert.ToString(data.Field<string>("MasterCode")),
                                      DealerName = Convert.ToString(data.Field<string>("DealerName")),
                                      SFACategory = Convert.ToString(data.Field<string>("SFACategory")),
                                      IncentiveCategory = Convert.ToString(data.Field<string>("IncentiveCategory")),
                                      TargetCategory = Convert.ToString(data.Field<string>("TargetCategory")),
                                      ProductCategory = Convert.ToString(data.Field<string>("ProductCategory")),
                                      QtyTarget = Convert.ToString(data.Field<string>("QtyTarget")),
                                      ValueTarget = Convert.ToString(data.Field<string>("ValueTarget"))

                                  }).ToList();
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetTargetToSFAByMonth", ex.StackTrace, ex.Message);
            }
            return targetlist;
        }

        public List<AssignTargetToSFAGrid> ShowTargetToSFAByMonth(AssignTargetToSFAGet targettoSFA)
        {
            DbParameterCollection objDBParam = new DbParameterCollection();
            List<AssignTargetToSFAGrid> targetlist = null;
            try
            {
                objDBParam.Add(new DbParameter("@TargetMonth", targettoSFA.TargetDate, DbType.String));
                objDBParam.Add(new DbParameter("@UserId", targettoSFA.UserId, DbType.Int64));
                objDBParam.Add(new DbParameter("@BranchId", targettoSFA.BranchId, DbType.Int64));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[ShowTargetToSFAByMonth]", objDBParam, CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {

                    targetlist = (from data in outputFromSP.AsEnumerable()
                                  select new AssignTargetToSFAGrid
                                  {
                                      Id = Convert.ToInt64(data.Field<Int64>("Id")),
                                      Month = Convert.ToString(data.Field<string>("Month")),
                                      Branch = Convert.ToString(data.Field<string>("Branch")),
                                      City = Convert.ToString(data.Field<string>("City")),
                                      Location = Convert.ToString(data.Field<string>("Location")),
                                      SFACode = Convert.ToString(data.Field<string>("SFACode")),
                                      SFAName = Convert.ToString(data.Field<string>("SFAName")),
                                      DealerCode = Convert.ToString(data.Field<string>("DealerCode")),
                                      MasterCode = Convert.ToString(data.Field<string>("MasterCode")),
                                      DealerName = Convert.ToString(data.Field<string>("DealerName")),
                                      SFACategory = Convert.ToString(data.Field<string>("SFACategory")),
                                      IncentiveCategory = Convert.ToString(data.Field<string>("IncentiveCategory")),
                                      TargetCategory = Convert.ToString(data.Field<string>("TargetCategory")),
                                      ProductCategory = Convert.ToString(data.Field<string>("ProductCategory")),
                                      QtyTarget = Convert.ToString(data.Field<string>("QtyTarget")),
                                      ValueTarget = Convert.ToString(data.Field<string>("ValueTarget"))

                                  }).ToList();
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("ShowTargetToSFAByMonth", ex.StackTrace, ex.Message);
            }
            return targetlist;
        }

        public IEnumerable<RoleRightsMappingGet> GetRoleRightsMappingGrid()
        {
            IEnumerable<RoleRightsMappingGet> output = null;

            try
            {                
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetRoleRightsMappingGrid]", CommandType.StoredProcedure);

                if (outputFromSP.Rows.Count > 0)
                {
                    output = (from rolerightsmappinggrid in outputFromSP.AsEnumerable()
                              select new RoleRightsMappingGet
                              {
                                  RoleRightsId = Convert.ToInt64(rolerightsmappinggrid.Field<Int64>("RoleRightsId")),
                                  RoleName = Convert.ToString(rolerightsmappinggrid.Field<string>("Role")),
                                  ModuleName = Convert.ToString(rolerightsmappinggrid.Field<string>("ModuleName")),
                                  SubModuleName = Convert.ToString(rolerightsmappinggrid.Field<string>("SubModuleName")),
                                  ViewPermission = Convert.ToString(rolerightsmappinggrid.Field<string>("ViewPermission")),
                                  CreatePermission = Convert.ToString(rolerightsmappinggrid.Field<string>("CreatePermission")),
                                  EditPermission = Convert.ToString(rolerightsmappinggrid.Field<string>("EditPermission")),
                                  DeletePermission = Convert.ToString(rolerightsmappinggrid.Field<string>("DeletePermission"))
                              });
                }
                else
                    output = new List<RoleRightsMappingGet>();

            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetRoleRightsMappingGrid", ex.StackTrace, ex.Message);
            }
            return output;
        }

        #region Master Grid with Filters
        public IEnumerable<RegionGridData> GetRegionGrid(RegionFilter InputParam)
        {
            IEnumerable<RegionGridData> output = null;
            DataTable dtregions = new DataTable();
            DataRow row;
            
            try
            {
                dtregions.Columns.Add("FilterId");
                if (InputParam.RegionIds != null)
                {
                    foreach (Int64 regionid in InputParam.RegionIds)
                    {
                        row = dtregions.NewRow();
                        row["FilterId"] = regionid;
                        dtregions.Rows.Add(row);
                    }
                }

                SqlParameter[] objDBParam = new SqlParameter[2];
                objDBParam[0] = new SqlParameter("@RegionIds", dtregions);
                objDBParam[1] = new SqlParameter("@Status", InputParam.Status);

                var outputFromSP = _dataHelper.ExecuteProcedure("[dbo].[GetRegionGrid]", objDBParam);

                if (outputFromSP.Rows.Count > 0)
                {
                    output = (from region in outputFromSP.AsEnumerable()
                                   select new RegionGridData
                                   {
                                       ID = Convert.ToInt64(region.Field<Int64>("ID")),
                                       RegionName = Convert.ToString(region.Field<string>("REGIONNAME")),
                                       Status = Convert.ToString(region.Field<string>("STATUS"))
                                   });
                }
                else
                    output = new List<RegionGridData>();
                
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetRegionGrid", ex.StackTrace, ex.Message);
            }
            return output;
        }

        public IEnumerable<StateGridData> GetStateGrid(StateFilter InputParam)
        {
            IEnumerable<StateGridData> output = null;
            DataTable dtregions = new DataTable();
            DataTable dtstates = new DataTable();
            DataRow row;

            try
            {
                dtregions.Columns.Add("FilterId");
                if (InputParam.RegionIds != null)
                {
                    foreach (Int64 regionid in InputParam.RegionIds)
                    {
                        row = dtregions.NewRow();
                        row["FilterId"] = regionid;
                        dtregions.Rows.Add(row);
                    }
                }

                dtstates.Columns.Add("FilterId");
                if (InputParam.StateIds != null)
                {
                    foreach (Int64 stateid in InputParam.StateIds)
                    {
                        row = dtstates.NewRow();
                        row["FilterId"] = stateid;
                        dtstates.Rows.Add(row);
                    }
                }

                SqlParameter[] objDBParam = new SqlParameter[3];
                objDBParam[0] = new SqlParameter("@RegionIds", dtregions);
                objDBParam[1] = new SqlParameter("@StateIds", dtstates);
                objDBParam[2] = new SqlParameter("@Status", InputParam.Status);

                var outputFromSP = _dataHelper.ExecuteProcedure("[dbo].[GetStateGrid]", objDBParam);

                if (outputFromSP.Rows.Count > 0)
                {
                    output = (from state in outputFromSP.AsEnumerable()
                              select new StateGridData
                              {
                                  ID = Convert.ToInt64(state.Field<Int64>("ID")),
                                  StateName = Convert.ToString(state.Field<string>("STATENAME")),
                                  RegionID = Convert.ToInt64(state.Field<Int64>("REGIONID")),
                                  RegionName = Convert.ToString(state.Field<string>("REGIONNAME")),
                                  Status = Convert.ToString(state.Field<string>("STATUS"))
                              });
                }
                else
                    output = new List<StateGridData>();

            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetStateGrid", ex.StackTrace, ex.Message);
            }
            return output;
        }

        public IEnumerable<CityGridData> GetCityGrid(CityFilter InputParam)
        {
            IEnumerable<CityGridData> output = null;
            DataTable dtregions = new DataTable();
            DataTable dtstates = new DataTable();
            DataTable dtcities = new DataTable();
            DataTable dtcitytypes = new DataTable();
            DataRow row;

            try
            {
                dtregions.Columns.Add("FilterId");
                if (InputParam.RegionIds != null)
                {
                    foreach (Int64 regionid in InputParam.RegionIds)
                    {
                        row = dtregions.NewRow();
                        row["FilterId"] = regionid;
                        dtregions.Rows.Add(row);
                    }
                }

                dtstates.Columns.Add("FilterId");
                if (InputParam.StateIds != null)
                {
                    foreach (Int64 stateid in InputParam.StateIds)
                    {
                        row = dtstates.NewRow();
                        row["FilterId"] = stateid;
                        dtstates.Rows.Add(row);
                    }
                }

                dtcities.Columns.Add("FilterId");
                if (InputParam.CityIds != null)
                {
                    foreach (Int64 cityid in InputParam.CityIds)
                    {
                        row = dtcities.NewRow();
                        row["FilterId"] = cityid;
                        dtcities.Rows.Add(row);
                    }
                }

                dtcitytypes.Columns.Add("FilterId");
                if (InputParam.CityTypeIds != null)
                {
                    foreach (Int64 citytypeid in InputParam.CityTypeIds)
                    {
                        row = dtcitytypes.NewRow();
                        row["FilterId"] = citytypeid;
                        dtcitytypes.Rows.Add(row);
                    }
                }

                SqlParameter[] objDBParam = new SqlParameter[5];
                objDBParam[0] = new SqlParameter("@RegionIds", dtregions);
                objDBParam[1] = new SqlParameter("@StateIds", dtstates);
                objDBParam[2] = new SqlParameter("@CityIds", dtcities);
                objDBParam[3] = new SqlParameter("@CityTypeIds", dtcitytypes);
                objDBParam[4] = new SqlParameter("@Status", InputParam.Status);

                var outputFromSP = _dataHelper.ExecuteProcedure("[dbo].[GetCityGrid]", objDBParam);

                if (outputFromSP.Rows.Count > 0)
                {
                    output = (from city in outputFromSP.AsEnumerable()
                              select new CityGridData
                              {
                                  ID = Convert.ToInt64(city.Field<Int64>("ID")),
                                  CityName = Convert.ToString(city.Field<string>("CITYNAME")),
                                  StateName = Convert.ToString(city.Field<string>("STATENAME")),
                                  RegionName = Convert.ToString(city.Field<string>("REGIONNAME")),
                                  CityTypeName = Convert.ToString(city.Field<string>("CITYTYPENAME")),
                                  Status = Convert.ToString(city.Field<string>("STATUS")),
                                  CityTypeId = Convert.ToInt64(city.Field<Int64>("CITYTYPEID")),
                                  StateId = Convert.ToInt64(city.Field<Int64>("STATEID")),
                                  RegionId = Convert.ToInt64(city.Field<Int64>("REGIONID"))
                              });
                }
                else
                    output = new List<CityGridData>();

            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetCityGrid", ex.StackTrace, ex.Message);
            }
            return output;
        }

        public IEnumerable<BranchGridData> GetBranchGrid(BranchFilter InputParam)
        {
            IEnumerable<BranchGridData> output = null;
            DataTable dtbranches = new DataTable();
            DataTable dtbranchcodes = new DataTable();
            DataRow row;

            try
            {
                dtbranches.Columns.Add("FilterId");
                if (InputParam.BranchIds != null)
                {
                    foreach (Int64 branchid in InputParam.BranchIds)
                    {
                        row = dtbranches.NewRow();
                        row["FilterId"] = branchid;
                        dtbranches.Rows.Add(row);
                    }
                }

                dtbranchcodes.Columns.Add("FilterId");
                if (InputParam.BranchCodeIds != null)
                {
                    foreach (Int64 branchcodeid in InputParam.BranchCodeIds)
                    {
                        row = dtbranchcodes.NewRow();
                        row["FilterId"] = branchcodeid;
                        dtbranchcodes.Rows.Add(row);
                    }
                }

                SqlParameter[] objDBParam = new SqlParameter[3];
                objDBParam[0] = new SqlParameter("@BranchIds", dtbranches);
                objDBParam[1] = new SqlParameter("@BranchCodeIds", dtbranchcodes);
                objDBParam[2] = new SqlParameter("@Status", InputParam.Status);

                var outputFromSP = _dataHelper.ExecuteProcedure("[dbo].[GetBranchGrid]", objDBParam);

                if (outputFromSP.Rows.Count > 0)
                {
                    output = (from branch in outputFromSP.AsEnumerable()
                              select new BranchGridData
                              {
                                  ID = Convert.ToInt32(branch.Field<Int64>("ID")),
                                  BranchCode = Convert.ToString(branch.Field<string>("BRANCHCODE")),
                                  BranchName = Convert.ToString(branch.Field<string>("BRANCHNAME")),
                                  Status = Convert.ToString(branch.Field<string>("ISACTIVE"))
                              });
                }
                else
                    output = new List<BranchGridData>();

            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetBranchGrid", ex.StackTrace, ex.Message);
            }
            return output;
        }

        public IEnumerable<ProductCategoryGridData> GetProductCategoryGrid(ProductCategoryFilter InputParam)
        {
            IEnumerable<ProductCategoryGridData> output = null;
            DataTable dtdivisions = new DataTable();
            DataTable dtprocat = new DataTable();
            DataRow row;

            try
            {
                dtprocat.Columns.Add("FilterId");
                if (InputParam.ProductCatIds != null)
                {
                    foreach (Int64 procatid in InputParam.ProductCatIds)
                    {
                        row = dtprocat.NewRow();
                        row["FilterId"] = procatid;
                        dtprocat.Rows.Add(row);
                    }
                }

                dtdivisions.Columns.Add("FilterId");
                if (InputParam.DivisionIds != null)
                {
                    foreach (Int64 divisionid in InputParam.DivisionIds)
                    {
                        row = dtdivisions.NewRow();
                        row["FilterId"] = divisionid;
                        dtdivisions.Rows.Add(row);
                    }
                }

                SqlParameter[] objDBParam = new SqlParameter[3];
                objDBParam[0] = new SqlParameter("@DivisionIds", dtdivisions);
                objDBParam[1] = new SqlParameter("@ProductCatIds", dtprocat);
                objDBParam[2] = new SqlParameter("@Status", InputParam.Status);

                var outputFromSP = _dataHelper.ExecuteProcedure("[dbo].[GetProductCategoryGrid]", objDBParam);

                if (outputFromSP.Rows.Count > 0)
                {
                    output = (from category in outputFromSP.AsEnumerable()
                              select new ProductCategoryGridData
                              {
                                  ID = Convert.ToInt64(category.Field<Int64>("ID")),
                                  DivisionName = Convert.ToString(category.Field<string>("DIVISION")),
                                  DivisionId = Convert.ToString(category.Field<string>("DivisionId")),
                                  Description = Convert.ToString(category.Field<string>("DESCRIPTION")),
                                  CategoryName = Convert.ToString(category.Field<string>("CATEGORYNAME")),
                                  ActiveStatus = Convert.ToString(category.Field<string>("STATUS"))
                              });
                }
                else
                    output = new List<ProductCategoryGridData>();

            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetProductCategoryGrid", ex.StackTrace, ex.Message);
            }
            return output;
        }

        public IEnumerable<ProductSubCategoryGridData> GetSubProductCategoryGrid(SubProductCategoryFilter InputParam)
        {
            IEnumerable<ProductSubCategoryGridData> output = null;
            DataTable dtsubpro = new DataTable();
            DataTable dtprocat = new DataTable();
            DataRow row;

            try
            {
                dtprocat.Columns.Add("FilterId");
                if (InputParam.ProductCatIds != null)
                {
                    foreach (Int64 procatid in InputParam.ProductCatIds)
                    {
                        row = dtprocat.NewRow();
                        row["FilterId"] = procatid;
                        dtprocat.Rows.Add(row);
                    }
                }

                dtsubpro.Columns.Add("FilterId");
                if (InputParam.SubProCatIds != null)
                {
                    foreach (Int64 subproid in InputParam.SubProCatIds)
                    {
                        row = dtsubpro.NewRow();
                        row["FilterId"] = subproid;
                        dtsubpro.Rows.Add(row);
                    }
                }

                SqlParameter[] objDBParam = new SqlParameter[3];
                objDBParam[0] = new SqlParameter("@SubProductCatIds", dtsubpro);
                objDBParam[1] = new SqlParameter("@ProductCatIds", dtprocat);
                objDBParam[2] = new SqlParameter("@Status", InputParam.Status);

                var outputFromSP = _dataHelper.ExecuteProcedure("[dbo].[GetSubProductCategoryGrid]", objDBParam);

                if (outputFromSP.Rows.Count > 0)
                {
                    output = (from subcategory in outputFromSP.AsEnumerable()
                              select new ProductSubCategoryGridData
                              {
                                  ID = Convert.ToInt64(subcategory.Field<Int64>("ID")),
                                  Division = Convert.ToString(subcategory.Field<string>("DIVISION")),
                                  ProductCategoryId = Convert.ToInt64(subcategory.Field<Int64>("CATEGORYID")),
                                  SubCategoryName = Convert.ToString(subcategory.Field<string>("SUBCATEGORYNAME")),
                                  Description = Convert.ToString(subcategory.Field<string>("DESCRIPTION")),
                                  CategoryName = Convert.ToString(subcategory.Field<string>("CATEGORYNAME")),
                                  ActiveStatus = Convert.ToString(subcategory.Field<string>("STATUS"))
                              });
                }
                else
                    output = new List<ProductSubCategoryGridData>();

            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetSubProductCategoryGrid", ex.StackTrace, ex.Message);
            }
            return output;
        }
        #endregion

        public GridOutput<IEnumerable<TrainingGridData>> GetTrainingMasterGrid(GridVariables objGridVariables)
        {
            GridOutput<IEnumerable<TrainingGridData>> output = new GridOutput<IEnumerable<TrainingGridData>>();
            output.draw = objGridVariables.Draw;
            DbParameterCollection objDBParam = GetDBParamFromGrid(ref objGridVariables);
            try
            {
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetTrainingMasterGrid]", objDBParam, CommandType.StoredProcedure);

                if (outputFromSP.Rows.Count > 0)
                {
                    output.data = (from training in outputFromSP.AsEnumerable()
                                   select new TrainingGridData
                                   {
                                       TrainingId = Convert.ToInt64(training.Field<Int64>("TrainingId")),
                                       CourseName = Convert.ToString(training.Field<string>("CourseName")),
                                       TrainerName = Convert.ToString(training.Field<string>("TrainerName")),
                                       ProductCategory = Convert.ToString(training.Field<string>("ProductCategory")),
                                       Channel = Convert.ToString(training.Field<string>("Channel")),
                                       FromDate = Convert.ToString(training.Field<string>("FromDate")),
                                       ToDate = Convert.ToString(training.Field<string>("ToDate")),
                                       Status = Convert.ToBoolean(training.Field<bool>("Status")),
                                       FORMId= Convert.ToInt64(training.Field<Int64>("FORMId"))
                                   });
                }
                else
                    output.data = new List<TrainingGridData>();
                output.recordsFiltered = Convert.ToInt32(outputFromSP.Rows[0]["FilteredCount"]);
                output.recordsTotal = Convert.ToInt32(outputFromSP.Rows[0]["FilteredCount"]);
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetTrainingMasterGrid", ex.StackTrace, ex.Message);
            }
            return output;
        }

        public GridOutput<IEnumerable<FeedbackGridData>> GetFeedbackMasterGrid(GridVariables objGridVariables)
        {
            GridOutput<IEnumerable<FeedbackGridData>> output = new GridOutput<IEnumerable<FeedbackGridData>>();
            output.draw = objGridVariables.Draw;
            DbParameterCollection objDBParam = GetDBParamFromGrid(ref objGridVariables);
            try
            {
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetFeedbackMasterGrid]", objDBParam, CommandType.StoredProcedure);

                if (outputFromSP.Rows.Count > 0)
                {
                    output.data = (from Feedback in outputFromSP.AsEnumerable()
                                   select new FeedbackGridData
                                   {
                                       FormId = Convert.ToInt64(Feedback.Field<Int64>("FormId")),
                                       FormName = Convert.ToString(Feedback.Field<string>("FormName")),
                                       Status = Convert.ToBoolean(Feedback.Field<bool>("Status"))
                                   });
                }
                else
                    output.data = new List<FeedbackGridData>();
                output.recordsFiltered = Convert.ToInt32(outputFromSP.Rows[0]["FilteredCount"]);
                output.recordsTotal = Convert.ToInt32(outputFromSP.Rows[0]["FilteredCount"]);
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetFeedbackMasterGrid", ex.StackTrace, ex.Message);
            }
            return output;
        }

        public IEnumerable<OfferedEmployeeGridData> GetOfferedEmployeeMasterGrid(OfferedEmployeeGridSearchFilters objGridVariables)
        {
            IEnumerable<OfferedEmployeeGridData> output = null;
            SqlParameter[] objDBParam = new SqlParameter[7];
            DataTable dtBranch = new DataTable();
            DataTable dtState = new DataTable();
            DataTable dtCity = new DataTable();
            DataTable dtAgency = new DataTable();
            DataRow dr;
            try
            {
                dtBranch.Columns.Add("FilterId");
                dtState.Columns.Add("FilterId");
                dtCity.Columns.Add("FilterId");
                dtAgency.Columns.Add("FilterId");

                if (objGridVariables.BranchIds != null)
                    foreach (Int64 BranchId in objGridVariables.BranchIds)
                    {
                        dr = dtBranch.NewRow();
                        dr["FilterId"] = BranchId;
                        dtBranch.Rows.Add(dr);
                    }

                if (objGridVariables.StateIds != null)
                    foreach (Int64 StateId in objGridVariables.StateIds)
                    {
                        dr = dtState.NewRow();
                        dr["FilterId"] = StateId;
                        dtState.Rows.Add(dr);
                    }

                if (objGridVariables.CityIds != null)
                    foreach (Int64 CityId in objGridVariables.CityIds)
                    {
                        dr = dtCity.NewRow();
                        dr["FilterId"] = CityId;
                        dtCity.Rows.Add(dr);
                    }

                objDBParam[0] = new SqlParameter("@UserId", objGridVariables.UserId);
                objDBParam[1] = new SqlParameter("@RoleName", objGridVariables.RoleName);
                objDBParam[2] = new SqlParameter("@BranchIds", dtBranch);
                objDBParam[3] = new SqlParameter("@StateIds", dtState);
                objDBParam[4] = new SqlParameter("@CityIds", dtCity);
                objDBParam[5] = new SqlParameter("@LoginId", objGridVariables.LoginId);
                objDBParam[6] = new SqlParameter("@EmployeeName", objGridVariables.EmployeeName);

                var outputFromSP = _dataHelper.ExecuteProcedure("[dbo].[GetOfferedEmployeeMasterGrid]", objDBParam);

                if (outputFromSP.Rows.Count > 0)
                {
                    output = (from employee in outputFromSP.AsEnumerable()
                                   select new OfferedEmployeeGridData
                                   {
                                       RoleName = Convert.ToString(employee.Field<string>("RoleName")),
                                       Id = Convert.ToInt64(employee.Field<Int64>("Id")),
                                       FirstName = Convert.ToString(employee.Field<string>("FirstName")),
                                       LastName = Convert.ToString(employee.Field<string>("LastName")),
                                       LoginId = Convert.ToString(employee.Field<string>("LoginId")),
                                       BranchName = Convert.ToString(employee.Field<string>("BranchName")),
                                       Role = Convert.ToString(employee.Field<string>("Role")),
                                       Status = Convert.ToString(employee.Field<string>("Status")),
                                       StateName = Convert.ToString(employee.Field<string>("StateName")),
                                       CityName = Convert.ToString(employee.Field<string>("CityName")),
                                       AgencyName = Convert.ToString(employee.Field<string>("AgencyName"))
                                       
                                   });
                }
                else
                    output = new List<OfferedEmployeeGridData>();
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetOfferedEmployeeMasterGrid", ex.StackTrace, ex.Message);
            }
            return output;
        }



        public TrainingMasterEditData TrainingDataBYID(TInputModel objTrainingMasterEditData)
        {
            TrainingMasterEditData output = new TrainingMasterEditData();
            //  output.draw = objBroadcastMessageGridData.Draw;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@ID", objTrainingMasterEditData.TrainingId, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetTrainingsProductByTrainingId]", objDBParam, CommandType.StoredProcedure);

                if (outputFromSP.Rows.Count > 0)
                {
                
                    var SData = (from message in outputFromSP.AsEnumerable()
                                 select new TMessageModel
                                 {
                                     ProductCategoryid = Convert.ToInt64(message.Field<Int64>("ProductCategoryid")),
                                     CategoryName = Convert.ToString(message.Field<String>("CategoryName"))

                                 }).ToList();
                    output.ProductCategoryid = (from SFA in SData.AsEnumerable()
                                     select SFA.ProductCategoryid).Distinct().ToList();

                    output.CategoryNames = (from SFA in SData.AsEnumerable()
                                      select SFA.CategoryName).Distinct().ToList();
                }
                else
                    output = new TrainingMasterEditData();

                var outputFromSP1 = _dataHelper.ExecuteDataTable("[dbo].[GetTrainingsChannelByTrainingId]", objDBParam, CommandType.StoredProcedure);

                if (outputFromSP1.Rows.Count > 0)
                {
                  

                    var SData = (from message in outputFromSP1.AsEnumerable()
                                 select new TMessageModel
                                 {
                                     Channelid = Convert.ToInt64(message.Field<Int64>("Channelid")),
                                     ChannelName = Convert.ToString(message.Field<String>("ChannelName")),
                                 }).ToList();
                    output.Channelid = (from SIDID in SData.AsEnumerable()
                                    select SIDID.Channelid).Distinct().ToList();
                    output.ChannelNames = (from SIDNAME in SData.AsEnumerable()
                                      select SIDNAME.ChannelName).Distinct().ToList();
                }
                


                var outputFromSP2 = _dataHelper.ExecuteDataTable("[dbo].[GetTrainingsBranchByTrainingId]", objDBParam, CommandType.StoredProcedure);
                if (outputFromSP2.Rows.Count > 0)
                {
                    var SData = (from message in outputFromSP2.AsEnumerable()
                                 select new TMessageModel
                                 {
                                     BranchIds = Convert.ToInt64(message.Field<Int64>("BranchId")),
                                     BranchName = Convert.ToString(message.Field<string>("BranchName"))
                                 }).ToList();

                    output.BranchIds = (from roleid in SData.AsEnumerable()
                                      select roleid.BranchIds).Distinct().ToList();
                    output.BranchNames = (from rolename in SData.AsEnumerable()
                                        select rolename.BranchName).Distinct().ToList();
                }

            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("TrainingDataBYID", ex.StackTrace, ex.Message);
                output = new TrainingMasterEditData();
            }
            return output;
        }


      

        public GridOutput<IEnumerable<DealerSFAMappingGridData>> GetSFADealerMappingHistoryGrid(DealerSFAFilter objGridVariables)
        {
            GridOutput<IEnumerable<DealerSFAMappingGridData>> output = new GridOutput<IEnumerable<DealerSFAMappingGridData>>();
            output.draw = objGridVariables.draw;
            //DbParameterCollection objDBParam = GetDBParamFromGrid(ref objGridVariables);

            DataTable dtdealer = new DataTable();
            //DataTable dtsfa = new DataTable();
            DataTable dtcity = new DataTable();
            DataTable dtstate = new DataTable();
            DataTable dtbranch = new DataTable();
            DataTable dtloc = new DataTable();
            DataRow row;
            try
            {
                dtdealer.Columns.Add("FilterId");
                if (objGridVariables.DealerIds != null)
                {
                    foreach (Int64 dealerid in objGridVariables.DealerIds)
                    {
                        row = dtdealer.NewRow();
                        row["FilterId"] = dealerid;
                        dtdealer.Rows.Add(row);
                    }
                }

                //dtsfa.Columns.Add("FilterId");
                //if (objGridVariables.SFAIds != null)
                //{
                //    foreach (Int64 sfaid in objGridVariables.SFAIds)
                //    {
                //        row = dtsfa.NewRow();
                //        row["FilterName"] = sfaid;
                //        dtsfa.Rows.Add(row);
                //    }
                //}

                dtcity.Columns.Add("FilterId");
                if (objGridVariables.CityIds != null)
                {
                    foreach (Int64 cityid in objGridVariables.CityIds)
                    {
                        row = dtcity.NewRow();
                        row["FilterId"] = cityid;
                        dtcity.Rows.Add(row);
                    }
                }

                dtstate.Columns.Add("FilterId");
                if (objGridVariables.StateIds != null)
                {
                    foreach (Int64 stateid in objGridVariables.StateIds)
                    {
                        row = dtstate.NewRow();
                        row["FilterId"] = stateid;
                        dtstate.Rows.Add(row);
                    }
                }

                dtbranch.Columns.Add("FilterId");
                if (objGridVariables.BranchIds != null)
                {
                    foreach (Int64 branchid in objGridVariables.BranchIds)
                    {
                        row = dtbranch.NewRow();
                        row["FilterId"] = branchid;
                        dtbranch.Rows.Add(row);
                    }
                }

                dtloc.Columns.Add("FilterId");
                if (objGridVariables.LocationIds != null)
                {
                    foreach (Int64 locid in objGridVariables.LocationIds)
                    {
                        row = dtloc.NewRow();
                        row["FilterId"] = locid;
                        dtloc.Rows.Add(row);
                    }
                }

                SqlParameter[] objDBParam = new SqlParameter[10];
                objDBParam[0] = new SqlParameter("@DealerIds", dtdealer);
                objDBParam[1] = new SqlParameter("@SFAIds", objGridVariables.SFAIds != null ? objGridVariables.SFAIds.Trim() : null);
                objDBParam[2] = new SqlParameter("@CityIds", dtcity);
                objDBParam[3] = new SqlParameter("@StateIds", dtstate);
                objDBParam[4] = new SqlParameter("@BranchIds", dtbranch);
                objDBParam[5] = new SqlParameter("@LocationIds", dtloc);
                objDBParam[6] = new SqlParameter("@PageStart", objGridVariables.start);
                objDBParam[7] = new SqlParameter("@PageSize", objGridVariables.length);
                if (objGridVariables.order != null)
                    objDBParam[8] = new SqlParameter("@SortColumn", objGridVariables.order.FirstOrDefault().column);
                else
                    objDBParam[8] = new SqlParameter("@SortColumn", 0);
                if (objGridVariables.order != null)
                    objDBParam[9] = new SqlParameter("@SortDirection", objGridVariables.order.FirstOrDefault().dir);
                else
                    objDBParam[9] = new SqlParameter("@SortDirection", "asc");

                //if (objGridVariables.search != null)
                //    objDBParam[9] = new SqlParameter("@SearchValue", objGridVariables.search.value);
                //else
                //    objDBParam[9] = new SqlParameter("@SearchValue", "");
                //objDBParam[10] = new SqlParameter("@SearchValue", objGridVariables.search.value);

                var outputFromSP = _dataHelper.ExecuteProcedure("[dbo].[GetSFADealerMappingHistoryGrid]", objDBParam);

                if (outputFromSP.Rows.Count > 0)
                {
                    output.data = (from dealer in outputFromSP.AsEnumerable()
                                   select new DealerSFAMappingGridData
                                   {
                                       ID = Convert.ToInt64(dealer.Field<Int64>("ID")),
                                       BranchId = Convert.ToInt64(dealer.Field<Int64>("BRANCHID")),
                                       BranchName = Convert.ToString(dealer.Field<string>("BRANCHNAME")),
                                       StateId = Convert.ToInt64(dealer.Field<Int64>("STATEID")),
                                       StateName = Convert.ToString(dealer.Field<string>("STATENAME")),
                                       CityId = Convert.ToInt64(dealer.Field<Int64>("CITYID")),
                                       CityName = Convert.ToString(dealer.Field<string>("CITYNAME")),
                                       LocationId = Convert.ToInt64(dealer.Field<Int64>("LOCATIONID")),
                                       LocationName = Convert.ToString(dealer.Field<string>("LOCATIONNAME")),
                                       DealerId = Convert.ToInt64(dealer.Field<Int64>("DEALERID")),
                                       DealerName = Convert.ToString(dealer.Field<string>("DEALERNAME")),
                                       EmployeeId = Convert.ToInt64(dealer.Field<Int64>("EMPLOYEEID")),
                                       SFAName = Convert.ToString(dealer.Field<string>("SFANAME")),
                                       LastModificationDate= Convert.ToString(dealer.Field<string>("LASTMODIFICATIONDATE")),
                                   });
                }
                else
                    output.data = new List<DealerSFAMappingGridData>();
                output.recordsFiltered = Convert.ToInt32(outputFromSP.Rows[0]["FilteredCount"]);
                output.recordsTotal = Convert.ToInt32(outputFromSP.Rows[0]["FilteredCount"]);
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetDealerSFAMappingGrid", ex.StackTrace, ex.Message);
            }
            return output;
        }

        public GridOutput<IEnumerable<SFAGridData>> GetSFABranchChangeHistoryGrid(SFAGridSearchFilters objGridVariables)
        {
            GridOutput<IEnumerable<SFAGridData>> output = new GridOutput<IEnumerable<SFAGridData>>();
            output.draw = objGridVariables.draw;
            SqlParameter[] objDBParam = new SqlParameter[12];
            DataTable dtBranch = new DataTable();
            DataTable dtState = new DataTable();
            DataTable dtCity = new DataTable();
            DataTable dtAgency = new DataTable();
            DataTable dtSFAType = new DataTable();
            DataRow dr;
            try
            {
                dtBranch.Columns.Add("FilterId");
                dtState.Columns.Add("FilterId");
                dtCity.Columns.Add("FilterId");
                dtAgency.Columns.Add("FilterId");
                dtSFAType.Columns.Add("FilterId");

                if (objGridVariables.BranchIds != null)
                    foreach (Int64 BranchId in objGridVariables.BranchIds)
                    {
                        dr = dtBranch.NewRow();
                        dr["FilterId"] = BranchId;
                        dtBranch.Rows.Add(dr);
                    }

                if (objGridVariables.StateIds != null)
                    foreach (Int64 StateId in objGridVariables.StateIds)
                    {
                        dr = dtState.NewRow();
                        dr["FilterId"] = StateId;
                        dtState.Rows.Add(dr);
                    }

                if (objGridVariables.CityIds != null)
                    foreach (Int64 CityId in objGridVariables.CityIds)
                    {
                        dr = dtCity.NewRow();
                        dr["FilterId"] = CityId;
                        dtCity.Rows.Add(dr);
                    }

                if (objGridVariables.AgencyIds != null)
                    foreach (Int64 AgencyId in objGridVariables.AgencyIds)
                    {
                        dr = dtAgency.NewRow();
                        dr["FilterId"] = AgencyId;
                        dtAgency.Rows.Add(dr);
                    }

                if (objGridVariables.SFAType != null)
                    foreach (Int64 sfatypeid in objGridVariables.SFAType)
                    {
                        dr = dtSFAType.NewRow();
                        dr["FilterId"] = sfatypeid;
                        dtSFAType.Rows.Add(dr);
                    }

                objDBParam[0] = new SqlParameter("@PageStart", objGridVariables.start);
                objDBParam[1] = new SqlParameter("@PageSize", objGridVariables.length);
                if (objGridVariables.order != null)
                    objDBParam[2] = new SqlParameter("@SortColumn", objGridVariables.order.FirstOrDefault().column);
                else
                    objDBParam[2] = new SqlParameter("@SortColumn", 0);
                if (objGridVariables.order != null)
                    objDBParam[3] = new SqlParameter("@SortDirection", objGridVariables.order.FirstOrDefault().dir);
                else
                    objDBParam[3] = new SqlParameter("@SortDirection", "asc");

                objDBParam[4] = new SqlParameter("@BranchIds", dtBranch);
                objDBParam[5] = new SqlParameter("@StateIds", dtState);
                objDBParam[6] = new SqlParameter("@CityIds", dtCity);
                objDBParam[7] = new SqlParameter("@AgencyIds", dtAgency);
                objDBParam[8] = new SqlParameter("@Status", objGridVariables.Status);
                objDBParam[9] = new SqlParameter("@LoginId", objGridVariables.LoginId);
                objDBParam[10] = new SqlParameter("@EmployeeName", objGridVariables.EmployeeName);
                objDBParam[11] = new SqlParameter("@SFAType", dtSFAType);

                var outputFromSP = _dataHelper.ExecuteProcedureForDataset("[dbo].[GetSFABranchMapping]", objDBParam);

                if (outputFromSP.Tables[0].Rows.Count > 0)
                {
                    output.data = (from sfa in outputFromSP.Tables[0].AsEnumerable()
                                   select new SFAGridData
                                   {
                                       EmployeeId = Convert.ToInt64(sfa.Field<Int64>("ID")),
                                       FirstName = Convert.ToString(sfa.Field<string>("FIRSTNAME")),
                                       LastName = Convert.ToString(sfa.Field<string>("LASTNAME")),
                                       LoginId = Convert.ToString(sfa.Field<string>("LOGINID")),
                                       BranchName = Convert.ToString(sfa.Field<string>("BRANCHNAME")),
                                       StateName = Convert.ToString(sfa.Field<string>("STATENAME")),
                                       CityName = Convert.ToString(sfa.Field<string>("CITYNAME")),
                                       Role = Convert.ToString(sfa.Field<string>("ROLE")),
                                       AgencyName = Convert.ToString(sfa.Field<string>("AGENCYNAME")),
                                       StatusName = Convert.ToString(sfa.Field<string>("STATUS")),
                                       EmployeeCode = Convert.ToString(sfa.Field<string>("EmployeeCode")),
                                       RegionName = Convert.ToString(sfa.Field<string>("RegionName")),
                                       Division = Convert.ToString(sfa.Field<string>("DivisionName")),
                                       Address = Convert.ToString(sfa.Field<string>("Address")) == "" ?
                                               "" : AMBOEcryption.DecryptData(sfa.Field<string>("Address"), true),
                                       MobileNumber = Convert.ToString(sfa.Field<string>("MobileNumber")),
                                       AltMobileNumber = Convert.ToString(sfa.Field<string>("AlternateNumber")),
                                       EmailId = Convert.ToString(sfa.Field<string>("Email")) == "" ?
                                               "" : AMBOEcryption.DecryptData(sfa.Field<string>("Email"), true),
                                       PANNo = Convert.ToString(sfa.Field<string>("PAN")) == "" ?
                                               "" : AMBOEcryption.DecryptData(sfa.Field<string>("PAN"), true),
                                       DOB = Convert.ToString(sfa.Field<string>("DOB")) == "" ?
                                               "" : AMBOEcryption.DecryptData(sfa.Field<string>("DOB"), true),
                                       DOJ = Convert.ToString(sfa.Field<string>("DOJ")),
                                       DOL = Convert.ToString(sfa.Field<string>("DOL")),
                                       IMEI1 = Convert.ToString(sfa.Field<string>("IMEI1")),
                                       IMEI2 = Convert.ToString(sfa.Field<string>("IMEI2")),
                                       SFALevelName = Convert.ToString(sfa.Field<string>("SFALevel")),
                                       SFAType = Convert.ToString(sfa.Field<string>("SFAType")),
                                       PrimaryCode = Convert.ToString(sfa.Field<string>("PrimaryCode")),
                                       MasterCode = Convert.ToString(sfa.Field<string>("MasterCode")),
                                       DealerCity = Convert.ToString(sfa.Field<string>("DealerCity")),
                                       DealerLocation = Convert.ToString(sfa.Field<string>("DealerLocation")),
                                       Channel = Convert.ToString(sfa.Field<string>("Channel")),
                                       DealerName = Convert.ToString(sfa.Field<string>("DealerName")),
                                       IncentiveCategoryName = Convert.ToString(sfa.Field<string>("CategoryName")),
                                       ShiftName = Convert.ToString(sfa.Field<string>("ShiftName")),
                                   });
                }
                else
                    output.data = new List<SFAGridData>();
                output.recordsFiltered = Convert.ToInt32(outputFromSP.Tables[0].Rows[0]["FilteredCount"]);
                output.recordsTotal = Convert.ToInt32(outputFromSP.Tables[0].Rows[0]["FilteredCount"]);
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetSFAMasterGrid", ex.StackTrace, ex.Message);
            }
            return output;
        }

        public List<QsrReportsGrid> GetReportQSRQuantity(QSRQuantityGet targettoSFA)
        {
            DbParameterCollection objDBParam = new DbParameterCollection();
            List<QsrReportsGrid> targetlist = null;
            try
            {
                objDBParam.Add(new DbParameter("@TargetMonth", targettoSFA.TargetDate, DbType.String));
                objDBParam.Add(new DbParameter("@UserId", targettoSFA.UserId, DbType.Int64));
                objDBParam.Add(new DbParameter("@BranchId", targettoSFA.BranchId, DbType.Int64));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[GetQuantityReport]", objDBParam, CommandType.StoredProcedure);
                if (outputFromSP.Rows.Count > 0)
                {

                    targetlist = (from data in outputFromSP.AsEnumerable()
                                  select new QsrReportsGrid
                                  {
                                      BranchName = Convert.ToString(data.Field<string>("Branch")),
                                      QSRDate = Convert.ToString(data.Field<string>("Date")),
                                      Dealer = Convert.ToString(data.Field<string>("DealerName")),
                                      City = Convert.ToString(data.Field<string>("City")),
                                      Location = Convert.ToString(data.Field<string>("Location")),
                                      PayerName = Convert.ToString(data.Field<string>("PayerName")),
                                      Channel = Convert.ToString(data.Field<string>("Channel")),
                                      DealerState = Convert.ToString(data.Field<string>("DealerState")),
                                      MasterCode = Convert.ToString(data.Field<string>("MasterCode")),
                                      DealerCode = Convert.ToString(data.Field<string>("DealerCode")),
                                      DealerClassification = Convert.ToString(data.Field<string>("DealerClassification")),
                                      SFACode = Convert.ToString(data.Field<string>("SFACode")),
                                      SFAName = Convert.ToString(data.Field<string>("SFAName")),

                                      SFALevel = Convert.ToString(data.Field<string>("SFALevel")),
                                      CompanyName = Convert.ToString(data.Field<string>("CompanyName")),
                                      ProductCategory = Convert.ToString(data.Field<string>("ProductCategory")),
                                      Material = Convert.ToString(data.Field<string>("Material")),

                                      SFAType = Convert.ToString(data.Field<string>("SFAType")),
                                      AmboQuantity = Convert.ToString(data.Field<string>("AmboQuantity")),


                                      QSRQuantity = Convert.ToString(data.Field<string>("QSRQuantity")),
                                      FinalQuantity = Convert.ToString(data.Field<string>("FinalQuantity")),

                                  }).ToList();
                }
            }
            catch (Exception ex)
            {
                _IErrorLogProvider.CreateErrorLog("GetReportQSRQuantity", ex.StackTrace, ex.Message);
            }
            return targetlist;
        }

    }
}