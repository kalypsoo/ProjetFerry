using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Windows.Networking.Connectivity;
using Projet.Model.Models;

namespace Projet.Model.Services
{
    class VilleService
    {
        public async Task<ListStringCodeErreur> GetLibellePays()
        {
            ListStringCodeErreur reponse = new ListStringCodeErreur();
            reponse.codeErreur = 0;
            try
            {
                var requete = new Uri("http://apiferry.azurewebsites.net/api/villes/rechercheNomPays");
                HttpClient client = new HttpClient();
                var json = await client.GetStringAsync(requete);
                reponse.listeTexte = JsonConvert.DeserializeObject<IEnumerable<String>>(json);
                return reponse;
            }
            catch (HttpRequestException e)
            {
                reponse.codeErreur = 1;
                return reponse;
            }
        }

        public async Task<ListStringCodeErreur> getLibelleVillePays(string nomPays)
        {
            ListStringCodeErreur reponse = new ListStringCodeErreur();
            reponse.codeErreur = 0;
            try
            {
                var requete = new Uri("http://apiferry.azurewebsites.net/api/villes/rechercheNomVillePays/?nomPays="+nomPays);
                HttpClient client = new HttpClient();
                var json = await client.GetStringAsync(requete);
                reponse.listeTexte = JsonConvert.DeserializeObject<IEnumerable<String>>(json);
                return reponse;
            }
            catch (HttpRequestException e)
            {
                reponse.codeErreur = 1;
                return reponse;
            }

        }

        public async Task<IntCodeErreur> getIdVille(string nomPays, string nomVille)
        {
            IntCodeErreur reponse = new IntCodeErreur();
            reponse.codeErreur = 0;
            try
            {
                var requete = new Uri("http://apiferry.azurewebsites.net/api/villes/rechercheIdVille/?nomPays=" + nomPays + "&nomVille=" + nomVille);
                HttpClient client = new HttpClient();
                var json = await client.GetStringAsync(requete);
                reponse.entier = JsonConvert.DeserializeObject<int>(json);
                return reponse;    
            }
            catch (HttpRequestException e)
            {
                reponse.codeErreur = 1;
                return reponse;
            }

        }

        public async Task<FerryInfoCodeErreur> getResultatRecherche(Recherche recherche)
        {
            FerryInfoCodeErreur reponse = new FerryInfoCodeErreur();
            reponse.codeErreur = 0;
            try
            {
                var requete = "http://apiferry.azurewebsites.net/api/ferries/resultatrechercheferry/?dateDepart=" + recherche.dateDepart.ToString() + "&idVilleDepart=" + recherche.numVilleDepart + "&idVilleArrivee=" + recherche.numVilleArrivee;
                HttpClient client = new HttpClient();
                var json = await client.GetStringAsync(requete);
                reponse.listeFerry = JsonConvert.DeserializeObject<IEnumerable<FerryInfo>>(json);
                return reponse;
            }
            catch (HttpRequestException e)
            {
                reponse.codeErreur = 1;
                return reponse;
            }
   
        }
    }
}
