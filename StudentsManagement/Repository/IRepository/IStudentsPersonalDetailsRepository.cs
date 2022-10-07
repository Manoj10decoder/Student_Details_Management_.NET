using StudentsManagement.Models;
using System.Collections.Generic;

namespace StudentsManagement.Repository.IRepository
{
    public interface IStudentsPersonalDetailsRepository
    {
        ICollection<StudentsPersonalDetails> GetStudentsPersonalDetails();
        StudentsPersonalDetails GetStudentPersonalDetails(int StudentPersonalDetailId);
        bool StudentPersonalDetailsExists(long? phoneNumber);
        bool StudentPersonalDetailsExists(int id);
        bool CreateStudentPersonalDetails(StudentsPersonalDetails studentDetails);
        bool UpdateStudentPersonalDetails(StudentsPersonalDetails studentDetails);
        bool DeleteStudentPersonalDetails(StudentsPersonalDetails studentDetails);
        bool Save();
    }
}
