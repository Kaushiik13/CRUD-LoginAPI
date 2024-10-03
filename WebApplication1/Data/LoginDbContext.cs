using Microsoft.EntityFrameworkCore;
using WebApplication1.Model;

namespace WebApplication1.Data
{
    public class LoginDbContext : DbContext
    {
        public LoginDbContext(DbContextOptions dbContextOptions): base(dbContextOptions) 
        {
                
        }
        public DbSet<Login> Logins { get; set; }
        public DbSet<UploadedFile> UploadedFiles { get; set; }
    }
}
