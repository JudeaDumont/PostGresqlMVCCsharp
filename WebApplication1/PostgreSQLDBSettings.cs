using System;
using System.Collections.Generic;
using LinqToDB.Configuration;

public class ConnectionStringSettings : IConnectionStringSettings
{
	public string ConnectionString { get; set; }
	public string Name { get; set; }
	public string ProviderName { get; set; }
	public bool IsGlobal => false;
}

public class PostgreSQLDbSettings : ILinqToDBSettings
{
	public IEnumerable<IDataProviderSettings> DataProviders
	{
		get { yield break; }
	}

	public string DefaultConfiguration => "PostgresReader";
	public string DefaultDataProvider => "PostgreSQL";

	public string ConnectionString { get; set; }

	public PostgreSQLDbSettings(string connectionString)
	{
		ConnectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
	}

	public IEnumerable<IConnectionStringSettings> ConnectionStrings
	{
		get
		{
			yield return
					new ConnectionStringSettings
					{
						Name = "PostgresReader",
						ProviderName = "PostgreSQL",
						ConnectionString = ConnectionString
					};
		}
	}
}