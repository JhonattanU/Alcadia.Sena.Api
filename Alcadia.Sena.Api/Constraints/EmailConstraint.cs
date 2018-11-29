using Microsoft.AspNetCore.Routing.Constraints;

namespace Alcadia.Sena.Api.Constraints
{
    public class EmailConstraint : RegexRouteConstraint
    {
        public EmailConstraint() : base(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$")
        {

        }
    }
}
