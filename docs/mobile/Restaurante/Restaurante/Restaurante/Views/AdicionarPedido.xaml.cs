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
    public partial class AdicionarPedido : ContentPage
    {
        public AdicionarPedido()
        {
            InitializeComponent();

            List<Artigo> lista = Connect.listaArtigos();
            ViewArtigos.ItemsSource = lista;
        }
        public async void lvItemTapped(object sender, ItemTappedEventArgs e) 
        { 
            var myListView = (ListView)sender; 
            var myItem = myListView.SelectedItem;
            await DisplayAlert(myItem.ToString(), "Volte a tentar!", "Ok");
        }
    }
}