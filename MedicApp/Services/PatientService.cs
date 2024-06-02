using AutoMapper;
using MedicApp.Data;
using MedicApp.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MedicApp.Services
{
    public class PatientService : IPatientService
    {
        public readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UserService>? _logger;
        public readonly IMapper _mapper;

        public PatientService(IUnitOfWork unitOfWork, ILogger<UserService>? logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<Patient?> GetPatientByIdAsync(int id)
        {
            Patient? patient = null;
            try
            {
                patient = await _unitOfWork!.PatientRepository.GetAsync(id);
                _logger!.LogInformation("{Message}", "User: " + patient + " found and returned");
            }
            catch (Exception e)
            {
                _logger!.LogError("{Message}{Exception}", e.Message, e.StackTrace);
            }
            return patient;
           

        }

        public async Task<Patient?> GetPatientByUserIdAsync(int userId)
        {
            var patient = await _unitOfWork.PatientRepository.GetPatientByUserIdAsync(userId);
            Console.WriteLine( patient.Id);
            return patient;

        }

        public async Task<List<Appointment>> GetAllPatientAppointments(int patiendId)
        {
            List<Appointment> appointments = await _unitOfWork.PatientRepository.GetAllPatientAppointments(patiendId);
          
            return appointments;
        }

        public async Task<int> GetUserIdByUsernameAsync(string username)
        {
            var user = await _unitOfWork.UserRepository.GetByUsernameAsync(username);
            return user.Id;
        }
    }
}
