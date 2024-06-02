using MedicApp.Data;

namespace MedicApp.Repositories
{
    public class MedicineRepository : BaseRepository<Medicine>
    {
        public MedicineRepository(MedicAppDbContext? context) : base(context)
        {
        }
    }
}
