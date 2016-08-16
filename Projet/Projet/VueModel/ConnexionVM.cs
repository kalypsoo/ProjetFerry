using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Projet.Model.Models;
using Projet.Model.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;

namespace Projet.VueModel
{
    class ConnexionVM : ViewModelBase, INotifyPropertyChanged
    {
        private INavigationService _navigationService;
        public RelayCommand Connexion { get; set; }

        public ConnexionVM(INavigationService navigationService)
        {
            _navigationService = navigationService;
            Connexion = new RelayCommand(SeConnecter,VerifierFormulaire);
        }

        public bool VerifierFormulaire()
        {
            if (String.IsNullOrEmpty(Identifiant) || String.IsNullOrEmpty(Password))
                return false;
            else
                return true;
        }

        public async void SeConnecter()
        {
            ConnexionService serviceConnexion = new ConnexionService();
            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            StringCodeErreur reponse = await serviceConnexion.Connexion(identifiant, password);
            if (reponse.codeErreur == 0)
            {
                tvaGerant = reponse.texte;
                if (String.IsNullOrEmpty(tvaGerant))
                {
                    MessageDialog message = new MessageDialog(loader.GetString("ConnexionIDErreurDialog"),loader.GetString("ConnexionIDErreurTitreDialog"));
                    await message.ShowAsync();
                }
                else
                {
                    Password = "";
                    GoToAccueil();
                }                    
            }
            else
            {
                MessageDialog message = new MessageDialog(loader.GetString("ErreurInternetDialog"), loader.GetString("ErreurInternetTitreDialog"));
                await message.ShowAsync();
            }            
        }

        private static string tvaGerant;
        public static string TvaGerant
        {
            get { return tvaGerant; }
            set {
                if (value != tvaGerant)
                {
                    tvaGerant = value;
                }
            }
        }

        private string identifiant;
        public string Identifiant
        {
            get { return identifiant; }
            set
            {
                if (value != identifiant)
                {
                    identifiant = value;
                    Connexion.RaiseCanExecuteChanged();
                }
            }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                if (value != password)
                {
                    password = value;
                    RaisePropertyChanged("Password");
                    Connexion.RaiseCanExecuteChanged();
                }
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

        public void GoToRechercheFerry()
        {
            _navigationService.NavigateTo("PageRechercheFerry");
        }

        public void GoToAccueil()
        {
            _navigationService.NavigateTo("PageAccueil");
        }
    }
}
