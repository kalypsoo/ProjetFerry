using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Projet.Model.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using Windows.UI.Xaml.Navigation;

namespace Projet.VueModel
{
    public class RechercheFerryResultatVueModel : ViewModelBase, INotifyPropertyChanged
    {
        private INavigationService _navigationService;
        private FerryObjetNavigation resultat;

        public RechercheFerryResultatVueModel(INavigationService navigationService)
        {
            _navigationService = navigationService;           
        }

        public void OnNavigatedTo(NavigationEventArgs e)
        {
            resultat = (FerryObjetNavigation)e.Parameter;
        }

        public string VilleDepart
        {
            get { return resultat.paysDepart+", "+resultat.villeDepart; }
        }

        public string VilleArrivee
        {
            get { return resultat.paysArrivee+", "+resultat.villeArrivee; }
        }

        public IEnumerable<FerryInfo> ResultatFerry
        {
            get {
                List<FerryInfo> liste = (List<FerryInfo>) resultat.listeFerry;
                liste.Sort((x, y) => x.heureDepart.CompareTo(y.heureDepart));
                return liste;
            }
        }

        private FerryInfo ferrySelectionne = null;
        public FerryInfo FerrySelectionne
        {
            get { return ferrySelectionne; }
            set
            {
                if (ferrySelectionne != value)
                    ferrySelectionne = value;
                ferrySelectionne.villeDepart = VilleDepart;
                ferrySelectionne.villeArrivee = VilleArrivee;
                _navigationService.NavigateTo("PageInformationFerry", FerrySelectionne);
            }
        }

        private ICommand _goToAccueil;
        public ICommand GoToAccueilCommand
        {
            get
            {
                if (_goToAccueil == null)
                    _goToAccueil = new RelayCommand(() => GoToAccueil());
                return _goToAccueil;
            }
        }

        private ICommand _goToAjouterFerry;
        public ICommand GoToAjouterFerryCommand
        {
            get
            {
                if (_goToAjouterFerry == null)
                    _goToAjouterFerry = new RelayCommand(() => GoToAjouterFerry());
                return _goToAjouterFerry;
            }
        }

        private ICommand _goToConnexion;
        public ICommand GoToConnexionCommand
        {
            get
            {
                if (_goToConnexion == null)
                    _goToConnexion = new RelayCommand(() => GoToConnexion());
                return _goToConnexion;
            }
        }


        private void GoToAjouterFerry()
        {
            _navigationService.NavigateTo("PageAjoutFerry");
        }

        private void GoToConnexion()
        {
            _navigationService.NavigateTo("PageConnexion");
        }

        public void GoToAccueil()
        {
            _navigationService.NavigateTo("PageAccueil");
        }

        private bool estConnecte;
        public bool EstConnecte
        {
            get
            {
                if (String.IsNullOrEmpty(ConnexionVM.TvaGerant))
                {
                    estConnecte = true;
                }
                else
                {
                    estConnecte = false;
                }

                return estConnecte;
            }
        }

        private bool estDeconnecte;
        public bool EstDeconnecte
        {
            get
            {
                if (String.IsNullOrEmpty(ConnexionVM.TvaGerant))
                {
                    estDeconnecte = false;
                }
                else
                {
                    estDeconnecte = true;
                }
                return estDeconnecte;
            }
        }
    }
}
