using CustomerReviewsModule.Core.Services;
using CustomerReviewsModule.Data;
using CustomerReviewsModule.Data.Repositories;
using CustomerReviewsModule.Data.Services;
using Microsoft.Practices.Unity;
using System.Linq;
using VirtoCommerce.Platform.Core.Common;
using VirtoCommerce.Platform.Core.Modularity;
using VirtoCommerce.Platform.Core.Settings;
using VirtoCommerce.Platform.Data.Infrastructure;
using VirtoCommerce.Platform.Data.Infrastructure.Interceptors;

namespace CustomerReviewsModule.Web
{
    public class Module : ModuleBase
    {
        private readonly string _connectionString = ConfigurationHelper.GetConnectionStringValue("VirtoCommerce.CustomerReviews") ?? ConfigurationHelper.GetConnectionStringValue("VirtoCommerce");
        private readonly IUnityContainer _container;


        public Module(IUnityContainer container)
        {
            _container = container;
        }

        public override void SetupDatabase()
        {
            using (var db = new CustomerReviewRepository(_connectionString, _container.Resolve<AuditableInterceptor>()))
            {
                var initializer = new SetupDatabaseInitializer<CustomerReviewRepository, Data.Migrations.Configuration>();
                initializer.InitializeDatabase(db);
            }

            // Modify database schema with EF migrations
            // using (var context = new MyRepository(_connectionString)))
            // {
            //     var initializer = new SetupDatabaseInitializer<MyRepository, Data.Migrations.Configuration>();
            //     initializer.InitializeDatabase(context);
            // }
        }

        public override void Initialize()
        {
            base.Initialize();

            _container.RegisterType<ICustomerReviewRepository>(new InjectionFactory(c => new CustomerReviewRepository(_connectionString, new EntityPrimaryKeyGeneratorInterceptor(), _container.Resolve<AuditableInterceptor>())));

            _container.RegisterType<ICustomerReviewSearchService, CustomerReviewSearchService>();

            _container.RegisterType<ICustomerReviewService, CustomerReviewService>();

            _container.RegisterType<IRatingService, RatingService>();


            // This method is called for each installed module on the first stage of initialization.

            // Register implementations:
            // _container.RegisterType<IMyRepository>(new InjectionFactory(c => new MyRepository(_connectionString, new EntityPrimaryKeyGeneratorInterceptor()));
            // _container.RegisterType<IMyService, MyService>();

            // Try to avoid calling _container.Resolve<>();
        }

        public override void PostInitialize()
        {
            base.PostInitialize();

            var settingManager = _container.Resolve<ISettingsManager>();

            var storeSettingsNames = new[] { "CustomerReviews.CustomerReviewsEnabled" };
            var storeSettings = settingManager.GetModuleSettings("CustomerReviews.Web").Where(x => storeSettingsNames.Contains(x.Name)).ToArray();
            settingManager.RegisterModuleSettings("VirtoCommerce.Store", storeSettings);

            //AbstractTypeFactory<VirtoCommerce.Domain.Catalog.Model.CatalogProduct>.OverrideType<VirtoCommerce.Domain.Catalog.Model.CatalogProduct, RatingProductEntity>();

            // This method is called for each installed module on the second stage of initialization.

            // Override types using AbstractTypeFactory:
            // AbstractTypeFactory<BaseModel>.OverrideType<BaseModel, BaseModelEx>();
            // AbstractTypeFactory<BaseModelEntity>.OverrideType<BaseModelEntity, BaseModelExEntity>();

            // Resolve registered implementations:
            // var settingManager = _container.Resolve<ISettingsManager>();
            // var value = settingManager.GetValue("Pricing.ExportImport.Description", string.Empty);
        }
    }
}
