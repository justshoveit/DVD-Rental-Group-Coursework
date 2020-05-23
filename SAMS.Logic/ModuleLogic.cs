using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SAMS.Repository.ModuleRepository;

using SAMS.Models;

namespace SAMS.Logic
{
    public class ModuleLogic
    {
        public ModuleRepository _repo { get; set; }
        public ModuleLogic()
        {
            _repo = new ModuleRepository();
        }

        public async Task<List<Module>> GetModules()
        {
            List<Module> modules = await _repo.GetModules();
            return modules;
        }

        public async Task<Module> GetModule(int id)
        {
            return await _repo.GetModule(id);
        }

        public async Task<bool> InsertUpdateModule(Module course)
        {
            return await _repo.InsertUpdateModule(course);
        }
        public async Task<bool> DeleteModule(int id)
        {
            return await _repo.DeleteModule(id);
        }
    }
}
