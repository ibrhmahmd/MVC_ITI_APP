using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MVC_PROJECT.Models
{
    public class Course
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Range(2, 6)]
        public int Hours { get; set; }

        public int? StudentID{ get; set; }
        public Student Student { get; set; }

        [Required]
        public int DepartmentID { get; set; }
        public  Department Department { get; set; }

    }
}