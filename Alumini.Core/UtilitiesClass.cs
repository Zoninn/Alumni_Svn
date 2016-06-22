using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alumini.Core
{
    public class UtilitiesClass
    {
        public const string BatchVisibleToAll = "Visible To All";
        public const string BatchName = "My BatchMates";
        public const string ImagePath = "~/UserPostingImages/";
        public const string DeleteMessage = "Deleted Successfully";
        public const string SuccessMessage = "Updated Successfully";
    }
    public enum Roles
    {
        RoleId1 = 1, RoleId2 = 2, RoleId3 = 3, RoleId4 = 4
    }
    public class LoginPages
    {
        public const string Login = "Login";
        public const string Account = "Account";
    }
    public class AlumniWhiteBoard
    {
        public const string Index = "Index";
        public const string WhiteBoard = "WhiteBoard";
    }
    public class AreasforAlumni
    {
        public const string Alumini = "Alumini";
        public const string Faculty = "Faculty";
        public const string AlumniFaculty = "AlumniFaculty";
    }
}
