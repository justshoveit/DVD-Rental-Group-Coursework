using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.Models
{
    public class Teacher
    { 
        public int Id { get; set; }
         [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
         [Required(ErrorMessage = "Teacher Type is required.")]
        public int Type { get; set; }
        public string Email { get; set; }
         [Required(ErrorMessage = "Mobile Number is required.")]
        public string MobileNo { get; set; }

         
    }
}
