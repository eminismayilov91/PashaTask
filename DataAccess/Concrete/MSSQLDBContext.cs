using Entity.DAO;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete
{
    public class MSSQLDBContext: DbContext
    {
        public MSSQLDBContext(DbContextOptions<MSSQLDBContext> options) : base(options)
        {
        }

        public virtual DbSet<EmployeeDAO>? Employees { get; set; }
        public virtual DbSet<DepartmentDAO>? Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeDAO>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(100).IsUnicode(false);
                entity.Property(e => e.Surname).HasMaxLength(100).IsUnicode(false);
                
            });

            modelBuilder.Entity<DepartmentDAO>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(100).IsUnicode(false);
            });
        }
    }
}
