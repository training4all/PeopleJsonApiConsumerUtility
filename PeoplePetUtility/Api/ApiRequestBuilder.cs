using System.Collections.Generic;
using System.Net.Http;
using PeoplePetUtility.Interface;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PeoplePetUtility.Model;

namespace PeoplePetUtility.Api
{
    public class ApiRequestBuilder : IApiRequestBuilder
    {
        public async Task<List<PeoplePet>> Get(IApiRequest request)
        {
            try
            {
                using (var client = new HttpClient())
                {                   
                    HttpResponseMessage response = await client.GetAsync(request.RequestURI);
                    response.EnsureSuccessStatusCode();

                    using (HttpContent content = response.Content)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        List<PeoplePet> people = JsonConvert.DeserializeObject<List<PeoplePet>>(responseBody);

                        return await Task.FromResult<List<PeoplePet>>(people);
                    }
                }
            }
            catch
            {
                List<PeoplePet> noData = new List<PeoplePet>();
                return await Task.FromResult<List<PeoplePet>>(noData);
            }
           
        }
    }
}
