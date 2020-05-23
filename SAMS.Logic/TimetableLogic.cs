using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SAMS.Repository.TimetableRepository;

using SAMS.Models;

namespace SAMS.Logic
{
    public class TimetableLogic
    {
        public TimetableRepository _repo { get; set; }
        public TimetableLogic()
        {
            _repo = new TimetableRepository();
        }

        public async Task<List<Timetable>> GetTimetables()
        {
            List<Timetable> timetables = await _repo.GetTimetables();
            return timetables;
        }

        public async Task<Timetable> GetTimetable(int id)
        {
            return await _repo.GetTimetable(id);
        }

        public async Task<bool> InsertUpdateTimeTable(Timetable timetable)
        {
            return await _repo.InsertUpdateTimeTable(timetable);
        }
        public async Task<bool> DeleteTimetable(int id)
        {
            return await _repo.DeleteTimetable(id);
        }
    }
}
