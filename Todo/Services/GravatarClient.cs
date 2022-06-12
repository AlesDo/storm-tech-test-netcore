using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Todo.Services
{
   public class GravatarClient
   {
         private readonly HttpClient _httpClient;

         public GravatarClient(HttpClient httpClient)
         {
            _httpClient = httpClient;
         }

         public async Task<string> GetName(string email)
         {
            try
            {
               string emailHash = Gravatar.GetHash(email);
               string jsonResponse = await _httpClient.GetStringAsync($"{emailHash}.json");
               JObject jObject = JObject.Parse(jsonResponse);
               JArray gravatarEntries = jObject.Value<JArray>("entry");
               return gravatarEntries[0].Value<string>("displayName");
            }
            catch
            {
               return "Gravatar name not available.";
            }

         }
   }
}
