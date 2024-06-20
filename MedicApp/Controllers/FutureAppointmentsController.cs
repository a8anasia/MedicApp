using MedicApp.Data;
using MedicApp.Models;
using MedicApp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MedicApp.Controllers
{
    public class FutureAppointmentsController : Controller
    {
        private readonly IApplicationService _applicationService;


        public FutureAppointmentsController(IApplicationService applicationService) : base()
        {
            _applicationService = applicationService;
        }

        public async Task<IActionResult> Index()
        {
            //finds the username of connected patient 
            var userUsername = User.FindFirstValue(ClaimTypes.NameIdentifier);

            //finds the userId of connected patient using his username
            var userId = await _applicationService.PatientService.GetUserIdByUsernameAsync(userUsername);

            //finds the patient using userID
            var patient = await _applicationService.PatientService.GetPatientByUserIdAsync(userId);

            //finds all patient's appointments
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
                    Specialty = x.Doctor.Specialty,
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

            var doctor = await _applicationService.DoctorService.GetDoctorAsync(appointment.DoctorId);
         
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);
            DateOnly tomorrow = today.AddDays(+1);

            //does not allows patient cancel an appointment only 2 days ago
            if (tomorrow == appointment.Date || today == appointment.Date)
            {
                TempData["Message"] = $"You can no longer cancel your appointment. Please contact your doctor at {doctor.Phone}";

                return RedirectToAction("Index");
            }

            await _applicationService.AppointmentService._unitOfWork.AppointmentRepository.DeleteAsync(id);
            await _applicationService.AppointmentService._unitOfWork.SaveAsync();

            return RedirectToAction("Index");

        }

        public async Task<IActionResult> BackToPatient()
        {
            //finds the connected patient to return to his home page
            var userUsername = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var userId = await _applicationService.PatientService.GetUserIdByUsernameAsync(userUsername);

            return RedirectToAction("Index", "Patient", new { id = userId });
        }
    }
}
