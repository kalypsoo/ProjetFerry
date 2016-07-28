using Projet.Vue;
using Projet.VueModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Projet
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Accueil : Page
    {
        public Accueil()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ((AccueilVueModel)DataContext).OnNavigatedTo(e);
        }

        //protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        //{
        //    ((AccueilVueModel)DataContext).OnNavigatedFrom(e);
        //}



        //private void GoToRechercheFerry(object sender, RoutedEventArgs e)
        //{
        //    Frame.Navigate(typeof(RechercheFerry));
        //}

        //private void GoToConnexion(object sender, RoutedEventArgs e)
        //{
        //    Frame.Navigate(typeof(Connexion));
        //}

        //private void GoToAPropos(object sender, RoutedEventArgs e)
        //{
        //    Frame.Navigate(typeof(APropos));
        //}
    }
}
