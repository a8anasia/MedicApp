﻿using MedicApp.Data;
using MedicApp.DTO;

namespace MedicApp.Services
{
    public interface IUserService
    {
        Task SignUpUserAsync(UserSignupDTO request);
        Task<User> LoginUserAsync(UserLoginDTO credentials);
        Task<User> UpdateUserAsync(UserPatchDTO request, int userId);

        Task<User?> GetUserByUsernameAsync(string username);
        Task<User?> VerifyAndGetUserAsync(UserLoginDTO request);
    }
}
