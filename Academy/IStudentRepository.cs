﻿using Academy.Model;

namespace Academy
{
    public interface IStudentRepository
    {
        Task<int> CreateStudentAsync(Student student);
        Task UpdateStudentAsync(Student student);
        Task<Student?> GetStudentByIdAsync(int studentId);      
        Task<List<Student>> GetStudentsAsync();
        Task DeleteStudentAsync(int studentId);

    }
}
