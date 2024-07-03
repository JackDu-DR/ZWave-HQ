using System.ComponentModel;

namespace ZWave.Views.Jobs;

public partial class JobMainView : ContentView
{
    private readonly LocalizationResourceManager _localizer;
	public JobMainView()
	{
		InitializeComponent();
        _localizer = LocalizationResourceManager.Instance;
        _localizer.PropertyChanged += OnCultureChanged;
        SetLocalizedAxis();
    }

    private void SetLocalizedAxis()
    {
        var localizedAxis = (string)_localizer["Axis"];
        AxisOne.Title = $"{localizedAxis} 1";
        AxisTwo.Title = $"{localizedAxis} 2";
        AxisThree.Title = $"{localizedAxis} 3";
        AxisFour.Title = $"{localizedAxis} 4";
    }

    private void OnCultureChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == null)
        {
            SetLocalizedAxis();
        }
    }
}