using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using SAMS.Models;

namespace SAMS.Repository.LoginRepository
{
    public class LoginRepository
    {
        private BaseDao _dao;
        public LoginRepository()
        {
            _dao = new BaseDao();
        }

        public async Task<User> GetUser(Login data)
        {
            string query = @"USP_LOGIN";
            List<SqlParam> collection = new List<SqlParam>(){
                new SqlParam("@USERNAME", SqlDbType.VarChar, data.UserName),
                new SqlParam("@PASSWORD", SqlDbType.VarChar, data.Password),
            };

            return await _dao.FetchItemAsync<User>(query, collection, CommandType.StoredProcedure);
        }
    }
}
