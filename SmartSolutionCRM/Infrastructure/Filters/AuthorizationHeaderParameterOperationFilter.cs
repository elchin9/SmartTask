using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;

namespace SmartSolutionCRM.Infrastructure.Filters
{
    public class AuthorizationHeaderParameterOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var filters = context.ApiDescription.ActionDescriptor.FilterDescriptors.Select(filterInfo => filterInfo.Filter).ToList();

            var isAuthorized = filters.Any(filter => filter is AuthorizeFilter);

            var allowAnonymous = filters.Any(filter => filter is IAllowAnonymousFilter);

            if (!isAuthorized || allowAnonymous) return;

            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "Authorization",
                In = ParameterLocation.Header,
                Description = "Access token",
                Required = true,
            });
        }
    }
}
