﻿using Dapper;
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
            string SQL = "INSERT INTO TTEvent (ProjectId, StartUTC, EndUTC, Comments, IsCustom, LoggedMinutes) VALUES (@ProjectId, @StartUTC, @EndUTC, @Comments,@IsCustom, @LoggedMinutes)";
            _dbConnection.Execute(SQL, ttevent);           
        }

        public List<TTEvent> GetProjectEvents(int v)
        {
            return _dbConnection.Query<TTEvent>("SELECT * FROM TTEvent WHERE ProjectId = @ProjectId", new { ProjectId = v }).ToList();

        }

        public List<TTProjectSummary> GetProjects()
        {
            return _dbConnection.Query<TTProjectSummary>("SELECT * FROM vwTTProjectSummary").ToList();
        }

        public int CreateNewProject(TTProject ttProject)
        {
            string SQL = "INSERT INTO TTProject (ProjectName, ProjectDescription) VALUES (@ProjectName,@ProjectDescription); SELECT last_insert_rowid()";
            

            var ret = _dbConnection.Query(SQL, ttProject).SingleOrDefault();

            return Convert.ToInt32(((object[])((IDictionary<string, object>)ret).Values)[0]);
           
        }
    }
}
