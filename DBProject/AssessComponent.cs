using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBProject
{
    class AssessComponent
    {
        private int id;
        private string name;
        private int rubricId;
        private int totalMarks;
        private DateTime dateCreated;
        private DateTime dateUpdated;
        private int assessmentId;

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public int RubricId { get => rubricId; set => rubricId = value; }
        public int TotalMarks { get => totalMarks; set => totalMarks = value; }
        public DateTime DateCreated { get => dateCreated; set => dateCreated = value; }
        public DateTime DateUpdated { get => dateUpdated; set => dateUpdated = value; }
        public int AssessmentId { get => assessmentId; set => assessmentId = value; }
    }
}
