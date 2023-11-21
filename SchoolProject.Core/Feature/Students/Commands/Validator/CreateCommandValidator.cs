using FluentValidation;
using SchoolProject.Core.Feature.Students.CommonValidator.StudentWithFullProperty;
using SchoolProject.Core.Feature.Students.DTOs;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Feature.Students.Commands.Validator
{
    public class CreateCommandValidator : AbstractValidator<CreateStudentDTO>
    {
        private readonly IDepartmentService _departmentService;

        public CreateCommandValidator(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
            Include(new StudentValidator(_departmentService));
        }
    }
}