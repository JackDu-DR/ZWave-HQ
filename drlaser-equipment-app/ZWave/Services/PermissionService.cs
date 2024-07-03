using ZWave.Models;
using ZWave.Services.Interfaces;
using ZWave.Shared.Enums;
using ZWave.Shared.models;

namespace ZWave.Services
{
    public class PermissionService : IPermissionService
    {
        private static readonly string PERMISSION_ENDPOINT = "api/Permission";

        private readonly IHttpClientService _httpClientService = Helpers.ServiceProvider.GetService<IHttpClientService>();

        public async Task<IEnumerable<PermissionModel>> GetAllPermissions()
        {
            var permissionDtos = await _httpClientService.GetDataAsync<IEnumerable<PermissionDto>>($"{PERMISSION_ENDPOINT}/GetAllRolesPages");
            var permisisonModels = InitPermissions().ToList();

            if (permissionDtos != null && permissionDtos.Count() > 0)
            {
                foreach(var permissionDto in permissionDtos)
                {
                    var existedPermission = permisisonModels.FirstOrDefault(pm => pm.UserRole == permissionDto.UserRole && pm.TabPage == permissionDto.TabPage);
                    if (existedPermission != null)
                    {
                        existedPermission.AccessAllowed = permissionDto.AccessAllowed;
                    }
                }
            }

            return permisisonModels;
        }

        public async Task SetPermissions(IEnumerable<PermissionModel> permissionModels)
        {
            var permissionDtos = new List<PermissionDto>();
            foreach (var permissionModel in permissionModels)
            {
                permissionDtos.Add(new PermissionDto
                {
                    UserRole = permissionModel.UserRole,
                    TabPage = permissionModel.TabPage,
                    AccessAllowed = permissionModel.AccessAllowed,
                });
            }

            await _httpClientService.PostDataAsync($"{PERMISSION_ENDPOINT}/SetPagesForRoles", permissionDtos);
        }

        private IEnumerable<PermissionModel> InitPermissions()
        {
            var permissions = new List<PermissionModel>();
            foreach (UserRole userRole in Enum.GetValues(typeof(UserRole)))
            {
                foreach (TabPage tabPage in Enum.GetValues(typeof(TabPage)))
                {
                    permissions.Add(new PermissionModel
                    {
                        UserRole = userRole,
                        TabPage = tabPage,
                        AccessAllowed = false,
                    });;
                }
            }

            return permissions;
        }
    }
}
