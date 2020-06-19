using PersonManager.ViewModels;
using PersonManager.ViewModels.Base;
using PersonManager.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PersonManager
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        internal Color BarBackgroundColor;

        public MainPage()
        {
            InitializeComponent();
            BindingContext = ViewModelLocator.Instance.Resolve<PersonViewModel>();
        }


        private void Button_Clicked_1(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PersonListPage());
        }
        

    }
}
