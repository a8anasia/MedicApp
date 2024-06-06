using MedicApp.Data;
using MedicApp.Models;
using MedicApp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MedicApp.Controllers
{
    public class FutureAppointmentsDocController : Controller
    {
        public List<Error> ErrorArray { get; set; } = new();

        private readonly IApplicationService _applicationService;

        public FutureAppointmentsDocController(IApplicationService applicationService) : base()
        {
            _applicationService = applicationService;

        }

        public async Task<IActionResult> Index()
        {
            var userUsername = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var userId = await _applicationService.DoctorService.GetUserIdByUsername(userUsername);

            var doctor = await _applicationService.DoctorService.GetDoctorByUserIdAsync(userId);

            List<Appointment?> appointments = await _applicationService.DoctorService.GetAllDoctorAppointments(doctor.Id);

            List<Patient> patient = await _applicationService.DoctorService.GetAllPatients();

            List<Medicine> medicines = await _applicationService.MedicineService.GetAllMedicinesAsync();

            List<Diagnosis> diagnoses = await _applicationService.DiagnosisService.GetAllDiagnosisAsync();

            if (appointments != null)
            {
                List<AppointmentsViewModelDoc> futureApp = appointments.Select(x => new AppointmentsViewModelDoc
                {
                    appointmentId = x.Id,
                    date = x.Date,
                    patientId = x.PatientId,
                    patientLastname = x.Patient.Lastname,
                    diagnosisId = x.DiagnosisId,
                    medicineId = x.MedicineId,
                    medicineName = x.Medicine?.Name,
                    diagnosisName = x.Diagnosis?.Name
                }).ToList();

                return View(futureApp);
            }
            else
            {
                return View();
            }
        }
    }
}