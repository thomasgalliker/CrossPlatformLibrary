using System;

namespace SampleApp.ViewModels
{
    internal class UserDto : IEquatable<UserDto>
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public bool Equals(UserDto other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return this.Id == other.Id &&
                   string.Equals(this.UserName, other.UserName, StringComparison.InvariantCultureIgnoreCase);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return this.Equals((UserDto)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (this.Id * 397) ^
                       (this.UserName != null ? StringComparer.InvariantCultureIgnoreCase.GetHashCode(this.UserName) : 0);
            }
        }

        public static bool operator ==(UserDto left, UserDto right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(UserDto left, UserDto right)
        {
            return !Equals(left, right);
        }
    }
}