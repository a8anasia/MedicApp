using MedicApp.Data;

namespace MedicApp.Repositories
{
    public class DiagnosisRepository : BaseRepository<Diagnosis>
    {
        public DiagnosisRepository(MedicAppDbContext? context) : base(context)
        {
        }
    }
}
