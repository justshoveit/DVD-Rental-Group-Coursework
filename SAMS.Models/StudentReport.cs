using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.Models
{
    public class StudentReport
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string RollNumber { get; set; }
        public string AcademicYear { get; set; }
        public DateTime EnrollDate { get; set; }

        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int SemesterId { get; set; }
        public string SemesterName { get; set; }
        public int ModuleId { get; set; }
        public int TeacherId { get; set; }
        public string ModuleName { get; set; }
        public string ReportType { get; set; }

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }

    
   
}
