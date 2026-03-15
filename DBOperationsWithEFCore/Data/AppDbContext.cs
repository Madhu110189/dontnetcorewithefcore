using Microsoft.EntityFrameworkCore;

namespace DBOperationsWithEFCore.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        { 

        }
    }
}
