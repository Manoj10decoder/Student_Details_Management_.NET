using StudentsManagement.Data;
using StudentsManagement.Models;
using StudentsManagement.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace StudentsManagement.Repository
{
    public class StudentsRepository : IStudentsRepository
    {
        private readonly ApplicationDbContext _db;

        public StudentsRepository(ApplicationDbContext db)
        {
            _db = db;
        }


        public bool CreateStudent(Students student)
        {
            _db.Students.Add(student);
            return Save();

        }

        public bool DeleteStudent(Students student)
        {
            _db.Students.Remove(student);
            return Save();
        }

        public Students GetStudent(int StudetnId)
        {
            return _db.Students.Include(x => x.StudentsPersonalDetails.StudentsAddress).FirstOrDefault(x => x.Id == StudetnId);
        }

        public ICollection<Students> GetStudents()
        {
            return _db.Students.Include(x => x.StudentsPersonalDetails.StudentsAddress).OrderBy(x => x.Name).ToList();
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool StudentExists(string name)
        {
            return _db.Students.Any(x => x.Name.ToLower().Trim() == name.ToLower().Trim());
        }

        public bool StudentExists(int id)
        {
            return _db.Students.Any(x => x.Id == id);
        }

        public bool StudentExists(long? registerNumber)
        {
            return _db.Students.Any(x => x.RegisterNumber == registerNumber);
        }

        public bool StudentExistAadhaar(long? aadhaarNumber)
        {
            return _db.Students.Any(x => x.AadhaarNumber == aadhaarNumber);
        }

        public bool UpdateStudent(Students student)
        {
            _db.Update(student);
            return Save();
        }

        public ICollection<Students> GetStudentPersonalDetailAndAddress(int personalDetailId)
        {
            return _db.Students.Include(c => c.StudentsPersonalDetails.StudentsAddress).Where(c => c.StudentsPersonalDetailsId == personalDetailId).ToList();
        }
    }
}
