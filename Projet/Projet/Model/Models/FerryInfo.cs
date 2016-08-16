using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet.Model.Models
{
    public class FerryInfo
    {
        public string nomFerry { get; set; }
        public string nIdentification { get; set; }
        public decimal prixAdulte { get; set; }
        public decimal prixEnfant { get; set; }
        public decimal prixVehicule { get; set; }
        public decimal prixRemorque { get; set; }
        public string heureDepart { get; set; }
        public string heureArrive { get; set; }
        public int nquaiDepart { get; set; }
        public int nquaiArrive { get; set; }
        public string nomCompagnie { get; set; }
        public string villeDepart { get; set; }
        public string villeArrivee { get; set; }
    }
}
