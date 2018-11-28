using System.Collections.Generic;

namespace Alcadia.Sena.Models
{
    public interface IFilter
    {
        int PageNumber { get; set; }
        int PageSize { get; set; }
        IEnumerable<OrderType> OrderBy { get; set; }
    }
}
