using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.Models
{
    public class Module
    {
        public int Id { get; set; }
          [Required(ErrorMessage = "Module is required.")]
        public string Name { get; set; }
        public string Code { get; set; }
          [Required(ErrorMessage = "Credit is required.")]
        public int Credit { get; set; }
        public int Level { get; set; }
          [Required(ErrorMessage = "Course is required.")]
        public int CourseId { get; set; }
        public string CourseName { get; set; }
          [Required(ErrorMessage = "Semester is required.")]
        public int SemesterId { get; set; }
          public string SemesterName { get; set; }
    }
}
