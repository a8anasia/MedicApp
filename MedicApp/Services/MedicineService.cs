using AutoMapper;
using MedicApp.Data;
using MedicApp.Repositories;

namespace MedicApp.Services
{
    public class MedicineService : IMedicineService
    {
        public readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UserService>? _logger;
        public readonly IMapper _mapper;

        public MedicineService(IUnitOfWork unitOfWork, ILogger<UserService>? logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<Medicine>> GetAllMedicinesAsync()
        {
            List<Medicine> medicines = await _unitOfWork.MedicineRepository.GetAllMedicinesAsync();
            return medicines;
        }

        public async Task<Medicine> GetMedicineById(int id)
        {
            Medicine medicine = await _unitOfWork.MedicineRepository.GetMedicineById(id);
            return medicine;
        }

        public async Task AddAsync(Medicine medicine)
        {
            await _unitOfWork.MedicineRepository.AddAsync(medicine);

        }
    }
}
