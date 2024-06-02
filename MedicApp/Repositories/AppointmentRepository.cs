using MedicApp.Data;

namespace MedicApp.Repositories
{
    public class AppointmentRepository : BaseRepository<Appointment>
    {
        public AppointmentRepository(MedicAppDbContext? context) : base(context)
        {
        }
    }
}
