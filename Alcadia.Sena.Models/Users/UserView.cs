using System;

namespace Alcadia.Sena.Models.Users
{
    public class UserView
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
