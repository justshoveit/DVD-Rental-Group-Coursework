using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using SAMS.Models;
using System.Data.SqlClient;

namespace SAMS.Repository.ModuleRepository
{
    public class ModuleRepository
    {
        private BaseDao _dao;
        public ModuleRepository()
        {
            _dao = new BaseDao();
        }
        public async Task<List<Module>> GetModules()
        {
            string query = @"SELECT M.*,C.Name AS CourseName,S.Name AS SemesterName FROM Module M INNER JOIN COURSE C ON M.CourseId= C.Id
                          INNER JOIN Semester S ON M.SemesterId = S.Id";

            return await _dao.FetchListAsync<Module>(query);
        }
        public async Task<Module> GetModule(int id)
        {
            string query = @"SELECT M.*,C.Name AS CourseName,S.Name AS SemesterName FROM Module M INNER JOIN COURSE C ON M.CourseId= C.Id
                          INNER JOIN Semester S ON M.SemesterId = S.Id WHERE M.Id = @id";
            List<SqlParam> collection = new List<SqlParam>(){
                new SqlParam("@id", SqlDbType.Int, id)
            };

            return await _dao.FetchItemAsync<Module>(query, collection);
        }
        public async Task<bool> InsertUpdateModule(Module module)
        {
            string query = "SpInsertUpdateModule";
            List<SqlParam> collection = new List<SqlParam>(){
                new SqlParam("@Id", SqlDbType.Int, module.Id),
                new SqlParam("@Name", SqlDbType.NVarChar, module.Name),
                new SqlParam("@Code", SqlDbType.NVarChar, module.Code),
                new SqlParam("@Credit", SqlDbType.Int, module.Credit),
                new SqlParam("@Level", SqlDbType.Int, module.Level),
                  new SqlParam("@CourseId", SqlDbType.Int, module.CourseId),
                new SqlParam("@SemesterId", SqlDbType.Int, module.SemesterId)
            };

            return await _dao.ExecuteNonQueryAsync(query, collection, CommandType.StoredProcedure);
        }
        public async Task<bool> DeleteModule(int id)
        {
            string query = @"DELETE FROM MODULE WHERE Id = @id";
            List<SqlParam> collection = new List<SqlParam>(){
                new SqlParam("@id", SqlDbType.Int, id)
            };

            return await _dao.ExecuteNonQueryAsync(query, collection);
        }
    }
}
