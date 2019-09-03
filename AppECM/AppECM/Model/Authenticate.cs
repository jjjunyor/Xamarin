using System;
using System.Collections.Generic;
using System.Text;

namespace AppECM.Model
{
    public class Authenticate
    {
        public Authenticate()
        {
            User = new User();
        }
        public bool IsAuthenticated { get; set; }
        public bool Error { get; set; }
        public string Message { get; set; }
        public User User { get; set; }
    }
}
