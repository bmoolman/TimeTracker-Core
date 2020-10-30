using Dapper;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
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
            //string SQL = "INSERT INTO Test (ProjectID, Comment) VALUES (1,'HW') ";
            string SQL = "INSERT INTO TTEvent (ProjectId, ProjectName, StartUTC, EndUTC, Comments) VALUES (@ProjectId, @ProjectName, @StartUTC, @EndUTC, @Comments)";

                //@"INSERT INTO [dbo].[Customer]([FirstName], [LastName], [State], [City], [IsActive], [CreatedOn]) VALUES (@FirstName, @LastName, @State, @City, @IsActive, @CreatedOn)";

            var result = _dbConnection.Execute(SQL, ttevent);
            //, new
            //{
            //    customerModel.FirstName,
            //    customerModel.LastName,
            //    StateModel.State,
            //    CityModel.City,
            //    isActive,
            //    CreatedOn = DateTime.Now
            //});
        }
    }
}
