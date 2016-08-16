using Newtonsoft.Json;
using Projet.Model.Models;
using System;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;

namespace Projet.Model.Services
{
    class ConnexionService
    {
        public async Task<StringCodeErreur> Connexion(string identifiant, string password)
        {
            StringCodeErreur reponse = new StringCodeErreur();
            try
            {
                string passcrypte = ConnexionService.Cryptage(password);
                var requete = new Uri("http://apiferry.azurewebsites.net/api/gerants/validationLoginGerant/?login=" + identifiant + "&mdp=" + passcrypte);
                HttpClient client = new HttpClient();

                var json = await client.GetStringAsync(requete);
                reponse.texte = JsonConvert.DeserializeObject<string>(json);
                reponse.codeErreur = 0;
                return reponse;
            }
            catch (HttpRequestException e)
            {
                reponse.codeErreur = 1;
                return reponse;
            }

        }

        private static string Cryptage(string texte)
        {
            var bufferString = CryptographicBuffer.ConvertStringToBinary(texte, BinaryStringEncoding.Utf8);

            var hashAlgorithmProvider = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Sha512);

            var bufferHash = hashAlgorithmProvider.HashData(bufferString);

            return CryptographicBuffer.EncodeToHexString(bufferHash);
        }
    }
}
