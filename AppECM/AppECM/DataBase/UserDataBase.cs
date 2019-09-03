using System.Collections.Generic;
using System.Linq;
using AppECM.Contratos;
using AppECM.Model;
using SQLite;
using Xamarin.Forms;


namespace AppECM.DataBase
{
    public class UserDataBase
    {
        private SQLiteConnection _dataBase;
        public UserDataBase()
        {
            _dataBase = DependencyService.Get<IDataBase>().GetConnection();
            _dataBase.CreateTable<User>();
        }
        public List<User> GetUser()
        {
            return _dataBase.Table<User>().ToList();
        }
        public User GetLastUser()
        {
            return _dataBase.Table<User>().LastOrDefault();
        }
        public int SaveUser(User user)
        {
            return _dataBase.Insert(user);
        }
        public int UpdateUser(User user)
        {
            return _dataBase.Update(user);
        }
        public int DeleteUser(User user)
        {
            return _dataBase.Delete(user);
        }
        public int DeleteAllUser()
        {
            return _dataBase.DeleteAll<User>();
        }
    }
}

