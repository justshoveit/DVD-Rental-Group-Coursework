using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using SAMS.Models;
using System.Data.SqlClient;

namespace SAMS.Repository.CourseRepository
{
    public class CourseRepository
    {
        private BaseDao _dao;
        public CourseRepository()
        {
            _dao = new BaseDao();
        }
        public async Task<List<Course>> GetCourses()
        {
            string query = @"SELECT * FROM COURSE";

            return await _dao.FetchListAsync<Course>(query);
        }
        public async Task<Course> GetCourse(int id)
        {
            string query = @"SELECT * FROM COURSE WHERE Id = @id";
            List<SqlParam> collection = new List<SqlParam>(){
                new SqlParam("@id", SqlDbType.Int, id)
            };

            return await _dao.FetchItemAsync<Course>(query, collection);
        }
        public async Task<bool> InsertUpdateCourse(Course student)
        {
            string query = "SpInsertUpdateCourse";
            List<SqlParam> collection = new List<SqlParam>(){
                new SqlParam("@Id", SqlDbType.Int, student.Id),
                new SqlParam("@Name", SqlDbType.NVarChar, student.Name),
                new SqlParam("@Description", SqlDbType.NVarChar, student.Description),
            };

            return await _dao.ExecuteNonQueryAsync(query, collection, CommandType.StoredProcedure);
        }
        public async Task<bool> DeleteCourse(int id)
        {
            string query = @"DELETE FROM COURSE WHERE Id = @id";
            List<SqlParam> collection = new List<SqlParam>(){
                new SqlParam("@id", SqlDbType.Int, id)
            };

            return await _dao.ExecuteNonQueryAsync(query, collection);
        }
    }
}
