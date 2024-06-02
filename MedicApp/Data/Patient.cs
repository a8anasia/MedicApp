using System;
using System.Collections.Generic;

namespace MedicApp.Data;

public partial class Patient
{
    public int Id { get; set; }

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public DateOnly BirthDate { get; set; }

    public string Phone { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int UserId { get; set; }

    public int? AppointmentId { get; set; }

    public virtual Appointment? Appointment { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual User User { get; set; } = null!;
}
