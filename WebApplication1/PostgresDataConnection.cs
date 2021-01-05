using LinqModels;
using LinqToDB;
using LinqToDB.Configuration;
using LinqToDB.Data;

public class PostgresDataConnection : DataConnection
{
    public ITable<User> Users => GetTable<User>();
}