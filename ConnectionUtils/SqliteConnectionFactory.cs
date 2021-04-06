using System;
using System.Data;
using DbUtils;
using System.Data.SQLite;
using System.Configuration;


namespace ConnectionUtils
{
    public class SqliteConnectionFactory : ConnectionFactory
    {
        public override IDbConnection CreateConnection()
        {
           
            var conStr = "Data Source=C:\\Fac\\Sem4\\MPP\\Proiect\\Db\\MotorcycleContest.db;Version=3";
            // var conStr = ConfigurationManager.ConnectionStrings["DbMotor"].ConnectionString;
            return new SQLiteConnection(conStr);
        }
    }
}