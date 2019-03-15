using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBProject
{
    /// <summary>
    /// This class of CLO contains specific Attributes according to the database CLO table
    /// </summary>
    class CLO
    {
        private int id;
        private string name;
        private DateTime dateCreated;
        private DateTime dateUpdated;

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public DateTime DateCreated { get => dateCreated; set => dateCreated = value; }
        public DateTime DateUpdated { get => dateUpdated; set => dateUpdated = value; }
    }
}
