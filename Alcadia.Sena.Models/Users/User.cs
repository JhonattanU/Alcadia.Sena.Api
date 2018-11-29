using Dapper.Contrib.Extensions;

namespace Alcadia.Sena.Models.Users
{
    [Table("dbo.Users")]
    public class User : Audit
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
