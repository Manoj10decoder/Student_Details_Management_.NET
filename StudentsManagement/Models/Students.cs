using Microsoft.VisualBasic;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentsManagement.Models
{
    public class Students
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public long? RegisterNumber { get; set; }
        public int Standard { get; set; }
        public string Section { get; set; }
        [Required]
        public long? AadhaarNumber { get; set; }
        public int? StudentsPersonalDetailsId { get; set; } = null!;

        [ForeignKey("StudentsPersonalDetailsId")]
        public StudentsPersonalDetails StudentsPersonalDetails { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
