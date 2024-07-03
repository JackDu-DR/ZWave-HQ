using ZWave.Helpers;

namespace ZWave.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
#if ANDROID
		UsernameBoder.Stroke = ResourceHelper.GetColor("GreyNeutral3");
        UsernameBoder.BackgroundColor = ResourceHelper.GetColor("GreyNeutral3");
		PasswordBoder.Stroke = ResourceHelper.GetColor("GreyNeutral3");
		PasswordBoder.BackgroundColor = ResourceHelper.GetColor("GreyNeutral3");
#endif
    }
}