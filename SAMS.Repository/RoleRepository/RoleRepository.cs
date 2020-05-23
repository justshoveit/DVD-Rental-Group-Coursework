using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using SAMS.Models;
using System.Data.SqlClient;

namespace SAMS.Repository.RoleRepository
{
    public class RoleRepository
    {
        private BaseDao _dao;
        public RoleRepository()
        {
            _dao = new BaseDao();
        }
        public async Task<List<Role>> GetRoles()
        {
            string query = @"SELECT * FROM ROLE";

            return await _dao.FetchListAsync<Role>(query);
        }
        public async Task<Role> GetRole(int id)
        {
            string query = @"SELECT * FROM ROLE WHERE Id = @id";
            List<SqlParam> collection = new List<SqlParam>(){
                new SqlParam("@id", SqlDbType.Int, id)
            };

            return await _dao.FetchItemAsync<Role>(query, collection);
        }
        public async Task<bool> InsertUpdateRole(Role role)
        {
            string query = "SpInsertUpdateRole";
            List<SqlParam> collection = new List<SqlParam>(){
                new SqlParam("@Id", SqlDbType.Int, role.Id),
                new SqlParam("@Name", SqlDbType.NVarChar, role.Name),
            };

            return await _dao.ExecuteNonQueryAsync(query, collection, CommandType.StoredProcedure);
        }
        public async Task<bool> DeleteRole(int id)
        {
            string query = @"DELETE FROM ROLE WHERE Id = @id";
            List<SqlParam> collection = new List<SqlParam>(){
                new SqlParam("@id", SqlDbType.Int, id)
            };

            return await _dao.ExecuteNonQueryAsync(query, collection);
        }
    }
}
