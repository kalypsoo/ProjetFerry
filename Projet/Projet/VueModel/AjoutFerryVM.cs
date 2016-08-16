using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Projet.Helpers;
using Projet.Model.Models;
using Projet.Model.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows.Input;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

namespace Projet.VueModel
{
    public class AjoutFerryVM : ViewModelBase, INotifyPropertyChanged
    {
        private INavigationService _navigationService;
        private RegexHelper regex;
        private int idVilleDepart, idVilleArrivee;


        public AjoutFerryVM(INavigationService navigationService)
        {
            _navigationService = navigationService;
            Ajouter = new RelayCommand(AjouterFerry);
            regex = new RegexHelper();
            TimeSpan mtn = DateTime.Now.TimeOfDay;
            HeureDepart = getHeureHHMM(mtn);
            HeureArrivee = getHeureHHMM(mtn);
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

        public async void AjouterFerry()
        {
            string messageErreur = VerifierFormulaire();
            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            if (!String.IsNullOrEmpty(messageErreur))
            {               
                MessageDialog message = new MessageDialog(messageErreur, loader.GetString("AjoutTitreErreurDialog"));
                await message.ShowAsync();
            }
            else
            {
                MessageDialog message = new MessageDialog(loader.GetString("AjoutConfirmationDialog"), loader.GetString("AjoutConfirmationTitreDialog"));
                message.Commands.Add(new UICommand { Label = loader.GetString("OuiDialog"), Id = 0 });
                message.Commands.Add(new UICommand { Label = loader.GetString("NonDialog"), Id = 1 });

                var res = await message.ShowAsync();

                if ((int)res.Id == 0)
                {
                    FerryService serviceFerry = new FerryService();
                    VilleService serviceVille = new VilleService();
                    IntCodeErreur reponseIdVilleDep = await serviceVille.getIdVille(PaysDepSelec, VilleDepSelec);
                    if (reponseIdVilleDep.codeErreur == 0)
                    {
                        idVilleDepart = reponseIdVilleDep.entier;
                        IntCodeErreur reponseIdVilleArr = await serviceVille.getIdVille(PaysArrSelec, villeArrSelec);
                        if (reponseIdVilleArr.codeErreur == 0)
                        {
                            idVilleArrivee = reponseIdVilleArr.entier;
                            Ferry nouveauFerry = new Ferry(nomFerry, identifiantFerry, Double.Parse(tarifAdulte, CultureInfo.InvariantCulture), Double.Parse(tarifEnfant, CultureInfo.InvariantCulture), Double.Parse(tarifVehicule, CultureInfo.InvariantCulture), Double.Parse(tarifRemorque, CultureInfo.InvariantCulture), heureDepart, heureArrivee, int.Parse(quaiDepart), int.Parse(quaiArrivee), idVilleDepart, idVilleArrivee, ConnexionVM.TvaGerant, dateDepart);
                            int reponse = await serviceFerry.SetFerryBD(nouveauFerry);
                            if (reponse == 0)
                            {
                                SuccesAjout();
                            }
                            else
                            {
                                if (reponse == 1)
                                {
                                    message = new MessageDialog(loader.GetString("AjoutFerryExistantDialog"), loader.GetString("AjoutFerryExistantTitreDialog"));
                                    await message.ShowAsync();
                                }
                                else
                                {
                                    message = new MessageDialog(loader.GetString("ErreurInternetDialog"), loader.GetString("ErreurInternetTitreDialog"));
                                    await message.ShowAsync();
                                }
                            }
                        }
                        else
                        {
                            message = new MessageDialog(loader.GetString("ErreurInternetDialog"), loader.GetString("ErreurInternetTitreDialog"));
                            await message.ShowAsync();
                        }

                    }
                    else
                    {
                        message = new MessageDialog(loader.GetString("ErreurInternetDialog"), loader.GetString("ErreurInternetTitreDialog"));
                        await message.ShowAsync();
                    }
                }    
            }
        }

        public async void SuccesAjout()
        {
            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();

            MessageDialog message = new MessageDialog(loader.GetString("AjoutSuccesDialog"), loader.GetString("AjoutSuccesTitreDialog"));
            message.Commands.Add(new UICommand { Label = loader.GetString("AjoutRetourAccueilDialog"), Id = 0 });
            message.Commands.Add(new UICommand { Label = loader.GetString("AjoutResterDialog"), Id = 1 });

            var res = await message.ShowAsync();

            if ((int)res.Id == 0)
            {
                RafraichirPage();
                _navigationService.NavigateTo("PageAccueil");
            }

            else
            {
                RafraichirPage();
                _navigationService.NavigateTo("PageAjoutFerry");
            }                    
        }

        public void RafraichirPage()
        {
            NomFerry = "";
            IdentifiantFerry = "";
            TarifEnfant = "";
            TarifAdulte = "";
            TarifVehicule = "";
            TarifRemorque = "";
            QuaiDepart = "";
            QuaiArrivee = "";
            DateDepart = new DateTime();
            TimeSpan mtn = DateTime.Now.TimeOfDay;
            HeureDepart = getHeureHHMM(mtn);
            HeureArrivee = getHeureHHMM(mtn);            
        }

        public RelayCommand Ajouter { get; set; }

        private string nomFerry;
        public string NomFerry
        {
            get { return nomFerry; }
            set
            {
                if (value != nomFerry)
                {
                    nomFerry = value;
                    RaisePropertyChanged("NomFerry");
                }
            }
        }

        private string identifiantFerry;
        public string IdentifiantFerry
        {
            get { return identifiantFerry; }
            set
            {
                if (value != identifiantFerry)
                {
                    identifiantFerry = value;
                    RaisePropertyChanged("IdentifiantFerry");
                }
            }
        }

        private string tarifAdulte;
        public string TarifAdulte
        {
            get { return tarifAdulte; }
            set
            {
                if (value != tarifAdulte)
                {
                    tarifAdulte = value;
                    RaisePropertyChanged("TarifAdulte");
                }
            }
        }
        private string tarifEnfant;
        public string TarifEnfant
        {
            get { return tarifEnfant; }
            set
            {
                if (value != tarifEnfant)
                {
                    tarifEnfant = value;
                    RaisePropertyChanged("TarifEnfant");
                }
            }
        }
        private string tarifVehicule;
        public string TarifVehicule
        {
            get { return tarifVehicule; }
            set
            {
                if (value != tarifVehicule)
                {
                    tarifVehicule = value;
                    RaisePropertyChanged("TarifVehicule");
                }
            }
        }
        private string tarifRemorque;
        public string TarifRemorque
        {
            get { return tarifRemorque; }
            set
            {
                if (value != tarifRemorque)
                {
                    tarifRemorque = value;
                    RaisePropertyChanged("TarifRemorque");
                }
            }
        }

        private string quaiDepart;
        public string QuaiDepart
        {
            get { return quaiDepart; }
            set
            {
                if (value != quaiDepart)
                {
                    quaiDepart = value;
                    RaisePropertyChanged("QuaiDepart");
                }
            }
        }

        private string paysDepartSelectionne;
        public string PaysDepSelec
        {
            get { return paysDepartSelectionne; }
            set
            {
                if (value != paysDepartSelectionne)
                {
                    paysDepartSelectionne = value;
                    InitVille(villeDepart, value, true);
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
                    RaisePropertyChanged("VilleArrSelec");
                }
            }
        }

        private string quaiArrivee;
        public string QuaiArrivee
        {
            get { return quaiArrivee; }
            set
            {
                if (value != quaiArrivee)
                {
                    quaiArrivee = value;
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
                }
            }
        }

        private string heureDepart;
        public string HeureDepart
        {
            get { return heureDepart; }
            set
            {
                if (value != heureDepart)
                {
                    heureDepart = value;
                }
            }
        }

        private string heureArrivee;
        public string HeureArrivee
        {
            get { return heureArrivee; }
            set
            {
                if (value != heureArrivee)
                {
                    heureArrivee = value;
                }
            }
        }

        public void CalendarDatePicker_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            var dateString = sender.Date.ToString();
            DateTime dateTime = DateTime.Parse(dateString).Date;
            DateDepart = dateTime;
        }

        public void TimePickerHeureDepart_TimeChanged(object sender, TimePickerValueChangedEventArgs e)
        {
            HeureDepart = getHeureHHMM(e.NewTime);
        }

        public void TimePickerHeureArrivee_TimeChanged(object sender, TimePickerValueChangedEventArgs e)
        {
            HeureArrivee = getHeureHHMM(e.NewTime);
        }

        private ObservableCollection<string> paysArrivee = new ObservableCollection<string>();
        public ObservableCollection<string> PaysArrivee
        {
            get { InitPays(paysArrivee, false);
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

        private ObservableCollection<string> villeDepart = new ObservableCollection<string>();
        public ObservableCollection<string> VilleDepart
        {
            get { return villeDepart; }
            set
            {
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


        public string VerifierFormulaire()
        {
            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            string messageErreur = "";
            DateTime mtn = DateTime.Today;

            if (String.IsNullOrEmpty(NomFerry) || !regex.estTexteValide(NomFerry))
                messageErreur += loader.GetString("AjoutNomFerryErreurDialog") + "\n";
            if (String.IsNullOrEmpty(IdentifiantFerry) || !regex.estTexteValide(IdentifiantFerry))
                messageErreur += loader.GetString("AjoutIdenfiantErreurDialog") + "\n";
            if (String.IsNullOrEmpty(TarifEnfant) || !regex.estPrixPersonne(TarifEnfant))
                messageErreur += loader.GetString("AjoutPrixEnfantErreurDialog") + "\n";
            if (String.IsNullOrEmpty(TarifAdulte) || !regex.estPrixPersonne(TarifAdulte))
                messageErreur += loader.GetString("AjoutPrixAdulteErreurDialog") + "\n";
            if (String.IsNullOrEmpty(TarifVehicule) || !regex.estPrixVehicule(TarifVehicule))
                messageErreur += loader.GetString("AjoutPrixVehiculeErreurDialog") + "\n";
            if (String.IsNullOrEmpty(TarifRemorque) || !regex.estPrixVehicule(TarifRemorque))
                messageErreur += loader.GetString("AjoutPrixRemorqueErreurDialog") + "\n";
            if (String.Compare(HeureDepart,HeureArrivee) >= 0)
                messageErreur += loader.GetString("AjoutHeuresErreurDialog") + "\n";
            if (String.IsNullOrEmpty(QuaiDepart) || !regex.estNumQuaiValide(QuaiDepart))
                messageErreur += loader.GetString("AjoutNumQuaiDepartErreurDialog") + "\n";
            if (String.IsNullOrEmpty(QuaiArrivee) || !regex.estNumQuaiValide(QuaiArrivee))
                messageErreur += loader.GetString("AjoutNumQuaiArriveeErreurDialog") + "\n";
            if (PaysDepSelec == PaysArrSelec && VilleDepSelec == VilleArrSelec)
                messageErreur += loader.GetString("AjoutDepartArriveeIdemErreurDialog") + "\n";
            if (DateTime.Compare(DateDepart, mtn.Date) < 0)
                messageErreur += loader.GetString("AjoutDateDepartErreurDialog") + "\n";

            return messageErreur;
        }

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
    }
}
