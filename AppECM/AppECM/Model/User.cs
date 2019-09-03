using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppECM.Model
{
    [Table("User")]
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int ResourceID { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string TokenFireBase { get; set; }


        public User getUser()
        {
            return new DataBase.UserDataBase().GetUser().LastOrDefault();
        }
        public void saveUser(User user)
        {
            if (user != null)
            {
                new DataBase.UserDataBase().SaveUser(user);
            }
        }
        public void updateUser(User user)
        {
            if (user != null)
            {
                new DataBase.UserDataBase().UpdateUser(user);
            }
        }
        public void setToken(string token)
        {
            if (!string.IsNullOrEmpty(token))
            {
                this.TokenFireBase = token;
            }
        }
    }
}
