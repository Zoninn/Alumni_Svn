using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alumini.Core
{
    public static class CustomStatus
    {
        public enum UserStatus
        {
            Pending = 0,
            Approved = 1,
            NotApproved = 2
        }

        public enum ProfileInfoPercentage
        {
            BasicInfo = 1,
            BasicAndPersonal = 2,
            Complete = 5,
            EducationDetails = 3,
            ContactInformation = 4,
        }
        public enum UserTypes
        {
            Alumini = 1,
            Faculty = 2
        }


    }
}
