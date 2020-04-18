using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Restaurante.ViewModels.ErrorAndEmpty
{
    /// <summary>
    /// ViewModel for no item page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class NoItemPageViewModel : BaseViewModel
    {
        private string imagePath;
        public NoItemPageViewModel()
        {
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
    }
}
