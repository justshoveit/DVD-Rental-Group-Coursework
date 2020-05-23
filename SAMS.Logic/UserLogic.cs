using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SAMS.Repository.UserRepository;

using SAMS.Models;

namespace SAMS.Logic
{
    public class UserLogic
    {
        public UserRepository _repo { get; set; }
        public UserLogic()
        {
            _repo = new UserRepository();
        }

        public async Task<List<User>> GetUsers()
        {
            List<User> users = await _repo.GetUsers();
            return users;
        }

        public async Task<User> GetUser(int id)
        {
            return await _repo.GetUser(id);
        }

        public async Task<bool> InsertUpdateUser(User user)
        {
            return await _repo.InsertUpdateUser(user);
        }
        public async Task<bool> DeleteUser(int id)
        {
            return await _repo.DeleteUser(id);
        }
    }
}
