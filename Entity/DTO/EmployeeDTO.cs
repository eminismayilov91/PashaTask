using System;

namespace Entity.DTO
{
    public class EmployeeDTO : BaseDTO
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public Guid DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
    }
}
