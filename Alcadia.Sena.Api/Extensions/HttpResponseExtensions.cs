using Alcadia.Sena.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Alcadia.Sena.Api.Extensions
{
    public static class HttpResponseExtensions
    {
        public static IEnumerable<T> GetPagedResponse<T>(this HttpResponse response, (Pagination pagination, IEnumerable<T> elements) result)
        {
            response.Headers.Add("X-Pagination", result.pagination.Serialize());
            return result.elements;
        }
    }
}
