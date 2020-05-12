using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace BetterHere.Common.Helpers
{
    public static class Settings
    {
        private const string _user = "user";
        private const string _token = "token";
        private const string _isLogin = "isLogin";
        private const string _establishment = "establishment";
        private const string _establishmentLocation = "establishmentLocation";
        private static readonly string _stringDefault = string.Empty;
        private static readonly bool _boolDefault = false;

        private static ISettings AppSettings => CrossSettings.Current;

        public static string User
        {
            get => AppSettings.GetValueOrDefault(_user, _stringDefault);
            set => AppSettings.AddOrUpdateValue(_user, value);
        }

        public static string Token
        {
            get => AppSettings.GetValueOrDefault(_token, _stringDefault);
            set => AppSettings.AddOrUpdateValue(_token, value);
        }

        public static bool IsLogin
        {
            get => AppSettings.GetValueOrDefault(_isLogin, _boolDefault);
            set => AppSettings.AddOrUpdateValue(_isLogin, value);
        }

        public static bool Establishment
        {
            get => AppSettings.GetValueOrDefault(_establishment, _boolDefault);
            set => AppSettings.AddOrUpdateValue(_establishment, value);
        }

        public static bool EstablishmentLocation
        {
            get => AppSettings.GetValueOrDefault(_establishmentLocation, _boolDefault);
            set => AppSettings.AddOrUpdateValue(_establishmentLocation, value);
        }
    }
}
