using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace UserServiceLibrary
{
    public class UserService
    {
        private List<User> _users = new List<User>();

        private int _currentId;

        private Func<int, int> _generateNextIdFunction;

        public int UsersCount
        {
            get { return _users.Count; }
        }

        public UserService()
        {
            _currentId = 0;
            _generateNextIdFunction = currentId => ++currentId;
        }

        public UserService(Func<int, int> generateNextIdFunction, int seed = 0)
        {
            if (generateNextIdFunction == null)
            {
                throw new ArgumentNullException("generateNextIdFunction");
            }

            _generateNextIdFunction = generateNextIdFunction;
            _currentId = seed;
        }

         public void Add(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            _currentId = _generateNextIdFunction(_currentId);
            user.Id = _currentId;

            _users.Add(user);
        }

        public void Delete(int userId)
        {
            _users.RemoveAll(u => u.Id == userId);
        }

        public IEnumerable<User> SearchByFirstName(string firstName)
        {
            if (firstName == null)
            {
                throw new ArgumentNullException("firstName");
            }

            return _users.FindAll(u => u.FirstName == firstName);
        }

        public IEnumerable<User> SearchByLastName(string lastname)
        {
            if (lastname == null)
            {
                throw new ArgumentNullException("lastname");
            }

            return _users.FindAll(u => u.LastName == lastname);
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            foreach (var user in _users)
            {
                stringBuilder.AppendLine(user.ToString());
            }

            return stringBuilder.ToString();
        }


    }
}
