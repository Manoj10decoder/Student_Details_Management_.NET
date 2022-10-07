using Microsoft.VisualBasic;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentsManagement.Models
{
    public class UpdateStudentsPersonalDetailsDto
    {
        public int Id { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Age { get; set; }
        public string MotherName { get; set; }
        public string FatherName { get; set; }
        [Required]
        public long? PhoneNumber { get; set; }
        [Required]
        public int? StudentId { get; set; }
        public int? StudentsAddressId { get; set; } = null!;
    }
}
