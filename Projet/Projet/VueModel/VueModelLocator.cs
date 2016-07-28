using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using Projet.Vue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet.VueModel
{
    class VueModelLocator
    {
        public VueModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<AccueilVueModel>();
            SimpleIoc.Default.Register<RechercheFerryVueModel>();

            NavigationService navigationService = new NavigationService();
            SimpleIoc.Default.Register<INavigationService>(() => navigationService);
            navigationService.Configure("PageAccueil", typeof(Accueil));
            navigationService.Configure("PageRechercheFerry", typeof(RechercheFerry));
        }

        public AccueilVueModel Accueil
        {
            get { return ServiceLocator.Current.GetInstance<AccueilVueModel>(); }
        }

        public RechercheFerryVueModel RechercheFerry
        {
            get { return ServiceLocator.Current.GetInstance<RechercheFerryVueModel>(); }
        }
    }
}
