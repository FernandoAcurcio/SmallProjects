// What to do to install and migrate databases
// Installin EF Core tools: tool install --global dotnet-ef
// Creating migrations: dotnet ef migrations add InitialMigration

using Academy;
using Microsoft.EntityFrameworkCore;

var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlite().Options;
var dbContext = new ApplicationDbContext(options);

// create the database
dbContext.Database.Migrate();


