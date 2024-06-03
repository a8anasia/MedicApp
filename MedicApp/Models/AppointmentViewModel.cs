using MedicApp.Data;

namespace MedicApp.Models
{
    public class AppointmentViewModel
    {
        public List<Doctor>? Doctors { get; set; }

        public DateOnly SelectedDate { get; set; }
        public int SelectedDoctorId { get; set; }
    }
}
