using MedicApp.Data;
using Microsoft.EntityFrameworkCore;

namespace MedicApp.Repositories
{
    public class DoctorRepository : BaseRepository<Doctor>, IDoctorRepository
    {
        public DoctorRepository(MedicAppDbContext? context) : base(context)
        {
        }

        public async Task<List<Appointment>> GetAllDoctorAppointments(int id)
        {
            List<Appointment> appointments;
            appointments = await _context.Doctors
                       .Where(a => a.Id == id)
                       .SelectMany(a => a.Appointments)
                       .ToListAsync();
            return appointments;
        }

        public async Task<Doctor?> GetByphoneNumber(string? Phone)
        {
            return await _context.Doctors.Where(d => d.Phone == Phone)
                .FirstOrDefaultAsync();
        }

        public async Task<Doctor?> GetDoctorByUserIdAsync(int userId)
        {
            return await _context.Doctors.FirstOrDefaultAsync(p => p.UserId == userId);
        }

        public async Task<int?> GetUserIdByUsernameAsync(string username)
        {
            var user = await _context.Users.FirstOrDefaultAsync(d => d.Username == username);
            return user!.Id;
        }

        public async Task<List<Patient>> GetAllPatient()
        {
            return await _context.Patients.ToListAsync();
        }
    }
}
