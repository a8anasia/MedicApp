using AutoMapper;
using MedicApp.Data;
using MedicApp.Repositories;

namespace MedicApp.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IUnitOfWork? _unitOfWork;
        private readonly ILogger<UserService>? _logger;
        private readonly IMapper? _mapper;
       

        public DoctorService(IUnitOfWork? unitOfWork, ILogger<UserService>? logger, IMapper? mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        
        }

        public async Task<IEnumerable<Doctor>> GetAllDoctorsAsync()
        {
            List<Doctor> doctors = new();
            try
            {
                doctors = (List<Doctor>)await _unitOfWork!.DoctorRepository.GetAllAsync();
                _logger!.LogInformation("{Message}", "All doctors returned with success");
            }
            catch (Exception e)
            {
                _logger!.LogError("{Message}{Exception}", e.Message, e.StackTrace);
            }
            return doctors;
        }

        public async Task<List<Medicine>> GetAllMedicinesAsync()
        {
            List<Medicine> medicines = new();
            try
            {
                medicines = (List<Medicine>)await _unitOfWork!.DoctorRepository.GetAllAsync();
                _logger!.LogInformation("{Message}", "All doctors returned with success");
            }
            catch (Exception e)
            {
                _logger!.LogError("{Message}{Exception}", e.Message, e.StackTrace);
            }
            return medicines;
        }

        public async Task<Doctor?> GetDoctorAsync(int id)
        {
            Doctor? doctor = null;
            try
            {
                doctor = await _unitOfWork!.DoctorRepository.GetAsync(id);
                _logger!.LogInformation("{Message}", "Doctor with id: " + id + " retrieved with success");
            }
            catch (Exception e)
            {
                _logger!.LogError("{Message}{Exception}", e.Message, e.StackTrace);
            }
            return doctor;
        }

        public async Task<IEnumerable<Patient>> GetAllPatientsAsync()
        {
            List<Patient> patients = new();
            try
            {
                patients = (List<Patient>)await _unitOfWork!.PatientRepository.GetAllAsync();
                _logger!.LogInformation("{Message}", "All patient returned with success");
            }
            catch (Exception e)
            {
                _logger!.LogError("{Message}{Exception}", e.Message, e.StackTrace);
            }
            return patients;
        }

        public async Task<Doctor?> GetDoctorByUserIdAsync(int? id)
        {
            Doctor? doctor = null;
            try
            {
                doctor = await _unitOfWork!.DoctorRepository.GetDoctorByUserIdAsync((int)id);
            }
            catch (Exception e)
            {
                _logger!.LogError("{Message}{Exception}", e.Message, e.StackTrace);
            }
            return doctor;
        }

        public async Task<int?> GetUserIdByUsername(string username)
        {
            int? id = null;
            try
            {
               id = await _unitOfWork!.DoctorRepository.GetUserIdByUsernameAsync(username);
            }
            catch (Exception e)
            {
                _logger!.LogError("{Message}{Exception}", e.Message, e.StackTrace);
            }
            return id;
        }

        public async Task<List<Appointment>> GetAllDoctorAppointments(int id)
        {
            List<Appointment> appointments = new();
            try
            {
               appointments = await _unitOfWork!.DoctorRepository.GetAllDoctorAppointments(id);
            }
            catch (Exception e)
            {
                _logger!.LogError("{Message}{Exception}", e.Message, e.StackTrace);
            }
            return appointments;
        }

        public async Task<List<Patient>> GetAllPatients()
        {
            List<Patient> patients = new();
            try
            {
                patients = await _unitOfWork!.DoctorRepository.GetAllPatient();
            }
            catch (Exception e)
            {
                _logger!.LogError("{Message}{Exception}", e.Message, e.StackTrace);
            }
            return patients;

        }
    }
}
