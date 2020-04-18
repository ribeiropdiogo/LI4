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
    public partial class ServicoMesaPage : ContentPage
    {
        private Funcionario funcionario;
        public ServicoMesaPage(Funcionario funcionario)
        {
            InitializeComponent();
            this.funcionario = new Funcionario(funcionario);
        }
        private void AddPedido_Clicked(object sender, System.EventArgs e)
        {
             App.Current.MainPage.Navigation.PushModalAsync(new InserirMesa(funcionario)); 
        }

        private void albumsButton_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage.Navigation.PushModalAsync(new PedidosFeitosPage(funcionario));
        }

        private void backButton_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage.Navigation.PushModalAsync(new LoginPage());
        }
    }
}