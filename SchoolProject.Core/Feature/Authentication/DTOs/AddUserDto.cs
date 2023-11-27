using SchoolProject.Core.Feature.User.CommonValidator.UserWithFullProperty;

namespace SchoolProject.Core.Feature.User.DTOs
{
    public class AddUserDto : IUserProperty
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
        public string? PhoneNumber { get; set; }

        public string Password { get; set; }

        //[Compare("Password",ErrorMessage ="Not Equals")]
        public string ConfirmPassword { get; set; }
    }
}