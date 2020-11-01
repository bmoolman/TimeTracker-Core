using Dapper;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using TimeTrackerLib.Models;

namespace TimeTrackerLib.Repositories
{

    public class SQLiteRepository
    {
       
        private readonly IDbConnection _dbConnection;
        public SQLiteRepository(string datasource)
        {
            _dbConnection = new SqliteConnection($"Data source = {datasource}");
        }

        public void SaveEvent(TTEvent ttevent)
        {
            string SQL = "INSERT INTO TTEvent (ProjectId, ProjectName, StartUTC, EndUTC, Comments) VALUES (@ProjectId, @ProjectName, @StartUTC, @EndUTC, @Comments)";
            _dbConnection.Execute(SQL, ttevent);           
        }

        public List<TTEvent> GetProjectEvents(int v)
        {
            return _dbConnection.Query<TTEvent>("SELECT * FROM TTEvent WHERE ProjectId = @ProjectId", new { ProjectId = v }).ToList();

        }
    }
}
