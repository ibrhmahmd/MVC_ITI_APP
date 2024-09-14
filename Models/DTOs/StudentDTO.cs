using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MVC_PROJECT.Models.DTOs
{
    public class StudentDTO
    {
        [Key]
        public int StudentId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(20)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20)]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [StringLength(100)]
        [RegularExpression(@"[a-zA-Z0-9_]+@[a-zA-Z]+.[a-zA-Z]{2,4}")]
        //[UniqueEmail] 
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [NotMapped]
        [Compare("Password")]
        public string CPassword { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }

        public static explicit operator StudentDTO(Student? v)
        {
            throw new NotImplementedException();
        }
    }
}
