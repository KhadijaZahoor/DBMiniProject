using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBProject
{
    /// <summary>
    /// This class of StudentAttendance contains specific Attributes according to the database StudentAttendance table
    /// </summary>
    class StudentAttendance
    {
        private int attendanceId;
        private int studentId;
        private int attendanceStatus;

        public int AttendanceId { get => attendanceId; set => attendanceId = value; }
        public int StudentId { get => studentId; set => studentId = value; }
        public int AttendanceStatus { get => attendanceStatus; set => attendanceStatus = value; }
    }
}
