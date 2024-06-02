namespace MedicApp.Services.Exceptions
{

	public class PatientAlreadyExistsException : Exception
    {
		public PatientAlreadyExistsException(string s) : base(s)
		{
		}
	}
}