﻿namespace MedicApp.Repositories
{
    public interface IUnitOfWork
    {
        public UserRepository UserRepository { get; }
        public PatientRepository PatientRepository { get; }
        public DoctorRepository DoctorRepository { get; }

        public AppointmentRepository AppointmentRepository { get; }

        public MedicineRepository MedicineRepository { get; }
        public DiagnosisRepository DiagnosisRepository { get; }

        Task<bool> SaveAsync();


    }
}
