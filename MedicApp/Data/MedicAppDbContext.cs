using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MedicApp.Data;

public partial class MedicAppDbContext : DbContext
{
    public MedicAppDbContext()
    {
    }

    public MedicAppDbContext(DbContextOptions<MedicAppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<Diagnosis> Diagnoses { get; set; }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<Medicine> Medicines { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost\\sqlexpress;Database=MedicAppDB;User=mediUser;Password=12345;MultipleActiveResultSets=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Greek_CI_AI");

        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Appointm__3213E83FBA2DB9A4");

            entity.ToTable("Appointment");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.DiagnosisId).HasColumnName("diagnosisId");
            entity.Property(e => e.DoctorId).HasColumnName("doctorId");
            entity.Property(e => e.MedicineId).HasColumnName("medicineId");
            entity.Property(e => e.PatientId).HasColumnName("patientId");

            entity.HasOne(d => d.Diagnosis).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.DiagnosisId)
                .HasConstraintName("FK__Appointme__diagn__71D1E811");

            entity.HasOne(d => d.Doctor).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.DoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Appointme__docto__6FE99F9F");

            entity.HasOne(d => d.Medicine).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.MedicineId)
                .HasConstraintName("FK__Appointme__medic__70DDC3D8");

            entity.HasOne(d => d.Patient).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Appointme__patie__6EF57B66");
        });

        modelBuilder.Entity<Diagnosis>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Diagnosi__3213E83F8950B8A6");

            entity.ToTable("Diagnosis");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Doctor__3213E83FFA611820");

            entity.ToTable("Doctor");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AppointmentsId).HasColumnName("appointmentsId");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Firstname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("firstname");
            entity.Property(e => e.Lastname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("lastname");
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.Speciality)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("speciality");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.AppointmentsNavigation).WithMany(p => p.Doctors)
                .HasForeignKey(d => d.AppointmentsId)
                .HasConstraintName("FK_Doctor_Appointment");

            entity.HasOne(d => d.User).WithMany(p => p.Doctors)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Doctor__userId__6C190EBB");
        });

        modelBuilder.Entity<Medicine>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Medicine__3213E83FB5530A10");

            entity.ToTable("Medicine");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Patient__3213E83F42DEF8D2");

            entity.ToTable("Patient");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AppointmentId).HasColumnName("appointmentId");
            entity.Property(e => e.BirthDate).HasColumnName("birthDate");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Firstname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("firstname");
            entity.Property(e => e.Lastname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("lastname");
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.Appointment).WithMany(p => p.Patients)
                .HasForeignKey(d => d.AppointmentId)
                .HasConstraintName("FK_Patient_Appointment");

            entity.HasOne(d => d.User).WithMany(p => p.Patients)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Patient__userId__693CA210");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3213E83F2D98EF55");

            entity.ToTable("User");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Firstname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("firstname");
            entity.Property(e => e.Lastname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("lastname");
            entity.Property(e => e.Password)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.UserRole)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("user_role")
                .HasConversion<string>();


            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    internal async Task<Appointment> SingleOrDefaultAsync(Func<object, bool> value)
    {
        throw new NotImplementedException();
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
