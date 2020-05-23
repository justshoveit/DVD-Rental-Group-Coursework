using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SAMS.Repository.RoleRepository;

using SAMS.Models;

namespace SAMS.Logic
{
    public class RoleLogic
    {
        public RoleRepository _repo { get; set; }
        public RoleLogic()
        {
            _repo = new RoleRepository();
        }

        public async Task<List<Role>> GetRoles()
        {
            List<Role> modules = await _repo.GetRoles();
            return modules;
        }

        public async Task<Role> GetRole(int id)
        {
            return await _repo.GetRole(id);
        }

        public async Task<bool> InsertUpdateRole(Role course)
        {
            return await _repo.InsertUpdateRole(course);
        }
        public async Task<bool> DeleteRole(int id)
        {
            return await _repo.DeleteRole(id);
        }
    }
}
