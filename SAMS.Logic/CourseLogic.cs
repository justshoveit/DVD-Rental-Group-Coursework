using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SAMS.Repository.CourseRepository;

using SAMS.Models;

namespace SAMS.Logic
{
    public class CourseLogic
    {
        public CourseRepository _repo { get; set; }
        public CourseLogic()
        {
            _repo = new CourseRepository();
        }

        public async Task<List<Course>> GetCourses()
        {
            List<Course> courses = await _repo.GetCourses();
            return courses;
        }

        public async Task<Course> GetCourse(int id)
        {
            return await _repo.GetCourse(id);
        }

        public async Task<bool> InsertUpdateCourse(Course course)
        {
            return await _repo.InsertUpdateCourse(course);
        }
        public async Task<bool> DeleteCourse(int id)
        {
            return await _repo.DeleteCourse(id);
        }
    }
}
