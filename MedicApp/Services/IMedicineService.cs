using MedicApp.Data;

namespace MedicApp.Services
{
    public interface IMedicineService
    {
        Task<List<Medicine>> GetAllMedicinesAsync();

    }
}
