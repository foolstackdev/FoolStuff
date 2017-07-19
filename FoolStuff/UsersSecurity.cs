using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FoolStaffDataAccess;

namespace FoolStuff
{
    public class UsersSecurity
    {
        public static bool Login(string username, string password)
        {
            using(FoolStaffDataModelContainer entities = new FoolStaffDataModelContainer())
            {
                return entities.Users.Any(user => user.EMAIL.Equals(username, StringComparison.OrdinalIgnoreCase) && user.PASSWORD == password);
            }
        }


    }
}