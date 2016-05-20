using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Microsoft.WindowsAzure.MobileServices;
using Rightpoint.Peeps.Client.Infrastructure;
using Rightpoint.Peeps.Client.Pages;

namespace Rightpoint.Peeps.Client.DI
{
    public static class UnityContainerFactory
    {
        private static IUnityContainer _container;

        public static IUnityContainer GetContainer()
        {
            return _container ?? (_container = BuildContainer());
        }
        private static IUnityContainer BuildContainer()
        {
            if (null != _container) return _container;

            _container = new UnityContainer();

            RegisterTypes("Application", () => RegisterApplication(_container));
            RegisterTypes("Services", () => RegisterServices(_container));
            RegisterTypes("Infrastructure", () => RegisterInfrastructure(_container));
            RegisterTypes("Misc", () => BootstrapServiceLocator(_container));

            return _container;
        }

        private static void RegisterApplication(IUnityContainer container)
        {
            // pages
            container.RegisterType<PageBase>(new HierarchicalLifetimeManager());

            // view models
            container.RegisterType<ViewModelBase>(new HierarchicalLifetimeManager());
        }

        private static void RegisterServices(IUnityContainer container)
        {
            // configure navigation
            NavigationService navigationService = new NavigationService();
            IEnumerable<Type> types = LoadTypesFromBaseType(typeof(PageBase));
            foreach (Type type in types)
            {
                navigationService.Configure(type.Name, type);
            }
            container.RegisterInstance(navigationService, new HierarchicalLifetimeManager());

            container.RegisterType<IDialogService, DialogService>(new HierarchicalLifetimeManager());
        }

        private static void RegisterInfrastructure(IUnityContainer container)
        {
            container.RegisterType<IMobileServiceClient, PeepsMobileServiceClient>(
                new HierarchicalLifetimeManager(),
                new InjectionFactory((c, t, s) =>
                {
                    // Find more information in the readme in the solution items folder
                    return new PeepsMobileServiceClient("https://rp-peeps-prod.azure-mobile.net/", "aoJINuiAQbbrWmwDcNjdQOzxoWhxPi81");
                }));
        }

        private static void BootstrapServiceLocator(IUnityContainer container)
        {
            UnityServiceLocator locator = new UnityServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => locator);
        }

        // Helper methods

        private static void RegisterTypes(string groupName, Action registration)
        {
            try
            {
                registration.Invoke();
            }
            catch (Exception ex)
            {
                throw new Exception($"Registration failed for {groupName}. {ex.Message}", ex.InnerException);
            }
        }

        private static IEnumerable<Type> LoadTypesFromBaseType(Type baseType)
        {
            return AllClasses.FromApplication().Where(t => t.GetTypeInfo().BaseType == baseType);
        }
    }
}
