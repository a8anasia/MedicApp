using AutoMapper;
using MedicApp.Repositories;

namespace MedicApp.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService>? _logger;

        public ApplicationService(IUnitOfWork unitOfWork, ILogger<UserService>? logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public UserService UserService => new(_unitOfWork, _logger, _mapper);

        public DoctorService DoctorService => new(_unitOfWork, _logger, _mapper);

        public PatientService PatientService => new(_unitOfWork, _logger, _mapper);

        public AppointmentService AppointmentService => new(_unitOfWork, _logger, _mapper);
    }
}
