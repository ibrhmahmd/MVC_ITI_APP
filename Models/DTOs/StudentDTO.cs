using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MVC_PROJECT.Models.DTOs
{
    public class StudentDTO
    {
        [Key]
        public int StudentId { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [StringLength(100)]
        [RegularExpression(@"[a-zA-Z0-9_]+@[a-zA-Z]+.[a-zA-Z]{2,4}")]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [NotMapped]
        [Compare("Password")]
        public string CPassword { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
    }
}
