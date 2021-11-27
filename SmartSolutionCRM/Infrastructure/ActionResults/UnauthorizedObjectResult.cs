using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SmartSolutionCRM.Infrastructure.ActionResults
{
    public class UnauthorizedObjectResult : ObjectResult
    {
        public UnauthorizedObjectResult(object error) : base(error)
        {
            StatusCode = StatusCodes.Status401Unauthorized;
        }
    }
}
