using MedicApp.Data;

namespace MedicApp.Repositories
{
    public class AppointmentRepository : BaseRepository<Appointment>
    {
        public AppointmentRepository(MedicAppDbContext? context) : base(context)
        {
            
        }

        public async Task<IEnumerable<Appointment>> GetAsync(Func<Appointment, bool> predicate)
        {
            return await Task.Run(() => _context.Appointments.Where(predicate).ToList());
        }

        
    }
}
