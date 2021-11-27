using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SmartSolution.Domain.Exceptions;
using SmartSolutionCRM.Infrastructure.ActionResults;
using System;
using System.Linq;
using System.Security.Authentication;

namespace SmartSolutionCRM.Infrastructure.ErrorHandling
{
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;
        private readonly ILogger<HttpGlobalExceptionFilter> _logger;

        public HttpGlobalExceptionFilter(IWebHostEnvironment env, IMapper mapper, ILogger<HttpGlobalExceptionFilter> logger)
        {
            _env = env ?? throw new ArgumentNullException(nameof(env));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public IWebHostEnvironment Env => _env;

        public IMapper Mapper => _mapper;

        public ILogger<HttpGlobalExceptionFilter> Logger => _logger;

        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            var exceptionType = exception.GetType();

            _logger.LogError(new EventId(exception.HResult), exception, exception.Message);

            var developerException = _env.IsProduction() ? null : _mapper.Map<DeveloperException>(exception);

            switch (exceptionType)
            {
                case var _ when exceptionType == typeof(AuthenticationException):
                    {

                        var json = new JsonErrorResponse(nameof(AuthenticationException), exception.Message, developerException);

                        context.Result = new Microsoft.AspNetCore.Mvc.UnauthorizedObjectResult(json);
                        context.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        break;
                    }
                case var _ when exceptionType == typeof(UnauthorizedAccessException):
                    {

                        var json = new JsonErrorResponse(nameof(UnauthorizedAccessException), exception.Message, developerException);

                        context.Result = new Microsoft.AspNetCore.Mvc.UnauthorizedObjectResult(json);
                        context.HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                        break;
                    }
                case var _ when exceptionType == typeof(DomainException):
                    {
                        var json = new JsonErrorResponse(nameof(DomainException), exception.Message, developerException);

                        context.Result = new BadRequestObjectResult(json);
                        context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                        break;
                    }

                case var _ when exceptionType == typeof(ValidationException):
                    {
                        var json = new JsonValidationErrorResponse(nameof(ValidationException), exception.Message, developerException);

                        json.Errors.AddRange(((ValidationException)exception).Errors.Select(e =>
                            new ValidationError(e.PropertyName, e.ErrorMessage)));

                        context.Result = new BadRequestObjectResult(json);
                        context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                        break;
                    }

                default:
                    {
                        var json = new JsonErrorResponse(nameof(Exception), "An error occured. Please contact administrator", developerException);

                        context.Result = new InternalServerErrorObjectResult(json);
                        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                        break;
                    }

            }

            context.ExceptionHandled = true;
        }
    }
}
