using System.Collections.Generic;
using System.Threading.Tasks;
using PeoplePetUtility.Model;

namespace PeoplePetUtility.Interface
{
    public interface IApiRequestBuilder
    {        
        Task<List<PeoplePet>> Get(IApiRequest request);
    }
}
