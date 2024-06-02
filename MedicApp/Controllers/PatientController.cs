using MedicApp.Data;
using MedicApp.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Principal;

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

        [HttpGet]
        public async Task<IActionResult> History()
        {
            var userUsername = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var userId = await _patientService.GetUserIdByUsernameAsync(userUsername);

            var patient = await _patientService.GetPatientByUserIdAsync(userId);

            List<Appointment> appointments = await _patientService.GetAllPatientAppointments(patient.Id);

            return View("History" ,appointments);
        }
    }
}

