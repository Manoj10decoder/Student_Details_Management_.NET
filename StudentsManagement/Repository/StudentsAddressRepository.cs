using StudentsManagement.Data;
using StudentsManagement.Models;
using StudentsManagement.Repository.IRepository;
using System.Collections.Generic;
using System.Linq;

namespace StudentsManagement.Repository
{
    public class StudentsAddressRepository : IStudentsAddressRepository
    {

        private readonly ApplicationDbContext _db;

        public StudentsAddressRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool CreateStudentAddress(StudentsAddress studentAddress)
        {
            _db.StudentsAddress.Add(studentAddress);
            return Save();
        }

        public bool DeleteStudentAddress(StudentsAddress studentAddress)
        {
            _db.StudentsAddress.Remove(studentAddress);
            return Save();
        }

        public StudentsAddress GetStudentAddress(int studentAddressId)
        {
            return _db.StudentsAddress.FirstOrDefault(x => x.Id == studentAddressId);
        }

        public ICollection<StudentsAddress> GetStudentsAddress()
        {
            return _db.StudentsAddress.OrderBy(x => x.Address).ToList();
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool StudentAddressExists(string address)
        {
            return _db.StudentsAddress.Any(x => x.Address == address);
        }

        public bool StudentAddressExists(int addressId)
        {
            return _db.StudentsAddress.Any(x => x.Id == addressId);
        }

        public bool UpdateStudentAddress(StudentsAddress studentAddress)
        {
            _db.StudentsAddress.Update(studentAddress);
            return Save();
        }
    }
}
