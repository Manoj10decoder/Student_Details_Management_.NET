using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace StudentsManagement.Models.Dtos
{
    public class CreateStudentsDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public long? RegisterNumber { get; set; }
        public int Standard { get; set; }
        public string Section { get; set; }
        [Required]
        public long? AadhaarNumber { get; set; } = null!;
        public int? StudentsPersonalDetailsId { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}
