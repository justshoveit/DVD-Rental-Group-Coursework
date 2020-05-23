using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.Models
{
   public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "User Name is required.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }

        public string ReTypePassword { get; set; }
        public string Email { get; set; }
        [Required(ErrorMessage = "Role is required.")]
        public int RoleId { get; set; }
        public string RoleName { get; set; }

        //public IList<SelectListItem> CourseList { get; set; }

    }
}
