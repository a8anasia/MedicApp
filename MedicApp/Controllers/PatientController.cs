using MedicApp.Data;
using MedicApp.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.EntityFrameworkCore;
using MedicApp.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MedicApp.Controllers
{
    public class PatientController : Controller
    {
        private readonly IApplicationService _applicationService;


        public PatientController(IApplicationService applicationService) : base()
        {
            _applicationService = applicationService;

        }

        [HttpGet]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> Index(int id)
        {

            var patient = await _applicationService.PatientService.GetPatientByUserIdAsync(id);

            Console.WriteLine("PatientController Index method called with id: " + patient);

            if (patient == null)
            {
                return NotFound();
            }

            return View("Index", patient);
        }  
    }
}

