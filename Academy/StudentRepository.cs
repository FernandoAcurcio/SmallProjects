using Academy.Model;
using Microsoft.EntityFrameworkCore;

namespace Academy
{
    public class StudentRepository : IStudentRepository
    {
        private ApplicationDbContext _dbContext { get; }
        public StudentRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CreateStudentAsync(Student student)
        {
            _dbContext.Students.Add(student);   
            await _dbContext.SaveChangesAsync();
            return student.Id;
        }

        public async Task DeleteStudentAsync(int studentId)
        {
            var student = await _dbContext.Students.FindAsync(studentId);   

            if (student != null)
            {
                _dbContext.Students.Remove(student);
                await _dbContext.SaveChangesAsync();
            }

            throw new InvalidOperationException("Id not found");
        }

        public async Task<Student?> GetStudentByIdAsync(int studentId)
        {
            return await _dbContext.Students.FindAsync(studentId);
        }

        public async Task<List<Student>> GetStudentsAsync()
        {
            return await _dbContext.Students.ToListAsync();
        }

        public async Task UpdateStudentAsync(Student student)
        {
            _dbContext.Students.Attach(student);
            _dbContext.Entry(student).State = EntityState.Modified; 
            await _dbContext.SaveChangesAsync();
        }
    }
}
