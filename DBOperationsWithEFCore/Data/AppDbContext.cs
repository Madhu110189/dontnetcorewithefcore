using Microsoft.EntityFrameworkCore;

namespace DBOperationsWithEFCore.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        { 

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Language>().HasData(
                new Language() { Id=1, Title="Hindi", Description="Hindi" },
                new Language() { Id=2, Title="English", Description="English" },
                new Language() { Id=3, Title="French", Description="French" },
                new Language() { Id=4, Title="Spanish", Description="Spanish" }
                );

            modelBuilder.Entity<Currency>().HasData(
                new Currency() { Id=1, Title="INR", Description="INR" },
                new Currency() { Id=2, Title="AED", Description="AED" },
                new Currency() { Id=3, Title="Dollar", Description="Dollar" },
                new Currency() { Id=4, Title="Riyal", Description="Riyal"}
                );
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<BookPrice> BooksPrice { get; set; }
        public DbSet<Currency> Currency { get; set; }        
        public DbSet<Author> Authors { get; set; }

    }
}
