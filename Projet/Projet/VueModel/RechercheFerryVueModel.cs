using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Projet.Model;
using Projet.Model.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Projet.VueModel
{
    public class RechercheFerryVueModel : ViewModelBase, INotifyPropertyChanged
    {
        private INavigationService _navigationService;

        public RechercheFerryVueModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            Rechercher = new RelayCommand(GoToAccueil, verifierFormulaire);
        }

        // Getter & Setter 
        private string paysDepartSelectionne;
        public string PaysDepSelec // Quand le pays change, appelle la methode initVille
        {
            get { return paysDepartSelectionne; }
            set {
                if (value != paysDepartSelectionne)
                {
                    paysDepartSelectionne = value;
                    Rechercher.RaiseCanExecuteChanged();
                    InitVille(villeDepart,value,true);
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
                    Rechercher.RaiseCanExecuteChanged();
                    InitVille(villeArrivee, value, false);
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

        // Propriete



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

        public async void InitPays(ObservableCollection<string> listePays,Boolean estPaysDepart)
        {
            VilleService service = new VilleService();
            IEnumerable<String> pays = await service.GetLibellePays();

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

        private ObservableCollection<string> villeDepart = new ObservableCollection<string>();
        public ObservableCollection<string> VilleDepart
        {
            get { return villeDepart; }
            set {
                if (villeDepart != value)
                {
                    villeDepart = value;
                    Rechercher.RaiseCanExecuteChanged();
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
                    Rechercher.RaiseCanExecuteChanged();
                }
            }
        }

        public async void InitVille(ObservableCollection<string> listeVille, string pays,Boolean estVilleDepart)
        {
            VilleService service = new VilleService();
            IEnumerable<String> ville = await service.getLibelleVillePays(pays);

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

        public void CalendarDatePicker_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            var dateString = sender.Date.ToString();
            DateTime dateTime = DateTime.Parse(dateString).Date;
            DateDepart = dateTime;
        }

        public RelayCommand Rechercher { get; set; }

        private bool verifierFormulaire()
        {
            if (!String.IsNullOrEmpty(PaysDepSelec))
                return true;
            else
                return false;
        }

        public void GoToAccueil()
        {
            _navigationService.NavigateTo("PageAccueil");
        }



        public void OnNavigatedTo(NavigationEventArgs e)
        {
            // throw new NotImplementedException();
        }

        //private string test;
        //public string Test
        //{
        //    get { return test; }
        //    set
        //    {
        //        if (value != test)
        //        {
        //            test = value;
        //            RaisePropertyChanged("Test");
        //        }
        //    }
        //}


    }





    //private async void ShowMessageDialog()
    //{
    //    MessageDialog msgbox = new MessageDialog("coucou");
    //    await msgbox.ShowAsync();
    //}

    //private ICommand _validerRecherche;
    //public ICommand ValiderRecherche
    //{
    //    get
    //    {
    //        if (_validerRecherche == null)
    //            _validerRecherche = new RelayCommand(GoToAccueil, canExecute); // Ca fonctionne le probleme avec canExecute c'est que ca doit rafraichir la page sinon prend pas en compte les changements (canExecute set a false ==> bouton desactiver)

    //        return _validerRecherche;
    //    }
    //}
}
