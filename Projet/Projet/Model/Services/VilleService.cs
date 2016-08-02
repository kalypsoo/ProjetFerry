using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Projet.Model.Services
{
    class VilleService
    {
        public async Task<IEnumerable<String>> GetLibellePays()
        {
            var requete = new Uri("http://apiferry.azurewebsites.net/api/villes/rechercheNomPays");
            HttpClient client = new HttpClient();
            var json = await client.GetStringAsync(requete);
            IEnumerable<string> reponse = JsonConvert.DeserializeObject<IEnumerable<String>>(json);
            return reponse;
        }

        public async Task<IEnumerable<String>> getLibelleVillePays(string nomPays)
        {
            var requete = new Uri("http://apiferry.azurewebsites.net/api/villes/rechercheNomVillePays/?nomPays="+nomPays);
            HttpClient client = new HttpClient();
            var json = await client.GetStringAsync(requete);
            IEnumerable<string> reponse = JsonConvert.DeserializeObject<IEnumerable<String>>(json);
            return reponse;
        }
    }
}
