using Restaurante.Models;
using Restaurante.Views.ErrorAndEmpty;
using Restaurante.Views.Forms;
using System;
using System.Buffers.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Restaurante.ViewModels.Forms
{
    [Preserve(AllMembers = true)]
    public class LoginPageViewModel : BaseViewModel
    {
        public string email, password;
        public Funcionario func;
        public LoginPageViewModel()
        {
            this.LoginCommand = new Command(this.LoginClicked);
        }
        public string Password
        {
            get
            {
                return this.password;
            }

            set
            {
                if (this.password == value)
                {
                    return;
                }
                this.password = value;
                this.NotifyPropertyChanged();
            }
        }

        public string Email
        {
            get
            {
                return this.email;
            }

            set
            {
                if (this.email == value)
                {
                    return;
                }
                this.email = value;
                this.NotifyPropertyChanged();
            }
        }

        public Command LoginCommand { get; set; }
        private void LoginClicked(object obj)
        {
            func = Connect.login(email, password);

            if (func != null)
            {
                string cargo;
                cargo = func.cargo;
                if (cargo.Equals("Emp. Mesa"))
                {
                    App.Current.MainPage.Navigation.PushModalAsync(new ServicoMesaPage(func));
                }
                if(cargo.Equals("Emp. Balcão"))
                {
                    App.Current.MainPage.Navigation.PushModalAsync(new ServicoBalcaoPage(func));
                }
            }
            else
            {
                App.Current.MainPage.Navigation.PushModalAsync(new Views.ErrorAndEmpty.SomethingWentWrongPage());
            }
        }
    }
}