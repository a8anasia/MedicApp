using MedicApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace MedicApp.Repositories
{
    public class MedicineRepository : BaseRepository<Medicine>
    {
        public MedicineRepository(MedicAppDbContext? context) : base(context)
        {
            
        }

        public async Task<List<Medicine>> GetAllMedicinesAsync()
        {
            List<Medicine> medicines;
            medicines = await _context.Medicines.ToListAsync();
            return medicines;
        }

        public async Task<Medicine> GetMedicineById(int id)
        {
            Medicine medicine = await _context.Medicines.FirstOrDefaultAsync(x => x.Id == id);
            return medicine;
        }

        public async Task AddAsync(Medicine medicine)
        {
            await _context.Medicines.AddAsync(medicine);
        }
    }
}
