using System;
using Xunit;
using PeoplePetUtility.Interface;
using System.Threading.Tasks;
using Moq;
using System.Collections.Generic;
using PeoplePetUtility.Model;
using Newtonsoft.Json;

namespace PeoplePetUtility.Tests
{
    public class PeoplePetServiceShould
    {
        [Trait("Category", "PeoplePetService")]
        [Theory]        
        [PeoplePetDataAttribute]
        public async Task GetPetsGroupedOnOwnerGender(IApiRequest apiRequest, IPeoplePetRepository peoplePetRepository, List<PetsGroupedOnOwnerGender> expectedResult)
        {
            //Arrange
            IPeoplePetService sut = new PeoplePetservice(peoplePetRepository);
            
            //Act
            List<PetsGroupedOnOwnerGender> result = await sut.getPetsGroupedOnOwnerGender(apiRequest);
            string resultJson = JsonConvert.SerializeObject(result);
            string expectedResultJson = JsonConvert.SerializeObject(expectedResult);

            //Assert
            Assert.Equal(expectedResultJson, resultJson);
        }

        [Trait("Category", "PeoplePetService")]
        [Fact]
        public async Task GetPetsGroupedOnOwnerGender_EmptyApiData()
        {
            //Arrange
            IApiRequest mockApiReq = new ApiRequest()
            {
                RequestURI = ""
            };
            List<PeoplePet> mockPeoplePetLst = new List<PeoplePet>() { };
            List<PetsGroupedOnOwnerGender> mockExpectedResult = new List<PetsGroupedOnOwnerGender>() { };

            var mockRepoService = new Mock<IPeoplePetRepository>();
            mockRepoService
                .Setup(ss => ss.getPeople(mockApiReq))
                .ReturnsAsync(mockPeoplePetLst);
            IPeoplePetService sut = new PeoplePetservice(mockRepoService.Object);

            //Assert
            await Assert.ThrowsAsync<Exception>(async () => await sut.getPetsGroupedOnOwnerGender(mockApiReq));
        }

    }
}
