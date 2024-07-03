using CommonLib.ClassHierarchy.Interface;
using CommonLib.MessageHandler.Interface;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ZWave.Models
{
    public abstract class BaseModel : ObservableObject
    {
        protected readonly IAppMessageHandler AppMessageHandler;
        protected readonly IMaster Master;

        public BaseModel()
        {
            AppMessageHandler = Helpers.ServiceProvider.GetService<IAppMessageHandler>();
            Master = Helpers.ServiceProvider.GetService<IMaster>();
        }

        public abstract void LoadDataFromDTOJson(object baseDTO);
    }
}