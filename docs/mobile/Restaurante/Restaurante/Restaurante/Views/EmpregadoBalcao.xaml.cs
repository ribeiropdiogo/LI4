using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Restaurante
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EmpregadoBalcao : ContentPage
    {
        public EmpregadoBalcao()
        {
            InitializeComponent();
        }

        private void BotaoListaPedidos(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new ListaPedidos());
        }

        private void BotaoAdicionar(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new AdicionarPedido());
        }

        private void BotaoSair(object sender, EventArgs e)
        {
            DisplayAlert("Bom trabalho!", "Até à próxima!", "OK");
            Navigation.PushModalAsync(new Menu());
        }
    }
}