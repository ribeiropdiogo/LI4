using Restaurante.Models;
using Restaurante.Views.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Restaurante.Views.Forms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ServicoBalcaoPage : ContentPage
    {
        private Funcionario funcionario;
        public ServicoBalcaoPage(Funcionario funcionario)
        {
            InitializeComponent();
            this.funcionario = new Funcionario(funcionario);
        }

        private void listaButton_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage.Navigation.PushModalAsync(new PedidosFeitosPage(funcionario));
        }

        private void backButton_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage.Navigation.PushModalAsync(new LoginPage());
        }
    }
}