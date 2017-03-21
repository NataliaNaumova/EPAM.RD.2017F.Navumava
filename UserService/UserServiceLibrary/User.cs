using System;

namespace UserServiceLibrary
{
    /// <summary>
    /// Class providing user data.
    /// </summary>
    [Serializable]
    public class User : ICloneable, IEquatable<User>
    {
        #region Properties

        /// <summary>
        /// User id in a service storage.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// User's first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// User's last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// User's age.
        /// </summary>
        public int Age { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Returns string representation of user data.
        /// </summary>
        /// <returns>String representation of user data.</returns>
        public override string ToString()
        {
            return string.Format("Id: {0}, FirstName: {1}, LastName: {2}, Age: {3}", Id, FirstName, LastName, Age);
        }

        /// <summary>
        /// Checks if instances of the User class are equal.
        /// </summary>
        /// <param name="other">An instance of the User class to compare this instance with.</param>
        /// <returns>Value indicating whether instances of the User class are equal.</returns>
        public bool Equals(User other)
        {
            return Id == other.Id && string.Equals(FirstName, other.FirstName) && string.Equals(LastName, other.LastName) && Age.Equals(other.Age);
        }

        /// <summary>
        /// Checks if instances are equal.
        /// </summary>
        /// <param name="obj">An object to compare this instance with.</param>
        /// <returns>Value indicating whether instances are equal.</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((User)obj);
        }

        /// <summary>
        /// Evaluates hash code for current instance.
        /// </summary>
        /// <returns>Hash code for current instance.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id;
                hashCode = (hashCode * 397) ^ (FirstName != null ? FirstName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (LastName != null ? LastName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Age.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// Clones current User instance.
        /// </summary>
        /// <returns>Cloned instance.</returns>
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

        #endregion


    }
}
