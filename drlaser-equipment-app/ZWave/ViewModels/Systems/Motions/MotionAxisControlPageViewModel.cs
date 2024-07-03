using CommonLib.DTO.Implementation;
using CommonLib.Enums;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Text;
using ZWave.Helpers;
using ZWave.Models;

namespace ZWave.ViewModels.Systems.Motions
{
    public partial class MotionAxisControlPageViewModel : BaseSystemMotionViewModel
    {
        protected string Topic => Master.System.Motion.MotionAxisControl.BasePath + "#";
        private string FetchDataId => Master.System.Motion.MotionAxisControl.Fetch;
        private string ApplyContentId => Master.System.Motion.MotionAxisControl.Content;
        private string ChangeAxisId => Master.System.Motion.MotionAxisControl.ChangeAxis;
        private string ActionId => Master.System.Motion.MotionAxisControl.Action;
        private string MotionParamUpdatesId => Master.System.Motion.MotionAxisControl.MotionParamUpdates;
        private string ExecutingBtnId => Master.System.Motion.MotionAxisControl.ExecutingBtnStatus;
        private string EnableBtnId => Master.System.Motion.MotionAxisControl.EnableBtnStatus;

        [ObservableProperty]
        MotionAxisControlModel _motionAxisControlModel;

        private readonly string POSITION_REL = "PosRel";
        private readonly string POSITION_ABS = "PosAbs";

        [ObservableProperty]
        private bool _isPosRelEntryEnabled = true;

        [ObservableProperty]
        private bool _isPosAbsEntryEnabled;

        [ObservableProperty]
        private string _selectedPosition;

        [ObservableProperty]
        private string _movePosRelValue;
        [ObservableProperty]
        private string _movePosAbsValue;
        [ObservableProperty]
        private string _velPosValue;
        [ObservableProperty]
        private string _acclPosValue;
        [ObservableProperty]
        private string _jerkPosValue;

        [ObservableProperty]
        private bool _enableBtnIsActive = true;

        [ObservableProperty]
        private bool _isShowGoBtn = false;
        [ObservableProperty]
        private bool _isShowStopBtn = false;
        [ObservableProperty]
        private bool _isShowSetZeroBtn = false;
        [ObservableProperty]
        private bool _isShowEStopBtn = false;
        [ObservableProperty]
        private bool _isShowHomeBtn = false;

        [ObservableProperty]
        private bool _loadCompleted = false;


        [ObservableProperty]
        private IList<MotionAxisControlConfigurationModel> _motionAxisControlConfigurationModel;
        [ObservableProperty]
        private MotionAxisControlConfigurationModel _selectedAxis;

        public MotionAxisControlPageViewModel()
        {
            MotionAxisControlModel = new MotionAxisControlModel();
            PropertyChanged += OnPropertyChanged;

            LoadConfigurationData();

            MovePosRelValue = MotionAxisControlModel.MovePosRel.ToString();
            MovePosAbsValue = MotionAxisControlModel.MovePosAbs.ToString();
            VelPosValue = MotionAxisControlModel.VelPos.ToString();
            AcclPosValue = MotionAxisControlModel.AcclPos.ToString();
            JerkPosValue = MotionAxisControlModel.JerkPos.ToString();
        }
        private async void LoadConfigurationData()
        {
            await ConfigurationService.GetConfig_MotionAxisControl();
            MotionAxisControlConfigurationModel = ConfigurationService.MotionAxisControlConfigurationModels.ToList();
            LoadCompleted = true;
        }


        [RelayCommand]
        void Loaded()
        {
            AppMessageHandler.SubcribeGeneral(Topic);
            AppMessageHandler.OnGeneralMessageReceived += OnMessageHandlerGeneralMessageReceived;
        }

        [RelayCommand]
        void Unloaded()
        {
            AppMessageHandler.UnsubcribeGeneral(Topic);
            AppMessageHandler.OnGeneralMessageReceived -= OnMessageHandlerGeneralMessageReceived;
        }

        [RelayCommand]
        void SelectedAxisChanged()
        {
            var motionAxisControlDTO = new MotionAxisControlDTO()
            {
                AxisSelection = MotionAxisControlModel.AxisSelection,
            };

            AppMessageHandler.PublishToMachine(ChangeAxisId, motionAxisControlDTO);
        }

        [RelayCommand]
        void Go()
        {
            if (SelectedPosition == POSITION_REL)
                MotionAxisControlModel.MotionCmd = MotionCmd.move_rel;
            else
                MotionAxisControlModel.MotionCmd = MotionCmd.move_abs; 

            MotionAxisControlModel.MotionUIElement = MotionUIElement.GO;

            var motionAxisControlDTO = new MotionAxisControlDTO()
            {
                AxisSelection = MotionAxisControlModel.AxisSelection,
                MotionUIElement = MotionAxisControlModel.MotionUIElement,
                MotionCmd = MotionAxisControlModel.MotionCmd,
                MovePos = IsPosRelEntryEnabled ? MotionAxisControlModel.MovePosRel : MotionAxisControlModel.MovePosAbs,
                VelPos = MotionAxisControlModel.VelPos,
                AcclPos = MotionAxisControlModel.AcclPos,
                JerkPos = MotionAxisControlModel.JerkPos,
            };

            AppMessageHandler.PublishToMachine(ActionId, motionAxisControlDTO);
        }
        [RelayCommand]
        void Stop()
        {
            MotionAxisControlModel.MotionCmd = MotionCmd.stop; 
            MotionAxisControlModel.MotionUIElement = MotionUIElement.STOP; 

            var motionAxisControlDTO = new MotionAxisControlDTO()
            {
                AxisSelection = MotionAxisControlModel.AxisSelection,
                MotionUIElement = MotionAxisControlModel.MotionUIElement,
                MotionCmd = MotionAxisControlModel.MotionCmd,
                MovePos = IsPosRelEntryEnabled ? MotionAxisControlModel.MovePosRel : MotionAxisControlModel.MovePosAbs,
                VelPos = MotionAxisControlModel.VelPos,
                AcclPos = MotionAxisControlModel.AcclPos,
                JerkPos = MotionAxisControlModel.JerkPos,
            };

            AppMessageHandler.PublishToMachine(ActionId, motionAxisControlDTO);
        }
        [RelayCommand]
        void SetZero()
        {
            MotionAxisControlModel.MotionCmd = MotionCmd.reset;
            MotionAxisControlModel.MotionUIElement = MotionUIElement.SET_ZERO;

            var motionAxisControlDTO = new MotionAxisControlDTO()
            {
                AxisSelection = MotionAxisControlModel.AxisSelection,
                MotionUIElement = MotionAxisControlModel.MotionUIElement,
                MotionCmd = MotionAxisControlModel.MotionCmd,
                MovePos = IsPosRelEntryEnabled ? MotionAxisControlModel.MovePosRel : MotionAxisControlModel.MovePosAbs,
                VelPos = MotionAxisControlModel.VelPos,
                AcclPos = MotionAxisControlModel.AcclPos,
                JerkPos = MotionAxisControlModel.JerkPos,
            };

            AppMessageHandler.PublishToMachine(ActionId, motionAxisControlDTO);
        }
        [RelayCommand]
        void Enable()
        {
            MotionAxisControlModel.MotionCmd = MotionCmd.enable;
            MotionAxisControlModel.MotionUIElement = MotionUIElement.ENABLE;

            var motionAxisControlDTO = new MotionAxisControlDTO()
            {
                AxisSelection = MotionAxisControlModel.AxisSelection,
                MotionUIElement = MotionAxisControlModel.MotionUIElement,
                MotionCmd = MotionAxisControlModel.MotionCmd,
                MovePos = IsPosRelEntryEnabled ? MotionAxisControlModel.MovePosRel : MotionAxisControlModel.MovePosAbs,
                VelPos = MotionAxisControlModel.VelPos,
                AcclPos = MotionAxisControlModel.AcclPos,
                JerkPos = MotionAxisControlModel.JerkPos,
            };

            AppMessageHandler.PublishToMachine(ActionId, motionAxisControlDTO);
        }
        [RelayCommand]
        void EStop()
        {
            MotionAxisControlModel.MotionCmd = MotionCmd.eStop;
            MotionAxisControlModel.MotionUIElement = MotionUIElement.E_STOP;

            var motionAxisControlDTO = new MotionAxisControlDTO()
            {
                AxisSelection = MotionAxisControlModel.AxisSelection,
                MotionUIElement = MotionAxisControlModel.MotionUIElement,
                MotionCmd = MotionAxisControlModel.MotionCmd,
                MovePos = IsPosRelEntryEnabled ? MotionAxisControlModel.MovePosRel : MotionAxisControlModel.MovePosAbs,
                VelPos = MotionAxisControlModel.VelPos,
                AcclPos = MotionAxisControlModel.AcclPos,
                JerkPos = MotionAxisControlModel.JerkPos,
            };

            AppMessageHandler.PublishToMachine(ActionId, motionAxisControlDTO);
        }
        [RelayCommand]
        void Home()
        {
            MotionAxisControlModel.MotionCmd = MotionCmd.home;
            MotionAxisControlModel.MotionUIElement = MotionUIElement.HOME;

            var motionAxisControlDTO = new MotionAxisControlDTO()
            {
                AxisSelection = MotionAxisControlModel.AxisSelection,
                MotionUIElement = MotionAxisControlModel.MotionUIElement,
                MotionCmd = MotionAxisControlModel.MotionCmd,
                MovePos = IsPosRelEntryEnabled ? MotionAxisControlModel.MovePosRel : MotionAxisControlModel.MovePosAbs,
                VelPos = MotionAxisControlModel.VelPos,
                AcclPos = MotionAxisControlModel.AcclPos,
                JerkPos = MotionAxisControlModel.JerkPos,
            };

            AppMessageHandler.PublishToMachine(ActionId, motionAxisControlDTO);
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MovePosRelValue))
            {
                if (string.IsNullOrEmpty(MovePosRelValue))
                {
                    return;
                }
                else if (MovePosRelValue.EndsWith("."))
                {
                    return;
                }
                else if ((!string.IsNullOrEmpty(MovePosRelValue) && (InputHelper.IsValidNumberInput(MovePosRelValue)) || InputHelper.IsValidNegativeNumberInput(MovePosRelValue)))
                {
                    MotionAxisControlModel.MovePosRel = double.Parse(MovePosRelValue);
                }
                else if (!string.IsNullOrEmpty(MovePosRelValue) && !InputHelper.IsValidNumericInput(MovePosRelValue))
                {
                    MovePosRelValue = MotionAxisControlModel.MovePosRel.ToString();
                }
            }
            else if (e.PropertyName == nameof(MovePosAbsValue))
            {
                if (string.IsNullOrEmpty(MovePosAbsValue))
                {
                    return;
                }
                else if (MovePosAbsValue.EndsWith("."))
                {
                    return;
                }
                else if ((!string.IsNullOrEmpty(MovePosAbsValue) && (InputHelper.IsValidNumberInput(MovePosAbsValue)) || InputHelper.IsValidNegativeNumberInput(MovePosAbsValue)))
                {
                    MotionAxisControlModel.MovePosAbs = double.Parse(MovePosAbsValue);
                }
                else if (!string.IsNullOrEmpty(MovePosAbsValue) && !InputHelper.IsValidNumericInput(MovePosAbsValue))
                {
                    MovePosAbsValue = MotionAxisControlModel.MovePosAbs.ToString();
                }
            }
            else if (e.PropertyName == nameof(VelPosValue))
            {
                if (string.IsNullOrEmpty(VelPosValue))
                {
                    return;
                }
                else if (VelPosValue.EndsWith("."))
                {
                    return;
                }
                else if ((!string.IsNullOrEmpty(VelPosValue) && (InputHelper.IsValidNumberInput(VelPosValue)) || InputHelper.IsValidNegativeNumberInput(VelPosValue)))
                {
                    MotionAxisControlModel.VelPos = double.Parse(VelPosValue);
                }
                else if (!string.IsNullOrEmpty(VelPosValue) && !InputHelper.IsValidNumericInput(VelPosValue))
                {
                    VelPosValue = MotionAxisControlModel.VelPos.ToString();
                }
            }
            else if (e.PropertyName == nameof(AcclPosValue))
            {
                if (string.IsNullOrEmpty(AcclPosValue))
                {
                    return;
                }
                else if (AcclPosValue.EndsWith("."))
                {
                    return;
                }
                else if ((!string.IsNullOrEmpty(AcclPosValue) && (InputHelper.IsValidNumberInput(AcclPosValue)) || InputHelper.IsValidNegativeNumberInput(AcclPosValue)))
                {
                    MotionAxisControlModel.AcclPos = double.Parse(AcclPosValue);
                }
                else if (!string.IsNullOrEmpty(AcclPosValue) && !InputHelper.IsValidNumericInput(AcclPosValue))
                {
                    AcclPosValue = MotionAxisControlModel.AcclPos.ToString();
                }
            }
            else if (e.PropertyName == nameof(JerkPosValue))
            {
                if (string.IsNullOrEmpty(JerkPosValue))
                {
                    return;
                }
                else if (JerkPosValue.EndsWith("."))
                {
                    return;
                }
                else if ((!string.IsNullOrEmpty(JerkPosValue) && (InputHelper.IsValidNumberInput(JerkPosValue)) || InputHelper.IsValidNegativeNumberInput(JerkPosValue)))
                {
                    MotionAxisControlModel.JerkPos = double.Parse(JerkPosValue);
                }
                else if (!string.IsNullOrEmpty(JerkPosValue) && !InputHelper.IsValidNumericInput(JerkPosValue))
                {
                    JerkPosValue = MotionAxisControlModel.JerkPos.ToString();
                }
            }
            else if (e.PropertyName == nameof(SelectedPosition))
            {
                if (SelectedPosition == POSITION_REL)
                {
                    IsPosRelEntryEnabled = true;
                    IsPosAbsEntryEnabled = false;
                }
                if (SelectedPosition == POSITION_ABS)
                {
                    IsPosRelEntryEnabled = false;
                    IsPosAbsEntryEnabled = true;
                }
            }
            else if (e.PropertyName == nameof(SelectedAxis) && SelectedAxis != null)
            {
                MotionAxisControlModel.SelectedAxis = SelectedAxis;
                MotionAxisControlModel.AxisSelection = (AxisSelection)Enum.Parse(typeof(AxisSelection), SelectedAxis.AxisName);
                MotionAxisControlModel.MovePosRel = SelectedAxis.PositionREL_DefaultValue;
                MotionAxisControlModel.MovePosAbs = SelectedAxis.PositionABS_DefaultValue;
                MotionAxisControlModel.VelPos = SelectedAxis.Velocity_DefaultValue;
                MotionAxisControlModel.AcclPos = SelectedAxis.Accel_DefaultValue;
                MotionAxisControlModel.JerkPos = SelectedAxis.Jerk_DefaultValue;
            }
        }

        private async void OnMessageHandlerGeneralMessageReceived(object sender, byte[] byteArray, string routingKey)
        {
            if (routingKey == ApplyContentId)
            {
                var stringData = Encoding.UTF8.GetString(byteArray);
                var motionAxisControlDTO = JsonConvert.DeserializeObject<MotionAxisControlDTO>(stringData);

                EnableBtnIsActive = motionAxisControlDTO.EnableBtnIsActive;
                MotionAxisControlModel.MovePosRel = motionAxisControlDTO.MovePos;
                MotionAxisControlModel.MovePosAbs = motionAxisControlDTO.MovePos;
                MotionAxisControlModel.VelPos = motionAxisControlDTO.VelPos;
                MotionAxisControlModel.AcclPos = motionAxisControlDTO.AcclPos;
                MotionAxisControlModel.JerkPos = motionAxisControlDTO.JerkPos;
                MotionAxisControlModel.ActualMovePos = motionAxisControlDTO.ActualMovePos;
                MotionAxisControlModel.ActualVelPos = motionAxisControlDTO.ActualVelPos;
                MotionAxisControlModel.ActualAcclPos = motionAxisControlDTO.ActualAcclPos;
                MotionAxisControlModel.ActualJerkPos = motionAxisControlDTO.ActualJerkPos;

                MovePosRelValue = motionAxisControlDTO.MovePos.ToString();
                MovePosAbsValue = motionAxisControlDTO.MovePos.ToString();
                VelPosValue = motionAxisControlDTO.VelPos.ToString();
                AcclPosValue = motionAxisControlDTO.AcclPos.ToString();
                JerkPosValue = motionAxisControlDTO.JerkPos.ToString();
            }
            else if (routingKey == MotionParamUpdatesId)
            {
                var stringData = Encoding.UTF8.GetString(byteArray);
                var motionAxisControlDTO = JsonConvert.DeserializeObject<MotionAxisControlDTO>(stringData);

                MotionAxisControlModel.ActualMovePos = motionAxisControlDTO.ActualMovePos;
                MotionAxisControlModel.ActualVelPos = motionAxisControlDTO.ActualVelPos;
                MotionAxisControlModel.ActualAcclPos = motionAxisControlDTO.ActualAcclPos;
                MotionAxisControlModel.ActualJerkPos = motionAxisControlDTO.ActualJerkPos;

                if (EnableBtnIsActive != motionAxisControlDTO.EnableBtnIsActive)
                    EnableBtnIsActive = motionAxisControlDTO.EnableBtnIsActive;
            }
            else if (routingKey == EnableBtnId)
            {
                var stringData = Encoding.UTF8.GetString(byteArray);
                var motionAxisControlDTO = JsonConvert.DeserializeObject<MotionAxisControlDTO>(stringData);

                EnableBtnIsActive = motionAxisControlDTO.EnableBtnIsActive;
            }
            else if (routingKey == ExecutingBtnId)
            {
                var stringData = Encoding.UTF8.GetString(byteArray);
                var motionAxisControlDTO = JsonConvert.DeserializeObject<MotionAxisControlDTO>(stringData);

                switch (motionAxisControlDTO.ExecutingBtn)
                {
                    case MotionUIElement.GO:
                        {
                            IsShowGoBtn = true;

                            IsShowStopBtn = false;
                            IsShowSetZeroBtn = false;
                            IsShowEStopBtn = false;
                            IsShowHomeBtn = false;
                            break;
                        }
                    case MotionUIElement.STOP:
                        {
                            IsShowStopBtn = true;

                            IsShowGoBtn = false;
                            IsShowSetZeroBtn = false;
                            IsShowEStopBtn = false;
                            IsShowHomeBtn = false;
                            break;
                        }
                    case MotionUIElement.SET_ZERO:
                        {
                            IsShowSetZeroBtn = true;

                            IsShowGoBtn = false;
                            IsShowStopBtn = false;
                            IsShowEStopBtn = false;
                            IsShowHomeBtn = false;
                            break;
                        }
                    case MotionUIElement.E_STOP:
                        {
                            IsShowEStopBtn = true;

                            IsShowGoBtn = false;
                            IsShowStopBtn = false;
                            IsShowSetZeroBtn = false;
                            IsShowHomeBtn = false;
                            break;
                        }
                    case MotionUIElement.HOME:
                        {
                            IsShowHomeBtn = true;

                            IsShowGoBtn = false;
                            IsShowStopBtn = false;
                            IsShowSetZeroBtn = false;
                            IsShowEStopBtn = false;
                            break;
                        }
                    default:
                        {
                            IsShowHomeBtn = false;
                            IsShowGoBtn = false;
                            IsShowStopBtn = false;
                            IsShowSetZeroBtn = false;
                            IsShowEStopBtn = false;
                            break;
                        }
                }
                await Task.Delay(1000);
                IsShowHomeBtn = false;
                IsShowGoBtn = false;
                IsShowStopBtn = false;
                IsShowSetZeroBtn = false;
                IsShowEStopBtn = false;
            }
        }
    }
}