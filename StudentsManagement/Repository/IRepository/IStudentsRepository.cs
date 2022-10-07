using StudentsManagement.Models;
using System.Collections.Generic;

namespace StudentsManagement.Repository.IRepository
{
    public interface IStudentsRepository
    {
        ICollection<Students> GetStudents();
        Students GetStudent(int StudetnId);
        bool StudentExists(string name);
        bool StudentExists(int id);
        bool StudentExists(long? registerNumber);
        bool StudentExistAadhaar(long? aadhaarNumber);
        bool CreateStudent(Students student);
        bool UpdateStudent(Students student);
        bool DeleteStudent(Students student);
        public ICollection<Students> GetStudentPersonalDetailAndAddress(int personalDetailId);
        bool Save();
    }
}
