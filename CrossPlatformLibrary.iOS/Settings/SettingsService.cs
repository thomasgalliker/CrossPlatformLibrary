using System;
using CrossPlatformLibrary.Internals;
using Foundation;

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
                using (var defaults = NSUserDefaults.StandardUserDefaults)
                {
                    var settingsValue = defaults.StringForKey(key);
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
                using (var defaults = NSUserDefaults.StandardUserDefaults)
                {
                    defaults.SetString(Convert.ToString(value), key);
                    defaults.Synchronize();
                }
            }
        }

        public void Remove(string key)
        {
            ////lock (this.locker)
            ////{
            ////    this.defaults.RemoveObject(key);
            ////    this.defaults.Synchronize();
            ////}
        }
    }
}