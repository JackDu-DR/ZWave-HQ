using ZWave.Models;

namespace ZWave.Services.Interfaces
{
    public interface IPermissionService
    {
        Task<IEnumerable<PermissionModel>> GetAllPermissions();
        Task SetPermissions(IEnumerable<PermissionModel> permissionModels);
    }
}
