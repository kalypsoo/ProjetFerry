using Projet.VueModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Projet.Vue
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RechercheFerryResultat : Page
    {
        public RechercheFerryResultat()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ((RechercheFerryResultatVueModel)DataContext).OnNavigatedTo(e);
        }
    }
}
