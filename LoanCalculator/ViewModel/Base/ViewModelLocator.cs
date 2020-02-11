using System;
using Autofac;
using Autofac.Core;

namespace LoanCalculator
{
    public class ViewModelLocator
    {
        private IContainer container;

        private static ILifetimeScope _rootScope;

        private static ContainerBuilder containerBuilder;

        private static readonly ViewModelLocator ViewModelLocatorInstance = new ViewModelLocator();

        public static ViewModelLocator Instance
        {
            get
            {
                return ViewModelLocatorInstance;
            }
        }

        public static void Start()
        {
            containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterType<EMIService>().As<IEMIService>().SingleInstance();
            containerBuilder.RegisterType<ShareService>().As<IShareService>();
            containerBuilder.RegisterType<ExportDataService>().As<IExportDataService>();

            containerBuilder.RegisterType<NavigationService>().As<INavigationService>();

            containerBuilder.RegisterType<LoanDetailsViewModel>();
            containerBuilder.RegisterType<StatisticPageViewModel>();

            _rootScope = containerBuilder.Build();
        }

        public object Resolve(Type type)
        {
            return _rootScope?.Resolve(type);
        }

        public static T Resolve<T>()
        {
            if (_rootScope == null) throw new Exception("Bootstrapper hasn't been started!");
            return _rootScope.Resolve<T>(new Parameter[0]);
        }

        public static T Resolve<T>(Parameter[] parameters)
        {
            if (_rootScope == null) throw new Exception("Bootstrapper hasn't been started!");
            return _rootScope.Resolve<T>(parameters);
        }
    }
}