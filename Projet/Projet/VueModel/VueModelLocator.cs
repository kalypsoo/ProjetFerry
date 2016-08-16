using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using Projet.Vue;

namespace Projet.VueModel
{
    class VueModelLocator
    {
        public VueModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<AccueilVueModel>();
            SimpleIoc.Default.Register<RechercheFerryVueModel>();
            SimpleIoc.Default.Register<RechercheFerryResultatVueModel>();
            SimpleIoc.Default.Register<InformationFerryVueModel>();
            SimpleIoc.Default.Register<AjoutFerryVM>();
            SimpleIoc.Default.Register<ConnexionVM>();

            NavigationService navigationService = new NavigationService();
            SimpleIoc.Default.Register<INavigationService>(() => navigationService);
            navigationService.Configure("PageAccueil", typeof(Accueil));
            navigationService.Configure("PageRechercheFerry", typeof(RechercheFerry));
            navigationService.Configure("PageRechercheFerryResultat", typeof(RechercheFerryResultat));
            navigationService.Configure("PageInformationFerry", typeof(InformationFerry));
            navigationService.Configure("PageAjoutFerry", typeof(AjoutFerry));
            navigationService.Configure("PageConnexion", typeof(Connexion));
        }

        public AccueilVueModel Accueil
        {
            get { return ServiceLocator.Current.GetInstance<AccueilVueModel>(); }
        }

        public RechercheFerryVueModel RechercheFerry
        {
            get { return ServiceLocator.Current.GetInstance<RechercheFerryVueModel>(); }
        }

        public RechercheFerryResultatVueModel RechercheFerryResultat
        {
            get { return ServiceLocator.Current.GetInstance<RechercheFerryResultatVueModel>(); }
        }

        public InformationFerryVueModel InformationFerry
        {
            get { return ServiceLocator.Current.GetInstance<InformationFerryVueModel>(); }
        }

        public AjoutFerryVM AjoutFerry
        {
            get { return ServiceLocator.Current.GetInstance<AjoutFerryVM>(); }
        }

        public ConnexionVM Connexion
        {
            get { return ServiceLocator.Current.GetInstance<ConnexionVM>(); }
        }
    }
}
