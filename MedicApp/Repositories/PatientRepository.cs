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

        public async Task<List<Appointment>> GetAllPatientAppointments(int patiendId)
        {
            List<Appointment> appointments;
            appointments = await _context.Appointments
                       .Where(a => a.PatientId == patiendId)
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

        public async Task<int> GetUserIdByUsernameAsync(string username)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            return user.Id;
        }

        public async Task<Patient?> GetPatientByUserIdAsync(int userId)
        {
            return await _context.Patients.FirstOrDefaultAsync(p => p.UserId == userId);
        }

    
    }
}
