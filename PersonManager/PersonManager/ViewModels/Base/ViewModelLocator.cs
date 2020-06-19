using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using PersonManager.DataLayer;
using PersonManager.Services;
using PersonManager.Services.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonManager.ViewModels.Base
{
    /// <summary>
    /// ViewModel Locator Pattern for Register and Resolve objects dependency
    /// </summary>
    public class ViewModelLocator
    {
        private readonly IUnityContainer _container;

        public static ViewModelLocator Instance { get; } = new ViewModelLocator();

        public ViewModelLocator()
        {
            _container = new UnityContainer();

            // services
            _container.RegisterType<INavigationService, NavigationService>();
            _container.RegisterType<IPersonService, PersonService>();

            // viewmodels
            _container.RegisterType<PersonViewModel>(new ContainerControlledLifetimeManager());



            var unityServiceLocator = new UnityServiceLocator(_container);
            ServiceLocator.SetLocatorProvider(() => unityServiceLocator);
        }

        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        public object Resolve(Type type)
        {
            return _container.Resolve(type);
        }
    }
}
