using System.Globalization;
using ZWave.Shared.Enums;
using ZWave.Views;

namespace ZWave.Converters
{
    public class TypeToViewConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var tabPage = (TabPage)value;

            switch (tabPage)
            {
                case TabPage.Jobs:
                    return Helpers.ServiceProvider.GetService(typeof(JobsPage));

                case TabPage.System:
                    return Helpers.ServiceProvider.GetService(typeof(SystemPage));

                case TabPage.Recipies:
                    return Helpers.ServiceProvider.GetService(typeof(RecipiesPage));

                case TabPage.Setup:
                    return Helpers.ServiceProvider.GetService(typeof(SetupPage));

                case TabPage.Datalog:
                    return Helpers.ServiceProvider.GetService(typeof(DatalogPage));

                case TabPage.Alarms:
                    return Helpers.ServiceProvider.GetService(typeof(AlarmsPage));

                case TabPage.Users:
                    return Helpers.ServiceProvider.GetService(typeof(UsersPage));
            }

            throw new Exception($"TabPage {tabPage} does not exist");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
