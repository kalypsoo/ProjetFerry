using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Projet.Helpers
{
    class RegexHelper
    {
        public bool estPrixPersonne(string texte)
        {
            Regex regex = new Regex("^[0-9]{1,3}([.][0-9]{0,2})?$");
            return regex.IsMatch(texte);
        }

        public bool estPrixVehicule(string texte)
        {
            Regex regex = new Regex("^[0-9]{1,4}([.][0-9]{0,2})?$");
            return regex.IsMatch(texte);
        }

        public bool estNumQuaiValide(string texte)
        {
            Regex regex = new Regex("^[0-9]{1,3}$");
            return regex.IsMatch(texte);
        }

        public bool estTexteValide(string texte)
        {
            Regex regex = new Regex(@"^[a-zA-Z0-9 ]+$");
            return regex.IsMatch(texte);
        }
    }
}
