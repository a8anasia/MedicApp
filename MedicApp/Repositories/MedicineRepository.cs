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
    }
}
