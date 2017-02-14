using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UserServiceLibrary;

namespace UserServiceApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var userService = new UserService();

            userService.Add(new User() { FirstName = "Ivan", LastName = "Ivanov", DateOfBirth = DateTime.Now.Date });
            userService.Add(new User() { FirstName = "Petr", LastName = "Petrov", DateOfBirth = DateTime.Now.Date });
            userService.Add(new User() { FirstName = "Anton", LastName = "Antonov", DateOfBirth = DateTime.Now.Date });

            Console.WriteLine(userService.ToString());

            var userService2 = new UserService(currentId => currentId + 4, 5);

            userService2.Add(new User() { FirstName = "Ivan", LastName = "Ivanov", DateOfBirth = DateTime.Now.Date });
            userService2.Add(new User() { FirstName = "Petr", LastName = "Petrov", DateOfBirth = DateTime.Now.Date });
            userService2.Add(new User() { FirstName = "Anton", LastName = "Antonov", DateOfBirth = DateTime.Now.Date });

            Console.WriteLine(userService2.ToString());

            Console.ReadLine();

        }
    }
}
