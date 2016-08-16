using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Projet.Model.Models;
using System;
using System.ComponentModel;
using System.Windows.Input;
using Windows.UI.Xaml.Navigation;

namespace Projet.VueModel
{
    class InformationFerryVueModel : ViewModelBase, INotifyPropertyChanged
    {
        private INavigationService _navigationService;
        private FerryInfo ferry;

        public InformationFerryVueModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public void OnNavigatedTo(NavigationEventArgs e)
        {
            ferry = (FerryInfo) e.Parameter;
        }

        public string NomCompagnie
        {
            get { return ferry.nomCompagnie; }
        }

        public string NomFerry
        {
            get { return ferry.nomFerry; }
        }

        public string VilleDepart
        {
            get { return ferry.villeDepart; }
        }

        public string QuaiDepart
        {
            get { return ferry.nquaiDepart.ToString(); }
        }

        public string VilleArrivee
        {
            get { return ferry.villeArrivee; }
        }

        public string QuaiArrive
        {
            get { return ferry.nquaiArrive.ToString(); }
        }

        public string PrixAdulte
        {
            get { return ferry.prixAdulte.ToString() + "€"; }
        }

        public string PrixEnfant
        {
            get { return ferry.prixEnfant.ToString() + "€"; }
        }

        public string PrixVehicule
        {
            get { return ferry.prixVehicule.ToString() + "€"; }
        }

        public string PrixRemorque
        {
            get { return ferry.prixRemorque.ToString() + "€"; }
        }

        private ICommand _retour;
        public ICommand Retour
        {
            get
            {
                if (_retour == null)
                    _retour = new RelayCommand(() => GoToRetour());
                return _retour;
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

        public void GoToAjouterFerry()
        {
            _navigationService.NavigateTo("PageAjoutFerry");
        }

        public void GoToConnexion()
        {
            _navigationService.NavigateTo("PageConnexion");
        }

        public void GoToAccueil()
        {
            _navigationService.NavigateTo("PageAccueil");
        }

        public void GoToRetour()
        {
            _navigationService.GoBack();
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
