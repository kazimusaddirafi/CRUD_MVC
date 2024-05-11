using Microsoft.EntityFrameworkCore;

namespace CRUD_MVC.Models
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options)
        {
            
        }

        public DbSet<StudentModel> Students { get; set; }

    }
}
