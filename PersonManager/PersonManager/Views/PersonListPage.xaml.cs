using PersonManager.Models;
using PersonManager.ViewModels;
using PersonManager.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PersonManager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PersonListPage : ContentPage
    {
        public PersonListPage()
        {
            InitializeComponent();
            BindingContext = ViewModelLocator.Instance.Resolve<PersonViewModel>();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModelLocator.Instance.Resolve<PersonViewModel>().PeopleCommand.Execute(null);
        }

        /// <summary>
        /// Event Menu ContextAction OnDelete : delete itemTodo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void OnDelete(object sender, EventArgs e)
        {
            var person = ((MenuItem)sender).CommandParameter as Person;
            if (person == null) return;
            var answ = await DisplayAlert("Alert", "Are you sure?", "Cancel", "Accept");
            if (answ) return;
            ViewModelLocator.Instance.Resolve<PersonViewModel>().SelectedPerson = person;
            ViewModelLocator.Instance.Resolve<PersonViewModel>().DeletePersonCommand.Execute(null);
        }

        private void Save_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NewPersonPage());
        }
    }
}