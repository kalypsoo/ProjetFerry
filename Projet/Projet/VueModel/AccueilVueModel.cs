using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Projet.Vue;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Projet.VueModel
{
    public class AccueilVueModel : ViewModelBase, INotifyPropertyChanged
    {
        private INavigationService _navigationService;

        public AccueilVueModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
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

        public void OnNavigatedTo(NavigationEventArgs e)
        {
            //throw new NotImplementedException(); // Par exemple recuperer un objet
        }

        private void GoToRechercheFerry()
        {
            _navigationService.NavigateTo("PageRechercheFerry");
        }

        //internal void OnNavigatedFrom(NavigatingCancelEventArgs e)
        //{
        //    throw new NotImplementedException();
        //}

    }
}
