using System;

namespace Entity.Filter
{
    public class EmployeeFilter
    {
        public Guid? Id { get; set; }
        public Guid? DepartmentId { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
    }
}
