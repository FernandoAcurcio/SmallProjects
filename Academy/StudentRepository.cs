using Academy.Model;
using Microsoft.EntityFrameworkCore;

namespace Academy
{
    public class StudentRepository : IStudentRepository
    {
        // Private field to hold a reference to the database context
        private ApplicationDbContext _dbContext { get; }

        // Constructor that initializes the database context
        public StudentRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Method to create a new student record asynchronously
        public async Task<int> CreateStudentAsync(Student student)
        {
            _dbContext.Students.Add(student); // Add the student entity to the context
            await _dbContext.SaveChangesAsync(); // Save changes to the database
            return student.Id; // Return the ID of the newly created student
        }

        // Method to delete a student record by ID asynchronously
        public async Task DeleteStudentAsync(int studentId)
        {
            // Attempt to find a student with the specified ID
            var student = await _dbContext.Students.FindAsync(studentId);

            if (student != null)
            {
                _dbContext.Students.Remove(student); // Remove the found student
                await _dbContext.SaveChangesAsync(); // Save changes to the database
            }
            else
            {
                // If the student is not found, throw an exception
                throw new InvalidOperationException("Id not found");
            }
        }

        // Method to get a student by ID asynchronously
        public async Task<Student?> GetStudentByIdAsync(int studentId)
        {
            return await _dbContext.Students.FindAsync(studentId); // Return the student entity if found, or null if not found
        }

        // Method to get a list of all students asynchronously
        public async Task<List<Student>> GetStudentsAsync()
        {
            return await _dbContext.Students.ToListAsync(); // Return a list of all students in the database
        }

        // Method to update a student's information asynchronously
        public async Task UpdateStudentAsync(Student student)
        {
            _dbContext.Students.Attach(student); // Attach the student entity to the context
            _dbContext.Entry(student).State = EntityState.Modified; // Mark the entity as modified
            await _dbContext.SaveChangesAsync(); // Save changes to the database
        }
    }

}
