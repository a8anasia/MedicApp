using MedicApp.Data;
using MedicApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MedicApp.Controllers
{
    public class PatientController : Controller
    {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpGet]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> Index(int id)
        {

            var patient = await _patientService.GetPatientByUserIdAsync(id);

            Console.WriteLine("PatientController Index method called with id: " + patient);

            if (patient == null)
            {
                return NotFound();
            }

            return View("Index", patient);
        }
    }
}

