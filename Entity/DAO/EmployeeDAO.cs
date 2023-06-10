using System;

namespace Entity.DAO
{
    public class EmployeeDAO : BaseDAO
    {
        public Guid DepartmentId { get; set; }
        public DepartmentDAO Department { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
