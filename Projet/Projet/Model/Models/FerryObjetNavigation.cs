using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet.Model.Models
{
    class FerryObjetNavigation
    {
        public IEnumerable<FerryInfo> listeFerry { get; set; }
        public string villeDepart { get; set; }
        public string villeArrivee { get; set; }
        public string paysDepart { get; set; }
        public string paysArrivee { get; set; }
    }
}
