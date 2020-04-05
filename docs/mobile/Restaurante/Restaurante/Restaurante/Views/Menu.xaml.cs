using Restaurante.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Restaurante
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Menu : ContentPage
    {
        private string user, password;
        public Menu()
        {
            InitializeComponent();
        }
        private void BotaoIniciar(object sender, EventArgs e)
        {
            this.user = usernameEntry.Text;
            this.password = passwordEntry.Text;
            //if(empregado de mesa)
            Funcionario func = Connect.login(user, password);
            if(func != null)
            {
                string cargo;
                cargo = func.cargo;
                if (cargo.Equals("Limpa Vidros")){
                    Navigation.PushModalAsync(new EmpregadoMesa(func));
                }
                else
                {
                    Navigation.PushModalAsync(new EmpregadoBalcao());
                }
            }
            else
            {
                DisplayAlert("Erro.", "Volte a tentar!", "Ok");
            }
        }
    }
}