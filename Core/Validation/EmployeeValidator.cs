using Entity.DTO;
using FluentValidation;

namespace Core.Validation
{
    public class EmployeeValidator : AbstractValidator<EmployeeDTO>
    {
        public EmployeeValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Name).Length(0, 100);
        }
    }
}
