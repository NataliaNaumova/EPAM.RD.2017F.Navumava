using System;
using System.Collections.Generic;


namespace UserServiceLibrary
{
    [Serializable]
    public class UserServiceStorage
    {
        public ICollection<User> UserCollection { get; set; }
        public int CurrentId { get; set; }
    }
}
