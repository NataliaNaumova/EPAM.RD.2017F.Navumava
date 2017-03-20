using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UserServiceLibrary
{
    [Serializable]
    public class StorageUpdateNotification
    {
        public User User { get; set; }
        public UpdateType UpdateType { get; set; }
    }

    public enum UpdateType
    {
        Add = 0,
        Delete = 1
    }
}
