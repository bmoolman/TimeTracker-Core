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

                string createSQL = "CREATE TABLE TTEvent (ProjectId INTEGER NOT NULL,StartUTC TEXT NOT NULL,EndUTC TEXT NOT NULL,Comments VARCHAR(200) NOT NULL, IsCustom INTEGER NOT NULL, LoggedMinutes INTEGER NOT NULL)";
                command.CommandText = createSQL;
                command.ExecuteNonQuery();

                createSQL = "CREATE TABLE TTProject" +
                    "(" +
                    "ProjectId INTEGER PRIMARY KEY," +
                    "ProjectName VARCHAR(100) NOT NULL," +
                    "ProjectDescription VARCHAR(100) NOT NULL" +
                    ")";
                command.CommandText = createSQL;
                command.ExecuteNonQuery();

                createSQL = "INSERT INTO TTProject (ProjectName,ProjectDescription) VALUES ('General','General or internal time')";
                command.CommandText = createSQL;
                command.ExecuteNonQuery();

                createSQL = "CREATE VIEW vwProjectInfo AS SELECT TTProject.ProjectId, ProjectName, StartUTC, EndUTC, Comments, IsCustom, LoggedMinutes FROM TTEvent INNER JOIN TTProject ON TTProject.ProjectId = TTEvent.ProjectId";
                command.CommandText = createSQL;
                command.ExecuteNonQuery();
                
                createSQL = "CREATE VIEW vwTTProjectSummary as SELECT TTProject.ProjectID, TTProject.ProjectName, ProjectDescription, IFNULL(SUM(LoggedMinutes),0) as SumLoggedMinutes FROM TTProject LEFT JOIN TTEvent ON TTProject.ProjectId = TTEvent.ProjectId GROUP BY TTProject.ProjectID,TTProject.ProjectName,TTProject.ProjectDescription";
                command.CommandText = createSQL;
                command.ExecuteNonQuery();

            }
        }
    }


}
