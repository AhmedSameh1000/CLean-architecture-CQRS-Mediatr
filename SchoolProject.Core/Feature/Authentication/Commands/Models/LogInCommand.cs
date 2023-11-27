using JWTApi.Models;
using MediatR;
using SchoolProject.Core.Bases;
using TestApiJWT.Models;

namespace SchoolProject.Core.Feature.Authentication.Commands.Models
{
    public class LogInCommand : IRequest<Response<AuthModel>>
    {
        public LogInModel model { get; set; }
    }
}