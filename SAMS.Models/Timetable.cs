using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.Models
{
    public class Timetable
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Course is required.")]
        public int CourseId { get; set; }
       

        public string CourseName { get; set; }

         [Required(ErrorMessage = "Semester is required.")]
        public int SemesterId { get; set; }
     

        public string SemesterName { get; set; }
           [Required(ErrorMessage = "Academic Year is required.")]
        public int AcademicYear { get; set; }
        [Required(ErrorMessage = "Module is required.")]
        public int ModuleId { get; set; }
        public string ModuleName { get; set; }

        [Required(ErrorMessage = "Teacher Name is required.")]
        public int TeacherId { get; set; }
          

        public string TeacherName { get; set; }
          [Required(ErrorMessage = "Start Time is required.")]
        public string StartTime { get; set; }
          [Required(ErrorMessage = "End Time is required.")]
        public string EndTime { get; set; }
          [Required(ErrorMessage = "Day is required.")]
        public int Day { get; set; }
        public string ClassDetails { get; set; }
    }
}
