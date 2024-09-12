using System.ComponentModel.DataAnnotations;

namespace MVC_PROJECT.Models.DTOs
{
    public class DepartmentDTO
    {
        [Key]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100)]
        public string Name { get; set; }
    }
}
