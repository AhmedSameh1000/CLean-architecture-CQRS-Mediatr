using System.ComponentModel.DataAnnotations;

namespace JWTApi.Models
{
    public class RegisterModel
    {
        public string FullName { get; set; }

        [StringLength(100)]
        public string UserName { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        public string Password { get; set; }
    }
}