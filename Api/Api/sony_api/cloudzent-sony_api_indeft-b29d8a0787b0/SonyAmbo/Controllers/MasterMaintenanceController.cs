using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using AmboServices.Interface;
using AmboUtilities;
using AmboLibrary.MasterMaintenance;
using AmboUtilities.Helper;
using System.Web;
using System.IO;
using System.Drawing;
using AmboLibrary.WebReports;

namespace SonyAmbo.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [ExceptionHandling]
    [Compression]
    public class MasterMaintenanceController : ApiController
    {
        private readonly IMasterMaintenanceService _IMasterMaintenanceService;
        
        /// <summary>
        /// Constructor
        /// </summary>
        public MasterMaintenanceController(IMasterMaintenanceService IMasterMaintenanceService)
        {
            _IMasterMaintenanceService = IMasterMaintenanceService;
        }

        /// <summary>
        /// Test Method
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult ValidateMethod()
        {
            return Ok();
        }

        #region Region Master API
        /// <summary>
        /// Get all active regions.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetAllRegions()
        {
            var output = _IMasterMaintenanceService.GetAllRegions();
            return Ok(output);
        }

        /// <summary>
        /// Get region data on basis of region ID.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetRegionByID(Envelope<RegionMaster> param)
        {
            var output = _IMasterMaintenanceService.GetRegionById(param.Data.ID);
            return Ok(output);
        }

        /// <summary>
        /// Create a new region. 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult CreateRegion(Envelope<RegionMaster> param)
        {
            var output = _IMasterMaintenanceService.CreateRegion(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// Delete an existing region.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DeleteRegion(Envelope<RegionMaster> param)
        {
            var output = _IMasterMaintenanceService.DeleteRegion(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// Update an existing region in the system.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult UpdateRegion(Envelope<RegionMaster> param)
        {
            var output = _IMasterMaintenanceService.UpdateRegion(param.Data);
            return Ok(output);
        }
        #endregion Region Master API

        #region State Master API
        /// <summary>
        /// Create a new state which is mapped to an existing region.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult CreateState(Envelope<CreateStateForm> param)
        {
            var output = _IMasterMaintenanceService.CreateState(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// Get all states from all regions.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetAllStates()
        {
            var output = _IMasterMaintenanceService.GetAllStates();
            return Ok(output);
        }

        /// <summary>
        /// Delete an existing state if the state is not mapped to any city/location.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DeleteState(Envelope<DeleteStateForm> param)
        {
            var output = _IMasterMaintenanceService.DeleteState(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// Get state data on basis of its ID.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetStateByID(Envelope<StateMaster> param)
        {
            var output = _IMasterMaintenanceService.GetStateById(param.Data.ID);
            return Ok(output);
        }

        /// <summary>
        /// Update an existing state.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult UpdateState(Envelope<UpdateStateForm> param)
        {
            var output = _IMasterMaintenanceService.UpdateState(param.Data);
            return Ok(output);
        }
        #endregion State Master API

        #region Branch Master API
        /// <summary>
        /// Get All Branch Details 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetAllBranch()
        {
            var getList = _IMasterMaintenanceService.GetAllBranchDetails();
            return Ok(getList);
        }


        /// <summary>
        /// Method to Add New Brach 
        /// Code not final. We need to Validate the User based on its Id
        /// </summary>
        /// <param name="List"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult AddNewBranch(Envelope<CreateBranchForm> List)
        {
            var getList = _IMasterMaintenanceService.AddNewBrachMaster(List.Data);
            return Ok(getList);
        }

        /// <summary>
        /// Method to Delete respective Branch
        /// Code not final. We need to Validate the User based on its Id
        /// </summary>
        /// <param name="List"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DeleteBranch(Envelope<DeleteBranchForm> List)
        {
            var getList = _IMasterMaintenanceService.DeleteBrach(List.Data);
            return Ok(getList);
        }

        /// <summary>
        /// Method to Update respective Branch
        /// Code not final. We need to Validate the User based on its Id
        /// </summary>
        /// <param name="List"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult UpdateBranch(Envelope<UpdateBranchForm> List)
        {
            var getList = _IMasterMaintenanceService.UpdateBrachMaster(List.Data);
            return Ok(getList);
        }
        #endregion Branch Master API

        #region City Master API
        /// <summary>
        /// Get all cities from all states.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetAllCities()
        {
            var output = _IMasterMaintenanceService.GetAllCities();
            return Ok(output);
        }

        /// <summary>
        /// Get all data related to a city on basis of it's ID.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetCityByID(Envelope<CityMaster> param)
        {
            var output = _IMasterMaintenanceService.GetCityById(param.Data.ID);
            return Ok(output);
        }

        /// <summary>
        /// Create a new city within a state.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult CreateCity(Envelope<CreateCityForm> param)
        {
            var output = _IMasterMaintenanceService.CreateCity(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// Delete an existing city if it is not mapped to any location/branch.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DeleteCity(Envelope<DeleteCityForm> param)
        {
            var output = _IMasterMaintenanceService.DeleteCity(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// Update an existing city in the system.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult UpdateCity(Envelope<UpdateCityForm> param)
        {
            var output = _IMasterMaintenanceService.UpdateCity(param.Data);
            return Ok(output);
        }
        #endregion City Master API

        #region Location Master API
        /// <summary>
        /// Get all locations from all cities.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetAllLocations()
        {
            var output = _IMasterMaintenanceService.GetAllLocations();
            return Ok(output);
        }

        /// <summary>
        /// Get data related to a specific location on basis of it's ID.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetLocationByID(Envelope<LocationMaster> param)
        {
            var output = _IMasterMaintenanceService.GetLocationById(param.Data.ID);
            return Ok(output);
        }

        /// <summary>
        /// Create a new location inside a city.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult CreateLocation(Envelope<CreateLocationForm> param)
        {
            var output = _IMasterMaintenanceService.CreateLocation(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// Delete a location if no dealer or employee is mapped to it.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DeleteLocation(Envelope<DeleteLocationForm> param)
        {
            var output = _IMasterMaintenanceService.DeleteLocation(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// Update an existing location.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult UpdateLocation(Envelope<UpdateLocationForm> param)
        {
            var output = _IMasterMaintenanceService.UpdateLocation(param.Data);
            return Ok(output);
        }
        #endregion Location Master API

        #region Product Category Master API
        /// <summary>
        /// Gets all product categories from all divisions.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetAllProductCategories()
        {
            var output = _IMasterMaintenanceService.GetAllProductCategories();
            return Ok(output);
        }

        /// <summary>
        /// Get data specific to a product category on basis of it's ID.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetProductCategoryByID(Envelope<ProductCategoryMaster> param)
        {
            var output = _IMasterMaintenanceService.GetProductCategoryById(param.Data.ID);
            return Ok(output);
        }

        /// <summary>
        /// Create a new product category for a division.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult CreateProductCategory(Envelope<CreateProductCategoryForm> param)
        {
            var output = _IMasterMaintenanceService.CreateProductCategory(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// Delete a product category if it is not mapped to any SFA / Material / Product Sub category and no target is set for it.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DeleteProductCategory(Envelope<DeleteProductCategoryForm> param)
        {
            var output = _IMasterMaintenanceService.DeleteProductCategory(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// Update an existing product category in the system.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult UpdateProductCategory(Envelope<UpdateProductCategoryForm> param)
        {
            var output = _IMasterMaintenanceService.UpdateProductCategory(param.Data);
            return Ok(output);
        }
        #endregion Product Category Master API

        #region Product Sub Category Master API
        /// <summary>
        /// Get all product sub categories from all product categories.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetAllProductSubCategories()
        {
            var output = _IMasterMaintenanceService.GetAllProductSubCategories();
            return Ok(output);
        }

        /// <summary>
        /// Get all data related to a product sub category on basis of it's ID.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetProductSubCategoryByID(Envelope<ProductSubCategoryMaster> param)
        {
            var output = _IMasterMaintenanceService.GetProductSubCategoryById(param.Data.ID);
            return Ok(output);
        }

        /// <summary>
        /// Create a new product sub category which is mapped to a product category.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult CreateProductSubCategory(Envelope<ProductSubCategoryMaster> param)
        {
            var output = _IMasterMaintenanceService.CreateProductSubCategory(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// Delete a product sub category if it is not mapped to any active material.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DeleteProductSubCategory(Envelope<ProductSubCategoryMaster> param)
        {
            var output = _IMasterMaintenanceService.DeleteProductSubCategory(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// Update an existing product sub category in the system.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult UpdateProductSubCategory(Envelope<ProductSubCategoryMaster> param)
        {
            var output = _IMasterMaintenanceService.UpdateProductSubCategory(param.Data);
            return Ok(output);
        }
        #endregion Product Sub Category Master API

        #region Material Master API
        /// <summary>
        /// Get all materials from all product categories and sub categories.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetAllMaterials()
        {
            var output = _IMasterMaintenanceService.GetAllMaterials();
            return Ok(output);
        }

        /// <summary>
        /// Get data related to a specific material on basis of it's ID.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetMaterialByID(Envelope<MaterialMaster> param)
        {
            var output = _IMasterMaintenanceService.GetMaterialById(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// Create a new material inside a product category and sub category.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult CreateMaterial(Envelope<MaterialMaster> param)
        {
            var output = _IMasterMaintenanceService.CreateMaterial(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// Delete a material from the system.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DeleteMaterial(Envelope<MaterialMaster> param)
        {
            var output = _IMasterMaintenanceService.DeleteMaterial(param.Data);
            return Ok(output);
        }
		/// <summary>
        /// Get data related to a specific material on basis of it's material code.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetMaterialByMaterialCode(Envelope<MaterialMaster> param)
        {
            var output = _IMasterMaintenanceService.GetMaterialByMaterialCode(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// Update an existing material in the system.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult UpdateMaterial(Envelope<MaterialMaster> param)
        {
            var output = _IMasterMaintenanceService.UpdateMaterial(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// Get Material Id on the basis of Material Code
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetMaterialIdByMaterialCode(Envelope<MaterialCodeGet> param)
        {
            var output = _IMasterMaintenanceService.GetMaterialIdByMaterialCode(param.Data);
            return Ok(output);
        }        
        #endregion Material Master API

        #region Channel Master API
        /// <summary>
        /// Get all channels saved in the system.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetAllChannels()
        {
            var output = _IMasterMaintenanceService.GetAllChannels();
            return Ok(output);
        }

        /// <summary>
        /// Get all data related to a specific channel on basis of it's ID.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetChannelByID(Envelope<ChannelMaster> param)
        {
            var output = _IMasterMaintenanceService.GetChannelById(param.Data.Id);
            return Ok(output);
        }

        /// <summary>
        /// Create a new channel in the system.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult CreateChannel(Envelope<ChannelMaster> param)
        {
            var output = _IMasterMaintenanceService.CreateChannel(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// Delete a channel if it is not mapped to any active dealer.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DeleteChannel(Envelope<ChannelMaster> param)
        {
            var output = _IMasterMaintenanceService.DeleteChannel(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// Update an existing channel in the system.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult UpdateChannel(Envelope<ChannelMaster> param)
        {
            var output = _IMasterMaintenanceService.UpdateChannel(param.Data);
            return Ok(output);
        }
        #endregion Channel Master API

        #region SFA Level Master API
        /// <summary>
        /// Get all SFA levels defined in the system.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetAllSFALevels()
        {
            var output = _IMasterMaintenanceService.GetAllSFALevels();
            return Ok(output);
        }

        /// <summary>
        /// Get a specific SFA level on basis of it's ID.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetSFALevelByID(Envelope<SFALevelMaster> param)
        {
            var output = _IMasterMaintenanceService.GetSFALevelById(param.Data.Id);
            return Ok(output);
        }

        /// <summary>
        /// Define a new SFA level in the system.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult CreateSFALevel(Envelope<SFALevelMaster> param)
        {
            var output = _IMasterMaintenanceService.CreateSFALevel(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// Delete an SFA level definition if it is not assigned to any active employee and if no sub level is present in the system which is related to that SFA level.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DeleteSFALevel(Envelope<SFALevelInput> param)
        {
            var output = _IMasterMaintenanceService.DeleteSFALevel(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// Update an SFA level definition in the system.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult UpdateSFALevel(Envelope<SFALevelMaster> param)
        {
            var output = _IMasterMaintenanceService.UpdateSFALevel(param.Data);
            return Ok(output);
        }
        #endregion SFA Level Master API

        #region SFA Sub Level Master API
        /// <summary>
        /// Get all SFA sub levels from all SFA levels.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetAllSFASubLevels()
        {
            var output = _IMasterMaintenanceService.GetAllSFASubLevels();
            return Ok(output);
        }

        /// <summary>
        /// Get all data related to a specific SFA sub level on basis of it's ID.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetSFASubLevelByID(Envelope<SFASubLevelMaster> param)
        {
            var output = _IMasterMaintenanceService.GetSFASubLevelById(param.Data.Id);
            return Ok(output);
        }

        /// <summary>
        /// Create a new SFA sub level inside an SFA level.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult CreateSFASubLevel(Envelope<SFASubLevelMaster> param)
        {
            var output = _IMasterMaintenanceService.CreateSFASubLevel(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// Delete an SFA sub level if it is not mapped to any active employee.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DeleteSFASubLevel(Envelope<SFASubLevelInput> param)
        {
            var output = _IMasterMaintenanceService.DeleteSFASubLevel(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// Update an existing SFA sub level in the system.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult UpdateSFASubLevel(Envelope<SFASubLevelMaster> param)
        {
            var output = _IMasterMaintenanceService.UpdateSFASubLevel(param.Data);
            return Ok(output);
        }
        #endregion SFA Sub Level Master API

        #region Competitor Master API
        /// <summary>
        /// Get all competitor companies which have been defined in the system.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetAllCompetitors()
        {
            var output = _IMasterMaintenanceService.GetAllCompetitors();
            return Ok(output);
        }

        /// <summary>
        /// Get data related to a specific competitor company on basis of it's ID.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetCompetitorByID(Envelope<CompetitorMaster> param)
        {
            var output = _IMasterMaintenanceService.GetCompetitorById(param.Data.ID);
            return Ok(output);
        }

        /// <summary>
        /// Create a new competitor company record in the system.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult CreateCompetitor(Envelope<CompetitorMaster> param)
        {
            var output = _IMasterMaintenanceService.CreateCompetitor(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// Delete a competitor company if it is not mapped to any competitor product.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DeleteCompetitor(Envelope<CompetitorMaster> param)
        {
            var output = _IMasterMaintenanceService.DeleteCompetitor(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// Update an existing competitor company in the system.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult UpdateCompetitor(Envelope<CompetitorMaster> param)
        {
            var output = _IMasterMaintenanceService.UpdateCompetitor(param.Data);
            return Ok(output);
        }
        #endregion Competitor Master API

        #region Competitor Product Master API
        /// <summary>
        /// Get all competitor products related to all competitor companies.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetAllCompetitorProducts()
        {
            var output = _IMasterMaintenanceService.GetAllCompetitorProducts();
            return Ok(output);
        }

        /// <summary>
        /// Get data related to a specific competitor product on the basis of it's ID.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetCompetitorProductByID(Envelope<CompetitorProductMaster> param)
        {
            var output = _IMasterMaintenanceService.GetCompetitorProductById(param.Data.ID);
            return Ok(output);
        }

        /// <summary>
        /// Create a new product for a competitor.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult CreateCompetitorProduct(Envelope<CompetitorProductMaster> param)
        {
            var output = _IMasterMaintenanceService.CreateCompetitorProduct(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// Delete a competitor product if no model is mapped to it.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DeleteCompetitorProduct(Envelope<CompetitorProductMaster> param)
        {
            var output = _IMasterMaintenanceService.DeleteCompetitorProduct(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// Update an existing competitor product in the system.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult UpdateCompetitorProduct(Envelope<CompetitorProductMaster> param)
        {
            var output = _IMasterMaintenanceService.UpdateCompetitorProduct(param.Data);
            return Ok(output);
        }
        #endregion Competitor Product Master API

        #region Competitor Model Master API
        /// <summary>
        /// Get all competitor models registered in the system.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetAllCompetitorModels()
        {
            var output = _IMasterMaintenanceService.GetAllCompetitorModels();
            return Ok(output);
        }

        /// <summary>
        /// Get data related to a specific competitor model on the basis of it's ID.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetCompetitorModelByID(Envelope<CompetitorDataInput> param)
        {
            var output = _IMasterMaintenanceService.GetCompetitorModelById(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// Create a new competitor model for a competitor product.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult CreateCompetitorModel(Envelope<CompetitorModelMaster> param)
        {
            var output = _IMasterMaintenanceService.CreateCompetitorModel(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// Delete a competitor model from the system.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DeleteCompetitorModel(Envelope<CompetitorModelMaster> param)
        {
            var output = _IMasterMaintenanceService.DeleteCompetitorModel(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// Update an existing competitor model in the system.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult UpdateCompetitorModel(Envelope<CompetitorModelMaster> param)
        {
            var output = _IMasterMaintenanceService.UpdateCompetitorModel(param.Data);
            return Ok(output);
        }
        #endregion Competitor Model Master API

        #region Dealer Master API
        /// <summary>
        /// Get all Dealers saved in the system.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetAllDealers()
        {
            var output = _IMasterMaintenanceService.GetAllDealers();
            return Ok(output);
        }
        /// <summary>
        /// Get Dealer Code.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetDealerCode(Envelope<PayerDetails> param)
        {
            var output = _IMasterMaintenanceService.GetDealerCode(param.Data.SAPCode);
            return Ok(output);
        }
        /// <summary>
        /// Get all data related to a specific Dealer on basis of it's ID.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetDealerByID(Envelope<DealerMaster> param)
        {
            var output = _IMasterMaintenanceService.GetDealerById(param.Data.ID, param.Data.DealerCode);
            return Ok(output);
        }

        /// <summary>
        /// Create a new Dealer in the system.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult CreateDealer(Envelope<DealerMaster> param)
        {
            var output = _IMasterMaintenanceService.CreateDealer(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// Delete a Dealer if it is not mapped to any active employee.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DeleteDealer(Envelope<DealerMaster> param)
        {
            var output = _IMasterMaintenanceService.DeleteDealer(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// check for PSIOutlet mapping
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult CheckPSIOutlet(Envelope<DealerMaster> param)
        {
            var output = _IMasterMaintenanceService.CheckPSIOutlet(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// Update an existing Dealer in the system.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult UpdateDealer(Envelope<DealerMaster> param)
        {
            var output = _IMasterMaintenanceService.UpdateDealer(param.Data);
            return Ok(output);
        }
        #endregion Dealer Master API

        #region Division Master API
        /// <summary>
        /// Get all Divisions saved in the system.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetAllDivisions()
        {
            var output = _IMasterMaintenanceService.GetAllDivisions();
            return Ok(output);
        }

        /// <summary>
        /// Get all data related to a specific Division on basis of it's ID.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetDivisionByID(Envelope<DivisionMaster> param)
        {
            var output = _IMasterMaintenanceService.GetDivisionById(param.Data.Id);
            return Ok(output);
        }

        /// <summary>
        /// Create a new Division in the system.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult CreateDivision(Envelope<DivisionMaster> param)
        {
            var output = _IMasterMaintenanceService.CreateDivision(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// Delete a Division if it is not mapped to any active employee/agency/product category.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DeleteDivision(Envelope<DivisionMaster> param)
        {
            var output = _IMasterMaintenanceService.DeleteDivision(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// Update an existing Division in the system.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult UpdateDivision(Envelope<DivisionMaster> param)
        {
            var output = _IMasterMaintenanceService.UpdateDivision(param.Data);
            return Ok(output);
        }
        #endregion Division Master API

        #region Asset Master API
        /// <summary>
        /// Get all Assets saved in the system.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetAllAssets()
        {
            var output = _IMasterMaintenanceService.GetAllAssets();
            return Ok(output);
        }

        /// <summary>
        /// Get all data related to a specific Asset on basis of it's ID.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetAssetByID(Envelope<AssetMaster> param)
        {
            var output = _IMasterMaintenanceService.GetAssetById(param.Data.Id);
            return Ok(output);
        }

        /// <summary>
        /// Create a new Asset in the system.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult CreateAsset(Envelope<AssetMaster> param)
        {
            var output = _IMasterMaintenanceService.CreateAsset(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// Delete a Asset if it has no dependency in the system.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DeleteAsset(Envelope<AssetMaster> param)
        {
            var output = _IMasterMaintenanceService.DeleteAsset(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// Update an existing Asset in the system.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult UpdateAsset(Envelope<AssetMaster> param)
        {
            var output = _IMasterMaintenanceService.UpdateAsset(param.Data);
            return Ok(output);
        }
        #endregion Asset Master API

        #region Product Target Category Master API
        /// <summary>
        /// Get all Product Target Category saved in the system.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetAllProductTargetCategories()
        {
            var output = _IMasterMaintenanceService.GetAllProductTargetCategories();
            return Ok(output);
        }

        /// <summary>
        /// Get all data related to a specific Product Target Category on basis of it's ID.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetProductTargetCategoryById(Envelope<ProductTargetCategoryMaster> param)
        {
            var output = _IMasterMaintenanceService.GetProductTargetCategoryById(param.Data.Id);
            return Ok(output);
        }

        /// <summary>
        /// Create a new Product Target Category in the system.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult CreateProductTargetCategory(Envelope<ProductTargetCategoryMaster> param)
        {
            var output = _IMasterMaintenanceService.CreateProductTargetCategory(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// Delete a Product Target Category if it has no dependency in the system.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DeleteProductTargetCategory(Envelope<ProductTargetCategoryMaster> param)
        {
            var output = _IMasterMaintenanceService.DeleteProductTargetCategory(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// Update an existing Product Target Category in the system.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult UpdateProductTargetCategory(Envelope<ProductTargetCategoryMaster> param)
        {
            var output = _IMasterMaintenanceService.UpdateProductTargetCategory(param.Data);
            return Ok(output);
        }
        #endregion Product Target Category Master API

        #region Product Definition Under Target Category Master API
        /// <summary>
        /// Get all Product Definition Under Target Category saved in the system.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetAllProductDefinitionUnderTargetCategories()
        {
            var output = _IMasterMaintenanceService.GetAllProductDefinitionUnderTargetCategories();
            return Ok(output);
        }

        /// <summary>
        /// Get all data related to a specific Product Definition Under Target Category on basis of it's ID.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetProductDefinitionUnderTargetCategoryById(Envelope<ProductDefinitionUnderTargetCategory> param)
        {
            var output = _IMasterMaintenanceService.GetProductDefinitionUnderTargetCategoryById(param.Data.Id);
            return Ok(output);
        }

        /// <summary>
        /// Create a new Product Definition Under Target Category in the system.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult CreateProductDefinitionUnderTargetCategory(Envelope<ProductDefinitionUnderTargetCategory> param)
        {
            var output = _IMasterMaintenanceService.CreateProductDefinitionUnderTargetCategory(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// Delete a Product Definition Under Target Category if it has no dependency in the system.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpDelete]
        public IHttpActionResult DeleteProductDefinitionUnderTargetCategory(Envelope<ProductDefinitionUnderTargetCategory> param)
        {
            var output = _IMasterMaintenanceService.DeleteProductDefinitionUnderTargetCategory(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// Update an existing Product Definition Under Target Category in the system.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult UpdateProductDefinitionUnderTargetCategory(Envelope<ProductDefinitionUnderTargetCategory> param)
        {
            var output = _IMasterMaintenanceService.UpdateProductDefinitionUnderTargetCategory(param.Data);
            return Ok(output);
        }
        #endregion Product Definition Under Target Category Master API

        #region SFA Salary Master API     
        /// <summary>
        /// Delete a SFA Salary Master if it has no dependency in the system.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DeleteSFASalaryMaster(Envelope<SFASalaryMasterDelete> param)
        {
            var output = _IMasterMaintenanceService.DeleteSFASalaryMaster(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// Update an existing SFA Salary Master in the system.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult UpdateSFASalaryMaster(Envelope<SFASalaryMasterGrid> param)
        {
            var output = _IMasterMaintenanceService.UpdateSFASalaryMaster(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// to get sfa salary master row detail by LoginId i.e. Id of User table
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetSFASalaryMasterById(Envelope<SFASalaryMasterGrid> param)
        {
            var output = _IMasterMaintenanceService.GetSFASalaryMasterById(param.Data.LoginId);
            return Ok(output);
        }

        /// <summary>
        /// to create new sfa salary master row by LoginId i.e. Id of User table
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult CreateSFASalaryMaster(Envelope<SFASalaryMasterGrid> param)
        {
            var output = _IMasterMaintenanceService.CreateSFASalaryMaster(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// to get sfa salary master row detail by LoginId i.e. Id of User table
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult SFASalaryMasterDataDownload(Envelope<SFASalaryMasterDownload> param)
        {
            var output = _IMasterMaintenanceService.SFASalaryMasterDataDownload(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// to add/update sfa salary master row detail through excel file
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult ManageSFASalaryMasterData(Envelope<List<SFASalaryMasterGrid>> param)
        {
            var output = _IMasterMaintenanceService.ManageSFASalaryMasterData(param.Data);
            return Ok(output);
        }
        #endregion SFA Salary Master API

        #region Target Date Setting Master API     
        /// <summary>
        /// Update an target update date in the system.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult UpdateTargetDateSettingMaster(Envelope<TargetDateSettingMaster> param)
        {
            var output = _IMasterMaintenanceService.UpdateTargetDateSettingMaster(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// to get Target Update Date row detail by Id
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetTargetDateSettingMasterById(Envelope<TargetDateSettingMaster> param)
        {
            var output = _IMasterMaintenanceService.GetTargetDateSettingMasterById(param.Data.Id);
            return Ok(output);
        }

        /// <summary>
        /// to create new Target Update Date Master row for respective Branch
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult CreateTargetDateSettingMaster(Envelope<TargetDateSettingMaster> param)
        {
            var output = _IMasterMaintenanceService.CreateTargetDateSettingMaster(param.Data);
            return Ok(output);
        }
        #endregion Target Date Setting Master API

        #region Broadcast Message
        [HttpPost]
        public IHttpActionResult CreateBroadcastMessage(Envelope<CreateBroadcastMessageForm> param)
        {
            var output = _IMasterMaintenanceService.CreateBroadcastMessage(param.Data);

            //try
            //{
            //    //code to convert base64 to File and save file 
            //    if (param.Data.PICTUREDATA != null)
            //    {
            //        //Save the Byte Array as File.

            //        dynamic x = Convert.FromBase64String(param.Data.PICTUREDATA);
            //        File.WriteAllBytes(HttpContext.Current.Server.MapPath("~/Uploads/Broadcast/" + param.Data.FileName), Convert.FromBase64String(param.Data.PICTUREDATA));
            //    }
            //    //
            //}
            //catch (Exception ex)
            //{
            //    return Ok(ex.Message);
            //}


            return Ok(output);
        }

        [HttpPost]
        public IHttpActionResult InActiveBroadcastMessage(Envelope<CreateBroadcastMessageForm> List)
        {
            var getList = _IMasterMaintenanceService.InActiveBroadcastMessage(List.Data);
            return Ok(getList);
        }
        #endregion Broadcast Message

        #region Role Rights Mapping Master API
        /// <summary>
        /// to create role rights mapping master
        /// </summary>
        /// <param name="rolerightsmappingData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult CreateRoleRightsMapping(Envelope<RoleRightsMappingMaster> param)
        {
            var output = _IMasterMaintenanceService.CreateRoleRightsMapping(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// to update existing role rights mapping master
        /// </summary>
        /// <param name="rolerightsmappingData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult UpdateRoleRightsMapping(Envelope<RoleRightsMappingMaster> param)
        {
            var output = _IMasterMaintenanceService.UpdateRoleRightsMapping(param.Data);
            return Ok(output);
        }
        #endregion Role Rights Mapping Master API

        #region Feedback & Trainings
        /// <summary>
        /// To create feedback form design.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult CreateFeedbackForm(Envelope<CreateFeedbackForm> objFormData)
        {
            var output = _IMasterMaintenanceService.CreateFeedbackForm(objFormData.Data);
            return Ok(output);
        }

        /// <summary>
        /// To delete feedback form design.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DeleteFeedbackForm(Envelope<DeleteFeedbackForm> objFormData)
        {
            var output = _IMasterMaintenanceService.DeleteFeedbackForm(objFormData.Data);
            return Ok(output);
        }

        /// <summary>
        /// To view feedback form design.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult ViewFeedbackFormDesign(Envelope<ViewFeedbackForm> objFormData)
        {
            var output = _IMasterMaintenanceService.ViewFeedbackFormDesign(objFormData.Data);
            return Ok(output);
        }

        /// <summary>
        /// To Create Training Form.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult CreateTrainingForm(Envelope<CreateTrainingForm> objFormData)
        {
            var output = _IMasterMaintenanceService.CreateTrainingForm(objFormData.Data);
            return Ok(output);
        }


        [HttpPost]
        public IHttpActionResult InActiveTrainingMessage(Envelope<CreateTrainingForm> List)
        {
            var getList = _IMasterMaintenanceService.InActiveTrainingMessage(List.Data);
            return Ok(getList);
        }

        #endregion Feedback & Trainings

        #region Update Monthly Attendance Report

        [HttpPost]
        public IHttpActionResult UpdateMonthlyReport(Envelope<UpdateMonthlyReport> List)
        {
            var getList = _IMasterMaintenanceService.UpdateMonthlyReport(List.Data);
            return Ok(getList);
        }

        #endregion

        #region Shift Master API
        /// <summary>
        /// Get all shuft saved in the system.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetShiftTiming()
        {
            var output = _IMasterMaintenanceService.GetShiftTiming();
            return Ok(output);
        }

        /// <summary>
        /// Get all data related to a specific shift on basis of it's ID.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetShiftTimingById(Envelope<ShiftMaster> param)
        {
            var output = _IMasterMaintenanceService.GetShiftTimingById(param.Data.Id);
            return Ok(output);
        }

        /// <summary>
        /// Create a new Shift in the system.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult CreateShiftTiming(Envelope<ShiftMaster> param)
        {
            var output = _IMasterMaintenanceService.CreateShiftTiming(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// Delete a Shift if it is not mapped to any active dealer.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DeleteShift(Envelope<ShiftMaster> param)
        {
            var output = _IMasterMaintenanceService.DeleteShift(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// Update an existing shift in the system.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult UpdateShiftTiming(Envelope<ShiftMaster> param)
        {
            var output = _IMasterMaintenanceService.UpdateShiftTiming(param.Data);
            return Ok(output);
        }
        #endregion Shift Master API
    }
}
