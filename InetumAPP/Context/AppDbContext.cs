using InetumAPP.Models;
using Microsoft.EntityFrameworkCore;

namespace InetumAPP.Context
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }

        public DbSet <User> Users { get; set; } //Communicate with the database

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("users"); //Take the enity and send it to the table in sql 
        }
    }
}
