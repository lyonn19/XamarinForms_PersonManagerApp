using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PersonManager
{
    public partial class App : Application
    {
        public static string BASE_URL = "https://3cc01b2004f4.ngrok.io";
        public static string API_URL = "/api/persons";

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage() 
            {
                BarBackgroundColor = Color.Orange
            });
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
