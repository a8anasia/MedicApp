using MedicApp.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MedicApp.Repositories
{
    public class AppointmentRepository : BaseRepository<Appointment>
    {
        public AppointmentRepository(MedicAppDbContext? context) : base(context)
        {
            
        }

        public async Task<bool> AnyAsync(Expression<Func<Appointment, bool>> predicate)
        {
            return await _context.Appointments.AnyAsync(predicate);
        }

    }
}
