// What to do to install and migrate databases
// Installin EF Core tools: tool install --global dotnet-ef
// Creating migrations: dotnet ef migrations add InitialMigration

using Academy;
using Academy.Model;
using Microsoft.EntityFrameworkCore;

var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlite().Options;
var dbContext = new ApplicationDbContext(options);

// create the database
//dbContext.Database.Migrate();

ProcessInsert();

void ProcessInsert()
{
    var address = new Address()
    {
        City = "Braga",
        Street = "Rua nº2",
        Zip = "4700",
        HouseNumber = 2
    };
    var professor = new Professor()
    {
        FirstName = "John",
        LastName = "Smith",
        Address = address,
    };
    var student1 = new Student()
    {
        FirstName = "Peter",
        LastName = "Parker",
        Address = address,
    };
    var student2 = new Student()
    {
        FirstName = "Maria",
        LastName = "Carey",
        Address = address,
    };
    var class1 = new Class()
    {
        Professor = professor,
        Students = new List<Student> { student1, student2 },
        Title = "IT Class"
    };

    dbContext.Addresses.Add(address);
    dbContext.Professors.Add(professor);
    dbContext.Students.Add(student1);   
    dbContext.Students.Add(student2);
    dbContext.Classes.Add(class1);

    dbContext.SaveChanges();
    dbContext.Dispose();
}
