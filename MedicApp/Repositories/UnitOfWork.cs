using MedicApp.Data;

namespace MedicApp.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MedicAppDbContext _context;

        public UnitOfWork(MedicAppDbContext context)
        {
            _context = context;
        }


        public UserRepository UserRepository => new(_context);

        public PatientRepository PatientRepository => new(_context);

        public DoctorRepository DoctorRepository => new(_context);

        public AppointmentRepository AppointmentRepository => new(_context);

        public MedicineRepository MedicineRepository => new(_context);

        public DiagnosisRepository DiagnosisRepository => new(_context);

      

        public async Task<bool> SaveAsync()
        {
           try
           {
            return await _context.SaveChangesAsync() >0;
           }
            catch (Exception ex)
           {
             Console.WriteLine($"An error occurred while saving changes: {ex.InnerException?.Message ?? ex.Message}");
             throw; 
           }
        }
    }
}
