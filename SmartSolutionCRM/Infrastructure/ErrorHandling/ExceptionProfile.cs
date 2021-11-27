using AutoMapper;
using System;
using System.ComponentModel.DataAnnotations;

namespace SmartSolutionCRM.Infrastructure.ErrorHandling
{
    public class ExceptionProfile : Profile
    {
        public ExceptionProfile()
        {
            CreateMap<ValidationException, DeveloperException>();

            CreateMap<Exception, DeveloperException>();
        }
    }
}
