using AutoMapper;
using MedicApp.Data;
using MedicApp.Repositories;

namespace MedicApp.Services
{
    public class DiagnosisService : IDiagnosisService
    {
        public readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UserService>? _logger;
        public readonly IMapper _mapper;

        public DiagnosisService(IUnitOfWork unitOfWork, ILogger<UserService>? logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<Diagnosis>> GetAllDiagnosisAsync()
        {
            List<Diagnosis> diagnoses = await _unitOfWork.DiagnosisRepository.GetAllDiagnosisAsync();
                return diagnoses;
        }
    }
}
