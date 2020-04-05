using Restaurante.Models;
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
    public partial class EmpregadoMesa : ContentPage
    {
        private Funcionario func;
        public EmpregadoMesa(Funcionario f)
        {
            this.func = f;
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
            DisplayAlert("Bom trabalho!", "Até à próxima", "Ok");
            Navigation.PushModalAsync(new Menu());
        }
    }
}