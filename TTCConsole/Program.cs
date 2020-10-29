using Dapper;
using Microsoft.Data.Sqlite;
using System;
using System.Linq;
using System.Threading;
using TimeTrackerLib.Models;

namespace TTCConsole
{
    class Program
    {
        static void CreateDb()
        {
            using (var connection = new SqliteConnection(@"Data Source=c:\temp\timetrack.db"))
            {

                var table = connection.Query("SELECT name FROM sqlite_master WHERE type='table' AND name = 'Product';");
                var tableName = table.FirstOrDefault();
                if (tableName == null)
                {
                    Console.WriteLine("Creating database");
                    connection.Execute("Create Table Product (" +
                        "Name VARCHAR(100) NOT NULL," +
                        "Description VARCHAR(1000) NULL);");
                }
                else
                {
                    Console.WriteLine("Database already exists");
                }
            }
        }
        static void Main(string[] args)
        {

            CreateDb();
            return;
            Console.WriteLine("Enter the name of the project to start logging time against it.");
            string projectName = Console.ReadLine();
            Console.WriteLine("Press ESC to stop");

            TTEvent ttevent = new TTEvent()
            {
                Id = 1,
                ProjectId = 1,
                ProjectName = projectName,
                StartUTC = DateTime.UtcNow
            };

            do
            {
                while (!Console.KeyAvailable)
                {
                    Thread.Sleep(2000);
                    TimeSpan span1 = DateTime.UtcNow - ttevent.StartUTC;
                    int totalSeconds1 = (int)span1.TotalSeconds;
                    Console.WriteLine($"{projectName}: running for {totalSeconds1} secs.");

                }
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);

            ttevent.EndUTC = DateTime.UtcNow;

            TimeSpan span = ttevent.EndUTC - ttevent.StartUTC;
            int totalSeconds = (int)span.TotalSeconds;

            Console.WriteLine($"Time logged for {projectName}: {totalSeconds} secs.");
            Console.WriteLine($"Start time UTC {ttevent.StartUTC}");
            Console.WriteLine($"End time UTC {ttevent.EndUTC}");

        }
    }
}
