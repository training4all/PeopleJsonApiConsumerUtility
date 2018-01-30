using System.Collections.Generic;
using PeoplePetUtility.Interface;
using PeoplePetUtility.Model;
using System.Threading.Tasks;

namespace PeoplePetUtility
{
    public class PeoplePetRepository : IPeoplePetRepository
    {
        private readonly IApiRequestBuilder _apiRequestBuilder;

        public PeoplePetRepository(IApiRequestBuilder apiRequestBuilder)
        {
            _apiRequestBuilder = apiRequestBuilder;
        }

        public async Task<List<PeoplePet>> getPeople(IApiRequest apiRequest)
        {
           return await _apiRequestBuilder.Get(apiRequest);           
        }
    
    }
}
