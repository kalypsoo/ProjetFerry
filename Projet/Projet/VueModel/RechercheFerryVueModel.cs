using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Projet.Model.Models;
using Projet.Model.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

namespace Projet.VueModel
{
    public class RechercheFerryVueModel : ViewModelBase, INotifyPropertyChanged
    {
        private INavigationService _navigationService;

        public RechercheFerryVueModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            Rechercher = new RelayCommand(AfficherResultat, VerifierFormulaire);
        }

        private string paysDepartSelectionne;
        public string PaysDepSelec // Quand le pays change, appelle la methode initVille
        {
            get { return paysDepartSelectionne; }
            set {
                if (value != paysDepartSelectionne)
                {
                    paysDepartSelectionne = value;
                    InitVille(villeDepart, value, true);
                    Rechercher.RaiseCanExecuteChanged();                    
                    RaisePropertyChanged("PaysDepSelec");
                }        
            }
        }

        private string paysArriveeSelectionne;
        public string PaysArrSelec
        {
            get { return paysArriveeSelectionne; }
            set
            {
                if (value != paysArriveeSelectionne)
                {
                    paysArriveeSelectionne = value;
                    InitVille(villeArrivee, value, false);
                    Rechercher.RaiseCanExecuteChanged();
                    RaisePropertyChanged("PaysArrSelec");
                }
            }
        }

        private string villeDepSelec;
        public string VilleDepSelec
        {
            get { return villeDepSelec; }
            set
            {
                if (value != villeDepSelec)
                {
                    villeDepSelec = value;
                    Rechercher.RaiseCanExecuteChanged();
                    RaisePropertyChanged("VilleDepSelec"); 
                }
            }
        }

        private string villeArrSelec;
        public string VilleArrSelec
        {
            get { return villeArrSelec; }
            set
            {
                if (value != villeArrSelec)
                {
                    villeArrSelec = value;
                    Rechercher.RaiseCanExecuteChanged();
                    RaisePropertyChanged("VilleArrSelec");
                }
            }
        }

        private DateTime dateDepart;
        public DateTime DateDepart
        {
            get { return dateDepart; }
            set
            {
                if (value != dateDepart)
                {
                    dateDepart = value;                    
                    Rechercher.RaiseCanExecuteChanged();
                }
            }
        }

        private ObservableCollection<string> paysArrivee = new ObservableCollection<string>();
        public ObservableCollection<string> PaysArrivee
        {
            get
            {
                InitPays(paysArrivee,false);
                return paysArrivee;
            }
        }

        private ObservableCollection<string> paysDepart = new ObservableCollection<string>();
        public ObservableCollection<string> PaysDepart
        {
            get
            {
                InitPays(paysDepart,true);
                return paysDepart;
            }
        }

        public async void InitPays(ObservableCollection<string> listePays, Boolean estPaysDepart)
        {
            VilleService service = new VilleService();
            ListStringCodeErreur reponse = new ListStringCodeErreur();
            reponse = await service.GetLibellePays();
            if (reponse.codeErreur == 0)
            {
                IEnumerable<String> pays = reponse.listeTexte;
                listePays.Clear();
                foreach (var p in pays)
                {
                    listePays.Add(p);
                }
                if (estPaysDepart)
                    PaysDepSelec = listePays.FirstOrDefault(); // Permet d'afficher le premier item de la combobox a la place d'un vide
                else
                    PaysArrSelec = listePays.FirstOrDefault();
            }
            else
            {
                var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
                MessageDialog message = new MessageDialog(loader.GetString("ErreurInternetDialog"), loader.GetString("ErreurInternetTitreDialog"));
                await message.ShowAsync();
            }
        }

        private ObservableCollection<string> villeDepart = new ObservableCollection<string>();
        public ObservableCollection<string> VilleDepart
        {
            get { return villeDepart; }
            set {
                if (villeDepart != value)
                {
                    villeDepart = value;
                }
            }
        }

        private ObservableCollection<string> villeArrivee = new ObservableCollection<string>();
        public ObservableCollection<string> VilleArrivee
        {
            get { return villeArrivee; }
            set
            {
                if (villeArrivee != value)
                {
                    villeArrivee = value;
                }
            }
        }

        public async void InitVille(ObservableCollection<string> listeVille, string pays, Boolean estVilleDepart)
        {
            VilleService service = new VilleService();
            ListStringCodeErreur reponse = new ListStringCodeErreur();
            reponse = await service.getLibelleVillePays(pays);
            if (reponse.codeErreur == 0)
            {
                IEnumerable<String> ville = reponse.listeTexte;

                listeVille.Clear();
                foreach (var v in ville)
                {
                    listeVille.Add(v);
                }
                if (estVilleDepart)
                    VilleDepSelec = listeVille.FirstOrDefault();
                else
                    VilleArrSelec = listeVille.FirstOrDefault();
            }
            else
            {
                var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
                MessageDialog message = new MessageDialog(loader.GetString("ErreurInternetDialog"), loader.GetString("ErreurInternetTitreDialog"));
                await message.ShowAsync();
            }
        }

        public void CalendarDatePicker_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            var dateString = sender.Date.ToString();
            DateTime dateTime = DateTime.Parse(dateString).Date;
            DateDepart = dateTime;
        }

        public RelayCommand Rechercher { get; set; }

        private bool VerifierFormulaire()
        {
            bool erreur = true;
            if (PaysDepSelec == PaysArrSelec && VilleDepSelec == VilleArrSelec)
                erreur = false;
            if (DateTime.Compare(DateDepart, DateTime.Today) < 0)
                erreur = false;

            return erreur;
        }

        public async void AfficherResultat()
        {
            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            FerryObjetNavigation objetNavigationFerry;
            Recherche recherche = new Recherche();
            VilleService service = new VilleService();
            IntCodeErreur reponseIdVilleDep = await service.getIdVille(PaysDepSelec, VilleDepSelec);
            if (reponseIdVilleDep.codeErreur == 0)
            {
                recherche.numVilleDepart = reponseIdVilleDep.entier;
                IntCodeErreur reponseIdVilleArr = await service.getIdVille(PaysArrSelec, villeArrSelec);
                if (reponseIdVilleArr.codeErreur == 0)
                {
                    recherche.numVilleArrivee = reponseIdVilleArr.entier;
                    recherche.dateDepart = DateDepart.Date.ToString("yyyy-MM-dd"); // Conversion car le format date sql et c# sont differents
                    FerryInfoCodeErreur reponse = await service.getResultatRecherche(recherche);
                    if (reponse.codeErreur == 0)
                    {
                        if (reponse.listeFerry.Count() == 0)
                        {
                            MessageDialog message = new MessageDialog(loader.GetString("RechercheAucunFerryDialog"), loader.GetString("RechercheAucunFerryTitreDialog"));
                            await message.ShowAsync();
                        }
                        else
                        {
                            objetNavigationFerry = new FerryObjetNavigation();
                            objetNavigationFerry.listeFerry = reponse.listeFerry;
                            objetNavigationFerry.villeDepart = VilleDepSelec;
                            objetNavigationFerry.villeArrivee = VilleArrSelec;
                            objetNavigationFerry.paysDepart = PaysDepSelec;
                            objetNavigationFerry.paysArrivee = PaysArrSelec;
                            _navigationService.NavigateTo("PageRechercheFerryResultat", objetNavigationFerry);
                        }
                    }
                    else
                    {
                        MessageDialog message = new MessageDialog(loader.GetString("ErreurInternetDialog"), loader.GetString("ErreurInternetTitreDialog"));
                        await message.ShowAsync();
                    }
                }
                else
                {
                    MessageDialog message = new MessageDialog(loader.GetString("ErreurInternetDialog"), loader.GetString("ErreurInternetTitreDialog"));
                    await message.ShowAsync();
                }
            }
            else
            {
                MessageDialog message = new MessageDialog(loader.GetString("ErreurInternetDialog"), loader.GetString("ErreurInternetTitreDialog"));
                await message.ShowAsync();
            }
        }

        // Renvoie l'heure au format hh:mm 
        public string getHeureHHMM(TimeSpan heure)
        {
            string heureConvertie = "";
            if (heure.Hours.ToString().Length == 1)
                heureConvertie += "0" + heure.Hours + ":";
            else
                heureConvertie += heure.Hours + ":";

            if (heure.Minutes.ToString().Length == 1)
                heureConvertie += "0" + heure.Minutes;
            else
                heureConvertie += heure.Minutes;

            return heureConvertie;
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
    }
}