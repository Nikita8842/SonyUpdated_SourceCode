using System.Web.Http;
using System.Web.Http.Cors;
using Microsoft.Practices.Unity;
using SonyAmbo.Resolver;
using AmboProvider.Interface;
using AmboProvider.Implimentation;
using AmboDataServices.Interface;
using AmboDataServices.Implimentation;
using AmboServices.Implimentation;
using AmboServices.Interface;
//using System.Web.Http.ExceptionHandling;
//using AmboUtilities.Helper;

namespace SonyAmbo
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);
            config.MessageHandlers.Add(new AmboUtilities.Helper.TokenAuthentication("SonyIndia", "APIValue"));

            var container = new UnityContainer();
            // Web API configuration and services
            
            config.DependencyResolver = new UnityResolver(container);

            //Registerd All Layer Services herewith

           container.RegisterType<IValidateTest, ValidateTest>(new HierarchicalLifetimeManager());

           container.RegisterType<IValidateDataService, ValidateDataService>(new HierarchicalLifetimeManager());

           container.RegisterType<IValidateService, ValidateService>(new HierarchicalLifetimeManager());

            container.RegisterType<IMasterMaintenanceProvider, MasterMaintenanceProvider>(new HierarchicalLifetimeManager());
            container.RegisterType<IUserManagementProvider, UserManagementProvider>(new HierarchicalLifetimeManager());
            container.RegisterType<ISFAMobileManagementProvider, SFAMobileManagementProvider>(new HierarchicalLifetimeManager());
            container.RegisterType<IReportProvider, ReportProvider>(new HierarchicalLifetimeManager());
            container.RegisterType<ISFAMobileReportsProvider, SFAMobileReportsProvider>(new HierarchicalLifetimeManager());
            container.RegisterType<IIncentiveCalculationDateSettingProvider, IncentiveCalculationDateSettingProvider>(new HierarchicalLifetimeManager());


            container.RegisterType<IValidateDataService, ValidateDataService>(new HierarchicalLifetimeManager());
            container.RegisterType<IMasterMaintenanceDataService, MasterMaintenanceDataService>(new HierarchicalLifetimeManager());
            container.RegisterType<IUserManagementDataService, UserManagementDataService>(new HierarchicalLifetimeManager());
            container.RegisterType<ISFAMobileManagementDataService, SFAMobileManagementDataService>(new HierarchicalLifetimeManager());
            container.RegisterType<IReportDataService, ReportDataService>(new HierarchicalLifetimeManager());
            container.RegisterType<ISFAMobileReportsDataService, SFAMobileReportsDataService>(new HierarchicalLifetimeManager());
            container.RegisterType<IIncentiveCalculationDateSettingDataService, IncentiveCalculationDateSettingDataService>(new HierarchicalLifetimeManager());

            container.RegisterType<IValidateService, ValidateService>(new HierarchicalLifetimeManager());
            container.RegisterType<IMasterMaintenanceService, MasterMaintenanceService>(new HierarchicalLifetimeManager());
            container.RegisterType<IIncentiveCalculationDateSettingService, IncentiveCalculationDateSettingService>(new HierarchicalLifetimeManager());

            container.RegisterType<IGridDataProvider, GridDataProvider>(new HierarchicalLifetimeManager());

            container.RegisterType<IGridDataService, GridDataService>(new HierarchicalLifetimeManager());

            container.RegisterType<IGridService, GridService>(new HierarchicalLifetimeManager());

            container.RegisterType<ICommonProvider, CommonProvider>(new HierarchicalLifetimeManager());

            container.RegisterType<ICommonDataService, CommonDataService>(new HierarchicalLifetimeManager());

            container.RegisterType<ICommonService, CommonService>(new HierarchicalLifetimeManager());

            container.RegisterType<IUserManagementService, UserManagementService>(new HierarchicalLifetimeManager());
            container.RegisterType<IUserManagementProvider, UserManagementProvider>(new HierarchicalLifetimeManager());
            container.RegisterType<IUserManagementDataService, UserManagementDataService>(new HierarchicalLifetimeManager());

            container.RegisterType<IUserValidationService, UserValidationService>(new HierarchicalLifetimeManager());
            container.RegisterType<IUserValidationProvider, UserValidationProvider>(new HierarchicalLifetimeManager());
            container.RegisterType<IUserValidationDataService, UserValidationDataService>(new HierarchicalLifetimeManager());
            container.RegisterType<ISFAMobileManagementService, SFAMobileManagementService>(new HierarchicalLifetimeManager());
            container.RegisterType<IReportService, ReportService>(new HierarchicalLifetimeManager());
            container.RegisterType<ISFAMobileReportsService, SFAMobileReportsService>(new HierarchicalLifetimeManager());

            container.RegisterType<IProductCategoryGroupProvider, ProductCategoryGroupProvider>(new HierarchicalLifetimeManager());
            container.RegisterType<IProductCategoryGroupDataService, ProductCategoryGroupDataService>(new HierarchicalLifetimeManager());
            container.RegisterType<IProductCategoryGroupService, ProductCategoryGroupService>(new HierarchicalLifetimeManager());

            container.RegisterType<IMappingProvider, MappingProvider>(new HierarchicalLifetimeManager());
            container.RegisterType<IMappingService, MappingService>(new HierarchicalLifetimeManager());
            container.RegisterType<IMappingDataService, MappingDataService>(new HierarchicalLifetimeManager());

            container.RegisterType<IIncentiveProvider, IncentiveProvider>(new HierarchicalLifetimeManager());
            container.RegisterType<IIncentiveService, IncentiveService>(new HierarchicalLifetimeManager());
            container.RegisterType<IIncentiveDataService, IncentiveDataService>(new HierarchicalLifetimeManager());

            container.RegisterType<IDeviationProvider, DeviationProvider>(new HierarchicalLifetimeManager());
            container.RegisterType<IDeviationService, DeviationService>(new HierarchicalLifetimeManager());
            container.RegisterType<IDeviationDataService, DeviationDataService>(new HierarchicalLifetimeManager());

            container.RegisterType<IModuleManagementProvider, ModuleManagementProvider>(new HierarchicalLifetimeManager());
            container.RegisterType<IModuleManagementDataService, ModuleManagementDataService>(new HierarchicalLifetimeManager());
            container.RegisterType<IModuleManagementService, ModuleManagementService>(new HierarchicalLifetimeManager());

            container.RegisterType<IWebReportsProvider, WebReportsProvider>(new HierarchicalLifetimeManager());
            container.RegisterType<IWebReportsDataService, WebReportsDataService>(new HierarchicalLifetimeManager());
            container.RegisterType<IWebReportsService, WebReportsService>(new HierarchicalLifetimeManager());

            container.RegisterType<IErrorLogProvider, ErrorLogProvider>(new HierarchicalLifetimeManager());
            container.RegisterType<IErrorLogDataService, ErrorLogDataService>(new HierarchicalLifetimeManager());
            container.RegisterType<IErrorLogService, ErrorLogService>(new HierarchicalLifetimeManager());

            container.RegisterType<IMPRIntegration, MPRIntegration>(new HierarchicalLifetimeManager());
            container.RegisterType<IMPRIntergrationDataService, MPRIntegrationDataService>(new HierarchicalLifetimeManager());
            container.RegisterType<IMPRIntegrationService, MPRIntegrationService>(new HierarchicalLifetimeManager());
            //Registeration Closed here

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
