using MedicApp.Data;

namespace MedicApp.Repositories
{
    public interface IPatientRepository
    {
        Task<List<Appointment>> GetAllPatientAppointments(int patiendId);
        Task<Patient?> GetByphoneNumber(string? Phone);
        Task<List<User>> GetAllUsersPatientAsync();

        Task<int> GetUserIdByUsernameAsync(string username);

    }
}
