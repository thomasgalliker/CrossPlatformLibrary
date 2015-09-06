using System;
using System.Reflection;

using CrossPlatformLibrary.Tools.PlatformSpecific;

namespace CrossPlatformLibrary.Tests.Stubs
{
    /// <summary>
    /// This implementation of IRegistrationConvention allows to probe for stubs in the CrossPlatformLibrary.Tests assembly.
    /// </summary>
    public class TestRegistrationConvention : DefaultRegistrationConvention
    {
        public override string PlatformNamingConvention(AssemblyName assemblyName)
        {
            return string.Format("{0}.{1}", assemblyName.Name, "Tests"); // The currently executing test assembly
        }

        public override string InterfaceToClassNamingConvention(Type interfaceType)
        {
            var defaultNamingConvention = base.InterfaceToClassNamingConvention(interfaceType);
            var testNamingConvention = defaultNamingConvention.Replace("CrossPlatformLibrary.", "CrossPlatformLibrary.Tests.");
            return string.Format("{0}{1}", testNamingConvention, "Stub");
        }
    }
}
