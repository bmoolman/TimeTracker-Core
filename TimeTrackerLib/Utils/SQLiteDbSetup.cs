using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TimeTrackerLib.Utils
{
    public class SQLiteDbSetup
    {
        internal string _datasource;

        public SQLiteDbSetup(string datasource)
        {
            _datasource = datasource;
        }

        public void CreateDb()
        {
            using (var connection = new SqliteConnection($"Data source = {_datasource}"))
            {
                string createSQL = "CREATE TABLE TTEvent" +
                                    "(" +
                                    "ProjectId int NOT NULL," +
                                    "ProjectName VARCHAR(100) NOT NULL," +
                                    "StartUTC TEXT NOT NULL," +
                                    "EndUTC TEXT NOT NULL," +
                                    "Comments VARCHAR(200) NOT NULL" +
                                    ")";
                connection.Open();
                SqliteCommand command = new SqliteCommand(createSQL, connection);
                command.ExecuteNonQuery();
    
            }
        }
    }


}
