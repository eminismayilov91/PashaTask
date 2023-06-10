using Entity.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Validation
{
    public class DepartmentValidator : AbstractValidator<DepartmentDTO>
    {
        public DepartmentValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Name).Length(0, 100);
        }
    }
}
