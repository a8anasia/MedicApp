using AutoMapper;
using Azure.Core;
using MedicApp.Data;
using MedicApp.DTO;
using MedicApp.Models;
using MedicApp.Repositories;
using MedicApp.Security;
using MedicApp.Services.Exceptions;

namespace MedicApp.Services
{
    public class UserService : IUserService
    {
        public readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UserService>? _logger;
        public readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, ILogger<UserService>? logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<User> LoginUserAsync(UserLoginDTO request)
        {
            var user = await _unitOfWork!.UserRepository.GetUserAsync(request.Username!, request.Password!);
            if (user == null)
            {
                return null;
            }
            return user;
        }

        public async Task SignUpUserAsync(UserSignupDTO signupDTO)
        {
            Patient patient;
            Doctor doctor;
            User user;

            try
            {
                user = ExtractUser(signupDTO);
                User? existingUser = await _unitOfWork.UserRepository.GetByUsernameAsync(user.Username!);

                if (existingUser != null)
                {
                   throw new UserAlreadyExistsException("User Exists: " + existingUser.Username);
                }

                user.Password = EncryptionUtil.Encrypt(user.Password);

              
                await _unitOfWork.UserRepository.AddAsync(user);
         

                if (user.UserRole == UserRole.Doctor)
                {
                    doctor = ExtractDoctor(signupDTO);
                    if (await _unitOfWork.DoctorRepository.GetByphoneNumber(doctor.Phone) is not null)
                    {
                        throw new DoctorAlreadyExistsException("Doctor Phone Number Exists");
                    }
                    await _unitOfWork.DoctorRepository.AddAsync(doctor);
                    doctor.User = user;
                }
                else if (user.UserRole == UserRole.Patient)
                {
                    patient = ExtractPatient(signupDTO);
                    if (await _unitOfWork.DoctorRepository.GetByphoneNumber(patient.Phone) is not null)
                    {
                        throw new PatientAlreadyExistsException("Patient Phone Number Exists");
                    }
                    await _unitOfWork.PatientRepository.AddAsync(patient);
                    patient.User = user;
                }

                await _unitOfWork!.SaveAsync();
                _logger!.LogInformation("{Message}", "User: " + user + " signup success");

            } catch (Exception ex)
            {
                _logger!.LogError("{Message}{Exception}", ex.Message, ex.StackTrace);
                throw;
            }
        }

        public async Task<User> UpdateUserAsync(UserPatchDTO request, int userId)
        {
            User? existingUser;
            User? user = null;
            try
            {
                existingUser = await _unitOfWork.UserRepository.GetAsync(userId);
                if (existingUser == null) return null;

                var userToUpdate = _mapper!.Map<User>(request);

                user = await _unitOfWork.UserRepository.UpdateUserAsync(userId, userToUpdate);
                await _unitOfWork.SaveAsync();
                _logger!.LogInformation("{Message}", "User: " + user + " updated successfully");
            }
            catch (Exception e)
            {
                _logger!.LogError("{Message}{Exception}", e.Message, e.StackTrace);
            }
            return user;
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            User? user = null;
            try
            {
                user = await _unitOfWork!.UserRepository.GetByUsernameAsync(username);
                _logger!.LogInformation("{Message}", "User: " + user + " found and returned");
            }
            catch (Exception e)
            {
                _logger!.LogError("{Message}{Exception}", e.Message, e.StackTrace);
            }
            return user;
        }


        public async Task<User?> VerifyAndGetUserAsync(UserLoginDTO request)
        {
            User? user = null;

            try
            {
                user = await _unitOfWork!.UserRepository.GetUserAsync(request.Username!, request.Password!);
                _logger!.LogInformation("{Message}", "User: " + user + " found and returned");
            }
            catch (Exception e)
            {
                _logger!.LogError("{Message}{Exception}", e.Message, e.StackTrace);
            }
            return user;
        }

        private User ExtractUser(UserSignupDTO signupDTO)
        {
            return new User()
            {
                Username = signupDTO.Username!,
                Password = signupDTO.Password!,
                Firstname = signupDTO.Firstname!,
                Lastname = signupDTO.Lastname!,
                UserRole = signupDTO.UserRole
            };
        }

        private Patient ExtractPatient(UserSignupDTO signupDTO)
        {
            return new Patient()
            {
                Firstname = signupDTO.Firstname!,
                Lastname = signupDTO.Lastname!,
                Phone = signupDTO.PatientPhone!,
                Email = signupDTO.PatientEmail!,
                BirthDate = (DateOnly)signupDTO.BirthDate!,
              
            };
        }

        private Doctor ExtractDoctor(UserSignupDTO signupDTO)
        {
            return new Doctor()
            {
                Firstname = signupDTO.Firstname!,
                Lastname = signupDTO.Lastname!,
                Phone = signupDTO.DoctorPhone!,
                Email = signupDTO.DoctorEmail!,
                Specialty = signupDTO.Specialty!

            };
        }

    }
}
