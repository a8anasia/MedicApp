using MedicApp.Data;

namespace MedicApp.Services
{
    public interface IPatientService
    {
       Task<Patient?> GetPatientByIdAsync(int id);
       Task<Patient?> GetPatientByUserIdAsync(int id);
    }
}
