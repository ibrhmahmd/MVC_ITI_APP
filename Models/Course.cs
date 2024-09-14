using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MVC_PROJECT.Models
{
    public class Course
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(100)]  // Assuming a reasonable length for the course name
        public string Name { get; set; }

        [Required]
        public int Hours { get; set; }
        public bool IsDeleted { get; set; } // Soft delete flag

        // Nullable foreign key to Student
        [ForeignKey("Student")]
        public int? StudentID { get; set; }  // Nullable as per the database schema

        // Foreign key to Department
        [Required]
        [ForeignKey("Department")]
        public int DepartmentID { get; set; }

        // Navigation properties
        public virtual Student Student { get; set; }
        public virtual Department Department { get; set; }

    }
}