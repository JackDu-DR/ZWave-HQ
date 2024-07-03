using ZWave.Services;
using ZWave.Services.Interfaces;

namespace ZWave.ViewModels.Users
{
    public abstract class UserBaseViewModel : BaseViewModel
    {
        protected readonly IUserService UserService = Helpers.ServiceProvider.GetService<IUserService>();
        protected readonly IPermissionService PermissionService = Helpers.ServiceProvider.GetService<IPermissionService>();
    }
}
