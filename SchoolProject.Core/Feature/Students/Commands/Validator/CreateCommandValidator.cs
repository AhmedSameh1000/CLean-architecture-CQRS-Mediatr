using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Feature.Students.CommonValidator.StudentWithFullProperty;
using SchoolProject.Core.Feature.Students.DTOs;
using SchoolProject.Core.Resources;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Feature.Students.Commands.Validator
{
    public class CreateCommandValidator : AbstractValidator<CreateStudentDTO>
    {
        private readonly IDepartmentService _departmentService;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public CreateCommandValidator(IDepartmentService departmentService, IStringLocalizer<SharedResources> stringLocalizer)
        {
            _departmentService = departmentService;
            _stringLocalizer = stringLocalizer;
            Include(new StudentValidator(_departmentService, _stringLocalizer));
        }
    }
}