using Microsoft.Practices.ServiceLocation;
using Projet.VueModel;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Projet.Vue
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AjoutFerry : Page
    {
        public AjoutFerry()
        {
            this.InitializeComponent();
        }

        public AjoutFerryVM AjoutFerryVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AjoutFerryVM>();
            }
        }
    }
}
