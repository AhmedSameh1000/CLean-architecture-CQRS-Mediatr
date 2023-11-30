using FluentValidation;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Feature.Emails.Command.Models;
using SchoolProject.Core.Feature.Emails.DTOs;
using SchoolProject.Core.Resources;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Feature.Emails.Command.Handlers
{
    public class EmailCommandHandler : ResponseHandler, IRequestHandler<SendEmailCommand, Response<string>>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IEmailServices _emailServices;
        private readonly IValidator<SendEmailDTO> _sendEmailValidator;

        public EmailCommandHandler(

            IStringLocalizer<SharedResources> stringLocalizer
            , IEmailServices emailServices
            , IValidator<SendEmailDTO> SendEmailValidator
            ) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _emailServices = emailServices;
            _sendEmailValidator = SendEmailValidator;
        }

        public async Task<Response<string>> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _sendEmailValidator.ValidateAsync(request.SendEmailDTO);
            if (!validationResult.IsValid)
            {
                return BadRequest<string>(_stringLocalizer[SharedSesourcesKeys.BadRequest]);
            }
            var Response = await _emailServices.SendEmail(request.SendEmailDTO.Email, request.SendEmailDTO.Message, request.SendEmailDTO.Subject);

            if (Response)
            {
                return Success<string>(_stringLocalizer[SharedSesourcesKeys.Done]);
            }
            else
            {
                return BadRequest<string>(_stringLocalizer[SharedSesourcesKeys.BadRequest]);
            }
        }
    }
}