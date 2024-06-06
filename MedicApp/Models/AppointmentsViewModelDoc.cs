using MedicApp.Data;

namespace MedicApp.Models
{
    public class AppointmentsViewModelDoc
    {
        public int? appointmentId { get; set; }
        public DateOnly? date { get; set; }
        public int? patientId { get; set; }
        public string? patientLastname { get; set; }
        public int? diagnosisId { get; set; }
        public string? diagnosisName { get; set; }
        public int? medicineId { get; set; }
        public string? medicineName { get; set; }
    }
}

