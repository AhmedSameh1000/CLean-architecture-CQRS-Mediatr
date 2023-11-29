using SchoolProject.Core.Feature.Authorization.CommonValidator.CommonRoleValidator;

namespace SchoolProject.Core.Feature.Authorization.DTOs
{
    public class AddRoleDto : IRolePropertiesValidation
    {
        public string Name { get; set; }
    }
}