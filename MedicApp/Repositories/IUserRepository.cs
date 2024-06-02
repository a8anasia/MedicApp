using MedicApp.Data;
using MedicApp.DTO;

namespace MedicApp.Repositories
{
    public interface IUserRepository
    {

        Task<bool> SignUpUserAsync(UserSignupDTO request);
        Task<User?> GetUserAsync(string username, string password);
        Task<User> UpdateUserAsync(int userId, User request);
        Task<User?> GetByUsernameAsync(string username);
    }
}
