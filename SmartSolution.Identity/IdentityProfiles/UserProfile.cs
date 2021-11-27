using AutoMapper;
using SmartSolution.Domain.AggregatesModel.UserAggregate;
using SmartSolution.Identity.ViewModels;

namespace SmartSolution.Identity.IdentityProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserProfileDto>();
            CreateMap<User, UserDto>();
        }
    }
}
