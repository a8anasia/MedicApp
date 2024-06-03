using MedicApp.Data;
using Microsoft.EntityFrameworkCore;

namespace MedicApp.Services
{
    public interface IAppointmentService
    {
        Task<List<Appointment>> GetAllAppointments();

        Task<bool> AppointmentExistsAsync(DateOnly date, int doctorId);
    }
}
