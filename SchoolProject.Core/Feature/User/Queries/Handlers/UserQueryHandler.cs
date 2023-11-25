using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Feature.User.Queries.Models;
using SchoolProject.Core.Feature.User.Queries.Result;
using SchoolProject.Core.Resources;
using SchoolProject.Service.Abstracts;
using UserEntity = SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Core.Feature.User.Queries.Handlers
{
    public class UserQueryHandler : ResponseHandler,
        IRequestHandler<GetusersListQuery, Response<List<GetUserResponse>>>,
        IRequestHandler<GetuserByIdQuery, Response<GetUserResponse>>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly UserManager<UserEntity.User> _userManager;

        public UserQueryHandler(
            IStringLocalizer<SharedResources> stringLocalizer,
            IMapper mapper,
            IUserService userService,
            UserManager<UserEntity.User> userManager) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _mapper = mapper;
            _userService = userService;
            _userManager = userManager;
        }

        public async Task<Response<List<GetUserResponse>>> Handle(GetusersListQuery request, CancellationToken cancellationToken)
        {
            if (request.RequestParams.PageNumber == 0)
                request.RequestParams.PageNumber = 1;

            var users = await _userService.GetUsersAsync(request.RequestParams);

            var UsersToReturn = _mapper.Map<List<GetUserResponse>>(users);

            var PageCount = _userService.GetCount() / request.RequestParams.PageSize;
            return Success(UsersToReturn, PageCount);
        }

        public async Task<Response<GetUserResponse>> Handle(GetuserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            if (user is null)
            {
                return BadRequest<GetUserResponse>(_stringLocalizer[SharedSesourcesKeys.NotFound]);
            }
            var UserToReturn = _mapper.Map<GetUserResponse>(user);

            return Success(UserToReturn);
        }
    }
}