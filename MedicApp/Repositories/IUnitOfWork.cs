namespace MedicApp.Repositories
{
    public interface IUnitOfWork
    {
        public UserRepository UserRepository { get; }
        public PatientRepository PatientRepository { get; }
        public DoctorRepository DoctorRepository { get; }

        Task<bool> SaveAsync();


    }
}
