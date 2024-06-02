using MedicApp.Data;
using MedicApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
namespace MedicApp.Repositories
{
    public class PatientRepository : BaseRepository<Patient>, IPatientRepository
    {

        public PatientRepository(MedicAppDbContext? context) : base(context)
        {
        }

        public async Task<List<Appointment>> GetAllPatientAppointments(int id)
        {
            List<Appointment> appointments;
            appointments = await _context.Patients
                       .Where(a => a.Id == id)
                       .SelectMany(a => a.Appointments)
                       .ToListAsync();
            return appointments;
        }

        public async Task<List<User>> GetAllUsersPatientAsync()
        {
            var usersWithPatientRole = await _context.Users
                   .Where(u => u.UserRole == UserRole.Patient)
                   .Include(u => u.Patients)
                   .ToListAsync();

            return usersWithPatientRole;
        }

        public async Task<Patient?> GetByphoneNumber(string? Phone)
        {
            return await _context.Patients.Where(s => s.Phone == Phone)
                .FirstOrDefaultAsync();
        }

        public async Task<Patient?> GetPatientByLastname(string? Lastname)
        {
            return await _context.Patients.Where(s => s.Lastname == Lastname)
                .FirstOrDefaultAsync()!;
        }
    }
}
