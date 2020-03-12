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
                Navigation.PushModalAsync(new EmpregadoMesa());
            //if(empregado de balcao)
               // Navigation.PushModalAsync(new EmpregadoBalcao());
        }
    }
}