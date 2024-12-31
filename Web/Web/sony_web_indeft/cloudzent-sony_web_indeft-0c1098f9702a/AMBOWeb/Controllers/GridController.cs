using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AMBOModels;
using AMBOModels.MasterMaintenance;
using AMBOModels.Modules;
using APIAccessLayer.INTERFACE;
using AMBOModels.IncentiveManagement;
using System.Collections.Generic;
using System.Linq;
using System;
using static AMBOWeb.Classes.Common;
using APIAccessLayer.Helper;
using System.Data;
using System.ComponentModel;
using ClosedXML.Excel;
using System.IO;  
using AMBOWeb.Classes;
using System.Runtime.Serialization.Formatters.Binary;
using AMBOWeb.Filters;
using AMBOModels.UserManagement;
using AMBOModels.Mappings;
using System.Globalization;

namespace AMBOWeb.Controllers
{
    public class GridController : SonyBaseController
    {
        private readonly IAPIGridData _APIGridData;

        public GridController(IAPIGridData IApiGridData)
        {
            _APIGridData = IApiGridData;
        }

        private GridVariables GetGridVariables(ModelGrid objGridData)
        {
            GridVariables objGridVariables = new GridVariables();
            objGridVariables.Draw = objGridData.draw;
            objGridVariables.PageSize = objGridData.length; 
            objGridVariables.PageStart = objGridData.start;
            objGridVariables.SortColumn = objGridData.order[0].column;
            objGridVariables.SortDirection = objGridData.order[0].dir;
            objGridVariables.SearchValue = objGridData.search.value;
            return objGridVariables;
        }

        [HttpPost]
        public async Task<ActionResult> RegionMasterGrid(ModelGrid objGridData)
        {
            GridVariables objGridVariables = GetGridVariables(objGridData);
            var output = _APIGridData.GetRegionMasterGrid(objGridVariables);
            return Json(output);
        }

        [HttpPost]
        public async Task<ActionResult> StateMasterGrid(ModelGrid objGridData)
        {
            GridVariables objGridVariables = GetGridVariables(objGridData);
            var output = _APIGridData.GetStateMasterGrid(objGridVariables);
            return Json(output);
        }

        [HttpPost]
        public async Task<ActionResult> CityMasterGrid(ModelGrid objGridData)
        {
            GridVariables objGridVariables = GetGridVariables(objGridData);
            var output = _APIGridData.GetCityMasterGrid(objGridVariables);
            return Json(output);
        }

        [HttpPost]
        public async Task<ActionResult> LocationMasterGrid(LocationFilter objGridData)
        {
            //GridVariables objGridVariables = GetGridVariables(objGridData);
            var output = _APIGridData.GetLocationMasterGrid(objGridData);
            return Json(output);
        }

        [HttpPost]
        public async Task<ActionResult> BranchMasterGrid(ModelGrid objGridData)
        {
            GridVariables objGridVariables = GetGridVariables(objGridData);
            var output = _APIGridData.GetBranchMasterGrid(objGridVariables);
            return Json(output);
        }

        [HttpPost]
        public async Task<ActionResult> ProductCategoryMasterGrid(ModelGrid objGridData)
        {
            GridVariables objGridVariables = GetGridVariables(objGridData);
            var output = _APIGridData.GetProductCategoryMasterGrid(objGridVariables);
            return Json(output);
        }

        [HttpPost]
        public async Task<ActionResult> ProductSubCategoryMasterGrid(ModelGrid objGridData)
        {
            GridVariables objGridVariables = GetGridVariables(objGridData);
            var output = _APIGridData.GetProductSubCategoryMasterGrid(objGridVariables);
            return Json(output);
        }

        [HttpPost]
        public async Task<ActionResult> MaterialMasterGrid(MaterialFilter objGridData)
        {
            //GridVariables objGridVariables = GetGridVariables(objGridData.modelgrid);
            var output = _APIGridData.GetMaterialMasterGrid(objGridData);
            return Json(output);
        }

        [HttpPost]
        public async Task<ActionResult> DealerMasterGrid(DealerFilter objGridData)
        {
            //GridVariables objGridVariables = GetGridVariables(objGridData);
            var output = _APIGridData.GetDealerMasterGrid(objGridData);
            return Json(output);
        }

        [HttpPost]
        public async Task<ActionResult> DealerSFAMappingGrid(DealerSFAFilter objGridData)
        {
            //GridVariables objGridVariables = GetGridVariables(objGridData);
            var output = _APIGridData.GetDealerSFAMappingGrid(objGridData);
            return Json(output);
        }

        [HttpPost]
        public async Task<ActionResult> DealerClassificationMappingGrid(ModelGrid objGridData)
        {
            GridVariables objGridVariables = GetGridVariables(objGridData);
            var output = _APIGridData.GetDealerClassificationMappingGrid(objGridVariables);
            return Json(output);
        }

        [HttpPost]
        public async Task<ActionResult> AssetMasterGrid(ModelGrid objGridData)
        {
            GridVariables objGridVariables = GetGridVariables(objGridData);
            var output = _APIGridData.GetAssetMasterGrid(objGridVariables);
            return Json(output);
        }

        [HttpPost]
        public async Task<ActionResult> EmployeeMasterGrid(EmployeeGridSearchFilters objGridData)
        {
            //GridVariables objGridVariables = GetGridVariables(objGridData);
            var output = _APIGridData.GetEmployeeMasterGrid(objGridData);
            return Json(output);
        }

        [HttpPost]
        public async Task<ActionResult> EmployeeStructureMappingGrid(EmployeeStructureMapGridFilters objGridData)
        {
            //GridVariables objGridVariables = GetGridVariables(objGridData);
            var output = _APIGridData.GetEmployeeStructureMappingGrid(objGridData);
            return Json(output);
        }

        [HttpPost]
        public async Task<ActionResult> SalesPICOutletMappingGrid(ModelGrid objGridData)
        {
            GridVariables objGridVariables = GetGridVariables(objGridData);
            var output = _APIGridData.GetSalesPICOutletMappingGrid(objGridVariables);
            return Json(output);
        }

        [HttpPost]
        public async Task<ActionResult> UserBranchChannelPCMappingGrid(UserBranchChannelPCMappingFilter objGridData)
        {
            //GridVariables objGridVariables = GetGridVariables(objGridData);
            var output = _APIGridData.GetUserBranchChannelPCMappingGrid(objGridData);
            return Json(output);
        }

        [HttpPost]
        public async Task<ActionResult> SFAMasterGrid(SFAGridSearchFilters objGridData)
        {
            //GridVariables objGridVariables = GetGridVariables(objGridData);
            var output = _APIGridData.GetSFAMasterGrid(objGridData);
            return Json(output);
        }

        [HttpPost]
        public async Task<ActionResult> ProductCategorySFAMappingGrid(ProdCatSFAMapGridSearchFilters objGridData)
        {
            //GridVariables objGridVariables = GetGridVariables(objGridData);
            var output = _APIGridData.GetProductCategorySFAMappingGrid(objGridData);
            return Json(output);
        }

        [HttpPost]
        public async Task<ActionResult> ChannelMasterGrid(ChannelFilter objGridData)
        {
            //GridVariables objGridVariables = GetGridVariables(objGridData);
            var output = _APIGridData.GetChannelMasterGrid(objGridData);
            return Json(output);
        }

        [HttpPost]
        public async Task<ActionResult> ProductTargetCategoryMasterGrid(ProductTargetCategoryGridFilters objGridData)
        {
            var output = _APIGridData.GetProductTargetCategoryMasterGrid(objGridData);
            return Json(output);
        }
        
        [HttpPost]
        public async Task<ActionResult> GetCompetitorProductMasterGrid(CompetitorProductFilter InputParam)
        {
            var output = _APIGridData.GetCompetitorProductMasterGrid(InputParam);
            return Json(output);
        }

        [HttpPost]
        public async Task<ActionResult> GetCompetitorModelMasterGrid(CompetitorModelFilter InputParam)
        {
            var output = _APIGridData.GetCompetitorModelMasterGrid(InputParam);
            return Json(output);
        }
		
		[HttpPost]
        public async Task<ActionResult> SFAMasterforHRGrid(SFAMasterforHRFilter objGridData)
        {
            //GridVariables objGridVariables = GetGridVariables(objGridData);
            var output = _APIGridData.GetSFAMasterforHR(objGridData);
            return Json(output);
        }

        [HttpPost]
        public async Task<ActionResult> SFASalaryMasterGrid(SFASalaryMasterFilter objGridData)
        {
            //GridVariables objGridVariables = GetGridVariables(objGridData);
            var output = _APIGridData.GetSFASalaryMaster(objGridData);
            return Json(output);
        }

        [HttpPost]
        public async Task<ActionResult> TargetDateSettingGrid(ModelGrid objGridData)
        {
            GridVariables objGridVariables = GetGridVariables(objGridData);
            var output = _APIGridData.GetTargetDateSettingGrid(objGridVariables);            
            return Json(output);
        }

        [HttpPost]
        public async Task<ActionResult> IncentiveTargetCategoryMappingGrid(ModelGrid objGridData)
        {
            GridVariables objGridVariables = GetGridVariables(objGridData);
            var output = _APIGridData.GetIncentiveTargetCategoryMappingGrid(objGridVariables);
            return Json(output);
        }

        [HttpPost]
        public async Task<ActionResult> BaseIncentiveGrid(ModelGrid objGridData)
        {
            GridVariables objGridVariables = GetGridVariables(objGridData);
            //add api call here in future
            var output = _APIGridData.GetBaseIncentiveGrid(objGridVariables);
            return Json(output);
        }

        [HttpPost]
        public async Task<ActionResult> PerPieceIncentiveGrid(ModelGrid objGridData)
        {
            GridVariables objGridVariables = GetGridVariables(objGridData);
            var output = _APIGridData.GetPerPieceIncentiveGrid(objGridVariables);
            return Json(output);
        }

        [HttpPost]
        public ActionResult GetProductCategoryGroup(ProCatGroupFilter objGridData)
        {
            var output = _APIGridData.GetProductCategoryGroup(objGridData);
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> FestivalIncentiveGrid(ModelGrid objGridData)
        {
            GridVariables objGridVariables = GetGridVariables(objGridData);
            var output = _APIGridData.GetFestivalIncentiveGrid(objGridVariables);
            return Json(output);
        }

        [HttpPost]
        public async Task<ActionResult> BroadcastMessageMasterGrid(ModelGrid objGridData,string search1)
        {
            if (search1 != null)
            {
                objGridData.search.value = search1;
            }
            GridVariables objGridVariables = GetGridVariables(objGridData);
            var output = _APIGridData.GetBroadcastMessageMasterGrid(objGridVariables);


            //for (int i = 0; i < output.data.Count(); i++)
            //{
            //    if (!System.IO.File.Exists(Server.MapPath("~/Uploads/Broadcast/" + output.data[i].FileName)))
            //    {
            //        output.data[i].FileName = "";
            //    }
            //}


            return Json(output);
        }        

        [HttpPost]
        public async Task<ActionResult> BroadcastMessageMasterGridBYID(BMessageInputModel objBMessageInputModel)
        {
            //GridVariables objGridVariables = GetGridVariables(objBroadcastMessageGridData);
            var output = _APIGridData.BroadcastMessageMasterGridBYID(objBMessageInputModel);
            return Json(output);
        }

        [HttpPost]
        public async Task<ActionResult> TrainingDataBYID(TInputModel objBMessageInputModel)
        {
            //GridVariables objGridVariables = GetGridVariables(objBroadcastMessageGridData);
            var output = _APIGridData.TrainingDataBYID(objBMessageInputModel);
            return Json(output);
        }
        

        [HttpGet]
        public async Task<ActionResult> GetAssetAssignmentToRDIByReference(string ReferenceNumber)
        {
            AssetAssignmentToRDIGet InputParam = new AssetAssignmentToRDIGet();
            InputParam.Reference = ReferenceNumber;
            //GridVariables objGridVariables = GetGridVariables(objGridData);
            var output = _APIGridData.GetAssetAssignmentToRDIByReference(InputParam);
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> GetRoleRightsMappingGrid()
        {
            var output = _APIGridData.GetRoleRightsMappingGrid();
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> CompetitorMaster(CompetitorFilter InputParam)
        {
            var output = _APIGridData.GetCompetitorMasterGrid(InputParam);
            return Json(output);
        }

        [HttpPost]
        public async Task<ActionResult> SFALevelMaster(SFALevelFilter InputParam)
        {
            var output = _APIGridData.GetSFALevelMasterData(InputParam);
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> GetShiftMaster(ShiftFilter InputParam)
        {
            var output = _APIGridData.GetShiftMaster(InputParam);
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> SFASubLevelMaster(SFASubLevelFilter InputParam)
        {
            var output = _APIGridData.GetSFASubLevelMasterData(InputParam);
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> GetRegionGrid(RegionFilter objGridData)
        {
            var output = _APIGridData.GetRegionGrid(objGridData);
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> GetStateGrid(StateFilter objGridData)
        {
            var output = _APIGridData.GetStateGrid(objGridData);
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> GetCityGrid(CityFilter objGridData)
        {
            var output = _APIGridData.GetCityGrid(objGridData);
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> GetBranchGrid(BranchFilter objGridData)
        {
            var output = _APIGridData.GetBranchGrid(objGridData);
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> GetProductCategoryGrid(ProductCategoryFilter objGridData)
        {
            var output = _APIGridData.GetProductCategoryGrid(objGridData);
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> GetSubProductCategoryGrid(SubProductCategoryFilter objGridData)
        {
            var output = _APIGridData.GetSubProductCategoryGrid(objGridData);
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> TrainingMasterGrid(ModelGrid objGridData)
        {
            GridVariables objGridVariables = GetGridVariables(objGridData);
            var output = _APIGridData.GetTrainingMasterGrid(objGridVariables);
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> FeedbackMasterGrid(ModelGrid objGridData)
        {
            GridVariables objGridVariables = GetGridVariables(objGridData);
            var output = _APIGridData.GetFeedbackMasterGrid(objGridVariables);
            return Json(output, JsonRequestBehavior.AllowGet);
        }


        

        [HttpPost]
        public ActionResult ExportExcelRegion(int ModuleId, RegionFilter InputParam)
        {
            try
            {         
                bool exists = false;
                exists = Enum.IsDefined(typeof(Module), ModuleId);

                DataTable dt = new DataTable(Enum.GetName(typeof(Module), ModuleId));
                string[] headerlist = GetReportHeaders(ModuleId);

                var list = _APIGridData.GetRegionGrid(InputParam).OrderBy(o => o.ID).ToList();
                foreach (var name in headerlist)
                    dt.Columns.Add(name);
                foreach (var record in list)
                {
                    dt.Rows.Add(record.RegionName, record.Status);
                }

                var fileName = Enum.GetName(typeof(Module), ModuleId) + "_Data.xls";
                string fullPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/DownloadTemplate"), fileName);
                //save the file to server temp folder
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
                //fullPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/DownloadTemplate"), fileName);

                if (dt.Rows.Count > 0)
                {
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt, dt.TableName);
                        using (MemoryStream stream = new MemoryStream())
                        {
                            wb.SaveAs(stream);
                            stream.Position = 0;
                            FileStream file = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
                            stream.WriteTo(file);
                            file.Close();

                            //var file= File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", Enum.GetName(typeof(Module), ModuleId) + "Data.xlsx");
                            //return Json(file, JsonRequestBehavior.AllowGet);
                            return Json(new { fileName = fileName, errorMessage = "" });
                        }

                    }


                }
                else
                {
                    return null;
                }


            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        public ActionResult ExportExcelState(int ModuleId, StateFilter InputParam)
        {
            try
            {
                bool exists = false;
                exists = Enum.IsDefined(typeof(Module), ModuleId);

                DataTable dt = new DataTable(Enum.GetName(typeof(Module), ModuleId));
                string[] headerlist = GetReportHeaders(ModuleId);

                var list = _APIGridData.GetStateGrid(InputParam).OrderBy(o => o.ID).ToList();
                foreach (var name in headerlist)
                    dt.Columns.Add(name);
                foreach (var record in list)
                {
                    dt.Rows.Add(record.StateName, record.RegionName, record.Status);
                }

                var fileName = Enum.GetName(typeof(Module), ModuleId) + "_Data.xls";
                string fullPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/DownloadTemplate"), fileName);
                //save the file to server temp folder
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }

                if (dt.Rows.Count > 0)
                {
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt, dt.TableName);
                        using (MemoryStream stream = new MemoryStream())
                        {
                            wb.SaveAs(stream);
                            stream.Position = 0;
                            FileStream file = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
                            stream.WriteTo(file);
                            file.Close();
                            return Json(new { fileName = fileName, errorMessage = "" });
                        }

                    }


                }
                else
                {
                    return null;
                }


            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        public ActionResult ExportExcelCity(int ModuleId, CityFilter InputParam)
        {
            try
            {
                bool exists = false;
                exists = Enum.IsDefined(typeof(Module), ModuleId);

                DataTable dt = new DataTable(Enum.GetName(typeof(Module), ModuleId));
                string[] headerlist = GetReportHeaders(ModuleId);

                var list = _APIGridData.GetCityGrid(InputParam).OrderBy(o => o.ID).ToList();
                foreach (var name in headerlist)
                    dt.Columns.Add(name);
                foreach (var record in list)
                {
                    dt.Rows.Add(record.CityName, record.CityTypeName, record.StateName, record.RegionName, record.Status);
                }

                var fileName = Enum.GetName(typeof(Module), ModuleId) + "_Data.xls";
                string fullPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/DownloadTemplate"), fileName);
                //save the file to server temp folder
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }

                if (dt.Rows.Count > 0)
                {
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt, dt.TableName);
                        using (MemoryStream stream = new MemoryStream())
                        {
                            wb.SaveAs(stream);
                            stream.Position = 0;
                            FileStream file = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
                            stream.WriteTo(file);
                            file.Close();
                            return Json(new { fileName = fileName, errorMessage = "" });
                        }

                    }


                }
                else
                {
                    return null;
                }


            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        public ActionResult ExportExcelLocation(int ModuleId, LocationFilter InputParam)
        {
            try
            {
                bool exists = false;
                exists = Enum.IsDefined(typeof(Module), ModuleId);

                InputParam.length = int.MaxValue;
                InputParam.start = 0;
                InputParam.search = null;
               if (InputParam.order !=null)
                {
                    InputParam.order.FirstOrDefault().column = 0;
                    InputParam.order.FirstOrDefault().dir = "asc";
                }
           

                DataTable dt = new DataTable(Enum.GetName(typeof(Module), ModuleId));
                string[] headerlist = GetReportHeaders(ModuleId);

                var list = _APIGridData.GetLocationMasterGrid(InputParam).data.OrderBy(o => o.ID).ToList();
                foreach (var name in headerlist)
                    dt.Columns.Add(name);
                foreach (var record in list)
                {
                    dt.Rows.Add(record.LocationName, record.CityName, record.StateName, record.RegionName, record.Status);
                }

                var fileName = Enum.GetName(typeof(Module), ModuleId) + "_Data.xls";
                string fullPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/DownloadTemplate"), fileName);
                //save the file to server temp folder
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }

                if (dt.Rows.Count > 0)
                {
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt, dt.TableName);
                        using (MemoryStream stream = new MemoryStream())
                        {
                            wb.SaveAs(stream);
                            stream.Position = 0;
                            FileStream file = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
                            stream.WriteTo(file);
                            file.Close();
                            return Json(new { fileName = fileName, errorMessage = "" });
                        }

                    }


                }
                else
                {
                    return null;
                }


            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        public ActionResult ExportExcelBranch(int ModuleId, BranchFilter InputParam)
        {
            try
            {
                bool exists = false;
                exists = Enum.IsDefined(typeof(Module), ModuleId);

                DataTable dt = new DataTable(Enum.GetName(typeof(Module), ModuleId));
                string[] headerlist = GetReportHeaders(ModuleId);

                var list = _APIGridData.GetBranchGrid(InputParam).OrderBy(o => o.ID).ToList();
                foreach (var name in headerlist)
                    dt.Columns.Add(name);
                foreach (var record in list)
                {
                    dt.Rows.Add(record.BranchCode, record.BranchName, record.Status);
                }

                var fileName = Enum.GetName(typeof(Module), ModuleId) + "_Data.xls";
                string fullPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/DownloadTemplate"), fileName);
                //save the file to server temp folder
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }

                if (dt.Rows.Count > 0)
                {
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt, dt.TableName);
                        using (MemoryStream stream = new MemoryStream())
                        {
                            wb.SaveAs(stream);
                            stream.Position = 0;
                            FileStream file = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
                            stream.WriteTo(file);
                            file.Close();
                            return Json(new { fileName = fileName, errorMessage = "" });
                        }

                    }


                }
                else
                {
                    return null;
                }


            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        public ActionResult ExportExcelProduct(int ModuleId, ProductCategoryFilter InputParam)
        {
            try
            {
                bool exists = false;
                exists = Enum.IsDefined(typeof(Module), ModuleId);

                DataTable dt = new DataTable(Enum.GetName(typeof(Module), ModuleId));
                string[] headerlist = GetReportHeaders(ModuleId);

                var list = _APIGridData.GetProductCategoryGrid(InputParam).OrderBy(o => o.ID).ToList();
                foreach (var name in headerlist)
                    dt.Columns.Add(name);
                foreach (var record in list)
                {
                    dt.Rows.Add(record.CategoryName, record.Description, record.DivisionName, record.ActiveStatus);
                }

                var fileName = Enum.GetName(typeof(Module), ModuleId) + "_Data.xls";
                string fullPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/DownloadTemplate"), fileName);
                //save the file to server temp folder
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }

                if (dt.Rows.Count > 0)
                {
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt, dt.TableName);
                        using (MemoryStream stream = new MemoryStream())
                        {
                            wb.SaveAs(stream);
                            stream.Position = 0;
                            FileStream file = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
                            stream.WriteTo(file);
                            file.Close();
                            return Json(new { fileName = fileName, errorMessage = "" });
                        }

                    }


                }
                else
                {
                    return null;
                }


            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        public ActionResult ExportExcelSubProduct(int ModuleId, SubProductCategoryFilter InputParam)
        {
            try
            {
                bool exists = false;
                exists = Enum.IsDefined(typeof(Module), ModuleId);

                DataTable dt = new DataTable(Enum.GetName(typeof(Module), ModuleId));
                string[] headerlist = GetReportHeaders(ModuleId);

                var list = _APIGridData.GetSubProductCategoryGrid(InputParam).OrderBy(o => o.ID).ToList();
                foreach (var name in headerlist)
                    dt.Columns.Add(name);
                foreach (var record in list)
                {
                    dt.Rows.Add(record.SubCategoryName, record.Description, record.CategoryName, record.Division, record.ActiveStatus);
                }

                var fileName = Enum.GetName(typeof(Module), ModuleId) + "_Data.xls";
                string fullPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/DownloadTemplate"), fileName);
                //save the file to server temp folder
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }

                if (dt.Rows.Count > 0)
                {
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt, dt.TableName);
                        using (MemoryStream stream = new MemoryStream())
                        {
                            wb.SaveAs(stream);
                            stream.Position = 0;
                            FileStream file = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
                            stream.WriteTo(file);
                            file.Close();
                            return Json(new { fileName = fileName, errorMessage = "" });
                        }

                    }


                }
                else
                {
                    return null;
                }


            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        public ActionResult ExportExcelMaterial(int ModuleId, MaterialFilter InputParam)
        {
            try
            {
                bool exists = false;
                exists = Enum.IsDefined(typeof(Module), ModuleId);

                InputParam.length = int.MaxValue;
                InputParam.start = 0;
                InputParam.search = null;
                if (InputParam.order != null)
                {
                    InputParam.order.FirstOrDefault().column = 0;
                    InputParam.order.FirstOrDefault().dir = "asc";
                }

                DataTable dt = new DataTable(Enum.GetName(typeof(Module), ModuleId));
                string[] headerlist = GetReportHeaders(ModuleId);

                var list = _APIGridData.GetMaterialMasterGrid(InputParam).data.OrderBy(o => o.Id).ToList();
                foreach (var name in headerlist)
                    dt.Columns.Add(name);
                foreach (var record in list)
                {
                    dt.Rows.Add(record.MaterialCode, record.Name, record.Description,record.ProductCategory, record.ProductSubCategory, record.MOP, record.ActiveStatus);
                }

                var fileName = Enum.GetName(typeof(Module), ModuleId) + "_Data.xls";
                string fullPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/DownloadTemplate"), fileName);
                //save the file to server temp folder
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }

                if (dt.Rows.Count > 0)
                {
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt, dt.TableName);
                        using (MemoryStream stream = new MemoryStream())
                        {
                            wb.SaveAs(stream);
                            stream.Position = 0;
                            FileStream file = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
                            stream.WriteTo(file);
                            file.Close();
                            return Json(new { fileName = fileName, errorMessage = "" });
                        }

                    }


                }
                else
                {
                    return null;
                }


            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        public ActionResult ExportExcelCompetitor(int ModuleId, CompetitorFilter InputParam)
        {
            try
            {
                bool exists = false;
                exists = Enum.IsDefined(typeof(Module), ModuleId);

                DataTable dt = new DataTable(Enum.GetName(typeof(Module), ModuleId));
                string[] headerlist = GetReportHeaders(ModuleId);

                var list = _APIGridData.GetCompetitorMasterGrid(InputParam).OrderBy(o => o.ID).ToList();
                foreach (var name in headerlist)
                    dt.Columns.Add(name);
                foreach (var record in list)
                {
                    dt.Rows.Add(record.CompetitorName, record.CompetitorName, record.CompetitorStatus);
                }

                var fileName = Enum.GetName(typeof(Module), ModuleId) + "_Data.xls";
                string fullPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/DownloadTemplate"), fileName);
                //save the file to server temp folder
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }

                if (dt.Rows.Count > 0)
                {
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt, dt.TableName);
                        using (MemoryStream stream = new MemoryStream())
                        {
                            wb.SaveAs(stream);
                            stream.Position = 0;
                            FileStream file = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
                            stream.WriteTo(file);
                            file.Close();
                            return Json(new { fileName = fileName, errorMessage = "" });
                        }

                    }


                }
                else
                {
                    return null;
                }


            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        public ActionResult ExportExcelCompetitorProduct(int ModuleId, CompetitorProductFilter InputParam)
        {
            try
            {
                bool exists = false;
                exists = Enum.IsDefined(typeof(Module), ModuleId);

                DataTable dt = new DataTable(Enum.GetName(typeof(Module), ModuleId));
                string[] headerlist = GetReportHeaders(ModuleId);

                var list = _APIGridData.GetCompetitorProductMasterGrid(InputParam).OrderBy(o => o.ID).ToList();
                foreach (var name in headerlist)
                    dt.Columns.Add(name);
                foreach (var record in list)
                {
                    dt.Rows.Add(record.ProductName, record.CompetitorName, record.ProductCategoryName, record.TopModel, record.ProductCategoryName, record.ProductStatus);
                }

                var fileName = Enum.GetName(typeof(Module), ModuleId) + "_Data.xls";
                string fullPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/DownloadTemplate"), fileName);
                //save the file to server temp folder
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }

                if (dt.Rows.Count > 0)
                {
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt, dt.TableName);
                        using (MemoryStream stream = new MemoryStream())
                        {
                            wb.SaveAs(stream);
                            stream.Position = 0;
                            FileStream file = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
                            stream.WriteTo(file);
                            file.Close();
                            return Json(new { fileName = fileName, errorMessage = "" });
                        }

                    }


                }
                else
                {
                    return null;
                }


            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        public ActionResult ExportExcelCompetitorModel(int ModuleId, CompetitorModelFilter InputParam)
        {
            try
            {
                bool exists = false;
                exists = Enum.IsDefined(typeof(Module), ModuleId);

                DataTable dt = new DataTable(Enum.GetName(typeof(Module), ModuleId));
                string[] headerlist = GetReportHeaders(ModuleId);

                var list = _APIGridData.GetCompetitorModelMasterGrid(InputParam).OrderBy(o => o.ID).ToList();
                foreach (var name in headerlist)
                    dt.Columns.Add(name);
                foreach (var record in list)
                {
                    dt.Rows.Add(record.CompetitorCode, record.CompetitorProductCategory, record.SonyProductCategory, record.SonyProductSubCategoryName, record.SonyModel, record.ModelName);
                }

                var fileName = Enum.GetName(typeof(Module), ModuleId) + "_Data.xls";
                string fullPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/DownloadTemplate"), fileName);
                //save the file to server temp folder
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }

                if (dt.Rows.Count > 0)
                {
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt, dt.TableName);
                        using (MemoryStream stream = new MemoryStream())
                        {
                            wb.SaveAs(stream);
                            stream.Position = 0;
                            FileStream file = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
                            stream.WriteTo(file);
                            file.Close();
                            return Json(new { fileName = fileName, errorMessage = "" });
                        }

                    }


                }
                else
                {
                    return null;
                }


            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        public ActionResult ExportExcelProductCategory(int ModuleId, ProCatGroupFilter InputParam)
        {
            try
            {
                bool exists = false;
                exists = Enum.IsDefined(typeof(Module), ModuleId);

                DataTable dt = new DataTable(Enum.GetName(typeof(Module), ModuleId));
                string[] headerlist = GetReportHeaders(ModuleId);

                var list = _APIGridData.GetProductCategoryGroup(InputParam).OrderBy(o => o.ProductIds).ToList();
                foreach (var name in headerlist)
                    dt.Columns.Add(name);
                foreach (var record in list)
                {
                    dt.Rows.Add(record.GroupName, record.DisplayOrder, record.CategoryName);
                }

                var fileName = Enum.GetName(typeof(Module), ModuleId) + "_Data.xls";
                string fullPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/DownloadTemplate"), fileName);
                //save the file to server temp folder
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }

                if (dt.Rows.Count > 0)
                {
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt, dt.TableName);
                        using (MemoryStream stream = new MemoryStream())
                        {
                            wb.SaveAs(stream);
                            stream.Position = 0;
                            FileStream file = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
                            stream.WriteTo(file);
                            file.Close();
                            return Json(new { fileName = fileName, errorMessage = "" });
                        }

                    }


                }
                else
                {
                    return null;
                }


            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        public ActionResult ExportExcelSFA(int ModuleId, SFAGridSearchFilters InputParam)
        {
            try
            {
                bool exists = false;
                exists = Enum.IsDefined(typeof(Module), ModuleId);

                InputParam.length = int.MaxValue;
                InputParam.start = 0;
                InputParam.search = null;
                if (InputParam.order != null)
                {
                    InputParam.order.FirstOrDefault().column = 0;
                    InputParam.order.FirstOrDefault().dir = "asc";
                }

                DataTable dt = new DataTable(Enum.GetName(typeof(Module), ModuleId));
                string[] headerlist = GetReportHeaders(ModuleId);

                var list = _APIGridData.GetSFAMasterGrid(InputParam).data.OrderBy(o => o.EmployeeId).ToList();
                foreach (var name in headerlist)
                    dt.Columns.Add(name);
                foreach (var record in list)
                {
                    dt.Rows.Add( record.FirstName, record.LastName, record.EmployeeCode, /*record.LoginId,*/ record.BranchName, record.RegionName,
                        record.StateName, /*record.CityName,*/ record.Division, record.Role, record.Address, record.MobileNumber,
                        record.AltMobileNumber, record.EmailId, record.PANNo, record.DOB, record.DOJ, record.DOL, record.IMEI1,
                        record.IMEI2, record.SFAType, record.SFALevelName, record.AgencyName, record.StatusName, record.DealerName,
                        record.PrimaryCode, record.MasterCode, record.DealerCity, record.DealerLocation, record.Channel,
                        record.IncentiveCategoryName, record.ShiftName);
                }

                var fileName = Enum.GetName(typeof(Module), ModuleId) + "_Data.xls";
                string fullPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/DownloadTemplate"), fileName);
                //save the file to server temp folder
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }

                if (dt.Rows.Count > 0)
                {
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt, dt.TableName);
                        using (MemoryStream stream = new MemoryStream())
                        {
                            wb.SaveAs(stream);
                            stream.Position = 0;
                            FileStream file = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
                            stream.WriteTo(file);
                            file.Close();
                            return Json(new { fileName = fileName, errorMessage = "" });
                        }

                    }


                }
                else
                {
                    return null;
                }


            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        public ActionResult ExportExcelSFALevel(int ModuleId, SFALevelFilter InputParam)
        {
            try
            {
                bool exists = false;
                exists = Enum.IsDefined(typeof(Module), ModuleId);

                DataTable dt = new DataTable(Enum.GetName(typeof(Module), ModuleId));
                string[] headerlist = GetReportHeaders(ModuleId);

                var list = _APIGridData.GetSFALevelMasterData(InputParam).OrderBy(o => o.Id).ToList();
                foreach (var name in headerlist)
                    dt.Columns.Add(name);
                foreach (var record in list)
                {
                    dt.Rows.Add(record.SFALevelName, record.LevelCreationDate);
                }

                var fileName = Enum.GetName(typeof(Module), ModuleId) + "_Data.xls";
                string fullPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/DownloadTemplate"), fileName);
                //save the file to server temp folder
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }

                if (dt.Rows.Count > 0)
                {
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt, dt.TableName);
                        using (MemoryStream stream = new MemoryStream())
                        {
                            wb.SaveAs(stream);
                            stream.Position = 0;
                            FileStream file = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
                            stream.WriteTo(file);
                            file.Close();
                            return Json(new { fileName = fileName, errorMessage = "" });
                        }

                    }


                }
                else
                {
                    return null;
                }


            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        public ActionResult ExportExcelShift(int ModuleId, ShiftFilter InputParam)
        {
            try
            {
                bool exists = false;
                exists = Enum.IsDefined(typeof(Module), ModuleId);

                DataTable dt = new DataTable(Enum.GetName(typeof(Module), ModuleId));
                string[] headerlist = GetReportHeaders(ModuleId);

                var list = _APIGridData.GetShiftMaster(InputParam).OrderBy(o => o.Id).ToList();
                foreach (var name in headerlist)
                    dt.Columns.Add(name);
                foreach (var record in list)
                {
                    dt.Rows.Add(record.ShiftName, record.CreationDate);
                }

                var fileName = Enum.GetName(typeof(Module), ModuleId) + "_Data.xls";
                string fullPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/DownloadTemplate"), fileName);
                //save the file to server temp folder
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }

                if (dt.Rows.Count > 0)
                {
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt, dt.TableName);
                        using (MemoryStream stream = new MemoryStream())
                        {
                            wb.SaveAs(stream);
                            stream.Position = 0;
                            FileStream file = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
                            stream.WriteTo(file);
                            file.Close();
                            return Json(new { fileName = fileName, errorMessage = "" });
                        }

                    }


                }
                else
                {
                    return null;
                }


            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        public ActionResult ExportExcelSFASubLevel(int ModuleId, SFASubLevelFilter InputParam)
        {
            try
            {
                bool exists = false;
                exists = Enum.IsDefined(typeof(Module), ModuleId);

                DataTable dt = new DataTable(Enum.GetName(typeof(Module), ModuleId));
                string[] headerlist = GetReportHeaders(ModuleId);

                var list = _APIGridData.GetSFASubLevelMasterData(InputParam).OrderBy(o => o.Id).ToList();
                foreach (var name in headerlist)
                    dt.Columns.Add(name);
                foreach (var record in list)
                {
                    dt.Rows.Add(record.SFASubLevelName, record.SFALevelName, record.Description, record.LevelCreationDate);
                }

                var fileName = Enum.GetName(typeof(Module), ModuleId) + "_Data.xls";
                string fullPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/DownloadTemplate"), fileName);
                //save the file to server temp folder
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }

                if (dt.Rows.Count > 0)
                {
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt, dt.TableName);
                        using (MemoryStream stream = new MemoryStream())
                        {
                            wb.SaveAs(stream);
                            stream.Position = 0;
                            FileStream file = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
                            stream.WriteTo(file);
                            file.Close();
                            return Json(new { fileName = fileName, errorMessage = "" });
                        }

                    }


                }
                else
                {
                    return null;
                }


            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        public ActionResult ExportExcelDealerSFA(int ModuleId, DealerSFAFilter InputParam)
        {
            try
            {
                bool exists = false;
                exists = Enum.IsDefined(typeof(Module), ModuleId);

                InputParam.length = int.MaxValue;
                InputParam.start = 0;
                InputParam.search = null;
                if (InputParam.order != null)
                {
                    InputParam.order.FirstOrDefault().column = 0;
                    InputParam.order.FirstOrDefault().dir = "asc";
                }

                DataTable dt = new DataTable(Enum.GetName(typeof(Module), ModuleId));
                string[] headerlist = GetReportHeaders(ModuleId);

                var list = _APIGridData.GetDealerSFAMappingGrid(InputParam).data.OrderBy(o => o.ID).ToList();
                foreach (var name in headerlist)
                    dt.Columns.Add(name);
                foreach (var record in list)
                {
                    dt.Rows.Add(record.SFAName, record.BranchName, record.CityName, record.StateName, record.LocationName, record.DealerName);
                }

                var fileName = Enum.GetName(typeof(Module), ModuleId) + "_Data.xls";
                string fullPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/DownloadTemplate"), fileName);
                //save the file to server temp folder
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }

                if (dt.Rows.Count > 0)
                {
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt, dt.TableName);
                        using (MemoryStream stream = new MemoryStream())
                        {
                            wb.SaveAs(stream);
                            stream.Position = 0;
                            FileStream file = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
                            stream.WriteTo(file);
                            file.Close();
                            return Json(new { fileName = fileName, errorMessage = "" });
                        }

                    }


                }
                else
                {
                    return null;
                }


            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        public ActionResult ExportExcelEmployeeStructure(int ModuleId, EmployeeStructureMapGridFilters InputParam)
        {
            try
            {
                bool exists = false;
                exists = Enum.IsDefined(typeof(Module), ModuleId);

                InputParam.length = int.MaxValue;
                InputParam.start = 0;
                InputParam.search = null;
                if (InputParam.order != null)
                {
                    InputParam.order.FirstOrDefault().column = 0;
                    InputParam.order.FirstOrDefault().dir = "asc";
                }

                DataTable dt = new DataTable(Enum.GetName(typeof(Module), ModuleId));
                string[] headerlist = GetReportHeaders(ModuleId);

                var list = _APIGridData.GetEmployeeStructureMappingGrid(InputParam).data.OrderBy(o => o.ID).ToList();
                foreach (var name in headerlist)
                    dt.Columns.Add(name);
                foreach (var record in list)
                {
                    dt.Rows.Add(record.RDIBranchName, record.RDIName, record.SFAName);
                }

                var fileName = Enum.GetName(typeof(Module), ModuleId) + "_Data.xls";
                string fullPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/DownloadTemplate"), fileName);
                //save the file to server temp folder
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }

                if (dt.Rows.Count > 0)
                {
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt, dt.TableName);
                        using (MemoryStream stream = new MemoryStream())
                        {
                            wb.SaveAs(stream);
                            stream.Position = 0;
                            FileStream file = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
                            stream.WriteTo(file);
                            file.Close();
                            return Json(new { fileName = fileName, errorMessage = "" });
                        }

                    }


                }
                else
                {
                    return null;
                }


            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        public ActionResult ExportExcelProductSFA(int ModuleId, ProdCatSFAMapGridSearchFilters InputParam)
        {
            try
            {
                bool exists = false;
                exists = Enum.IsDefined(typeof(Module), ModuleId);

                InputParam.length = int.MaxValue;
                InputParam.start = 0;
                InputParam.search = null;
                if (InputParam.order != null)
                {
                    InputParam.order.FirstOrDefault().column = 0;
                    InputParam.order.FirstOrDefault().dir = "asc";
                }

                DataTable dt = new DataTable(Enum.GetName(typeof(Module), ModuleId));
                string[] headerlist = GetReportHeaders(ModuleId);

                var list = _APIGridData.GetProductCategorySFAMappingGrid(InputParam).data.OrderBy(o => o.ID).ToList();
                foreach (var name in headerlist)
                    dt.Columns.Add(name);
                foreach (var record in list)
                {
                    dt.Rows.Add(record.BranchName, record.DealerName, record.SFAName, record.SFACode, record.ProductCategoryName, record.DealerChannel, record.City, record.Location);
                }

                var fileName = Enum.GetName(typeof(Module), ModuleId) + "_Data.xls";
                string fullPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/DownloadTemplate"), fileName);
                //save the file to server temp folder
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }

                if (dt.Rows.Count > 0)
                {
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt, dt.TableName);
                        using (MemoryStream stream = new MemoryStream())
                        {
                            wb.SaveAs(stream);
                            stream.Position = 0;
                            FileStream file = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
                            stream.WriteTo(file);
                            file.Close();
                            return Json(new { fileName = fileName, errorMessage = "" });
                        }

                    }


                }
                else
                {
                    return null;
                }


            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        public ActionResult ExportExcelEmployee(int ModuleId, EmployeeGridSearchFilters InputParam)
        {
            try
            {
                bool exists = false;
                exists = Enum.IsDefined(typeof(Module), ModuleId);

                InputParam.length = int.MaxValue;
                InputParam.start = 0;
                InputParam.search = null;
                if (InputParam.order != null)
                {
                    InputParam.order.FirstOrDefault().column = 0;
                    InputParam.order.FirstOrDefault().dir = "asc";
                }

                DataTable dt = new DataTable(Enum.GetName(typeof(Module), ModuleId));
                string[] headerlist = GetReportHeaders(ModuleId);

                var list = _APIGridData.GetEmployeeMasterGrid(InputParam).data.OrderBy(o => o.EmployeeId).ToList();
                foreach (var name in headerlist)
                    dt.Columns.Add(name);
                foreach (var record in list)
                {
                    dt.Rows.Add(record.FirstName, record.LastName, /*record.LoginId,*/ record.EmployeeCode, record.Role, record.BranchName,
                       record.RegionName, record.StateName, record.CityName, record.Address, record.MobileNumber, record.AltMobileNumber, record.PANNo,
                       record.DOB, record.DOJ, record.DOL, record.SFAType, record.SFALevelName,  record.AgencyName, 
                       record.Division, record.StatusName);
                }

                var fileName = Enum.GetName(typeof(Module), ModuleId) + "_Data.xls";
                string fullPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/DownloadTemplate"), fileName);
                //save the file to server temp folder
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }

                if (dt.Rows.Count > 0)
                {
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt, dt.TableName);
                        using (MemoryStream stream = new MemoryStream())
                        {
                            wb.SaveAs(stream);
                            stream.Position = 0;
                            FileStream file = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
                            stream.WriteTo(file);
                            file.Close();
                            return Json(new { fileName = fileName, errorMessage = "" });
                        }

                    }


                }
                else
                {
                    return null;
                }


            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        public ActionResult ExportExcelDealer(int ModuleId, DealerFilter InputParam)
        {
            try
            {
                bool exists = false;
                exists = Enum.IsDefined(typeof(Module), ModuleId);

                InputParam.length = int.MaxValue;
                InputParam.start = 0;
                InputParam.search = null;
                if (InputParam.order != null)
                {
                    InputParam.order.FirstOrDefault().column = 0;
                    InputParam.order.FirstOrDefault().dir = "asc";
                }

                DataTable dt = new DataTable(Enum.GetName(typeof(Module), ModuleId));
                string[] headerlist = GetReportHeaders(ModuleId);

                var list = _APIGridData.GetDealerMasterGrid(InputParam).data.OrderBy(o => o.ID).ToList();
                foreach (var name in headerlist)
                    dt.Columns.Add(name);
                foreach (var record in list)
                {
                    dt.Rows.Add(record.DealerCode, record.DealerName, record.SAPCode, record.MasterCode1, record.MasterCode2,
                        record.PayerCode, record.PayerName, record.OutletType, record.StoreCode, record.CustomerId,

                        record.BranchName, record.LocationName, record.CityName, record.StateName, record.Latitude, record.Longitude, 
                        record.ChannelName, record.Address, record.MobileNumber, record.PSIOutlet1Data, record.PSIOutlet2Data, 
                        record.TIN, record.PAN, record.ContactPerson, record.EmailID, record.OpeningDate, record.ClosingDate, record.ActiveStatus);
                }

                var fileName = Enum.GetName(typeof(Module), ModuleId) + "_Data.xls";
                string fullPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/DownloadTemplate"), fileName);
                //save the file to server temp folder
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }

                if (dt.Rows.Count > 0)
                {
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt, dt.TableName);
                        using (MemoryStream stream = new MemoryStream())
                        {
                            wb.SaveAs(stream);
                            stream.Position = 0;
                            FileStream file = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
                            stream.WriteTo(file);
                            file.Close();
                            return Json(new { fileName = fileName, errorMessage = "" });
                        }

                    }


                }
                else
                {
                    return null;
                }


            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        public ActionResult ExportExcelDealerClassification(int ModuleId, string SearchVal)
        {
            try
            {
                bool exists = false;
                exists = Enum.IsDefined(typeof(Module), ModuleId);

                GridVariables objVariables = new GridVariables();
                objVariables.PageSize = int.MaxValue;
                objVariables.PageStart = 0;
                objVariables.SearchValue = SearchVal;
                objVariables.SortColumn = 0;
                objVariables.SortDirection = "asc";

                DataTable dt = new DataTable(Enum.GetName(typeof(Module), ModuleId));
                string[] headerlist = GetReportHeaders(ModuleId);

                var listDealerClassification = _APIGridData.GetDealerClassificationMappingGrid(objVariables).data.OrderBy(o => o.ID).ToList();
                foreach (var name in headerlist)
                    dt.Columns.Add(name);
                foreach (var record in listDealerClassification)
                {
                    dt.Rows.Add(record.DealerName, record.BranchName, record.ProductCategoryName, record.ClassificationName);
                }

                var fileName = Enum.GetName(typeof(Module), ModuleId) + "_Data.xls";
                string fullPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/DownloadTemplate"), fileName);
                //save the file to server temp folder
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }

                if (dt.Rows.Count > 0)
                {
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt, dt.TableName);
                        using (MemoryStream stream = new MemoryStream())
                        {
                            wb.SaveAs(stream);
                            stream.Position = 0;
                            FileStream file = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
                            stream.WriteTo(file);
                            file.Close();
                            return Json(new { fileName = fileName, errorMessage = "" });
                        }

                    }


                }
                else
                {
                    return null;
                }


            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        public ActionResult ExportExcelChannel(int ModuleId, ChannelFilter InputParam)
        {
            try
            {
                bool exists = false;
                exists = Enum.IsDefined(typeof(Module), ModuleId);

                DataTable dt = new DataTable(Enum.GetName(typeof(Module), ModuleId));
                string[] headerlist = GetReportHeaders(ModuleId);

                var listChannel = _APIGridData.GetChannelMasterGrid(InputParam).OrderBy(o => o.Id).ToList();
                foreach (var name in headerlist)
                    dt.Columns.Add(name);
                foreach (var record in listChannel)
                {
                    dt.Rows.Add(record.ChannelCode, record.ChannelName);
                }

                var fileName = Enum.GetName(typeof(Module), ModuleId) + "_Data.xls";
                string fullPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/DownloadTemplate"), fileName);
                //save the file to server temp folder
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }

                if (dt.Rows.Count > 0)
                {
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt, dt.TableName);
                        using (MemoryStream stream = new MemoryStream())
                        {
                            wb.SaveAs(stream);
                            stream.Position = 0;
                            FileStream file = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
                            stream.WriteTo(file);
                            file.Close();
                            return Json(new { fileName = fileName, errorMessage = "" });
                        }

                    }


                }
                else
                {
                    return null;
                }


            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        public ActionResult ExportExcelProductTargetCategory(int ModuleId, ProductTargetCategoryGridFilters InputParam)
        {
            try
            {
                bool exists = false;
                exists = Enum.IsDefined(typeof(Module), ModuleId);

                InputParam.length = int.MaxValue;
                InputParam.start = 0;
                InputParam.search = null;
                if (InputParam.order != null)
                {
                    InputParam.order.FirstOrDefault().column = 0;
                    InputParam.order.FirstOrDefault().dir = "asc";
                }

                DataTable dt = new DataTable(Enum.GetName(typeof(Module), ModuleId));
                string[] headerlist = GetReportHeaders(ModuleId);

                var list = _APIGridData.GetProductTargetCategoryMasterGrid(InputParam).data.OrderBy(o => o.Id).ToList();
                foreach (var name in headerlist)
                    dt.Columns.Add(name);
                foreach (var record in list)
                {
                    dt.Rows.Add(record.TargetCategory, record.ProductCategory, record.ActiveStatus);
                }

                var fileName = Enum.GetName(typeof(Module), ModuleId) + "_Data.xls";
                string fullPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/DownloadTemplate"), fileName);
                //save the file to server temp folder
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }

                if (dt.Rows.Count > 0)
                {
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt, dt.TableName);
                        using (MemoryStream stream = new MemoryStream())
                        {
                            wb.SaveAs(stream);
                            stream.Position = 0;
                            FileStream file = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
                            stream.WriteTo(file);
                            file.Close();
                            return Json(new { fileName = fileName, errorMessage = "" });
                        }

                    }


                }
                else
                {
                    return null;
                }


            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        public ActionResult ExportExcelIncentiveCategory(int ModuleId, string SearchVal)
        {
            try
            {
                bool exists = false;
                exists = Enum.IsDefined(typeof(Module), ModuleId);

                GridVariables objVariables = new GridVariables();
                objVariables.PageSize = int.MaxValue;
                objVariables.PageStart = 0;
                objVariables.SearchValue = SearchVal;
                objVariables.SortColumn = 0;
                objVariables.SortDirection = "asc";

                DataTable dt = new DataTable(Enum.GetName(typeof(Module), ModuleId));
                string[] headerlist = GetReportHeaders(ModuleId);

                var listIncentiveCategory = _APIGridData.GetIncentiveTargetCategoryMappingGrid(objVariables).data.OrderBy(o => o.IncentiveCategoryId).ToList();
                foreach (var name in headerlist)
                    dt.Columns.Add(name);
                foreach (var record in listIncentiveCategory)
                {
                    dt.Rows.Add(record.IncentiveCategoryName, record.StatusName);
                }

                var fileName = Enum.GetName(typeof(Module), ModuleId) + "_Data.xls";
                string fullPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/DownloadTemplate"), fileName);
                //save the file to server temp folder
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }

                if (dt.Rows.Count > 0)
                {
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt, dt.TableName);
                        using (MemoryStream stream = new MemoryStream())
                        {
                            wb.SaveAs(stream);
                            stream.Position = 0;
                            FileStream file = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
                            stream.WriteTo(file);
                            file.Close();
                            return Json(new { fileName = fileName, errorMessage = "" });
                        }

                    }


                }
                else
                {
                    return null;
                }


            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        public ActionResult ExportExcelBase(int ModuleId)
        {
            try
            {
                GridVariables objVariables = new GridVariables();
                objVariables.PageSize = int.MaxValue;
                objVariables.PageStart = 0;
                objVariables.SearchValue = null;
                objVariables.SortColumn = 0;
                objVariables.SortDirection = "asc";

                bool exists = false;
                exists = Enum.IsDefined(typeof(Module), ModuleId);

                DataTable dt = new DataTable(Enum.GetName(typeof(Module), ModuleId));
                string[] headerlist = GetReportHeaders(ModuleId);

                var listBase = _APIGridData.GetBaseIncentiveGrid(objVariables).data.OrderBy(o => o.TargetCategoryId).ToList();
                foreach (var name in headerlist)
                    dt.Columns.Add(name);
                foreach (var record in listBase)
                {
                    dt.Rows.Add(record.TargetCategory, record.Status);
                }

                var fileName = Enum.GetName(typeof(Module), ModuleId) + "_Data.xls";
                string fullPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/DownloadTemplate"), fileName);
                //save the file to server temp folder
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }

                if (dt.Rows.Count > 0)
                {
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt, dt.TableName);
                        using (MemoryStream stream = new MemoryStream())
                        {
                            wb.SaveAs(stream);
                            stream.Position = 0;
                            FileStream file = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
                            stream.WriteTo(file);
                            file.Close();
                            return Json(new { fileName = fileName, errorMessage = "" });
                        }

                    }


                }
                else
                {
                    return null;
                }


            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        public ActionResult ExportExcelPerPiece(int ModuleId)
        {
            try
            {
                GridVariables objVariables = new GridVariables();
                objVariables.PageSize = int.MaxValue;
                objVariables.PageStart = 0;
                objVariables.SearchValue = null;
                objVariables.SortColumn = 0;
                objVariables.SortDirection = "asc";

                bool exists = false;
                exists = Enum.IsDefined(typeof(Module), ModuleId);

                DataTable dt = new DataTable(Enum.GetName(typeof(Module), ModuleId));
                string[] headerlist = GetReportHeaders(ModuleId);

                var listPerPiece = _APIGridData.GetPerPieceIncentiveGrid(objVariables).data.OrderBy(o => o.SchemeId).ToList();
                foreach (var name in headerlist)
                    dt.Columns.Add(name);
                foreach (var record in listPerPiece)
                {
                    dt.Rows.Add( record.SchemeName, record.Applicability, record.DateFrom, record.DateTo);
                }

                var fileName = Enum.GetName(typeof(Module), ModuleId) + "_Data.xls";
                string fullPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/DownloadTemplate"), fileName);
                //save the file to server temp folder
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }

                if (dt.Rows.Count > 0)
                {
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt, dt.TableName);
                        using (MemoryStream stream = new MemoryStream())
                        {
                            wb.SaveAs(stream);
                            stream.Position = 0;
                            FileStream file = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
                            stream.WriteTo(file);
                            file.Close();
                            return Json(new { fileName = fileName, errorMessage = "" });
                        }

                    }


                }
                else
                {
                    return null;
                }


            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        public ActionResult ExportExcelFestival(int ModuleId)
        {
            try
            {
                GridVariables objVariables = new GridVariables();
                objVariables.PageSize = int.MaxValue;
                objVariables.PageStart = 0;
                objVariables.SearchValue = null;
                objVariables.SortColumn = 0;
                objVariables.SortDirection = "asc";

                bool exists = false;
                exists = Enum.IsDefined(typeof(Module), ModuleId);

                DataTable dt = new DataTable(Enum.GetName(typeof(Module), ModuleId));
                string[] headerlist = GetReportHeaders(ModuleId);

                var listFestival = _APIGridData.GetFestivalIncentiveGrid(objVariables).data.OrderBy(o => o.SchemeId).ToList();
                foreach (var name in headerlist)
                    dt.Columns.Add(name);
                foreach (var record in listFestival)
                {
                    dt.Rows.Add(record.SchemeName, record.CalculateBaseIncentive, record.StartDate, record.EndDate);
                }

                var fileName = Enum.GetName(typeof(Module), ModuleId) + "_Data.xls";
                string fullPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/DownloadTemplate"), fileName);
                //save the file to server temp folder
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }

                if (dt.Rows.Count > 0)
                {
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt, dt.TableName);
                        using (MemoryStream stream = new MemoryStream())
                        {
                            wb.SaveAs(stream);
                            stream.Position = 0;
                            FileStream file = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
                            stream.WriteTo(file);
                            file.Close();
                            return Json(new { fileName = fileName, errorMessage = "" });
                        }

                    }


                }
                else
                {
                    return null;
                }


            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        public ActionResult ExportExcelAsset(int ModuleId)
        {
            try
            {
                GridVariables objVariables = new GridVariables();
                objVariables.PageSize = int.MaxValue;
                objVariables.PageStart = 0;
                objVariables.SearchValue = null;
                objVariables.SortColumn = 0;
                objVariables.SortDirection = "asc";

                bool exists = false;
                exists = Enum.IsDefined(typeof(Module), ModuleId);

                DataTable dt = new DataTable(Enum.GetName(typeof(Module), ModuleId));
                string[] headerlist = GetReportHeaders(ModuleId);

                var list = _APIGridData.GetAssetMasterGrid(objVariables).data.OrderBy(o => o.Id).ToList();
                foreach (var name in headerlist)
                    dt.Columns.Add(name);
                foreach (var record in list)
                {
                    dt.Rows.Add(record.ProductCode, record.ProductName, record.Category, record.Type, record.Status);
                }

                var fileName = Enum.GetName(typeof(Module), ModuleId) + "_Data.xls";
                string fullPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/DownloadTemplate"), fileName);
                //save the file to server temp folder
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }

                if (dt.Rows.Count > 0)
                {
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt, dt.TableName);
                        using (MemoryStream stream = new MemoryStream())
                        {
                            wb.SaveAs(stream);
                            stream.Position = 0;
                            FileStream file = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
                            stream.WriteTo(file);
                            file.Close();
                            return Json(new { fileName = fileName, errorMessage = "" });
                        }

                    }


                }
                else
                {
                    return null;
                }


            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        public ActionResult ExportExcelSFAMasterforHR(int ModuleId, SFAMasterforHRFilter InputParam)
        {
            try
            {
                InputParam.length = int.MaxValue;
                InputParam.start = 0;
                InputParam.search = null;
                if (InputParam.order != null)
                {
                    InputParam.order.FirstOrDefault().column = 0;
                    InputParam.order.FirstOrDefault().dir = "asc";
                }

                bool exists = false;
                exists = Enum.IsDefined(typeof(Module), ModuleId);

                DataTable dt = new DataTable(Enum.GetName(typeof(Module), ModuleId));
                string[] headerlist = GetReportHeaders(ModuleId);

                var list = _APIGridData.GetSFAMasterforHR(InputParam).data.OrderBy(o => o.Id).ToList();
                foreach (var name in headerlist)
                    dt.Columns.Add(name);
                foreach (var record in list)
                {
                    dt.Rows.Add(record.SFACode, record.SFAName, record.SourceName, record.AgencyRef, record.SourceCode, 
                                record.DOL, record.DOJ, record.DOB, record.Gender, record.FatherName, 
                                record.LevelofEducation, record.Education, record.Experience, record.BankName, 
                                record.BankAccountNo, record.Region, record.Branch, record.State, record.City, record.CityType, 
                                record.Location, record.DealerName, record.DealerCode, record.Channel, record.Division, record.SFALevel, 
                                record.SFASubLevel, record.SFAType, record.Agency, record.Address, record.EmailId, record.MobileNumber, 
                                record.ESIAccountNo, record.PFAccountNo, record.MedicalInsuranceNo, record.MICoverageFrom, 
                                record.MICoverageTo, record.PersonalInsuranceNo, record.PICoverageFrom, record.PICoverageTo);
                }

                var fileName = Enum.GetName(typeof(Module), ModuleId) + "_Data.xls";
                string fullPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/DownloadTemplate"), fileName);
                //save the file to server temp folder 
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }   

                if (dt.Rows.Count > 0)
                {
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt, dt.TableName);
                        using (MemoryStream stream = new MemoryStream())
                        {
                            wb.SaveAs(stream);
                            stream.Position = 0;
                            FileStream file = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
                            stream.WriteTo(file);
                            file.Close();
                            return Json(new { fileName = fileName, errorMessage = "" });
                        }

                    }


                }
                else
                {
                    return null;
                }


            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        public ActionResult ExportExcelSFASalaryMaster(int ModuleId, SFASalaryMasterFilter InputParam)
        {
            try
            {
                InputParam.length = int.MaxValue;
                InputParam.start = 0;
                InputParam.search = null;
                if (InputParam.order != null)
                {
                    InputParam.order.FirstOrDefault().column = 0;
                    InputParam.order.FirstOrDefault().dir = "asc";
                }

                bool exists = false;
                exists = Enum.IsDefined(typeof(Module), ModuleId);

                DataTable dt = new DataTable(Enum.GetName(typeof(Module), ModuleId));
                string[] headerlist = GetReportHeaders(ModuleId);

                var list = _APIGridData.GetSFASalaryMaster(InputParam).data.OrderBy(o => o.Id).ToList();
                foreach (var name in headerlist)
                    dt.Columns.Add(name);
                foreach (var record in list)
                {
                    dt.Rows.Add(record.SFAName, record.SFACode, record.Branch, record.State, record.City, record.Division, record.SFALevel,
                                record.Basic, record.HRA, record.Med, record.Conv, record.Other, record.Airtime, record.Insurance);
                }

                var fileName = Enum.GetName(typeof(Module), ModuleId) + "_Data.xls";
                string fullPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/DownloadTemplate"), fileName);
                //save the file to server temp folder
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }

                if (dt.Rows.Count > 0)
                {
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt, dt.TableName);
                        using (MemoryStream stream = new MemoryStream())
                        {
                            wb.SaveAs(stream);
                            stream.Position = 0;
                            FileStream file = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
                            stream.WriteTo(file);
                            file.Close();
                            return Json(new { fileName = fileName, errorMessage = "" });
                        }

                    }


                }
                else
                {
                    return null;
                }


            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //[HttpPost]
        //public ActionResult ExportExcel(int ModuleId, string searchval)
        //{
        //    try
        //    {
        //        GridVariables objVariables = new GridVariables();
        //        objVariables.PageSize = int.MaxValue;
        //        objVariables.PageStart = 0;
        //        objVariables.SearchValue = searchval;
        //        objVariables.SortColumn = 0;
        //        objVariables.SortDirection = "asc";


        //        bool exists = false;
        //        exists = Enum.IsDefined(typeof(Module), ModuleId);



        //        if (exists)
        //        {
        //            DataTable dt = new DataTable(Enum.GetName(typeof(Module), ModuleId));
        //            string[] headerlist = GetReportHeaders(ModuleId);

        //            switch (ModuleId)
        //            {
        //                case (int)Module.Region:

        //                    var listregion =_APIGridData.GetRegionMasterGrid(objVariables).data.OrderBy(o => o.ID).ToList();
        //                    foreach (var name in headerlist)
        //                        dt.Columns.Add(name);
        //                    foreach (var record in listregion)
        //                    {
        //                        dt.Rows.Add(record.ID,record.RegionName,record.Status);
        //                    }
        //                    break;

        //                case (int)Module.Branch:
        //                    var listbranch = _APIGridData.GetBranchMasterGrid(objVariables).data.OrderBy(o => o.ID).ToList();
        //                    foreach (var name in headerlist)
        //                        dt.Columns.Add(name);
        //                    foreach (var record in listbranch)
        //                    {
        //                        dt.Rows.Add(record.ID, record.BranchCode, record.BranchName, record.Status);
        //                    }
        //                    break;

        //                case (int)Module.City:
        //                    var listcity = _APIGridData.GetCityMasterGrid(objVariables).data.OrderBy(o => o.ID).ToList();
        //                    foreach (var name in headerlist)
        //                        dt.Columns.Add(name);
        //                    foreach (var record in listcity)
        //                    {
        //                        dt.Rows.Add(record.ID, record.CityName, record.CityTypeName, record.StateName, record.RegionName, record.Status);
        //                    }
        //                    break;

        //                case (int)Module.Location:
        //                    LocationFilter objloc = new LocationFilter();
        //                    objloc.CityIds = null;
        //                    objloc.LocationIds = null;
        //                    objloc.RegionIds = null;
        //                    objloc.StateIds = null;
        //                    objloc.Status = null;
        //                    objloc.length = int.MaxValue;
        //                    objloc.start = 0;
        //                    objloc.search.value = searchval;
        //                    objloc.order.First().column = 0;
        //                    objloc.order.First().dir = "asc";

        //                    var listlocation = _APIGridData.GetLocationMasterGrid(objloc).data.OrderBy(o => o.ID).ToList();
        //                    foreach (var name in headerlist)
        //                        dt.Columns.Add(name);
        //                    foreach (var record in listlocation)
        //                    {
        //                        dt.Rows.Add(record.ID, record.LocationName, record.CityName, record.StateName, record.RegionName, record.Status);
        //                    }
        //                    break;

        //                case (int)Module.State:
        //                    var liststate = _APIGridData.GetStateMasterGrid(objVariables).data.OrderBy(o => o.ID).ToList();
        //                    foreach (var name in headerlist)
        //                        dt.Columns.Add(name);
        //                    foreach (var record in liststate)
        //                    {
        //                        dt.Rows.Add(record.ID, record.StateName, record.RegionName, record.Status);
        //                    }
        //                    break;

        //                case (int)Module.Product:
        //                    var listProduct = _APIGridData.GetProductCategoryMasterGrid(objVariables).data.OrderBy(o => o.ID).ToList();
        //                    foreach (var name in headerlist)
        //                        dt.Columns.Add(name);
        //                    foreach (var record in listProduct)
        //                    {
        //                        dt.Rows.Add(record.ID, record.CategoryName,record.DivisionName, record.ActiveStatus);
        //                    }
        //                    break;

        //                case (int)Module.SubProduct:
        //                    var listSubProduct = _APIGridData.GetProductSubCategoryMasterGrid(objVariables).data.OrderBy(o => o.ID).ToList();
        //                    foreach (var name in headerlist)
        //                        dt.Columns.Add(name);
        //                    foreach (var record in listSubProduct)
        //                    {
        //                        dt.Rows.Add(record.ID, record.SubCategoryName, record.CategoryName,record.Division, record.ActiveStatus);
        //                    }
        //                    break;

        //                case (int)Module.Material:

        //                    MaterialFilter objmat = new MaterialFilter();
        //                    objmat.MaterialIds = null;
        //                    objmat.ProductCatIds = null;
        //                    objmat.ProductCatIds = null;
        //                    objmat.SubProCatIds = null;
        //                    objmat.Status = null;
        //                    objmat.length = int.MaxValue;
        //                    objmat.start = 0;
        //                    objmat.search.value = searchval;
        //                    objmat.order.First().column = 0;
        //                    objmat.order.First().dir = "asc";

        //                    var listMaterial = _APIGridData.GetMaterialMasterGrid(objmat).data.OrderBy(o => o.Id).ToList();
        //                    foreach (var name in headerlist)
        //                        dt.Columns.Add(name);
        //                    foreach (var record in listMaterial)
        //                    {
        //                        dt.Rows.Add(record.Id, record.MaterialCode,record.Name,record.Description,record.ProductCategory,record.ProductSubCategory,record.MOP, record.ActiveStatus);
        //                    }
        //                    break;

        //                case (int)Module.Competitor:
        //                    CompetitorFilter objcomp = new CompetitorFilter();
        //                    objcomp.CompetitorCodeIds = null;
        //                    objcomp.CompetitorNames = null;
        //                    objcomp.Status = null;
        //                    var listCompetitor = _APIGridData.GetCompetitorMasterGrid(objcomp).OrderBy(o => o.ID).ToList();
        //                    foreach (var name in headerlist)
        //                        dt.Columns.Add(name);
        //                    foreach (var record in listCompetitor)
        //                    {
        //                        dt.Rows.Add(record.ID, record.CompetitorName, record.CompetitorName, record.CompetitorStatus);
        //                    }
        //                    break;

        //                case (int)Module.CompetitorProduct:
        //                    CompetitorProductFilter objcomppro = new CompetitorProductFilter();
        //                    objcomppro.CompetitorIds = null;
        //                    objcomppro.ProductCatIds = null;
        //                    objcomppro.Status = null;
        //                    var listCompetitorProduct = _APIGridData.GetCompetitorProductMasterGrid(objcomppro).OrderBy(o => o.ID).ToList();
        //                    foreach (var name in headerlist)
        //                        dt.Columns.Add(name);
        //                    foreach (var record in listCompetitorProduct)
        //                    {
        //                        dt.Rows.Add(record.ID, record.ProductName, record.CompetitorID, record.SonyProductCategory, record.TopModel, record.CompetitorName,record.ProductCategoryName, record.ProductStatus);
        //                    }
        //                    break;

        //                case (int)Module.CompetitorModel:
        //                    CompetitorModelFilter objcompmodel = new CompetitorModelFilter();
        //                    objcompmodel.CompetitorIds = null;
        //                    objcompmodel.ModelIds = null;
        //                    objcompmodel.ProductCatIds = null;
        //                    objcompmodel.Status = null;
        //                    var listCompetitorModel = _APIGridData.GetCompetitorModelMasterGrid(objcompmodel).OrderBy(o => o.ID).ToList();
        //                    foreach (var name in headerlist)
        //                        dt.Columns.Add(name);
        //                    foreach (var record in listCompetitorModel)
        //                    {
        //                        dt.Rows.Add(record.ID, record.CompetitorCode, record.CompetitorProductCategory,  record.SonyProductCategory, record.SonyProductSubCategoryName,  record.SonyModel, record.ModelName);
        //                    }
        //                    break;

        //                case (int)Module.ProductCategory:
        //                    ProCatGroupFilter objgrp = new ProCatGroupFilter();
        //                    objgrp.GroupIds = null;
        //                    objgrp.Status = null;
        //                    var listProductCategory = _APIGridData.GetProductCategoryGroup(objgrp).OrderBy(o => o.GroupId).ToList();
        //                    foreach (var name in headerlist)
        //                        dt.Columns.Add(name);
        //                    foreach (var record in listProductCategory)
        //                    {
        //                        dt.Rows.Add(record.GroupId, record.GroupName, record.DisplayOrder);
        //                    }
        //                    break;                        

        //                case (int)Module.SFA:
        //                    SFAGridSearchFilters objInput = new SFAGridSearchFilters();
        //                    objInput.start = 0;
        //                    objInput.length = int.MaxValue;
        //                    var listSFA = _APIGridData.GetSFAMasterGrid(objInput).data.OrderBy(o => o.EmployeeId).ToList();
        //                    foreach (var name in headerlist)
        //                        dt.Columns.Add(name);
        //                    foreach (var record in listSFA)
        //                    {
        //                        dt.Rows.Add(record.EmployeeId, record.FirstName, record.LastName, record.LoginId, record.BranchName, record.StateName, record.CityName, record.Role, record.AgencyName, record.Status);
        //                    }
        //                    break;

        //                case (int)Module.SFAlevel:
        //                    SFALevelFilter objsfalevel = new SFALevelFilter();
        //                    objsfalevel.SFALevelIds = null;
        //                    var listSFAlevel = _APIGridData.GetSFALevelMasterData(objsfalevel).OrderBy(o => o.Id).ToList();
        //                    foreach (var name in headerlist)
        //                        dt.Columns.Add(name);
        //                    foreach (var record in listSFAlevel)
        //                    {
        //                        dt.Rows.Add(record.Id, record.SFALevelName, record.LevelCreationDate);
        //                    }
        //                    break;

        //                case (int)Module.SFASubLevel:
        //                    SFASubLevelFilter objsfasub = new SFASubLevelFilter();
        //                    objsfasub.SFALevelIds = null;
        //                    objsfasub.SFASubLevelIds = null;
        //                    var listSFASubLevel = _APIGridData.GetSFASubLevelMasterData(objsfasub).OrderBy(o => o.Id).ToList();
        //                    foreach (var name in headerlist)
        //                        dt.Columns.Add(name);
        //                    foreach (var record in listSFASubLevel)
        //                    {
        //                        dt.Rows.Add(record.Id, record.SFASubLevelName, record.SFALevelName, record.Description,record.LevelCreationDate);
        //                    }
        //                    break;

        //                case (int)Module.DealerSFA:
        //                    DealerSFAFilter objdeasfa = new DealerSFAFilter();
        //                    objdeasfa.BranchIds = null;
        //                    objdeasfa.SFAIds = null;
        //                    objdeasfa.CityIds = null;
        //                    objdeasfa.StateIds = null;
        //                    objdeasfa.BranchIds = null;
        //                    objdeasfa.LocationIds = null;
        //                    objdeasfa.length = int.MaxValue;
        //                    objdeasfa.start = 0;
        //                    objdeasfa.search.value = searchval;
        //                    objdeasfa.order.First().column = 0;
        //                    objdeasfa.order.First().dir = "asc";
        //                    var listDealerSFA = _APIGridData.GetDealerSFAMappingGrid(objdeasfa).data.OrderBy(o => o.ID).ToList();
        //                    foreach (var name in headerlist)
        //                        dt.Columns.Add(name);
        //                    foreach (var record in listDealerSFA)
        //                    {
        //                        dt.Rows.Add(record.ID, record.SFAName, record.BranchName, record.CityName, record.StateName, record.LocationName, record.DealerName);
        //                    }
        //                    break;

        //                case (int)Module.ProductSFA:
        //                    var listProductSFA = _APIGridData.GetProductCategorySFAMappingGrid(new ProdCatSFAMapGridSearchFilters {start = 0, length = int.MaxValue }).data.OrderBy(o => o.ID).ToList();
        //                    foreach (var name in headerlist)
        //                        dt.Columns.Add(name);
        //                    foreach (var record in listProductSFA)
        //                    {
        //                        dt.Rows.Add(record.ID, record.BranchName, record.DealerName, record.SFAName,record.ProductCategoryName);
        //                    }
        //                    break;

        //                case (int)Module.Employee:
        //                    var listEmployee = _APIGridData.GetEmployeeMasterGrid(new EmployeeGridSearchFilters { length=int.MaxValue, start = 0 }).data.OrderBy(o => o.EmployeeId).ToList();
        //                    foreach (var name in headerlist)
        //                        dt.Columns.Add(name);
        //                    foreach (var record in listEmployee)
        //                    {
        //                        dt.Rows.Add(record.EmployeeId, record.FirstName, record.LastName, record.LoginId, record.BranchName,record.Role, record.Status);
        //                    }
        //                    break;

        //                case (int)Module.Dealer:
        //                    DealerFilter objdealer = new DealerFilter();
        //                    objdealer.BranchIds = null;
        //                    objdealer.LocationIds = null;
        //                    objdealer.MasterCodeIds = null;
        //                    objdealer.DealerIds = null;
        //                    objdealer.Status = null;
        //                    objdealer.length = int.MaxValue;
        //                    objdealer.start = 0;
        //                    objdealer.search.value = searchval;
        //                    objdealer.order.First().column = 0;
        //                    objdealer.order.First().dir = "asc";
        //                    var listDealer = _APIGridData.GetDealerMasterGrid(objdealer).data.OrderBy(o => o.ID).ToList();
        //                    foreach (var name in headerlist)
        //                        dt.Columns.Add(name);
        //                    foreach (var record in listDealer)
        //                    {
        //                        dt.Rows.Add(record.ID, record.DealerName, record.SAPCode, record.PayerName,record.OutletType,record.BranchName,record.LocationName, record.ActiveStatus);
        //                    }
        //                    break;

        //                case (int)Module.DealerClassification:
        //                    var listDealerClassification = _APIGridData.GetDealerClassificationMappingGrid(objVariables).data.OrderBy(o => o.ID).ToList();
        //                    foreach (var name in headerlist)
        //                        dt.Columns.Add(name);
        //                    foreach (var record in listDealerClassification)
        //                    {
        //                        dt.Rows.Add(record.ID, record.DealerName, record.BranchName, record.ProductCategoryName, record.ClassificationName);
        //                    }
        //                    break;

        //                case (int)Module.Channel:
        //                    ChannelFilter objchannel = new ChannelFilter();
        //                    objchannel.ChannelCodeIds = null;
        //                    objchannel.ChannelNameIds = null;
        //                    var listChannel = _APIGridData.GetChannelMasterGrid(objchannel).OrderBy(o => o.Id).ToList();
        //                    foreach (var name in headerlist)
        //                        dt.Columns.Add(name);
        //                    foreach (var record in listChannel)
        //                    {
        //                        dt.Rows.Add(record.Id, record.ChannelCode, record.ChannelName);
        //                    }
        //                    break;

        //                case (int)Module.ProductTargetCategory:
        //                    var listProductTargetCategory = _APIGridData.GetProductTargetCategoryMasterGrid(new ProductTargetCategoryGridFilters { length = int.MaxValue, start = 0 }).data.OrderBy(o => o.Id).ToList();
        //                    foreach (var name in headerlist)
        //                        dt.Columns.Add(name);
        //                    foreach (var record in listProductTargetCategory)
        //                    {
        //                        dt.Rows.Add(record.Id, record.TargetCategory, record.ProductCategory, record.ActiveStatus);
        //                    }
        //                    break;

        //                case (int)Module.IncentiveCategory:
        //                    var listIncentiveCategory = _APIGridData.GetIncentiveTargetCategoryMappingGrid(objVariables).data.OrderBy(o => o.IncentiveCategoryId).ToList();
        //                    foreach (var name in headerlist)
        //                        dt.Columns.Add(name);
        //                    foreach (var record in listIncentiveCategory)
        //                    {
        //                        dt.Rows.Add(record.IncentiveCategoryId, record.IncentiveCategoryName, record.StatusName);
        //                    }
        //                    break;

        //                case (int)Module.Base:
        //                    var listBase = _APIGridData.GetBaseIncentiveGrid(objVariables).data.OrderBy(o => o.TargetCategoryId).ToList();
        //                    foreach (var name in headerlist)
        //                        dt.Columns.Add(name);
        //                    foreach (var record in listBase)
        //                    {
        //                        dt.Rows.Add(record.TargetCategoryId, record.TargetCategory, record.Status);
        //                    }
        //                    break;

        //                case (int)Module.PerPiece:
        //                    var listPerPiece = _APIGridData.GetPerPieceIncentiveGrid(objVariables).data.OrderBy(o => o.SchemeId).ToList();
        //                    foreach (var name in headerlist)
        //                        dt.Columns.Add(name);
        //                    foreach (var record in listPerPiece)
        //                    {
        //                        dt.Rows.Add(record.SchemeId, record.SchemeName, record.Applicability, record.DateFrom, record.DateTo);
        //                    }
        //                    break;

        //                case (int)Module.Festival:
        //                    var listFestival = _APIGridData.GetFestivalIncentiveGrid(objVariables).data.OrderBy(o => o.SchemeId).ToList();
        //                    foreach (var name in headerlist)
        //                        dt.Columns.Add(name);
        //                    foreach (var record in listFestival)
        //                    {
        //                        dt.Rows.Add(record.SchemeId, record.SchemeName, record.CalculateBaseIncentive, record.StartDate, record.EndDate);
        //                    }
        //                    break;

        //                case (int)Module.Asset:
        //                    var listAsset = _APIGridData.GetAssetMasterGrid(objVariables).data.OrderBy(o => o.Id).ToList();
        //                    foreach (var name in headerlist)
        //                        dt.Columns.Add(name);
        //                    foreach (var record in listAsset)
        //                    {
        //                        dt.Rows.Add(record.Id, record.ProductCode, record.ProductName, record.Category, record.Type, record.Status);
        //                    }
        //                    break;
        //            }
        //            var fileName = Enum.GetName(typeof(Module), ModuleId) + "_Data.xls";
        //            string fullPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/DownloadTemplate"), fileName);
        //            //save the file to server temp folder
        //            if (System.IO.File.Exists(fullPath))
        //            {
        //                System.IO.File.Delete(fullPath);
        //            }
        //            //fullPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/DownloadTemplate"), fileName);

        //            if (dt.Rows.Count > 0)
        //            {
        //                using (XLWorkbook wb = new XLWorkbook())
        //                {
        //                    wb.Worksheets.Add(dt, dt.TableName);
        //                    using (MemoryStream stream = new MemoryStream())
        //                    {
        //                        wb.SaveAs(stream);
        //                        stream.Position = 0;
        //                        FileStream file = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
        //                        stream.WriteTo(file);
        //                        file.Close();

        //                        //var file= File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", Enum.GetName(typeof(Module), ModuleId) + "Data.xlsx");
        //                        //return Json(file, JsonRequestBehavior.AllowGet);
        //                        return Json(new { fileName = fileName, errorMessage = "" });
        //                    }

        //                }


        //            }
        //            else
        //            {
        //                return null;
        //            }

        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        return null;
        //    }
        //}

        [HttpGet]
        [DeleteFileAttribute]
        public ActionResult Download(string file)
        {
            //string fullPath = Path.Combine(Server.MapPath("~/DownloadTemplate"), file);
            string fullPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/DownloadTemplate"), file); 
            //return the file for download, this is an Excel 
            //so I set the file content type to "application/vnd.ms-excel"
            return File(fullPath, "application/vnd.ms-excel", file);
        }

      
        [HttpPost]
        public async Task<ActionResult> SFADealerMappingHistoryGrid(DealerSFAFilter objGridData)
        {
            //GridVariables objGridVariables = GetGridVariables(objGridData);
            var output = _APIGridData.GetSFADealerMappingHistoryGrid(objGridData);
            return Json(output);
        }

        [HttpPost]
        public async Task<ActionResult> SFABranchChangeHistoryGrid(SFAGridSearchFilters objGridData)
        {
            //GridVariables objGridVariables = GetGridVariables(objGridData);
            var output = _APIGridData.GetSFABranchChangeHistoryGrid(objGridData);
            return Json(output);
        }



    }

}