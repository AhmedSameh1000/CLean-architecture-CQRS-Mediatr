using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Feature.Students.CommonValidator.StudentWithFullProperty;
using SchoolProject.Core.Feature.Students.DTOs;
using SchoolProject.Core.Resources;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Feature.Students.Commands.Validator
{
    public class UpdateStudentCommandValidator : AbstractValidator<UpdateStudentDTO>
    {
        private readonly IDepartmentService _departmentService;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public UpdateStudentCommandValidator(IDepartmentService departmentService,

            IStringLocalizer<SharedResources> stringLocalizer)
        {
            _departmentService = departmentService;
            _stringLocalizer = stringLocalizer;
            Include(new StudentValidator(_departmentService, _stringLocalizer));

            RuleFor(c => c.Id).NotEmpty().WithMessage(_stringLocalizer[SharedSesourcesKeys.NotEmpty]);
        }
    }
}