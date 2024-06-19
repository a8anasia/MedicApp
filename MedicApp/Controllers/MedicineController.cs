using MedicApp.Data;
using MedicApp.DTO;
using MedicApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Runtime.Intrinsics.X86;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MedicApp.Controllers
{
    public class MedicineController : Controller
    {
        public List<Error> ErrorArray { get; set; } = new();

        private readonly IApplicationService _applicationService;


        public MedicineController(IApplicationService applicationService) : base()
        {
            _applicationService = applicationService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Medicine> medicines = new();
            medicines = await _applicationService.MedicineService.GetAllMedicinesAsync();
            return View(medicines);
        }

        [HttpGet]
        public IActionResult Insert()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Insert(Medicine medicine)
        {
            List<Medicine> medicines = await _applicationService.MedicineService.GetAllMedicinesAsync();

            if (medicine.Name == null)
            {
                TempData["ErrorMessage"] = "Medicine Name is not optional";
                return RedirectToAction("Insert");
            } 

            foreach (var med in medicines)
            {
                if (med.Name == medicine.Name)
                {
                    TempData["ErrorMessage"] = "The medicine already exists";
                    return RedirectToAction("Insert");
                }
            }

            await _applicationService.MedicineService.AddAsync(medicine);
            await _applicationService.MedicineService._unitOfWork.SaveAsync();
            return RedirectToAction("Index");
   
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var medicine = await _applicationService.MedicineService._unitOfWork.MedicineRepository.GetAsync(id);

            List<Appointment> appointments  = (List<Appointment>)await _applicationService.AppointmentService._unitOfWork.AppointmentRepository.GetAllAsync();

            foreach (var appointment in appointments)
            {
                if (appointment.MedicineId == medicine.Id)
                {
                    TempData["ErrorMessage"] = "You can't delete this medicine, it is in use";
                    return RedirectToAction("Index");
                }
            } 

            await _applicationService.MedicineService._unitOfWork.MedicineRepository.DeleteAsync(id);
            await _applicationService.MedicineService._unitOfWork.SaveAsync();

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
