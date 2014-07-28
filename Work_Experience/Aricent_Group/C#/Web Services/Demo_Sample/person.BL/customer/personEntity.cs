using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace person.BL
{
    public class personEntity
    {
        public personEntity()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        int m_userId = 0;
        string m_firstName = string.Empty;
        string m_lastName = string.Empty;
        int m_age = 0;


        public int userId
        {

            get { return m_userId; }

            set { m_userId = value; }

        }

        public string firstName
        {

            get { return m_firstName; }

            set { m_firstName = value; }

        }

        public string lastName
        {

            get { return m_lastName; }

            set { m_lastName = value; }

        }

        public int age
        {

            get { return m_age; }

            set { m_age = value; }

        }
    }

        
}
