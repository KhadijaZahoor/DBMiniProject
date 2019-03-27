using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBProject
{
    class Assessment
    {
        private int id;
        private string title;
        private DateTime dateCreated;
        private int totalMarks;
        private int totalWeightage;

        public int Id { get => id; set => id = value; }
        public string Title { get => title; set => title = value; }
        public DateTime DateCreated { get => dateCreated; set => dateCreated = value; }
        public int TotalMarks { get => totalMarks; set => totalMarks = value; }
        public int TotalWeightage { get => totalWeightage; set => totalWeightage = value; }
    }
}
