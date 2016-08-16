using Microsoft.Practices.ServiceLocation;
using Projet.VueModel;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Projet.Vue
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RechercheFerry : Page
    { 
        public RechercheFerry()
        {
            this.InitializeComponent();           
        }

        public RechercheFerryVueModel RechercheFerryVueModel {
            get {
                return ServiceLocator.Current.GetInstance<RechercheFerryVueModel>();
            }           
        }
    }
}
