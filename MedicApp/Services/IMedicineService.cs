using MedicApp.Data;

namespace MedicApp.Services
{
    public interface IMedicineService
    {
        Task<List<Medicine>> GetAllMedicinesAsync();
        Task<Medicine> GetMedicineById(int id);
        Task AddAsync(Medicine medicine);
    }
}
