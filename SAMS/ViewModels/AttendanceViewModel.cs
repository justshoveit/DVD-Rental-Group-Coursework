using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SAMS.ViewModels
{
    public class AttendanceViewModel : BaseViewModel
    {
        public int ModuleId { get; set; }
        [Required]
        public System.DateTime AttendanceDate { get; set; }
        [Required]
        [Display(Name = "Class")]
        public int TimetableId { get; set; }
        public List<AttendanceDetailViewModel> details { get; set; }
        [Required]
        [Display(Name="Course")]
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        [Required]
        [Display(Name = "Semester")]
        public int SemesterId { get; set; }
        public string SemesterName { get; set; }
        public string ModuleName { get; set; }
        public string ClassInformation { get; set; }
        public IList<SelectListItem> CourseList { get; set; }
        public IList<SelectListItem> SemesterList { get; set; }
        public IList<SelectListItem> ClassList { get; set; }
    }

    public class AttendanceDetailViewModel
    {
        public int Id { get; set; }
        public int MastId { get; set; }
        [Required]
        public int StudentId { get; set; }
        [Required]
        public string StudentName { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
    }
}