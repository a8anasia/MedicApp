using MedicApp.Data;
using Microsoft.EntityFrameworkCore;

namespace MedicApp.Repositories
{
    public class DiagnosisRepository : BaseRepository<Diagnosis>
    {
        public DiagnosisRepository(MedicAppDbContext? context) : base(context)
        {
        }

        public async Task<List<Diagnosis>> GetAllDiagnosisAsync()
        {
            List<Diagnosis> diagnoses;
            diagnoses = await _context.Diagnoses.ToListAsync();
            return diagnoses;
        }
    }
}
