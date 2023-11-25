using SchoolProject.Core.Feature.User.DTOs;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Core.Mapping.UserMapping
{
    public partial class UserProfile
    {
        public void UserCommandmapping()
        {
            CreateMap<User, UpdateUserDto>().ReverseMap();
        }
    }
}