using System;
using System.ComponentModel;

namespace CrossPlatformLibrary.Internals
{
    /// <summary>
    /// Linker control attribute.
    /// See: https://docs.microsoft.com/en-us/xamarin/ios/deploy-test/linker
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class PreserveAttribute : Attribute
    {
        /// <summary>
        /// If you want to preserve the whole type, you can use the syntax [Preserve (AllMembers = true)] on the type.
        /// </summary>
        public bool AllMembers;

        /// <summary>
        /// Sometimes you want to preserve certain members, but only if the containing type was preserved. In those cases, use [Preserve (Conditional=true)]
        /// </summary>
        public bool Conditional;

        public PreserveAttribute(bool allMembers, bool conditional)
        {
            this.AllMembers = allMembers;
            this.Conditional = conditional;
        }

        public PreserveAttribute()
        {
        }
    }
}
