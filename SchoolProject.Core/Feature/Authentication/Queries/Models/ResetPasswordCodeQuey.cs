using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Feature.Authentication.DTOs;

namespace SchoolProject.Core.Feature.Authentication.Queries.Models
{
    public class ResetPasswordCodeQuey : IRequest<Response<string>>
    {
        public ResetPasswordCodeDTO ResetCode { get; set; }
    }
}