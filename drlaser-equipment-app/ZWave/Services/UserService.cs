using ZWave.Models;
using ZWave.Shared.Enums;
using ZWave.Shared.Models;

namespace ZWave.Services
{
    public class UserService : BaseService, IUserService
    {
        private static readonly string USER_ENDPOINT = "api/User";

        public async Task<IEnumerable<UserModel>> GetUsers()
        {
            var userDtos = await HttpClientService.GetDataAsync<IEnumerable<UserDto>>($"{USER_ENDPOINT}/GetUsers");
            var userModels = new List<UserModel>();
            if (userDtos != null && userDtos.Count() > 0)
            {
                foreach (var userDto in userDtos)
                {
                    var newUserModel = new UserModel();
                    newUserModel.LoadDataFromDTOJson(userDto);
                    userModels.Add(newUserModel);
                }
            }

            return userModels;
        }

        public async Task<UserModel> GetUserById(Guid userId)
        {
            var userDto = await HttpClientService.GetDataAsync<UserDto>($"{USER_ENDPOINT}/GetUserById?id={userId}");
            var userModel = new UserModel();

            if (userDto != null)
            {
                userModel.LoadDataFromDTOJson(userDto);
            }
            return userModel;
        }

        public Task CreateUser(string fullName, string username, string passwordHash, UserRole userRole, Guid machineId)
        {
            var userDto = new UserForCreateDto
            {
                FullName = fullName,
                UserName = username,
                PasswordHash = passwordHash,
                UserRole = userRole,
                MachineId = machineId
            };

            return HttpClientService.PostDataAsync($"{USER_ENDPOINT}/CreateUser", userDto);
        }

        public Task UpdateUser(Guid userId, string fullName, bool isActive, UserRole userRole)
        {
            var userDto = new UserForUpdateDto
            {
                Id = userId,
                FullName = fullName,
                IsActive = isActive,
                UserRole = userRole
            };

            return HttpClientService.PostDataAsync($"{USER_ENDPOINT}/UpdateUser", userDto);
        }

        public Task ChangePassword(Guid userId, string newPasswordHash)
        {
            var passwordDto = new ChangePasswordDto
            {
                UserId = userId,
                NewPasswordHash = newPasswordHash
            };

            return HttpClientService.PostDataAsync($"{USER_ENDPOINT}/ChangePassword", passwordDto);
        }

        public async Task<IEnumerable<UserModel>> SearchUsers(string search)
        {
            var userDtos = await HttpClientService.GetDataAsync<IEnumerable<UserDto>>($"{USER_ENDPOINT}/SearchUsers?search={search}");
            var userModels = new List<UserModel>();
            if (userDtos.Any())
            {
                foreach (var userDto in userDtos)
                {
                    var newUserModel = new UserModel();
                    newUserModel.LoadDataFromDTOJson(userDto);
                    userModels.Add(newUserModel);
                }
            }

            return userModels;
        }
    }
}
