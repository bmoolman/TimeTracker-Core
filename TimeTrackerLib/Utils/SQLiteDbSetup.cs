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
                
                SqliteCommand command = new SqliteCommand();
                command.Connection = connection;
                connection.Open();

                string createSQL = "CREATE TABLE TTEvent" +
                                    "(" +
                                    "ProjectId INTEGER NOT NULL," +                                    
                                    "StartUTC TEXT NOT NULL," +
                                    "EndUTC TEXT NOT NULL," +
                                    "Comments VARCHAR(200) NOT NULL" +
                                    ")";
                command.CommandText = createSQL;
                command.ExecuteNonQuery();

                createSQL = "CREATE TABLE TTProject" +
                    "(" +
                    "ProjectId INTEGER PRIMARY KEY," +
                    "ProjectName VARCHAR(100) NOT NULL" +
                    ")";
                command.CommandText = createSQL;
                command.ExecuteNonQuery();

                createSQL = "INSERT INTO TTProject (ProjectName) VALUES ('General')";
                command.CommandText = createSQL;
                command.ExecuteNonQuery();

            }
        }
    }


}
