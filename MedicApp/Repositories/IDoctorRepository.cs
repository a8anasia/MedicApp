using MedicApp.Data;

namespace MedicApp.Repositories
{
    public interface IDoctorRepository
    {
        Task<List<Appointment>> GetAllDoctorAppointments(int id);
        Task<Doctor?> GetByphoneNumber(string? Phone);
        Task<Doctor?> GetDoctorsByLastname(string? Lastname);
        Task<List<Medicine>> GetAllMedicinesAsync();
        Task<List<Diagnosis>> GetAllDiagnosisAsync();
    }
}
