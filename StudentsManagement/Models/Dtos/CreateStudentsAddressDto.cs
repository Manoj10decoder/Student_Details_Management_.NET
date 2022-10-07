using System.ComponentModel.DataAnnotations;

namespace StudentsManagement.Models.Dtos
{
    public class CreateStudentsAddressDto
    {
        public string Address { get; set; }
        public int Pincode { get; set; }
        [Required]
        public int? StudentId { get; set; }
        [Required]
        public int? StudentPersonalDetailId { get; set; }
    }
}
