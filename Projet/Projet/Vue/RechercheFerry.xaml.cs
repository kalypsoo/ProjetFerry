using Microsoft.Practices.ServiceLocation;
using Projet.VueModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Projet.Vue
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RechercheFerry : Page
    { 
        //private DateTimeOffset? dateDepart = null;
        public RechercheFerry()
        {
            this.InitializeComponent();           
            //DataContext = new RechercheFerryVueModel();
        }

        //public void DateDepart_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        //{
        //    //Checking selected date is null 
        //    DateTimeOffset? dateDepart; 
        //    if (args.NewDate != null)
        //    {
        //        dateDepart = args.NewDate.Value;
        //    }
        //}

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ((RechercheFerryVueModel)DataContext).OnNavigatedTo(e);
        }

        public RechercheFerryVueModel RechercheFerryVueModel {
            get {
                return ServiceLocator.Current.GetInstance<RechercheFerryVueModel>();
            }           
        }
    }
}
