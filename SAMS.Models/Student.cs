using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace SAMS.Models
{
    public class Student
    {
        public int Id { get; set; }
         [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
         [Required(ErrorMessage = "Roll Number is required.")]
         [DisplayName("Roll Number")]
        public string RollNumber { get; set; }
        public string Email { get; set; }
         [Required(ErrorMessage = "Academic Year is required.")]
         [DisplayName("Academic Year")]
         
        public int AcademicYear { get; set; }

         [Required(ErrorMessage = "Enroll Date is required.")]
         [DisplayName("Enroll Date")]
         [DataType(DataType.Date)]
         [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
         public DateTime EnrollDate { get; set; }
        
         public bool IsEdit { get; set; }
    }
}

