using MedicApp.Models;
using System.ComponentModel.DataAnnotations;

namespace MedicApp.DTO
{
    public class UserSignupDTO
    {
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Username should be between 2 and 50 characters.")]
        public string? Username { get; set; }


        [StringLength(200, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters.")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*\W).{8,}$",
        ErrorMessage = "Password must contain at least one uppercase letter, " +
            "one lowercase letter, one digit, and one special character.")]
        public string? Password { get; set; }


        [StringLength(50, MinimumLength = 2, ErrorMessage = "Firstname should not exceed 50 characters.")]
        public string? Firstname { get; set; }


        [StringLength(50, MinimumLength = 2, ErrorMessage = "Lastname should not exceed 50 characters.")]
        public string? Lastname { get; set; }


        [EnumDataType(typeof(UserRole), ErrorMessage = "Invalid user role.")]
        public UserRole? UserRole { get; set; }

        //Doctors specific properties
        [StringLength(10, ErrorMessage = "Phone number should not exceed 15 characters.")]
        [Phone(ErrorMessage = "Invalid phone number.")]
        public string? DoctorPhone { get; set; }

        [StringLength(100, ErrorMessage = "Email should not exceed 100 characters.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string? DoctorEmail { get; set; }

        [StringLength(100, ErrorMessage = "Specialty should not exceed 100 characters.")]
        public string? Specialty { get; set; }



        //Patients specific properties
        [StringLength(10, ErrorMessage = "Phone number should not exceed 15 characters.")]
        [Phone(ErrorMessage = "Invalid phone number.")]
        public string? PatientPhone { get; set; }

        [StringLength(100, ErrorMessage = "Email should not exceed 100 characters.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string? PatientEmail { get; set; }

        public DateOnly? BirthDate { get; set; }

        public override string? ToString()
        {
            return $"{Username} {Firstname} {Lastname} {DoctorEmail} {DoctorEmail} {PatientEmail} {BirthDate} {Password} {PatientPhone}{DoctorPhone} {Specialty} {UserRole!.Value}";
        }
    }
}
