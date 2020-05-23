using Microsoft.AspNet.Identity.EntityFramework;

namespace SAMS.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        public System.Data.Entity.DbSet<SAMS.Models.AttendanceMaster> AttendanceMasters { get; set; }

        public System.Data.Entity.DbSet<SAMS.Models.AttendanceDetail> AttendanceDetails { get; set; }

        public System.Data.Entity.DbSet<SAMS.Models.Module> Modules { get; set; }

        public System.Data.Entity.DbSet<SAMS.Models.Student> Students { get; set; }
    }
}