using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.Models
{
   public class Role
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Role is required.")]
        public string Name { get; set; }
    }
}
