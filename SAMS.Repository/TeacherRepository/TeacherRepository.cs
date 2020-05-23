using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using SAMS.Models;
using System.Data.SqlClient;

namespace SAMS.Repository.TeacherRepository
{
    public class TeacherRepository
    {
        private BaseDao _dao;
        public TeacherRepository()
        {
            _dao = new BaseDao();
        }

        public async Task<List<Teacher>> GetTeachers()
        {
            string query = @"SELECT * FROM TEACHER";

            return await _dao.FetchListAsync<Teacher>(query);
        }

        public async Task<Teacher> GetTeacher(int id)
        {
            string query = @"SELECT * FROM TEACHER WHERE Id = @id";
            List<SqlParam> collection = new List<SqlParam>(){
                new SqlParam("@id", SqlDbType.Int, id)
            };

            return await _dao.FetchItemAsync<Teacher>(query, collection);
        }

        public async Task<bool> InsertUpdateTeacher(Teacher teacher)
        {
            string query = "SpInsertUpdateTeacher";
            List<SqlParam> collection = new List<SqlParam>(){
                new SqlParam("@Id", SqlDbType.Int, teacher.Id),
                new SqlParam("@Name", SqlDbType.VarChar, teacher.Name),
                new SqlParam("@Type", SqlDbType.Int, teacher.Type),
                new SqlParam("@Email", SqlDbType.VarChar, teacher.Email),
                new SqlParam("@MobileNo", SqlDbType.VarChar, teacher.MobileNo),
            };

            return await _dao.ExecuteNonQueryAsync(query, collection,CommandType.StoredProcedure);
        }

        

        public async Task<bool> DeleteTeacher(int id)
        {
            string query = @"DELETE FROM TEACHER WHERE Id = @id";
            List<SqlParam> collection = new List<SqlParam>(){
                new SqlParam("@id", SqlDbType.Int, id)
            };

            return await _dao.ExecuteNonQueryAsync(query, collection);
        }
    }
}
