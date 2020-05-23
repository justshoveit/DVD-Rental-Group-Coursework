using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SAMS.Repository.StudentModuleRepository;

using SAMS.Models;

namespace SAMS.Logic
{
    public class StudentModuleLogic
    {
        public StudentModuleRepository _repo { get; set; }
        public StudentModuleLogic()
        {
            _repo = new StudentModuleRepository();
        }

        public async Task<List<StudentModuleMapping>> GetStudentModuleMappings()
        {
            List<StudentModuleMapping> modules = await _repo.GetStudentModuleMappings();
            return modules;
        }

        public async Task<StudentModuleMapping> GetStudentModuleMapping(int id)
        {
            return await _repo.GetStudentModuleMapping(id);
        }

        public async Task<bool> InsertUpdateStudentModuleMapping(StudentModuleMapping studentModuleMapping)
        {
            return await _repo.InsertUpdateStudentModuleMapping(studentModuleMapping);
        }
        public async Task<bool> DeleteStudentModuleMapping(int id)
        {
            return await _repo.DeleteStudentModuleMapping(id);
        }
    }
}
