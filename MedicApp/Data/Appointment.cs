using System;
using System.Collections.Generic;

namespace MedicApp.Data;

public partial class Appointment
{
    public int Id { get; set; }

    public int PatientId { get; set; }

    public int DoctorId { get; set; }

    public DateOnly Date { get; set; }

    public int? DiagnosisId { get; set; }

    public int? MedicineId { get; set; }

    public virtual Diagnosis? Diagnosis { get; set; }

    public virtual Doctor Doctor { get; set; } = null!;

    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();

    public virtual Medicine? Medicine { get; set; }

    public virtual Patient Patient { get; set; } = null!;

    public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();
}
