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

        public async Task<bool> DeleteDoctorAsync(int id)
        {
            bool doctorDeleted = false;
            try
            {
                doctorDeleted = await _unitOfWork!.DoctorRepository.DeleteAsync(id);
                _logger!.LogInformation("{Message}", "Doctor with id:  " + id + " deleted, success");
            }
            catch (Exception e)
            {
                _logger!.LogError("{Message}{Exception}", e.Message, e.StackTrace);
            }
            return doctorDeleted;
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
    }
}
