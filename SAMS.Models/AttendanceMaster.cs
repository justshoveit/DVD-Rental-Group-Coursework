using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.Models
{
    public class AttendanceMaster
    {
        public int Id { get; set; }
        public int ModuleId { get; set; }
        public System.DateTime AttendanceDate { get; set; }
        public int TimetableId { get; set; }
        public List<AttendanceDetail> details { get; set; }
        public int CourseId { get; set; }
        public int SemesterId { get; set; }
        public string CourseName { get; set; }
        public string SemesterName { get; set; }
        public string ModuleName { get; set; }
        public string ClassInformation { get; set; }
        public int Day { get; set; }

    }
}