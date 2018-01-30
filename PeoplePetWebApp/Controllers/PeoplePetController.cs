using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PeoplePetWebApp.Models;
using PeoplePetUtility;
using PeoplePetUtility.Interface;
using PeoplePetUtility.Model;

namespace PeoplePetWebApp.Controllers
{
    public class PeoplePetController : Controller
    {
        private readonly IOptions<PeopleApiSettings> _peopleApiSettings;
        private readonly IPeoplePetService _peoplePetService;

        public PeoplePetController(IOptions<PeopleApiSettings> peopleApiSettings, IPeoplePetService peoplePetService)
        {
            _peopleApiSettings = peopleApiSettings;
            _peoplePetService = peoplePetService;
        }

        private IApiRequest CreateApiRequest()
        {
            IApiRequest request = new ApiRequest();
            if (_peopleApiSettings != null && _peopleApiSettings.Value != null)
            {               
                request.RequestURI = _peopleApiSettings.Value.URI ?? null;            
            }
            return request;
        }

        public IActionResult Index()
        {
            try
            {
                List<PetsGroupedOnOwnerGender> result = new List<PetsGroupedOnOwnerGender>();

                if (_peopleApiSettings.Value.URI == null)
                    ViewData["Message"] = "appSettings.json don't have the uri for the API.";
                else
                {
                    Task<List<PetsGroupedOnOwnerGender>> petsTask = _peoplePetService.getPetsGroupedOnOwnerGender(CreateApiRequest());

                    petsTask.Wait();
                    result = petsTask.Result;
                }

                return View(result);
            }
            catch(Exception ex)
            {
                //use ex object to log exception
                return RedirectToAction("Error");

            }
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
