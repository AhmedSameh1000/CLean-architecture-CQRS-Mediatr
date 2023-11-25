using SchoolProject.Core.Feature.User.DTOs;
using SchoolProject.Core.Feature.User.Queries.Result;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Core.Mapping.UserMapping
{
    public partial class UserProfile
    {
        public void UserQeurymapping()
        {
            CreateMap<User, AddUserDto>().ReverseMap();
            CreateMap<User, GetUserResponse>().ReverseMap();
        }
    }
}