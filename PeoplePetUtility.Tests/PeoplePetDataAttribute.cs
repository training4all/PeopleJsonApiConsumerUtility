using System.Collections.Generic;
using System.Reflection;
using Xunit.Sdk;
using Moq;
using PeoplePetUtility.Interface;
using PeoplePetUtility.Model;

namespace PeoplePetUtility.Tests
{
    public class PeoplePetDataAttribute : DataAttribute
    {
        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            yield return GetScenarioWithValidApiData_GroupOwnerOnGender();
            yield return GetScenarioWithValidApiData_SortedOnPetsName();

        }
        
        private object[] GetScenarioWithValidApiData_GroupOwnerOnGender()
        {
            IApiRequest mockApiReq = new ApiRequest()
            {
                RequestURI = ""
            };
            List<PeoplePet> mockPeoplePetLst = new List<PeoplePet>()
            {
                new PeoplePet()
                {
                    name = "Bob",
                    gender = "Male",
                    age = 23,
                    pets = new List<Pet>()
                    {
                        new Pet()
                        {
                            name = "Garfield",
                            type = "Cat"
                        },
                         new Pet()
                        {
                            name = "Fido",
                            type = "Dog"
                        }
                    }
                },
                 new PeoplePet()
                {
                    name = "Samantha",
                    gender = "Female",
                    age = 40,
                    pets = new List<Pet>()
                    {
                        new Pet()
                        {
                            name = "Tabby",
                            type = "Cat"
                        }
                    }
                }
            };
            List<PetsGroupedOnOwnerGender> mockExpectedResult = new List<PetsGroupedOnOwnerGender>()
            {
                new PetsGroupedOnOwnerGender()
                {
                    gender = "Male",
                    pets = new List<string>()
                    {
                        "Garfield"
                    }
                },
                 new PetsGroupedOnOwnerGender()
                {
                    gender = "Female",
                    pets = new List<string>()
                    {
                        "Tabby"
                    }
                }
            };

            var mockRepoService = new Mock<IPeoplePetRepository>();
            mockRepoService
                .Setup(ss => ss.getPeople(mockApiReq))
                .ReturnsAsync(mockPeoplePetLst);

            return new object[] { mockApiReq, mockRepoService.Object, mockExpectedResult };
        }

        private object[] GetScenarioWithValidApiData_SortedOnPetsName()
        {
            IApiRequest mockApiReq = new ApiRequest()
            {
                RequestURI = ""
            };
            List<PeoplePet> mockPeoplePetLst = new List<PeoplePet>()
            {
                new PeoplePet()
                {
                    name = "Bob",
                    gender = "Male",
                    age = 23,
                    pets = new List<Pet>()
                    {
                        new Pet()
                        {
                            name = "Garfield",
                            type = "Cat"
                        },
                         new Pet()
                        {
                            name = "Fido",
                            type = "Dog"
                        }
                    }
                },
                 new PeoplePet()
                {
                    name = "Samantha",
                    gender = "Female",
                    age = 40,
                    pets = new List<Pet>()
                    {
                        new Pet()
                        {
                            name = "Tabby",
                            type = "Cat"
                        }
                    }
                },
                  new PeoplePet()
                {
                    name = "Fred",
                    gender = "Male",
                    age = 40,
                    pets = new List<Pet>()
                    {
                        new Pet()
                        {
                            name = "Tom",
                            type = "Cat"
                        },
                         new Pet()
                        {
                            name = "Huggie",
                            type = "Cat"
                        }
                    }
                }
            };

            List<PetsGroupedOnOwnerGender> mockExpectedResult = new List<PetsGroupedOnOwnerGender>()
            {
                new PetsGroupedOnOwnerGender()
                {
                    gender = "Male",
                    pets = new List<string>()
                    {
                        "Garfield",
                        "Huggie",
                        "Tom"

                    }
                },
                 new PetsGroupedOnOwnerGender()
                {
                    gender = "Female",
                    pets = new List<string>()
                    {
                        "Tabby"
                    }
                }
            };

            var mockRepoService = new Mock<IPeoplePetRepository>();
            mockRepoService
                .Setup(ss => ss.getPeople(mockApiReq))
                .ReturnsAsync(mockPeoplePetLst);

            return new object[] { mockApiReq, mockRepoService.Object, mockExpectedResult };
        }

    }
}
