using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using SAMS.Models;
using System.Data.SqlClient;

namespace SAMS.Repository.TimetableRepository
{
    public class TimetableRepository
    {
        private BaseDao _dao;
        public TimetableRepository()
        {
            _dao = new BaseDao();
        }

        public async Task<List<Timetable>> GetTimetables()
        {
            string query = @"SELECT T.*,S.Name AS SemesterName, M.Name AS ModuleName,TE.Name AS TeacherName , C.Name as CourseName FROM Timetable T INNER JOIN Semester S ON T.SemesterId = S.Id
                         INNER JOIN Module M on T.ModuleId = M.Id INNER JOIN Teacher TE ON T.TeacherId = TE.Id INNER JOIN COURSE C ON T.CourseId= C.ID";

            return await _dao.FetchListAsync<Timetable>(query);
        }

        public async Task<Timetable> GetTimetable(int id)
        {
            string query = @"SELECT T.*,S.Name AS SemesterName, M.Name AS ModuleName,TE.Name AS TeacherName FROM Timetable T INNER JOIN Semester S ON T.SemesterId = S.Id
 INNER JOIN Module M on T.ModuleId = M.Id INNER JOIN Teacher TE ON T.TeacherId = TE.Id WHERE T.Id = @id";
            List<SqlParam> collection = new List<SqlParam>(){
                new SqlParam("@id", SqlDbType.Int, id)
            };

            return await _dao.FetchItemAsync<Timetable>(query, collection);
        }

        public async Task<bool> InsertUpdateTimeTable(Timetable timetable)
        {
            string query = "SpInsertUpdateTimetable";
            List<SqlParam> collection = new List<SqlParam>(){
                  new SqlParam("@Id", SqlDbType.Int, timetable.Id),
                new SqlParam("@CourseId", SqlDbType.Int, timetable.CourseId),
                new SqlParam("@SemesterId", SqlDbType.Int, timetable.SemesterId),
                new SqlParam("@AcademicYear", SqlDbType.Int, timetable.AcademicYear),
                new SqlParam("@ModuleId", SqlDbType.Int, timetable.ModuleId),
                  new SqlParam("@TeacherId", SqlDbType.Int, timetable.TeacherId),
                new SqlParam("@StartTime", SqlDbType.NVarChar, timetable.StartTime),
                  new SqlParam("@EndTime", SqlDbType.NVarChar, timetable.EndTime),
                new SqlParam("@Day", SqlDbType.NVarChar, timetable.Day),
            };

            return await _dao.ExecuteNonQueryAsync(query, collection,CommandType.StoredProcedure);
        }



        public async Task<bool> DeleteTimetable(int id)
        {
            string query = @"DELETE FROM Timetable WHERE Id = @id";
            List<SqlParam> collection = new List<SqlParam>(){
                new SqlParam("@id", SqlDbType.Int, id)
            };
            return await _dao.ExecuteNonQueryAsync(query, collection);
        }
    }
}
