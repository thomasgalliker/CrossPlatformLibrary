using System;
using Android.App;
using Android.Preferences;
using CrossPlatformLibrary.Internals;

namespace CrossPlatformLibrary.Settings
{
    public class SettingsService : SettingsServiceBase
    {
        private static readonly object Locker = new object();

        public SettingsService(ITracer tracer) : base(tracer)
        {
        }

        public SettingsService() : this(new DebugTracer(nameof(SettingsService)))
        {
        }

        protected override object GetValueOrDefaultFunction<T>(string key, T defaultValue)
        {
            lock (Locker)
            {
                using (var sharedPreferences = PreferenceManager.GetDefaultSharedPreferences(Application.Context))
                {
                    var settingsValue = sharedPreferences.GetString(key, null);
                    if (settingsValue != null)
                    {
                        return settingsValue;
                    }
                }
            }

            return defaultValue;
        }

        protected override void AddOrUpdateValueFunction<T>(string key, T value)
        {
            lock (Locker)
            {
                using (var sharedPreferences = PreferenceManager.GetDefaultSharedPreferences(Application.Context))
                {
                    using (var sharedPreferencesEditor = sharedPreferences.Edit())
                    {
                        sharedPreferencesEditor.PutString(key, Convert.ToString(value));
                        sharedPreferencesEditor.Commit();
                    }
                }
            }
        }

        //public void Remove(string key)
        //{
        //    lock (this.locker)
        //    {
        //        sharedPreferencesEditor.Remove(key);
        //        sharedPreferencesEditor.Commit();
        //    }
        //}
    }
}