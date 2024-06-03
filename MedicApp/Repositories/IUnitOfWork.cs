namespace MedicApp.Repositories
{
    public interface IUnitOfWork
    {
        public UserRepository UserRepository { get; }
        public PatientRepository PatientRepository { get; }
        public DoctorRepository DoctorRepository { get; }

        public AppointmentRepository AppointmentRepository { get; }

        Task<bool> SaveAsync();


    }
}
