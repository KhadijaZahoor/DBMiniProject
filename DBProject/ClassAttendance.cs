using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBProject
{
    /// <summary>
    /// This class of ClassAttendance contains specific Attributes according to the database ClassAttendance table
    /// </summary>
    class ClassAttendance
    {
        private int id;
        private DateTime attendanceDate;

        public int Id { get => id; set => id = value; }
        public DateTime AttendanceDate { get => attendanceDate; set => attendanceDate = value; }
    }
}
