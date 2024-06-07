using MedicApp.Data;
using MedicApp.Models;
using MedicApp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;
using System.Security.Claims;

namespace MedicApp.Controllers
{
    public class NewAppointmentController : Controller
    {
        public List<Error> ErrorArray { get; set; } = new();

        private readonly IApplicationService _applicationService;


        public NewAppointmentController(IApplicationService applicationService) : base()
        {
            _applicationService = applicationService;

        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var doctors = await _applicationService.DoctorService.GetAllDoctorsAsync();
            var viewModel = new NewAppointmentViewModel
            {
                Doctors = (List<Doctor>)doctors,
                SelectedDate = DateOnly.FromDateTime(DateTime.Today)
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CheckAppointment(NewAppointmentViewModel viewModel)
        {
            var doctors = await _applicationService.DoctorService.GetAllDoctorsAsync();

            viewModel.Doctors = (List<Doctor>)doctors;

            bool appointmentExists = await _applicationService.AppointmentService
                        .AppointmentExistsAsync(viewModel.SelectedDate, viewModel.SelectedDoctorId);

            if (appointmentExists)
            {
                ViewData["AvailabilityMessage"] = $"The doctor is not available on the {viewModel.SelectedDate}";
                ViewData["ShowAddButton"] = false;
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

            return View("Index", viewModel);
        }

        public async Task<IActionResult> BackToPatient()
        {
            var userUsername = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var userId = await _applicationService.PatientService.GetUserIdByUsernameAsync(userUsername);

            return RedirectToAction("Index", "Patient", new { id = userId });
        }
    }
}
