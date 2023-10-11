using Academy.Model;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace Academy;

public class ApplicationDbContext : DbContext
{
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Professor> Professors { get; set; }
    public DbSet<Class> Classes { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Filename=Academy.db");
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // define primary key
        modelBuilder.Entity<Address>().HasKey(x => x.Id);
        modelBuilder.Entity<Student>().HasKey(x => x.Id);
        modelBuilder.Entity<Professor>().HasKey(x => x.Id);
        modelBuilder.Entity<Class>().HasKey(x => x.Id);

        // define relations
        modelBuilder.Entity<Student>().HasOne(x => x.Address);
        modelBuilder.Entity<Professor>().HasOne(x => x.Address);

        // one class can have many students and one student can have many classes
        modelBuilder.Entity<Class>().HasMany(x => x.Students).WithMany(x => x.Classes);
        modelBuilder.Entity<Class>().HasOne(x => x.Professor).WithMany(x => x.Classes);

        base.OnModelCreating(modelBuilder);
    }
}
