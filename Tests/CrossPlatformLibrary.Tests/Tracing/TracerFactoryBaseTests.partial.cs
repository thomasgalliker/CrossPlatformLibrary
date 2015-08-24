using System;
using System.Diagnostics;
using System.Globalization;

namespace CrossPlatformLibrary.Tests.Tracing
{
    public partial class TracerFactoryBaseTests
    {
        private static string NameAndVersion(Type type)
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}[{1}]", type.FullName, FileVersionInfo.GetVersionInfo(type.Assembly.Location).FileVersion);
        }
    }
}