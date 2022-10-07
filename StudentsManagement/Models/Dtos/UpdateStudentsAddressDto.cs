using System.ComponentModel.DataAnnotations;

namespace StudentsManagement.Models.Dtos
{
    public class UpdateStudentsAddressDto
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public int Pincode { get; set; }
        [Required]
        public int? StudentId { get; set; }
        [Required]
        public int? StudentPersonalDetailId { get; set; }
    }
}
