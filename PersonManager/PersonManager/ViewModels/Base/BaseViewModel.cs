using PersonManager.Services.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PersonManager.ViewModels.Base
{
    public class ViewModelBase : BindableObject
    {
        /// <summary>
        /// PROVIDES ACCESS TO THE NAVIGATION SERVICE
        /// </summary>
        protected readonly INavigationService Navigation;

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }
        public virtual Task InitializeAsync(object navigationData)
        {
            return Task.FromResult(false);
        }
    }
}
