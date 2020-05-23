using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAMS.Models;
using SAMS.Repository;

namespace SAMS.Repository.AttendanceRepository
{
    public class AttendanceRepository
    {
        private BaseDao _dao;
        public AttendanceRepository()
        {
            _dao = new BaseDao();
        }

        public async Task<List<Course>> GetCourses()
        {
            string qry = @"SELECT Id, Name FROM COURSE";

            return await _dao.FetchListAsync<Course>(qry);
        }

        public async Task<List<Semester>> GetSemesters()
        {
            string qry = @"SELECT Id, Name FROM SEMESTER";

            return await _dao.FetchListAsync<Semester>(qry);
        }

        public async Task<List<Module>> GetModules()
        {
            string qry = @"SELECT Id, Name FROM Module";

            return await _dao.FetchListAsync<Module>(qry);
        }

        public async Task<List<Timetable>> GetTimetable(AttendanceMaster data)
        {
            string qry = @"SELECT  T.Id, concat(M.Name, ' | ', isnull(T.StartTime, ''), ' - ', isnull(T.EndTime, ''), ' (', isnull(cast(T.Day as varchar), ''), ')') as ClassDetails
                FROM Timetable T
                	INNER JOIN Module M on M.Id = T.ModuleId
                WHERE T.CourseId = @CourseId
                	and T.SemesterId = @SemesterId
                	and T.day = @day";

            List<SqlParam> parameters = new List<SqlParam>(){
                new SqlParam("@CourseId", SqlDbType.Int, data.CourseId),
                new SqlParam("@SemesterId", SqlDbType.Int, data.SemesterId),
                new SqlParam("@day", SqlDbType.Int, data.Day),
            };

            return await _dao.FetchListAsync<Timetable>(qry, parameters);
        }

        public async Task<List<Student>> GetStudents(AttendanceMaster param)
        {
            AttendanceMaster attendanceMaster = new AttendanceMaster();
            string qry = @"SELECT s.id, s.rollNumber, s.Name
                        FROM STUDENT S 
                        	INNER JOIN STUDENTMODULEMAPPING ST ON s.Id = st.StudentId
                        	INNER JOIN TIMETABLE T ON T.ModuleId = st.ModuleId
                        where t.id = @timetableId";

            List<SqlParam> parameters = new List<SqlParam>(){
                new SqlParam("@timetableId", SqlDbType.Int, param.TimetableId),
            };

            List<Student> list = await _dao.FetchListAsync<Student>(qry, parameters);

            return list;
        }

        public async Task<List<Student>> GetStudents(int moduleId)
        {
            AttendanceMaster attendanceMaster = new AttendanceMaster();
            string qry = @"SELECT s.id, s.rollNumber, s.Name
                        FROM STUDENT S 
                        	INNER JOIN STUDENTMODULEMAPPING ST ON s.Id = st.StudentId
                        where st.ModuleId = @moduleId";

            List<SqlParam> parameters = new List<SqlParam>(){
                new SqlParam("@moduleId", SqlDbType.Int, moduleId),
            };

            List<Student> list = await _dao.FetchListAsync<Student>(qry, parameters);

            return list;
        }

        public async Task<List<AttendanceMaster>> GetAttendanceList()
        {
            string qry = @"SELECT A.Id 
                            	, M.Id as ModuleId, M.Name as ModuleName
, A.AttendanceDate
                            	, T.Id as TimetableId, concat(T.StartTime, ' - ', T.EndTime, ' (', T.Day, ')') as ClassInformation
                            	, C.Id as CourseId, C.Name as CourseName
                            	, S.Id as SemesterId, S.Name as SemesterName
                            FROM AttendanceMast A
                            	INNER JOIN Timetable T on T.Id = A.TimetableId
                            	INNER JOIN Module M on M.Id = T.ModuleId
                            	INNER JOIN Course C on C.Id = T.CourseId
                            	INNER JOIN Semester S on S.Id = T.SemesterId";
            return await _dao.FetchListAsync<AttendanceMaster>(qry);
        }

        public AttendanceMaster ViewAttendance(int id)
        {
            AttendanceMaster attendanceMaster = new AttendanceMaster();
            string qry = @"SELECT A.Id 
                            	, M.Id as ModuleId, M.Name as ModuleName
                            	, T.Id as TimetableId, concat(T.StartTime, ' - ', T.EndTime, ' (', T.Day, ')') as ClassInformation
                            	, C.Id as CourseId, C.Name as CourseName
                            	, S.Id as SemesterId, S.Name as SemesterName
                            FROM AttendanceMast A
                            	INNER JOIN Timetable T on T.Id = A.TimetableId
                            	INNER JOIN Module M on M.Id = T.ModuleId
                            	INNER JOIN Course C on C.Id = T.CourseId
                            	INNER JOIN Semester S on S.Id = T.SemesterId
                            WHERE A.Id = @id
                            
                            SELECT d.id, s.Id as StudentId, s.name as studentName, d.[status], d.remarks
                            FROM AttendanceDetl D
                            	INNER JOIN STUDENT S ON d.StudentId = s.Id
                            WHERE D.MastId = @id";

            List<SqlParam> parameters = new List<SqlParam>(){
                new SqlParam("@id", SqlDbType.Int, id),
            };

            DataSet ds = _dao.ExecuteDataSet(qry, parameters);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                attendanceMaster = ds.Tables[0].ToList<AttendanceMaster>().FirstOrDefault();
                attendanceMaster.details = ds.Tables[1].ToList<AttendanceDetail>();
            }

            return attendanceMaster;
        }
        public AttendanceMaster ViewAttendance(int id, int weekDay)
        {
            AttendanceMaster attendanceMaster = new AttendanceMaster();
            string qry = @"SELECT A.Id 
                            	, M.Id as ModuleId, M.Name as ModuleName
                            	, T.Id as TimetableId, concat(T.StartTime, ' - ', T.EndTime, ' (', T.Day, ')') as ClassInformation
                            	, C.Id as CourseId, C.Name as CourseName
                            	, S.Id as SemesterId, S.Name as SemesterName
                            FROM AttendanceMast A
                            	INNER JOIN Timetable T on T.Id = A.TimetableId
                            	INNER JOIN Module M on M.Id = T.ModuleId
                            	INNER JOIN Course C on C.Id = T.CourseId
                            	INNER JOIN Semester S on S.Id = T.SemesterId
                            WHERE A.Id = @id
                            	and T.[day] = @day
                            
                            SELECT d.id, s.Id as StudentId, d.[status], d.remarks
                            FROM AttendanceDetl D
                            	INNER JOIN STUDENT S ON d.StudentId = s.Id
                            WHERE D.MastId = @id";

            List<SqlParam> parameters = new List<SqlParam>(){
                new SqlParam("@id", SqlDbType.Int, id),
                new SqlParam("@day", SqlDbType.Int, weekDay),
            };

            DataSet ds = _dao.ExecuteDataSet(qry, parameters);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                attendanceMaster = ds.Tables[0].ToList<AttendanceMaster>().FirstOrDefault();
                attendanceMaster.details = ds.Tables[1].ToList<AttendanceDetail>();
            }

            return attendanceMaster;
        }

        public async Task<bool> SaveAttendance(AttendanceMaster data)
        {
            string qry = @"USP_UPDATE_ATTENDANCE";

            DataTable dtDetails = data.details.Select(x => new 
            {
                Id = (int)x.Id,
                MastId = (int)x.MastId,
                StudentId = (int)x.StudentId,
                Status = x.Status ?? "",
                Remarks = x.Remarks ?? ""
            }
                ).ToList().ToDataTable();

            List<SqlParam> parameters = new List<SqlParam>(){
                new SqlParam("@AttendanceId", SqlDbType.Int, data.Id),
                new SqlParam("@ModuleId", SqlDbType.Int, data.ModuleId),
                new SqlParam("@AttendanceDate", SqlDbType.Date, data.AttendanceDate),
                new SqlParam("@TimetableId", SqlDbType.Int, data.TimetableId),
                new SqlParam("@ATTENDANCE_DETAILS", SqlDbType.Structured, dtDetails),
            };

            return await _dao.ExecuteNonQueryAsync(qry, parameters, CommandType.StoredProcedure);
        }


    }
}
