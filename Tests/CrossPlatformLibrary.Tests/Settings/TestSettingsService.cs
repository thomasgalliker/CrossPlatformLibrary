using System.Collections.Generic;
using CrossPlatformLibrary.Internals;
using CrossPlatformLibrary.Settings;

namespace CrossPlatformLibrary.Tests.Settings.Internals
{
    public partial class SettingsServiceBaseTests
    {
        internal class TestSettingsService : SettingsServiceBase
        {
            public TestSettingsService(ITracer tracer) : base(tracer)
            {
            }

            private readonly IDictionary<string, object> storage = new Dictionary<string, object>();

            protected override void AddOrUpdateValueFunction<T>(string key, T value)
            {
                storage[key] = value;
            }

            protected override object GetValueOrDefaultFunction<T>(string key, T defaultValue)
            {
                if (storage.TryGetValue(key, out var value))
                {
                    return value;
                }

                return default(T);
            }
        }
    }
}
