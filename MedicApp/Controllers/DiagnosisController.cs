using MedicApp.Data;
using MedicApp.Services;
using Microsoft.AspNetCore.Mvc;
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
            if (ModelState.IsValid)
            {
                await _applicationService.DiagnosisService._unitOfWork.DiagnosisRepository.AddAsync(diagnosis);
                await _applicationService.DiagnosisService._unitOfWork.SaveAsync();
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var diagnosis = await _applicationService.DiagnosisService._unitOfWork.DiagnosisRepository.GetAsync(id);

            if (diagnosis == null)
            {
                return NotFound();
            }

            await _applicationService.DiagnosisService._unitOfWork.DiagnosisRepository.DeleteAsync(id);
            await _applicationService.DiagnosisService._unitOfWork.SaveAsync();

            return RedirectToAction("Index");

        }
    }
}
