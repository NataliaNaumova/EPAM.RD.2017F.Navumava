﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UserServiceLibrary
{
    public class User
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public override string ToString()
        {
            return string.Format("Id: {0}, FirstName: {1}, LastName: {2}, DateOfBirth: {3}", Id, FirstName, LastName, DateOfBirth);
        }
    }
}
