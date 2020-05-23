using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SAMS.Repository.TeacherRepository;

using SAMS.Models;

namespace SAMS.Logic
{
    public class TeacherLogic
    {
          public TeacherRepository _repo { get; set; }
          public TeacherLogic()
        {
            _repo = new TeacherRepository();
        }

          public async Task<List<Teacher>> GetTeachers()
        {
            List<Teacher> teachers = await _repo.GetTeachers();
            return teachers;
        }

          public async Task<Teacher> GetTeacher(int id)
        {
            return await _repo.GetTeacher(id);
        }

          public async Task<bool> InsertUpdateTeacher(Teacher teacher)
        {
            return await _repo.InsertUpdateTeacher(teacher);
        }
          public async Task<bool> DeleteTeacher(int id)
        {
            return await _repo.DeleteTeacher(id);
        }
    }
}
