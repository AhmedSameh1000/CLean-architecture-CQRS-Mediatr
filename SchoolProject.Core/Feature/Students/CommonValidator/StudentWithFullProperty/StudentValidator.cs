using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Resources;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Feature.Students.CommonValidator.StudentWithFullProperty
{
    public class StudentValidator : AbstractValidator<IStudentProperty>
    {
        private readonly IDepartmentService _departmentService;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        #region Constractor

        public StudentValidator(IDepartmentService departmentService, IStringLocalizer<SharedResources> stringLocalizer)
        {
            _departmentService = departmentService;
            _stringLocalizer = stringLocalizer;
            ApplyValidationRules();
        }

        #endregion Constractor

        #region ActionsValidation

        public void ApplyValidationRules()
        {
            #region BadValidator

            //BadPractice
            //RuleFor(c => c.NameAr)
            //    .NotEmpty().WithMessage(_stringLocalizer[SharedSesourcesKeys.Name] + " " + _stringLocalizer[SharedSesourcesKeys.NotEmpty])
            //    .NotNull().WithMessage(_stringLocalizer[SharedSesourcesKeys.Name] + " " + _stringLocalizer[SharedSesourcesKeys.NotEmpty])
            //    .MaximumLength(10).WithMessage(_stringLocalizer[SharedSesourcesKeys.Name] + " " + _stringLocalizer[SharedSesourcesKeys.LessThan10]);

            //RuleFor(c => c.NameEn)
            //    .NotEmpty().WithMessage(_stringLocalizer[SharedSesourcesKeys.Name] + " " + _stringLocalizer[SharedSesourcesKeys.NotEmpty])
            //    .NotNull().WithMessage(_stringLocalizer[SharedSesourcesKeys.Name] + " " + _stringLocalizer[SharedSesourcesKeys.NotEmpty])
            //    .MaximumLength(10).WithMessage(_stringLocalizer[SharedSesourcesKeys.Name] + " " + _stringLocalizer[SharedSesourcesKeys.LessThan10]);

            #endregion BadValidator

            //Good Practice
            Func<IRuleBuilderInitial<IStudentProperty, string>, IRuleBuilderOptions<IStudentProperty, string>> ApplyNameRules = ruleBuilder => ruleBuilder
                .NotEmpty().WithMessage(_stringLocalizer[SharedSesourcesKeys.Name] + " " + _stringLocalizer[SharedSesourcesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedSesourcesKeys.Name] + " " + _stringLocalizer[SharedSesourcesKeys.NotEmpty])
                .MaximumLength(10).WithMessage(_stringLocalizer[SharedSesourcesKeys.Name] + " " + _stringLocalizer[SharedSesourcesKeys.LessThan10]);

            ApplyNameRules(RuleFor(c => c.NameAr));
            ApplyNameRules(RuleFor(c => c.NameEn));

            RuleFor(c => c.Address)
                .NotEmpty().WithMessage(_stringLocalizer[SharedSesourcesKeys.Address] + " " + _stringLocalizer[SharedSesourcesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedSesourcesKeys.Address] + " " + _stringLocalizer[SharedSesourcesKeys.NotEmpty])
                .MaximumLength(10).WithMessage(_stringLocalizer[SharedSesourcesKeys.Address] + " " + _stringLocalizer[SharedSesourcesKeys.LessThan10]);

            RuleFor(c => c.DepartmentId)
            .MustAsync(async (Id, CancellationToken) =>
               await _departmentService.IsExist(Id)
            ).WithMessage(_stringLocalizer[SharedSesourcesKeys.Department] + " " + _stringLocalizer[SharedSesourcesKeys.NotFound]);
        }

        #endregion ActionsValidation
    }
}