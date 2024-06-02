using System;
using System.Collections.Generic;

namespace MedicApp.Data;

public partial class Diagnosis
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
