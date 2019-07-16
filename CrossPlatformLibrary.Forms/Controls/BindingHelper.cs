using System;
using System.Reflection;

namespace CrossPlatformLibrary.Forms.Controls
{
    internal static class BindingHelper
    {
        internal static string GetDisplayMemberString(object item, string displayMemberPath)
        {
            return GetDisplayMember(item, displayMemberPath)?.ToString();
        }

        internal static object GetDisplayMember(object item, string displayMemberPath)
        {
            var type = item.GetType();
            var prop = type.GetRuntimeProperty(displayMemberPath);
            if (prop == null)
            {
                throw new InvalidOperationException($"DisplayMemberPath={displayMemberPath} does not exist on item of type {type.Name}.");
            }
            var propValue = prop.GetValue(item);
            return propValue;
        }
    }
}