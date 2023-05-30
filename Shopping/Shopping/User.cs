using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping
{
    class User
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Password { get; set; }

        public string Rols => RoleInstalation();

        public User()
        {
        }

        public User(string firstName, string lastName, string email, string phoneNumber, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            Password = password;
        }

        /// <summary>
        /// Дізнаємося, яку ролі надати користувачу.
        /// </summary>
        /// <returns>Роль користувача.</returns>
        private string RoleInstalation()
        {
            if (Password == "123456")
            {
                return "Admin";
            }
            else
            {
                return "Buyer";
            }
        }
    }
}
