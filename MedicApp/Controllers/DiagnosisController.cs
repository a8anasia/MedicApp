using MedicApp.Data;
using MedicApp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MedicApp.Controllers
{
    public class DiagnosisController : Controller
    {
        public List<Error> ErrorArray { get; set; } = new();

        private readonly IApplicationService _applicationService;


        public DiagnosisController(IApplicationService applicationService) : base()
        {
            _applicationService = applicationService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Diagnosis> diagnoses = new();
            diagnoses = await _applicationService.DiagnosisService.GetAllDiagnosisAsync();
            return View(diagnoses);
        }

        [HttpGet]
        public IActionResult Insert()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Insert(Diagnosis diagnosis)
        {
            List<Diagnosis> diagnoses = await _applicationService.DiagnosisService.GetAllDiagnosisAsync();

            if (diagnosis.Name == null)
            {
                TempData["ErrorMessage"] = "Diagnosis Name is not optional";
                return RedirectToAction("Insert");
            }

            foreach (var dia in diagnoses)
            {
                if (dia.Name == diagnosis.Name)
                {
                    TempData["ErrorMessage"] = "The diagnosis already exists";
                    return RedirectToAction("Insert");
                }
            }

            await _applicationService.DiagnosisService._unitOfWork.DiagnosisRepository.AddAsync(diagnosis);
            await _applicationService.DiagnosisService._unitOfWork.SaveAsync();
            return RedirectToAction("Index");
          
        }



        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var diagnosis = await _applicationService.DiagnosisService._unitOfWork.DiagnosisRepository.GetAsync(id);

            if (diagnosis == null)
            {
                return NotFound();
            }


            List<Appointment> appointments = (List<Appointment>)await _applicationService.AppointmentService._unitOfWork.AppointmentRepository.GetAllAsync();

            foreach (var appointment in appointments)
            {
                if (appointment.DiagnosisId == diagnosis.Id)
                {
                    TempData["ErrorMessage"] = "You can't delete this diagnosis, it is in use";
                    return RedirectToAction("Index");
                }
            }

            await _applicationService.DiagnosisService._unitOfWork.DiagnosisRepository.DeleteAsync(id);
            await _applicationService.DiagnosisService._unitOfWork.SaveAsync();

            return RedirectToAction("Index");

        }

        public async Task<IActionResult> BackToDoctor()
        {
           
            var userUsername = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var userId = await _applicationService.DoctorService.GetUserIdByUsername(userUsername);

            return RedirectToAction("Index", "Doctor", new { id = userId });
        }
    }
}
