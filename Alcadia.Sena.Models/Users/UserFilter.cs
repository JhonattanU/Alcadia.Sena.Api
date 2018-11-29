using System;
using System.Collections.Generic;

namespace Alcadia.Sena.Models.Users
{
    public class UserFilter : IFilter
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<OrderType> OrderBy { get; set; }
    }
}
