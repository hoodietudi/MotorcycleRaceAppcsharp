using System.Data;
using DbUtils;
using Mono.Data.Sqlite;

namespace ConnectionUtils
{
    public class SqliteConnectionFactory : ConnectionFactory
    {
        public override IDbConnection CreateConnection()
        {
            var connectionString = "URI=file:/Fac/Sem4/MPP/Proiect/Db/MotorcycleContest.db";
            return new SqliteConnection(connectionString);
        }
    }
}