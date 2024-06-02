using System;
using System.Collections.Generic;

namespace MedicApp.Data;

public partial class Doctor
{
    public int Id { get; set; }

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int UserId { get; set; }

    public string Speciality { get; set; } = null!;

    public int? AppointmentsId { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual Appointment? AppointmentsNavigation { get; set; }

    public virtual User User { get; set; } = null!;
}
