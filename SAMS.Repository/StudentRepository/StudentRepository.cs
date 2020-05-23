using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using SAMS.Models;
using System.Data.SqlClient;

namespace SAMS.Repository.StudentRepository
{
    public class StudentRepository
    {
        private BaseDao _dao;
        public StudentRepository()
        {
            _dao = new BaseDao();
        }

        public async Task<List<Student>> GetStudents()
        {
            string query = @"SELECT * FROM STUDENT";

            return await _dao.FetchListAsync<Student>(query);
        }

        public async Task<Student> GetStudent(int studentId)
        {
            string query = @"SELECT * FROM STUDENT WHERE Id = @studentId";
            List<SqlParam> collection = new List<SqlParam>(){
                new SqlParam("@studentId", SqlDbType.Int, studentId)
            };

            return await _dao.FetchItemAsync<Student>(query,collection);
        }

        public async Task<bool> InsertUpdateStudent(SAMS.Models.Student student)
        {
            string query = "SpInsertUpdateStudent";
            List<SqlParam> collection = new List<SqlParam>(){
                new SqlParam("@Id", SqlDbType.Int, student.Id),
                new SqlParam("@Name", SqlDbType.NVarChar, student.Name),
                new SqlParam("@RollNumber", SqlDbType.NVarChar, student.RollNumber),
                new SqlParam("@Email", SqlDbType.NVarChar, student.Email),
                new SqlParam("@AcademicYear", SqlDbType.NVarChar, student.AcademicYear),
                new SqlParam("@EnrollDate", SqlDbType.Date, student.EnrollDate),
            };

            return await _dao.ExecuteNonQueryAsync(query, collection,CommandType.StoredProcedure);
        }

      

        public async Task<bool> DeleteStudent(int studentId)
        {
            string query = @"DELETE FROM STUDENT WHERE Id = @studentId";
            List<SqlParam> collection = new List<SqlParam>(){
                new SqlParam("@studentId", SqlDbType.Int, studentId)
            };

            return await _dao.ExecuteNonQueryAsync(query, collection);
        }
    }
}
