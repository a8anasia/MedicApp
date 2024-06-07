using MedicApp.Data;
using MedicApp.Models;
using MedicApp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MedicApp.Controllers
{
    public class FutureAppointmentsController : Controller
    {
        public List<Error> ErrorArray { get; set; } = new();

        private readonly IApplicationService _applicationService;


        public FutureAppointmentsController(IApplicationService applicationService) : base()
        {
            _applicationService = applicationService;

        }

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
                List<AppointmentsViewModel> futureApp = appointments.Select(x => new AppointmentsViewModel
                {
                    appointmentId = x.Id,
                    date = x.Date,
                    doctorId = x.DoctorId,
                    diagnosisId = x.DiagnosisId,
                    medicineId = x.MedicineId,
                    doctorLastname = x.Doctor.Lastname,
                    Speciality = x.Doctor.Speciality,
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

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var appointment = await _applicationService.AppointmentService._unitOfWork.AppointmentRepository.GetAsync(id);

            if (appointment == null)
            {
                return NotFound();
            }
            await _applicationService.AppointmentService._unitOfWork.AppointmentRepository.DeleteAsync(id);
            await _applicationService.AppointmentService._unitOfWork.SaveAsync();

            return RedirectToAction("Index");

        }

        public async Task<IActionResult> BackToPatient()
        {
            var userUsername = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var userId = await _applicationService.PatientService.GetUserIdByUsernameAsync(userUsername);

            return RedirectToAction("Index", "Patient", new { id = userId });
        }
    }
}
