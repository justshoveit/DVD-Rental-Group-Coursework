using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.Models
{
    public class Course
    {
        public int Id { get; set; }
          [Required(ErrorMessage = "Course is required.")]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
