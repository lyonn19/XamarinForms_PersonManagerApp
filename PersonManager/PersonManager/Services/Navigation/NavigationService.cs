﻿using PersonManager.ViewModels;
using PersonManager.ViewModels.Base;
using PersonManager.Views;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace PersonManager.Services.Navigation
{
    /// <summary>
    /// Navigation Services implementation (NavigateTo, NavigateBack,...)
    /// </summary>
    public class NavigationService : INavigationService
    {
        protected readonly Dictionary<Type, Type> Mappings;

        protected Application CurrentApplication => Application.Current;

        public NavigationService()
        {
            Mappings = new Dictionary<Type, Type>();

            CreatePageViewModelMappings();
        }

        public Task InitializeAsync()
        {
            return NavigateToAsync<PersonViewModel>();
        }


        public Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), null);
        }

        public Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), parameter);
        }

        public Task NavigateToAsync(Type viewModelType)
        {
            return InternalNavigateToAsync(viewModelType, null);
        }

        public Task NavigateToAsync(Type viewModelType, object parameter)
        {
            return InternalNavigateToAsync(viewModelType, parameter);
        }

        public async Task NavigateBackAsync()
        {
            await CurrentApplication.MainPage.Navigation.PopAsync();
        }

        public virtual Task RemoveLastFromBackStackAsync()
        {
            return Task.FromResult(true);
        }

        protected virtual async Task InternalNavigateToAsync(Type viewModelType, object parameter)
        {
            Page page = CreateAndBindPage(viewModelType, parameter);

            await (page.BindingContext as ViewModelBase).InitializeAsync(parameter);

            await CurrentApplication.MainPage.Navigation.PushAsync(page);
        }

        protected Type GetPageTypeForViewModel(Type viewModelType)
        {
            if (!Mappings.ContainsKey(viewModelType))
            {
                throw new KeyNotFoundException($"No map for ${viewModelType} was found on navigation mappings");
            }

            return Mappings[viewModelType];
        }

        protected Page CreateAndBindPage(Type viewModelType, object parameter)
        {
            Type pageType = GetPageTypeForViewModel(viewModelType);

            if (pageType == null)
            {
                throw new Exception($"Mapping type for {viewModelType} is not a page");
            }

            Page page = Activator.CreateInstance(pageType) as Page;
            ViewModelBase viewModel = ViewModelLocator.Instance.Resolve(viewModelType) as ViewModelBase;
            page.BindingContext = viewModel;

            return page;
        }

        private void CreatePageViewModelMappings()
        {
            Mappings.Add(typeof(PersonViewModel), typeof(PersonListPage));
        }
    }
}