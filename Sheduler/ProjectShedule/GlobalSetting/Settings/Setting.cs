using Xamarin.Essentials;

namespace ProjectShedule.GlobalSetting.Settings
{
    public abstract class Setting<TClass>
    {
        private string ConvertKey(string nameType) => typeof(TClass).Name + nameType;

        private protected void SavePreference(string key, float value)
        {
            Preferences.Set(ConvertKey(key), value);
        }
        private protected void SavePreference(string key, double value)
        {
            Preferences.Set(ConvertKey(key), value);
        }
        private protected void SavePreference(string key, bool value)
        {
            Preferences.Set(ConvertKey(key), value);
        }

        private protected float GetPreference(string key, float defaultValue)
        {
            return Preferences.Get(ConvertKey(key), defaultValue);
        }
        private protected double GetPreference(string key, double defaultValue)
        {
            return Preferences.Get(ConvertKey(key), defaultValue);
        }
        private protected bool GetPreference(string key, bool defaultValue)
        {
            return Preferences.Get(ConvertKey(key), defaultValue);
        }
    }
}
