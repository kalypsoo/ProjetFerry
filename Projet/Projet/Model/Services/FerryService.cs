using Newtonsoft.Json;
using Projet.Model.Models;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Projet.Model.Services
{
    class FerryService
    {
        public async Task<int> SetFerryBD(Ferry newFerry)
        {
            try
            {
                var requete = new Uri("http://apiferry.azurewebsites.net/api/ferries/ajoutFerry");
                HttpClient client = new HttpClient();
                StringContent content = new StringContent(JsonConvert.SerializeObject(newFerry), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(requete, content);

                if (response.IsSuccessStatusCode)
                    return 0;
                else
                    return 1;
            }
            catch (HttpRequestException e)
            {
                return 2;
            }

        }
    }
}
