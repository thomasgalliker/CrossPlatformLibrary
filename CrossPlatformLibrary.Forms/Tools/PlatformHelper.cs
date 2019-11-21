using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Tools
{
    public static class PlatformHelper
    {
        public static T OnPlatform<T>(T ios, T android, T uwp)
        {
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    return ios;

                case Device.Android:
                    return android;

                case Device.UWP:
                    return uwp;
            }

            return default(T);
        }

        /// <summary>
        /// Define functions for platforms which return the specified values for the current <seealso cref="Device.RuntimePlatform"/>.
        /// </summary>
        /// <typeparam name="T">The platform-specific value.</typeparam>
        /// <param name="platformFactories">The value providers for each platform.</param>
        /// <returns>Returns a single platform-specific value.</returns>
        public static T OnPlatformValue<T>(params (string, Func<T>)[] platformFactories)
        {
            return OnPlatformValues(platformFactories).SingleOrDefault();
        }

        /// <summary>
        /// Define functions for platforms which return the specified values for the current <seealso cref="Device.RuntimePlatform"/>.
        /// </summary>
        /// <typeparam name="T">The platform-specific value.</typeparam>
        /// <param name="platformFactories">The value providers for each platform.</param>
        /// <returns>Returns multiple platform-specific value.</returns>
        public static IEnumerable<T> OnPlatformValues<T>(params (string, Func<T>)[] platformFactories)
        {
            var functions = platformFactories.Where(pf => pf.Item1 == Device.RuntimePlatform).Select(pf => pf.Item2);

            foreach (var func in functions)
            {
                yield return func();
            }
        }

        /// <summary>
        /// Runs actions on specified platforms.
        /// </summary>
        /// <example>
        /// PlatformHelper.RunOnPlatform(
        /// (Device.iOS, () =&gt;{ iosCalls++; }),
        /// (Device.Android, ()=&gt;{ androidCalls++; }));
        /// </example>
        public static void RunOnPlatform(params (string, Action)[] platformActions)
        {
            var actions = platformActions.Where(pf => pf.Item1 == Device.RuntimePlatform).Select(pf => pf.Item2);

            foreach (var action in actions)
            {
                action();
            }
        }

        public static T GetValue<T>(object res)
        {
            var onPlatform = (OnPlatform<T>)res;
            var value = onPlatform.Platforms.FirstOrDefault(p => p.Platform[0] == Device.RuntimePlatform)?.Value;
            if (value != null)
            {
                return (T)System.Convert.ChangeType(value, typeof(T));
            }

            return default(T);
        }
    }
}
