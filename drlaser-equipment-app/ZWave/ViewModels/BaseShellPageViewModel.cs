using CommunityToolkit.Maui.Views;
using ZWave.Extensions;
using ZWave.PopupBuilders;
using ZWave.Shared.Enums;

namespace ZWave.ViewModels
{
    public abstract class BaseShellPageViewModel : BaseViewModel
    {
        private Popup _establishConnectionToServerErrorPopup;
        private Popup _lostConnectionToServerPopup;
        private Popup _lostConnectionToMachinePopup;

        protected virtual void Appearing()
        {
            if (AppMessageHandler.IsConnectionOpen)
            {
                InitRegisteringOnConnectionOpen();
            }
            else
            {
                ShowEstablishConnectionFailedPopup();
            }
        }

        private void ShowEstablishConnectionFailedPopup()
        {
            var popup = new EstablishConnectionErrorPopupBuilder().Build();
            popup.OnRetryButtonClicked += Popup_OnRetryButtonClicked;
            _establishConnectionToServerErrorPopup = popup;
            _establishConnectionToServerErrorPopup.OpenPopup();
        }

        private void Popup_OnRetryButtonClicked(object sender, EventArgs e)
        {
            try
            {
                AppMessageHandler.EstablishConnection(AppSettings.RabbitMQConfiguration.Url);
                InitRegisteringOnConnectionOpen();
                ((Popup)sender).ClosePopup();

                //ConfigurationService.GetConfig();
            }
            catch (Exception ex)
            {
                LogService.LogUser(LogAction.Connect, $"Connect failed {ex.Message}");
            }
        }

        protected virtual void InitRegisteringOnConnectionOpen()
        {
            AppMessageHandler.OnConnectionShutdown -= OnAppMessageHandlerConnectionShutdown;
            AppMessageHandler.OnConnectionShutdown += OnAppMessageHandlerConnectionShutdown;

            AppMessageHandler.OnConnectionRecovered -= OnAppMessageHandlerConnectionRecovered;
            AppMessageHandler.OnConnectionRecovered += OnAppMessageHandlerConnectionRecovered;

            //AppMessageHandler.IsMachineAliveChanged -= AppMessageHandlerIsMachineAliveChanged;
            //AppMessageHandler.IsMachineAliveChanged += AppMessageHandlerIsMachineAliveChanged;
        }

        private void AppMessageHandlerIsMachineAliveChanged(object sender, bool e)
        {
            if (e == false)
            {
                _lostConnectionToMachinePopup = new MachineConnectionLostPopupBuilder().Build();
                _lostConnectionToMachinePopup.OpenPopup();
                LogService.LogUser(LogAction.Connect, "Machine connection lost!");
            }
            else
            {
                _lostConnectionToMachinePopup.ClosePopup();
                LogService.LogUser(LogAction.Connect, "Machine connection recovered.");
            }
        }

        protected virtual void Disappearing()
        {
            AppMessageHandler.OnConnectionShutdown -= OnAppMessageHandlerConnectionShutdown;
            AppMessageHandler.OnConnectionRecovered -= OnAppMessageHandlerConnectionRecovered;
            AppMessageHandler.IsMachineAliveChanged -= AppMessageHandlerIsMachineAliveChanged;

            _establishConnectionToServerErrorPopup?.ClosePopup();
        }

        private void OnAppMessageHandlerConnectionRecovered(object sender, EventArgs e)
        {
            _lostConnectionToServerPopup.ClosePopup();
            LogService.LogUser(LogAction.Connect, "RabbitMQ connection recovered");
        }

        protected void OnAppMessageHandlerConnectionShutdown(object sender, RabbitMQ.Client.ShutdownEventArgs e)
        {
            _lostConnectionToServerPopup = new SeverConnectionLostPopupBuilder().Build();
            _lostConnectionToServerPopup.OpenPopup();
            LogService.LogUser(LogAction.Connect, "RabbitMQ connection lost!");
        }
    }
}
