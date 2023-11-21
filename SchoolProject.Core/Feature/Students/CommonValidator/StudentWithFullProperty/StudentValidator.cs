using FluentValidation;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Feature.Students.CommonValidator.StudentWithFullProperty
{
    public class StudentValidator : AbstractValidator<IStudentProperty>
    {
        private readonly IDepartmentService _departmentService;

        #region Constractor

        public StudentValidator(IDepartmentService departmentService)
        {
            ApplyValidationRules();
            _departmentService = departmentService;
        }

        #endregion Constractor

        #region ActionsValidation

        public void ApplyValidationRules()
        {
            RuleFor(c => c.Name)

                .NotEmpty().WithMessage("Name Must Not Be Empty")
                .NotNull().WithMessage("Name Must Not Be Null")
                .MaximumLength(10).WithMessage("Name Must Not Be More than 10");
            RuleFor(c => c.Address)
                .NotEmpty().WithMessage("Address Must Not Be Empty")
                .NotNull().WithMessage("Address Must Not Be Null")
                .MaximumLength(10).WithMessage("{PropertyName} Must Not Be More than 10");

            RuleFor(c => c.DepartmentId)
                .MustAsync(async (Id, CancellationToken) =>
                   await _departmentService.IsExist(Id)
                ).WithMessage("Department with this Id Not Found");
        }

        #endregion ActionsValidation
    }
}