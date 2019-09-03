using SQLite;


namespace AppECM.Contratos
{
    public interface IDataBase
    {
        SQLiteConnection GetConnection();
    }
}
