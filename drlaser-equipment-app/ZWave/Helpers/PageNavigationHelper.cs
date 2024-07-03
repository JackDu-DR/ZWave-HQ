using ZWave.Enums;

namespace ZWave.Helpers
{
    internal class PageNavigationHelper
    {
        internal static string FromPageParamName = "FromPage";
        internal static void GoTo(Enums.Page payType, IDictionary<string, object> parameters = default)
        {
            var url = string.Empty;

            switch (payType)
            {
                case Enums.Page.Login:
                    url = "//LoginPage";
                    break;
                case Enums.Page.Master:
                    url = "//MasterPage";
                    break;
                default:
                    break;
            }

            if (!string.IsNullOrEmpty(url))
            {
                if (parameters != null)
                {
                    _ = Shell.Current.GoToAsync(url, parameters);
                }
                else
                {
                    _ = Shell.Current.GoToAsync(url);
                }
            }
        }
    }
}
