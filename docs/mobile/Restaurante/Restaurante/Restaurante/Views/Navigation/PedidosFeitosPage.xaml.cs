using Restaurante.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Restaurante.Views.Navigation
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PedidosFeitosPage : ContentPage
    {
        private Funcionario funcionario;
        public PedidosFeitosPage(Funcionario f)
        {
            InitializeComponent();
            this.funcionario = new Funcionario(f);
            ViewPedidos.ItemsSource = Connect.pedidosFeitos(this.funcionario);
        }

        private void BackClicked(object sender, System.EventArgs e)
        {
            if(this.funcionario.cargo.Equals("Emp. Mesa"))
                App.Current.MainPage.Navigation.PushModalAsync(new Views.Forms.ServicoMesaPage(this.funcionario));
            else
                App.Current.MainPage.Navigation.PushModalAsync(new Views.Forms.ServicoBalcaoPage(this.funcionario));
        }

        
    }
}
