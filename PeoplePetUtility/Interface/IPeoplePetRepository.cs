using System.Collections.Generic;
using PeoplePetUtility.Model;
using System.Threading.Tasks;

namespace PeoplePetUtility.Interface
{
    public interface IPeoplePetRepository
    {        
        Task<List<PeoplePet>> getPeople(IApiRequest apiRequest);
    }
}
