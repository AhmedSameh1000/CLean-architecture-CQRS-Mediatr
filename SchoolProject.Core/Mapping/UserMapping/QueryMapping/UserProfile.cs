using SchoolProject.Core.Feature.User.DTOs;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Core.Mapping.UserMapping
{
    public partial class UserProfile
    {
        public void UserQeurymapping()
        {
            CreateMap<User, AddUserDto>().ReverseMap();
        }
    }
}