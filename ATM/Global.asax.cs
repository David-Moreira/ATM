﻿using ATM.Core.Facade;
using ATM.Core.Interfaces;
using ATM.Core.Interfaces.Services;
using ATM.Core.Interfaces.Validation;
using ATM.Core.Services;
using ATM.Core.Validation;
using ATM.Infrastructure.Data;
using ATM.Infrastructure.Repositories;
using ATM.Models;
using Ninject;
using Ninject.Web.Common.WebHost;
using System.Linq;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ATM
{
    public class MvcApplication : NinjectHttpApplication
    {
        //protected void Application_Start()
        //{
        //    AreaRegistration.RegisterAllAreas();
        //    FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        //    RouteConfig.RegisterRoutes(RouteTable.Routes);
        //    BundleConfig.RegisterBundles(BundleTable.Bundles);
        //}

        protected override IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            RegisterServices(kernel);
            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private void RegisterServices(IKernel kernel)
        {
            kernel.Bind(typeof(IRepositoryBase<>)).To(typeof(RepositoryBase<>));
            kernel.Bind<IBankAccountRepo>().To<BankAccountRepo>();
            kernel.Bind<ITransactionRepo>().To<TransactionRepo>();

            kernel.Bind(typeof(IServiceBase<>)).To(typeof(ServiceBase<>));
            kernel.Bind<IBankAccountService>().To<BankAccountManager>();
            kernel.Bind<ITransactionService>().To<TransactionManager>();
            kernel.Bind<IOperationService>().To<OperationManager>();
            kernel.Bind<IOperationValidator>().To<OperationValidator>();
        }

        protected override void OnApplicationStarted()
        {
            base.OnApplicationStarted();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ModelBinders.Binders.Add(typeof(decimal), new DecimalModelBinder());
            EFWarmUp();
        }

        //Dumb solution for now.
        private void EFWarmUp()
        {
            using (var dbContext = new ATM.Models.ApplicationDbContext())
            { dbContext.Users.FirstOrDefault(); }
        }
    }
}