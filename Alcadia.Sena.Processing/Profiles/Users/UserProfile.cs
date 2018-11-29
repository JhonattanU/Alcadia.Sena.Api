using Alcadia.Sena.Models.Users;
using AutoMapper;

namespace Alcadia.Sena.Processing.Profiles.Users
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserView>();
            CreateMap<User, UserAdd>();

            CreateMap<UserView, User>();
            CreateMap<UserAdd, User>();
        }
    }
}
