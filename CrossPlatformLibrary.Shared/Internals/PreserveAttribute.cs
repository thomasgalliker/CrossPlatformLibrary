using System;
using System.ComponentModel;

namespace CrossPlatformLibrary.Internals
{
    [AttributeUsage(AttributeTargets.All)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class PreserveAttribute : Attribute
    {
        /// <summary>For internal use by platform renderers.</summary>
        /// <remarks>To be added.</remarks>
        public bool AllMembers;
        /// <summary>For internal use by platform renderers.</summary>
        /// <remarks>To be added.</remarks>
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
