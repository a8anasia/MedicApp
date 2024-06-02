using MedicApp.Data;
using Microsoft.EntityFrameworkCore;

namespace MedicApp.Repositories
{
    public class DoctorRepository : BaseRepository<Doctor>, IDoctorRepository
    {
        public DoctorRepository(MedicAppDbContext? context) : base(context)
        {
        }

        public async Task<List<Diagnosis>> GetAllDiagnosisAsync()
        {
           
            return await _context.Diagnoses.ToListAsync();
     
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

        public async Task<List<Medicine>> GetAllMedicinesAsync()
        {
            return await _context.Medicines.ToListAsync();
        }

        public async Task<Doctor?> GetByphoneNumber(string? Phone)
        {
            return await _context.Doctors.Where(d => d.Phone == Phone)
                .FirstOrDefaultAsync();
        }

     

        public async Task<Doctor?> GetDoctorsByLastname(string? Lastname)
        {
            return await _context.Doctors.Where(s => s.Lastname == Lastname)
           .FirstOrDefaultAsync()!;
        }
    }
}
