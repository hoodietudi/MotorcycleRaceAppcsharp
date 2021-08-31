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
           
            var conStr = "Data Source=D:\\UBB\\anul 2\\sem2\\MPP\\motoare.db;Version=3";
            // var conStr = ConfigurationManager.ConnectionStrings["DbMotor"].ConnectionString;
            return new SQLiteConnection(conStr);
        }
    }
}