using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBProject
{
    /// <summary>
    /// This class of Student contains specific Attributes according to the database Student table
    /// </summary>
    class Student
    {
        private int id;
        private string firstName;
        private string lastName;
        private string contact;
        private string email;
        private string registrationNo;
        private int status;

        public int Id { get => id; set => id = value; }

        public string FirstName
        {
            get
            { return firstName; }

            set
            {
                bool n = true;
                if (string.IsNullOrEmpty(value))
                { n = false; }
                else
                {
                    for (int i = 0; i < value.Length; i++)
                    {
                        //check that the given name should be alphabets
                        if (!Char.IsLetter(value[i]) && !Char.IsWhiteSpace(value[i]))
                        {
                            n = false;

                        }

                    }
                }
                if (n == true)
                {
                    firstName = value;
                }
                else
                {
                    //exception is raised in case of an error
                    throw new Exception();
                }
            }
        }


        public string LastName
        {
            get
            { return lastName; }

            set
            {
                bool n = true;
                if (string.IsNullOrEmpty(value))
                { n = false; }
                else
                {
                    for (int i = 0; i < value.Length; i++)
                    {
                        //check that the given name should be alphabets
                        if (!Char.IsLetter(value[i]) && !Char.IsWhiteSpace(value[i]))
                        {
                            n = false;

                        }

                    }
                }
                if (n == true)
                {
                    lastName = value;
                }
                else
                {
                    //exception is raised in case of an error
                    throw new Exception();
                }
            }
        }

        public string Contact { get => contact; set => contact = value; }

        public string Email
        {
            get
            { return email; }

            set
            {
                bool n = true;
                foreach (char c in value)
                {
                    //there should not be any spaces in the email id
                    if (Char.IsWhiteSpace(c))
                    {
                        n = false;
                    }
                }
                if (n)
                {
                    email = value;
                }
                else
                {
                    //exception is raised in case of an error
                    throw new Exception();
                }
            }
        }

        public string RegistrationNo { get => registrationNo; set => registrationNo = value; }
        public int Status { get => status; set => status = value; }

        
    }
}
