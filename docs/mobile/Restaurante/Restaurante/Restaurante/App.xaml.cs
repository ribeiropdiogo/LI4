using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Restaurante
{
    public partial class App : Application
    {
        public static string BaseImageUrl { get; } = "https://cdn.syncfusion.com/essential-ui-kit-for-xamarin.forms/common/uikitimages/";
        public App()
        {
            InitializeComponent();

            MainPage = new Views.Forms.LoginPage();
            //MainPage = new Views.Bookmarks.CartPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
