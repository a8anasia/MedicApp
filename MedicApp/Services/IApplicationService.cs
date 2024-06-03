namespace MedicApp.Services
{
	public interface IApplicationService
	{
		UserService UserService { get; }
		DoctorService DoctorService { get; }
		PatientService PatientService {get; }

		AppointmentService AppointmentService { get; }
	}
}