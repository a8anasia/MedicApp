using MedicApp.Data;
using MedicApp.Models;
using MedicApp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MedicApp.Controllers
{
    public class HistoryController : Controller
    {
        public List<Error> ErrorArray { get; set; } = new();

        private readonly IApplicationService _applicationService;


        public HistoryController(IApplicationService applicationService) : base()
        {
            _applicationService = applicationService;

        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userUsername = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var userId = await _applicationService.PatientService.GetUserIdByUsernameAsync(userUsername);

            var patient = await _applicationService.PatientService.GetPatientByUserIdAsync(userId);

            List<Appointment?> appointments = await _applicationService.PatientService.GetAllPatientAppointments(patient.Id);

            List<Doctor> doctors = (List<Doctor>)await _applicationService.DoctorService.GetAllDoctorsAsync();

            List<Medicine> medicines = await _applicationService.MedicineService.GetAllMedicinesAsync();

            List<Diagnosis> diagnoses = await _applicationService.DiagnosisService.GetAllDiagnosisAsync();

            if (appointments != null)
            {
                List<AppointmentsViewModel> history = appointments.Select(x => new AppointmentsViewModel
                {
                    appointmentId = x.Id,
                    date = x.Date,
                    doctorId = x.DoctorId,
                    diagnosisId = x.DiagnosisId,
                    medicineId = x.MedicineId,
                    doctorLastname = x.Doctor.Lastname,
                    Specialty = x.Doctor.Specialty,
                    medicineName = x.Medicine?.Name,
                    diagnosisName = x.Diagnosis?.Name
                }).ToList();

                return View(history);
            }
            else
            {
                List<AppointmentsViewModel> history = new List<AppointmentsViewModel>();
                return View(history);
            }
           
        }

        public async Task<IActionResult> BackToPatient()
        {
            var userUsername = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var userId = await _applicationService.PatientService.GetUserIdByUsernameAsync(userUsername);

            return RedirectToAction("Index", "Patient", new { id = userId });
        }
    }
}
