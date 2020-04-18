using Restaurante.Models;
using Restaurante.Views.ErrorAndEmpty;
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
    public partial class InserirMesa : ContentPage
    {
        private Funcionario funcionario;
        private int mesa;

        public int Mesa
        {
            get
            {
                return this.mesa;
            }

            set
            {
                if (this.mesa == value)
                {
                    return;
                }
                this.mesa = value;
            }
        }
        public InserirMesa(Funcionario f)
        {
            InitializeComponent();
            this.funcionario = new Funcionario(f);
        }

        private void BackClicked(object sender, System.EventArgs e)
        {
            App.Current.MainPage.Navigation.PushModalAsync(new ServicoMesaPage(funcionario));
        }

        private void Continuar_Clicked(object sender, System.EventArgs e)
        {
            int aux = 0;
            bool result = int.TryParse(MesaEntry.Text, out aux);
            if(!result)
                App.Current.MainPage.Navigation.PushModalAsync(new NoItemPage(funcionario));
            this.mesa = aux;
            bool estaOcupada = Models.Connect.addMesa(mesa);
            if (estaOcupada)
                App.Current.MainPage.Navigation.PushModalAsync(new AddPedidoPage(funcionario, mesa));
            else
                App.Current.MainPage.Navigation.PushModalAsync(new NoItemPage(funcionario));
        }
    }
}