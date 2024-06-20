using MedicApp.Data;

namespace MedicApp.Services
{
    public interface IDoctorService
    {
        Task<IEnumerable<Doctor>> GetAllDoctorsAsync();
        Task<List<Medicine>> GetAllMedicinesAsync();
        Task<Doctor?> GetDoctorAsync(int id);

        Task<IEnumerable<Patient>> GetAllPatientsAsync();

        Task<Doctor?> GetDoctorByUserIdAsync(int? id);

        Task<int?> GetUserIdByUsername(string username);
        Task<List<Appointment>> GetAllDoctorAppointments(int id);

        Task<List<Patient>> GetAllPatients();
    }
}
