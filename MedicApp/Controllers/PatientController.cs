using MedicApp.Data;
using MedicApp.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.EntityFrameworkCore;
using MedicApp.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MedicApp.Controllers
{
    public class PatientController : Controller
    {
        public List<Error> ErrorArray { get; set; } = new();

        private readonly IApplicationService _applicationService;


        public PatientController(IApplicationService applicationService) : base()
        {
            _applicationService = applicationService;

        }

        [HttpGet]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> Index(int id)
        {

            var patient = await _applicationService.PatientService.GetPatientByUserIdAsync(id);

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

            var userId = await _applicationService.PatientService.GetUserIdByUsernameAsync(userUsername);

            var patient = await _applicationService.PatientService.GetPatientByUserIdAsync(userId);

            List<Appointment> appointments = await _applicationService.PatientService.GetAllPatientAppointments(patient.Id);

            return View("History", appointments);
        }


        [HttpGet]
        public async Task<IActionResult> Appointment()
        {
            var doctors = await _applicationService.DoctorService.GetAllDoctorsAsync();
            var viewModel = new AppointmentViewModel
            {
                Doctors = (List<Doctor>)doctors,
                SelectedDate = DateOnly.FromDateTime(DateTime.Today)
            };
            return View(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> CheckAppointment(AppointmentViewModel viewModel)
        {
            var doctors = await _applicationService.DoctorService.GetAllDoctorsAsync();

            viewModel.Doctors = (List<Doctor>)doctors;

            bool appointmentExists = await _applicationService.AppointmentService
                        .AppointmentExistsAsync(viewModel.SelectedDate, viewModel.SelectedDoctorId);

            if (appointmentExists)
            {
                ViewData["AvailabilityMessage"] = $"The doctor is not available on the {viewModel.SelectedDate}";
                ViewData["ShowAddButton"] = false;
                return View("Appointment", viewModel);
            }
            else
            {
                ViewData["AvailabilityMessage"] = "The selected doctor is available on the selected date, add your appointment.";
                var userUsername = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var userId = await _applicationService.PatientService.GetUserIdByUsernameAsync(userUsername);
                var patient = await _applicationService.PatientService.GetPatientByUserIdAsync(userId);

                var appointment = new Appointment
                {
                    PatientId = patient.Id,
                    DoctorId = viewModel.SelectedDoctorId,
                    Date = viewModel.SelectedDate
                };
                await _applicationService.AppointmentService.
                _unitOfWork.AppointmentRepository.AddAsync(appointment);

                await _applicationService.PatientService._unitOfWork.SaveAsync();

                await Task.Delay(3000);

                return RedirectToAction("Index", "Patient", new { id = userId });

            }
        } 
    }
}

