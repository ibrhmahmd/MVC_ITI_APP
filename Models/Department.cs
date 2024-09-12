﻿using System.ComponentModel.DataAnnotations;

namespace MVC_PROJECT.Models
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public virtual ICollection<Course> Courses{ get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}
