using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using SAMS.Models;
using System.Data.SqlClient;

namespace SAMS.Repository.StudentModuleRepository
{
    public class StudentModuleRepository
    {
        private BaseDao _dao;
        public StudentModuleRepository()
        {
            _dao = new BaseDao();
        }
        public async Task<List<StudentModuleMapping>> GetStudentModuleMappings()
        {
            string query = @"SELECT SMM.*,S.Name AS StudentName,M.Name AS ModuleName FROM StudentModuleMapping SMM INNER JOIN Student S
                        ON SMM.StudentId = S.Id INNER JOIN Module M ON SMM.ModuleId= M.Id";

            return await _dao.FetchListAsync<StudentModuleMapping>(query);
        }
        public async Task<StudentModuleMapping> GetStudentModuleMapping(int id)
        {
            string query = @"SELECT SMM.*,S.Name AS StudentName,M.Name AS ModuleName FROM StudentModuleMapping SMM INNER JOIN Student S
                         ON SMM.StudentId = S.Id INNER JOIN Module M ON SMM.ModuleId= M.Id WHERE SMM.Id = @id";
            List<SqlParam> collection = new List<SqlParam>(){
                new SqlParam("@id", SqlDbType.Int, id)
            };

            return await _dao.FetchItemAsync<StudentModuleMapping>(query, collection);
        }
        public async Task<bool> InsertUpdateStudentModuleMapping(StudentModuleMapping studentModuleMapping)
        {
            string query = "SpInsertUpdateStudentModuleMapping";
            List<SqlParam> collection = new List<SqlParam>(){
                new SqlParam("@Id", SqlDbType.Int, studentModuleMapping.Id),
                new SqlParam("@StudentId", SqlDbType.Int, studentModuleMapping.StudentId),
                new SqlParam("@ModuleId", SqlDbType.Int, studentModuleMapping.ModuleId),
       
            };

            return await _dao.ExecuteNonQueryAsync(query, collection, CommandType.StoredProcedure);
        }
        public async Task<bool> DeleteStudentModuleMapping(int id)
        {
            string query = @"DELETE FROM StudentModuleMapping WHERE Id = @id";
            List<SqlParam> collection = new List<SqlParam>(){
                new SqlParam("@Id", SqlDbType.Int, id)
            };

            return await _dao.ExecuteNonQueryAsync(query, collection);
        }
    }
}
