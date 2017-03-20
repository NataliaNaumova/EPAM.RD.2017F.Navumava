using System.Collections.Generic;

namespace UserServiceLibrary.Interfaces
{
    public interface IUserServiceStorageSerializer
    {
        void Serialize(UserServiceStorage usesServiceStorage);
        UserServiceStorage Deserialize();
    }
}
