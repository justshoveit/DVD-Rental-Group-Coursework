using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.Models
{
    public class AttendanceDetail
    {
        public int Id { get; set; }
        public int MastId { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
    }
}
