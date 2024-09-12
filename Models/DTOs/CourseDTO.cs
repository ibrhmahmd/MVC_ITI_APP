using System.ComponentModel.DataAnnotations;

namespace MVC_PROJECT.Models.DTOs
{
    public class CourseDTO
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Range(2, 6)]
        public int Hours { get; set; }

        public int? StudentID { get; set; }

        [Required]
        public int DepartmentID { get; set; }
    }
}
