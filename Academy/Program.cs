// What to do to install and migrate databases
// Installin EF Core tools: tool install --global dotnet-ef
// Creating migrations: dotnet ef migrations add InitialMigration

using Academy;
using Academy.Model;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlite().Options;
        var dbContext = new ApplicationDbContext(options);

        // create the database
        //dbContext.Database.Migrate();

        ProcessDelete();
        ProcessInsert();
        ProcessSelect();
        ProcessUpdate();

        void ProcessInsert()
        {
            dbContext = new ApplicationDbContext(options);

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
                LastName = "Malcolm",
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

        void ProcessDelete()
        {
            // Load all the professors from database
            var professors = dbContext.Professors.ToList();
            var students = dbContext.Students.ToList();
            var classes = dbContext.Classes.ToList();
            var addresses = dbContext.Addresses.ToList();

            dbContext.Professors.RemoveRange(professors);
            dbContext.Students.RemoveRange(students);
            dbContext.Classes.RemoveRange(classes);
            dbContext.Addresses.RemoveRange(addresses);

            dbContext.SaveChanges();
            dbContext.Dispose();
        }

        void ProcessSelect()
        {
            dbContext = new ApplicationDbContext(options);
            var professor = dbContext.Professors.Single(x => x.FirstName == "John");

            var student = dbContext.Students.Include(x => x.Address).Where(x => x.FirstName == "Maria").ToList();

            dbContext.Dispose();
        }

        void ProcessUpdate()
        {
            dbContext = new ApplicationDbContext(options);

            var student = dbContext.Students.First();
            student.FirstName = "Timmy";
            dbContext.SaveChanges();
            dbContext.Dispose();

            dbContext = new ApplicationDbContext(options);
            student = dbContext.Students.First();
            student.FirstName = "John";
            dbContext.SaveChanges();
            dbContext.Dispose();

            dbContext = new ApplicationDbContext(options);
            var studentUntracked = new Student()
            {
                Id = student.Id,
                FirstName = "Dennis",
                LastName = "Williams"
            };
            dbContext.Students.Attach(studentUntracked);
            dbContext.Students.Entry(studentUntracked).State = EntityState.Modified;
            dbContext.SaveChanges();
            dbContext.Dispose();

            dbContext = new ApplicationDbContext(options);
            student = dbContext.Students.First();


            Console.ReadLine();
        }
    }
}