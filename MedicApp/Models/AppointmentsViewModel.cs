using MedicApp.Data;
using Microsoft.Identity.Client;

namespace MedicApp.Models
{
    public class AppointmentsViewModel
    {
        public int? appointmentId { get; set; }
        public DateOnly? date {  get; set; }
        public int? doctorId { get; set;}
        public string? doctorLastname { get; set; }
        public string? Specialty { get; set; }
        public int? diagnosisId { get; set; }
        public string? diagnosisName {  get; set; }
        public int? medicineId { get; set; }
        public string? medicineName { get; set; }

    }
}
