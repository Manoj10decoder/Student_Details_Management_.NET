using StudentsManagement.Models;
using System.Collections.Generic;

namespace StudentsManagement.Repository.IRepository
{
    public interface IStudentsAddressRepository
    {
        ICollection<StudentsAddress> GetStudentsAddress();
        StudentsAddress GetStudentAddress(int studentAddressId);
        bool StudentAddressExists(string address);
        bool StudentAddressExists(int id);
        bool CreateStudentAddress(StudentsAddress studentAddress);
        bool UpdateStudentAddress(StudentsAddress studentAddress);
        bool DeleteStudentAddress(StudentsAddress studentAddress);
        bool Save();
    }
}
