using Restaurante.Models;
using Restaurante.Views.Forms;
using System;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace Restaurante.Views.Navigation
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddPedidoPage : ContentPage
    {
        private List<Artigo> lista = new List<Artigo>();
        private Pedido pedido;
        private int mesa, idArtigoPedido, idPedido;
        private Funcionario funcionario;
        private ICollection<ArtigoInPedido> artigos;

        public AddPedidoPage(Funcionario funcionario, int mesa)
        {
            InitializeComponent();
            this.funcionario = new Funcionario(funcionario);
            this.mesa = mesa;
            this.idArtigoPedido = Connect.linhasArtigoInPedido(1);
            this.idPedido = Connect.linhasArtigoInPedido(2);
            this.lista = Connect.listaArtigos();
            this.artigos = new HashSet<ArtigoInPedido>();
            ViewArtigos.ItemsSource = this.lista;
        }
        private void MainSearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var keyword = MainSearchBar.Text;
            ViewArtigos.ItemsSource = this.lista.Where(name => name.nome.ToLower().Contains(keyword.ToLower()));
        }

        private void BackClicked(object sender, System.EventArgs e)
        {
            App.Current.MainPage.Navigation.PushModalAsync(new Views.Forms.ServicoMesaPage(this.funcionario));
        }

        private void MenuClicked(object sender, System.EventArgs e)
        {
            var button = sender as Button;
            var artigo = button.BindingContext as Artigo;

            ArtigoInPedido a = new ArtigoInPedido(this.idArtigoPedido, this.idPedido, artigo.id);
            Console.WriteLine(this.idArtigoPedido + "," + this.idPedido + "," + artigo.id);
            this.artigos.Add(a);
            this.idArtigoPedido++;
        }
        private void DoneButton_Clicked(object sender, System.EventArgs e)
        {
            DateTime agora = DateTime.Now;
            agora = new DateTime(agora.Year, agora.Month, agora.Day, agora.Hour, agora.Minute, agora.Second);
            string formatForMySql = agora.ToString("yyyy-MM-dd HH:mm:ss");
            Connect.insertPedido(formatForMySql, 1, "Por pagar", this.funcionario.id, this.mesa);
            Console.WriteLine(this.idPedido);
            Connect.addArtigos(this.artigos);
            Connect.mesaOcupada(this.mesa);
            App.Current.MainPage.Navigation.PushModalAsync(new ServicoMesaPage(this.funcionario));
        }
    }
}