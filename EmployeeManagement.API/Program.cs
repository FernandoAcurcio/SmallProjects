using EmployeeManagement.API;
using EmployeeManagement.Business;
using EmployeeManagement.Common.Interfaces;
using EmployeeManagement.Common.Model;
using EmployeeManagement.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Diagnostics;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        DIConfiguration.RegisterServices(builder.Services);
        var dbFilename = Environment.GetEnvironmentVariable("DB_FILENAME");
        builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite($"Filename={dbFilename}"));
        builder.Services.AddScoped<IGenericRepository<Address>, GenericRepository<Address>>();  
        builder.Services.AddScoped<IGenericRepository<Job>, GenericRepository<Job>>();  
        builder.Services.AddScoped<IGenericRepository<Employee>, GenericRepository<Employee>>();  
        builder.Services.AddScoped<IGenericRepository<Team>, GenericRepository<Team>>();  
        
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // this needs to be after app.build()
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            dbContext.Database.EnsureCreated();
        }

        app.UseMiddleware<ExceptionMiddleware>();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}