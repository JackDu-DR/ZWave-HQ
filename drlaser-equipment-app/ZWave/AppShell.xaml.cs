using ZWave.Helpers;
using ZWave.Services;

namespace ZWave
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Appearing += OnAppShellAppearing;
        }

        private void OnAppShellAppearing(object sender, EventArgs e)
        {
            CheckNavigation();
        }

        private async void CheckNavigation()
        {
            var accessToken = await SecureStorage.Default.GetAsync(AuthService.ACCESS_TOKEN);

            if (accessToken != null)
            {
                var AuthService = Helpers.ServiceProvider.GetService<IAuthService>();
                AuthService.InitLoggedInData(accessToken);

                PageNavigationHelper.GoTo(Enums.Page.Master);
            }
            else
            {
                PageNavigationHelper.GoTo(Enums.Page.Login);
            }
        }
    }
}
