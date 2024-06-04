using MedicApp.Data;

namespace MedicApp.Services
{
    public interface IDiagnosisService
    {
        Task<List<Diagnosis>> GetAllDiagnosisAsync();
    }
}
