using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UserServiceLibrary
{
    [Serializable]
    public class User : ICloneable, IEquatable<User>
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public override string ToString()
        {
            return string.Format("Id: {0}, FirstName: {1}, LastName: {2}, Age: {3}", Id, FirstName, LastName, Age);
        }

        public bool Equals(User other)
        {
            return Id == other.Id && string.Equals(FirstName, other.FirstName) && string.Equals(LastName, other.LastName) && Age.Equals(other.Age);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((User) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id;
                hashCode = (hashCode*397) ^ (FirstName != null ? FirstName.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (LastName != null ? LastName.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ Age.GetHashCode();
                return hashCode;
            }
        }

        public User Clone()
            => new User
            {
                Age = Age,
                FirstName = this.FirstName,
                Id = this.Id,
                LastName = this.LastName
            };

        object ICloneable.Clone()
        {
            return Clone();
        }
    }
}
