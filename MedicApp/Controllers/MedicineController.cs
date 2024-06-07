using MedicApp.Data;
using MedicApp.DTO;
using MedicApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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
            if (ModelState.IsValid)
            {
                await _applicationService.MedicineService.AddAsync(medicine);
                await _applicationService.MedicineService._unitOfWork.SaveAsync();
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var medicine = await _applicationService.MedicineService._unitOfWork.MedicineRepository.GetAsync(id);

            if (medicine == null)
            {
                return NotFound();
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
