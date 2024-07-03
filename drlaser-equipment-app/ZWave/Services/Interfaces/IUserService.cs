using ZWave.Models;
using ZWave.Shared.Enums;

namespace ZWave.Services
{
    public interface IUserService
    {
        Task ChangePassword(Guid userId, string newPasswordHash);
        Task CreateUser(string fullName, string username, string passwordHash, UserRole userRole, Guid machineId);
        Task<UserModel> GetUserById(Guid userId);
        Task<IEnumerable<UserModel>> GetUsers();
        Task UpdateUser(Guid userId, string fullName, bool isActive, UserRole userRole);
        Task<IEnumerable<UserModel>> SearchUsers(string search);
    }
}