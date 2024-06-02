using MedicApp.Data;

namespace MedicApp.Repositories
{
    public interface IPatientRepository
    {
        Task<List<Appointment>> GetAllPatientAppointments(int id);
        Task<Patient?> GetByphoneNumber(string? Phone);
        Task<List<User>> GetAllUsersPatientAsync();
        Task<Patient?> GetPatientByLastname(string? Lastname);

    }
}
