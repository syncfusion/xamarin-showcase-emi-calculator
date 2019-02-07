using System;
using Autofac;

namespace LoanCalculator
{
    public class ViewModelLocator
    {
        private IContainer container;

        private readonly ContainerBuilder containerBuilder;

        private static readonly ViewModelLocator ViewModelLocatorInstance = new ViewModelLocator();

        public static ViewModelLocator Instance
        {
            get
            {
                return ViewModelLocatorInstance;
            }
        }

        public ViewModelLocator()
        {
            containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterType<EMIService>().As<IEMIService>().SingleInstance();
            containerBuilder.RegisterType<ShareService>().As<IShareService>();
            containerBuilder.RegisterType<ExportDataService>().As<IExportDataService>();

            containerBuilder.RegisterType<NavigationService>().As<INavigationService>();

            containerBuilder.RegisterType<LoanDetailsViewModel>();
            containerBuilder.RegisterType<StatisticPageViewModel>();
        }

        public T Resolve<T>()
        {
            return container.Resolve<T>();
        }

        public object Resolve(Type type)
        {
            return container.Resolve(type);
        }

        public void Build()
        {
            container = containerBuilder?.Build();
        }
    }
}