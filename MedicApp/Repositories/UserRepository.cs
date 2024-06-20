using MedicApp.Data;
using MedicApp.DTO;
using MedicApp.Models;
using MedicApp.Security;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace MedicApp.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(MedicAppDbContext? context) : base(context)
        {
        }

        public async Task<User?> GetByUsernameAsync(string username)
        { 
            var user = await _context.Users.Where(u => u.Username == username)
                .FirstOrDefaultAsync();
            return user;
        }
  
        public async Task<User?> GetUserAsync(string username, string password)
        {
            User? user = null;

            try
            {
                user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            }
            catch(Exception e)
            {
                Console.WriteLine("user does not exits", e.StackTrace);
            }


            if (user == null)
            {
                return null;
            }
            if (!EncryptionUtil.IsValidPassword(password, user.Password!))
            {
                return null;
            }
            return user;
        }

        public async Task<bool> SignUpUserAsync(UserSignupDTO request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var existingUser = await _context.Users
                 .FirstOrDefaultAsync(u => u.Username == request.Username);

            if (existingUser != null) return false;

            var user = new User()
            {
                Username = request.Username!,
                Firstname = request.Firstname!,
                Lastname = request.Lastname!,
                Password = EncryptionUtil.Encrypt(request.Password!),
                UserRole = request.UserRole
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            if (request.UserRole == UserRole.Doctor)
            {
                var doctor = new Doctor
                {
                    Firstname = request.Firstname!,
                    Lastname = request.Lastname!,
                    Phone = request.DoctorPhone!,
                    Email = request.DoctorEmail!,
                    Specialty = request.Specialty!,
                    UserId = user.Id
                };

                _context.Doctors.Add(doctor);
                await _context.SaveChangesAsync();
            }

            if (request.UserRole == UserRole.Patient)
            {
                var patient = new Patient
                {
                    Firstname = request.Firstname!,
                    Lastname = request.Lastname!,
                    BirthDate = request.BirthDate!.Value,
                    Phone = request.PatientPhone!,
                    Email = request.PatientEmail!,
                    UserId = user.Id
                };

                _context.Patients.Add(patient);
                await _context.SaveChangesAsync();
            }

            return true;

        }

        public async Task<User> UpdateUserAsync(int userId, User user)
        {
            var existingUser = await _context.Users.Where(u => u.Id == userId)
                .FirstOrDefaultAsync();

            if (existingUser == null) { return null!; }

            if(existingUser.Id != userId) { return null!; }

            _context.Users.Attach(user);
            _context.Entry(user).State = EntityState.Modified;

            return existingUser;
        }
    }
}
