using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.Models
{
   public class StudentModuleMapping
    {
        public int Id { get; set; }
         [Required(ErrorMessage = "Student is required.")]
        public int StudentId { get; set; }
      

         public string StudentName { get; set; }

          [Required(ErrorMessage = "Module is required.")]
        public int ModuleId { get; set; }

        public string ModuleName { get; set; }
    }
}
