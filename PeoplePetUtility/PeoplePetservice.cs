using System;
using System.Collections.Generic;
using PeoplePetUtility.Interface;
using PeoplePetUtility.Model;
using System.Threading.Tasks;
using System.Linq;

namespace PeoplePetUtility
{
    public class PeoplePetservice : IPeoplePetService
    {
        private readonly IPeoplePetRepository _peoplePetRepository;

        public PeoplePetservice(IPeoplePetRepository peoplePetRepository)
        {
            _peoplePetRepository = peoplePetRepository;
        }               

        public async Task<List<PetsGroupedOnOwnerGender>> getPetsGroupedOnOwnerGender(IApiRequest apiRequest)
        {
            List<PetsGroupedOnOwnerGender> petsGrouped = new List<PetsGroupedOnOwnerGender>();

            try
            {   
                List<PeoplePet> peopleLst = await _peoplePetRepository.getPeople(apiRequest);

                if (peopleLst == null || peopleLst.Count() == 0)
                {
                    throw new Exception("No data found from API.");
                }

                var peopleGenderGrouped = from people in peopleLst
                                          group new { gender = people.gender, pets = people.pets } by people.gender into peopleGender
                                          select peopleGender.ToList();


                foreach (var peopleCounter in peopleGenderGrouped)
                {
                    PetsGroupedOnOwnerGender ownerPets = new PetsGroupedOnOwnerGender();
                    ownerPets.gender = peopleCounter[0].gender;
                    ownerPets.pets = new List<string>();

                    foreach (var peoplePet in peopleCounter)
                    {
                        if (peoplePet.pets != null)
                        {
                            List<string> petsName = new List<string>();
                            foreach (var pets in peoplePet.pets)
                            {
                                if (pets.type.ToLower() == "cat")
                                    petsName.Add(pets.name);

                            }
                            ownerPets.pets.AddRange(petsName);
                        }
                    }

                    ownerPets.pets.Sort();
                    petsGrouped.Add(ownerPets);
                }
            }
            catch
            {
                throw new Exception("Error in grouping or sorting API data.");
            }
            return petsGrouped;

        }

        
    }
}
