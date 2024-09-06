using System.ComponentModel.DataAnnotations;

namespace MVC_PROJECT.Models.DTOs
{
    public class DepartmentDTO
    {
        [Key]
        public int DepartmentId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}
