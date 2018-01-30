using System.Collections.Generic;
using PeoplePetUtility.Model;
using System.Threading.Tasks;

namespace PeoplePetUtility.Interface
{
    public interface IPeoplePetService
    {   
         Task<List<PetsGroupedOnOwnerGender>> getPetsGroupedOnOwnerGender(IApiRequest apiRequest);            
    }
}
