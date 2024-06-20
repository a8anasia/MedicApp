using MedicApp.Data;

namespace MedicApp.Repositories
{
    public interface IDoctorRepository
    {
        Task<List<Appointment>> GetAllDoctorAppointments(int id);
        Task<Doctor?> GetByphoneNumber(string? Phone);
        Task<Doctor?> GetDoctorByUserIdAsync(int userId);
        Task<int?> GetUserIdByUsernameAsync(string username);

        Task<List<Patient>> GetAllPatient();
    }
}
