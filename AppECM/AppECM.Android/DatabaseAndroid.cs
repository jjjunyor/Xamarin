using System;
using System.Collections.Generic;
using System.Linq;
using SQLite;
using System.IO;
using AppECM.Droid;
using AppECM.Contratos;


[assembly: Xamarin.Forms.Dependency(typeof(DatabaseAndroid))]
namespace AppECM.Droid
{
    public class DatabaseAndroid : IDataBase
    {
        public SQLiteConnection GetConnection()
        {
            var nomeDB = "Mobile.db3";
            var caminhoDB = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), nomeDB);
            return new SQLiteConnection(caminhoDB);
        }
    }
}