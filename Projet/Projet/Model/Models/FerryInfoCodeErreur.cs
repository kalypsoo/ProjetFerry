using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet.Model.Models
{
    public class FerryInfoCodeErreur
    {
        public IEnumerable<FerryInfo> listeFerry { get; set; }
        public int codeErreur { get; set; }
    }
}
