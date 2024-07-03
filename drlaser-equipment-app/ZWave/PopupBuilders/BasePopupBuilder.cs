namespace ZWave.PopupBuilders
{
    public abstract class BasePopupBuilder<T>
    {
        public static LocalizationResourceManager Localization => LocalizationResourceManager.Instance;
        public abstract T Build();
    }
}
