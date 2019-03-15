using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBProject
{
    /// <summary>
    /// This class of Rubric contains specific Attributes according to the database Rubric table
    /// </summary>
    class Rubric
    {
        private int id;
        private string details;
        private int cloId;

        public int Id { get => id; set => id = value; }
        public string Details { get => details; set => details = value; }
        public int CloId { get => cloId; set => cloId = value; }
    }
}
