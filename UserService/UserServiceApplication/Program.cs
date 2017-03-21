using System;
using System.Collections.Generic;
using System.Linq;
using UserServiceLibrary;

namespace UserServiceApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            UserServiceManager userServiceManager = new UserServiceManager();
            (userServiceManager.Master as UserService).LoadState();
            //User user1 = new User() { FirstName = "Ivan", LastName = "Ivanov", Age = 18};
            //User user2 = new User() { FirstName = "Petr", LastName = "Petrov", Age = 21};
            //User user3 = new User() { FirstName = "Anton", LastName = "Antonov", Age = 54};
            //userServiceManager.Master.Add(user1);
            //userServiceManager.Master.Add(user2);
            //userServiceManager.Master.Add(user3);
            List<User> users = userServiceManager.Slaves.ElementAt(0).Search(u => u.FirstName == "Ivan").ToList();
            Console.WriteLine(users.Count);

 //           (userServiceManager.Master as UserService).SaveState();
            
            Console.ReadLine();


        }
    }
}
