using CommonLib.DTO.Implementation;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Text;
using ZWave.Extensions;
using ZWave.Helpers;
using ZWave.PopupBuilders;
using ZWave.Resources.Strings;
using ZWave.Shared.Enums;

namespace ZWave.ViewModels
{
    public partial class MasterPageViewModel : BaseShellPageViewModel, IQueryAttributable
    {
        private string MachineMessageRoutingKey => Master.Message;

        private bool _isNavigatedFromLoginPage;

        protected string Topic => Master.BasePath + "#";

        private string ApplyContentId => Master.Content;

        [ObservableProperty]
        List<string> _messageBoxContentList = new List<string>();

        [ObservableProperty]
        string _messageBoxContent;

        [ObservableProperty]
        TabPage _selectedTabPage;

        [ObservableProperty]
        string _currentDateTime;

        [ObservableProperty]
        string _selectedPageTitle;

        [ObservableProperty]
        string _fullName;

        [ObservableProperty]
        string _displayVersion;

        [ObservableProperty]
        IEnumerable<TabPage> _availableTabPages;

        [ObservableProperty]
        string _message;

        [RelayCommand]
        void OnNavigationButtonSelected(TabPage pageType)
        {
            var pageName = Enum.GetName(typeof(TabPage), pageType);
            LogService.LogUser(LogAction.UserActivity, $"Clicked - {pageName} button");
            SelectedPageTitle = Localization[pageName].ToString();
            NavigateToPage(pageType);
        }

        [ObservableProperty]
        List<string> _languages;

        string _selectedLanguage;
        public string SelectedLanguage
        {
            get => _selectedLanguage;
            [System.Diagnostics.CodeAnalysis.MemberNotNull(nameof(_selectedLanguage))]
            set
            {
                if (!EqualityComparer<string>.Default.Equals(_selectedLanguage, value))
                {
                    _selectedLanguage = value;
                    OnPropertyChanged(nameof(SelectedLanguage));

                    ChangeLanguage();
                }
            }
        }

        [RelayCommand]
        protected override void Appearing()
        {
            base.Appearing();
            SetupView();
        }

        protected override void InitRegisteringOnConnectionOpen()
        {
            base.InitRegisteringOnConnectionOpen();
            //AppMessageHandler.SubcribeGeneral(MachineMessageRoutingKey);
            AppMessageHandler.SubcribeGeneral(ApplyContentId);
            AppMessageHandler.OnGeneralMessageReceived += GeneralMessageReceived;

            CriticalAction.SubscribeCriticalTopic();
            CriticalAction.FetchData();
        }

        private void GeneralMessageReceived(object sender, byte[] data, string routingKey)
        {
            //if (routingKey == MachineMessageRoutingKey)
            //{
            //    var jsonString = StringHelper.ByteArrayToString(data);
            //    Message = JsonConvert.DeserializeObject<string>(jsonString);
            //}

            if (routingKey == ApplyContentId)
            {
                var stringData = Encoding.UTF8.GetString(data);
                var masterDTO = JsonConvert.DeserializeObject<MasterDTO>(stringData);

                string receivedMessage = DateTime.Now.ToString("dd MMM yyyy HH:mm:ss") + " - " + masterDTO.MessageContent + "\n";

                MessageBoxContentList.Add(receivedMessage);

                if (MessageBoxContentList.Count > 50)
                {
                    MessageBoxContentList.RemoveAt(0);
                }

                string tempShowingMessage = "";
                //for (int i = MessageBoxContentList.Count -1; i >= 0; i--)
                //{
                //    tempShowingMessage += MessageBoxContentList[i];
                //}
                for (int i = 0; i < MessageBoxContentList.Count; i++)
                {
                    tempShowingMessage += MessageBoxContentList[i];
                }
                MessageBoxContent = tempShowingMessage;
            }
        }

        [RelayCommand]
        protected override void Disappearing()
        {
            base.Disappearing();
            AppMessageHandler.UnsubcribeGeneral(MachineMessageRoutingKey);
            AppMessageHandler.OnGeneralMessageReceived -= GeneralMessageReceived;
        }

        [RelayCommand]
        void Logout()
        {
            LogService.LogUser(LogAction.UserActivity, "Clicked - Logout");
            LogService.RegisterUserForLogActivities(null);
            AuthService.ClearLoggedInData();
            PageNavigationHelper.GoTo(Enums.Page.Login);
        }

        private async void SetupView()
        {
            var assembly = typeof(App).GetTypeInfo().Assembly;
            var version = assembly.GetName().Version;
            DisplayVersion = $"v{version.Major}.{version.Minor}.{version.Build} ZWave\n" +
                $"v1.0.1 MachineApps\n" +
                 $"v1.0.2 Controller A\n" +
                  $"v1.0.3 Camera A\n" +
                   $"v1.0.4 Camera B\n" +
                    $"v1.0.5 Laser Head\n" +
                     $"v1.0.6 IO Control";

            var userInfoDto = await AuthService.GetUserInfo();

            FullName = userInfoDto.Fullname ?? string.Empty;
            AvailableTabPages = userInfoDto.PermittedPages;
            //LogService.RegisterUserForLogActivities(userInfoDto);
            if (_isNavigatedFromLoginPage)
            {
                LogService.LogUser(LogAction.UserActivity, "Login");
                //OnNavigationButtonSelected(AvailableTabPages.Min());
                _isNavigatedFromLoginPage = false;
            }
        }

        public MasterPageViewModel()
        {
            UpdateCurrentDateTime();

            _languages = new List<string>() { "English", "中文" };
            _selectedLanguage = "English";

            Localization.PropertyChanged -= OnLanguagesChanged;
            Localization.PropertyChanged += OnLanguagesChanged;

            AuthService.LogoutRequested -= OnAuthServiceLogoutRequested;
            AuthService.LogoutRequested += OnAuthServiceLogoutRequested;      
        }

        [RelayCommand]
        void VersionBtnClicked()
        {
            var popup = new InformationBoxPopupBuilder("Version Details", DisplayVersion).Build();
            popup.OnOKButtonClicked += Popup_OnOKButtonClicked;
            popup.OpenPopup();
        }

        private void Popup_OnOKButtonClicked(object sender, EventArgs e)
        {
            ((Popup)sender).ClosePopup();
        }

        private void OnAuthServiceLogoutRequested(object sender, EventArgs e)
        {
            Logout();
        }

        private void NavigateToPage(TabPage pageType)
        {
            SelectedTabPage = pageType;
        }

        private void ChangeLanguage()
        {
            LogService.LogUser(LogAction.UserActivity, $"Clicked - Change Language to {_selectedLanguage}");

            var culture = AppResources.Culture.TwoLetterISOLanguageName
                .Equals("zh", StringComparison.InvariantCultureIgnoreCase) ?
                new CultureInfo("en-US") : new CultureInfo("zh-CN");

            Localization.SetCulture(culture);

            AppResources.Culture = culture;
            CultureInfo.CurrentUICulture = culture;
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }

        private void OnLanguagesChanged(object sender, PropertyChangedEventArgs e)
        {
            SelectedPageTitle = Localization[Enum.GetName(typeof(TabPage), SelectedTabPage)].ToString();
        }

        private void UpdateCurrentDateTime()
        {
            CurrentDateTime = DateTime.Now.ToString("dd MMM yyyy - HH:mm");
            Dispatcher.GetForCurrentThread().StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                CurrentDateTime = DateTime.Now.ToString("dd MMM yyyy - HH:mm");
                return true;
            });
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey(PageNavigationHelper.FromPageParamName))
            {
                _isNavigatedFromLoginPage = (Enums.Page)query[PageNavigationHelper.FromPageParamName] == Enums.Page.Login;
            }
        }
    }
}
