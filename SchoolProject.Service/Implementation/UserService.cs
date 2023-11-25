using Microsoft.AspNetCore.Identity;
using SchoolProject.Core.Wrappers;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Infrustructure.Abstracts;
using SchoolProject.Service.Abstracts;
using X.PagedList;

namespace SchoolProject.Service.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;

        public UserService(IUserRepository userRepository, UserManager<User> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

        public int GetCount()
        {
            return _userRepository.Count();
        }

        public async Task<IPagedList<User>> GetUsersAsync(RequestParams requestParams)
        {
            var user = await _userRepository.GetAllAsTracking(requestParams);
            return user;
        }
    }
}