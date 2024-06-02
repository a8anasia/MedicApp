using MedicApp.Data;

namespace MedicApp.Services
{
    public interface IDoctorService
    {
        Task<IEnumerable<Doctor>> GetAllDoctorsAsync();
        Task<List<Medicine>> GetAllMedicinesAsync();
        Task<Doctor?> GetDoctorAsync(int id);
        Task<bool> DeleteDoctorAsync(int id);

        Task<IEnumerable<Patient>> GetAllPatientsAsync();
    }
}
