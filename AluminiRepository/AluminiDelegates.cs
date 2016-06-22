using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluminiRepository
{
    public delegate void OnUserRegistrationCompleted(object sender, UserRegistrationDoneEventArgs eventArgs);

    public class UserRegistrationDoneEventArgs : System.EventArgs
    {
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string Role { get; set; }
    }
}
