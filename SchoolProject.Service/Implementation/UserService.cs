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

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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