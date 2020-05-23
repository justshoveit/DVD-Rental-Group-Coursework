using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using SAMS.Models;
using System.Data.SqlClient;

namespace SAMS.Repository.UserRepository
{
    public class UserRepository
    {
        private BaseDao _dao;
        public UserRepository()
        {
            _dao = new BaseDao();
        }

       

        public async Task<List<User>> GetUsers()
        {
            string query = @"SELECT U.*,R.Name AS RoleName FROM [USER] U INNER JOIN [ROLE] R ON  U.RoleId = R.Id";

            return await _dao.FetchListAsync<User>(query);
        }

        public async Task<User> GetUser(int id)
        {
            string query = @"SELECT U.*,R.Name AS RoleName FROM [USER] U INNER JOIN [ROLE] R ON  U.RoleId = R.Id WHERE U.Id = @id";
            List<SqlParam> collection = new List<SqlParam>(){
                new SqlParam("@id", SqlDbType.Int, id)
            };

            return await _dao.FetchItemAsync<User>(query, collection);
        }

        public async Task<bool> InsertUpdateUser(User user)
        {
            string query = "SpInsertUpdateUser";
            List<SqlParam> collection = new List<SqlParam>(){
                  new SqlParam("@Id", SqlDbType.Int, user.Id),
                new SqlParam("@UserName", SqlDbType.VarChar, user.UserName),
                new SqlParam("@Password", SqlDbType.NVarChar, user.Password),
                new SqlParam("@Email", SqlDbType.NVarChar, user.Email),
                new SqlParam("@RoleId", SqlDbType.Int, user.RoleId),
            };

            return await _dao.ExecuteNonQueryAsync(query, collection,CommandType.StoredProcedure);
        }



        public async Task<bool> DeleteUser(int id)
        {
            string query = @"DELETE FROM [USER] WHERE Id = @id";
            List<SqlParam> collection = new List<SqlParam>(){
                new SqlParam("@id", SqlDbType.Int, id)
            };
            return await _dao.ExecuteNonQueryAsync(query, collection);
        }
    }
}
