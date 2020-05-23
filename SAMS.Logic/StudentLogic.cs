using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SAMS.Repository.StudentRepository;

using SAMS.Models;

namespace SAMS.Logic
{
    public class StudentLogic
    {
        public StudentRepository _repo { get; set; }
        public StudentLogic()
        {
            _repo = new StudentRepository();
        }

        public async Task<List<Student>> 
            GetStudents()
        {
            List<Student> students = await _repo.GetStudents();

            return students;
        }

        public async Task<Student> GetStudent(int studentId)
        {
            return await _repo.GetStudent(studentId);
        }

        public async Task<bool> InsertUpdateStudent(SAMS.Models.Student student)
        {
            return await _repo.InsertUpdateStudent(student);
        }
        public async Task<bool> DeleteStudent(int studentId)
        {
            return await _repo.DeleteStudent(studentId);
        }
    }
}
