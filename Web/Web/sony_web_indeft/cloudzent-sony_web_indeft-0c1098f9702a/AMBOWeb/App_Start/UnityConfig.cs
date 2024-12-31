using APIAccessLayer.IMPLEMENTATION;
using APIAccessLayer.INTERFACE;
using System;

using Unity;

namespace AMBOWeb
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your type's mappings here.
            // container.RegisterType<IProductRepository, ProductRepository>();
            container.RegisterType<IAPIGridData, APIGridData>();
            container.RegisterType<IAPIDealerData, APIDealerData>();
            container.RegisterType<IAPICommon, APICommon>();
            container.RegisterType<ISFALevelMaster, SFALevelMaster>();
            container.RegisterType<IUserValidation, UserValidation>();
            container.RegisterType<IAPICityData, APICityData>();
			container.RegisterType<IAPIRegionData, APIRegionData>();
            container.RegisterType<IAPIStateData, APIStateData>();
            container.RegisterType<IAPIProductCategoryData, APIProductCategoryData>();
            container.RegisterType<IAPIProductSubCategoryData, APIProductSubCategoryData>();
            container.RegisterType<IAPIEmployeeData, APIEmployeeData>();
            container.RegisterType<IAPISFAData, APISFAData>();
            container.RegisterType<IAPIBranchData, APIBranchData>();
			container.RegisterType<IAPILocationData, APILocationData>();
            container.RegisterType<IAPISFAMasterforHRData, APISFAMasterforHRData>();
            container.RegisterType<IAPICompetitorMasterData, APICompetitorMasterData>();
            container.RegisterType<IAPIMaterialData, APIMaterialData>();
            container.RegisterType<IAPIMappingData, APIMappingData>();
            container.RegisterType<IAPISFASalaryMaster, APISFASalaryMaster>();
			container.RegisterType<IAPIChannelData, APIChannelData>();
            container.RegisterType<IAPIAssetData, APIAssetData>();
            container.RegisterType<IAPIProductTargetCategoryData, APIProductTargetCategoryData>();
			container.RegisterType<IAPITargetDateSettingMaster, APITargetDateSettingMaster>();
            container.RegisterType<IAPIDealerMasterCodeUpdate, APIDealerMasterCodeUpdate>();
            container.RegisterType<IAPIProductCategoryGroup, APIProductCategoryGroup>();
            container.RegisterType<IAPIAssetIssuedToSFA, APIAssetIssuedToSFA>();
			container.RegisterType<IAPIAssetAssignment, APIAssetAssignment>();
            container.RegisterType<IAPIBroadcastMessage, APIBroadcastMessage>();
            container.RegisterType<IAPIIncentiveData, APIIncentiveData>();
            container.RegisterType<IAPIWebReportsData, APIWebReportsData>();
			container.RegisterType<IAPIRoleRightsData, APIRoleRightsData>();
            container.RegisterType<IAPIDeviationData, APIDeviationData>();
            container.RegisterType<IAPIFeedbackData, APIFeedbackData>();

            container.RegisterType<IAPIIncentiveCalculationDateSetting, APIIncentiveCalculationDateSetting>();
            container.RegisterType<IAPIShiftData, APIShiftData>();
        }
    }
}