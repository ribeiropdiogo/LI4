using Restaurante.Views.Forms;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Restaurante.ViewModels.ErrorAndEmpty
{
    [Preserve(AllMembers = true)]
    public class SomethingWentWrongPageViewModel : BaseViewModel
    {
        private string imagePath;
        public SomethingWentWrongPageViewModel()
        {
            this.ImagePath = "SomethingWentWrong.svg";
            this.TryAgainCommand = new Command(this.TryAgain);
        }
        public ICommand TryAgainCommand { get; set; }
        public string ImagePath
        {
            get
            {
                return this.imagePath;
            }
            set
            {
                this.imagePath = value;
            }
        }
        private void TryAgain(object obj)
        {
            App.Current.MainPage.Navigation.PushModalAsync(new LoginPage());
        }
    }
}
