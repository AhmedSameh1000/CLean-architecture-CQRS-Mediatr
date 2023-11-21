using FluentValidation;
using SchoolProject.Core.Feature.Students.CommonValidator.StudentWithFullProperty;
using SchoolProject.Core.Feature.Students.DTOs;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Feature.Students.Commands.Validator
{
    public class UpdateStudentCommandValidator : AbstractValidator<UpdateStudentDTO>
    {
        private readonly IDepartmentService _departmentService;

        public UpdateStudentCommandValidator(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
            Include(new StudentValidator(_departmentService));

            RuleFor(c => c.Id).NotEmpty().WithMessage("{PropertyName} Must Not Be Empty");
        }
    }
}