using System.Collections.Generic;
using Newtonsoft.Json;

namespace PeoplePetUtility.Model
{
    public class PeoplePet
    {
        [JsonProperty(PropertyName = "name")]
        public string name;

        [JsonProperty(PropertyName = "gender")]
        public string gender;

        [JsonProperty(PropertyName = "age")]
        public int age;

        [JsonProperty(PropertyName = "pets")]
        public List<Pet> pets;
    }

    public class Pet
    {
        [JsonProperty(PropertyName = "name")]
        public string name;

        [JsonProperty(PropertyName = "type")]
        public string type;
    }
        
    public class PetsGroupedOnOwnerGender
    {
        public string gender;
        public List<string> pets;
    }
}
