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
    public partial class NewPersonPage : ContentPage
    {
        public NewPersonPage()
        {
            InitializeComponent();
            BindingContext = ViewModelLocator.Instance.Resolve<PersonViewModel>();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (!ValidateFields()) return;
               ViewModelLocator.Instance.Resolve<PersonViewModel>().NewPersonCommand.Execute(null);
        }

        private bool ValidateFields()
        {
            bool result = true;
            NameValidator.Validate();
            if (!NameValidator.IsValid)
            {
                result = false;
            }

            AgeValidator.Validate();
            if (!AgeValidator.IsValid)
            {
                result = false;
            }


            return result;
        }
    }
}