using MedicApp.Data;
using MedicApp.Models;
using MedicApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MedicApp.Controllers
{
    public class TodaysAppointmentsController : Controller
    {
        public List<Error> ErrorArray { get; set; } = new();

        private readonly IApplicationService _applicationService;


        public TodaysAppointmentsController(IApplicationService applicationService) : base()
        {
            _applicationService = applicationService;

        }

        [HttpGet]
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
                List<AppointmentsViewModelDoc> todaysApp = appointments.Select(x => new AppointmentsViewModelDoc
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

                return View(todaysApp);
            }
            else
            {
                return View();
            }
        }



        [HttpGet]
        public async Task<IActionResult> Edit(int appointmentId)
        {
            Appointment? appointment;

             appointment = await _applicationService.AppointmentService._unitOfWork
                .AppointmentRepository.GetAsync(appointmentId);

            var patient = await _applicationService.PatientService._unitOfWork
       .PatientRepository.GetAsync(appointment.PatientId);

            var viewModel = new AppointmentsViewModelDoc
            {
                appointmentId = appointment.Id,
                date = appointment.Date,
                patientId = appointment.PatientId,
                patientLastname = patient?.Lastname,
                diagnosisId = appointment.DiagnosisId,
                medicineId = appointment.MedicineId,
            };

            ViewBag.Medicines = new SelectList(await _applicationService.MedicineService.GetAllMedicinesAsync(), "Id", "Name");
            ViewBag.Diagnoses = new SelectList(await _applicationService.DiagnosisService.GetAllDiagnosisAsync(), "Id", "Name");

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AppointmentsViewModelDoc viewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Medicines = new SelectList(await _applicationService.MedicineService.GetAllMedicinesAsync(), "Id", "Name");
                ViewBag.Diagnoses = new SelectList(await _applicationService.DiagnosisService.GetAllDiagnosisAsync(), "Id", "Name");
                return View(viewModel);
            }

            var appointment = await _applicationService.AppointmentService._unitOfWork
                .AppointmentRepository.GetAsync(viewModel.appointmentId.Value);

            if (appointment == null)
            {
                return NotFound();
            }

            appointment.DiagnosisId = viewModel.diagnosisId;
            appointment.MedicineId = viewModel.medicineId;

             _applicationService.AppointmentService._unitOfWork.AppointmentRepository.UpdateAsync(appointment);
            await _applicationService.AppointmentService._unitOfWork.SaveAsync();

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
