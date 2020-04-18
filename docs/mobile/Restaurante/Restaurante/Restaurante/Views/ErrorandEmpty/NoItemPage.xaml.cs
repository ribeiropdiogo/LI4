using Restaurante.Models;
using Restaurante.Views.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace Restaurante.Views.ErrorAndEmpty
{
    /// <summary>
    /// Page to show the no item
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NoItemPage
    {
        private Funcionario funcionario;
        private string imagePath;
        public NoItemPage(Funcionario funcionario)
        {
            InitializeComponent();
            this.funcionario = new Funcionario(funcionario);
            this.ImagePath = "NoItem.svg";
        }
        public string ImagePath
        {
            get
            {
                return this.imagePath;
            }
            set
            {
                this.imagePath = value;
            }
        }

        private void DoneButton_Clicked(object sender, System.EventArgs e)
        {
            App.Current.MainPage.Navigation.PushModalAsync(new InserirMesa(this.funcionario));
        }
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            if (width > height)
            {
                if (Device.Idiom == TargetIdiom.Phone)
                {
                    ErrorImage.IsVisible = false;
                }
            }
            else
            {
                ErrorImage.IsVisible = true;
            }
        }
    }
}