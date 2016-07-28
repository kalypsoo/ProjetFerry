using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet.Model.Models
{
    class Ferry
    {
        private string nom { get; set; }
        private int nIdentification { get; set; }
        private double prixAdulte { get; set; } 
        private double prixEnfant { get; set; } 
        private double prixVehicule { get; set; } 
        private double prixRemorque { get; set; } 
        private DateTime heureDepart { get; set; }
        private DateTime heureArrive { get; set; }
        private int nquaiDepart { get; set; }
        private int nquaiArrive { get; set; }
        private int numeroVilleDepart { get; set; } //a remplacer par le model correspondant ? dans l'app weather c'est une entity ville pour faire le lien a une météo ???
        private int numeroVilleArrivee { get; set; } //
        private string nTVA{ get; set; } //
        private DateTime dateDepart { get; set; }
    }
}
