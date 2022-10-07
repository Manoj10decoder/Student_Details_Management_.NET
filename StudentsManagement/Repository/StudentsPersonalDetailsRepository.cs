using StudentsManagement.Data;
using StudentsManagement.Models;
using StudentsManagement.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace StudentsManagement.Repository
{
    public class StudentsPersonalDetailsRepository : IStudentsPersonalDetailsRepository
    {
        private readonly ApplicationDbContext _db;

        public StudentsPersonalDetailsRepository(ApplicationDbContext db)
        {
            _db = db;
        }


        public bool CreateStudentPersonalDetails(StudentsPersonalDetails studentDetails)
        {
            _db.StudentsPersonalDetails.Add(studentDetails);
            return Save();

        }

        public bool DeleteStudentPersonalDetails(StudentsPersonalDetails studentDetails)
        {
            _db.StudentsPersonalDetails.Remove(studentDetails);
            return Save();
        }

        public StudentsPersonalDetails GetStudentPersonalDetails(int StudentPersonalDetailId)
        {
            return _db.StudentsPersonalDetails.Include(x => x.StudentsAddress).FirstOrDefault(x => x.Id == StudentPersonalDetailId);
        }

        public ICollection<StudentsPersonalDetails> GetStudentsPersonalDetails()
        {
            return _db.StudentsPersonalDetails.Include(x => x.StudentsAddress).OrderBy(x => x.DateOfBirth).ToList();
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool StudentPersonalDetailsExists(long? phoneNumber)
        {
            return _db.StudentsPersonalDetails.Any(x => x.PhoneNumber == phoneNumber);
        }

        public bool StudentPersonalDetailsExists(int id)
        {
            return _db.StudentsPersonalDetails.Any(x => x.Id == id);
        }

        public bool UpdateStudentPersonalDetails(StudentsPersonalDetails studentDetails)
        {
            _db.Update(studentDetails);
            return Save();
        }
    }
}
