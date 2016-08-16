using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System;
using System.ComponentModel;
using System.Windows.Input;
using Windows.UI.Popups;

namespace Projet.VueModel
{
    public class AccueilVueModel : ViewModelBase, INotifyPropertyChanged
    {
        private INavigationService _navigationService;

        public AccueilVueModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            // ApplicationLanguages.PrimaryLanguageOverride = "en-US"; // Test pour changer la langue (besoin de refresh la page pour que sa soit pris en compte)
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

        private ICommand _goToRechercheFerry;
        public ICommand GoToRechercheFerryCommand
        {
            get
            {
                if (_goToRechercheFerry == null)
                    _goToRechercheFerry = new RelayCommand(() => GoToRechercheFerry());
                return _goToRechercheFerry;
            }
        }

        private ICommand deconnexion;
        public ICommand Deconnexion
        {
            get
            {
                if (deconnexion == null)
                {
                    deconnexion = new RelayCommand(() => ResterAccueil());
                }
                return deconnexion;
            }
        }

        public void ResterAccueil()
        {
            ConfirmationDeconnexion();
        }
        public async void ConfirmationDeconnexion()
        {
            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            MessageDialog message = new MessageDialog(loader.GetString("DeconnexionValidationDialog"), loader.GetString("DeconnexionTitreDialog"));
            message.Commands.Add(new UICommand { Label = loader.GetString("OuiDialog"), Id = 0 });
            message.Commands.Add(new UICommand { Label = loader.GetString("NonDialog"), Id = 1 });

            var res = await message.ShowAsync();
            
            if ((int)res.Id == 0)
            {
                ConnexionVM.TvaGerant = "";
                RafraichirAccueil();
            }
        }

        private void GoToRechercheFerry()
        {
            _navigationService.NavigateTo("PageRechercheFerry");
        }

        private void GoToAjouterFerry()
        {
            _navigationService.NavigateTo("PageAjoutFerry");
        }

        private void GoToConnexion()
        {
            _navigationService.NavigateTo("PageConnexion");
        }

        private void RafraichirAccueil()
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
