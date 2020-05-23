using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using SAMS.Models;

namespace SAMS.Repository.ReportRepository
{
    public class ReportRepository
    {

        private BaseDao _dao;
        public ReportRepository()
        {
            _dao = new BaseDao();
        }

        public async Task<List<StudentReport>> StudentReport(StudentReport param)
        {
            string qry = @"select s.id as studentId, s.name as StudentName , s.RollNumber, s.AcademicYear, s.EnrollDate
								, m.id as moduleId, m.Name as moduleName
								, c.Id as courseId, c.Name as courseName
								, sem.id as semesterId, sem.Name as semesterName
							from student s
								inner join StudentModuleMapping SMM ON SMM.StudentId = s.id
								inner join module m on m.id = smm.id
								inner join course c on c.Id = m.CourseId
								inner join semester sem on sem.id = m.SemesterId
							where m.courseId = case when @courseId <> -1  then @courseid else m.courseid end	
							order by EnrollDate asc";

            List<SqlParam> collection = new List<SqlParam>{
                new SqlParam("@courseId", SqlDbType.VarChar, param.CourseId)
            };
            return await _dao.FetchListAsync<StudentReport>(qry, collection);
        }

        public async Task<List<TeacherReport>> AdministrativeReport(int teacherTypeId)
        {
            string qry = @"	select tc.id as TeacherID
							, case tc.type when 1 then 'Tutor' else 'Lecturer' end as TeacherType
							, tc.Name as TeacherName
							, c.id as courseId, c.name as courseName
							, m.id as moduleId, m.name as moduleName
							, count('x') as ClassesPerWeek
							from Teacher tc
								inner join Timetable t  on tc.Id = t.SemesterId
								inner join module m on t.ModuleId = m.id
								inner join course c on t.CourseId = c.id
							where tc.Type = case when @teacherType <> -1 then @teacherType else tc.Type end
							group by tc.id, tc.Type, tc.name, m.id, m.name, c.id, c.name";

            List<SqlParam> collection = new List<SqlParam>{
                new SqlParam("@teacherType", SqlDbType.VarChar, teacherTypeId)
            };
            return await _dao.FetchListAsync<TeacherReport>(qry, collection);
        }

        public async Task<List<AttendanceReport>> AttendanceReport(StudentReport param)
        {
            string qry = @"select s.id as StudentId, s.RollNumber as RollNumber, s.Name as StudentName
								,sum(case when ad.[Status] = 'P' then 1 else 0 end) as PresentDays
								,sum(case when ad.[Status] = 'L' then 1 else 0 end) as LateDays
								,sum(case when ad.[Status] = 'A' then 1 else 0 end) as AbsentDays
							from attendanceDetl ad
								inner join attendanceMast am on ad.MastId = am.Id
								inner join student s on ad.StudentId = s.Id
							where am.AttendanceDate between @from_date and @to_date
								and am.ModuleId = @moduleId
								and s.Id = case when @studentId <> -1 then @studentId else s.Id end
							group by s.id, s.RollNumber, s.Name";

            List<SqlParam> collection = new List<SqlParam>{
                new SqlParam("@from_date", SqlDbType.VarChar, param.FromDate),
                new SqlParam("@to_date", SqlDbType.VarChar, param.ToDate),
                new SqlParam("@moduleId", SqlDbType.VarChar, param.ModuleId),
                new SqlParam("@studentId", SqlDbType.VarChar, param.StudentId),
            };
            return await _dao.FetchListAsync<AttendanceReport>(qry, collection);
        }
    }
}