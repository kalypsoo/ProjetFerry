using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet.Model.Models
{
    class Ferry
    {
        public string nom { get; set; }
        public string nIdentification { get; set; }
        public double prixAdulte { get; set; } 
        public double prixEnfant { get; set; } 
        public double prixVehicule { get; set; } 
        public double prixRemorque { get; set; } 
        public string heureDepart { get; set; }
        public string heureArrive { get; set; }
        public int nquaiDepart { get; set; }
        public int nquaiArrive { get; set; }
        public int numeroVilleDepart { get; set; } 
        public int numeroVilleArrivee { get; set; }
        public string nTVA{ get; set; } 
        public DateTime dateDepart { get; set; }

        public Ferry(string nom, string id, double prixA, double prixE, double prixV, double prixR, string hDepart, string hArrive, int nQD, int nQA, int nVD, int nVA, string tva, DateTime dateDep)
        {
            this.nom = nom;
            nIdentification = id;
            prixAdulte = prixA;
            prixEnfant = prixE;
            prixVehicule = prixV;
            prixRemorque = prixR;
            heureDepart = hDepart;
            heureArrive = hArrive;
            nquaiDepart = nQD;
            nquaiArrive = nQA;
            numeroVilleDepart = nVD;
            numeroVilleArrivee = nVA;
            nTVA = tva;
            dateDepart = dateDep;
        }
    }
}
