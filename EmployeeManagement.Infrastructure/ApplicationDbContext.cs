using EmployeeManagement.Common.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Job> Jobs { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=Company.db");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Address>().HasKey(x => x.Id);
            builder.Entity<Employee>().HasKey(x => x.Id);
            builder.Entity<Team>().HasKey(x => x.Id);
            builder.Entity<Job>().HasKey(x => x.Id);

            builder.Entity<Employee>().HasOne(x => x.Address);
            builder.Entity<Employee>().HasOne(x => x.Job);

            builder.Entity<Team>().HasMany(x => x.Employees).WithMany(x => x.Teams);

            base.OnModelCreating(builder);
        }

    }
}