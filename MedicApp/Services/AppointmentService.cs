using AutoMapper;
using MedicApp.Data;
using MedicApp.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MedicApp.Services
{
    public class AppointmentService : IAppointmentService
    {
        public readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UserService>? _logger;
        public readonly IMapper _mapper;

        public AppointmentService(IUnitOfWork unitOfWork, ILogger<UserService>? logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<List<Appointment>> GetAllAppointments()
        {
            List<Appointment> appointments = (List<Appointment>)await _unitOfWork.AppointmentRepository.GetAllAsync();
            return appointments;
        }

        public async Task<bool> AppointmentExistsAsync(DateOnly date, int doctorId)
        {

            var exists = await _unitOfWork.AppointmentRepository
                .AnyAsync(a => a.Date == date && a.DoctorId == doctorId);

            return exists;
        }
    }
}
