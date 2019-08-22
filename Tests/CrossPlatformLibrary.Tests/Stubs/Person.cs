using System;

namespace CrossPlatformLibrary.Tests.Stubs
{
    public class Person : IEquatable<Person>
    {
        public string Name { get; set; }

        public bool Equals(Person other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return string.Equals(this.Name, other.Name, StringComparison.InvariantCultureIgnoreCase);
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

            return this.Equals((Person)obj);
        }

        public override int GetHashCode()
        {
            return (this.Name != null ? StringComparer.InvariantCultureIgnoreCase.GetHashCode(this.Name) : 0);
        }

        public static bool operator ==(Person left, Person right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Person left, Person right)
        {
            return !Equals(left, right);
        }
    }
}