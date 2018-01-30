Solution Description - The repository consist of 3 solutions which are created in Visual Studio 2017 IDE.

    PeoplePetUtility- This is the class library which includes all the business logic to request web api to get the json data and then group and sort json data on owner gender and pets(cat) names. In this project I have used the dependenct inject pattern to dynamically inject the repository into class so that we can change the repository at any time. Today the repository is fetching data from rest api but it can easily we pointed to other data sources. HttpClient is used to call the web api asynchronously and the operation is waited for completion using await. Exception handling is done by throwing exceptions from the project to the consumer project so that the consumer can have project specific handling of the exceptions. Model class peopleis created which represents the json data returned from web api.

    PeoplePetUtility.Tests- This is the xunit test project created to test the PeoplePetUtility class library. Both Fact and Theory test methods are created to test different scenerios. I have created a custome PeoplePetUtilityData Attribute which will feed the test method with differnet sets of data for different test cases.

    PeoplePetUtilityWebApp - This is the Asp.net Core MVC Web application which consumes the PeoplePetUtility class library to display fetched json data in browser.

How to Execute-

   1. Git clone the repo.
   2. Set PeoplePetUtilityWebApp as Startup Project and start IIS Express from Visual Studio IDE.
   3. In Browser, you will see output of json data grouped on owner gender along with pets[Cat] name arranged in alphabatical order.
   4. To execute the test cases, navigate to view -> Test Explorer and then select Run All.

