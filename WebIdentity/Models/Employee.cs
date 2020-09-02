using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebIdentity.Models
{
    public class Employee
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }

        public Employee()
        {

        }

        public Employee(ApplicationUser user)
        {
            Id = user.Id;
            Email = user.Email;
            UserName = user.UserName;
        }
    }
}