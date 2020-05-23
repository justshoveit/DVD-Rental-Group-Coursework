using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.Models
{
    public class TeacherReport
    {
        public int TeacherId { get; set; }
        public string TeacherName { get; set; }
        public int TeacherTypeId { get; set; }
        public string TeacherType { get; set; }
        public int ModuleId { get; set; }
        public string ModuleName { get; set; }
        public int ClassesPerWeek { get; set; }

        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int SemesterId { get; set; }
        public string SemesterName { get; set; }
        public string ReportType { get; set; }

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
